using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
//using Foundation;
using System.Collections.Generic;
using Parse;

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

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //hide the delete button if the calling page indicates that it is a new gaslog
            if (IsNewGasLog) btnGasLogDelete.Hidden = true;
            

            //action to be taken when the save button is selected
            btnGasLogSave.TouchUpInside += (sender, e) =>
            {
                btnGasLogSave.Hidden = true;
                //apply trim to cleanse the user entered data
                string checkedFueled = txtFueled.Text;
                //gaslog amount fueled is a required field, always perform input validation
                if (checkedFueled == "" || txtCart.Text == "" || txtEmployee.Text == "")
                { //if something is missing, show this
                    var alert = new UIAlertView("Changes Failed to Save!", "Your changes were not saved. Please enter all required values!", null, "Ok");
                    alert.Show();
                }
                else
                { //all the things you want are there, so save the cart!
                  //delegate the task to be execute on the on the cartlistcontroller page
                    currentGasLog.Fueled = Convert.ToDouble(txtFueled.Text);
                    currentGasLog.CartNo = Convert.ToInt32(txtCart.Text);
                    currentGasLog.Cart = selectCartParse;
                    currentGasLog.Employee = selectEmployeeParse;
                    currentGasLog.EmployeeName = txtEmployee.Text;
                    currentGasLog.User = ParseUser.CurrentUser;
                    DateTime dateNow = Convert.ToDateTime(txtWhen.Text);
                    currentGasLog.BeginWeek = StartOfWeek(dateNow, DayOfWeek.Sunday);
                    currentGasLog.When = Convert.ToDateTime(txtWhen.Text);

                    //currentGasLog.BeginWeek = 
                    Delegate.SaveGasLog(currentGasLog);
                }
                btnGasLogSave.Hidden = false;
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

            //running picker methods
            await GetAllCarts(); //get the carts
            SetUpCartPicker();
            await GetAllEmployees();
            SetUpEmployeePicker();

        }

        //***** Start of Cart Picker Code
        public IList<string> CartNoDict = new List<string>{ };
        public List<Cart> cartList;
        public async System.Threading.Tasks.Task GetAllCarts()
        {
            //Initialize the list of carts
            cartList = new List<Cart> { };     //used to upload to server
            CartNoDict = new List<string> { }; //used to put into the text field


            //get a list of carts from parse and sort by updatedAt date
            var query = from cartList in ParseObject.GetQuery("Carts")
                        where cartList["User"] == ParseUser.CurrentUser
                        orderby cartList["updatedAt"] descending
                        select cartList;

            IEnumerable<ParseObject> cartListResult = await query.FindAsync();
            //if the returned list from parse is not empty
            if (cartListResult != null)
            {
                //loop through the results and create
                foreach (var myCart in cartListResult)
                {
                    var cartItem = new Cart()
                    {
                        Id = cartList.Count + 1, //takes the count of the list 
                        ObjectID = myCart.ObjectId, //mycart is the variable for the items in the results
                        Year = myCart.Get<int>("Year"), //takes the year attribute from my cart and puts it in the object attribute of year
                        Fleet_No = myCart.Get<string>("Fleet_No"), //does the same for fleet_no
                        Active = myCart.Get<bool>("Active"),
                    };
                    cartList.Add(cartItem);
                    CartNoDict.Add(myCart.Get<int>("Fleet_No").ToString());//start to build a list of cartNo
                }
            }
        }

        public Cart selectedCart;
        public string selectedCartNo;
        public int selectedCartIDIndex = 0;
        ParseObject selectCartParse;

        async private System.Threading.Tasks.Task SetCartNo()
        {
            //Takes the value from selected cart no,
            selectedCart = cartList.Find(t => t.Id == selectedCartIDIndex);
            
            //build a query for retrieving data from the parse table/class = Carts
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Carts");
            //based on the objectID of the selected Task, retrieve the object
            selectCartParse = await query.GetAsync(selectedCart.ObjectID);
        }

        private void SetUpCartPicker()
        {
            //setup the picker and the model
            PickerModel model = new PickerModel(this.CartNoDict);
            model.PickerChanged += (sender, e) => {
                this.selectedCartNo = e.SelectedValue;
                this.selectedCartIDIndex = e.SelectedIndex;
            };

            UIPickerView picker = new UIPickerView();
            picker.ShowSelectionIndicator = true;
            picker.Model = model;

            //setup the toolbar
            UIToolbar toolbar = new UIToolbar();
            toolbar.BarStyle = UIBarStyle.Default;
            toolbar.Translucent = true;
            toolbar.SizeToFit();

            //create a 'done' button for the toolbar and add it to the toolbar
            UIBarButtonItem btnDone = new UIBarButtonItem("Done",
                UIBarButtonItemStyle.Done, async (s, e) =>
                {
					if (selectedCartNo == null) {
						this.txtCart.Text = CartNoDict[0];
						this.txtCart.ResignFirstResponder();
						await SetCartNo();
					} else {
						this.txtCart.Text = selectedCartNo;
						this.txtCart.ResignFirstResponder();
						await SetCartNo();
					}
                });
            //connects the button to the toolbar
            toolbar.SetItems(new UIBarButtonItem[] { btnDone }, true);

            //connect the picker view to the input field
            this.txtCart.InputView = picker;

            //display the toolbar over the pickers
            this.txtCart.InputAccessoryView = toolbar;
        }

        //***** End of Cart Picker Code *****


        //***** Start of Employee Picker Code *****
        public IList<string> employeeNameDict = new List<string> { };
        public List<Employee> employeeList = new List<Employee> { };

        public async System.Threading.Tasks.Task GetAllEmployees()
        {
            //initialized the list of employees
            employeeList = new List<Employee> { };
            employeeNameDict = new List<string> { };

            //get a list of tsks from parse and sort by updatedAt date
            var query = from employeeList in ParseObject.GetQuery("Employees")
                        where employeeList["User"] == ParseUser.CurrentUser
                        orderby employeeList["updatedAt"] descending
                        select employeeList;
            
            try
            {
                IEnumerable<ParseObject> employeeListResults = await query.FindAsync();

                //if the returned list from parse is not empty
                if (employeeListResults != null)
                {
                    foreach (var myEmployee in employeeListResults)
                    {
                        var employeeItem = new Employee()
                        {
                            Id = employeeList.Count + 1, //takes the count of the list 
                            ObjectID = myEmployee.ObjectId, //mycart is the variable for the items in the results
                            First_Name = myEmployee.Get<string>("First_Name"), //takes the year attribute from my cart and puts it in the object attribute of year
                            Last_Name = myEmployee.Get<string>("Last_Name"), //does the same for fleet_no Last_Name
                            Address = myEmployee.Get<string>("Address"),
                            City = myEmployee.Get<string>("City"),
                            State = myEmployee.Get<string>("State"),
                            Zip = myEmployee.Get<string>("Zip"),
                            Email = myEmployee.Get<string>("Email"),
                            Employed = myEmployee.Get<bool>("Employed"),
                        };
                        employeeList.Add(employeeItem);
                        employeeNameDict.Add(myEmployee.Get<string>("First_Name") +" " + myEmployee.Get<string>("Last_Name"));
                    }
                }

            }
            catch (Exception ex)
            {
                //display error message
                var error = ex.Message;
                var alert = new UIAlertView("Load Failed", "Sorry, we might be experiencing some connectivity difficulties. Please make sure you are connected to the internet." + error, null, "OK");
                alert.Show();
            }
        }

        public Employee selectedEmployee;
        public string selectedEmployeeName;
        public int selectedEmployeeIndex = 0;
        ParseObject selectEmployeeParse;

        async public System.Threading.Tasks.Task SetEmployee()
        {
            //Takes the value from selected cart no,
            selectedEmployee = employeeList.Find(t => t.Id == selectedEmployeeIndex);

            //build a query for retrieving data from the parse table/class = Carts
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Employees");
            //based on the objectID of the selected Task, retrieve the object
            selectEmployeeParse = await query.GetAsync(selectedEmployee.ObjectID);
        }

        private void SetUpEmployeePicker()
        {
            //setup the picker and the model
            PickerModel modelEmp = new PickerModel(this.employeeNameDict);
            modelEmp.PickerChanged += (sender, e) => {
                this.selectedEmployeeName = e.SelectedValue;
                this.selectedEmployeeIndex = e.SelectedIndex;
            };

            UIPickerView pickerEmp = new UIPickerView();
            pickerEmp.ShowSelectionIndicator = true;
            pickerEmp.Model = modelEmp;

            //setup the toolbar
            UIToolbar toolbarEmp = new UIToolbar();
            toolbarEmp.BarStyle = UIBarStyle.Default;
            toolbarEmp.Translucent = true;
            toolbarEmp.SizeToFit();

            //create a 'done' button for the toolbar and add it to the toolbar
            UIBarButtonItem btnDoneEmp = new UIBarButtonItem("Done",
                UIBarButtonItemStyle.Done, async (s, e) =>
                {
					if (selectedCartNo == null) {
						this.txtEmployee.Text = CartNoDict[0];
						this.txtEmployee.ResignFirstResponder();
						await SetEmployee();
					} else {
						this.txtEmployee.Text = selectedEmployeeName;
						this.txtEmployee.ResignFirstResponder();
						await SetEmployee();
					}
                });
            //connects the button to the toolbar
            toolbarEmp.SetItems(new UIBarButtonItem[] { btnDoneEmp }, true);

            //connect the picker view to the input field
            this.txtEmployee.InputView = pickerEmp;

            //display the toolbar over the pickers
            this.txtEmployee.InputAccessoryView = toolbarEmp;
        }

        //***** End of Employee Picker Code *****

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            //populate the page with the selected gaslog's information
            lblGasLogObjectID.Text = "Gas Log Id: " + currentGasLog.ObjectID;
            if(currentGasLog.Cart == null)
            {
                txtCart.Text = "";
            } else
            {
                txtCart.Text = currentGasLog.CartNo.ToString();

            }
            if (currentGasLog.Employee == null)
            {
                txtEmployee.Text = "";
            }
            else
            {
                txtEmployee.Text = currentGasLog.EmployeeName.ToString();
            }
            //txtEmployee.Text = Convert.ToString(currentGasLog.Employee);
            if (IsNewGasLog)
            {  //this is a new cart
                txtFueled.Text = "";
                txtWhen.Text = DateTime.Now.Date.ToString("d");
            }
            else //this is not a new cart
            {
                txtFueled.Text = Convert.ToString(currentGasLog.Fueled);
                txtWhen.Text = currentGasLog.When.ToString("d");
            }
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

        //this will be called before the view is displayed
        //it allows us to delegate work to the calling controller
        public void SetGasLog(GasLogController from, GasLog gasLog)
        {
            Delegate = from;
            currentGasLog = gasLog;
        }
        
    }
}
