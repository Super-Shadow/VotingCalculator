using System;
using System.Collections.Generic;
using System.Text;

namespace VotingCalculator
{
	class Party
	{
		public readonly string m_name;
		public readonly string m_shortName;
		public int m_iSeatsClaimed;
		public int m_iVotes;

		private readonly int m_iAppliedSeats;

		public Party(string name, string shortName, int votes, int appliedSeats)
		{
			m_name = name;
			m_shortName = shortName;
			m_iVotes = votes;
			m_iAppliedSeats = appliedSeats;
		}

		//------------------------------
		// Method Name: claimSeat
		// Arguments: int iRound
		// Returns: void
		// Desc: Increases the seats this party has claimed. Also has specific functionality
		//		 if it is the first round of a dhon't system.
		//------------------------------
		public void claimSeat(int iRound)
		{
			if (iRound < 1)
				throw new ArgumentException("Tried to parse 0 or negative number as round number for claimSeat method in Party class.");

			m_iSeatsClaimed++;
			if (iRound != 1) // First round there is no division
				m_iVotes /= (1 + m_iSeatsClaimed);
		}

#if DEBUG
		public override string ToString()
		{
			return String.Format("Party Name: {0}; Short Name: {1}; Votes: {2}; Applied Seats: {3}; Claimed Seats: {4};", m_name, m_shortName, m_iVotes, m_iAppliedSeats, m_iSeatsClaimed);
		}
#endif
	}
}
