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

            ProfileView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background1024x1024-01.png"));

            lblProfileWelcome.Text = currentUser["fName"] + "'s profile";

			btnEdit.TouchUpInside += BtnEdit_TouchUpInside;
			btnEdit.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnEdit.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnEdit.Layer.BorderColor = UIColor.White.CGColor;
			btnEdit.Layer.BorderWidth = System.nfloat.Parse("2.0");


			btnSave.TouchUpInside += BtnSave_TouchUpInside;
			btnSave.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnSave.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnSave.Layer.BorderColor = UIColor.White.CGColor;
			btnSave.Layer.BorderWidth = System.nfloat.Parse("2.0");


            //Make Save button hidden
            btnSave.Hidden = true;
            //Make text fields hidden
            txtEmail.Hidden = true;
            txtFName.Hidden = true;
            txtLName.Hidden = true;
            txtBusinessName.Hidden = true;
            //Load values into labels
			lblEmail.Text = currentUser["email"].ToString();
			lblFName.Text = currentUser["fName"].ToString();
			lblLName.Text = currentUser["lName"].ToString();
            lblBusinessName.Text = currentUser["businessName"].ToString();
            //load Values into text fields
            txtEmail.Text = currentUser["email"] + "";
            txtFName.Text = currentUser["fName"] + "";
            txtLName.Text = currentUser["lName"] + "";
            txtBusinessName.Text = currentUser["businessName"].ToString();
            
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
                //Reset values of labels
				lblEmail.Text = txtEmail.Text;
				lblFName.Text = txtFName.Text;
				lblLName.Text = txtLName.Text;
				lblBusinessName.Text = txtBusinessName.Text;
				lblProfileWelcome.Text = txtFName.Text.Trim() + "'s profile";
				await currentUser.SaveAsync();
            } catch (Exception error)
            {
                string errorM = error.Message;
                //lblProfileWelcome.Text = errorM;
            }
            //on success, hide text fields
            txtEmail.Hidden = true;
            txtFName.Hidden = true;
            txtLName.Hidden = true;
            txtBusinessName.Hidden = true;
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
        }
    }
}
