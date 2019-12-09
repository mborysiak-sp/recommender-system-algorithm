using System;
using System.IO;

namespace RecommenderSystem
{
	internal static class ObjectiveFunction
	{
		static double lastResult = 1;
		
		public static double Calculate(RMatrix R, Matrix U, Matrix P, double lambda, string testName, DateTime dateTime)
		{
			double firstSum = 0, secondSum = 0, thirdSum = 0;

			for (int u = 0; u < U.ColumnCount; u++)
			{
				for (int p = 0; p < P.ColumnCount; p++)
				{
					firstSum += Math.Pow(R[u, p] - (U.GetVector(u).GetTransposed() * P.GetVector(p)).Data[0, 0], 2);
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

			double result = firstSum + lambda * (secondSum + thirdSum);

		    Console.WriteLine($"{result / lastResult * 100}");
		    lastResult = result;
		    
		    //nwm czy konieczne
		    ///////////////////////////////////////ZAPISYWANIE TO PLIKU
		    File.WriteAllText(
		        $@"../../../src/results/{testName} {dateTime.ToString().Replace(":", "_")}.txt",
		        $"Jakiś procent czy coś: {result / lastResult * 100}\n\n"
		        );
		    return result;
		}
	}
}
