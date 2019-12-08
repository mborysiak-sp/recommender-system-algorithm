using System;
using System.Collections.Generic;
using System.Text;

namespace RecommenderSystem
{
    class ALS
    {
        private RMatrix R;
        private Matrix U, P;

        private int countOfFactors;

        public ALS(int iloscFaktorow, int dawkaPliku, int iloscProduktowMacierzyR)
        {
            R = Extractor.createR(dawkaPliku, iloscProduktowMacierzyR);
            U = new Matrix(iloscFaktorow, R.u);
            U.FillRandom();
            P = new Matrix(iloscFaktorow, R.p);
            P.FillRandom();
            countOfFactors = iloscFaktorow;
        }

        public void Execute(double lambda, int numOfAlsIterations)
        {
            for (int k = 0; k < numOfAlsIterations; k++) 
            {
                for (int u = 0; u < R.u; u++) 
                {
                    List<int> productsRatedByU = R.FindAllProductsRatedByUser(u);
                    
                    Matrix Piu = new Matrix(countOfFactors, productsRatedByU.Count);

                    for (int i = 0; i < productsRatedByU.Count; i++) 
                    {
                        for (int row = 0; row < countOfFactors; row++) 
                        {
                            Piu.Data[row, i] = P.Data[row, productsRatedByU[i]];
                        }
                    }
                    // W tym miejscu Piu jest gotowe do użytku

                    Matrix Au = Piu * Piu.GetTransposed();
                    Au.AddLambdaMatrix(lambda);
                    
                    Matrix Vu = new Matrix(countOfFactors, 1);

                    for (int col = 0; col < Piu.ColumnCount; col++) 
                    {
                        int rating = R[u, productsRatedByU[col]];

                        for (int row = 0; row < countOfFactors; row++) 
                        {
                            Vu.Data[row, 0] += rating * Piu.Data[row, col];
                        }
                    }
                    // W tym miejscu Vu jest gotowe do użytku
                    
                    // Gauss statyczny : Krzychu
                    Matrix X = Gauss.Calculate(Au, Vu);

                    for (int row = 0; row < countOfFactors; row++) 
                    {
                        U.Data[row, u] = X.Data[row, 0];
                    }
                }
                
                for (int p = 0; p < R.p; p++)
                {
                    List<int> usersWhoRatedP = R.FindAllUsersWhoRatedProduct(p);
                    
                    Matrix Uip = new Matrix(countOfFactors, usersWhoRatedP.Count);

                    for (int i = 0; i < usersWhoRatedP.Count; i++) 
                    {
                        for (int row = 0; row < countOfFactors; row++) 
                        {
                            Uip.Data[row, i] = U.Data[row, usersWhoRatedP[i]];
                        }
                    }
                    // W tym miejscu Uip jest gotowe do użytku
                    
                    Matrix Bu = Uip * Uip.GetTransposed();
                    Bu.AddLambdaMatrix(lambda);
                    
                    Matrix Wp = new Matrix(countOfFactors, 1);

                    for (int col = 0; col < Uip.ColumnCount; col++) 
                    {
                        int rating = R[p, usersWhoRatedP[col]];

                        for (int row = 0; row < countOfFactors; row++) 
                        {
                            Wp.Data[row, 0] += rating * Uip.Data[row, col];
                        }
                    }
                    // W tym miejscu Wp jest gotowe do użytku
                    
                    Matrix X = Gauss.Calculate(Bu, Wp);

                    for (int row = 0; row < countOfFactors; row++) 
                    {
                        P.Data[row, p] = X.Data[row, 0];
                    }
                }
                
                Console.WriteLine($"Iteracja {k + 1}) Wartość funkcji celu = {ObjectiveFunction.Calculate(R, U, P,0.1)}");
            }
        }
    }
}

