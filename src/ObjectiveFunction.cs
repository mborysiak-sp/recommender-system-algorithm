using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace RecommenderSystem
{
	static class ObjectiveFunction
	{
		public static double Calculate(RMatrix R, Matrix U, Matrix P, double lambda)
		{
			double firstSum = 0, secondSum = 0, thirdSum = 0;

			for (int u = 0; u < U.ColumnCount; u++)
			{
				for (int p = 0; p < P.ColumnCount; p++)
				{
					Console.WriteLine($"first sum = {firstSum}");
					firstSum += Math.Pow(R[u, p] - (U.GetTransposed().GetVector(u) * P.GetVector(p) ).Data[0, 0], 2);
					Console.WriteLine($"first sum = {firstSum}");
				}
			}

			for (int u = 0; u < U.ColumnCount; u++)
			{
				secondSum += U.GetVector(u).GetSquaredNorm();
			}

			for (int p = 0; p < P.ColumnCount; p++)
			{
				thirdSum += P.GetVector(p).GetSquaredNorm();
			}

			return firstSum + lambda * (secondSum + thirdSum);
		}
	}
}
