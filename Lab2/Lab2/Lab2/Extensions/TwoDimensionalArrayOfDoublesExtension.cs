using System;

namespace Extensions
{
	public static class TwoDimensionalArrayOfDoublesExtension
	{
		public static void FillWithRandom(
			   this double[,] array,
			   double minValue = 0,
			   double maxValue = 10)
		{
			Random rand = new Random();
			for (int i = 0; i < array.GetLength(0); ++i)
			{
				for (int j = 0; j < array.GetLength(1); ++j)
				{
					double minRand = rand.NextDouble() * minValue;
					double maxRand = rand.NextDouble() * maxValue;
                    array[i, j] = maxRand + minRand;
				}
			}
		}

		public static void Print(this double[,] array)
		{
			for (int i = 0; i < array.GetLength(0); ++i)
			{
				for (int j = 0; j < array.GetLength(1); ++j)
				{
					Console.Write($"{array[i,j], 3:F1}{new string(' ', 3)}");
				}
				Console.WriteLine();
			}
		}

		public static double[] GetRow(this double[,] array, int rowIndex)
		{
			if (rowIndex > array.GetLength(0) - 1)
			{
				throw new ArgumentOutOfRangeException("Row index is out of range");
			}

			double[] row = new double[array.GetLength(1)];
			for (int i = 0; i < array.GetLength(1); ++i)
			{
				row[i] = array[rowIndex, i];
			}

			return row;
		}
	}
}

