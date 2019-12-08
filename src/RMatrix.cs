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
		Dictionary<Tuple<int, int>, int> ratings;
		public int u, p;

		public RMatrix()
		{
			ratings = new Dictionary<Tuple<int, int>, int>();
		}

		public void setSize(int u, int p)
		{
			this.u = u;
			this.p = p;
		}

		public void Add(int u, int p, int r)
		{
			var temp = Tuple.Create(u, p);

			if (!ratings.ContainsKey(temp))
			{
				ratings.Add(temp, r);
			}
		}

		public int this[int u, int p] => Search(u, p);

		private int Search(int u, int p)
		{
			var search = Tuple.Create(u, p);
			if (!ratings.ContainsKey(search))
			{
				return 0;
			}
			return ratings[search];
		}

		public List<int> FindAllProductsRatedByUser(int u)
		{
			List<int> list = new List<int>();

			for (int i = 0; i < p; i++)
			{
				if (Search(u, i) != 0)
				{
					list.Add(i);
				}
			}

			return list;
		}

		public List<int> FindAllUsersWhoRatedProduct(int p)
		{
			List<int> list = new List<int>();

			for (int i = 0; i < u; i++)
			{
				if (Search(i, p) != 0)
				{
					list.Add(i);
				}
			}

			return list;
		}

		public Dictionary<Tuple<int, int>, int> PrepareToHidingTest(double percent)
		{
			var listOfHiddenRatings = new Dictionary<Tuple<int, int>, int>();

			double total = 0;

			foreach (KeyValuePair<Tuple<int, int>, int> rating in ratings)
			{
				if (total > 100)
				{
					listOfHiddenRatings.Add(rating.Key, rating.Value);
					total -= 100;
				}
				total += percent;
			}

			foreach (KeyValuePair<Tuple<int, int>, int> rating in listOfHiddenRatings)
			{
				ratings.Remove(rating.Key);
			}

			return listOfHiddenRatings;
		}

		public void CheckFillDegree()
		{
			double notEmpty = 0, all = 0;

			for (int i = 0; i < u; i++)
			{
				for (int j = 0; j < p; j++)
				{
					if (Search(i, j) != 0)
					{
						notEmpty++;
					}
					all++;
				}
			}
			Console.WriteLine("\t{0:N2} % Matrix Filled (no duplicates)", notEmpty / all * 100);
		}
	}
}
