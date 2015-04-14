using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hdp.CoreRx.Models
{
    public class ElectionArticle
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string Body {get;set;}

        [JsonProperty(PropertyName="media_type")]
        public MediaType Type {get;set;}

        [JsonProperty(PropertyName="image_url")]
        public string ImageUrl {get;set;}

        [JsonProperty(PropertyName="video_url")]
        public string VideoUrl {get;set;}

        [JsonProperty(PropertyName="created_at")]
        public DateTime CreatedAt { get; set; }

        public string VideoImageUrl { get; set; }
        public string VideoId { get; set; }

        public enum MediaType
        {
            None,
            Image,
            Video
        }
    }
}

