using System;
using System.Collections;


//https://www.sanfoundry.com/java-program-gaussian-elimination-algorithm/
//https://www.bragitoff.com/2018/02/gauss-elimination-c-program/

namespace Gauss {
internal class Matrix<T> where T : new() {
    
    private T[,] Data { get; set; }
    private int RowCount { get; set; }
    private int ColumnCount { get; set; }
    
    private ArrayList columnSwaps;
    
    public Matrix(int size) {
        RowCount = size;
        ColumnCount = size;
        Data = new T[size, size];
        columnSwaps = new ArrayList();
    }
    
    public Matrix(int rows, int columns) {
        RowCount = rows;
        ColumnCount = columns;
        Data = new T[rows, columns];
        columnSwaps = new ArrayList();
    }

    public void Fill(T[,] numbers) {
        for (int i = 0; i < RowCount; i++) {
            for (int j = 0; j < ColumnCount; j++) {
                Data[i, j] = numbers[i,j];
            }
        }
    }
    
    public void Fill() {
        for (int i = 0; i < RowCount; i++) {
            for (int j = 0; j < ColumnCount; j++) {
                Data[i, j] = GetRandomNumber(typeof(T));
            }
        }
    }
    
    private static dynamic GetRandomNumber(Type type) {
        if (type == typeof(float))
            return Convert.ToSingle(GetRandRange() / Math.Pow(2, 16));
        if (type == typeof(double))
            return GetRandRange() / Math.Pow(2, 16);
        return type == typeof(Fraction) ? 
            new Fraction(GetRandRange(), (int)Math.Pow(2, 16))
            : null;
    }

    private static int GetRandRange() {
        int min = (int)Math.Round(-Math.Pow(2, 16));
        int max = (int)Math.Round(Math.Pow(2, 16) - 1);
        Random rand = new Random();
        return rand.Next(min, max);
    }
    
    public static Matrix<T> operator *(Matrix<T> leftFactor, Matrix<T> rightFactor) {
        if (leftFactor.ColumnCount != rightFactor.RowCount)
            throw new ArgumentException("Cannot multiply, invalid size of matrices");

        Matrix<T> product = new Matrix<T>(leftFactor.RowCount, rightFactor.ColumnCount);
        for (int i = 0; i < leftFactor.RowCount; i++) {
            for (int j = 0; j < rightFactor.ColumnCount; j++) {
                T sum = new T();
                for (int k = 0; k < rightFactor.RowCount; k++) {
                    dynamic leftValue = leftFactor.Data[i, k];
                    dynamic rightValue = rightFactor.Data[k, j];
                    //check if value is Fraction
                    sum += leftValue * rightValue;
                }
                product.Data[i, j] = sum;
            }
        }
        return product;
    }
    
    public static Matrix<T> Concatenate(Matrix<T> matrix, Matrix<T> vector) {
        if (matrix.RowCount != vector.RowCount) 
            throw new ArgumentException("Matrices need to have the same number of rows.");

        Matrix<T> result = new Matrix<T>(matrix.RowCount, 
                                      matrix.ColumnCount + 1);
        for (int i = 0; i < matrix.RowCount; i++) {
            for (int j = 0; j < matrix.ColumnCount + 1; j++) {
                if (j == matrix.ColumnCount)
                    result.Data[i, j] = vector.Data[i, 0];
                else
                    result.Data[i, j] = matrix.Data[i, j];
            }
        }
        return result;
    }
    
