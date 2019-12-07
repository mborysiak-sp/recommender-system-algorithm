using System;

namespace RecommenderSystem
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix a = new Matrix(3,1);
			a.Fill();
			Console.WriteLine($"A: \n{a}");
			
			//Matrix x = new Matrix(3, 1);
			//x.Fill();
			
			
			Matrix aTr = a.GetTransposed();
			Console.WriteLine($"Transposed A: \n{aTr}");

			Matrix result = a * aTr;

			result.AddLambdaMatrix(21.37);
			Console.WriteLine($"Au: \n{result}");

			/*
			GaussianElimination gaussianElimination = new GaussianElimination();

			Console.WriteLine($"Calculated X: {gaussianElimination.Calculate(a, b)}");

			Console.WriteLine($"Original X: {x}");
			*/
		}
	}
}
