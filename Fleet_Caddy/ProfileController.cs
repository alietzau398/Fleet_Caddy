using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;

namespace Fleet_Caddy
{
	partial class ProfileController : UIViewController
	{

        ParseUser currentUser = ParseUser.CurrentUser;

        public ProfileController (IntPtr handle) : base (handle)
		{
            Title = "Profile";
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblProfileWelcome.Text = currentUser["fName"] + "'s profile";

            btnEdit.TouchUpInside += BtnEdit_TouchUpInside;
            btnSave.TouchUpInside += BtnSave_TouchUpInside;

            //Make Save button hidden
            btnSave.Hidden = true;
            //Make text fields hidden
            txtEmail.Hidden = true;
            txtFName.Hidden = true;
            txtLName.Hidden = true;
            txtBusinessName.Hidden = true;
            lblEmail2.Hidden = true;
            lblFName2.Hidden = true;
            lblLName2.Hidden = true;
            lblBusinessName2.Hidden = true;
            //Load values into labels
            lblEmail.Text = "Email: " + currentUser["email"];
            lblFName.Text = "First Name: " + currentUser["fName"];
            lblLName.Text = "Last Name: " + currentUser["lName"];
            lblBusinessName.Text = "Business Name: " + currentUser["businessName"];
            //load Values into text fields
            txtEmail.Text = currentUser["email"] + "";
            txtFName.Text = currentUser["fName"] + "";
            txtLName.Text = currentUser["lName"] + "";
            txtBusinessName.Text = currentUser["businessName"] + "";
            
        }

        async private void BtnSave_TouchUpInside(object sender, EventArgs e)
        {
            //hide save button
            btnSave.Hidden = true;
            //try to update profile
            try
            {
                //Save items to parse
                currentUser["email"] = txtEmail.Text;
                currentUser["fName"] = txtFName.Text;
                currentUser["lName"] = txtLName.Text;
                currentUser["businessName"] = txtBusinessName.Text;
                await currentUser.SaveAsync();
                //Reset values of labels
                lblEmail.Text = "Email: " + currentUser["email"];
                lblFName.Text = "First Name: " + currentUser["fName"];
                lblLName.Text = "Last Name: " + currentUser["lName"];
                lblBusinessName.Text = "Business Name: " + currentUser["businessName"];
                lblProfileWelcome.Text = currentUser["fName"] + "'s profile";
            } catch (Exception error)
            {
                string errorM = error.Message;
                lblProfileWelcome.Text = errorM;
            }
            //on success, hide text fields
            txtEmail.Hidden = true;
            txtFName.Hidden = true;
            txtLName.Hidden = true;
            txtBusinessName.Hidden = true;
            lblEmail2.Hidden = true;
            lblFName2.Hidden = true;
            lblLName2.Hidden = true;
            lblBusinessName2.Hidden = true;
            //Go through and make labels visible
            lblEmail.Hidden = false;
            lblFName.Hidden = false;
            lblLName.Hidden = false;
            lblBusinessName.Hidden = false;
            //unhide the edit button
            btnEdit.Hidden = false;
        }

        private void BtnEdit_TouchUpInside(object sender, EventArgs e)
        {
            //make save button visible
            btnSave.Hidden = false;
            //make edit button hidden
            btnEdit.Hidden = true;
            //Go through and make the labels hidden
            lblEmail.Hidden = true;
            lblFName.Hidden = true;
            lblLName.Hidden = true;
            lblBusinessName.Hidden = true;
            //make text fields visible
            txtEmail.Hidden = false;
            txtFName.Hidden = false;
            txtLName.Hidden = false;
            txtBusinessName.Hidden = false;
            lblEmail2.Hidden = false;
            lblFName2.Hidden = false;
            lblLName2.Hidden = false;
            lblBusinessName2.Hidden = false;
        }
    }
}
