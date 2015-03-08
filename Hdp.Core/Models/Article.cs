using System;

namespace Hdp.Core.Models
{
    public class Article
    {
        public string Category {get;set;} = "Politika";
        public string Title {get;set;}
        public string ImageUrl {get;set;}

        public static Article Create (string title) 
        {
            var random = new Random ();

            var article = new Article {
                Title = title,
                ImageUrl = "http://lorempixel.com/640/480/city?a=" + random.Next (1, 1000)
            };

            return article;
        }
    }
}

