namespace Felix.Messaging.Interfaces
{
    public interface IAction
    {
        string Type { get; set; }

        string Payload { get; set; }
    }
  
}