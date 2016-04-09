using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fleet_Caddy
{
	partial class CartDetailController : UITableViewController
	{
        Cart currentCart { get; set; }

        public CartListController Delegate { get; set; } //could be used to save, delete later

        public bool IsNewTask { get; set; }

		public CartDetailController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //hide the delete button if the calling page indicates that it is a new task
            if (IsNewTask) btnCartDelete.Hidden = true;

            //action to be taken when the save button is selected
            btnCartSave.TouchUpInside += (sender, e) =>
            {
                //apply trim() to cleanse the user entered data
                currentCart.Fleet_No = txtFleetNo.Text.Trim();
                currentCart.Year = Convert.ToInt32(txtYear.Text.Trim());
                var tempYear = Convert.ToString(currentCart.Year);

                //Go through and make sure required fields are filled in
                if ((string.IsNullOrEmpty(currentCart.Fleet_No)) || (string.IsNullOrEmpty(tempYear)))
                { //if something is missing, show this
                    var alert = new UIAlertView("Changes Failed to Save!", "Your changes were not saved. Please enter all required values!", null, "Ok");
                    alert.Show();
                }
                else
                { //all the things you want are there, so save the cart!
                    //delegate the task to be execute on the on the cartlistcontroller page
                    Delegate.SaveCart(currentCart);
                }
            };

            //action to be taken when the delete button is selected
            btnCartDelete.TouchUpInside += (sender, e) =>
            {
                //create an alert with two buttons to confirm deletion:yes/no options
                var okCancelAlertController = UIAlertController.Create("Confirm Deletion", "Are you sure you want to delete this Cart?", UIAlertControllerStyle.Alert);

                //add actions
                okCancelAlertController.AddAction(
                    UIAlertAction.Create("Yes", UIAlertActionStyle.Default,
                        alert => Delegate.DeleteCart(currentCart, true)
                        //'yes' was clicked, delegate to deleteTask
                    )
                );

                okCancelAlertController.AddAction(
                    UIAlertAction.Create("No", UIAlertActionStyle.Cancel,
                        alert =>
                            Console.WriteLine("Canel was clicked") //do nothing
                        )
                );

                //present alert
                PresentViewController(okCancelAlertController, true, null);
            };
        }

        //this event fires every time the page is displayed
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //populate the page with the selected task's information
            txtFleetNo.Text = currentCart.Fleet_No;
            txtYear.Text = Convert.ToString(currentCart.Year);
        }

        //this will be called before the view is displayed
        //it allows us to delegate work to the calling controller
        public void SetCart (CartListController from, Cart cart)
        {
            Delegate = from;
            currentCart = cart;
        }
    }
}
