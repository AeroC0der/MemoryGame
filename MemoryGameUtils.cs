using System;

namespace Ex02
{
    public class MemoryGameUtils
    {
        public static ePlayerType TypeOfSecondPlayer()
        {
            while (true)
            {
                Console.WriteLine(ConsoleRender.k_TypeOfPlayerToPlayAgainst);
                string? playerType = Console.ReadLine()?.ToUpper();

                if (playerType != null && ValidatorForPlayerType(playerType))
                {
                    return playerType == ConsoleRender.k_playAgainstPerson ? ePlayerType.Person : ePlayerType.Computer;
                }
                else
                {
                    Console.WriteLine(ConsoleRender.k_InvalidTypeOfPlayer);
                }
            }
        }

        private static bool ValidatorForPlayerType(string i_PlayerType)
        {
            return i_PlayerType == ConsoleRender.k_playAgainstPerson || i_PlayerType == ConsoleRender.k_PlayAgainstComputer;
        }

        internal static string GetPlayerName(string i_RequestMessage)
        {
            Console.WriteLine(i_RequestMessage);
            string? playerName;

            while (!Game.IsValidPlayerName(playerName = Console.ReadLine()))
            {
                Console.WriteLine(ConsoleRender.k_InvalidPlayerName);
                Console.WriteLine(i_RequestMessage);
            }
            return playerName;
        }

        internal static bool HandlePlayerMove(GameEngine i_GameEngine, string i_RequestMessage)
        {
            bool validFirstInput = false;
            bool isMoveSucceed = true;
            int row, col;

            while (!validFirstInput)
            {
                string? playerMove = GetUserMove(i_RequestMessage);

                if (playerMove != null && playerMove == ConsoleRender.k_Quit)
                {
                    Console.WriteLine("Game quit by the player.");
                    isMoveSucceed = false;
                    Game.PressEnterToExit();
                    break;
                }

                if (playerMove != null && IsValidCoors(playerMove))
                {
                    row = SplitAndConvertDigitOfInput(playerMove);
                    col = SplitAndConvertCharOfInput(playerMove);

                    if (!i_GameEngine.GameBoard.IsValidIndex(row, col))
                    {
                        Console.WriteLine("Selected coordinates are out of the game board's range.");
                        continue;
                    }

                    if (i_GameEngine.GameBoard.IsValidSlot(row, col))
                    {
                        i_GameEngine.GameBoard.RevealSlot(row, col);
                        ConsoleRender.DisplayBoard(i_GameEngine.GameBoard);
                        ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);
                        i_GameEngine.SetPreviousCardPosition(row, col); // Store the first card position
                        validFirstInput = true;
                    }
                    else
                    {
                        Console.WriteLine(ConsoleRender.k_InvalidCard);
                    }
                }
                else
                {
                    Console.WriteLine(ConsoleRender.k_InvalidCard);
                }
            }
            return isMoveSucceed;
        }

        internal static void HandleComputerMove(GameEngine i_GameEngine, (int row, int col) move)
        {
            int firstCardRow = move.row;
            int firstCardCol = move.col;

            if (!i_GameEngine.GameBoard.IsValidIndex(firstCardRow, firstCardCol))
            {
                throw new InvalidOperationException("Invalid move by computer.");
            }

            i_GameEngine.GameBoard.RevealSlot(firstCardRow, firstCardCol);
            ConsoleRender.DisplayBoard(i_GameEngine.GameBoard);
            ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);

