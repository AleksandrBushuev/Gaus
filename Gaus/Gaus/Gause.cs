using System;
using System.Threading.Tasks;

namespace Gaus
{
    public static class Gause
    {        
        /// <summary>
        /// Создать матрицу
        /// </summary>
        /// <param name="row">Количество строк</param>
        /// <param name="column">Количество столбцов</param>
        /// <param name="random">Генератор чисел</param>
        /// <returns></returns>
        public static double[,] CreateMatrix(int row, int column, IRandom random)
        {
            if (row <= 0)
                throw new ArgumentException("Количество строк должно быть больше ноля");
            if (column <= 0)
                throw new ArgumentException("Количество столбцов должно быть больше ноля");

            double[,] matrix = new double[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    double value = random.NextDouble();
                    matrix[i, j] = value;
                }
            }
            return matrix;
        }

        public static double[] Calculate(double[,] matrix)
        {
            int countRows = matrix.GetLength(0); 
            double[,] buffer = new double[countRows, countRows + 1]; 

            for (int i = 0; i < countRows; i++)
                for (int j = 0; j < countRows + 1; j++)
                    buffer[i, j] = matrix[i, j];

            //Прямой ход   
            Parallel.For(0, countRows, numberRow =>
            {
                for (int numberColumn = 0; numberColumn < countRows + 1; numberColumn++)
                    buffer[numberRow, numberColumn] = buffer[numberRow, numberColumn] / matrix[numberRow, numberRow];

                for (int numberRowNext = numberRow + 1; numberRowNext < countRows; numberRowNext++)
                {
                    double m = buffer[numberRowNext, numberRow] / buffer[numberRow, numberRow];

                    for (int numberColumnNext = 0; numberColumnNext < countRows + 1; numberColumnNext++)
                        buffer[numberRowNext, numberColumnNext] = buffer[numberRowNext, numberColumnNext] - buffer[numberRow, numberColumnNext] * m; //Зануление элементов матрицы ниже первого члена, преобразованного в единицу
                }


                for (int i = 0; i < countRows; i++)
                    for (int j = 0; j < countRows + 1; j++)
                        matrix[i, j] = buffer[i, j];
            });           

            //Обратный ход (Зануление верхнего правого угла)
            for (int k = countRows - 1; k > -1; k--) //k-номер строки
            {
                for (int i = countRows; i > -1; i--) //i-номер столбца
                    buffer[k, i] = buffer[k, i] / matrix[k, k];
                for (int i = k - 1; i > -1; i--) //i-номер следующей строки после k
                {
                    double K = buffer[i, k] / buffer[k, k];
                    for (int j = countRows; j > -1; j--) //j-номер столбца следующей строки после k
                        buffer[i, j] = buffer[i, j] - buffer[k, j] * K;
                }
            }

            
            double[] result = new double[countRows];
            for (int i = 0; i < countRows; i++)
                result[i] = buffer[i, countRows];

            return result;
        }

    }
}
