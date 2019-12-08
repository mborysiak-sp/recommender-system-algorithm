using System;
using System.Collections.Generic;
using System.Text;

namespace RecommenderSystem
{
    class ALS
    {
        private RMatrix R;
        private Matrix U, P;
        
        U.FillRandom();
        P.FillRandom();

        private int countOfFactors;

        public ALS(int iloscFaktorow, int dawkaPliku, int iloscProduktowMacierzyR)
        {
            R = Extractor.createR(dawkaPliku, iloscProduktowMacierzyR);
            U = new Matrix(iloscFaktorow, R.u);
            P = new Matrix(iloscFaktorow, R.p);
            countOfFactors = iloscFaktorow;
            // Nw czy tak być powinno, że konstruktor domyślnie zapełnia randomami, no ale ok
            // Ok patrz linijka 33
        }

        public void start(double lambda, int numOfAlsIterations)
        {
            for (int k = 0; k < numOfAlsIterations; k++) 
            {
                for (int u = 0; u < R.u; u++) {
                    List<int> productsRatedByU = R.FindAllProductsRatedByUser(u);

                    // Nie, zdecydowanie nie
                    // Ale teraz grzebać w matrix nie będę
                    Matrix Piu = new Matrix(countOfFactors, productsRatedByU.Count);

                    for (int i = 0; i < productsRatedByU.Count; i++) {
                        for (int row = 0; row < countOfFactors; row++) {
                            Piu.Data[row, i] = P.Data[row, productsRatedByU[i]];
                        }
                    }
                    // W tym miejscu Piu jest gotowe do użytku

                    Matrix Au = Piu * Piu.GetTransposed();
                    Au.AddLambdaMatrix(lambda);
                    
// !!!          // Trzeba albo zmienić ten konstruktor, żeby nie dawał wartości losowych, 
                    // albo niech ktoś napisze funkcję która zeruje macierz Xdd
                    // inaczej to nie zadziała
                    Matrix Vu = new Matrix(countOfFactors, 1);

                    for (int col = 0; col < Piu.ColumnCount; col++) {
                        int rating = R[u, productsRatedByU[col]];

                        for (int row = 0; row < countOfFactors; row++) {
                            Vu.Data[row, 0] += rating * Piu.Data[row, col];
                        }
                    }
                    // W tym miejscu Vu jest gotowe do użytku

                    // Może by to dać statyczne?
                    GaussianElimination gauss = new GaussianElimination();
                    Matrix X = gauss.Calculate(Au, Vu);

                    for (int row = 0; row < countOfFactors; row++) {
                        U.Data[row, u] = X.Data[row, 0];
                    }
                }
                Console.WriteLine($"Iteracja {k}) Wartość funkcji celu = {ObjectiveFunction.Calculate(R, U, P,0.1)}");
            }

            for (int p = 0; p < R.p; p++)
            {
                // przekopiować i przepisać na P pętlę wyżej
            }
            
        }


    }
}

