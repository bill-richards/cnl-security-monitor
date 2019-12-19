namespace Security.Common
{
    public interface IJsonSerializer
    {
        string ConvertObjectToJson(object obj);
        T ConvertObjectFromJson<T>(string json);
    }
}