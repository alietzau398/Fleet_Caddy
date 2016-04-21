using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Drawing;
using System.Collections.Generic;

namespace Fleet_Caddy
{
	partial class GasLogController : UIViewController
	{
        List<GasLog> gasLogs;

        UITableView table;

		public GasLogController (IntPtr handle) : base (handle)
		{
            //Title="Gas Log";
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "GasLogSegue")
            {//set in story board
                var navctlr = segue.DestinationViewController as GasLogDetailController;

                if (navctlr != null)
                {
                    var source = tblGasLog.Source as GasLogTableSource;
                    var rowPath = tblGasLog.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctlr.SetGasLog(this, item);
                    //to be defined on the GasLogDetailController
                }
            }
        }

        public async System.Threading.Tasks.Task GetAllGasLog(int type)
        {
            //initialize the list of GasLogs
            gasLogs = new List<GasLog> { };

            var queryEnd = from gasLogList in ParseObject.GetQuery("Gas_Log")
                           orderby gasLogList["updatedAt"] descending
                           select gasLogList; ;
            var query1 = from gasLogList in ParseObject.GetQuery("Gas_Log")
                            orderby gasLogList["updatedAt"] descending
                            select gasLogList;
            var query2 = from gasLogList in ParseObject.GetQuery("Gas_Log")
                          orderby gasLogList["updatedAt"] descending
                          select gasLogList;
            //get a list of GasLogs from parse and sort by updatedAt date
            switch (type)
            {
                case 1:
                    queryEnd = query1; 
                    break;
                case 2:
                    queryEnd = query2;
                    break;
            }
            

            IEnumerable<ParseObject> gasLogListResult = await queryEnd.FindAsync();

            //if the returned list from parse is not empty
            if (gasLogListResult != null)
            {
                //loop through the results and create
                foreach(var myGasLog in gasLogListResult)
                {
                    var gasLogItem = new GasLog()
                    {
                        ObjectID = myGasLog.ObjectId,
                        Id = gasLogs.Count + 1,
                        Fueled = myGasLog.Get<double>("Fueled"),
                        When = myGasLog.Get<DateTime>("Date"),
                    };

                    gasLogs.Add(gasLogItem);
                }
            }
        }

        public void CreateGasLog()
        {
            //first add the gasLog to the underlying data
            var newId = gasLogs[gasLogs.Count - 1].Id + 1;
            var newGasLog = new GasLog() { Id = newId };

            //then open the detail view to edit it
            var detail = Storyboard.InstantiateViewController("GasLogDetailCon") as GasLogDetailController;
            detail.SetGasLog(this, newGasLog);
            detail.IsNewGasLog = true;
            NavigationController.PushViewController(detail, true);
        }

        async public void SaveGasLog(GasLog gasLog)
        {
            var oldGasLog = gasLogs.Find(t => t.Id == gasLog.Id);

            //parse code for saving a task
            if(oldGasLog != null)
            {
                //determine if this an update or new gaslog
                oldGasLog = gasLog;

                //build a query for retrieving data from parse table/class = Gas_Log
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Gas_Log");
                //based on the objectid of the selected gaslog, retrieve the object
                ParseObject updatedGasLog = await query.GetAsync(oldGasLog.ObjectID);

                updatedGasLog["Users"] = oldGasLog.User;
                updatedGasLog["Cart"] = oldGasLog.Cart;
                updatedGasLog["Employee"] = oldGasLog.Employee;
                updatedGasLog["Fueled"] = oldGasLog.Fueled;
                updatedGasLog["Date"] = oldGasLog.When;

                await updatedGasLog.SaveAsync();

            } else
            {
                //first, add the gasLog to the underlying data
                var newGasLog = gasLog;
                //add parse code for saving a gasLog
                var addGasLog = new ParseObject("Gas_Log");
                //this is the name of your class/table name on parse
                addGasLog["User"] = ParseUser.CurrentUser;
                addGasLog["Cart"] = newGasLog.Cart;
                addGasLog["Employee"] = newGasLog.Employee;
                addGasLog["Fueled"] = newGasLog.Fueled;
                addGasLog["Date"] = newGasLog.When;

                await addGasLog.SaveAsync();

                newGasLog.ObjectID = addGasLog.ObjectId;

                gasLogs.Add(newGasLog);
            }

            //get the latest list of tasks from parse and update the table view
            await GetAllGasLog(1);
            tblGasLog.Source = new GasLogTableSource(gasLogs, this);

            //now show the previous page
            NavigationController.PopViewController(true);
        }

        public async void DeleteGasLog(GasLog gasLog)
        {
            var oldGasLog = gasLogs.Find(t => t.Id == gasLog.Id);

            //build a query for retrieving data from the parse table/class = Gas_Log
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Gas_Log");

            //based on the objectID of the selected gaslog, retrieve the object
            ParseObject removeGasLog = await query.GetAsync(oldGasLog.ObjectID);

            //remove the object from parse
            await removeGasLog.DeleteAsync();

            //remove the object from memory
            gasLogs.Remove(oldGasLog);

            //get the latest list of tasks from parse and update the tableview
            await GetAllGasLog(1);
            tblGasLog.Source = new GasLogTableSource(gasLogs, this);

            NavigationController.PopViewController(true);
        }

        async public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //tblGasLog = new UITableView(View.Bounds); //defaults to plan style

            //populate the list from parse
            //call a method to get parse data 

            await GetAllGasLog(1);

            //before the page appears, go bind the table view to our data source
            //bind everytime
            tblGasLog.Source = new GasLogTableSource(gasLogs, this);
            Add(tblGasLog);
            tblGasLog.ReloadData();

            //perform any additional setup after loading the view 
            //this fires when the top bar items' add is clicked on 
            btnAdd.TouchUpInside += BtnAdd_TouchUpInside;
        }

        private void BtnAdd_TouchUpInside(object sender, EventArgs e)
        {
            CreateGasLog();
        }
    }
}
