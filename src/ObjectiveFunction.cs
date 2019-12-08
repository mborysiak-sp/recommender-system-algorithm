using System;
using System.Collections.Generic;
using System.Text;

namespace RecommenderSystem
{
	static class ObjectiveFunction
	{
		public static double Calculate(RMatrix R, Matrix U, Matrix P, double lambda)
		{
			double firstSum = 0, secondSum = 0, thirdSum = 0;
	
			for (int u = 0; u < U.RowCount; u++)
				for (int p = 0; p < P.RowCount; p++)
					firstSum += Math.Pow(R.Search(u, p) - (U.GetTransposed().GetVector(u) * P.GetVector(p)).Data[0, 0], 2);
			
			for (int u = 0; u < U.RowCount; u++)
				secondSum += U.GetVector(u).GetSquaredNorm();

			for (int p = 0; p < P.RowCount; p++)
				thirdSum += P.GetVector(p).GetSquaredNorm();

			return firstSum + lambda * (secondSum + thirdSum);
		}
	}
}
