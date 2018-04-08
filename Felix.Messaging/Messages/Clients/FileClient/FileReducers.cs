using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felix.Messaging.Interfaces;
using Felix.Messaging.Messages.Reducers;
using Newtonsoft.Json;

namespace Felix.Messaging.Messages.Clients.FileClient
{
    public class FileReadReducer : ActionReducer
    {
        public override async Task<string> Reduce(IAction action)
        {
            string stateString = string.Empty;

            try
            {
                FileReadPayload payload = JsonConvert.DeserializeObject<FileReadPayload>(action.Payload);

                string path = payload.FilePath;

                if (!File.Exists(payload.FilePath))
                {
                    throw new ArgumentException($"Unable to find file {path}");
                }

                string contents;

                byte[] byteBuffer = new byte[1024];

                StringBuilder bldr = new StringBuilder();

                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    contents = await reader.ReadToEndAsync();
                }

                string testing = "This is a string for testing.";

                var fileStatePayload = new FileStatePayload(path, false, testing);

                var state = new FileState(fileStatePayload);

                Debug.WriteLine($"fileState:\r\nPath: {fileStatePayload.FilePath}\r\nContents: {fileStatePayload.Contents}\r\n");

                stateString = JsonConvert.SerializeObject(state);

                Debug.WriteLine($"serialized filestate:\r\n{stateString}");
            }
            catch (Exception e)
            {

                Debug.WriteLine($"Error: {e.Message}");

                Debugger.Break();
            }

            return stateString;
        }
    }

    public class FileWriteReducer : ActionReducer
    {
        public override async Task<string> Reduce(IAction action)
        {
            FileWritePayload payload = JsonConvert.DeserializeObject<FileWritePayload>(action.Payload);

            string contents = payload.FileContent;

            if (string.IsNullOrEmpty(contents))
            {
                throw new ArgumentException($"Contents cannot be empty");
            }

            string path = payload.FilePath;

            byte[] byteBuffer = Encoding.UTF8.GetBytes(contents);

            bool bAppend = payload.Append;

            bool success = false;

            try
            {
                using (FileStream fileStream = bAppend ? new FileStream(path, FileMode.Append, FileAccess.Write) : new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await fileStream.WriteAsync(byteBuffer, 0, byteBuffer.Length);

                    success = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            var fileStatePayload = new FileStatePayload(path, bAppend, contents);

            var state = new FileState(fileStatePayload);

            return JsonConvert.SerializeObject(state);
        }
    }
}
