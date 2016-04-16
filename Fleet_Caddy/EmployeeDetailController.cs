using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fleet_Caddy
{
	partial class EmployeeDetailController : UITableViewController
	{
        Employee currentEmployee { get; set; }

        public EmployeeListController Delegate { get; set; }

        public bool IsNewEmployee { get; set; }

		public EmployeeDetailController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //hide the delete button if the calling page indicates that it is a new task
            //if (IsNewTask) btnCartDelete.Hidden = true;

            //action to be taken when the save button is selected
            btnEmployeeSave.TouchUpInside += (sender, e) =>
            {
                btnEmployeeSave.Hidden = true;
                try
                {
                    //apply trim to cleanse the user entered data
                    currentEmployee.First_Name = txtFirst_Name.Text.Trim();
                    currentEmployee.Last_Name = txtLast_Name.Text.Trim();
                    currentEmployee.Address = txtAddress.Text.Trim();
                    currentEmployee.City = txtCity.Text.Trim();
                    currentEmployee.State = txtState.Text.Trim();
                    currentEmployee.Zip = txtZip.Text.Trim();
                    currentEmployee.Employed = chkEmployed.On;

                    //these are the fields that are required to be entered
                    if ((string.IsNullOrEmpty(currentEmployee.First_Name)) || (string.IsNullOrEmpty(currentEmployee.Last_Name)))
                    { //if something is missing, show this
                        var alert = new UIAlertView("Changes Failed to Save!", "Your changes were not saved. Please enter all required values!", null, "Ok");
                        alert.Show();
                    }
                    else
                    { //all the things you want are there, so save the cart!
                        //delegate the task to be execute on the on the cartlistcontroller page
                        Delegate.SaveEmployee(currentEmployee);
                    }
                } catch (Exception ex)
                {
                    //display error message
                    var errorM = ex.Message;
                    var alert = new UIAlertView("Save Failed", "Sorry, we might be experiencing some connectivity difficulties. " + errorM, null, "OK");
                    alert.Show();
                }
                btnEmployeeSave.Hidden = false;
            };

            //action to be taken when the dlete button is selected
            //put code here later
            btnEmployeeDelete.TouchUpInside += (sender, e) =>
            {
                btnEmployeeDelete.Hidden = true;
                //create an alert with two buttons to confirm deletion:yes/no options
                var okCancelAlertController = UIAlertController.Create("Confirm Deletion", "Are you sure you want to delete this Cart?", UIAlertControllerStyle.Alert);

                    //add actions
                    okCancelAlertController.AddAction(
                        UIAlertAction.Create("Yes", UIAlertActionStyle.Default,
                            alert => Delegate.DeleteEmployee(currentEmployee, true)
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

            btnEmployeeDelete.Hidden = false;
        }

        //this event fires every time the page is displayed
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //populate the page with the selected task's information
            txtFirst_Name.Text = currentEmployee.First_Name;
            txtLast_Name.Text = currentEmployee.Last_Name;
            txtAddress.Text = currentEmployee.Address;
            txtCity.Text = currentEmployee.City;
            txtState.Text = currentEmployee.State;
            txtZip.Text = currentEmployee.Zip;
            txtEmail.Text = currentEmployee.Email;
            chkEmployed.On = currentEmployee.Employed;
            
        }

        //this will be called before the view is displayed
        //it allows us to delegate work to the calling controller
        public void SetEmployee(EmployeeListController from, Employee employee)
        {
            Delegate = from;
            currentEmployee = employee;
        }
    }
}
