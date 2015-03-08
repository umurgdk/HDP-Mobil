using System;
using UIKit;
using CoreGraphics;
using Foundation;

namespace Hdp.Touch.Views
{
    [Register ("CategoryIconView")]
    public class CategoryIconView : UIView
    {
        private string name;
        public string Name {
            get { return name; }
            set { name = value; UpdateLabel (); }
        }

        public UILabel InitialLabel { get; set; }

        public CategoryIconView (IntPtr handle) : base (handle)
        {
            Layer.BackgroundColor = UIColor.FromRGB (0.56f, 0.74f, 0.15f).CGColor;

            InitialLabel = new UILabel (new CGRect (0, 0, Frame.Width, Frame.Height));
            InitialLabel.TextAlignment = UITextAlignment.Center;
            InitialLabel.TextColor = UIColor.White;
            InitialLabel.Font = UIFont.SystemFontOfSize (20);

            Add (InitialLabel);
        }

        public void UpdateLabel ()
        {
            InitialLabel.Text = Name.Substring (0, 1).ToUpper();
        }
    }
}

