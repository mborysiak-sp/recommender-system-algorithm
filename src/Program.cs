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

            RMatrix R = Extractor.createR(10000, 100);

            var test1 = R.FindAllProductsRatedByUser(150);
            var test2 = R.FindAllUsersWhoRatedProduct(0);
            RMatrix R2 = R;

            var test3 = R.PrepareToHidingTest(2);
            Console.WriteLine();


		}
	}
}
