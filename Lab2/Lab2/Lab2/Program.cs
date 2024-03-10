using System;
using static System.Math;
using Extensions;
using Pair = System.Tuple<string, string>;
using System.Collections.Generic;

/*
1) Сформувати список із рядків. Написати функцію, яка перевіряє, чи впорядковані
елементи списку за алфавітом.
2) Створіть додаток, який розділить вихідний список двовимірних масивів на два
списки: список одновимірних масивів із рядків двовимірних масивів, у яких сума
елементів більша за заданий А і список одновимірних масивів із рядків двовимірних
масивів, у яких сума елементів менша за заданий А.
3) Дано 2 стеки, що містять прізвища спортсменів 2-х фехтувальних команд. Провести
жеребкування. У першій команді вибирається кожен n-й гравець (по колу), а в другій -
кожен m-й (по колу). Сформувати пари супротивників і вивести на екран.
*/

internal class Program
{
    public static void Main()
    {
        ConsoleKey keyword = ConsoleKey.Enter;
        while (keyword != ConsoleKey.Escape)
        {
            Console.WriteLine("\nChoose task: \n");
            Console.WriteLine($"{1, 3} ---> Task1");
            Console.WriteLine($"{2, 3} ---> Task1");
            Console.WriteLine($"{3, 3} ---> Task1");
            Console.WriteLine("Esc ---> Exit");
            Console.WriteLine();
            keyword = Console.ReadKey().Key;

            switch (keyword)
            {
                case ConsoleKey.D1:
                    Task1();
                    break;
                case ConsoleKey.D2:
                    Task2();
                    break;
                case ConsoleKey.D3:
                    Task3();
                    break;
                default:
                    break;
            }
        }
    }

    public static void Task1()
    {
        StreamReader reader = new StreamReader("../../../Files/task1_input.txt");
        StreamWriter writer = new StreamWriter("../../../Files/task1_output.txt");
        List<string> words = new List<string>();
        while (!reader.EndOfStream)
        {
            words.AddRange(reader.ReadLine().Split());
        }
        words.Sort();
        writer.Write(text: string.Join(' ', words), justificationLimit: 50);

        reader.Close();
        writer.Close();
        Console.WriteLine($"\n{new string('#', 120)}");
        Console.WriteLine("\nYour task been completed succesfully, see processed files\n");
        Console.WriteLine(new string('#', 120));
    }

    public static void Task2()
    {
        Random rand = new Random();
        Console.WriteLine($"\n{new string('#', 120)}");
        Console.Write("\nInput number of 2-dim arrays: ");
        int twoDimListLength = Convert.ToInt32(Console.ReadLine());
        List<double[,]> twoDimArrayList = new List<double[,]>();
        for (int i = 0; i < twoDimListLength; ++i)
        {
            int m, n;
            m = rand.Next(2, 5);
            n = rand.Next(2, 5);
            double[,] twoDimArray = new double[m,n];
            twoDimArray.FillWithRandom();

            twoDimArrayList.Add(twoDimArray);
        }

        Console.WriteLine(new string('-', 30));
        for (int i = 0; i < twoDimArrayList.Count; ++i)
        {
            double[,] twoDimArray = twoDimArrayList[i];
            Console.WriteLine($"\n{i + 1}) {twoDimArray.GetLength(0)} * " +
                $"{twoDimArray.GetLength(1)} array:\n");
            twoDimArray.Print();
        }
        Console.WriteLine(new string('-', 30));

        Console.Write("\nInput A: ");
        double A = Convert.ToDouble(Console.ReadLine());
        List<double[]> lessThanA = new List<double[]>();
        List<double[]> greaterThanA = new List<double[]>();

        foreach (double[,] twoDimArray in twoDimArrayList)
        {
            for (int i = 0; i < twoDimArray.GetLength(0); ++i)
            {
                double[] oneDimArray = twoDimArray.GetRow(i);
                if (oneDimArray.Sum() < A) { lessThanA.Add(oneDimArray); }
                else { greaterThanA.Add(oneDimArray); }
            }
        }

        Console.WriteLine($"\nRows with sums less than {A}:");
        PrintListOfArrays(lessThanA);

        Console.WriteLine($"\nRows with sums greater than {A}:");
        PrintListOfArrays(greaterThanA);
        Console.WriteLine(new string('#', 120));
    }

    public static void PrintListOfArrays(List<double[]> array)
    {
        if (array.Count == 0)
        {
            Console.WriteLine("None");
            return;
        }
        foreach (double[] twoDimArray in array)
        {
            for (int i = 0; i < twoDimArray.Length; ++i)
            {
                Console.Write($"{twoDimArray[i],3:F1}{new string(' ', 3)}");
            }
            Console.WriteLine();
        }
    }

    public static void Task3()
    {
        Console.WriteLine($"\n{new string('#', 120)}");
        StreamReader reader1 = new StreamReader("../../../Files/task3_inputF.txt");
        StreamReader reader2 = new StreamReader("../../../Files/task3_inputG.txt");

        List<string> teamFrance = new List<string>();
        List<string> teamGerman = new List<string>();
        List<Pair> contestants = new List<Pair>();
        while (!reader1.EndOfStream) { teamFrance.Add(reader1.ReadLine()); }
        while (!reader2.EndOfStream) { teamGerman.Add(reader2.ReadLine()); }
        reader1.Close();
        reader2.Close();

        int n, m;
        Console.Write("\nInput n: ");
        n = Convert.ToInt32(Console.ReadLine());
        Console.Write("\nInput m: ");
        m = Convert.ToInt32(Console.ReadLine());

        teamFrance = teamFrance.Rearrange(n);
        teamGerman = teamGerman.Rearrange(m);

        contestants = Zip(teamFrance, teamGerman);

        Console.WriteLine(new string('-', 120));
        Console.WriteLine("\nPairs of contestants:\n");
        for (int i = 0; i < contestants.Count; ++i)
        {
            Pair pair = contestants[i];
            string formattedString = $"({pair.Item1}, {pair.Item2})";
            Console.Write($"{formattedString,-36}{new string(' ', 5)}");
            if ((i + 1) % 3 == 0) { Console.WriteLine(); }
        }
        Console.WriteLine();
        Console.WriteLine($"{new string('#', 120)}");
    }

    public static List<Pair> Zip(List<string> listA, List<string> listB)
    {
        int minCount = Min(listA.Count, listB.Count);
        List<Pair> mergedList = new List<Pair>();
        for (int i = 0; i < minCount; ++i)
        {
            mergedList.Add(new Pair(listA[i], listB[i]));
        }

        return mergedList;
    }
}