using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hill
{
    /// <summary>
    /// Класс реализации шифра Хилла (Вариант 9).
    /// </summary>
    public static class HillCipher 
    {
        private const string Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const int Modules = 33;

        /// <summary>
        /// Шифрование текста.
        /// </summary>
        /// <param name="text">Открытый текст</param>
        /// <param name="key">Ключевая матрица (int[,])</param>
        /// <returns>Зашифрованная строка</returns>
        public static string Encrypt(string text, int[,] key)
        {
            ValidateInput(text, key);

            int blockSize = key.GetLength(0);
            if (text.Length % blockSize != 0)
                throw new ArgumentException($"Длина текста должна быть кратна {blockSize}");

            return ProcessText(text, key);
        }

        /// <summary>
        /// Дешифрование текста.
        /// </summary>
        /// <param name="text">Зашифрованный текст</param>
        /// <param name="key">Ключевая матрица</param>
        /// <returns>Расшифрованная строка</returns>
        public static string Decrypt(string text, int[,] key)
        {
            ValidateInput(text, key);
            int[,] inverseKey = GetInverseMatrix(key);
            return ProcessText(text, inverseKey);
        }

        /// <summary>
        /// Валидация входных данных.
        /// </summary>
        private static void ValidateInput(string text, int[,] key)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Текст не может быть пустым");

            if (key == null)
                throw new ArgumentNullException(nameof(key), "Ключ не может быть пустым");

            if (key.GetLength(0) != key.GetLength(1))
                throw new ArgumentException("Матрица должна быть квадратной");

            if (!text.All(c => Alphabet.Contains(char.ToUpper(c))))
                throw new ArgumentException("Текст должен содержать только русские буквы");

            if (!IsMatrixInvertible(key))
            {
                int det = CalculateDeterminant(key);
                if (det == 0)
                    throw new ArgumentException("Ключевая матрица необратима (det = 0)");
                else
                    throw new ArgumentException($"Ключевая матрица необратима\n(НОД(det, {Modules}) != 1)");
            }
        }

        /// <summary>
        /// Проверка обратимости матрицы: det != 0 и НОД(det, 33) == 1.
        /// </summary>
        private static bool IsMatrixInvertible(int[,] matrix)
        {
            int det = CalculateDeterminant(matrix);
            if (det == 0) return false;
            return NOD(det, Modules) == 1;
        }

        /// <summary>
        /// Преобразование блоков текста.
        /// </summary>
        private static string ProcessText(string text, int[,] matrix)
        {
            StringBuilder result = new StringBuilder();
            int size = matrix.GetLength(0);

            for (int i = 0; i < text.Length; i += size)
            {
                int[] vector = new int[size];

                for (int j = 0; j < size; j++)
                {
                    vector[j] = Alphabet.IndexOf(char.ToUpper(text[i + j]));
                }

                int[] resultVector = VectorAndMatrix(vector, matrix);

                foreach (int val in resultVector)
                {
                    result.Append(Alphabet[val]);
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Умножение вектора-строки на матрицу по модулю.
        /// </summary>
        private static int[] VectorAndMatrix(int[] vector, int[,] matrix)
        {
            int size = vector.Length;
            int[] res = new int[size];

            for (int i = 0; i < size; i++)
            {
                int sum = 0;
                for (int j = 0; j < size; j++)
                {
                    sum += vector[j] * matrix[j, i];
                }
                res[i] = ((sum % Modules) + Modules) % Modules;
            }
            return res;
        }

        /// <summary>
        /// Вычисление обратной матрицы по модулю 33.
        /// </summary>
        private static int[,] GetInverseMatrix(int[,] matrix)
        {
            int size = matrix.GetLength(0);
            int det = CalculateDeterminant(matrix);
            int detInv = ModularInverse(det, Modules);
            int[,] adj = GetAdjMatrix(matrix);
            int[,] res = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int val = adj[i, j] % Modules;
                    val = (val + Modules) % Modules;

                    res[i, j] = (val * detInv) % Modules;
                }
            }
            return res;
        }

        /// <summary>
        /// Вспомогательный метод расчета детерминанта.
        /// </summary>
        /// <param name="matrix">Матрица.</param>
        /// <returns>Значение детерминанта</returns>
        /// <exception cref="NotImplementedException">Появляется, если размер матрицы не 2х2 или 3х3</exception>
        private static int CalculateDeterminant(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int det;

            if (n == 2)
            {
                det = matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else if (n == 3)
            {
                det = 0;
                det += matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]);
                det -= matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]);
                det += matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
            }
            else
            {
                throw new NotImplementedException("Поддерживаются только матрицы 2x2 и 3x3");
            }

            det = det % Modules;
            if (det < 0) det += Modules;

            return det;
        }

        /// <summary>
        /// Вычисление союзной матрицы.
        /// </summary>
        /// <param name="matrix">Исходная квадратная матрица</param>
        /// <returns>Союзная матрица</returns>
        private static int[,] GetAdjMatrix(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int[,] adj = new int[n, n];

            if (n == 2)
            {
                adj[0, 0] = matrix[1, 1];
                adj[0, 1] = -matrix[0, 1];
                adj[1, 0] = -matrix[1, 0];
                adj[1, 1] = matrix[0, 0];
            }
            else if (n == 3)
            {
                adj[0, 0] = (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1]);
                adj[1, 0] = -(matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0]);
                adj[2, 0] = (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);

                adj[0, 1] = -(matrix[0, 1] * matrix[2, 2] - matrix[0, 2] * matrix[2, 1]);
                adj[1, 1] = (matrix[0, 0] * matrix[2, 2] - matrix[0, 2] * matrix[2, 0]);
                adj[2, 1] = -(matrix[0, 0] * matrix[2, 1] - matrix[0, 1] * matrix[2, 0]);

                adj[0, 2] = (matrix[0, 1] * matrix[1, 2] - matrix[0, 2] * matrix[1, 1]);
                adj[1, 2] = -(matrix[0, 0] * matrix[1, 2] - matrix[0, 2] * matrix[1, 0]);
                adj[2, 2] = (matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0]);
            }
            return adj;
        }

        /// <summary>
        /// Вычисление наибольшего общего делителя (НОД) двух чисел 
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns>НОД a и b</returns>
        private static int NOD(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Нахождение обратного элемента по модулю.
        /// </summary>
        /// <param name="a">Число, для которого ищется обратный элемент</param>
        /// <param name="m">Модуль</param>
        /// <returns>Обратный элемент a^(-1) mod m</returns>
        private static int ModularInverse(int a, int m)
        {
            int m0 = m, t, q;
            int x0 = 0, x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                q = a / m;
                t = m;
                m = a % m; a = t;
                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }

            if (x1 < 0) x1 += m0;
            return x1;
        }
    }
}
