using System;

namespace Security
{
    public interface IDoor
    {
        int Id { get; }

        string Label { get; }
        DoorStates State { get; }
        IDoor WhoseLabelIs(string label);
        IDoor SetStateTo(DoorStates state);
        IDoor And { get; }
        string MonitorId { get; }
        IDoor SetMonitorId(string monitorId);
    }
}