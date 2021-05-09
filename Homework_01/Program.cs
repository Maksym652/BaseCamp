using System;
using System.Collections.Generic;

namespace BaseCamp_Homework
{
    class Program
    {
        //ЗАВДАННЯ 1
        //По результатам семестра в 1-А классе 10 отличником, 14 хорошистов, 4 троечника.В 1-Б - 8 отличников, 12 хорошистов, 5 троечников. 1-В - 12 отличников, 7 хорошистов, 8 троечников.
        //Найти: 
        //-Сколько отличников, хорошистов и троечников на всей параллели классов
        //-соотношение отличников, хорошистов и троечников в классах
        //-соотношение отличников, хорошистов и троечников на парллели
        int[] FindSumOfEachCategory(params int[][] classesStatistics)
        {
            int[] result = new int[3];
            foreach (var c in classesStatistics)
            {
                result[0] += c[0];
                result[1] += c[1];
                result[2] += c[2];
            }
            return result;
        }
        float[] FindPercentage(int[] statistics)
        {
            float sum = statistics[0] + statistics[1] + statistics[2];
            float[] persentage = new float[3];
            persentage[0] = (statistics[0] / sum) * 100;
            persentage[1] = (statistics[1] / sum) * 100;
            persentage[2] = (statistics[2] / sum) * 100;
            return persentage;
        }
        //ЗАВДАННЯ 2 Дано число, проверить какого оно типа: нечётное, чётное
        bool IsEven(int number) => number % 2 == 0;
        //ЗАВДАННЯ 3 Дано 3 числа, найти число которое находится между. A < B < C
        int MiddleNumber(int a, int b, int c)
        {
            if (a <= b && b <= c || c <= b && b <= a) return b;
            if (b <= a && a <= c || c <= a && a <= b) return a;
            return c;
        }
        //ЗАВДАННЯ 4 Дан произвольный массив чисел, найти уникальные числа в нём.Использовать только циклы, условные операторы.
        int[] FindUniqueNumbers(int[] numbers)
        {
            List<int> result = new List<int>(numbers.Length);
            bool isUnique;
            for (int i = 0; i < numbers.Length; i++)
            {
                isUnique = true;
                for (int j = 0; j < numbers.Length; j++)
                {
                    if (j != i && numbers[i] == numbers[j]) { isUnique = false; break; }
                }
                if (isUnique) result.Add(numbers[i]);
            }
            return result.ToArray();
        }
        //ЗАВДАННЯ 5 Написать метод который сможет транспонировать матрицу
        int[,] TransposeMatrix(int[,] matrix)
        {
            int[,] result = new int[matrix.GetLength(1), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    result[i, j] = matrix[j, i];
                }
            }
            return result;
        }
        //ЗАВДАННЯ 6 Написать метод который будет округлять число до N символов
        float RoundNumber(float number, int decimals)
        {
            if (number % (Math.Pow(0.1, decimals)) < Math.Pow(0.1, decimals) * 0.5)
                return (float)(number - number % Math.Pow(0.1, decimals));
            else
                return (float)(number - number % (Math.Pow(0.1, decimals)) + Math.Pow(0.1, decimals));
        }
        //ЗАВДАННЯ 7
        //Найти y():
        //Y = 100 * tg(x) * √x / x
        //Y = 2 * sin(x) - 4
        double Function1(double x) => (x == 0 || x == Math.PI / 2) ? 0 : 100 * Math.Tan(x) * Math.Sqrt(x) / x;
        double Function2(double x) => 2 * Math.Sin(x) - 4;
        //ЗАВДАННЯ 8 Дано произвольную строку, найти строку между указаным символом.Выводить только первое совпадение.
        string FindSubstringBetweenSymbols(string str, char symbol)
        {
            int begin = 0, end = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == symbol)
                {
                    begin = i + 1;
                    break;
                }
            }
            for (int i = begin; i < str.Length; i++)
            {
                if (str[i] == symbol)
                {
                    end = i;
                    break;
                }
            }
            if (begin >= end) return string.Empty;
            return str.Substring(begin, end - begin);
        }
        //ЗАВДАННЯ 9 Найти слово в произвольной строке и вывести индексы границ этого слова в строке.
        int[] FindBoundsOfWord(string str, string word)
        {
            int begin = 0, end = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == word[0])
                {
                    begin = i;
                    end = i;
                    for (int j = 0; j < word.Length; j++)
                    {
                        if (str[begin + j] != word[j]) break;
                        end++;
                    }
                    if (end - begin == word.Length) break;
                    i = end;
                }
            }
            if (end - begin != word.Length) return new int[] { 0, 0 };
            return new int[] { begin, end };
        }
        //ДОДАТКОВЕ ЗАВДАННЯ Дан произвольный массив чисел, найти числа которые повторяются более 2-х раз. Использовать только циклы, условные операторы.
        int[] NumbersFoundMoreThanTwoTimes(int[] numbers)
        {
            List<int> result = new List<int>(numbers.Length);
            int times;
            for (int i = 0; i < numbers.Length; i++)
            {
                if (result.Contains(numbers[i]))
                    continue;
                times = 1;
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] == numbers[i])
                        times++;
                }
                if (times > 2)
                    result.Add(numbers[i]);
            }
            return result.ToArray();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Program p = new Program();
            int[] paralel_statistics = p.FindSumOfEachCategory(new int[] { 10, 14, 4 }, new int[] { 8, 12, 5 }, new int[] { 12, 7, 8 });
            float[] classA_persentage = p.FindPercentage(new int[] { 10, 14, 4 });
            float[] classB_persentage = p.FindPercentage(new int[] { 8, 12, 5 });
            float[] classC_persentage = p.FindPercentage(new int[] { 12, 7, 8 });
            float[] paralel_persentage = p.FindPercentage(paralel_statistics);
            Console.WriteLine("Завдання №1:");
            Console.WriteLine($"Всього на паралелі:\n\t{paralel_statistics[0]} відмінників,\n\t{paralel_statistics[1]} хорошистів,\n\t{paralel_statistics[2]} трієчників.");
            Console.WriteLine($"В 1-А класі:\n\t{classA_persentage[0]}% відмінників,\n\t{classA_persentage[1]}% хорошистів,\n\t{classA_persentage[2]}% трієчників.");
            Console.WriteLine($"В 1-Б класі:\n\t{classB_persentage[0]}% відмінників,\n\t{classB_persentage[1]}% хорошистів,\n\t{classB_persentage[2]}% трієчників.");
            Console.WriteLine($"В 1-В класі:\n\t{classC_persentage[0]}% відмінників,\n\t{classC_persentage[1]}% хорошистів,\n\t{classC_persentage[2]}% трієчників.");
            Console.WriteLine($"На паралелі вцілому:\n\t{paralel_persentage[0]}% відмінників,\n\t{paralel_persentage[1]}% хорошистів,\n\t{paralel_persentage[2]}% трієчників.");
            Console.WriteLine("\nЗавдання №2:");
            if (p.IsEven(25))
                Console.WriteLine("25 - парне число");
            else
                Console.WriteLine("25 - непарне число");
            Console.WriteLine("\nЗавдання №3:");
            Console.WriteLine("1,2,3 --> " + p.MiddleNumber(1, 2, 3));
            Console.WriteLine("3,2,1 --> " + p.MiddleNumber(3, 2, 1));
            Console.WriteLine("2,1,3 --> " + p.MiddleNumber(2, 1, 3));
            Console.WriteLine("2,3,1 --> " + p.MiddleNumber(2, 3, 1));
            Console.WriteLine("3,1,2 --> " + p.MiddleNumber(3, 1, 2));
            Console.WriteLine("1,3,2 --> " + p.MiddleNumber(1, 3, 2));
            Console.WriteLine("\nЗавдання №4:");
            Console.Write("[ 1 2 3 1 2 1 4 ] --> [ ");
            foreach (var number in p.FindUniqueNumbers(new int[] { 1, 2, 3, 1, 2, 1, 4 }))
            {
                Console.Write(number + " ");
            }
            Console.WriteLine(']');
            Console.WriteLine("\nЗавдання №5:");
            Console.WriteLine("Матриця:");
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            matrix = p.TransposeMatrix(matrix);
            Console.WriteLine("Транспонована матриця:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nЗавдання №6:");
            Console.WriteLine(p.RoundNumber(1.234567f, 3));
            Console.WriteLine("\nЗавдання №7:");
            Console.WriteLine(p.Function1(2));
            Console.WriteLine(p.Function2(2));
            Console.WriteLine("\nЗавдання №8:");
            Console.WriteLine(p.FindSubstringBetweenSymbols("я-не хочу-робити-дз", '-'));
            Console.WriteLine("\nЗавдання №9:");
            int[] bounds = p.FindBoundsOfWord("“Lorem ipsum dolor sit amet”", "ipsum");
            Console.WriteLine(bounds[0] + "-" + bounds[1]);
            Console.WriteLine("\nДОДАТКОВЕ ЗАВДАННЯ:");
            Console.Write("[ 1 2 3 1 2 1 4 1 2 3 5 ] --> [ ");
            foreach (var number in p.NumbersFoundMoreThanTwoTimes(new int[] { 1, 2, 3, 1, 2, 1, 4, 1, 2, 3, 5 }))
            {
                Console.Write(number + " ");
            }
            Console.WriteLine(']');
            Console.ReadKey();
        }
    }
}