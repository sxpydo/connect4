using connect4;

namespace connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            Player playerRed = new Player('R', "Red");
            Player playerYellow = new Player('Y', "Yellow");
            Player currentPlayer = playerRed;

            while (true)
            {
                board.Display();
                Console.Write($"Player {currentPlayer.Name}, enter column (1-7): ");
                string? input = Console.ReadLine();

                // 1. Check for null input
                if (input == null)
                {
                    Console.WriteLine("No input detected. Please try again.");
                    continue;
                }

                // 2. Try to parse and validate the input
                int col;
                if (int.TryParse(input, out col) && col >= 1 && col <= 7)
                {
                    col--; // Convert to 0-based index
                    if (board.PlaceDisc(col, currentPlayer.Symbol))
                    {
                        if (board.CheckWin(col, currentPlayer.Symbol))
                        {
                            board.Display();
                            Console.WriteLine($"Player {currentPlayer.Name} wins!");
                            break;
                        }
                        currentPlayer = (currentPlayer == playerRed) ? playerYellow : playerRed;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
                }
            }
        }
    }
}
