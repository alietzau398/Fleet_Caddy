using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;

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

        //In the near future when login works, this will point the user to the welcome page if they are already logged in 
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            //if the seque "LogInSeque" is performed, perform the following actions
            if (segueIdentifier == "LogInSeque")
            {
                loginParseUser();// this goes throught the method of trying to log the user into parse
                if (ParseUser.CurrentUser != null) // if there is someone logged in, Let them go to the next page!
                {
                    new UIAlertView("No username", "Something happened and you still went through", null, "Ok").Show();
                    return true;
                }
                else //The user failed to login, 
                {   
                    //displays the error message and doesn't have the user go to the next page
                    new UIAlertView("Login Failed", errorM, null, "Ok").Show();
                    return false;
                }
            }
            return base.ShouldPerformSegue(segueIdentifier, sender);
        }

        public async void loginParseUser()
        {
            //when method is run, the process for logging the user into parse will commence
            btnLogin.Hidden = true; //Hide the login button so the user isn't pressing it more than once
            var username = txtUsername.Text;
            var password = txtPassword.Text;

            //if email and password are not provided, don't make a parse call
            if((string.IsNullOrEmpty(username)) || (string.IsNullOrEmpty(password)))
            {
                //if the user didn't put anything in either field, let them know!
                errorM = "Enter a valid username and password";
            } else
            {
                //the user entered things correctly, time to get cracking
                try
                {
                    //try to connect to parse
                    ParseUser myUser = await ParseUser.LogInAsync(username, password);
                } catch (ParseException f)
                {
                    errorM = f.Message;
                } catch (Exception f)
                {
                    errorM = f.Message;
                }
            }

            //now I will display my login button
            btnLogin.Hidden = false;
        }
    }
}
