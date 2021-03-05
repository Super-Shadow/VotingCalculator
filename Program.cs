using System;
using System.IO;
using System.Collections.Generic;

namespace VotingCalculator
{
	class Program
	{

		static void Main(string[] args)
		{
			int iAvailableSeats = 0;
			int iTotalVotes = 0;
			string constituency = "";
			string path = @"Assessment1Data.txt";
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
			List<Party> Parties = new List<Party>();
			for (int i = 3; i < lines.Length; i++)
			{
				string[] seperatedLine = lines[i].Split(",");
				Party party = new Party(seperatedLine[0], seperatedLine[2].Remove(seperatedLine[2].Length - 1), Int32.Parse(seperatedLine[1]), seperatedLine.Length - 2);
				Parties.Add(party);
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
