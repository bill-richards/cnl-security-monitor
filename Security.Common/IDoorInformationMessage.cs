using Security.Models;

namespace Security.Common
{
    public interface IDoorInformationMessage
    {
        string MonitorId { get; }
        Door Door { get; }
        string AsJson();
    }
}