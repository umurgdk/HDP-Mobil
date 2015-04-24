using System;
using Newtonsoft.Json;
using Refit;
using Hdp.CoreRx.Helpers;

namespace Hdp.CoreRx.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Category {get;set;} = "Politika";
        public string Title {get;set;} = "";

        [JsonProperty(PropertyName="image_url")]
        public string ImageUrl {get;set;} = "";

        [JsonProperty(PropertyName="created_at")]
        public DateTime CreatedAt { get; set; }

        public string Body {get;set;} = "";

        public string Summary {get;set;} = "";
    }
}

