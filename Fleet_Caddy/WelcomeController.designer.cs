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
	[Register ("WelcomeController")]
	partial class WelcomeController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmployeeList { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnFleetList { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogOff { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblWelcome { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnEmployeeList != null) {
				btnEmployeeList.Dispose ();
				btnEmployeeList = null;
			}
			if (btnFleetList != null) {
				btnFleetList.Dispose ();
				btnFleetList = null;
			}
			if (btnLogOff != null) {
				btnLogOff.Dispose ();
				btnLogOff = null;
			}
			if (lblWelcome != null) {
				lblWelcome.Dispose ();
				lblWelcome = null;
			}
		}
	}
}
