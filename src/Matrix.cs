﻿using System;

namespace RecommenderSystem
{
	class Matrix
	{
		public double[,] Data { get; set; }
		public int RowCount { get; set; }
		public int ColumnCount { get; set; }

		public Matrix(int size)
		{
			RowCount = size;
			ColumnCount = size;
			Data = new double[size, size];
		}

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
			Fill(data);
		}


		public void Fill(double[,] numbers)
		{
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					Data[i, j] = numbers[i, j];
				}
			}
		}

		public void Fill()
		{
			for (int i = 0; i < RowCount; i++)
			{
				for (int j = 0; j < ColumnCount; j++)
				{
					Data[i, j] = GetRandomNumber();
				}
			}
		}

		private static double GetRandomNumber()
		{
			return GetRandRange() / Math.Pow(2, 16);
		}

		private static int GetRandRange()
		{
			int min = (int)Math.Round(-Math.Pow(2, 16));
			int max = (int)Math.Round(Math.Pow(2, 16) - 1);
			Random rand = new Random();
			return rand.Next(min, max);
		}

		public static Matrix operator *(Matrix leftFactor, Matrix rightFactor)
		{
			if (leftFactor.ColumnCount != rightFactor.RowCount)
				throw new ArgumentException("Cannot multiply, invalid size of matrices");

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