using System;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using Hdp.Core.Models;


namespace Hdp.Core.ViewModels
{
    public class ArticlesViewModel : MvxViewModel
    {
        private List<string> _articles;

        public List<Article> Articles {
            get { return _articles; }
            set {
                _articles = value;
                RaisePropertyChanged (() => Articles);
            }
        }

        public ArticlesViewModel ()
        {
            var newArticles = new List<string> ();

            for (int i = 0; i < 20; i++) {
                newArticles.Add (Article.Create("Article " + i.ToString()));
            }

            Articles = newArticles;
        }
    }
}

