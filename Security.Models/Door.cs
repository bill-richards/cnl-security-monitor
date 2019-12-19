using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Security.Models
{
    public class Door : IDoor
    {
        public int Id { get; set; }

        public DoorStates State { get; set; }
        public string Label { get; set; }

        public IDoor WhoseLabelIs(string label)
        {
            Label = label;
            return this;
        }

        public IDoor SetStateTo(DoorStates state)
        {
            State = state;
            return this;
        }

        public IDoor And => this;

        [NotMapped]
        public string MonitorId { get; set; } = string.Empty;

        public IDoor SetMonitorId(string monitorId)
        {
            MonitorId = monitorId;
            return this;
        }
    }
}