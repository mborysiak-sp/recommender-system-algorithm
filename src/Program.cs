using System.Threading.Tasks;
// ReSharper disable InvalidXmlDocComment

namespace RecommenderSystem
{
    class Program
    {
        private static void Main(string[] args) 
        {
            //NOWATORSKI SYSTEM WYBIERANIA ILOŚCI TESTÓW
            //WYSTARCZY ODKOMENTOWAĆ DANY ZAKRES
            
            Parallel.Invoke(
                ///*
                
                //dawka: 10000
                () => new Test("TEST_1").Execute(3,10000,50,0.1, 10, 0.1)
                ///*
                ,
                () => new Test("TEST_2").Execute(3,10000,500,0.1, 10, 0.1)
                ///*
                ,
                () => new Test("TEST_3").Execute(3,10000,1050,0.1, 10, 0.1)
                ///*
                ,
                
                //dawka: 200000
                () => new Test("TEST_4").Execute(3,200000,50,0.1, 10, 0.1)
                ////*
                ,
                () => new Test("TEST_5").Execute(3,200000,50,0.1, 10, 0.1)
                ///*
                ,
                () => new Test("TEST_6").Execute(3,200000,50,0.1, 10, 0.1)
                
                ///*
                ,
                
                //dawka: 550000
                () => new Test("TEST_7").Execute(3,550000,50,0.1, 10, 0.1)
                ///*
                ,
                () => new Test("TEST_8").Execute(3,550000,50,0.1, 10, 0.1)
                ///*
                ,
                () => new Test("TEST_9").Execute(3,550000,50,0.1, 10, 0.1)
                //*/
            );
        }
    }
}
