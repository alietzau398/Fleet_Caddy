using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using UIKit;

namespace Fleet_Caddy
{
    public class CartTableSource : UITableViewSource
    {
        List<Cart> tableCartItems;
        string cellIdentifier = "CartCell"; //this is the table cell name in the storyboard

        UIViewController parentController; //pass in the original controller object here

        public CartTableSource(List<Cart> cartItems, UIViewController parentController)
        {
            this.parentController = parentController;
            tableCartItems = cartItems;
        }

        public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            //base.CommitEditingStyle(tableView, editingStyle, indexPath);
            switch(editingStyle)
            {
                case UITableViewCellEditingStyle.Delete:
                    //remove the item from the underlying data source
                    CartListController mainController = (CartListController)parentController;


                    var okCancelAlertController = UIAlertController.Create("Confirm Deletion", "Are you sure you want to delete this Cart?", UIAlertControllerStyle.Alert);

                    //add actions
                    okCancelAlertController.AddAction(
                        UIAlertAction.Create("Yes", UIAlertActionStyle.Default,
                            alert =>
                            {
                                mainController.DeleteCart(tableCartItems[indexPath.Row], false);

                                tableCartItems.RemoveAt(indexPath.Row);

                                //delete the row from the table
                                tableView.DeleteRows(new NSIndexPath[]
                                    { indexPath}, UITableViewRowAnimation.Fade);
                            } //'yes' was clicked, delegate to deleteTask
                        )
                    );

                    okCancelAlertController.AddAction(
                        UIAlertAction.Create("No", UIAlertActionStyle.Cancel,
                            alert => 
                                Console.WriteLine("Canel was clicked") //do nothing
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

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, NSIndexPath indexPath)
        {
            //return base.TitleForDeleteConfirmation(tableView, indexPath);
            return "Delete";
            //optional - default text is 'delete'
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            //throw new NotImplementedException();
            return tableCartItems.Count;
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

            cell.TextLabel.Text = tableCartItems[indexPath.Row].Fleet_No;
            //show an arrow icon next to the cell
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            return cell;
        }

        public Cart GetItem(int id)
        {
            return tableCartItems[id];
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            //base.RowSelected(tableView, indexPath);
            var selectedCart = tableCartItems[indexPath.Row];

            CartListController mainController = (CartListController)parentController;

            //then open the detail view to edit cart
            var detail = parentController.Storyboard.InstantiateViewController("CartDetailCon") as CartDetailController;
            detail.SetCart(mainController, selectedCart); //moves the variable to the next controller
            parentController.NavigationController.PushViewController(detail, true);
        }
    }
}
