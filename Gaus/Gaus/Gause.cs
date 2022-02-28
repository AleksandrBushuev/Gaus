using System;

namespace Gaus
{
    public class Gause
    {
        private double[,] _matrix;

        public Gause(double[,] matrix)
        {
            _matrix = matrix;
        }

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


    }
}
