using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;

namespace Fleet_Caddy
{
	partial class ProfileController : UIViewController
	{
		public ProfileController (IntPtr handle) : base (handle)
		{
            Title = "Profile";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var currentUser = ParseUser.CurrentUser;
            lblProfileWelcome.Text = currentUser["fName"] + "'s profile";
        }
    }
}
