using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        {
        //
        ALS als1 = new ALS(3,10000,100);
        als1.Execute(0.1,5);
        //
        // ALS als2 = new ALS(3,100000,1000);
        //
        //ALS als3 = new ALS(3,100000,10000);
        }
    }
}
