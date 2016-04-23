using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace Fleet_Caddy
{
    public class GasLogTableSource : UITableViewSource
    {
        List<GasLog> gasLogItems;
        string cellIdentifier = "GasLogCell";

        UIViewController parentController;

        public GasLogTableSource(List<GasLog> items, UIViewController parentController)
        {
            this.parentController = parentController;
            gasLogItems = items;
        }

        //No swip edit controls are needed

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return gasLogItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            //request a recycled cell to save memory
            //if there are no cells to reuse, create a new one
            if (cell == null) cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);

            cell.TextLabel.Text = "Cart: " + gasLogItems[indexPath.Row].CartNo + " " + gasLogItems[indexPath.Row].Fueled + " Gal.";
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public GasLog GetItem(int id)
        {
            return gasLogItems[id];
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            var selectedGasLog = gasLogItems[indexPath.Row];

            //CartListController maincontroller
            GasLogController mainController = (GasLogController)parentController;

            var detail = parentController.Storyboard.InstantiateViewController("GasLogDetailCon") as GasLogDetailController;
            detail.SetGasLog(mainController, selectedGasLog);
            //parentController.NavigationController.PushViewController(detail, true);
        }
    }
}
