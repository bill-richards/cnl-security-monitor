namespace Security.Common
{
    public interface IDoorControlMessageFactory
    {
        IDoorControlMessage CreateDoorControlMessage(int doorId, DoorStates doorAction);
    }
}