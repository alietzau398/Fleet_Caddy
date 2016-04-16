using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Threading;

namespace Fleet_Caddy
{
	partial class HomeController : UIViewController
	{
        //class level cariables that are used between methods
        string errorM; //error messages that the user should see will be put here.

		public HomeController (IntPtr handle) : base (handle)
		{
            //Nothing to see here!
		}

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //sets the form equal to nothing
            txtUsername.Text = "";
            txtPassword.Text = "";
            
            this.NavigationItem.SetHidesBackButton(true, false);
        }

        //In the near future when login works, this will point the user to the welcome page if they are already logged in 
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (!(this.NavigationItem.HidesBackButton))
            {
                this.NavigationItem.SetHidesBackButton(true, false);
            }

            btnLogin.TouchUpInside += BtnLogin_TouchUpInside;
            if (ParseUser.CurrentUser != null)
            {
                //Oh no! the user got here but wasn't logged off! Send them to the welcome page!
                NavigationController.PopViewController(true);
                //PerformSegue("LogInSeque", this);
                //var welcome = Storyboard.InstantiateViewController("Welcome") as WelcomeController;
                //NavigationController.PushViewController(welcome, true);
            }
        }

        async private void BtnLogin_TouchUpInside(object sender, EventArgs e)
        {
            //when method is run, the process for logging the user into parse will commence
            btnLogin.Hidden = true; //Hide the login button so the user isn't pressing it more than once
            var usernamePU = txtUsername.Text;
            var passwordPU = txtPassword.Text;
            //if email and password are not provided, don't make a parse call
            if ((string.IsNullOrEmpty(usernamePU)) || (string.IsNullOrEmpty(passwordPU)))
            {
                //if the user didn't put anything in either field, let them know!
                new UIAlertView("Login Failed", "Enter a valid username and password", null, "Ok").Show();
            }
            else
            {
                //the user entered things correctly, time to get cracking
                try
                {
                    //try to connect to parse
                    ParseUser myUser = await ParseUser.LogInAsync(usernamePU, passwordPU);

                    //pop, don't push 
                    NavigationController.PopViewController(true);

                    //var welcome = Storyboard.InstantiateViewController("Welcome") as WelcomeController;
                    //NavigationController.PushViewController(welcome, true);
                }
                catch (ParseException f)
                {
                    errorM = f.Message + " -Parse";
                }
                catch (Exception f)
                {
                    errorM = f.Message + " -Something else!";
                }
            }
            //now I will display my login button
            btnLogin.Hidden = false;
        }

    }
    }
