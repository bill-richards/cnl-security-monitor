namespace Security.Common
{
    public interface IDoorControlMessage
    {
        int DoorId { get; }
        DoorStates DoorAction { get; }
        string AsJson();
    }
}