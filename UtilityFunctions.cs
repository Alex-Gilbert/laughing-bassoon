namespace GameOfLife
{
    static class UtilityFunctions
    {
        /// <summary>
        /// Returns the value of x modulo m
        /// This version of modulo always returns a positive number
        /// </summary>
        public static int Modulo(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
