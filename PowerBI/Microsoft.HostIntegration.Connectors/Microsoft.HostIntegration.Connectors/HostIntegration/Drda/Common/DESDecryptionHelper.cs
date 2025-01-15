using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200084C RID: 2124
	public class DESDecryptionHelper : IDecryptionHelper
	{
		// Token: 0x06004360 RID: 17248 RVA: 0x000E2476 File Offset: 0x000E0676
		public DESDecryptionHelper(bool isUnicodeManagerSupported = false)
		{
			this.dh = new DiffieHellmanAlgorithm(Constants.OakleyGroup5P, Constants.OakleyGroup5G, 256);
			this.isUnicodeManagerSupported = isUnicodeManagerSupported;
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x000E249F File Offset: 0x000E069F
		public void GetPublicKey(out byte[] publicKey)
		{
			publicKey = this.dh.CreateKeyExchange();
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x000E24B0 File Offset: 0x000E06B0
		public byte[] EncryptText(string Data, byte[] Key, byte[] IV)
		{
			byte[] array3;
			try
			{
				MemoryStream memoryStream = new MemoryStream();
				DES des = DES.Create();
				CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write);
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
				Console.WriteLine("A Cryptographic error occurred: {0}", ex.Message);
				array3 = null;
			}
			return array3;
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x000E2550 File Offset: 0x000E0750
		public string DecryptText(byte[] Data, byte[] sourcePublicKey, byte[] initVector)
		{
			string text;
			try
			{
				Stream stream = new MemoryStream(Data);
				DES des = DES.Create();
				Stream stream2 = new CryptoStream(stream, des.CreateDecryptor(sourcePublicKey, initVector), CryptoStreamMode.Read);
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

		// Token: 0x06004364 RID: 17252 RVA: 0x000E25DC File Offset: 0x000E07DC
		public byte[] DecryptKey(byte[] sectoken)
		{
			Array array = this.dh.DecryptKeyExchange(sectoken);
			byte[] array2 = new byte[8];
			Array.Copy(array, 12, array2, 0, 8);
			return array2;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x000E2608 File Offset: 0x000E0808
		public byte[] GetKeyIV(byte[] sectoken)
		{
			byte[] array = new byte[8];
			Array.Copy(sectoken, 12, array, 0, 8);
			return array;
		}

		// Token: 0x04002F8B RID: 12171
		private DiffieHellmanAlgorithm dh;

		// Token: 0x04002F8C RID: 12172
		private bool isUnicodeManagerSupported;
	}
}
