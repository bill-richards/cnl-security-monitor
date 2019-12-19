using System;

namespace Security.Common
{
    public interface IInformationRequestMessage
    {
        string AsJson(string monitorId);
    }
}