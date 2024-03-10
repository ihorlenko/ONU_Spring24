using System;
using static System.Math;

namespace Lab1
{
	public struct TableOfVectors
	{
		private R3Vector[,] data;
		private int height;
		private int width;
		private readonly Random rand = new Random();
		private int randRange = 10;
		private bool filledWithInt;

		public TableOfVectors() { data = null; height = 0; width = 0; }

		public TableOfVectors(int height, int width, bool fillWithInts=false)
		{
			if (height <= 0) { throw new ArgumentOutOfRangeException(nameof(height), "Height must be of positive size"); }
            if (width <= 0) { throw new ArgumentOutOfRangeException(nameof(width), "Width must be of positive size"); }
            this.height = height;
            this.width = width;
			this.data = new R3Vector[height, width];
			this.filledWithInt = fillWithInts;
            FillWithRandom(fillWithInts);
		}

		private void FillWithRandom(bool fillWithInts=false)
		{
			for (int i = 0; i < this.height; ++i)
			{
				for (int j = 0; j < this.width; ++j)
				{
					double[] temp = new double[3];
					for (int k = 0; k < 3; ++k)
					{
						temp[k] = fillWithInts ?
							rand.Next(-randRange, randRange)
							: Pow(-1, rand.Next(-1, 1)) * rand.NextDouble() * randRange;
					}
					this.data[i, j] = new R3Vector(temp[0], temp[1], temp[2]);
                }
			}
		}

		/// <summary>
		/// Returns array of arrays of vectors
		/// with given magnitude and precision
		/// </summary>
		/// <param name="magnitude">Magnitude of the vectors
		/// to be retrieved</param>
		/// <param name="precision">Precision of the error</param>
		/// <returns></returns>
		public R3Vector[][] RetrieveVectors(double magnitude, double precision=0.1)
		{
			R3Vector[][] retrievedVectors = new R3Vector[this.width][];
			for (int i = 0; i < this.width; ++i)
			{
				List<R3Vector> temp = new List<R3Vector>();
                for (int j = 0; j < this.height; ++j)
				{
					if (Abs(data[j,i].magnitude - magnitude) <= precision)
					{
						temp.Add(data[j,i]);
					}
				}
				retrievedVectors[i] = temp.ToArray();
			}
			return retrievedVectors;
		} 

		public void Print()
		{
			Console.WriteLine("TABLE OF VECTORS");
			Console.WriteLine($"{new string('#', 21 * width + 3 * (width - 1))}");

            for (int i = 0; i < this.height; ++i)
			{
				for (int j = 0; j < this.width; ++j)
				{
					string tuple = this.filledWithInt ? $"({data[i, j].x,0:F0}, {data[i, j].y,1:F0}, {data[i, j].z,1:F0})"
						: $"({data[i, j].x,0:F2}, {data[i, j].y,5:F2}, {data[i, j].z,5:F2})";

                    Console.Write($"{tuple, -21}");
					Console.Write(new string(' ', 3));
				}
				Console.Write(new string('\n', 2));
			}
		}
	}
}

