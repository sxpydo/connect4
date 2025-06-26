namespace connect4
{
    public class Board
    {
        private char[,] grid;
        public int Rows { get; } = 6;
        public int Cols { get; } = 7;

        public Board()
        {
            grid = new char[Rows, Cols];
            // Fill the board with empty spaces ('.')
            for (int i = 0; i < Rows; i++)
                for (int j = 0; j < Cols; j++)
                    grid[i, j] = '.';
        }

        // Place a disc in the specified column for the given player
        // Returns true if successful, false if the column is full
        public bool PlaceDisc(int column, char player)
        {
            // Start from the bottom of the column and move up
            for (int row = Rows - 1; row >= 0; row--)
                if (grid[row, column] == '.')
                {
                    grid[row, column] = player;
                    return true;
                }
            return false;
        }

        // Check if the last move resulted in a win
        public bool CheckWin(int lastCol, char player)
        {
            // Find the row where the last disc was placed
            int lastRow = -1;
            for (int row = 0; row < Rows; row++)
                if (grid[row, lastCol] == player)
                {
                    lastRow = row;
                    break;
                }
            if (lastRow == -1) return false; // Should not happen if PlaceDisc was called

            // Check all possible directions for 4 in a row
            return CheckHorizontal(lastRow, lastCol, player) ||
                   CheckVertical(lastRow, lastCol, player) ||
                   CheckDiagonal(lastRow, lastCol, -1, 1, player) || // up-right
                   CheckDiagonal(lastRow, lastCol, 1, 1, player);   // down-right
        }

        // Helper method: Check for 4 in a row horizontally
        private bool CheckHorizontal(int row, int col, char player)
        {
            int count = 0;
            for (int c = 0; c < Cols; c++)
            {
                if (grid[row, c] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else count = 0;
            }
            return false;
        }

        // Helper method: Check for 4 in a row vertically
        private bool CheckVertical(int row, int col, char player)
        {
            int count = 0;
            for (int r = 0; r < Rows; r++)
            {
                if (grid[r, col] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else count = 0;
            }
            return false;
        }

        // Helper method: Check for 4 in a row diagonally
        // deltaRow and deltaCol are the direction steps (e.g., -1,1 for up-right)
        private bool CheckDiagonal(int row, int col, int deltaRow, int deltaCol, char player)
        {
            int count = 0;
            // Check up to 4 positions in each direction from the current disc
            for (int step = -3; step <= 3; step++)
            {
                int r = row + step * deltaRow;
                int c = col + step * deltaCol;
                // Make sure we stay inside the grid
                if (r >= 0 && r < Rows && c >= 0 && c < Cols)
                {
                    if (grid[r, c] == player)
                    {
                        count++;
                        if (count == 4) return true;
                    }
                    else count = 0;
                }
            }
            return false;
        }

        // Display the board with column numbers and color
        public void Display()
        {
            Console.WriteLine("1 2 3 4 5 6 7");
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (grid[i, j] == 'R')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(grid[i, j] + " ");
                        Console.ResetColor();
                    }
                    else if (grid[i, j] == 'Y')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(grid[i, j] + " ");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(grid[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
