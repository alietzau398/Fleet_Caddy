// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fleet_Caddy
{
	[Register ("RegisterController")]
	partial class RegisterController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnSubmitRegister { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView RegisterView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtBusinessName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtConfirmPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtFirstName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtLastName { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtPassword { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtUsername { get; set; }

		[Action ("BtnSubmitRegister_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void BtnSubmitRegister_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnSubmitRegister != null) {
				btnSubmitRegister.Dispose ();
				btnSubmitRegister = null;
			}
			if (RegisterView != null) {
				RegisterView.Dispose ();
				RegisterView = null;
			}
			if (txtBusinessName != null) {
				txtBusinessName.Dispose ();
				txtBusinessName = null;
			}
			if (txtConfirmPassword != null) {
				txtConfirmPassword.Dispose ();
				txtConfirmPassword = null;
			}
			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}
			if (txtFirstName != null) {
				txtFirstName.Dispose ();
				txtFirstName = null;
			}
			if (txtLastName != null) {
				txtLastName.Dispose ();
				txtLastName = null;
			}
			if (txtPassword != null) {
				txtPassword.Dispose ();
				txtPassword = null;
			}
			if (txtUsername != null) {
				txtUsername.Dispose ();
				txtUsername = null;
			}
		}
	}
}
