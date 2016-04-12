using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Collections.Generic;
using System.Drawing;

namespace Fleet_Caddy
{
	partial class EmployeeListController : UITableViewController
	{
        //List<Employee> employees;
        List<Employee> employees;
        UITableView table;

		public EmployeeListController (IntPtr handle) : base (handle)
		{
            //Title = "Employees";
		}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "EmployeeSeque")
            {   //set in storyboar
                var navctrl = segue.DestinationViewController as EmployeeDetailController;

                if (navctrl != null)
                {
                    var source = TableView.Source as EmployeeTableSource;
                    var rowPath = TableView.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctrl.SetEmployee(this, item);
                    //to be defined on the employee detailviewcontroller
                }
            }
        }

        public async System.Threading.Tasks.Task GetAllEmployees()
        {
            //initialized the list of employees
            employees = new List<Employee> { };

            //get a list of tsks from parse and sort by updatedAt date
            var query = from employeeList in ParseObject.GetQuery("Employees")
                        where employeeList["User"] == ParseUser.CurrentUser
                        orderby employeeList["updatedAt"] descending
                        select employeeList;

            /*get a list of carts from parse and sort by updatedAt date
            var query = from cartList in ParseObject.GetQuery("Carts")
                        where cartList["User"] == ParseUser.CurrentUser
                        orderby cartList["updatedAt"] descending
                        select cartList;
            */
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
                        Id = employees.Count + 1, //takes the count of the list 
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
                    employees.Add(employeeItem);
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

        public void CreateEmployee()
        {
            //first add the task to the underlying data
            int newId;
            if (employees.Count > 0)
            {
                newId = employees[employees.Count - 1].Id + 1; //takes the id of the last cart and adds one to it
            } else
            {
                newId = 1; //takes the id of the last cart and adds one to it
            }
            
            var newEmployee = new Employee() { Id = newId };

            //then open the detail view to edit it
            var detail = Storyboard.InstantiateViewController("EmployeeDetailCon") as EmployeeDetailController;

            detail.SetEmployee(this,newEmployee);
            detail.IsNewEmployee = true;
            NavigationController.PushViewController(detail, true);
        }

        async public void SaveEmployee(Employee employee)
        {
            var oldEmployee = employees.Find(t => t.Id == employee.Id);

            //Parse Code for saving the employee
            if (oldEmployee != null)
            {
                //determine if this is an update or a new task
                oldEmployee = employee;

                //build a query for retrieving data from the parse table/class
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Employees");
                //based on on the objectId of the selected employee, retreieve the object
                ParseObject updatedEmployee = await query.GetAsync(oldEmployee.ObjectID);

                updatedEmployee["First_Name"] = oldEmployee.First_Name;
                updatedEmployee["Last_Name"] = oldEmployee.Last_Name;
                updatedEmployee["Address"] = oldEmployee.Address;
                updatedEmployee["City"] = oldEmployee.City;
                updatedEmployee["State"] = oldEmployee.State;
                updatedEmployee["Zip"] = oldEmployee.Zip;
                updatedEmployee["Email"] = oldEmployee.Email;
                updatedEmployee["Employed"] = oldEmployee.Employed;

                await updatedEmployee.SaveAsync(); //save the changes to parse
            } else
            {
                //first, add the task to the underlying data
                var newEmployee = employee;
                //add parse code for saving an employee
                var addEmployee = new ParseObject("Employees");
                //this is the name of your class/table name on parse
                addEmployee["First_Name"] = oldEmployee.First_Name;
                addEmployee["Last_Name"] = oldEmployee.Last_Name;
                addEmployee["Address"] = oldEmployee.Address;
                addEmployee["City"] = oldEmployee.City;
                addEmployee["State"] = oldEmployee.State;
                addEmployee["Zip"] = oldEmployee.Zip;
                addEmployee["Email"] = oldEmployee.Email;
                addEmployee["Employed"] = oldEmployee.Employed;
                await addEmployee.SaveAsync(); //save the changes to parse

                newEmployee.ObjectID = addEmployee.ObjectId;

                employees.Add(newEmployee);
            }

            //get the latest list of tasks from parse and update the tableview
            await GetAllEmployees();
            table.Source = new EmployeeTableSource(employees, this);

            //show the previous page
            NavigationController.PopViewController(true);
        }

        async public void DeleteEmployee(Employee employee, bool back)
        {
            var oldEmployee = employees.Find(t => t.Id == employee.Id);

            //build a query for retreiving data from the parse table/class = employees
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Employees");
            //based on the objectID of the selected employee, retrieve the object
            ParseObject removeEmployee = await query.GetAsync(oldEmployee.ObjectID);
            //remove the object from parse
            await removeEmployee.DeleteAsync();
            //remove employee from memory
            employees.Remove(oldEmployee);
            //get the latest list of employees from parse and update the tableview
            await GetAllEmployees();
            table.Source = new EmployeeTableSource(employees, this);

            if (back)
            {
                NavigationController.PopViewController(true);
            }
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            table = new UITableView(View.Bounds); //defaults to plain style
            table.AutoresizingMask = UIViewAutoresizing.All;

            //populate the list from parse
            //call a method to get parse data

            await GetAllEmployees();

            //before the page appears, go bind the table view to our data source
            //bind every time
            table.Source = new EmployeeTableSource(employees, this);
            Add(table);

            //perform any additional setup affer loading the view
            //this firses when the top bar's item's "+" icon is clicked on
            btnAdd.Clicked += (sender, e) =>
            {
                CreateEmployee();
            };
        }
    }
}
