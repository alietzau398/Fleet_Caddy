using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;

namespace Fleet_Caddy
{
	partial class WelcomeController : UIViewController
	{
		public WelcomeController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            btnLogOff.TouchUpInside += BtnLogOff_TouchUpInside;
            btnFleetList.TouchUpInside += BtnFleetList_TouchUpInside;
            
            var currentUser = ParseUser.CurrentUser;
            lblWelcome.Text = "Welcome, " + currentUser["fName"];
        }

        private void BtnFleetList_TouchUpInside(object sender, EventArgs e)
        {
            //PerformSegue("CartListSeque", this);
        }

        private void BtnLogOff_TouchUpInside(object sender, EventArgs e)
        {
            ParseUser.LogOut();
            PerformSegue("LogOffSeque", this);
        }
    }
}
