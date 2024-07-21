using System;

namespace Ex02
{
    public class ComputerPlayer
    {
        private Player m_Player;
        private Random m_Random;

        public ComputerPlayer(ePlayerType playerType)
        {
            m_Player = new Player("Computer", 0, playerType);
            m_Random = new Random();
        }

        public Player PlayerStruct
        {
            get { return m_Player; }
        }

        //public void IncrementScore()
        //{
        //    m_Player.Score++;
        //}

        public (int row, int col) MakeMove(GameBoard gameBoard)
        {
            int row, col;
            do
            {
                row = m_Random.Next(gameBoard.NumberOfRowsOnBoard);
                col = m_Random.Next(gameBoard.NumberOfColumnsOnBoard);
            } while (!gameBoard.IsValidSlot(row, col));

            return (row, col);
        }
    }
}
