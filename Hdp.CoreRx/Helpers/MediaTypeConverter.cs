using System;
using Newtonsoft.Json;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.Helpers
{
    public class MediaTypeConverter : JsonConverter
    {
        public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
        {
            
        }

        public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            ElectionArticle.MediaType type;
            if (Enum.TryParse ((string)reader.Value, true, out type))
            {
                return type;
            }
            else
            {
                return ElectionArticle.MediaType.None;
            }
        }

        public override bool CanConvert (Type objectType)
        {
            return objectType == typeof(ElectionArticle.MediaType);
        }
    }
}

