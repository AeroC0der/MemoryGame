using System;

namespace Ex02
{
	public struct Card
	{
		private readonly eCardMarks r_CardMarks;
		private bool m_IsOpen;

		public Card(eCardMarks i_cardMarks, bool i_isOpen)
		{
			r_CardMarks = i_cardMarks;
			m_IsOpen = i_isOpen;
		}

		public eCardMarks CardMarks
		{
			get { return r_CardMarks; }
		}

		public bool IsOpen
		{
			get { return m_IsOpen;  }
		}

		public void FlipCard()
		{
			m_IsOpen = !m_IsOpen;
		}
	}
}
