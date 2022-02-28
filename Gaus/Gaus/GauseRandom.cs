using System;
using System.Collections.Generic;
using System.Text;

namespace Gaus
{
    public class GauseRandom : IRandom
    {
        private readonly Random _rng;
        public GauseRandom(Random random)
        {
            _rng = random;
        }
        public double NextDouble()
        {
            double value = Math.Round(_rng.NextDouble() * 10);
            return value;
        }
    }
}