            i_GameEngine.SetPreviousCardPosition(firstCardRow, firstCardCol); // Store the first card position
        }

        private static int SplitAndConvertCharOfInput(string input)
        {
            char letterOfTheColumn = input[0];
            int colDigitAfterParse = (int)Enum.Parse(typeof(eBoardMapper), letterOfTheColumn.ToString());

            return colDigitAfterParse;
        }

        private static int SplitAndConvertDigitOfInput(string input)
        {
            int letterOfTheRow = int.Parse(input[1].ToString());
            int rowDigitAfterParse = letterOfTheRow - 1; // Convert to zero-based index

            return rowDigitAfterParse;
        }

        internal static bool CheckMatch(GameEngine i_GameEngine, ref Player currentPlayer, string requestMessage)
        {
            bool validSecondInput = false;
            bool isMoveSucceed;
            int secondCardRow = -1, secondCardCol = -1;

            while (!validSecondInput)
            {
                if (currentPlayer.PlayerType == ePlayerType.Computer && i_GameEngine.ComputerPlayer != null)
                {
                    // Computer's second move
                    var move = i_GameEngine.ComputerPlayer.MakeMove(i_GameEngine.GameBoard);
                    secondCardRow = move.row;
                    secondCardCol = move.col;
                    //Console.WriteLine($"{currentPlayer.Name} chooses ({secondCardRow + 1}, {secondCardCol + 1})"); // $C$
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    // Human player's move
                    string? input = GetUserMove(requestMessage);

                    if (input != null && IsValidCoors(input))
                    {
                        secondCardRow = SplitAndConvertDigitOfInput(input);
                        secondCardCol = SplitAndConvertCharOfInput(input);

                        if (!i_GameEngine.GameBoard.IsValidSlot(secondCardRow, secondCardCol))
                        {
                            Console.WriteLine(ConsoleRender.k_InvalidCard);
                            continue;
                        }

                        int prevRow = i_GameEngine.PreviousCardRow;
                        int prevCol = i_GameEngine.PreviousCardCol;

                        if (prevRow == secondCardRow && prevCol == secondCardCol)
                        {
                            Console.WriteLine("You cannot choose the same slot twice. Please choose another slot.");
                            continue;
                        }
                        validSecondInput = true;
                    }
                    else
                    {
                        Console.WriteLine(ConsoleRender.k_InvalidCard);
                    }
                }
                validSecondInput = true;
            }

            i_GameEngine.GameBoard.RevealSlot(secondCardRow, secondCardCol);
            ConsoleRender.DisplayBoard(i_GameEngine.GameBoard);
            ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);

            int prevRow1 = i_GameEngine.PreviousCardRow;
            int prevCol1 = i_GameEngine.PreviousCardCol;

            eCardMarks prevCard = i_GameEngine.GameBoard.CardContent(prevRow1, prevCol1);
            eCardMarks curCard = i_GameEngine.GameBoard.CardContent(secondCardRow, secondCardCol);

            // Check if the two cards match
            if (prevCard == curCard)
            {
                currentPlayer.Score++;
                Console.WriteLine("Match found!");
                System.Threading.Thread.Sleep(2000); // Pause for 2 seconds to allow the player to see the play.
                Console.WriteLine($"{currentPlayer.Name}'s current score: {currentPlayer.Score}"); // $C$
                System.Threading.Thread.Sleep(2000); // Pause for 2 seconds to allow the player to see the play.
                i_GameEngine.ResetPreviousCardPosition(); // Reset the first card position
                isMoveSucceed = true;
            }
            else
            {
                Console.WriteLine("No match found. Cards will be flipped back.");
                System.Threading.Thread.Sleep(2000); // Pause for 2 seconds to allow the player to see the cards
                i_GameEngine.GameBoard.HideSlot(prevRow1, prevCol1);
                i_GameEngine.GameBoard.HideSlot(secondCardRow, secondCardCol);
                Console.Clear(); // Clear the screen
                ConsoleRender.DisplayBoard(i_GameEngine.GameBoard); // Display the board again
                i_GameEngine.ResetPreviousCardPosition(); // Reset the first card position
                isMoveSucceed = false;
            }
            return isMoveSucceed;
        }

        internal static bool InputBoardSizeValidator(string i_SizeOfTheBoard)
        {
            return int.TryParse(i_SizeOfTheBoard, out int result) && result >= 4 && result <= 6;
        }

        private static bool IsValidCoors(string i_PlayerMove)
        {
            bool isValidCoors = false;

            if (i_PlayerMove.Length == 2)
            {
                char letterOfTheRow = i_PlayerMove[0];
                char letterOfTheColumn = i_PlayerMove[1];

                isValidCoors = char.IsLetter(letterOfTheRow) && char.IsDigit(letterOfTheColumn) &&
                       letterOfTheRow >= 'A' && letterOfTheRow <= 'F' && letterOfTheColumn >= '1' && letterOfTheColumn <= '6';
            }
            return isValidCoors;
        }

        private static string? GetUserMove(string i_RequestMessage)
        {
            Console.WriteLine(i_RequestMessage);
            return Console.ReadLine();
        }

        public static void EndGame(GameEngine i_GameEngine)
        {
            Console.WriteLine(ConsoleRender.k_GameOverMessage);
            ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);
            Player winner = i_GameEngine.Players[0].Score > i_GameEngine.Players[1].Score ? i_GameEngine.Players[0] : i_GameEngine.Players[1];

            if (i_GameEngine.Players[0].Score == i_GameEngine.Players[1].Score)
            {
                Console.WriteLine("The game is a tie!");
            }
            else
            {
                ConsoleRender.DisplayWinner(winner);
            }

            Console.WriteLine(ConsoleRender.k_GameOverMessage);
            string? playerInputAgain = Game.CheckPlayAgain();
            bool isPlayAgain = (playerInputAgain == "Y");

            if (isPlayAgain)
            {
                Console.Clear();
                GameEngine gameEngine = Runner.SetUpGame();

                Runner.RunGame(gameEngine);
            }
            else
            {
                Console.WriteLine(ConsoleRender.k_ExitMessage);
                Game.PressEnterToExit();
            }
        }
    }
}
