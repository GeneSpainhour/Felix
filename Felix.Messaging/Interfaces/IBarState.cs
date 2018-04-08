namespace Felix.Messaging.Interfaces
{
    public interface IBarState: IState
    {
        int BarId { get; set; }
        bool Result { get; set; }
    }
}