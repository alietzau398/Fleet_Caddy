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
	[Register ("EmployeeDetailController")]
	partial class EmployeeDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmployeeDelete { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnEmployeeSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch chkEmployed { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtAddress { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtCity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtFirst_Name { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtLast_Name { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtState { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtZip { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnEmployeeDelete != null) {
				btnEmployeeDelete.Dispose ();
				btnEmployeeDelete = null;
			}
			if (btnEmployeeSave != null) {
				btnEmployeeSave.Dispose ();
				btnEmployeeSave = null;
			}
			if (chkEmployed != null) {
				chkEmployed.Dispose ();
				chkEmployed = null;
			}
			if (txtAddress != null) {
				txtAddress.Dispose ();
				txtAddress = null;
			}
			if (txtCity != null) {
				txtCity.Dispose ();
				txtCity = null;
			}
			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}
			if (txtFirst_Name != null) {
				txtFirst_Name.Dispose ();
				txtFirst_Name = null;
			}
			if (txtLast_Name != null) {
				txtLast_Name.Dispose ();
				txtLast_Name = null;
			}
			if (txtState != null) {
				txtState.Dispose ();
				txtState = null;
			}
			if (txtZip != null) {
				txtZip.Dispose ();
				txtZip = null;
			}
		}
	}
}
