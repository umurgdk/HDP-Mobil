using System;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels
{
    public class ArticleViewModel : BaseViewModel
    {
        private string articleTitle;
        public string ArticleTitle {
            get { return this.articleTitle; }
            set { this.RaiseAndSetIfChanged (ref this.articleTitle, value); }
        }

        private string body;
        public string Body {
            get { return this.body; }
            set { this.RaiseAndSetIfChanged (ref this.body, value); }
        }

        private string imageUrl;
        public string ImageUrl {
            get { return this.imageUrl; }
            set { this.RaiseAndSetIfChanged (ref this.imageUrl, value); }
        }

        private Article model;
        public Article Model {
            get { return this.model; }
            set { this.RaiseAndSetIfChanged (ref this.model, value); }
        }
    }
}

