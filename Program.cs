using System;
using System.IO;
using System.Collections.Generic;

namespace VotingCalculator
{
	class Program
	{

		static void Main(string[] args)
		{
			// Preset variable values, set the path of the party data.
			int iAvailableSeats = 0;
			int iTotalVotes = 0;
			string constituency = "";
			string path = @"Assessment1Data.txt";
			string store = @"Assessment1TestResults.txt";
			string[] lines;
			try
			{
				lines = File.ReadAllLines(path);
			}
			catch
			{
				throw new FileNotFoundException("Please place Assessment1Data.txt in the same folder as the program.");
			}

			constituency = lines[0];
			iAvailableSeats = Int32.Parse(lines[1]);
			iTotalVotes = Int32.Parse(lines[2]);
			List<Party> Parties = new List<Party>();        // Create a new List of Party objects.
			for (int i = 3; i < lines.Length; i++)          // Going through the string of text previously imported, Create a new Party object, and add it to the List
			{
				string[] seperatedLine = lines[i].Split(",");
				Party party = new Party(seperatedLine[0], seperatedLine[2].Remove(seperatedLine[2].Length - 1), Int32.Parse(seperatedLine[1]), seperatedLine.Length - 2);
				Parties.Add(party);
			}

			while (iAvailableSeats > 0)
			{
				Party winningParty = null;
				int iHighestVotes = 0;

				foreach (Party party in Parties)
				{
					if (party.m_iVotes > iHighestVotes)
					{
						iHighestVotes = party.m_iVotes;
						winningParty = party;
					}
				}
				winningParty.m_iSeatsClaimed++;
				if (iAvailableSeats != 5) // Round 1 there is no division
					winningParty.m_iVotes /= (1 + winningParty.m_iSeatsClaimed);

				iAvailableSeats--;
			}

			List<string> linesToWrite = new List<string>();
			linesToWrite.Add("#East Midlands (European Parliment Constituency)");
			string partyLine;
			foreach(Party p in Parties)
			{
				if (p.m_iSeatsClaimed >= 1)
				{
					partyLine = p.m_name;
					for (int i = 1; i <= p.m_iSeatsClaimed; i++)
					{
						partyLine = partyLine + "," + p.m_shortName + i;
					}
					partyLine += ";";
					linesToWrite.Add(partyLine);
				}
			}

			try
			{
				File.WriteAllLines(store, linesToWrite);
			}
			catch
			{
				throw new FileNotFoundException("No file was found to write to, could not output results.");
			}
			

#if DEBUG
			Console.WriteLine(constituency);
			Console.WriteLine("Total votes: {0}", iTotalVotes);
			Console.WriteLine("Available seats: {0}", iAvailableSeats);

			foreach (Party party1 in Parties)
			{
				Console.WriteLine(party1);
			}
#endif
		}
	}
}
