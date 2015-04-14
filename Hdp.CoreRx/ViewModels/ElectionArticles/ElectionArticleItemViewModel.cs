using System;
using Hdp.CoreRx.Models;
using ReactiveUI;

namespace Hdp.CoreRx.ViewModels.ElectionArticles
{
    public class ElectionArticleItemViewModel : ReactiveObject, ICanGoToViewModel
    {
        private int id;
        public int Id {
            get { return this.id; }
            set { this.RaiseAndSetIfChanged (ref this.id, value); }
        }

        private string title;
        public string Title {
            get { return this.title; }
            set { this.RaiseAndSetIfChanged (ref this.title, value); }
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

        private string videoUrl;
        public string VideoUrl {
            get { return this.videoUrl; }
            set { this.RaiseAndSetIfChanged (ref this.videoUrl, value); }
        }

        private string videoImageUrl;
        public string VideoImageUrl {
            get { return this.videoImageUrl; }
            set { this.RaiseAndSetIfChanged (ref this.videoImageUrl, value); }
        }

        private string videoId;
        public string VideoId {
            get { return this.videoId; }
            set { this.RaiseAndSetIfChanged (ref this.videoId, value); }
        }

        private ElectionArticle.MediaType mediaType;
        public ElectionArticle.MediaType MediaType {
            get { return this.mediaType; }
            set { this.RaiseAndSetIfChanged (ref this.mediaType, value); }
        }

        private DateTime createdAt;
        public DateTime CreatedAt {
            get { return this.createdAt; }
            set { this.RaiseAndSetIfChanged (ref this.createdAt, value); }
        }

        public ElectionArticle Model { get; set; }

        public IReactiveCommand<object> GoToCommand { get; private set; }

        public ElectionArticleItemViewModel (ElectionArticle model, Action<ElectionArticleItemViewModel> gotoCommand)
        {
            Model = model;

            Id = model.Id;
            Title = model.Title;
            Body = model.Body;
            MediaType = model.Type;
            VideoUrl = model.VideoUrl;
            VideoImageUrl = model.VideoImageUrl;
            VideoId = model.VideoId;
            ImageUrl = model.ImageUrl;
            CreatedAt = model.CreatedAt;

            GoToCommand = ReactiveCommand.Create ();
            GoToCommand.Subscribe (x => gotoCommand (this));
        }
    }
}

