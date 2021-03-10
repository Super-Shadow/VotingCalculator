using System;
using System.IO;
using System.Collections.Generic;

namespace VotingCalculator
{
	class Program
	{

		static void Main(string[] args)
		{
			// Preset variable values, set the path of the party data, as well as file to write to.
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
				throw new FileNotFoundException("Please place Assessment1Data.txt in the same directory as the executable.");
			}

			constituency = lines[0];
			iAvailableSeats = Int32.Parse(lines[1]);
			iTotalVotes = Int32.Parse(lines[2]);

			List<Party> winningParties = applyDhondtToParties(parsePartiesFromText(lines), iAvailableSeats); // Create a new List of Party objects from string array and applies Dhond't to the new List.
			outputPartyList(winningParties, store, constituency); // Output the parties that claimed a seat.

#if DEBUG
			Console.WriteLine(constituency);
			Console.WriteLine("Total votes: {0}", iTotalVotes);
			Console.WriteLine("Available seats: {0}", iAvailableSeats);

			foreach (Party party1 in winningParties)
			{
				Console.WriteLine(party1);
			}
#endif
		}

		//------------------------------
		// Method Name: parsePartiesFromText
		// Arguments: string[] inputText
		// Returns: List<Party>
		// Desc: Takes in an array of strings, where each element contains Party
		//		 information in the formatted specified by the example input data.
		//		 Returns a List containing each party as Party object.
		//------------------------------
		private static List<Party> parsePartiesFromText(string[] inputText)
		{
			List<Party> Parties = new List<Party>();
			for (int i = 3; i < inputText.Length; i++)          // Going through the string of text previously imported, Create a new Party object, and add it to the List
			{
				string[] seperatedLine = inputText[i].Split(",");
				// Removes semi colon at end of each line. This is necessary as parties that apply for 1 seat have an incorrectly formatted shot name.
				seperatedLine[seperatedLine.Length - 1] = seperatedLine[seperatedLine.Length - 1].Remove(seperatedLine[seperatedLine.Length - 1].Length - 1); 
				Party party = new Party(seperatedLine[0], seperatedLine[2].Remove(seperatedLine[2].Length - 1), Int32.Parse(seperatedLine[1]), seperatedLine.Length - 2);
				Parties.Add(party);
			}
			return Parties;
		}

		//------------------------------
		// Method Name: applyDhondtToParties
		// Arguments: List<Party> Parties, int iMaxClaimableSeats
		// Returns: List<Party>
		// Desc: Takes in a List of Party objects and integer representing the max
		//		 seats that are available to be claimed. The Dhond't system is then
		//		 applied to list, which specifies which parties can claim a seat.
		//		 Returns a List containing each party that claimed a seat.
		//------------------------------
		private static List<Party> applyDhondtToParties(List<Party> Parties, int iMaxClaimableSeats)
		{
			for (int i = 0; i < iMaxClaimableSeats; i++)
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

				winningParty.claimSeat(i+1);
			}

			return Parties;
		}

		//------------------------------
		// Method Name: outputPartyList
		// Arguments: List<Party> Parties, string outputName, string constituency
		// Returns: void
		// Desc: Takes in a List of Party objects and prints a formatted version
		//		 to a file specified in the method arguements. It also allows for
		//		 a specific constituency to be specified when generating the file.
		//------------------------------
		private static void outputPartyList(List<Party> Parties, string outputName, string constituency = "")
		{
			List<string> linesToWrite = new List<string>(); // Create an empty list which will be used to store the lines written to storage.
			linesToWrite.Add(constituency); // Add constituency found in input file to first line
			string partyLine;                                                     // empty string used in creation of each line
			foreach (Party p in Parties) // For each party..
			{
				if (p.m_iSeatsClaimed >= 1)  // If the party has more then 1 seat..
				{
					// Format as such:  Party Name, Seat1, Seat2... 
					partyLine = p.m_name;
					for (int i = 1; i <= p.m_iSeatsClaimed; i++)  // For each seat the party has..
					{
						partyLine = partyLine + "," + p.m_shortName + i;
					}
					partyLine += ";";
					linesToWrite.Add(partyLine);  // Add the line to the list of lines.
				}
			}

			// Write data to file.
			File.WriteAllLines(outputName, linesToWrite);

		}
	}
}
