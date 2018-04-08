using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felix.Messaging.Messages.Clients.FileClient
{
    public class FileMessage
    {
        public string FilePath { get; set; }

        public FileMessage() { }

        public FileMessage(string filePath)
        {
            FilePath = filePath;
        }
    }
    public class FileReadPayload : FileMessage
    {
        public FileReadPayload() { }

        public FileReadPayload(string filePath) : base(filePath)
        {
        }
    }

    public class FileWritePayload : FileMessage
    {
        public string FileContent { get; set; }

        public bool Append { get; set; }

        public FileWritePayload() { }

        public FileWritePayload(string filePath, bool append, string content) : base(filePath)
        {
            FileContent = content;

            Append = append;
        }
    }

    public class FileReadResponse : FileMessage
    {
        public string Contents { get; set; }

        public FileReadResponse() { }

        public FileReadResponse(string filePath, string contents) : base(filePath)
        {
            Contents = contents;
        }
    }

    public class FileWriteResponse : FileMessage
    {
        public bool Result { get; set; }

        public FileWriteResponse() { }

        public FileWriteResponse(string filePath, bool result) : base(filePath)
        {
            Result = result;
        }
    }

    public class FileStatePayload : FileMessage
    {
        public bool Append { get; set; }

        public string Contents { get; set; }

        public bool Success { get; set; }

        public FileStatePayload(string filePath, bool append, string contents = null, bool success = true)
            : base(filePath)
        {
            Append = append;

            Contents = contents;

            Success = success;
        }
    }
}