    public void EliminateGaussian(Choice choice) {
        int percent = -1;
        for (int i = 0; i < RowCount; i++) {
            if (percent != i * 100 / RowCount) {
                percent = i * 100 / RowCount;
                Console.WriteLine(typeof(T) + " | " + choice + " | " + percent + "% of rows done");
            }
            switch (choice) {
                case Choice.None:
                    break;
                case Choice.Partial:
                    PartialChoice(i, i);
                    break;
                case Choice.Full:
                    FullChoice(i, i);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), choice, null);
            }
            for (int j = i + 1; j < RowCount; j++) {
                dynamic zerowany = Data[j, i];
                dynamic zerujacy = Data[i, i];
                dynamic multiplier = zerowany / zerujacy;
                for (int column = i; column < ColumnCount; column++) {
                    zerujacy = Data[i, column];
                    Data[j, column] -= zerujacy * multiplier;
                }
            }
        }
    }
    
    public void BackwardsOperation() {
        dynamic position, value, multiplier;
        int lastRow = RowCount;
        for (int row = lastRow - 1; row >= 0; row--) {
            for (int i = row + 1; i < lastRow; i++) {
                position = Data[row, i];
                value = Data[i, lastRow];
                Data[row, lastRow] -= position * value;
            }
            multiplier = Data[row, row];
            Data[row, lastRow] /= multiplier;
        }
    }

    private void PartialChoice(int row, int column) {
        int rowToSwap = GetRowToSwap(row, column);
        SwapRows(row, rowToSwap);
    }
    
    private int GetRowToSwap(int startRow, int column) {
        dynamic maxValue = Data[startRow, column];
        maxValue = Abs.GetAbs(maxValue);
        int rowOfMaxValue = startRow;
        dynamic val;
        for (int row = startRow + 1; row < RowCount; row++) {
            val = Data[row, column];
            val = Abs.GetAbs(val);
            if (val > maxValue) {
                maxValue = val;
                rowOfMaxValue = row;
            }
        }
        return rowOfMaxValue;
    }

    private void SwapRows(int row1, int row2) {
        for (int i = 0; i < ColumnCount; i++) {
            T tmp = Data[row1, i];
            Data[row1, i] = Data[row2, i];
            Data[row2, i] = tmp;
        }
    }
    
    //AUTOR: KORDIAN CERANOWSKI {
    private void FullChoice(int row, int column) {
        Tuple<int, int> coordinates = FindMaxInRowsAndColumns(row, column);
        SwapRows(row, coordinates.Item1);
        SwapColumns(column, coordinates.Item2);
    }

    private Tuple<int, int> FindMaxInRowsAndColumns(int startRow, int startColumn) {
        int rowOfMaxValue = startRow;
        int colOfMaxValue = startColumn;

        dynamic maxValue = Data[startRow, startColumn];
        maxValue = Abs.GetAbs(maxValue);

        dynamic temp;

        for (int row = startRow; row < RowCount; row++) {
            for (int col = startColumn; col < ColumnCount - 1; col++) {
                temp = Data[row, col];
                temp = Abs.GetAbs(temp);
                if (maxValue < temp) {
                    maxValue = temp;
                    rowOfMaxValue = row;
                    colOfMaxValue = col;
                }
            }
        }
        return Tuple.Create(rowOfMaxValue, colOfMaxValue);
    }

    private void SwapColumns(int column1, int column2) {
        if(column1 != column2) {
            dynamic temp;
            for (int row = 0; row < RowCount; row++) {
                temp = Data[row, column1];
                Data[row, column1] = Data[row, column2];
                Data[row, column2] = temp;
            }
            columnSwaps.Add(Tuple.Create(column1, column2));
        }
    }
    
    // Wyciąga wartości z ostatniej kolumny macierzy do tablicy, bardziej przejrzyste
    // Naprawia kolejność zepsutą mieszaniem kolumn
    private T[] ExtractLastColumn() {
        T[] outArr = new T[RowCount];

        for (int row = 0; row < RowCount; row++) {
            outArr[row] = Data[row, ColumnCount - 1];
        }

        columnSwaps.Reverse();
        foreach (Tuple<int, int> swap in columnSwaps) {
            T temp = outArr[swap.Item1];
            outArr[swap.Item1] = outArr[swap.Item2];
            outArr[swap.Item2] = temp;
        }

        return outArr;
    }

    public void ShowVector() {
        T[] vec = ExtractLastColumn();
        foreach (var i in vec) {
            Console.WriteLine(i);
        }
    }
    //}
    
    //chart x = norm, y = matrix size
    
    public override string ToString() {
        string result = "";
        for (int i = 0; i < RowCount; i++) {
            for (int j = 0; j < ColumnCount; j++) {
                result += $"{Data[i, j]} \t";
            }
            result += "\n";
        }
        return result;
    }
}
}
