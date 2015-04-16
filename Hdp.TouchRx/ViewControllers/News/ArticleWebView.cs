using System;
using UIKit;
using Foundation;
using System.IO;

namespace Hdp.TouchRx.ViewControllers.News
{
    public class ArticleWebView : UIWebView
    {
        public ArticleWebView ()
            :base()
        {
            LoadFinished += WebViewDidLoad;
        }

        public void WebViewDidLoad (object sender, EventArgs e)
        {
            var css = File.ReadAllText ("WebHelpers/ArticleBody.css").Replace('\n', ' ');
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

