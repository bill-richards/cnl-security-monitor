using Security.Common;

namespace Security.Server.Services
{
    public interface IDoorRegistrationService
    {
        void RegisterNewDoor(string label, DoorStates initialState);
    }
}