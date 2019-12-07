using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace wyrzynarka
{
    class AmazonReader
    {

        public static ArrayList ReadFile(int maxItems) 
        {
            ArrayList productsList = new ArrayList();

            try
            {
                ItemFAT item = new ItemFAT();
                using StreamReader sr = new StreamReader("amazon.txt");
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();

                    if (line.Contains("Id:"))
                    {
                        if(item.Group != null)
                        {
                            productsList.Add(item);
                        }

                        item = new ItemFAT();
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
