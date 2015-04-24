using System;
using ReactiveUI;
using Hdp.CoreRx.Models;

namespace Hdp.CoreRx.ViewModels
{
    public class ArticleItemViewModel : ReactiveObject, ICanGoToViewModel
    {
        private bool isHidden;
        public bool IsHidden {
            get { return this.isHidden; }
            set { this.RaiseAndSetIfChanged (ref this.isHidden, value); }
        }

        private string title;
        public string Title {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged (ref this.title, value); }
        }

        private string imageUrl;
        public string ImageUrl {
            get { return this.imageUrl; }
            set { this.RaiseAndSetIfChanged (ref this.imageUrl, value); }
        }

        private string category;
        public string Category {
            get { return this.category; }
            set { this.RaiseAndSetIfChanged (ref this.category, value); }
        }

        private string body;
        public string Body {
            get { return this.body; }
            set { this.RaiseAndSetIfChanged (ref this.body, value); }
        }

        private string summary;
        public string Summary {
            get { return this.summary; }
            set { this.RaiseAndSetIfChanged (ref this.summary, value); }
        }

        private DateTime createdAt;
        public DateTime CreatedAt {
            get { return this.createdAt; }
            set { this.RaiseAndSetIfChanged (ref this.createdAt, value); }
        }

        public Article Model { get; set; }


        public IReactiveCommand<object> GoToCommand { get; private set; }


        public ArticleItemViewModel (Article model, Action<ArticleItemViewModel> gotoCommand)
        {
            Model = model;

            Title = model.Title;
            Category = model.Category;
            ImageUrl = model.ImageUrl;
            Body = model.Body;
            Summary = model.Summary;
            CreatedAt = model.CreatedAt;

            GoToCommand = ReactiveCommand.Create ();
            GoToCommand.Subscribe (x => gotoCommand (this));
        }
    }
}

