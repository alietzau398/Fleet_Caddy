using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace Fleet_Caddy
{
    public class Cart
    {
        ///<summary>
        /// Represents a Cart.
        /// </summary>
        public Cart()
        {

        }
        
        public int Id { get; set; }
        public string ObjectID { get; set; }
        public string Fleet_No { get; set; }
        public int Year { get; set; }
        public ParseUser User { get; set; } //I don't think I need this but why not?
        public string Barcode_String { get; set; }
        public string Serial_No { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public ParseFile Photo { get; set; } //need a real var type
        public string Notes { get; set; }
        public bool Active { get; set; }
    }
}
