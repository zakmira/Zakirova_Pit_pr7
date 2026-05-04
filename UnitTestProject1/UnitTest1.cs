using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEncryptBigram()
        {
            string text = "ПР";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            string encrypted = Hill.Encrypt(text, key);
            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(text, encrypted);
        }

        [TestMethod]
        public void TestDecryptBigram()
        {
            string text = "ПР";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            string encrypted = Hill.Encrypt(text, key);
            string decrypted = Hill.Decrypt(encrypted, key);
            Assert.AreEqual(text, decrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptWrongLength()
        {
            string text = "ПРИ";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            Hill.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptDetZero()
        {
            string text = "ПР";
            int[,] key = { { 2, 4 }, { 1, 2 } };
            Hill.Encrypt(text, key);
        }

        [TestMethod]
        public void TestEncryptDetCheck()
        {
            string text = "ПР";
            int[,] key = { { 1, 2 }, { 3, 4 } };
            string encrypted = Hill.Encrypt(text, key);
            Assert.IsNotNull(encrypted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptEmpty()
        {
            string text = "";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            Hill.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestEncryptInvalid()
        {
            string text = "ПР1ВЕТ";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            Hill.Encrypt(text, key);
        }

        [TestMethod]
        public void TestEncryptTrigram()
        {
            string text = "ПРИ";
            int[,] key = { { 6, 24, 1 }, { 13, 16, 10 }, { 20, 17, 15 } };
            string encrypted = Hill.Encrypt(text, key);
            Assert.IsNotNull(encrypted);
            Assert.AreNotEqual(text, encrypted);
        }

        [TestMethod]
        public void TestEncryptDecrypt()
        {
            string text = "ПРИВЕТ";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            string encrypted = Hill.Encrypt(text, key);
            string decrypted = Hill.Decrypt(encrypted, key);
            Assert.AreEqual(text, decrypted);
        }

        [TestMethod]
        public void TestDecryptUnencryptedText()
        {
            string text = "ПРИВЕТ";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            string result = Hill.Decrypt(text, key);
            Assert.AreNotEqual(text, result);
        }

        [TestMethod]
        public void TestReEncryptText()
        {
            string original = "ПРИВЕТ";
            int[,] key = { { 3, 2 }, { 5, 7 } };
            string encrypted1 = Hill.Encrypt(original, key);
            string encrypted2 = Hill.Encrypt(encrypted1, key);
            Assert.AreNotEqual(encrypted1, encrypted2);
            Assert.AreNotEqual(original, encrypted2);
        }

        [TestMethod]
        public void TestValidationLogic()
        {
            int[,] validKey = { { 3, 2 }, { 5, 7 } };
            int[,] invalidKey = { { 2, 4 }, { 1, 2 } };

            Assert.ThrowsException<ArgumentException>(() => Hill.Encrypt("", validKey));
            Assert.ThrowsException<ArgumentException>(() => Hill.Encrypt("АБ", invalidKey));
            Assert.ThrowsException<ArgumentException>(() => Hill.Encrypt("А1Б", validKey));
        }
    }
}
