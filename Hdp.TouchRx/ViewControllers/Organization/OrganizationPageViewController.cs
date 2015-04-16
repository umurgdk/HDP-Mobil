using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;

using Foundation;
using UIKit;

using ReactiveUI;
using CommonMark;

using Hdp.CoreRx.ViewModels.Organization;

namespace Hdp.TouchRx.ViewControllers.Organization
{
    public partial class OrganizationPageViewController : BaseViewController<OrganizationPageViewModel>
    {
        public ContentWebView ContentWebView {
            get {
                return (ContentWebView)webView;
            }
        }

        public OrganizationPageViewController () : base ()
        {
            
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            var topItem = NavigationController.NavigationBar.TopItem;
            topItem.Title = "Parti";
			
            // Perform any additional setup after loading the view, typically from a nib.
            this.WhenAnyValue (x => x.ViewModel)
                .Where (x => x != null)
                .Subscribe (x => {
                    var md = File.ReadAllText("Content/" + ViewModel.Document);
                    var html = NSData.FromString(CommonMarkConverter.Convert(md));
                    ContentWebView.LoadData(html, "text/html", "utf-8", NSUrl.FromString("http://localhost"));   
                });
            
        }
    }
}

