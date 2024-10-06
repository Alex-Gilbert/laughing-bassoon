namespace GameOfLife
{
    /// <summary>
    /// The simulator class is responsible for mainting state
    /// and driving the Game of Life simulation
    /// </summary>
    public class Simulator
    {
        // The current generation of the simulation
        // we start with generation 0 and increment each time we update the board
        private int currentGeneration;

        // We store multiple boards to allow for double buffering
        // This allows us to update the board without modifying the current state
        // and prevent any memory allocation during the update
        // The current board is the board at index currentGeneration % 2
        private Board[] boards;

        // The rules of the game are stored in two boolean arrays
        // The first array stores the rules for cells that are alive
        // The second array stores the rules for cells that are dead
        // A living cell lives on to the next generation if live_rules[neighbors] is true
        // A dead cell becomes alive in the next generation if dead_rules[neighbors] is true
        // Storing the rules in arrays like this makes our application data driven and easy to modify
        private static bool[] live_rules;
        private static bool[] dead_rules;

        static Simulator()
        {
            live_rules = new bool[9];
            dead_rules = new bool[9];

            // Initialize the rules
            live_rules[0] = false;
            live_rules[1] = false;
            live_rules[2] = true;
            live_rules[3] = true;
            live_rules[4] = false;
            live_rules[5] = false;
            live_rules[6] = false;
            live_rules[7] = false;
            live_rules[8] = false;

            dead_rules[0] = false;
            dead_rules[1] = false;
            dead_rules[2] = false;
            dead_rules[3] = true;
            dead_rules[4] = false;
            dead_rules[5] = false;
            dead_rules[6] = false;
            dead_rules[7] = false;
            dead_rules[8] = false;
        }

        /// <summary>
        /// Constructs a simulator with the given initial board
        /// </summary>
        public Simulator(Board generation0)
        {
            boards = new[]{
                generation0,
                new Board(new byte[8])
            };
            currentGeneration = 0;
        }

        /// <summary>
        /// Updates the board to the next generation
        /// </summary>
        public void Update()
        {
            // Get the current board
            Board currentBoard = GetCurrentBoard();

            // Get the next board
            Board nextBoard = GetNextBoard();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    int neighbors = currentBoard.GetNeighborCount(x, y);
                    bool isAlive = currentBoard.IsCellAlive(x, y);
                    bool nextState = isAlive ? live_rules[neighbors] : dead_rules[neighbors];
                    nextBoard.SetCell(x, y, nextState);
                }
            }

            currentGeneration++;
        }

        /// <summary>
        /// Returns the current board
        /// </summary>
        public Board GetCurrentBoard()
        {
            return boards[currentGeneration % 2];
        }

        /// <summary>
        /// Returns the next board
        /// </summary>
        private Board GetNextBoard()
        {
            return boards[(currentGeneration + 1) % 2];
        }
    }
}
