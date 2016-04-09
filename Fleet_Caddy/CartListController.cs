using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Parse;
using System.Collections.Generic;
using System.Drawing;

namespace Fleet_Caddy
{
	partial class CartListController : UITableViewController
	{
        List<Cart> carts;

        UITableView table;

		public CartListController (IntPtr handle) : base (handle)
		{
            Title = "Fleet Manager";
		}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            //base.PrepareForSegue(segue, sender);
            if (segue.Identifier == "")
            { //set in storyboard
                var navctrl = segue.DestinationViewController as CartDetailController;

                if (navctrl != null)
                {
                    var source = TableView.Source as CartTableSource;
                    var rowPath = TableView.IndexPathForSelectedRow;
                    var item = source.GetItem(rowPath.Row);
                    navctrl.SetCart(this, item);
                    //to be defined on the cart detailviewcontroller
                }
            }
        }

        public async System.Threading.Tasks.Task GetAllCarts()
        {
            //Initialize the list of carts
            carts = new List<Cart> { };

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
                        Id = carts.Count + 1, //takes the count of the list 
                        ObjectID = myCart.ObjectId, //mycart is the variable for the items in the results
                        Year = myCart.Get<int>("Year"), //takes the year attribute from my cart and puts it in the object attribute of year
                        Fleet_No = myCart.Get<string>("Fleet_No"), //does the same for fleet_no
                    };
                    carts.Add(cartItem);
                }
            }
        }
        
        public void CreateCart()
        {
            //first add the cart to the underlying data
            var newId = carts[carts.Count - 1].Id + 1; //takes the id of the last cart and adds one to it
            var newCart = new Cart() { Id = newId };

            //then open the detail view to edit it
            var detail = Storyboard.InstantiateViewController("CartDetailCon") as CartDetailController;

            detail.SetCart(this, newCart);
            detail.IsNewTask = true;
            NavigationController.PushViewController(detail, true);
        }

        async public void SaveCart(Cart cart)
        {
            var oldCart = carts.Find(t => t.Id == cart.Id);//searches for the cart that was inputed into the method

            //parse code for saving a task
            if(oldCart != null) //determine if this is an update or new task
            {
                oldCart = cart;

                //build a query for retrieving data from the parse table/class = Carts
                ParseQuery<ParseObject> query = ParseObject.GetQuery("Carts");
                //based on the objectID of the selected Task, retrieve the object
                ParseObject updatedCart = await query.GetAsync(oldCart.ObjectID);

                updatedCart["Fleet_No"] = oldCart.Fleet_No;
                updatedCart["Year"] = oldCart.Year;
                updatedCart["User"] = ParseUser.CurrentUser;

                await updatedCart.SaveAsync(); //save the changes to parse
            } else
            {
                //first, add the cart to the underlying data
                var newCart = cart;
                //add parse code for saving a task
                var addCart = new ParseObject("Carts");
                //this is the name of your class/table name on parse

                addCart["Fleet_No"] = newCart.Fleet_No;
                addCart["Year"] = newCart.Year;
                addCart["User"] = ParseUser.CurrentUser;

                await addCart.SaveAsync(); //save the changes to parse

                newCart.ObjectID = addCart.ObjectId; //set the object ID and save it for later

                carts.Add(newCart);
            }

            //get the latest list of carts from parse and update the tableview
            await GetAllCarts();
            table.Source = new CartTableSource(carts, this);

            //now show the previous page
            NavigationController.PopViewController(true);
        }

        async public void DeleteCart (Cart cart, bool back)
        {
            var oldCart = carts.Find(t => t.Id == cart.Id);

            //build a query for retrieving data from the parse table/class = Carts
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Carts");

            //based on the objectID of the selected car, retrieve the object
            ParseObject removeCart = await query.GetAsync(oldCart.ObjectID);

            //Remove the object from parse
            await removeCart.DeleteAsync();

            //remove the object from memory
            carts.Remove(oldCart);

            //get the latest list of carts from parse and update the tableview
            await GetAllCarts();
            table.Source = new CartTableSource(carts, this);

            //if flagged, go back to previous page
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

            await GetAllCarts();

            //before the page appears, go bind the table view to our data source
            //bind every time
            table.Source = new CartTableSource(carts, this);
            Add(table);

            //perform any additional setup after loading the view
            //this fires when the top bar item's "+" icon is clicked on 
            btnAdd.Clicked += (sender, e) =>
            {
                //when the add button on the top bar is clicked, call the method
                CreateCart();
            };
        }
    }
}
