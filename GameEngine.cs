using System;

namespace Ex02
{
    public class GameEngine
    {
        private readonly GameBoard r_GameBoard;
        private readonly Player[] r_Players;
        private readonly Player r_WinnerPlayer;
        private readonly ComputerPlayer r_ComputerPlayer;
        private bool m_PlayerTurnIdx;
        private bool m_IsTie;
        private int m_PreviousCardRow;
        private int m_PreviousCardCol;

        public GameEngine(int i_NumOfRows, int i_NumOfColumns, ePlayerType playerType)
        {
            r_GameBoard = new GameBoard(i_NumOfRows, i_NumOfColumns);
            r_Players = new Player[2];
            r_Players[0] = new Player("Player 1", 0, ePlayerType.Person);
            if (playerType == ePlayerType.Computer)
            {
                r_Players[1] = new Player("Computer", 0, ePlayerType.Computer);
                r_ComputerPlayer = new ComputerPlayer(r_Players[1].PlayerType);
            }
            else
            {
                r_Players[1] = new Player("Player 2", 0, ePlayerType.Person);
            }
            m_PlayerTurnIdx = false;
            m_IsTie = false;
            m_PreviousCardRow = -1;
            m_PreviousCardCol = -1;
        }

        public ComputerPlayer ComputerPlayer
        {
            get { return r_ComputerPlayer; }
        }

        public Player[] Players
        {
            get { return r_Players; }
        }

        public Player WinnerPlayer
        {
            get { return r_WinnerPlayer; }
        }

        public bool PlayerTurnIdx
        {
            get { return m_PlayerTurnIdx; }
            set { m_PlayerTurnIdx = value; }
        }

        public bool IsTie
        {
            get { return m_IsTie; }
            set { m_IsTie = value; }
        }

        public GameBoard GameBoard
        {
            get { return r_GameBoard; }
        }

        public bool IsGameOver()
        {
            return r_GameBoard.IsBoardFull();
        }

        public void SetPreviousCardPosition(int i_Row, int i_Col)
        {
            if (IsValidIndex(i_Row, i_Col))
            {
                m_PreviousCardRow = i_Row;
                m_PreviousCardCol = i_Col;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid card position set.");
            }
        }

        private bool IsValidIndex(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_GameBoard.NumberOfRowsOnBoard && i_Col >= 0 && i_Col < r_GameBoard.NumberOfColumnsOnBoard;
        }

        public int PreviousCardRow
        {
            get { return m_PreviousCardRow; }
        }

        public int PreviousCardCol
        {
            get { return m_PreviousCardCol; }
        }

        public void ResetPreviousCardPosition()
        {
            m_PreviousCardRow = -1;
            m_PreviousCardCol = -1;
        }
    }
}
