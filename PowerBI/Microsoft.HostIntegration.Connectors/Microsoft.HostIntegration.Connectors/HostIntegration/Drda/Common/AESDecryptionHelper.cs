using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200084B RID: 2123
	public class AESDecryptionHelper : IDecryptionHelper
	{
		// Token: 0x0600435A RID: 17242 RVA: 0x000E223A File Offset: 0x000E043A
		public AESDecryptionHelper(bool isUnicodeManagerSupported = false)
		{
			this.dh = new DiffieHellmanAlgorithm(Constants.OakleyGroup14P, Constants.OakleyGroup14G, 512);
			this.isUnicodeManagerSupported = isUnicodeManagerSupported;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x000E2263 File Offset: 0x000E0463
		public void GetPublicKey(out byte[] publicKey)
		{
			publicKey = this.dh.CreateKeyExchange();
		}

		// Token: 0x0600435C RID: 17244 RVA: 0x000E2274 File Offset: 0x000E0474
		public byte[] EncryptText(string Data, byte[] Key, byte[] IV)
		{
			byte[] array3;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				AesManaged aesManaged = new AesManaged();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, aesManaged.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
				byte[] array = (this.isUnicodeManagerSupported ? Encoding.UTF8.GetBytes(Data) : Encoding.GetEncoding(500).GetBytes(Data));
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				byte[] array2 = memoryStream.ToArray();
				cryptoStream.Close();
				memoryStream.Close();
				array3 = array2;
			}
			catch (CryptographicException ex)
			{
				if (Logger.maxTracingLevel >= 2)
				{
					Logger.Warning(0, "A Cryptographic error occurred: {0}", new object[] { ex.Message });
				}
				array3 = null;
			}
			return array3;
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x000E2328 File Offset: 0x000E0528
		public string DecryptText(byte[] Data, byte[] sourcePublicKey, byte[] initVector)
		{
			string text;
			try
			{
				Stream stream = new MemoryStream(Data);
				AesManaged aesManaged = new AesManaged();
				Stream stream2 = new CryptoStream(stream, aesManaged.CreateDecryptor(sourcePublicKey, initVector), CryptoStreamMode.Read);
				byte[] array = new byte[Data.Length];
				stream2.Read(array, 0, array.Length);
				text = (this.isUnicodeManagerSupported ? Encoding.UTF8.GetString(array) : Encoding.GetEncoding(500).GetString(array));
			}
			catch (CryptographicException ex)
			{
				Console.WriteLine("A Cryptographic error occurred: {0}", ex.Message);
				text = null;
			}
			return text;
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x000E23B4 File Offset: 0x000E05B4
		public byte[] DecryptKey(byte[] sectoken)
		{
			byte[] array = this.dh.DecryptKeyExchange(sectoken);
			byte[] array2 = new byte[32];
			SHA1 sha = SHA1.Create();
			byte[] array3 = new byte[32];
			byte[] array4 = new byte[32];
			Array.Copy(array, 0, array3, 0, 32);
			Array.Copy(array, 32, array4, 0, 32);
			array3 = sha.ComputeHash(array3);
			array4 = sha.ComputeHash(array4);
			sha.Dispose();
			for (int i = 0; i < 8; i++)
			{
				array3[12 + i] = array3[12 + i] ^ array4[i];
			}
			Array.Copy(array3, 0, array2, 0, 20);
			Array.Copy(array4, 8, array2, 20, 12);
			return array2;
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x000E2454 File Offset: 0x000E0654
		public byte[] GetKeyIV(byte[] sectoken)
		{
			byte[] array = new byte[16];
			Array.Copy(sectoken, 24, array, 0, 16);
			return array;
		}

		// Token: 0x04002F89 RID: 12169
		private DiffieHellmanAlgorithm dh;

		// Token: 0x04002F8A RID: 12170
		private bool isUnicodeManagerSupported;
	}
}
