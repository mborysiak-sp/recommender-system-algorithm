using System;
using System.Numerics;

namespace RecommenderSystem
{
public class Matrix
	{
		public double[,] Data { get; set; }
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }
		
		public Matrix(int rows, int columns)
		{
			RowCount = rows;
			ColumnCount = columns;
			Data = new double[rows, columns];
		}

		public Matrix(int rows, int columns, double[,] data)
		{
			RowCount = rows;
			ColumnCount = columns;
			Data = data;
		}

		public Matrix GetVector(int number)
		{
			Matrix matrix = new Matrix(RowCount, 1);

			for (int i = 0; i < RowCount; i++)
			{
				matrix.Data[i, 0] = Data[i, 0];
			}

			return matrix;
		}

		public void FillRandom() 
		{
			Random random = new Random();
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					Data[i, j] = random.NextDouble();
				}
			}
		}

		public double GetSquaredNorm()
		{
			double result = 0;

			if (ColumnCount != 1) {
				return result;
			}
			else
			{
				foreach (double value in Data)
					result += Math.Pow(value,2);

				return result;
			}
		}

		public static Matrix operator *(Matrix leftFactor, Matrix rightFactor) 
		{
			if (leftFactor.ColumnCount != rightFactor.RowCount) 
			{
				throw new ArgumentException("Cannot multiply, invalid size of matrices");
			}

			Matrix product = new Matrix(leftFactor.RowCount, rightFactor.ColumnCount);
			for (int i = 0; i < leftFactor.RowCount; i++)
			{
				for (int j = 0; j < rightFactor.ColumnCount; j++)
				{
					double sum = 0;

					for (int k = 0; k < rightFactor.RowCount; k++)
					{
						double leftValue = leftFactor.Data[i, k];
						double rightValue = rightFactor.Data[k, j];

						sum += leftValue * rightValue;
					}
					product.Data[i, j] = sum;
				}
			}
			
			return product;
		}

		public void SwapRows(int row1, int row2)
		{
			for (int i = 0; i < ColumnCount; i++)
			{
				double tmp = Data[row1, i];
				Data[row1, i] = Data[row2, i];
				Data[row2, i] = tmp;
			}
		}

		public Matrix GetTransposed() 
		{
			var transposedMatrix = new Matrix(ColumnCount,RowCount);
			
			for (int i = 0; i < RowCount; i++) 
			{
				for (int j = 0; j < ColumnCount; j++) 
				{
					transposedMatrix.Data[j, i] = Data[i, j];
				}
			}

			return transposedMatrix;
		}

		public void AddLambdaMatrix(double lambda) 
		{
			for(int i = 0; i < Math.Min(RowCount, ColumnCount); i++)
			{
				Data[i, i] += lambda;
			}
		}


        // też imo do wywalenia, mamy debugger cmon
		public override string ToString()
		{
			string result = "";
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					result += $"{Data[i, j]} \t";
				}
				result += "\n";
			}
			
			return result;
		}
	}
}
