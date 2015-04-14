using System;
using Newtonsoft.Json;
using Refit;

namespace Hdp.CoreRx.Models
{
    public class Article
    {
        public string Category {get;set;} = "Politika";
        public string Title {get;set;}

        [JsonProperty(PropertyName="image_url")]
        public string ImageUrl {get;set;}

        public string Body {get;set;}
    }
}

