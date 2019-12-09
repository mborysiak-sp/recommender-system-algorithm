using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace RecommenderSystem
{
    class Extractor
    {
        bool CONSOLE_OUTPUT = false;

        List<ItemFAT> All;

        public static RMatrix createR(int dawkaPliku, int iloscProduktowMacierzyR)
        {
            Extractor e = new Extractor(dawkaPliku);
            return e.extractRfromBooks(iloscProduktowMacierzyR);
        }

        public Extractor(int dawkaPliku)
        {
            All = AmazonReader.ReadFile(dawkaPliku);
            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("Successfully parsed " + All.Count + " items");
            }
        }

        public RMatrix extractRfromBooks(int iloscProduktowMacierzyR)
        {
            Pigeonhole books = new Pigeonhole();

            foreach (ItemFAT item in All)
            {
                if (item.Group == "Book")
                {
                    books.items.Add(new Item(item.ItemID, item.Rates));
                }
            }
            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("Successfully extracted " + books.items.Count + " Books");
            }

            books.extractBestXIndexes(iloscProduktowMacierzyR);

            if (CONSOLE_OUTPUT)
            {
                Console.WriteLine("\t" + books.items.Count + " Products");
                Console.WriteLine("\t" + books.users.Count + " Users");
                Console.WriteLine("\t{0:N2} % Matrix Filled", books.GetCovering() * 100);
            }

            return books.extractToR();
        }


        public void showCoverageByCategory(int iloscProduktowMacierzyR)
        {
            Dictionary<string, Pigeonhole> Groups = new Dictionary<string, Pigeonhole>();

            foreach (ItemFAT item in All)
            {
                if (item.Group != null)
                {
                    if (!Groups.ContainsKey(item.Group))
                    {
                        Groups.Add(item.Group, new Pigeonhole());
                    }
                    Groups[item.Group].items.Add(new Item(item.ItemID, item.Rates));
                }
            }
            Console.WriteLine("Found " + Groups.Count + " groups within items");

            foreach (KeyValuePair<string, Pigeonhole> category in Groups)
            {
                category.Value.extractBestXIndexes(iloscProduktowMacierzyR);
            }

            foreach (KeyValuePair<string, Pigeonhole> category in Groups)
            {
                Console.WriteLine(category.Key + ":");
                Console.WriteLine("\t" + category.Value.items.Count + " Products");
                Console.WriteLine("\t" + category.Value.users.Count + " Users");
                Console.WriteLine("\t{0:N2} % Matrix Filled", category.Value.GetCovering() * 100);
            }
        }
    }

    class Pigeonhole
    {
        public List<Item> items;
        public Dictionary<string, int> users;

        //UWAGA NIE WYPEŁNIA DICTIONARY
        public Pigeonhole()
        {
            this.items = new List<Item>();
        }

        public Pigeonhole(List<Item> list)
        {
            this.items = list;
            FillDictionary();
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

        private void Sort()
        {
            items.Sort();
            items.Reverse();
        }

        //TO NIE SĄ PROCENTY
        //0.01 = 1%
        //goddamnit
        public double GetCovering()
        {
            double ratings = 0;
            foreach (Item item in items)
            {
                ratings += item.Rates.Count;
            }

            return ratings / ((double)items.Count * (double)users.Count);
        }

        public void extractBestXIndexes(int X)
        {
            Sort();
            if (this.items.Count > X)
            {
                this.items = this.items.GetRange(0, X);
                FillDictionary();
            }
            return;
        }

        public RMatrix extractToR()
        {
            RMatrix r = new RMatrix();
            int standardItemID = 0;

            foreach (Item item in items)
            {
                foreach (Tuple<string, int> ratingTuple in item.Rates)
                {
                    r.Add(users[ratingTuple.Item1], standardItemID, ratingTuple.Item2);
                }
                standardItemID++;
            }
            r.setSize(users.Count, items.Count);

            return r;
        }
    }

    class Item : IComparable<Item>
    {
        public int ItemID { get; set; }
        public List<Tuple<string, int>> Rates { get; set; }


        public Item()
        {
            this.Rates = new List<Tuple<string, int>>();
        }

        public Item(int itemID)
        {
            this.ItemID = itemID;
            this.Rates = new List<Tuple<string, int>>();
        }

        public Item(int itemID, List<Tuple<string, int>> rates)
        {
            this.ItemID = itemID;
            this.Rates = rates;
        }

        public int CompareTo(Item other)
        {
            if (other == null) return 1;

            return Rates.Count.CompareTo(other.Rates.Count);
        }
    }

    class ItemFAT
    {
        public List<Tuple<string, int>> Rates { get; set; }
        public string Group { get; set; }
        public int ItemID { get; set; }
        public ItemFAT(int itemID, string group)
        {
            this.ItemID = itemID;
            this.Group = group;
            this.Rates = new List<Tuple<string, int>>();
        }

        public ItemFAT()
        {
            this.Rates = new List<Tuple<string, int>>();
        }
    }

    class AmazonReader
    {

        public static List<ItemFAT> ReadFile(int maxItems)
        {
            List<ItemFAT> productsList = new List<ItemFAT>();

            try
            {
                ItemFAT item = new ItemFAT();
                using StreamReader sr = new StreamReader("../../../src/amazon-meta.txt");
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("Id:"))
                    {
                        if (item.Group != null)
                        {
                            productsList.Add(item);
                        }

                        item = new ItemFAT();
                        //if(productsList.Count > 480000)
                        //    Console.WriteLine(line);  //w razie znalezienia Id w np. nazwie produku
                        item.ItemID = Int32.Parse(line.Trim().Substring(3).Trim());

                        if (item.ItemID > maxItems)
                        {
                            break;
                        }
                    }

                    if (line.Contains("group:"))
                    {
                        item.Group = line.Trim().Substring(6).Trim();
                    }

                    if (line.Contains("cutomer:"))
                    {
                        string customerASIN = CarveUserId(line);
                        int rating = CarveRating(line);
                        var newRating = Tuple.Create(customerASIN, rating);
                        item.Rates.Add(newRating);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            return productsList;
        }

        private static string CarveUserId(string line)
        {
            string pattern = "cutomer:.*rating:";
            string output = Regex.Match(line, pattern).Value;
            output = output[8..^7].Trim();

            return output;
        }

        private static int CarveRating(string line)
        {
            string pattern = "rating:.*votes:";
            string output = Regex.Match(line, pattern).Value;
            int rate = Int32.Parse(output[7..^7].Trim());

            return rate;
        }

    }
}
