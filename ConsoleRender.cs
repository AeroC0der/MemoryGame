using System;
using System.Diagnostics.Metrics;

namespace Ex02
{
	public class ConsoleRender
	{
        public const string k_WelcomeMassege = "Welcome to the The Memory Game";
        public const string k_PlayerOneName = "Enter first Player name:";
        public const string k_InvalidPlayerName = "Player name cannot be empty. Please enter a valid name.";
        public const string k_TypeOfPlayerToPlayAgainst = "If you want to play against the computer, press CO. If you want to play against a friend, press FR";
        public const string k_PlayerTwoName = "Enter second player name:";
        public const string k_InvalidTypeOfPlayer = "Invalid input. Please enter CO to play against the computer or FR to play against a friend.";
        public const string k_NumberOfRows = "Enter the number of rows on board (between 4 to 6):";
        public const string k_NumberOfColumns = "Enter the number of columns on board (between 4 to 6):";
        public const string k_InvalidTotalNumberOfCells = "The total number of cells must be even. Please enter valid dimensions.";
        public const string k_NumberNotInTheRange = "Number needs to be an integer between 4 to 6";
        public const string k_InvalidNumberInput = "Invalid input. Please enter numeric integer values.";
        public const string k_PlayerTurn = "Enter your card";
        public const string k_InvalidCard = "Invalid card please choose another one";
        public const string k_PlayAgainstComputer = "CO";
        public const string k_playAgainstPerson = "FR";
        public const string k_Quit = "Q";
        public const string k_GameOverMessage = "================== GAME OVER ==================";
        public const string k_PlayAgainMessage = "The game is over, please press Y to play again, or N to exit.";
        public const string k_ExitMessage = "Game quit by the user. Goodbye!";

        public static void DisplayBoard(GameBoard i_gameBoard)
        {
            Console.Clear();

            // Print column headers
            Console.Write("  ");
            for (char column = 'A'; column < 'A' + i_gameBoard.NumberOfColumnsOnBoard; column++)
            {
                Console.Write($"  {column} ");
            }
            Console.WriteLine();

            // Print top border
            Console.WriteLine(new string('=', i_gameBoard.NumberOfColumnsOnBoard * 5 - 2));

            for (int i = 0; i < i_gameBoard.NumberOfRowsOnBoard; i++)
            {
                // Print row number
                Console.Write($"{i + 1} ");

                for (int j = 0; j < i_gameBoard.NumberOfColumnsOnBoard; j++)
                {
                    // Print left border of each cell
                    Console.Write("|");

                    // Print card value or space
                    if (i_gameBoard.GameBoardToPlay[i, j].IsOpen)
                    {
                        Console.Write($" {i_gameBoard.GameBoardToPlay[i, j].CardMarks} ");
                    }
                    else
                    {
                        Console.Write("   ");
                    }
                }

                // Print right border of the last cell
                Console.WriteLine("|");

                // Print middle border
                Console.WriteLine(new string('=', i_gameBoard.NumberOfColumnsOnBoard * 5 - 2));
            }
        }

        public static void DisplayPlayersScore(Player[] i_Players)
        {
            foreach (Player player in i_Players)
            {
                Console.WriteLine($"{player.Name}'s score: {player.Score}");
            }
        }

        public static void DisplayWinner(Player i_Player)
        {
            Console.WriteLine($"Congratulations, {i_Player.Name}! you are the winner!");
        }
    }
}
