namespace Security.Common.Exchanges
{
    public static class RoutingKeys
    {
        public static string AllDoorMessagesRoutingKey = "cnl.#";
        public static string DoorRegisterRoutingKey = "cnl.door.register";
        public static string DoorInformationRoutingKey = "cnl.door.info";
        public static string DoorInformationRequestRoutingKey = "cnl.door.get-info";
        public static string SpecificDoorRoutingKey = "cnl.door.control.id_";
    }
}