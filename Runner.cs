using System;
using System.Reflection;

namespace Ex02
{
    public class Runner
    {
        public static GameEngine SetUpGame()
        {
            Console.WriteLine(ConsoleRender.k_WelcomeMassege);
            System.Threading.Thread.Sleep(1000);

            string? player1Name; // Declare player1Name

            player1Name = MemoryGameUtils.GetPlayerName(ConsoleRender.k_PlayerOneName);

            ePlayerType secondPlayerType = MemoryGameUtils.TypeOfSecondPlayer();
            string? player2Name = "Computer";

            if (secondPlayerType == ePlayerType.Person) // If second player is a person
            {
                player2Name = MemoryGameUtils.GetPlayerName(ConsoleRender.k_PlayerTwoName);
            }

            int rows = 0, columns = 0;
            bool validRows = false, validColumns = false;

            while (true) // Start of board size input loop
            {
                while (!validRows)
                {
                    Console.WriteLine(ConsoleRender.k_NumberOfRows);
                    string? rowsInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(rowsInput) && MemoryGameUtils.InputBoardSizeValidator(rowsInput))
                    {
                        rows = int.Parse(rowsInput);
                        validRows = true;
                    }
                    else
                    {
                        Console.WriteLine(ConsoleRender.k_NumberNotInTheRange);
                    }
                }

                while (!validColumns)
                {
                    Console.WriteLine(ConsoleRender.k_NumberOfColumns);
                    string? columnsInput = Console.ReadLine();
                    if (!string.IsNullOrEmpty(columnsInput) && MemoryGameUtils.InputBoardSizeValidator(columnsInput))
                    {
                        columns = int.Parse(columnsInput);
                        validColumns = true;
                    }
                    else
                    {
                        Console.WriteLine(ConsoleRender.k_NumberNotInTheRange);
                    }
                }

                if (GameBoard.IsValidBoardSize(rows, columns))
                {
                    break;
                }
                else
                {
                    Console.WriteLine(ConsoleRender.k_InvalidTotalNumberOfCells);
                    // Reset the validation flags if the total board size is not valid
                    validRows = false;
                    validColumns = false;
                }
            }
            // End of board size input loop

            GameEngine gameEngine = new GameEngine(rows, columns, secondPlayerType);
            gameEngine.Players[0] = new Player(player1Name, 0, ePlayerType.Person);
            gameEngine.Players[1] = new Player(player2Name, 0, secondPlayerType);

            return gameEngine;
        }

        public static void RunGame(GameEngine i_GameEngine)
        {
            ComputerPlayer? computerPlayer = null;
            if (i_GameEngine.Players[1].PlayerType == ePlayerType.Computer)
            {
                computerPlayer = new ComputerPlayer(ePlayerType.Computer);
            }

            while (!i_GameEngine.IsGameOver())
            {
                ConsoleRender.DisplayBoard(i_GameEngine.GameBoard);
                ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);

                int playerIndex = i_GameEngine.PlayerTurnIdx ? 1 : 0;
                Player currentPlayer = i_GameEngine.Players[playerIndex];
                string requestMessage = $"{currentPlayer.Name}, " + ConsoleRender.k_PlayerTurn;

                if (currentPlayer.PlayerType == ePlayerType.Computer && computerPlayer != null)
                {
                    var move = computerPlayer.MakeMove(i_GameEngine.GameBoard);
                    MemoryGameUtils.HandleComputerMove(i_GameEngine, move);
                }
                else
                {
                    if (!MemoryGameUtils.HandlePlayerMove(i_GameEngine, requestMessage))
                    {
                        return;
                    }
                }

                ConsoleRender.DisplayBoard(i_GameEngine.GameBoard);
                ConsoleRender.DisplayPlayersScore(i_GameEngine.Players);
                i_GameEngine.Players[playerIndex] = currentPlayer;

                if (!i_GameEngine.IsGameOver() && !MemoryGameUtils.CheckMatch(i_GameEngine, ref currentPlayer, requestMessage))
                {
                    i_GameEngine.PlayerTurnIdx = !i_GameEngine.PlayerTurnIdx; // Switch turn
                }

                i_GameEngine.Players[playerIndex] = currentPlayer;
            }
            MemoryGameUtils.EndGame(i_GameEngine);
        }
    }
}
