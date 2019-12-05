using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ALS
{
    
    class Parser
    {
        private int productsCount;
        private int customersCount;

        ArrayList[] productsRatedByCustomer;
        ArrayList[] listOfCustomersWhoRatedProduct;

        private Dictionary<string, int> customers;
        private string path;
        private double[,] R;

        public double[,] GetR()
        {
            return R;
        }

        public Parser(string filename)
        {
            productsCount = 0;
            customersCount = 0;
            customers = new Dictionary<string, int>();
            path = "input/" + filename;
            StartReading();

            R = new double[customersCount, productsCount];
            FillRatings();

        }

        private void StartReading()
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();

                        if(line.Contains("Id:")) 
                        {
                            productsCount++;
                            for (int i = 0; i < 6; i++){sr.ReadLine();} //przewija 6 linijek po niej, zawierają rzeczy nieważne w algorytmie
                            line = sr.ReadLine();
                        }

                        if (line.Contains("cutomer:"))
                        {
                            string customerID = CarveUserId(line);
                            if (!customers.ContainsKey(customerID))
                            {
                                customers.Add(customerID, customersCount);
                                customersCount++;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        private string CarveUserId(string line) 
        {
            string pattern = "cutomer:.*rating:";
            string output = Regex.Match(line, pattern).Value;
            output = output[8..^7].Trim();

            return output;
        }

        private double CarveRating(string line)
        {
            string pattern = "rating:.*votes:";
            string output = Regex.Match(line, pattern).Value;
            double rate = Double.Parse(output[7..^7].Trim());

            return rate;
        }

        private void FillRatings()
        {

            productsRatedByCustomer = new ArrayList[customersCount];
            for (int i = 0; i < customersCount; i++)
            {
                productsRatedByCustomer[i] = new ArrayList();
            }

            listOfCustomersWhoRatedProduct = new ArrayList[productsCount];
            for (int i = 0; i < productsCount; i++)
            {
                listOfCustomersWhoRatedProduct[i] = new ArrayList();
            }

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    int currProductID = -1;
                    int currCustomerID = -1;

                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();

                        if (line.Contains("Id:"))
                        {
                            currProductID++;
                            for (int i = 0; i < 6; i++) { sr.ReadLine(); } //przewija 6 linijek po niej, zawierają rzeczy nieważne w algorytmie
                            line = sr.ReadLine();
                        }

                        if (line.Contains("cutomer:"))
                        {
                            currCustomerID = customers[CarveUserId(line)];
                            double rate = CarveRating(line);

                            R[currCustomerID, currProductID] = rate;

                            listOfCustomersWhoRatedProduct[currProductID].Add(currCustomerID);
                            productsRatedByCustomer[currCustomerID].Add(currProductID);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
