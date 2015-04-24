
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

using ReactiveUI;
using ReactiveUI.AndroidSupport;
using Hdp.CoreRx.ViewModels.Organization;
using Android.Webkit;
using System.IO;
using CommonMark;

namespace Hdp.Droid.Fragments
{
    public class ReadOrganizationFragment : ReactiveUI.AndroidSupport.ReactiveFragment<OrganizationPageViewModel>
    {
        WebView _bodyWebView;

        public ReadOrganizationFragment (OrganizationPageViewModel viewModel)
            :base()
        {
            ViewModel = viewModel;
        }

        public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var view = inflater.Inflate (Resource.Layout.ReadOrganizationLayout, null);

            _bodyWebView = view.FindViewById<WebView> (Resource.Id.organizationPageBodyWeb);

            using (var sr = new StreamReader(container.Context.Assets.Open("Content/" + ViewModel.Document)))
            {
                var html = CommonMarkConverter.Convert (sr.ReadToEnd());
                _bodyWebView.LoadData (PrepareHtml(html), "text/html; charset=UTF-8", null);
            }

            return view;
        }

        private string PrepareHtml (string html)
        {
            return @"<html><head>
            <meta charset=""UTF-8"">
            <style>body { margin: 0; padding: 0; color: #444; }</style>
            </head><body>" + html + @"</body></html>";
        }

        public override void OnAttach (Android.App.Activity activity)
        {
            base.OnAttach (activity);

            var mainActivity = (MainActivity)activity;
            if (mainActivity != null)
            {
                mainActivity.IsBackButtonVisible = true;
            }
        }
    }
}

