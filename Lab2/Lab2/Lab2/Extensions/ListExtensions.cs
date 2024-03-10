using System;

namespace Extensions
{
	public static class ListExtensions
	{
		public static List<string> Rearrange(
                      this List<string> list,
                      int freq)
		{

            if (freq == 0) { throw new ArgumentException("Frequency must be non-zero"); }
            if (freq == 1) { return list; }
            if (freq < 0)
            {
                list.Reverse();
                freq *= -1;
            }

            int curPos = 0;
            int mod = list.Count();
            List<string> rearrangedList = new List<string>();

            while (list.Count != 0)
            {
                curPos += freq - 1;
                rearrangedList.Add(list[curPos % mod]);
                list.RemoveAt(curPos % mod);
                mod = list.Count();
            }

            return rearrangedList;
        }
    }
}

