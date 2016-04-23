using System;
using System.Collections.Generic;
using System.Text;
using Parse;

namespace Fleet_Caddy
{
    public class GasLog
    {
        ///<summary>
        /// Represents a Gas Log 
        ///</summary>
        public GasLog()
        {
        }

        public int Id { get; set; }
        public string ObjectID { get; set; }
        public ParseObject Cart { get; set; }
        public int CartNo { get; set; }
        public ParseUser User { get; set; }
        public ParseObject Employee { get; set; }
        public double Fueled { get; set; }
        public DateTime When { get; set; }
        public DateTime BeginWeek { get; set; }
    }
}
