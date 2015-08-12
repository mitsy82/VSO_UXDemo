using CoreGraphics;
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace PopOver_Sample
{
	partial class NavController : UIViewController
	{
		public NavController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
           
            UIImage image = UIImage.FromBundle("Settings.png");
            UIButton settingsButton = new UIButton(new CoreGraphics.CGRect(0, 0, image.Size.Width, image.Size.Height));
            settingsButton.SetBackgroundImage(image, UIControlState.Normal);
            UIBarButtonItem barButtonItem = new UIBarButtonItem(settingsButton);
            this.NavigationItem.RightBarButtonItem = barButtonItem;
            this.NavigationItem.RightBarButtonItem.TintColor = UIColor.White;
          
            CGSize size = new CGSize(200, 200);
            CGRect r = this.NavigationItem.RightBarButtonItem.CustomView.Frame;
            r.Width = 6.566f;
            r.Height = 15;
            r.X += 2.2f;
            r.Y += 25;
            this.View.Layer.BorderWidth = 2f;
            settingsButton.TouchUpInside += new EventHandler((a, e) =>
            {
                
                var settingsView = new UIViewController();
                var navController = new UINavigationController(settingsView);
                var poctl = new UIPopoverController(navController);
                poctl.SetPopoverContentSize(size, true);
                //poctl.PresentFromBarButtonItem(this.NavigationItem.RightBarButtonItem, UIPopoverArrowDirection.Up, true);
                 
                poctl.PresentFromRect(r, this.View, UIPopoverArrowDirection.Up, true);
            });
            base.ViewDidLoad();

           
        }
	}
}
