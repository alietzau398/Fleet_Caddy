using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;

namespace Fleet_Caddy
{
	partial class RegisterController : UIViewController
	{
		public RegisterController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
        }

        async partial void BtnSubmitRegister_TouchUpInside(UIButton sender)
        {

            var uName = txtUsername.Text;
            var firstName = txtFirstName.Text;
            var lastName = txtLastName.Text;
            var businessName = txtBusinessName.Text;
            var email = txtEmail.Text;
            var pWord = txtPassword.Text;
            var cPassword = txtConfirmPassword.Text;
            var alert = new UIAlertView();

            if ((string.IsNullOrEmpty(email)) || (string.IsNullOrEmpty(uName)) || (string.IsNullOrEmpty(pWord)))
            {
                //display an alert pop-up if any of the required fields are left out
                alert = new UIAlertView("Input Validation Failed", "Please complete all the input fields!", null, "OK");
                alert.Show();
            }
            else
            {
                if (pWord != cPassword)
                {
                    //if password is not the same as the confirm password, alert user
                    alert = new UIAlertView("Input Validation Failed", "Password and Confirm Password must match!", null, "OK");
                    alert.Show();
                }
                else
                {
                    //call Parse to create a new user, put it in a try catch block
                    //if an internet connection doesn't exist, the parse call will fail
                    //in addition, if the email already exists in parse, the call will fail

                    try
                    {
                        //create a new user in pare
                        //by setting the default user class properties as follows;
                        var user = new ParseUser()
                        {
                            Username = uName,
                            Password = pWord,
                            Email = email
                        };

                        //adding the non-default fields using the following approach
                        user["fName"] = firstName;
                        user["lName"] = lastName;
                        if (!(string.IsNullOrEmpty(businessName)))
                        {
                            //if the string is not empty, add attribute to user profile
                            user["businessName"] = businessName;
                        }

                        //make an asynchronous call to parse to create a new user
                        await user.SignUpAsync();

                        //show an alert to confirm 
                        alert = new UIAlertView("Account Created", "Your account has been successfully created!", null, "OK");
                        alert.Show();

                        //navigate to the login page now that the user registered
                        NavigationController.PopViewController(true);
                    }
                    catch (Exception ex)
                    {
                        //display error message
                        var error = ex.Message;
                        alert = new UIAlertView("Registration Failed", "Sorry, we might be experiencing some connectivity difficulties or your email is already in the system.", null, "OK");
                        alert.Show();
                    }



                }
            }
        }
    }
}
