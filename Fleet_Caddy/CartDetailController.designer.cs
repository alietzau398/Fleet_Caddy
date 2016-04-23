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
	[Register ("CartDetailController")]
	partial class CartDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCartDelete { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnCartSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch chkActive { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableViewCell s { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtBrand { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtFleetNo { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtModel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtNotes { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtSerial_No { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtYear { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnCartDelete != null) {
				btnCartDelete.Dispose ();
				btnCartDelete = null;
			}
			if (btnCartSave != null) {
				btnCartSave.Dispose ();
				btnCartSave = null;
			}
			if (chkActive != null) {
				chkActive.Dispose ();
				chkActive = null;
			}
			if (s != null) {
				s.Dispose ();
				s = null;
			}
			if (txtBrand != null) {
				txtBrand.Dispose ();
				txtBrand = null;
			}
			if (txtFleetNo != null) {
				txtFleetNo.Dispose ();
				txtFleetNo = null;
			}
			if (txtModel != null) {
				txtModel.Dispose ();
				txtModel = null;
			}
			if (txtNotes != null) {
				txtNotes.Dispose ();
				txtNotes = null;
			}
			if (txtSerial_No != null) {
				txtSerial_No.Dispose ();
				txtSerial_No = null;
			}
			if (txtYear != null) {
				txtYear.Dispose ();
				txtYear = null;
			}
		}
	}
}
