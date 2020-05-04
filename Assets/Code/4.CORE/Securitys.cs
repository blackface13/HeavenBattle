using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace Assets.Code._4.CORE
{
    public static class Securitys
    {
        #region Encryption

        private const string passPhrase = "BlackFace13"; // can be any string
        private const string saltValue = "sALtValue"; // can be any string
        private const string hashAlgorithm = "SHA1"; // can be "MD5"
        private const int passwordIterations = 7; // can be any number
        private const string initVector = "~1B2c3D4e5F6g7H8"; // must be 16 bytes
        private const int keySize = 256; // can be 192 or 128

        /// <summary>
        /// Encrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Encrypt2(string data)
        {
            var bytes = Encoding.ASCII.GetBytes(initVector);
            var rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            var buffer = Encoding.UTF8.GetBytes(data);
#pragma warning disable 618
            var rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
#pragma warning restore 618
            var managed = new RijndaelManaged { Mode = CipherMode.CBC };
            var transform = managed.CreateEncryptor(rgbKey, bytes);
            var stream = new MemoryStream();
            var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray).Replace("=", "~").Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        /// Decrypts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static string Decrypt2(string data)
        {
            data = data.Replace('~', '=').Replace('-', '+').Replace('_', '/');

            byte[] bytes = Encoding.ASCII.GetBytes(initVector);
            byte[] rgbSalt = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(data);
#pragma warning disable 618
            byte[] rgbKey = new PasswordDeriveBytes(passPhrase, rgbSalt, hashAlgorithm, passwordIterations).GetBytes(keySize / 8);
#pragma warning restore 618
            var managed = new RijndaelManaged { Mode = CipherMode.CBC };
            ICryptoTransform transform = managed.CreateDecryptor(rgbKey, bytes);
            var stream = new MemoryStream(buffer);
            var stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            var buffer5 = new byte[buffer.Length];
            int count = stream2.Read(buffer5, 0, buffer5.Length);
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, count);
        }

        #region Security
        /// <summary>
        /// Encode a string by protect key (var name: PROTECTKEY)
        /// </summary>
        /// <param name="strEnCrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string strEnCrypt)
        {
            try
            {
                byte[] keyArr;
                byte[] EnCryptArr = UTF8Encoding.UTF8.GetBytes(strEnCrypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(passPhrase));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateEncryptor();
                byte[] arrResult = transform.TransformFinalBlock(EnCryptArr, 0, EnCryptArr.Length);
                return Convert.ToBase64String(arrResult, 0, arrResult.Length);
            }
            catch (Exception) { }
            return "";
        }
        /// <summary>
        /// Decode a string encoded by protect key (var name: PROTECTKEY)
        /// </summary>
        /// <param name="strDecypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string strDecypt)
        {
            try
            {
                byte[] keyArr;
                byte[] DeCryptArr = Convert.FromBase64String(strDecypt);
                MD5CryptoServiceProvider MD5Hash = new MD5CryptoServiceProvider();
                keyArr = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(passPhrase));
                TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider();
                tripDes.Key = keyArr;
                tripDes.Mode = CipherMode.ECB;
                tripDes.Padding = PaddingMode.PKCS7;
                ICryptoTransform transform = tripDes.CreateDecryptor();
                byte[] arrResult = transform.TransformFinalBlock(DeCryptArr, 0, DeCryptArr.Length);
                return UTF8Encoding.UTF8.GetString(arrResult);
            }
            catch (Exception) { }
            return "";
        }
        #endregion
        #endregion
    }
}
