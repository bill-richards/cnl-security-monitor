using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Security.Common
{
    public sealed class JsonSerializer : IJsonSerializer
    {
        public string ConvertObjectToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, DefaultSettings);
        }
        public T ConvertObjectFromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, DefaultSettings);
        }

        private JsonSerializerSettings DefaultSettings => new JsonSerializerSettings
        {
            Converters = new JsonConverter[] { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            Error = HandleSerializationError
        };

        private void HandleSerializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;

            // We're just swallowing the error for brevity
        }
    }
}