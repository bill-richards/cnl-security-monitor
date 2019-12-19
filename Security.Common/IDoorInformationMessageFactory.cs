using System;

namespace Security.Common
{
    public interface IDoorInformationMessageFactory
    {
        IDoorInformationMessage CreateDoorInformationMessage(IDoor door);
    }
}