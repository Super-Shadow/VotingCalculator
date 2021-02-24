using System;
using System.Collections.Generic;
using System.Text;

namespace VotingCalculator
{
	class Party
	{
		public string m_name;
		public int m_iVotes;
		public int m_iAppliedSeats;

		private string m_shortName;
		
		public Party(string name, string shortName, int votes, int appliedSeats)
		{
			m_name = name;
			m_shortName = shortName;
			m_iVotes = votes;
			m_iAppliedSeats = appliedSeats;
		}
	}
}
