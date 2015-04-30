using System;
using Newtonsoft.Json;

using Hdp.CoreRx.Extensions;
using System.Diagnostics;

namespace Hdp.CoreRx.Helpers
{
    public class RailsDateConverter : JsonConverter
    {
        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
        {
            var date = (DateTime)value;
            writer.WriteRaw (date.ToUnixTimestamp ().ToString ());
        }

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return DateTime.Parse ((string)reader.Value);
        }

        public override bool CanConvert (Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}

