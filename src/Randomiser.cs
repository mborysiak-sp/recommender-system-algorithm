using System;
using System.Collections.Generic;
using System.Text;

namespace ALS
{
    class Randomiser
    {

        public static double[,] xd(int d, int size)
        {
            Random random = new Random();
            double[,] arr = new double[d, size];

            for (int i = 0; i < d; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    arr[i, j] = random.NextDouble();
                }
            }
            
            return arr;
        }

    }
}
