using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace wyrzynarka 
{
    [Serializable]
    class ItemFAT
    {
        public ArrayList Rates { get; set; }
        public string Group { get; set; }
        public int ItemID { get; set; }
        public ItemFAT(int itemID, string group)
        {
            this.ItemID = itemID;
            this.Group = group;
            this.Rates = new ArrayList();
        }

        public ItemFAT()
        {
            this.Rates = new ArrayList();
        }
    }
}
