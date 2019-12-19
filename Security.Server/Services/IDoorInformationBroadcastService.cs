using System;

namespace Security.Server.Services
{
    public interface IDoorInformationBroadcastService
    {
        void BroadcastAllDoorsInformation(string monitorId);
        void BroadcastAllDoorsInformation();
    }
}