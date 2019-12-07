using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace wyrzynarka
{
    class Pigeonhole
    {

        public ArrayList items;
        public Dictionary<string, int> users;

        public Pigeonhole()
        {
            this.items = new ArrayList();
        }

        public void FillDictionary()
        {
           
            users = new Dictionary<string, int>();
            int counter = 0;

            foreach (Item item in items)
            {
                foreach (Tuple<string, int> rating in item.Rates)
                {
                    string userASIN = rating.Item1;
                    if (!users.ContainsKey(userASIN))
                    {
                        users.Add(userASIN, counter);
                        counter++;
                    }
                }
            }
        }

        public void Sort()
        {
            //nie działa
            items.Sort();
        }

        public double GetCovering()
        {
            double ratings = 0;
            foreach (Item item in items)
            {
                ratings += item.Rates.Count;
            }

            return ratings / ((double)items.Count * (double)users.Count);
        }

    }
}
