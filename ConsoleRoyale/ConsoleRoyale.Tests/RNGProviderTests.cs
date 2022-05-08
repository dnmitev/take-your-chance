using ConsoleRoyale.RNG;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class RNGProviderTests
    {
        private const int ITERATIONS_COUNT = 1000000;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private RNGProvider _rng;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            _rng = new RNGProvider();
        }

        [TestMethod]
        public void GetPercentageChance_Should_Always_Return_Between_0_100()
        {
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                var result = _rng.GetPercentageChance();
                Assert.IsTrue(result > 0);
                Assert.IsTrue(result <= 100);
            }
        }

        [TestMethod]
        public void GetRandomDouble_Should_Return_Random_Numbers_From_0_To_1_As_Double()
        {
            // if the operation is done N times, the outcome should be constant
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                var result = _rng.GetRandomDouble();
                Assert.IsTrue(result >= 0 && result <= 1.0);
            }
        }

        [TestMethod]
        public void GetRandomDouble_Should_Distribute_Well()
        {
            var all = new List<double>();
            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                var result = _rng.GetRandomDouble();
                all.Add(result);
            }

            var max = all.Max();
            var min = all.Min();
            var avg = all.Average();

            Assert.AreNotEqual(0, min);
            Assert.AreNotEqual(1, max);

            Assert.AreEqual(0, 0.50 - Math.Round(avg, 2));
        }

        [TestMethod]
        public void GetRandomDoubleInInterval_Should_Always_Provide_Random_Within_Given_Interval()
        {
            var rnd = new Random();

            for (int i = 0; i < ITERATIONS_COUNT; i++)
            {
                var limit = rnd.Next(1, 1000);
                var start = rnd.Next(1, limit);
                var end = start + rnd.Next(limit, limit * 100);

                var result = _rng.GetRandomDoubleInInterval(start, end);

                Assert.IsTrue(result >= start);
                Assert.IsTrue(result < end);
            }
        }
    }
}
