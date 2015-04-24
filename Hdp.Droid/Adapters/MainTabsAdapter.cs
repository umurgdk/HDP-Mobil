using System;
using Java.Lang;

using System.Collections.Generic;
using Hdp.Droid.Fragments;
using Android.Runtime;
using Android.Support.V4.App;
using Hdp.CoreRx.ViewModels.ElectionArticles;
using Splat;
using Hdp.CoreRx.Services;
using Hdp.CoreRx.ViewModels;
using Hdp.CoreRx.ViewModels.Events;
using Hdp.CoreRx.ViewModels.Organization;

namespace Hdp.Droid.Adapters
{
    public class MainTabsAdapter : FragmentStatePagerAdapter
    {
        ICharSequence[] _titles;
        List<Fragment> _fragments;

        public MainTabsAdapter (FragmentManager fm) : base (fm)
        {
            var serviceConstructor = Locator.Current.GetService<IServiceConstructor> ();

            var electionArticlesViewModel = serviceConstructor.Construct<ElectionArticlesViewModel> ();
            var newsViewModel = serviceConstructor.Construct<NewsViewModel> ();
            var eventsViewModel = serviceConstructor.Construct<EventsViewModel> ();
            var organizationMenuViewModel = serviceConstructor.Construct<OrganizationMenuViewModel> ();

            _titles = CharSequence.ArrayFromStringArray(new string[] {"Secim", "Haberler", "Etkinlikler", "Parti"});
            _fragments = new List<Fragment> {
                new ElectionArticlesFragment(electionArticlesViewModel),
                new NewsArticlesFragment(newsViewModel),
                new EventsFragment(eventsViewModel),
                new OrganizationFragment(organizationMenuViewModel)
            };
        }

        public override int Count {
            get {
                return 4;
            }
        }

        public override Fragment GetItem (int position)
        {
            return _fragments [position];
        }

        public override ICharSequence GetPageTitleFormatted (int position)
        {
            return _titles [position];
        }
    }
}

