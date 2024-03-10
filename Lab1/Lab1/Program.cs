using System;
using Lab1;

internal class Program
{
    public static void Main()
    {
        UserPromt prompt = UserPromt.yes;

        while (prompt != UserPromt.no)
        {
            Console.Write("Input p: ");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.Write("Input q: ");
            int q = Convert.ToInt32(Console.ReadLine());

            TableOfVectors A = new TableOfVectors(height: p, width: q, fillWithInts: true);

            A.Print();

            Console.Write("Input M: ");
            int M = Convert.ToInt32(Console.ReadLine());

            R3Vector[][] B = A.RetrieveVectors(magnitude: M);
            PrintArray(B);

            double x, y, z;
            Console.Write("Input 3-dimensional vector in format x, y, z: ");
            string[] input = Console.ReadLine().Split();
            x = Convert.ToDouble(input[0]);
            y = Convert.ToDouble(input[1]);
            z = Convert.ToDouble(input[2]);
            R3Vector userVector = new R3Vector(x, y, z);

            Console.WriteLine($"{CountCollinearVectors(B, userVector)} vectors from B were found " +
                $"collinear with user vector ({userVector.x}, {userVector.y}, {userVector.z})");

            prompt = AskForPrompt();
        }

        //ProofOfConcept();

    }

    public static void PrintArray(R3Vector[][] array)
    {
        Console.WriteLine("TABLE OF RETRIEVED VECTORS");
        Console.WriteLine($"{new string('#', 26)}");
        for (int i = 0; i < array.Length; ++i)
        {
            Console.Write($"{i + 1, 2}: ");
            for (int j = 0; j < array[i].Length; ++j)
            {
                Console.Write($"({array[i][j].x,0:F2}, {array[i][j].y,4:F2}, {array[i][j].z,4:F2})");
                Console.Write(new string(' ', 3));
            }
            if (array[i].Length == 0) { Console.Write("No vectors"); }
            Console.Write(new string('\n', 2));
        }
    }

    public static int CountCollinearVectors(R3Vector[][] table, R3Vector userVector)
    {
        int counter = 0;

        foreach (R3Vector[] array in table)
        {
            foreach (R3Vector vector in array)
            {
                if (vector | userVector) { counter += 1; }
            }
        }

        return counter;
    }

    private enum UserPromt
    {
        yes,
        no
    }

    private static UserPromt AskForPrompt()
    {
        while (true)
        {
            Console.WriteLine("\nShall we proceed? y - yes; n - no");
            string prompt = Console.ReadLine();
            switch (prompt)
            {
                case "y":
                    return UserPromt.yes;
                case "n":
                    return UserPromt.no;
                default:
                    break;
            }
        }
    }

    public static void ProofOfConcept()
    {
        StreamReader reader = new StreamReader("Test1.txt");
        int N = Convert.ToInt32(reader.ReadLine());
        R3Vector[][] test = new R3Vector[N / 2][];

        for (int i = 0; i < N; i+= 2)
        {
            List<R3Vector> temp = new List<R3Vector>();
            for (int j = 0; j < 2; j++)
            {
                string[] v = reader.ReadLine().Split(' ');
                temp.Add(new R3Vector(Convert.ToDouble(v[0]),
                                      Convert.ToDouble(v[1]),
                                      Convert.ToDouble(v[2])));
            }
            test[i / 2] = temp.ToArray();
        }

        for (int i = 0; i < test.Length; ++i)
        {
            for (int j = 0; j < test[i].Length; ++j)
            {
                Console.Write($"({test[i][j].x, 0:F1}, {test[i][j].y,4:F1}, {test[i][j].z,4:F1})");
                Console.Write(new string(' ', 4));
            }
            Console.Write(new string('\n', 2));
        }

        R3Vector testVector = new R3Vector(1, 2, 1);
        int counter = CountCollinearVectors(table: test, userVector: testVector);
        Console.WriteLine(counter);


        reader.Close();
    }
}