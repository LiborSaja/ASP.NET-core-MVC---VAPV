using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace VAPV.DTO {
    public class CustomDateTimeConverter : DateTimeConverterBase {
        private readonly string _format = "dd-MM-yyyy";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value is DateTime dateTime) {
                writer.WriteValue(dateTime.ToString(_format));
            }
            else {
                throw new JsonSerializationException("Expected date object value.");
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if (reader.TokenType == JsonToken.String) {
                var dateStr = reader.Value.ToString();
                if (DateTime.TryParseExact(dateStr, _format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)) {
                    return date;
                }
            }
            throw new JsonSerializationException("Expected date string.");
        }
    }
}
