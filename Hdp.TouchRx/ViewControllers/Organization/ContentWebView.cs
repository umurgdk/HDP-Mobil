using System;
using Foundation;
using UIKit;
using System.IO;

namespace Hdp.TouchRx.ViewControllers.Organization
{
    [Register ("ContentWebView")]
    public class ContentWebView : UIWebView
    {
        public ContentWebView ()
            :base()
        {
            LoadFinished += WebViewDidLoad;
        }

        public ContentWebView (IntPtr ptr)
            :base(ptr)
        {
            LoadFinished += WebViewDidLoad;
        }

        public void WebViewDidLoad (object sender, EventArgs e)
        {
            var css = File.ReadAllText ("WebHelpers/Content.css").Replace('\n', ' ');
            var js = @"
            var css = '" + css + @"'
            var head = document.head || document.getElementsByTagName('head')[0];
            var style = document.createElement('style');
            style.type = 'text/css';
            style.appendChild(document.createTextNode(css));
            head.appendChild(style);";
            Console.WriteLine (js);

            EvaluateJavascript (js);
        }
    }
}

