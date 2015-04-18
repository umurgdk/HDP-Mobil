﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Widget = Android.Support.V7.Widget;

using com.refractored;
using Splat;

using Hdp.CoreRx.Services;
using Hdp.Droid.Adapters;
using Hdp.Droid.Services;
using Hdp.CoreRx;

namespace Hdp.Droid
{
    [Activity (Label = "HDP", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.HDP")]
    public class MainActivity : ActionBarActivity
    {
        Widget.Toolbar _toolbar;

        MainTabsAdapter _tabsAdapter;
        ViewPager _viewPager;
        PagerSlidingTabStrip _tabs;

        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            Locator.CurrentMutable.InitializeServices ();

            var viewModelViews = Locator.Current.GetService<IViewModelViewService> ();
            viewModelViews.RegisterViewModels (typeof(MainActivity).Assembly);

            var hdpApp = new HDPApp(DeviceType.ios2x);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            _toolbar = FindViewById<Widget.Toolbar> (Resource.Id.toolbar);
            SetSupportActionBar (_toolbar);

            _tabsAdapter = new MainTabsAdapter (SupportFragmentManager);
            _viewPager = FindViewById<ViewPager> (Resource.Id.pager);
            _viewPager.Adapter = _tabsAdapter;

            _tabs = FindViewById<PagerSlidingTabStrip> (Resource.Id.tabs);
            _tabs.SetViewPager (_viewPager);
        }
    }
}


