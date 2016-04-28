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
