using System.Numerics;

namespace GameOfLife
{
    /// <summary>
    /// Represents a state of the Game of Life board
    /// </summary>
    public class Board
    {
        // The board is an 8x8 array of cells
        // Each row of cells is stored as a byte
        // Each cell is a bit in the byte
        // 0 = dead, 1 = alive
        private byte[] cells;

        /// <summary>
        /// Constructs an empty board
        /// </summary>
        public Board()
        {
            cells = new byte[8];
        }

        /// <summary>
        /// Constructs a board with the given cells
        /// </summary>
        /// <param name="cells">An array of 8 bytes representing the board's cells</param>
        /// <exception cref="ArgumentException">Thrown when the array length is not 8</exception>
        public Board(byte[] cells)
        {
            if (cells.Length != 8)
            {
                throw new ArgumentException("The cells array must contain exactly 8 elements.");
            }
            this.cells = cells;
        }

        /// <summary>
        /// Given a position on the x-axis of the board, returns the mask for the x-th bit in a byte.
        /// The mask has a single bit set at the x-th position, from left to right.
        /// </summary>
        private byte GetByteMaskAt(int x)
        {
            // Use modulo to wrap around the x position within the range [0, 7] (board width)
            // Invert the x coordinate since the top bit of the byte represents the leftmost cell
            x = UtilityFunctions.Modulo(7 - x, 8);
            return (byte)(0b00000001 << x);
        }

        /// <summary>
        /// Given a position on the y-axis of the board, returns a reference to the byte at the given row.
        /// </summary>
        private ref byte GetRow(int y)
        {
            // Use modulo to wrap around the y position within the range [0, 7] (board height)
            y = UtilityFunctions.Modulo(y, 8);
            return ref cells[y];
        }
        /// <summary>
        /// Returns true if the cell at the given position is alive
        /// </summary>
        public bool IsCellAlive(int x, int y)
        {

            return (GetRow(y) & GetByteMaskAt(x)) != 0;
        }

        /// <summary>
        /// Sets the cell at the given position to the given value
        /// </summary>
        public void SetCell(int x, int y, bool value)
        {
            if (value)
            {
                GetRow(y) |= GetByteMaskAt(x);
            }
            else
            {
                GetRow(y) &= (byte)~GetByteMaskAt(x);
            }
        }

        /// <summary>
        /// Gets the number of "alive" neighbors of the cell at the given position
        /// </summary>
        public int GetNeighborCount(int x, int y)
        {
            // This function uses bit manipulation to count the number of alive neighbors of a cell
            // We first create a bit mask where the bits at the x-1 and x+1 positions are set
            // For example, when x = 3, the mask would be 00101000
            byte horizontalNeigborMask = (byte)(GetByteMaskAt(x - 1) | GetByteMaskAt(x + 1));

            // Calculate the mask for the center cell
            // This mask represents the position of the cell itself
            // We need this when counting the number of alive neighbors in the rows above and below
            byte centerMask = GetByteMaskAt(x);

            // Get the rows above, below and at the current position
            byte rowAbove = GetRow(y - 1);
            byte row = GetRow(y);
            byte rowBelow = GetRow(y + 1);

            // Using the masks, count the number of alive neighbors
            // BitOperations.PopCount is used to count the number of set bits in a byte
            int count = 0;
            count += BitOperations.PopCount((byte)(rowAbove & (horizontalNeigborMask | centerMask)));
            count += BitOperations.PopCount((byte)(rowBelow & (horizontalNeigborMask | centerMask)));
            count += BitOperations.PopCount((byte)(row & (horizontalNeigborMask)));

            return count;
        }

    }
}
