using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Program
    {
        private static void Main(string[] args) 
        {
            Parallel.Invoke(
                () => new Test("TEST_1").Execute(3,10000,50,0.1, 10, 0.1)
                /*
                () => new Test("TEST_2").Execute(3,10000,50,0.1, 10, 0.1),
                () => new Test("TEST_3").Execute(3,10000,1050,0.001, 10, 0.1),
                
                () => new Test("TEST_4").Execute(3,10000,50,0.1, 10, 0.1),
                () => new Test("TEST_5").Execute(3,10000,50,0.1, 10, 0.1),
                () => new Test("TEST_6").Execute(3,10000,50,0.1, 10, 0.1),
                
                () => new Test("TEST_7").Execute(3,10000,50,0.1, 10, 0.1),
                () => new Test("TEST_8").Execute(3,10000,50,0.1, 10, 0.1),
                () => new Test("TEST_9").Execute(3,10000,50,0.1, 10, 0.1)
                */
            );
        }
    }
}
