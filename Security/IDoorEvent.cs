using System;

namespace Security
{
    public interface IDoorEvent
    {
        int Id { get; }
        int DoorId { get; }
        DateTime Time { get; }
        string Description { get; }
        IDoorEvent ForDoorWithId(int doorId);
        IDoorEvent SetTheEventDescriptionTo(DoorStates doorState);
        IDoorEvent And { get; }
    }
}