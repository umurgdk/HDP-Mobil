using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Widget = Android.Support.V7.Widget;

using ReactiveUI;
using ReactiveUI.AndroidSupport;
using com.refractored;
using Splat;

using Hdp.CoreRx.Services;
using Hdp.Droid.Adapters;
using Hdp.Droid.Services;
using Hdp.CoreRx;
using Android.Support.V4.App;

namespace Hdp.Droid
{
    [Activity (Label = "HDP", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.HDP")]
    public class MainActivity : ReactiveActionBarActivity
    {
        Widget.Toolbar _toolbar;

        MainTabsAdapter _tabsAdapter;
        ViewPager _viewPager;
        PagerSlidingTabStrip _tabs;

        private bool isBackButtonVisible;
        public bool IsBackButtonVisible {
            get { return this.isBackButtonVisible; }
            set { this.RaiseAndSetIfChanged (ref this.isBackButtonVisible, value); }
        }

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            _toolbar = FindViewById<Widget.Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (_toolbar);

            _tabsAdapter = new MainTabsAdapter (SupportFragmentManager);
            _viewPager = FindViewById<ViewPager> (Resource.Id.pager);
            _viewPager.Adapter = _tabsAdapter;

            _tabs = FindViewById<PagerSlidingTabStrip> (Resource.Id.tabs);
            _tabs.SetViewPager (_viewPager);

            IsBackButtonVisible = false;

            this.WhenAnyValue (x => x.IsBackButtonVisible)
                .Subscribe (visible => {
                    SupportActionBar.SetDisplayHomeAsUpEnabled(visible);
                });
        }

        public override bool OnSupportNavigateUp ()
        {
            base.OnSupportNavigateUp ();

            Android.Support.V4.App.Fragment fragment = SupportFragmentManager.FindFragmentByTag ("MODAL");
            if (fragment != null)
            {
                SupportFragmentManager
                    .BeginTransaction ()
                    .Remove (fragment)
                    .Commit ();

                IsBackButtonVisible = false;

                return true;
            }

            return true;
        }

        public override void OnBackPressed ()
        {
            base.OnBackPressed ();

            IsBackButtonVisible = false;
        }
    }
}


