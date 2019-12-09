using System;
using System.Diagnostics;
using System.IO;

namespace RecommenderSystem 
{
    public class Test 
    {
        public static void Execute(int iloscFaktorow, int dawkaPliku, int iloscProduktowMacierzyR, 
                            double lambda, int iterations, double percentToHide) 
        {
            DateTime dateTime = DateTime.Now;
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start(); 
            
            ALS als = new ALS(iloscFaktorow, dawkaPliku, iloscProduktowMacierzyR, dateTime);
            als.HidingTest(lambda, iterations, percentToHide);
            
            stopwatch.Stop();
            
            ///////////////////////////////////////ZAPISYWANIE TO PLIKU
            File.AppendAllText(
                $@"../../../src/results/Test {dateTime.ToString().Replace(":", "_")}.txt",
                "--------------------------------------------------------------------------------------\n" +
                $"Czas wykonania:             {stopwatch.Elapsed}\n" + 
                $"Ilość faktorów:             {iloscFaktorow}\n" +
                $"Dawka pliku:                {dawkaPliku}\n" +
                $"Ilosc produktow macierzy R: {iloscProduktowMacierzyR}\n" +
                $"Lambda:                     {lambda}\n" +
                $"Ilość iteracji:             {iterations}\n" +
                $"Zakryty procent :           {percentToHide}\n"
                );
        }
    }
}
