using System;
using System.Collections.Generic;

namespace PracticalWork1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> history = new List<string>();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("\n КАЛЬКУЛЯТОР ");
                Console.WriteLine("1. Обчислити вираз");
                Console.WriteLine("2. Показати iсторiю обчислень");
                Console.WriteLine("0. Вихiд");

                int choice = ReadInt("Ваш вибiр: ", 0, 2);

                switch (choice)
                {
                    case 1:
                        CalculateExpression(history);
                        break;

                    case 2:
                        ShowHistory(history);
                        break;

                    case 0:
                        isRunning = false;
                        break;
                }
            }

            Console.WriteLine("Роботу програми завершено.");
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);

                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value) &&
                    value >= min &&
                    value <= max)
                {
                    return value;
                }

                Console.WriteLine(
                    $"Некоректне значення. Введіть число вiд {min} до {max}.");
            }
        }

        static void CalculateExpression(List<string> history)
        {
            Console.Write(
                "Введiть вираз у форматi \"число оператор число\" (наприклад, 12 + 7): ");

            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Помилка: вираз не може бути порожнiм.");
                return;
            }

            string[] parts = input.Split(
                ' ',
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 3)
            {
                Console.WriteLine(
                    "Помилка: введiть вираз у форматi \"число оператор число\".");
                return;
            }

            if (!double.TryParse(parts[0], out double firstNumber))
            {
                Console.WriteLine("Помилка: перше значення не є числом.");
                return;
            }

            if (!double.TryParse(parts[2], out double secondNumber))
            {
                Console.WriteLine("Помилка: друге значення не є числом.");
                return;
            }

            if (parts[1].Length != 1)
            {
                Console.WriteLine("Помилка: некоректний оператор.");
                return;
            }

            char operation = parts[1][0];

            if (operation != '+' &&
                operation != '-' &&
                operation != '*' &&
                operation != '/')
            {
                Console.WriteLine(
                    $"Помилка: операцiя '{operation}' не пiдтримується.");
                return;
            }

            if (operation == '/' && secondNumber == 0)
            {
                Console.WriteLine("Помилка: дiлення на нуль неможливе.");
                return;
            }

            double result = Calculate(
                firstNumber,
                secondNumber,
                operation);

            string historyItem =
                $"{firstNumber} {operation} {secondNumber} = {result}";

            Console.WriteLine($"Результат: {result}");

            history.Add(historyItem);
        }

        static double Calculate(
            double firstNumber,
            double secondNumber,
            char operation)
        {
            return operation switch
            {
                '+' => firstNumber + secondNumber,
                '-' => firstNumber - secondNumber,
                '*' => firstNumber * secondNumber,
                '/' => firstNumber / secondNumber,
                _ => 0
            };
        }

        static void ShowHistory(List<string> history)
        {
            Console.WriteLine("\n IСТОРIЯ ОБЧИСЛЕНЬ ");

            if (history.Count == 0)
            {
                Console.WriteLine("Iсторiя порожня.");
                return;
            }

            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {history[i]}");
            }
        }
    }
}