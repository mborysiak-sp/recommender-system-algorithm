using System.Threading.Tasks;

namespace RecommenderSystem
{
    class Program
    {
        private static void Main(string[] args) 
        {
            Parallel.Invoke(
                () => Test.Execute(3,10000,50,0.1, 10, 0.1),
                () => Test.Execute(3,10000,50,0.01, 10, 0.1),
                () => Test.Execute(3,10000,50,0.001, 10, 0.1)
            );
        }
    }
}
