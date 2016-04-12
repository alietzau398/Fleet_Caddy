using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace Fleet_Caddy
{
    public class Employee
    {
        ///<summary>
        ///Represents an Employee 
        ///</summary>
        public Employee ()
        {
            
        }

        public int Id { get; set; }
        public string ObjectID { get; set; }
        public ParseUser User { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public bool Employed { get; set; }

    }
}
