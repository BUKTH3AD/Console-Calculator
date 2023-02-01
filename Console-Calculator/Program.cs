namespace Console_Calculator
{
    internal class Program
    {
        private static bool CheckInput(string input)
        {
            if (input == "exit")
                Environment.Exit(0);


            if (input == "help")
            {
                Console.WriteLine("Введите два целых положительных числа до 1000, в формате \"x+y\"");
                return false;
            }

            if (input.Contains('+') || input.Contains('-') || input.Contains('*') || input.Contains('/'))
            {
                return true;
            }
            Console.WriteLine("Wrong format");
            return false;
        }

        private static bool CheckValueCorrect(string input, out int value)
        {
            bool isValid = int.TryParse(input, out value);

            if (!isValid)
            {
                Console.WriteLine($"Operand {value} is incorrect");
                return false;
            }

            if (value >= 1000 || value < 0)
            {
                Console.WriteLine($"Значение {value} не в диапазоне между 1 и 999");
                return false;
            }

            return true;

        }

        private static (bool IsParsed, int X, int Y, char symbol) SplitString(string phrase)
        {
            char symbol = default;
            char[] delimiterChars = { '+', '-', '*', '/' };

            foreach (char item in delimiterChars)
            {
                if (phrase.Contains(item))
                    symbol = item;
            }



            string[] input = phrase.Split(symbol);

            //if (input[count] == "")
            //    count++;

            var isXParsed = CheckValueCorrect(input[0], out var x);
            var isYParsed = CheckValueCorrect(input[1], out var y);

            if (isXParsed && isYParsed)
            {
                return (true, x, y, symbol);
            }

            return (false, 0, 0, symbol);
        }

        static void Main(string[] args)
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

                switch (result.symbol)
                {
                    case '+':
                        Console.WriteLine(result.X + result.Y);
                        break;
                    case '-':
                        Console.WriteLine(result.X - result.Y);
                        break;
                    case '*':
                        Console.WriteLine(result.X * result.Y);
                        break;
                    case '/':
                        if (result.Y == 0)
                        {
                            Console.WriteLine("Делить на ноль нельзя");
                            break;
                        }

                        Console.WriteLine((float)result.X / (float)result.Y);
                        break;
                    default:
                        break;
                }

            } while (true);
        }
    }
}