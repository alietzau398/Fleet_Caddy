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
	[Register ("GasLogController")]
	partial class GasLogController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnAdd { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnShow1st { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnShow2nd { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnShowAll { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblGalBegin { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblGalBeginBefore { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblTest { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView tblGasLog { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnAdd != null) {
				btnAdd.Dispose ();
				btnAdd = null;
			}
			if (btnShow1st != null) {
				btnShow1st.Dispose ();
				btnShow1st = null;
			}
			if (btnShow2nd != null) {
				btnShow2nd.Dispose ();
				btnShow2nd = null;
			}
			if (btnShowAll != null) {
				btnShowAll.Dispose ();
				btnShowAll = null;
			}
			if (lblGalBegin != null) {
				lblGalBegin.Dispose ();
				lblGalBegin = null;
			}
			if (lblGalBeginBefore != null) {
				lblGalBeginBefore.Dispose ();
				lblGalBeginBefore = null;
			}
			if (lblTest != null) {
				lblTest.Dispose ();
				lblTest = null;
			}
			if (tblGasLog != null) {
				tblGasLog.Dispose ();
				tblGasLog = null;
			}
		}
	}
}
