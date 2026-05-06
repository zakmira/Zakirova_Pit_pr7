using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Hill;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEncryptBigram()
        {
            string text = "ПР";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            string encrypted = HillCipher.Encrypt(text, key);

            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(text, encrypted);
            Assert.AreEqual(text.Length, encrypted.Length);
        }

        [TestMethod]
        public void TestDecryptBigram()
        {
            string text = "ПР";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            string encrypted = HillCipher.Encrypt(text, key);
            string decrypted = HillCipher.Decrypt(encrypted, key);

            Assert.AreEqual(text, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptWrongLength()
        {
            string text = "ПРИ";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            HillCipher.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptDetZero()
        {
            string text = "ПР";
            int[,] key = { { 2, 4 }, { 1, 2 } };

            HillCipher.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptDetAndModule()
        {
            string text = "ПР";
            int[,] key = { { 3, 6 }, { 1, 3 } };

            HillCipher.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptEmpty()
        {
            string text = "";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            HillCipher.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptInvalid()
        {
            string text = "ПР1ВЕТ";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            HillCipher.Encrypt(text, key);
        }

        [TestMethod]
        public void TestEncryptTrigram()
        {
            string text = "ПРИ";
            int[,] key = { { 1, 1, 0 }, { 0, 1, 1 }, { 1, 0, 1 } };

            string encrypted = HillCipher.Encrypt(text, key);

            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(text, encrypted);
        }

        [TestMethod]
        public void TestEncryptDecrypt()
        {
            string text = "ПРИВЕТ";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            string encrypted = HillCipher.Encrypt(text, key);
            string decrypted = HillCipher.Decrypt(encrypted, key);

            Assert.AreEqual(text, decrypted);
        }

        public void TestIgnoreSpaces()
        {
            string textSpace = "П Р И В Е Т";
            string textNoSpace = "ПРИВЕТ";
            int[,] key = { { 1, 2 }, { 3, 4 } };

            string encryptedSpace = HillCipher.Encrypt(textSpace, key);
            string encryptedNoSpace = HillCipher.Encrypt(textNoSpace, key);

            Assert.AreEqual(encryptedSpace, encryptedNoSpace);
        }

        [TestMethod]
        public void TestNegativeMatrix()
        {
            string text = "ПР";
            int[,] key = { { -1, 2 }, { 3, -4 } };

            string encrypted = HillCipher.Encrypt(text, key);
            string decrypted = HillCipher.Decrypt(encrypted, key);

            Assert.IsNotNull(encrypted);
            Assert.AreEqual(text, decrypted);
        }

        [TestMethod]
        public void TestValidationLogic()
        {
            int[,] validKey = { { 1, 2 }, { 3, 4 } };
            int[,] zeroDetKey = { { 2, 4 }, { 1, 2 } };
            int[,] notCoprimeKey = { { 3, 6 }, { 1, 3 } };

            Assert.ThrowsException<ArgumentException>(() => HillCipher.Encrypt("", validKey));
            Assert.ThrowsException<ArgumentException>(() => HillCipher.Encrypt("ПР", zeroDetKey));
            Assert.ThrowsException<ArgumentException>(() => HillCipher.Encrypt("ПР", notCoprimeKey));
            Assert.ThrowsException<ArgumentException>(() => HillCipher.Encrypt("ПР1ВЕТ", validKey));
        }
    }
}
