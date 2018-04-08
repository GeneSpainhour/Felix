using System.Text;

namespace Felix.Messaging.Interfaces
{
    public interface IFileState: IState
    {
        bool Append { get; set; }
        string Contents { get; set; }
        string FilePath { get; set; }
        bool Success { get; set; }
    }
}