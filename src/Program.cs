using System.Threading.Tasks;
// ReSharper disable InvalidXmlDocComment

namespace RecommenderSystem
{
    class Program
    {
        private static void Main(string[] args)
        {
            //domyślne wartości
            const int iloscFaktorow = 3;
            const int dawkaPliku = 10000;
            const int iloscProduktowMacierzyR = 50;
            const double lambda = 0.1;
            const int iterations = 10;
            const double percentToHide = 0.1;
            
            //NOWATORSKI SYSTEM WYBIERANIA ILOŚCI TESTÓW
            //WYSTARCZY ODKOMENTOWAĆ DANY ZAKRES
            
            Parallel.Invoke(
                    //test ilości faktorów
                ///*
                    () => new Test("TEST_1").Execute(3, dawkaPliku, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_2").Execute(6, dawkaPliku, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_3").Execute(9, dawkaPliku, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    //*/
                    
                    //test dawki pliku
                ///*
                    () => new Test("TEST_4").Execute(iloscFaktorow, 10000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_5").Execute(iloscFaktorow, 220000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_6").Execute(iloscFaktorow, 550000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                //*/
                    
                    //test ilości produktów macierzy R
                ///*
                    () => new Test("TEST_7").Execute(iloscFaktorow, dawkaPliku, 50, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_8").Execute(iloscFaktorow, dawkaPliku, 500, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_9").Execute(iloscFaktorow, dawkaPliku, 1050, lambda, iterations, percentToHide),
                //*/

                    //test lambdy
                ///*    
                    () => new Test("TEST_10").Execute(iloscFaktorow, dawkaPliku, iloscProduktowMacierzyR, 0.1, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_11").Execute(iloscFaktorow, dawkaPliku, iloscProduktowMacierzyR, 0.01, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_12").Execute(iloscFaktorow, dawkaPliku, iloscProduktowMacierzyR, 0.001, iterations, percentToHide)
                //*/
               
            );
        }
    }
}
