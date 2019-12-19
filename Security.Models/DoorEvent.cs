using System;

namespace Security.Models
{
    public class DoorEvent : IDoorEvent
    {
        public DoorEvent() 
            => Time = DateTime.Now;
        public IDoorEvent And => this;

        public int Id { get; set; }
        public int DoorId { get; set; }
        public DateTime Time { get; set; }
        public string Description { get; set; }

        public IDoorEvent ForDoorWithId(int doorId)
        {
            DoorId = doorId;
            return this;
        }

        public IDoorEvent SetTheEventDescriptionTo(DoorStates doorState)
        {
            Description = doorState.ToString();
            return this;
        }
    }
}