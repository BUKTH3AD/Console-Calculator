using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Console_Calculator
{
    internal class Program
    {
        private static readonly Regex Format = new Regex(@"^(\d+)([+-\/*])(\d+)$");
        private static int _x;
        private static int _y;

        private static readonly Dictionary<char, Action<int, int>> Mapping = new Dictionary<char, Action<int, int>>
    {
      { '+', Addition },
      { '-', Subtraction },
      { '*', Multiply },
      { '/', Divide }
    };

        static void Main()
        {
            do
            {
                var input = Console.ReadLine();

                if (!CheckInput(input))
                {
                    continue;
                }

                var result = SplitString(input);

                if (!result.IsParsed)
                {
                    continue;
                }

                Mapping[result.symbol].Invoke(_x, _y);

            } while (true);
        }

        private static bool CheckInput(string input)
        {
            if (input == "exit")
                Environment.Exit(0);

            if (input == "help")
            {
                Console.WriteLine("Введите два целых положительных числа до 1000, в формате \"x{+,-,*,/}y\"");

                return false;
            }

            if (Format.IsMatch(input))
                return true;

            Console.WriteLine("Wrong format. Введите два целых положительных числа до 1000, в формате \"x{+,-,*,/}y\"");

            return false;
        }

        private static bool CheckValueCorrect(string input, out int value)
        {
            var isValid = int.TryParse(input, out value);

            if (!isValid)
            {
                Console.WriteLine($"Operand {value} is incorrect");
                return false;
            }

            if (value < 1000 && value >= 0)
                return true;

            Console.WriteLine($"Значение {value} не в диапазоне между 1 и 999");

            return false;
        }

        private static (bool IsParsed, char symbol) SplitString(string phrase)
        {
            var match = Format.Match(phrase);
            var symbol = char.Parse(match.Groups[2].Value);
            var xString = match.Groups[1].Value;
            var yString = match.Groups[3].Value;

            var isXParsed = CheckValueCorrect(xString, out _x);
            var isYParsed = CheckValueCorrect(yString, out _y);

            if (isXParsed && isYParsed)
            {
                return (true, symbol);
            }

            return (false, symbol);
        }

        private static void Addition(int x, int y) => Console.WriteLine(x + y);

        private static void Subtraction(int x, int y) => Console.WriteLine(x - y);


        private static void Multiply(int x, int y) => Console.WriteLine(x * y);

        private static void Divide(int x, int y)
        {
            if (_y == 0)
            {
                Console.WriteLine("Делить на ноль нельзя");

                return;
            }

            Console.WriteLine(x / (float)y);
        }
    }
}