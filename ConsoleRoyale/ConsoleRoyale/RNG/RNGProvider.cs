using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ConsoleRoyale.RNG
{
    /// <summary>
    /// An implementation of the random number generator
    /// </summary>
    public class RNGProvider : IRNGProvider
    {
        private readonly RandomNumberGenerator _randomNumberGenerator;

        public RNGProvider()
        {
            _randomNumberGenerator = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Get % based on TRNG
        /// </summary>
        /// <returns>A whole number as %,i.e. 12</returns>
        public int GetPercentageChance() => RandomNumberGenerator.GetInt32(1, 101);

        /// <summary>
        /// Gets random double using TRNG
        /// </summary>
        /// <returns>A random double in the interval (0, 1)</returns>
        public double GetRandomDouble()
        {
            /// This method is taken from a StackOverflow post:
            /// https://stackoverflow.com/a/2854635

            // Step 1: fill an array with 8 random bytes
            byte[]? bytes = new byte[8];
            _randomNumberGenerator.GetBytes(bytes);

            // Step 2: bit-shift 11 and 53 based on double's mantissa bits
            ulong ul = BitConverter.ToUInt64(bytes, 0) >> 11;
            double d = ul / (double)(1UL << 53);

            return d;
        }

        /// <summary>
        /// Gets a random floating-point number in a given interval where the interval is closed open [start, end)
        /// </summary>
        /// <param name="start">The start of the interval where it is inclusive</param>
        /// <param name="end">The interval's end where it is exclusive</param>
        /// <returns>A random floating-point number</returns>
        public decimal GetRandomDoubleInInterval(int start, int end)
        {
            var randomIntInInteval = RandomNumberGenerator.GetInt32(start, end);
            var result = randomIntInInteval * GetRandomDouble();
            return (decimal)(result < start ? start + result : result);
        }
    }
}
