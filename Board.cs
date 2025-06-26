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
                   CheckDiagonalUp(lastRow, lastCol, player) ||
                   CheckDiagonalDown(lastRow, lastCol, player);
        }

        // Helper method: Check for 4 in a row horizontally
        private bool CheckHorizontal(int row, int col, char player)
        {
            int count = 0;
            // Check to the left and right of the current position
            for (int c = 0; c < Cols; c++)
            {
                if (grid[row, c] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        // Helper method: Check for 4 in a row vertically
        private bool CheckVertical(int row, int col, char player)
        {
            int count = 0;
            // Check above and below the current position
            for (int r = 0; r < Rows; r++)
            {
                if (grid[r, col] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        // Helper method: Check for 4 in a row diagonally (up-right)
        private bool CheckDiagonalUp(int row, int col, char player)
        {
            int count = 0;
            // Find the starting position (bottom-left of the diagonal)
            int startRow = row + col;
            if (startRow >= Rows) startRow = Rows - 1;
            int startCol = 0;
            if (startRow - row + col < Cols) startCol = startRow - row + col;
            // Now check along the diagonal
            for (int r = startRow, c = startCol; r >= 0 && c < Cols; r--, c++)
            {
                if (grid[r, c] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
                }
            }
            return false;
        }

        // Helper method: Check for 4 in a row diagonally (down-right)
        private bool CheckDiagonalDown(int row, int col, char player)
        {
            int count = 0;
            // Find the starting position (top-left of the diagonal)
            int startRow = row - col;
            if (startRow < 0) startRow = 0;
            int startCol = col - row;
            if (startCol < 0) startCol = 0;
            // Now check along the diagonal
            for (int r = startRow, c = startCol; r < Rows && c < Cols; r++, c++)
            {
                if (grid[r, c] == player)
                {
                    count++;
                    if (count == 4) return true;
                }
                else
                {
                    count = 0;
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
