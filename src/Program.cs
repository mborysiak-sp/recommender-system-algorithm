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
            const int dawkaPliku = 200000;
            const int iloscProduktowMacierzyR = 50;
            const double lambda = 0.1;
            const int iterations = 10;
            const double percentToHide = 0.1;
            
            //NOWATORSKI SYSTEM WYBIERANIA ILOŚCI TESTÓW
            //WYSTARCZY ODKOMENTOWAĆ DANY ZAKRES
            
            Parallel.Invoke(
                    //test ilości faktorów
                ///*
                    () => new Test("TEST_1_1").Execute(2, dawkaPliku, 50, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_2").Execute(3, dawkaPliku, 50, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_3").Execute(4, dawkaPliku, 50, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_4").Execute(2, dawkaPliku, 500, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_5").Execute(3, dawkaPliku, 500, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_6").Execute(4, dawkaPliku, 500, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_7").Execute(2, dawkaPliku, 1050, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_8").Execute(3, dawkaPliku, 1050, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_1_9").Execute(4, dawkaPliku, 1050, lambda, iterations, percentToHide)
                    
                //*/
                    
                    //test dawki pliku
                /*
                    () => new Test("TEST_4").Execute(iloscFaktorow, 10000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_5").Execute(iloscFaktorow, 220000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_6").Execute(iloscFaktorow, 550000, iloscProduktowMacierzyR, lambda, iterations, percentToHide),
                //*/
                    
                    //test ilości produktów macierzy R
                /*
                    () => new Test("TEST_7").Execute(iloscFaktorow, dawkaPliku, 50, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_8").Execute(iloscFaktorow, dawkaPliku, 500, lambda, iterations, percentToHide),
                    ///*
                    () => new Test("TEST_9").Execute(iloscFaktorow, dawkaPliku, 1050, lambda, iterations, percentToHide),
                //*/

                    //test lambdy
                /*    
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
