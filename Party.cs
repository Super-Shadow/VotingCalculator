using System;
using System.Collections.Generic;
using System.Text;

namespace VotingCalculator
{
	class Party
	{
		public readonly string m_name;
		public readonly int m_iVotes;
		public readonly int m_iAppliedSeats;

		private readonly string m_shortName;
		
		public Party(string name, string shortName, int votes, int appliedSeats)
		{
			m_name = name;
			m_shortName = shortName;
			m_iVotes = votes;
			m_iAppliedSeats = appliedSeats;
		}
#if DEBUG
		public override string ToString()
		{
			return String.Format("Party Name: {0}; Short Name: {1}; Votes: {2}; Applied Seats: {3};", m_name, m_shortName, m_iVotes, m_iAppliedSeats);
		}
#endif
	}
}
