using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackupsExtra.Classes
{
    public class Conversion : JsonConverter
    {
        public override bool CanConvert(Type type)
        {
            return type == typeof(FileInfo);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string div)
            {
                return new FileInfo(div);
            }

            throw new ArgumentOutOfRangeException(nameof(reader));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is FileInfo fileInfo))
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            writer.WriteValue(fileInfo.FullName);
        }
    }
}