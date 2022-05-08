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
        /// Get random double in a given interval
        /// </summary>
        /// <param name="start">The start of the interval</param>
        /// <param name="end">The end of the interval</param>
        /// <returns>A random double number in the given interval</returns>
        double GetRandomDoubleInInterval(int start, int end);
    }
}
