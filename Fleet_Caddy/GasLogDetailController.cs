using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Foundation;


namespace Fleet_Caddy
{
	partial class GasLogDetailController : UITableViewController
	{
        GasLog currentGasLog { get; set; }

        public GasLogController Delegate { get; set; }

        public bool IsNewGasLog { get; set; }
        
        public GasLogDetailController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //hide the delete button if the calling page indicates that it is a new gaslog
            if (IsNewGasLog) btnGasLogDelete.Hidden = true;
            

            //action to be taken when the save button is selected
            btnGasLogSave.TouchUpInside += (sender, e) =>
            {
                //apply trim to cleanse the user entered data
                string checkedFueled = txtFueled.Text;
                //gaslog amount fueled is a required field, always perform input validation
                if (checkedFueled == "") 
                { //if something is missing, show this
                    var alert = new UIAlertView("Changes Failed to Save!", "Your changes were not saved. Please enter all required values!", null, "Ok");
                    alert.Show();
                }
                else
                { //all the things you want are there, so save the cart!
                  //delegate the task to be execute on the on the cartlistcontroller page
                    currentGasLog.Fueled = Convert.ToDouble(txtFueled.Text);
                    DateTime dateNow = Convert.ToDateTime(txtWhen.Text);
                    currentGasLog.BeginWeek = StartOfWeek(dateNow, DayOfWeek.Sunday);

                    //currentGasLog.BeginWeek = 
                    Delegate.SaveGasLog(currentGasLog);
                }
            };

            //action to be taken when the delete button is selected
            btnGasLogDelete.TouchUpInside += (sender, e) =>
            {
                //create an alert with two buttons to confirm deletion; yes/no options
                var okCancelAlertController = UIAlertController.Create("Confirm Deletion",
                    "Are you sure you want to delete this Gas Log?",
                    UIAlertControllerStyle.Alert);

                //add action
                okCancelAlertController.AddAction(
                    UIAlertAction.Create("Yes",
                        UIAlertActionStyle.Default,
                        alert => Delegate.DeleteGasLog(currentGasLog)
                    )
                );

                okCancelAlertController.AddAction(
                    UIAlertAction.Create("No",
                        UIAlertActionStyle.Cancel,
                        alert => Console.WriteLine("Cancel was clicked")
                    )
                );

                //present alert
                PresentViewController(okCancelAlertController, true, null);
            };
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        private void SetUpCartPicker()
        { 
            //setup the picker and the model
            

        }

        private void SetUpEmployeePicker()
        {

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //populate the page with the selected gaslog's information
            lblGasLogObjectID.Text = "Gas Log Id: " + currentGasLog.ObjectID;
            txtCart.Text = Convert.ToString(currentGasLog.Cart);
            txtEmployee.Text = Convert.ToString(currentGasLog.Employee);
            if (IsNewGasLog)
            {  //this is a new cart
                txtFueled.Text = "";
                txtWhen.Text = "";
            }
            else //this is not a new cart
            {
                txtFueled.Text = Convert.ToString(currentGasLog.Fueled);
                txtWhen.Text = currentGasLog.When.ToString("d");
            }
        }

        //this will be called before the view is displayed
        //it allows us to delegate work to the calling controller
        public void SetGasLog(GasLogController from, GasLog gasLog)
        {
            Delegate = from;
            currentGasLog = gasLog;
        }
    }
}
