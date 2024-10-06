namespace GameOfLife
{
    /// <summary>
    /// The board factory class is responsible for creating new boards
    /// </summary>
    public static class BoardFactory
    {
        // A dictionary of premade boards
        private static Dictionary<string, Board> premadeBoards;

        static BoardFactory()
        {
            premadeBoards = new Dictionary<string, Board>();

            premadeBoards.Add("BLINKER", new Board(new byte[]
            {
                0b00000000,
                0b00000000,
                0b00001000,
                0b00001000,
                0b00001000,
                0b00000000,
                0b00000000,
                0b00000000
            }));

            premadeBoards.Add("BEACON", new Board(new byte[]
            {
                0b00000000,
                0b00000110,
                0b00000110,
                0b00011000,
                0b00011000,
                0b00000000,
                0b00000000,
                0b00000000
            }));

            premadeBoards.Add("TOAD", new Board(new byte[]
            {
                0b00000000,
                0b00000000,
                0b00000000,
                0b00011100,
                0b00111000,
                0b00000000,
                0b00000000,
                0b00000000
            }));

            premadeBoards.Add("GLIDER", new Board(new byte[]
            {
                0b00000000,
                0b00000000,
                0b00001000,
                0b00000100,
                0b00011100,
                0b00000000,
                0b00000000,
                0b00000000
            }));

            premadeBoards.Add("FULL", new Board(new byte[]
            {
                0b11111111,
                0b11111111,
                0b11111111,
                0b11111111,
                0b11111111,
                0b11111111,
                0b11111111,
                0b11111111
            }));

        }

        /// <summary>
        /// Returns a premade board with the given name or a random board if the name is "RANDOM"
        /// </summary>
        /// <param name="name">The name of the premade board.</param>
        /// <exception cref="ArgumentException">Thrown when "name" is not available in the list of premade boards</exception>
        public static Board GetPremadeBoard(string name)
        {
            string upperName = name.ToUpper();
            if (premadeBoards.ContainsKey(upperName))
            {
                return premadeBoards[upperName];
            }

            if (upperName == "RANDOM")
            {
                return GetRandomBoard();
            }

            throw new ArgumentException($"The board {name} is not available.");
        }

        /// <summary>
        /// Returns a random board
        /// </summary>
        public static Board GetRandomBoard()
        {
            var random = new Random();
            return new Board(new byte[]
            {
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
                (byte)random.Next(0, 256),
            });
        }

        /// <summary>
        /// Checks if a board with the given name exists
        /// </summary>
        public static bool DoesBoardExist(string name)
        {
            string upperName = name.ToUpper();
            return upperName == "RANDOM" || premadeBoards.ContainsKey(upperName);
        }

        /// <summary>
        /// Returns the names of all available premade boards
        /// </summary>
        public static string[] GetAvailableBoards()
        {
            return premadeBoards.Keys.ToArray();
        }
    }
}
