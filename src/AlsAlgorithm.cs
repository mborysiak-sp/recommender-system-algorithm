namespace RecommenderSystem 
{

public class AlsAlgorithm {

	private RMatrix R;
		
	private Matrix P;
	private Matrix U;
	
    public void Execute(Matrix P, Matrix U, int d) 
    {
	    R = new RMatrix();
	    P = new Matrix(d, R.p);
	    U = new Matrix(d,R.u);
	    
	    Matrix a = new Matrix(d);// nwm czy d
	    Matrix aU = a * a.GetTransposed();
		aU.AddLambdaMatrix(0.1);
		
		for (int i = 0; i < R.p; i++) {
		    //aU
	    }

		Matrix b = new Matrix(d);// nwm czy d
		Matrix bP = b * b.GetTransposed();
		bP.AddLambdaMatrix(21.37);
	    //for (int i = 0; i < U; i++) {}












	    /*
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
			
	    
	    GaussianElimination gaussianElimination = new GaussianElimination();

	    Console.WriteLine($"Calculated X: {gaussianElimination.Calculate(a, b)}");

	    Console.WriteLine($"Original X: {x}");
	    
			
	    RMatrix R = Extractor.createR(10000, 100);

	    var test1 = R.FindAllProductsRatedByUser(150);
	    var test2 = R.FindAllUsersWhoRatedProduct(0);
	    RMatrix R2 = R;

	    var test3 = R.PrepareToHidingTest(2);
	    Console.WriteLine();
	    */
    }
}
}