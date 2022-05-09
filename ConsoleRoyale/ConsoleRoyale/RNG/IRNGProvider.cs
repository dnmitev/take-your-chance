namespace ConsoleRoyale.RNG
{
    /// <summary>
    /// Random Number Generator (RNG) provider
    /// </summary>
    public interface IRNGProvider
    {
        /// <summary>
        /// Get chance in percen
        /// </summary>
        /// <returns></returns>
        int GetPercentageChance();

        /// <summary>
        /// Get random double number
        /// </summary>
        /// <returns>Return a random double number</returns>
        double GetRandomDouble();

        /// <summary>
        /// Get random floating-point number in a given interval
        /// </summary>
        /// <param name="start">The start of the interval</param>
        /// <param name="end">The end of the interval</param>
        /// <returns>A random floating-point number in the given interval</returns>
        decimal GetRandomDoubleInInterval(int start, int end);
    }
}
