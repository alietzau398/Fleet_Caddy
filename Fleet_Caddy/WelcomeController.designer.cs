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
		UIButton btnGasLog { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogOff { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnManual { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnProfile { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnRepairLog { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnVendor { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblWelcome { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView WelcomeView { get; set; }

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
			if (btnGasLog != null) {
				btnGasLog.Dispose ();
				btnGasLog = null;
			}
			if (btnLogOff != null) {
				btnLogOff.Dispose ();
				btnLogOff = null;
			}
			if (btnManual != null) {
				btnManual.Dispose ();
				btnManual = null;
			}
			if (btnProfile != null) {
				btnProfile.Dispose ();
				btnProfile = null;
			}
			if (btnRepairLog != null) {
				btnRepairLog.Dispose ();
				btnRepairLog = null;
			}
			if (btnVendor != null) {
				btnVendor.Dispose ();
				btnVendor = null;
			}
			if (lblWelcome != null) {
				lblWelcome.Dispose ();
				lblWelcome = null;
			}
			if (WelcomeView != null) {
				WelcomeView.Dispose ();
				WelcomeView = null;
			}
		}
	}
}
