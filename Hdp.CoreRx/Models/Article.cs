using System;

namespace Hdp.CoreRx.Models
{
    public class Article
    {
        public string Category {get;set;} = "Politika";
        public string Title {get;set;}
        public string ImageUrl {get;set;}
        public string Body {get;set;}

        public static Article Create (string title) 
        {
            var random = new Random ();

            var article = new Article {
                Title = title,
                ImageUrl = "http://lorempixel.com/640/480/city?a=" + random.Next (1, 1000),
                Body = "Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Etiam porta sem malesuada magna mollis euismod. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Duis mollis, est non commodo luctus, nisi erat porttitor ligula, eget lacinia odio sem nec elit. Nullam quis risus eget urna mollis ornare vel eu leo.\n\nVestibulum id ligula porta felis euismod semper. Praesent commodo cursus magna, vel scelerisque nisl consectetur et. Sed posuere consectetur est at lobortis. Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor auctor. Vestibulum id ligula porta felis euismod semper. Sed posuere consectetur est at lobortis.\n\nEtiam porta sem malesuada magna mollis euismod. Cras mattis consectetur purus sit amet fermentum. Donec sed odio dui. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam id dolor id nibh ultricies vehicula ut id elit."
            };

            return article;
        }
    }
}

