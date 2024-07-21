using System;
using System.Text;

namespace Ex02
{
    public static class Game
    {
        public const int k_MaxSizeOfRowOrColumn = 6;
        public const int k_MinSizeOfRowOrColumn = 4;

        public static bool IsValidPlayerName(string i_PlayerName)
        {
            bool isValidName = false;

            if (!string.IsNullOrWhiteSpace(i_PlayerName) && !(i_PlayerName.Length > 20) && !(i_PlayerName.Contains(" ")))
            {
                isValidName = true;
            }
            return isValidName;
        }

        public static bool IsValidCard(GameBoard i_GameBoard, int i_RowInput, int i_ColumnInput)
        {
            bool isValidCard;
            if (i_RowInput > k_MaxSizeOfRowOrColumn || i_RowInput < k_MinSizeOfRowOrColumn || i_ColumnInput > k_MaxSizeOfRowOrColumn || i_ColumnInput < k_MinSizeOfRowOrColumn)
            {
                isValidCard = false;
            }

            else if (i_GameBoard.GameBoardToPlay[i_RowInput, i_ColumnInput].IsOpen == true)
            {
                isValidCard = false;
            }

            else
            {
                isValidCard = true;
            }
            return isValidCard;
        }

        public static bool IsGameOver(GameBoard i_GameBoard)
        {
            return i_GameBoard.IsBoardFull();
        }

        public static void PressEnterToExit()
        {
            Console.WriteLine("Press Enter to exit...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                // Wait for the Enter key to be pressed
            }
        }

        public static string CheckPlayAgain()
        {
            Console.WriteLine(ConsoleRender.k_PlayAgainMessage);
            string? playerInput = Console.ReadLine();

            while (playerInput != "Y" && playerInput != "N")
            {
                Console.WriteLine("What you wrote is invalid (please write Y to play again or N to exit):");
                playerInput = Console.ReadLine();
            }

            return playerInput;
        }

    }
}
