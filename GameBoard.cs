using System;

namespace Ex02
{
    public class GameBoard
    {
        private readonly int r_NumberOfRowsOnBoard;
        private readonly int r_NumberOfColumnsOnBoard;
        private readonly Card[,] r_GameBoardToPlay;
        private int m_NumberOfNotOpenCards;

        public GameBoard(int i_numberOfRowsOnBoard, int i_numberOfColumnsOnBoard)
        {
            if (IsValidBoardSize(i_numberOfRowsOnBoard, i_numberOfColumnsOnBoard))
            {
                r_NumberOfRowsOnBoard = i_numberOfRowsOnBoard;
                r_NumberOfColumnsOnBoard = i_numberOfColumnsOnBoard;
                r_GameBoardToPlay = new Card[r_NumberOfRowsOnBoard, r_NumberOfColumnsOnBoard];
                m_NumberOfNotOpenCards = r_NumberOfRowsOnBoard * r_NumberOfColumnsOnBoard;
                InitializeBoard();
            }
            else
            {
                throw new ArgumentException("Board size must result in an even number of cells.");
            }
        }

        public int NumberOfRowsOnBoard
        {
            get { return r_NumberOfRowsOnBoard; }
        }

        public int NumberOfColumnsOnBoard
        {
            get { return r_NumberOfColumnsOnBoard; }
        }

        public Card[,] GameBoardToPlay
        {
            get { return r_GameBoardToPlay; }
        }

        public int NumberOfOpenCards
        {
            get { return m_NumberOfNotOpenCards; }
        }
        public static bool IsValidBoardSize(int i_Rows, int i_Cols)
        {
            return (i_Rows * i_Cols) % 2 == 0 && i_Rows >= 3 && i_Rows <= 6 && i_Cols >= 3 && i_Cols <= 6;
        }

        private void InitializeBoard()
        {
            // Generate pairs of cards
            List<eCardMarks> cardMarks = new List<eCardMarks>();

            foreach (eCardMarks mark in Enum.GetValues(typeof(eCardMarks)))
            {
                cardMarks.Add(mark);
                cardMarks.Add(mark); // Add each mark twice
            }

            // Calculate the total number of cards needed
            int totalCards = r_NumberOfRowsOnBoard * r_NumberOfColumnsOnBoard;

            // Ensure we have exactly the number of cards needed for the board
            while (cardMarks.Count < totalCards)
            {
                foreach (eCardMarks mark in Enum.GetValues(typeof(eCardMarks)))
                {
                    cardMarks.Add(mark);
                    cardMarks.Add(mark); // Add each mark twice until we reach the required number of cards
                    if (cardMarks.Count >= totalCards)
                    {
                        break;
                    }
                }
            }

            // Trim the list if there are too many cards
            if (cardMarks.Count > totalCards)
            {
                cardMarks = cardMarks.GetRange(0, totalCards);
            }
            // Shuffle the cards
            cardMarks = Shuffle(cardMarks);

            for (int i = 0; i < r_NumberOfRowsOnBoard; i++)
            {
                for (int j = 0; j < r_NumberOfColumnsOnBoard; j++)
                {
                    r_GameBoardToPlay[i, j] = new Card(cardMarks[i * r_NumberOfColumnsOnBoard + j], false);
                }
            }
        }

        private static List<eCardMarks> Shuffle(List<eCardMarks> list)
        {
            int listIdx = list.Count;
            Random m_Random = new Random();

            while (listIdx > 1)
            {
                listIdx--;

                int randomIdx = m_Random.Next(listIdx + 1);
                eCardMarks value = list[randomIdx];
                list[randomIdx] = list[listIdx];
                list[listIdx] = value;
            }
            return list;
        }

        public eCardMarks CardContent(int i_RowIndex, int i_ColumnIndex)
        {
            if (i_RowIndex < 0 || i_RowIndex >= r_NumberOfRowsOnBoard || i_ColumnIndex < 0 || i_ColumnIndex >= r_NumberOfColumnsOnBoard)
            {
                throw new IndexOutOfRangeException($"Attempted access out of bounds: Row {i_RowIndex}, Column {i_ColumnIndex}");
            }
            return r_GameBoardToPlay[i_RowIndex, i_ColumnIndex].CardMarks;
        }

        public bool IsBoardFull()
        {
            return NumberOfOpenCards == 0;
        }

        public void UpdateBoard()
        {
            m_NumberOfNotOpenCards--;
        }

        public bool IsValidSlot(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_NumberOfRowsOnBoard && i_Col >= 0 && i_Col < r_NumberOfColumnsOnBoard && !r_GameBoardToPlay[i_Row, i_Col].IsOpen;
        }

        public void RevealSlot(int i_Row, int i_Col)
        {
            if (!IsValidSlot(i_Row, i_Col)) // Ensure this method checks range first.
            {
                throw new IndexOutOfRangeException("Trying to reveal a slot outside of the board's bounds.");
            }
            if (IsValidSlot(i_Row, i_Col))
            {
                r_GameBoardToPlay[i_Row, i_Col].FlipCard();
                m_NumberOfNotOpenCards--;
            }
            else
            {
                throw new ArgumentException("Invalid slot or slot already revealed.");
            }
        }

        public void HideSlot(int i_Row, int i_Col)
        {
            if (r_GameBoardToPlay[i_Row, i_Col].IsOpen)
            {
                r_GameBoardToPlay[i_Row, i_Col].FlipCard();
                m_NumberOfNotOpenCards++;
            }
        }

        public bool IsValidIndex(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_NumberOfRowsOnBoard && i_Col >= 0 && i_Col < r_NumberOfColumnsOnBoard;
        }

    }
}
