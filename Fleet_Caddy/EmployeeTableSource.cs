using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Foundation;

namespace Fleet_Caddy
{
    public class EmployeeTableSource : UITableViewSource
    {
        List<Employee> tableEmployeeItems;
        string cellIdentifier = "EmployeeCell"; //this is the table cell name in the storyboard

        UIViewController parentController;

        public EmployeeTableSource(List<Employee> employeeItems, UIViewController parentController)
        {
            this.parentController = parentController;
            tableEmployeeItems = employeeItems;
        }

        /* Code for editing employees. left out because employees should not be deleted
        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            //base.CommitEditingStyle(tableView, editingStyle, indexPath);
            switch (editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    //remove the item from the underlying data source
                    EmployeeListController mainController = (EmployeeListController)parentController;


                    var okCancelAlertController = UIAlertController.Create("Confirm Deletion", "Are you sure you want to delete this Employee?", UIAlertControllerStyle.Alert);

                    //add actions
                    okCancelAlertController.AddAction(
                        UIAlertAction.Create("Yes", UIAlertActionStyle.Default,
                            alert =>
                            {
                                mainController.DeleteEmployee(tableEmployeeItems[indexPath.Row], false);

                                tableEmployeeItems.RemoveAt(indexPath.Row);

                                //delete the row from the table
                                tableView.DeleteRows(new NSIndexPath[]
                                    { indexPath}, UITableViewRowAnimation.Fade);
                            } //'yes' was clicked, delegate to deleteTask
                        )
                    );

                    okCancelAlertController.AddAction(
                        UIAlertAction.Create("No", UIAlertActionStyle.Cancel,
                            alert =>
                                Console.WriteLine("Cancel was clicked") //do nothing
                            )
                    );

                    //present alert
                    mainController.PresentViewController(okCancelAlertController, true, null);

                    break;
                case UITableViewCellEditingStyle.None:
                    //Console.write("Commiteditingstyle:none called");
                    break;
            }
        }
        */

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return false;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {
            return "Delete";
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return tableEmployeeItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            //throw new NotImplementedException();
            //in a storyboard, dequeue will always return a cell
            UITableViewCell cell = tableView.DequeueReusableCell(cellIdentifier);

            //request a recycled cell to save memory
            //if there are no cells to reuse, create a new one
            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Default, cellIdentifier);

            string fullName = tableEmployeeItems[indexPath.Row].First_Name + " " + tableEmployeeItems[indexPath.Row].Last_Name;
            cell.TextLabel.Text = fullName;
            //show an arrow icon next to the cell
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public Employee GetItem(int id)
        {
            return tableEmployeeItems[id];
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //base.RowSelected(tableView, indexPath);
            var selectedCart = tableEmployeeItems[indexPath.Row];

            EmployeeListController mainController = (EmployeeListController)parentController;

            //then open the detail view to edit cart
            var detail = parentController.Storyboard.InstantiateViewController("EmployeeDetailCon") as EmployeeDetailController;
            detail.SetEmployee(mainController, selectedCart); //moves the variable to the next controller
            parentController.NavigationController.PushViewController(detail, true);
        }
    }
}
