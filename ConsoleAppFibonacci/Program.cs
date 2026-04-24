using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFibonacci
{
    /// <summary>
    /// Старт вычислений чисел Фибоначчи.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Запускт расчета и вывод пятого числа Фибоначчи в консоль.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        static void Main(string[] args)
        {
            int result = Fibonacci(5);
            Console.WriteLine(result);
        }

        /// <summary>
        /// Вычисление n-ого числа ряда Фибоначчи итеративным способом.
        /// </summary>
        /// <param name="n">Порядковый номер числа (отсчет идет с нуля).</param>
        /// <returns>Значение n-го числа Фибоначчи.</returns>
        static int Fibonacci(int n)
        {
            Console.WriteLine("The output is: ");
            int n1 = 0;
            int n2 = 1;
            int sum;

            for (int i = 2; i <= n; i++)
            {
                sum = n1 + n2;
                n1 = n2;
                n2 = sum;
            }

            return n == 0 ? n1 : n2;
        }
    }
}