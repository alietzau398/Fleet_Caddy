using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Drawing;

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

            //WelcomeView.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            WelcomeView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background1024x1024-01.png"));
            btnLogOff.TouchUpInside += BtnLogOff_TouchUpInside;
            btnLogOff.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnLogOff.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnLogOff.Layer.BorderColor = UIColor.FromRGB(168, 221, 237).CGColor;
            btnLogOff.Layer.BorderWidth = System.nfloat.Parse("2.0");
            
            btnFleetList.TouchUpInside += BtnFleetList_TouchUpInside;
            btnFleetList.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnFleetList.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnFleetList.Layer.BorderColor = UIColor.FromRGB(168, 221, 237).CGColor;
            btnFleetList.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnEmployeeList.TouchUpInside += BtnFleetList_TouchUpInside;
            btnEmployeeList.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnEmployeeList.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnEmployeeList.Layer.BorderColor = UIColor.FromRGB(168, 221, 237).CGColor;
            btnEmployeeList.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnGasLog.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnGasLog.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnGasLog.Layer.BorderColor = UIColor.FromRGB(168, 221, 237).CGColor;
            btnGasLog.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnRepairLog.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnRepairLog.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnRepairLog.Layer.BorderColor = UIColor.White.CGColor;
            btnRepairLog.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnVendor.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnVendor.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnVendor.Layer.BorderColor = UIColor.White.CGColor;
            btnVendor.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnManual.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnManual.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnManual.Layer.BorderColor = UIColor.White.CGColor;
            btnManual.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnProfile.SetTitleColor(UIColor.White, UIControlState.Normal);
            btnProfile.BackgroundColor = UIColor.FromRGB(95, 202, 234);
            btnProfile.Layer.BorderColor = UIColor.White.CGColor;
            btnProfile.Layer.BorderWidth = System.nfloat.Parse("2.0");

            if (!(this.NavigationItem.HidesBackButton))
            {
                this.NavigationItem.SetHidesBackButton(true, false);
            }

            if(ParseUser.CurrentUser == null) //the user is not logged in, navigate to the log in page.
            {
                NavigationController.PopViewController(true);
                var home = Storyboard.InstantiateViewController("Home") as HomeController;
                NavigationController.PushViewController(home, true);
            } else //there is a user!!
            {
                var currentUser = ParseUser.CurrentUser;
                lblWelcome.Text = "Welcome, " + currentUser["fName"];
                //lblWelcome.Font = UIFont.FromName("Amperzand", 17f);
                //WelcomeView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background1024x1024-01.png"));
                //DateTime dateNow = DateTime.Now.Date;
                //lblDateNow.Text = dateNow.ToString("d");
                //this code is for determining what day of the week it is
                //lblDayWeek.Text = dateNow.DayOfWeek.ToString();
                //Goes through goes to the day before until it finds the start of the week (sunday)
                //lblBeginWeek.Text = StartOfWeek(dateNow, DayOfWeek.Sunday).ToString("d");
                
            }
        }
                
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //remove the back button if the back button is not hidden
            if (!(this.NavigationItem.HidesBackButton))
            {
                this.NavigationItem.SetHidesBackButton(true, false);

                var currentUser = ParseUser.CurrentUser;
                lblWelcome.Text = "Welcome, " + currentUser["fName"];
            }
            if (ParseUser.CurrentUser != null) //the user is not logged in, navigate to the log in page.
            {
                var currentUser = ParseUser.CurrentUser;
                lblWelcome.Text = "Welcome, " + currentUser["fName"];
            }

        }

        private void BtnFleetList_TouchUpInside(object sender, EventArgs e)
        {
            //PerformSegue("CartListSeque", this);
        }

        private void BtnLogOff_TouchUpInside(object sender, EventArgs e)
        {
            ParseUser.LogOut();
            var home = Storyboard.InstantiateViewController("Home") as HomeController;
            NavigationController.PushViewController(home, true);
        }
    }
}
