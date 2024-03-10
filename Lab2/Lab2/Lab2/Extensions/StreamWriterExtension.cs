using System.IO;

namespace Extensions
{
	public static class StreamWriterExtensions
	{
		private static void WriteLine(
				this System.IO.StreamWriter writer,
				string[] row,
				int extraSpaces)
		{
			int basicSpacerLength = 1 + extraSpaces / (row.Length - 1);
			extraSpaces %= (row.Length - 1);
			int extraSpacesFrequency = (row.Length - 1) / (extraSpaces + 1);
			int extraSpacesLeft = extraSpaces;

			string basicSpacer = new string(' ', basicSpacerLength);
			for (int i = 0; i < row.Length; ++i)
			{
				writer.Write($"{row[i]}{basicSpacer}");
				if (extraSpacesLeft > 0 &&
					extraSpacesFrequency * (extraSpaces - extraSpacesLeft) == i)
				{
					writer.Write(" ");
					--extraSpacesLeft;
				}
			}
			writer.WriteLine();
        }

		public static void Write(
				this System.IO.StreamWriter writer,
				string text,
				int justificationLimit)
		{
			if (justificationLimit <= 0)
			{
				throw new ArgumentException("Justification limit " +
					"must be positive");
			}

			string[] words = text.Split();
			int[] prefixSumOfLenghts = new int[words.Length];
			prefixSumOfLenghts[0] = words[0].Length;
			for (int i = 1; i < words.Length; ++i)
			{
				prefixSumOfLenghts[i] = prefixSumOfLenghts[i - 1]
					+ words[i].Length;
			}

			int nextRowStarterIndex = 0;
			int printedRowsWeight = 0;
			for (int i = 0; i < prefixSumOfLenghts.Length; ++i)
			{
				if (prefixSumOfLenghts[i] + i - printedRowsWeight
							- nextRowStarterIndex > justificationLimit)
				{
					string[] row = new string[i - nextRowStarterIndex];
					Array.Copy(words, nextRowStarterIndex, row, 0, i - nextRowStarterIndex);
					int extraSpaces = justificationLimit -
						prefixSumOfLenghts[i - 1] - i + 1 +
						printedRowsWeight + nextRowStarterIndex;
                    writer.WriteLine(
								row: row,
								extraSpaces: extraSpaces);
					nextRowStarterIndex = i;
					printedRowsWeight = prefixSumOfLenghts[i - 1];
				}
			}
			writer.WriteLine();
		}
	}
}

