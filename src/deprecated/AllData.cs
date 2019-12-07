using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace wyrzynarka
{
    class AllData
    {
        ArrayList All;
        Dictionary<string, int> Groups;

        Pigeonhole[] AllCategorized;


        public AllData(int maxItems)
        {
            this.All = AmazonReader.ReadFile(maxItems);
            Groups = FindAllGroups();
            AllCategorized = new Pigeonhole[Groups.Count];
            DivideAll();
            All = null; // Bo po co to komu

            foreach (Pigeonhole pigeonhole in AllCategorized)
            {
                pigeonhole.FillDictionary();
            }
            ShowCoverage();
            //SortAllCategories();
        }

        private Dictionary<string, int> FindAllGroups()
        {
            var Groups = new Dictionary<string, int>();

            int counter = 0;
            foreach (ItemFAT item in All)
            {
                if (item.Group != null && !Groups.ContainsKey(item.Group))
                {
                    Groups.Add(item.Group, counter);
                    counter++;
                }
            }

            return Groups;
        }
        private void DivideAll() 
        {
            for (int i = 0; i < Groups.Count; i++)
            {
                AllCategorized[i] = new Pigeonhole();
            }

            foreach (ItemFAT item in All)
            {
                AllCategorized[Groups[item.Group]].items.Add(new Item(item.ItemID, item.Rates));
            }
        }

        public void ShowCoverage()
        {
            foreach (var item in Groups)
            {
                Console.WriteLine(item.Key + ": " +
                    AllCategorized[item.Value].GetCovering()*100 +
                    "% matrix filled\n\t" + 
                    AllCategorized[item.Value].items.Count + " Items\n\t" +
                    AllCategorized[item.Value].users.Count + " Users");
            }
        }

        public void SortAllCategories()
        {
            foreach (Pigeonhole item in AllCategorized)
            {
                item.Sort();
            }
        }
    }
}
