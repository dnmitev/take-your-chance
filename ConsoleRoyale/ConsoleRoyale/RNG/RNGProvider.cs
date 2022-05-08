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

        public int GetPercentageChance() => RandomNumberGenerator.GetInt32(1, 101);


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

        public double GetRandomDoubleInInterval(int start, int end)
        {
            var randomIntInInteval = RandomNumberGenerator.GetInt32(start, end);
            var result = randomIntInInteval * GetRandomDouble();
            return result < start ? start + result : result;
        }
    }
}
