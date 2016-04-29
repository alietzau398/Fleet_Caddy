using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Fleet_Caddy
{
	partial class GasLogController : UIViewController
	{
        List<GasLog> gasLogs;

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

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public async System.Threading.Tasks.Task GetAllGasLog(int type)
        {
            //initialize the list of GasLogs
            gasLogs = new List<GasLog> { };

            //find the begining day of the week and the week before that
            DateTime beginWeek = StartOfWeek(DateTime.Now, DayOfWeek.Sunday);
            DateTime beginWeekBefore = beginWeek.AddDays(-7);

            var queryEnd = from gasLogList in ParseObject.GetQuery("Gas_Log")
                           where gasLogList["User"] == ParseUser.CurrentUser
                           orderby gasLogList["updatedAt"] descending
                           select gasLogList; ;
            var query1 = from gasLogList in ParseObject.GetQuery("Gas_Log")
                         where gasLogList["User"] == ParseUser.CurrentUser
                         orderby gasLogList["updatedAt"] descending
                            select gasLogList;
            var query2 = from gasLogList in ParseObject.GetQuery("Gas_Log")
                         where gasLogList["User"] == ParseUser.CurrentUser
                         where gasLogList.Get<DateTime>("BeginWeek") == beginWeek
                         orderby gasLogList["updatedAt"] descending
                          select gasLogList;
            var query3 = from gasLogList in ParseObject.GetQuery("Gas_Log")
                         where gasLogList["User"] == ParseUser.CurrentUser
                         where gasLogList.Get<DateTime>("BeginWeek") == beginWeekBefore
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
                case 3:
                    queryEnd = query3;
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
                        Cart = myGasLog.Get<ParseObject>("Cart"),
                        Fueled = myGasLog.Get<double>("Fueled"),
                        Employee = myGasLog.Get<ParseObject>("Employee"),
                        EmployeeName = myGasLog.Get<string>("EmployeeName"),
                        When = myGasLog.Get<DateTime>("Date"),
                        BeginWeek = myGasLog.Get<DateTime>("BeginWeek"),
                        CartNo = myGasLog.Get<int>("CartNo"),
                    };
                    gasLogs.Add(gasLogItem);
                }
            }
        }

        public void CreateGasLog()
        {
            int newId;
            //first add the gasLog to the underlying data
            if (gasLogs.Count == 0)
            {
                newId = 1;
            } else
            {
                newId = gasLogs[gasLogs.Count - 1].Id + 1;
            }
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
                updatedGasLog["CartNo"] = oldGasLog.CartNo;
                updatedGasLog["Employee"] = oldGasLog.Employee;
                updatedGasLog["EmployeeName"] = oldGasLog.EmployeeName;
                updatedGasLog["Fueled"] = oldGasLog.Fueled;
                updatedGasLog["BeginWeek"] = oldGasLog.BeginWeek;
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
                addGasLog["CartNo"] = newGasLog.CartNo;
                addGasLog["Employee"] = newGasLog.Employee;
                addGasLog["EmployeeName"] = newGasLog.EmployeeName;
                addGasLog["Fueled"] = newGasLog.Fueled;
                addGasLog["BeginWeek"] = newGasLog.BeginWeek;
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

            GasLogView.BackgroundColor = UIColor.FromPatternImage(UIImage.FromFile("Background1024x1024-01.png"));

            //tblGasLog = new UITableView(View.Bounds); //defaults to plan style

            //populate the list from parse
            //call a method to get parse data 
            try
            {
                await GetAllGasLog(1);
            } catch (Exception ex)
            {
                //display error message
                var error = ex.Message;
                var alert = new UIAlertView("Load Failed", "Sorry, we might be experiencing some connectivity difficulties. Please make sure you are connected to the internet." + error, null, "OK");
                alert.Show();
            }

            //before the page appears, go bind the table view to our data source
            //bind everytime
            tblGasLog.Source = new GasLogTableSource(gasLogs, this);
            Add(tblGasLog);
            tblGasLog.ReloadData();

            //perform any additional setup after loading the view 
            //this fires when the top bar items' add is clicked on 
            btnAdd.TouchUpInside += BtnAdd_TouchUpInside;
			btnAdd.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnAdd.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnAdd.Layer.BorderColor = UIColor.White.CGColor;
			btnAdd.Layer.BorderWidth = System.nfloat.Parse("2.0");

			btnShow1st.TouchUpInside += BtnShow1st_TouchUpInside;
			btnShow1st.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnShow1st.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnShow1st.Layer.BorderColor = UIColor.White.CGColor;
			btnShow1st.Layer.BorderWidth = System.nfloat.Parse("2.0");

            btnShow2nd.TouchUpInside += BtnShow2nd_TouchUpInside;
			btnShow2nd.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnShow2nd.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnShow2nd.Layer.BorderColor = UIColor.White.CGColor;
			btnShow2nd.Layer.BorderWidth = System.nfloat.Parse("2.0");

			btnShowAll.TouchUpInside += BtnShowAll_TouchUpInside;
			btnShowAll.SetTitleColor(UIColor.White, UIControlState.Normal);
			btnShowAll.BackgroundColor = UIColor.FromRGB(93, 203, 235);
			btnShowAll.Layer.BorderColor = UIColor.White.CGColor;
			btnShowAll.Layer.BorderWidth = System.nfloat.Parse("2.0");

            //lblTest.Text = beginWeek.ToString("d") + " " + beginWeekBefore.ToString("d");
            GasLogGroupBy();
        }

        async public void BtnShowAll_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                await GetAllGasLog(1);
                tblGasLog.Source = new GasLogTableSource(gasLogs, this);
                Add(tblGasLog);
                tblGasLog.ReloadData();
            } catch (Exception ex)
            {
                //display error message
                var error = ex.Message;
                var alert = new UIAlertView("Load Failed", "Sorry, we might be experiencing some connectivity difficulties. Please make sure you are connected to the internet." + error, null, "OK");
                alert.Show();
            }
        }

        async public void BtnShow2nd_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                await GetAllGasLog(3);
                tblGasLog.Source = new GasLogTableSource(gasLogs, this);
                Add(tblGasLog);
                tblGasLog.ReloadData();
            }
            catch (Exception ex)
            {
                //display error message
                var error = ex.Message;
                var alert = new UIAlertView("Load Failed", "Sorry, we might be experiencing some connectivity difficulties. Please make sure you are connected to the internet." + error, null, "OK");
                alert.Show();
            }
        }

        async public void BtnShow1st_TouchUpInside(object sender, EventArgs e)
        {
            try
            {
                await GetAllGasLog(2);
                tblGasLog.Source = new GasLogTableSource(gasLogs, this);
                Add(tblGasLog);
                tblGasLog.ReloadData();
            }
            catch (Exception ex)
            {
                //display error message
                var error = ex.Message;
                var alert = new UIAlertView("Load Failed", "Sorry, we might be experiencing some connectivity difficulties. Please make sure you are connected to the internet." + error, null, "OK");
                alert.Show();
            }
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            GasLogGroupBy();
        }

        public void GasLogGroupBy()
        {
            DateTime beginWeek = StartOfWeek(DateTime.Now, DayOfWeek.Sunday);
            DateTime beginWeekBefore = beginWeek.AddDays(-7);
            double total = gasLogs.FindAll(t => t.BeginWeek == beginWeek).Sum(item => item.Fueled);
            double totalBefore = gasLogs.FindAll(t => t.BeginWeek == beginWeekBefore).Sum(item => item.Fueled);
            lblGalBegin.Text = total.ToString() + " Gal.";
            lblGalBeginBefore.Text = totalBefore.ToString() + " Gal.";
        }

        private void BtnAdd_TouchUpInside(object sender, EventArgs e)
        {
            CreateGasLog();
        }
    }
}
