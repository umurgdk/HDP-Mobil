using System;
using ReactiveUI;

namespace Hdp.CoreRx.Models
{
    public class AppState
    {
        public ReactiveList<Article> Articles { get; private set; }
        public ReactiveList<ElectionArticle> ElectionArticles { get; private set;}
        public ReactiveList<Event> Events { get; private set; }
        public ReactiveList<OrganizationMenuItem> OrganizationMenuItems { get; private set; }

        public AppState ()
        {
            Articles = new ReactiveList<Article> ();
            ElectionArticles = new ReactiveList<ElectionArticle> ();
            Events = new ReactiveList<Event> ();
            OrganizationMenuItems = new ReactiveList<OrganizationMenuItem> ();
        }
    }
}

