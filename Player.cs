using System;

namespace Ex02
{
	public struct Player
	{
		private readonly string r_Name;
		private int m_Score;
		private readonly ePlayerType r_PlayerType;

		public Player(string i_Name, int i_Score, ePlayerType i_PlayerType)
		{
			r_Name = i_Name;
			m_Score = i_Score;
			r_PlayerType = i_PlayerType;
		}

		public readonly string Name
		{
            get { return r_Name; }
		}

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public readonly ePlayerType PlayerType
        {
            get { return r_PlayerType; }
        }
    }
}
