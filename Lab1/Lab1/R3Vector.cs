using System;
using static System.Math;

namespace Lab1
{
	public struct R3Vector
	{
		private Tuple<double, double, double> data;
		public double x { get { return data.Item1; } }
        public double y { get { return data.Item2; } }
        public double z { get { return data.Item3; } }
		public double magnitude
		{
			get
			{
				return Pow(x * x + y * y + z * z, 0.5);
			}
		}

        public R3Vector(double x, double y, double z)
		{
			this.data = new Tuple<double, double, double>(x, y, z);
        }

		public static bool operator |(R3Vector a, R3Vector b)
		{
			double error = 0.000000001;
			double i = a.y * b.z - b.y * a.z;
			double j = a.z * b.x - b.z * a.x;
			double k = a.x * b.y - b.x * a.y;

			return Abs(i) + Abs(j) + Abs(k) < 3 * error;
		}
	}
}

