using System;

namespace RecommenderSystem
{
	class Program
	{
		static void Main(string[] args)
		{
			Matrix a = new Matrix(3);

			a.Fill();

			Matrix x = new Matrix(3, 1);

			x.Fill();

			Matrix b = a * x;

			GaussianElimination gaussianElimination = new GaussianElimination();

			Console.WriteLine($"Calculated X: {gaussianElimination.Calculate(a, b)}");

			Console.WriteLine($"Original X: {x}");

            Extractor.createR(100000, 100);
		}
	}
}
