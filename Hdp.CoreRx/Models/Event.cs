using System;
using Newtonsoft.Json;

namespace Hdp.CoreRx.Models
{
    public class Event
    {
        public int      Id          { get; set; }
        public string   Title       { get; set; }
        public DateTime Time        { get; set; }
        public string   Location    { get; set; }

        [JsonProperty(PropertyName="created_at")]
        public DateTime CreatedAt   { get; set; }
    }
}

