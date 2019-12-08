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
    }
}
}
