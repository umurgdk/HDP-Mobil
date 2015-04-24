
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

using ReactiveUI;
using ReactiveUI.AndroidSupport;
using Hdp.CoreRx.ViewModels;
using Hdp.CoreRx.ViewModels.Events;

namespace Hdp.Droid.Fragments
{
    public abstract class BaseLoadingFragment<TLoadingViewModel> : ReactiveUI.AndroidSupport.ReactiveFragment<TLoadingViewModel>
        where TLoadingViewModel : ReactiveObject, ILoadingViewModel
    {
        public View ContentView { get; private set; }
        public Color BackgroundColor { get; protected set; }

        private View _loadingView;

        public override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            BackgroundColor = Color.ParseColor ("#EEEEEE");
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            RelativeLayout layout = new RelativeLayout (container.Context);
            layout.LayoutParameters = new ViewGroup.LayoutParams (ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            layout.SetBackgroundColor (Color.ParseColor ("#EEEEEE"));
            layout.SetGravity (GravityFlags.Center);

            ContentView = OnCreateContentView (inflater, container, savedInstanceState);
            ContentView.Visibility = ViewStates.Gone;

            _loadingView = inflater.Inflate (Resource.Layout.LoadingLayout, null);

            layout.AddView (_loadingView);
            layout.AddView (ContentView);

            return layout;
        }

        public override void OnViewCreated (View view, Bundle savedInstanceState)
        {
            base.OnViewCreated (view, savedInstanceState);

            this.OneWayBind (ViewModel,
                x => x.IsLoading, 
                x => x._loadingView.Visibility,
                x => x ? ViewStates.Visible : ViewStates.Gone);

            this.OneWayBind (ViewModel,
                x => x.IsLoading,
                x => x.ContentView.Visibility,
                x => x ? ViewStates.Gone : ViewStates.Visible);
        }

        protected abstract View OnCreateContentView (LayoutInflater inflater, ViewGroup container, Bundle savedInstance);
    }
}

