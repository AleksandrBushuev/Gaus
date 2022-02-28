using Gaus;
using System;

namespace GausExample
{

    class Program
    {
        static void Main(string[] args)
        {
           double [,] matrix  =  Gause.CreateMatrix(3, 3, new GauseRandom(new Random()));

            Gause test = new Gause(matrix);

            


        }
    }
}
