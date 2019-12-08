using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Program
    {
        static void Main(string[] args)
        { 
            ALS alsOne = new ALS(3, 10000, 50);
           
            ALS alsTwo = new ALS(3, 10000, 50);

            ALS alsThree = new ALS(3, 10000, 50);
            
            Parallel.Invoke(
                () => alsOne.HidingTest(0.001, 10, 1),
                () => alsOne.HidingTest(0.1, 10, 1),
                () => alsThree.HidingTest(0.1, 10, 1)
            ); 
        }
    }
}
