using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hill
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Нажатие кнопки "Зашифровать".
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Encrypt_Click(object sender, RoutedEventArgs e)
        {
            Result.Text = "";
            try
            {
                int[,] key = ReadMatrix();
                string text = TextInput.Text.ToUpper().Replace(" ", "");

                string result = HillCipher.Encrypt(text, key);
                Result.Text = result;
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }

        /// <summary>
        /// Нажатие кнопки "Расшифровать".
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Decrypt_Click(object sender, RoutedEventArgs e)
        {
            Result.Text = "";
            try
            {
                int[,] key = ReadMatrix();
                string text = TextInput.Text.ToUpper().Replace(" ", "");

                string result = HillCipher.Decrypt(text, key);
                Result.Text = result;
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
            }
        }

        /// <summary>
        /// Очистка текста.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void TextInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Result != null)
            {
                Result.Text = "";
            }
        }

        /// <summary>
        /// Очистка текста.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Matrix_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Result != null)
            {
                Result.Text = "";
            }
        }

        /// <summary>
        /// Изменение выбранного размера матрицы.
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void MatrixSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Grid2x2 == null || Grid3x3 == null)
                return;

            if (MatrixSize.SelectedIndex == 0)
            {
                Grid2x2.Visibility = System.Windows.Visibility.Visible;
                Grid3x3.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                Grid2x2.Visibility = System.Windows.Visibility.Collapsed;
                Grid3x3.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Считывание значений из TextBox'ов в матрицу.
        /// </summary>
        private int[,] ReadMatrix()
        {
            try
            {
                if (Grid2x2.Visibility == Visibility.Visible)
                {
                    return new int[2, 2]
                    {
                { ParseInt(M00.Text), ParseInt(M01.Text) },
                { ParseInt(M10.Text), ParseInt(M11.Text) }
                    };
                }
                else
                {
                    return new int[3, 3]
                    {
                { ParseInt(M3_00.Text), ParseInt(M3_01.Text), ParseInt(M3_02.Text) },
                { ParseInt(M3_10.Text), ParseInt(M3_11.Text), ParseInt(M3_12.Text) },
                { ParseInt(M3_20.Text), ParseInt(M3_21.Text), ParseInt(M3_22.Text) }
                    };
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("Матрица должна содержать целые числа");
            }
        }

        private static int ParseInt(string text) => int.Parse(text);
    }
}

