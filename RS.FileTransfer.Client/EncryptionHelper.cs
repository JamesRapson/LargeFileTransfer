using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace RS.FileTransfer.Client
{
	public static class EncryptionHelper
	{
		static readonly string m_iv = "-CtEt=z%QWr)+A'x";
		static readonly int m_keySize = 128;
		static byte[] m_KeyBytes = null;
		static byte[] m_IVBytes = null;
		static byte[] saltBytes = Encoding.UTF8.GetBytes("YvQvUxh!quD*");
		static readonly string m_Key  = "DJA4a3JK#D/fj39afjFDD!@][fx ads4AAk9j";

        static EncryptionHelper()
		{
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(m_Key, saltBytes, "SHA1", 2);
			m_KeyBytes = pdb.GetBytes(m_keySize / 8);
			m_IVBytes = Encoding.UTF8.GetBytes(m_iv);
		}

		public static string Encrypt(string data)
		{
			if ((data == null) || (data == ""))
				return null;

			RijndaelManaged rijndaelAlg = new RijndaelManaged();
			rijndaelAlg.Padding = PaddingMode.PKCS7;
			rijndaelAlg.Mode = CipherMode.CBC;
			rijndaelAlg.KeySize = m_keySize;
			rijndaelAlg.BlockSize = m_keySize;

			byte[] dataBytes = Encoding.UTF8.GetBytes(data);

			using (MemoryStream encryptStream = new MemoryStream())
			{
				using (CryptoStream csEncrypt = new CryptoStream(encryptStream, rijndaelAlg.CreateEncryptor(m_KeyBytes, m_IVBytes), CryptoStreamMode.Write))
				{
					csEncrypt.Write(dataBytes, 0, dataBytes.Length);
					csEncrypt.FlushFinalBlock();
				}
				return Convert.ToBase64String(encryptStream.ToArray());
			}
		}

        public static string Decrypt(byte[] data)
        {
            if ((data == null) || (data.Length == 0))
                return null;

            RijndaelManaged rijndaelAlg = new RijndaelManaged();
            rijndaelAlg.Padding = PaddingMode.PKCS7;
            rijndaelAlg.Mode = CipherMode.CBC;
            rijndaelAlg.KeySize = m_keySize;
            rijndaelAlg.BlockSize = m_keySize;

            using (MemoryStream decryptStream = new MemoryStream(data))
            {
                using (CryptoStream cStream = new CryptoStream(decryptStream, rijndaelAlg.CreateDecryptor(m_KeyBytes, m_IVBytes), CryptoStreamMode.Read))
                {
                    byte[] buff = new byte[10000];
                    int bytesRead = cStream.Read(buff, 0, buff.Length);
                    return Encoding.UTF8.GetString(buff, 0, bytesRead);
                }
            }

        }


		public static string Decrypt(string data)
		{
			if ((data == null) || (data == ""))
				return null;

			return Decrypt(Convert.FromBase64String(data));
		}

        public static string GetSignature(byte[] data, string privateKey)
        {
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(privateKey);
            byte[] buff = rsaProvider.SignData(data, new SHA1CryptoServiceProvider());
            return Convert.ToBase64String(buff);
        }

        public static bool VerifySignature(byte[] data, byte[] signature, string key)
        {
            RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(key);
            return rsaProvider.VerifyData(data, new SHA1CryptoServiceProvider(), signature);
        }
	}
}

