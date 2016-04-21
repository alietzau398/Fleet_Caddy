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
	[Register ("GasLogDetailController")]
	partial class GasLogDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnGasLogDelete { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnGasLogSave { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblGasLogObjectID { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtCart { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtEmployee { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtFueled { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtWhen { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnGasLogDelete != null) {
				btnGasLogDelete.Dispose ();
				btnGasLogDelete = null;
			}
			if (btnGasLogSave != null) {
				btnGasLogSave.Dispose ();
				btnGasLogSave = null;
			}
			if (lblGasLogObjectID != null) {
				lblGasLogObjectID.Dispose ();
				lblGasLogObjectID = null;
			}
			if (txtCart != null) {
				txtCart.Dispose ();
				txtCart = null;
			}
			if (txtEmployee != null) {
				txtEmployee.Dispose ();
				txtEmployee = null;
			}
			if (txtFueled != null) {
				txtFueled.Dispose ();
				txtFueled = null;
			}
			if (txtWhen != null) {
				txtWhen.Dispose ();
				txtWhen = null;
			}
		}
	}
}
