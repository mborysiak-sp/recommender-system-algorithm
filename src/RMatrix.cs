using System;
using System.Collections.Generic;
using System.Text;

namespace RecommenderSystem
{
    //Można używać jak zwykłej macierzy dwuwymiarowej 
    //  tj. [0,2] zwróci ocenę (1-5) dla u = 0 i p = 2, albo 0 jeśli tej oceny nie ma 
    //  Tylko jak wyskoczy poza index to zwraca 0 a nie rzuca nullpointerem
    //  może się to potem doda jak się komuś chce

    class RMatrix
    {
        Dictionary<Tuple<int,int>, int> rates;
        int u, p;

        public RMatrix()
        {
            this.rates = new Dictionary<Tuple<int, int>, int>();
        }

        public void setSize(int u, int p)
        {
            this.u = u;
            this.p = p;
        }

        public void Add(int u, int p, int r)
        {
            var temp = Tuple.Create(u, p);
            if (!rates.ContainsKey(temp))
                rates.Add(temp, r);
        }

        public int this[int u, int p] => Search(u, p);

        private int Search(int u, int p)
        {
            var search = Tuple.Create(u, p);
            if (!rates.ContainsKey(search))
                return 0;
            return rates[search];
        }

        public void checkFillDegree()
        {
            double notEmpty = 0;
            double all = 0;
            for (int i = 0; i < u; i++)
            {
                for (int j = 0; j < p; j++)
                {
                    if (Search(i, j) != 0)
                        notEmpty++;
                    all++;
                }
            }
            Console.WriteLine("\t{0:N2} % Matrix Filled (no duplicates)", notEmpty/all * 100);
        }
    }
}
