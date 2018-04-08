using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace Felix.Messaging.Messages.Clients.FileClient
{
    public class FileState : Felix.Messaging.Messages.State.State
    {
        public FileState(FileStatePayload payload)
            : base("FileState", JsonConvert.SerializeObject(payload)) { }
    }
}
