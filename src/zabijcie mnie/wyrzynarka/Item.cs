using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace wyrzynarka
{
    class Item : IComparable<Item>
    {
        
        public int ItemID { get; set; }
        public ArrayList Rates { get; set; }

        public Item(int itemID, ArrayList rates)
        {
            ItemID = itemID;
            Rates = rates;
        }

        public int CompareTo(Item other)
        {
            if (other == null) return 1;

            return Rates.Count.CompareTo(other.Rates.Count);
        }



    }
}
