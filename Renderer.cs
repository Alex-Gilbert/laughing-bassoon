namespace GameOfLife 
{
    public class Renderer
    {
        private char[] symbols;

        /// <summary>
        /// Constructs a renderer with the given symbols for dead and alive cells
        /// </summary>
        public Renderer(char dead, char alive)
        {
            symbols = new []
            {
                dead,
                alive
            };
        }

        /// <summary>
        /// Renders the board to the console
        /// </summary>
        public void Render(Board board)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Console.Write(symbols[board.IsCellAlive(x, y) ? 1 : 0]);

                    if (x != 7)
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
