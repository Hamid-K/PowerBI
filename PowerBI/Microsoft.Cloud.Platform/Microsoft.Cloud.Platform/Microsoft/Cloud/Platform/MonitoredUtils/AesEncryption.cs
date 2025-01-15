using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x0200011A RID: 282
	public class AesEncryption : IObfuscation, IDisposable
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x0001A14B File Offset: 0x0001834B
		public AesEncryption(AesCryptoServiceProvider aesProvider, int keyIdentifier)
		{
			if (aesProvider == null)
			{
				throw new ArgumentNullException("aesProvider");
			}
			this.aesProvider = aesProvider;
			this.keyIdentifier = keyIdentifier;
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0001A16F File Offset: 0x0001836F
		public int GetKeyIdentifier()
		{
			return this.keyIdentifier;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0001A178 File Offset: 0x00018378
		public string ComputePiiReplacement(byte[] piiData)
		{
			this.aesProvider.GenerateIV();
			byte[] iv = this.aesProvider.IV;
			byte[] array;
			using (ICryptoTransform cryptoTransform = this.aesProvider.CreateEncryptor(this.aesProvider.Key, this.aesProvider.IV))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
					{
						cryptoStream.Write(piiData, 0, piiData.Length);
					}
					array = memoryStream.ToArray();
				}
			}
			byte[] array2 = new byte[array.Length + iv.Length];
			Buffer.BlockCopy(iv, 0, array2, 0, iv.Length);
			Buffer.BlockCopy(array, 0, array2, iv.Length, array.Length);
			return Convert.ToBase64String(array2);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0001A260 File Offset: 0x00018460
		public string DecryptObfuscatedData(string obfuscatedData)
		{
			string text = string.Empty;
			byte[] array = ObfuscationUtility.DeserializeByteArray(obfuscatedData);
			byte[] array2 = new byte[this.aesProvider.IV.Length];
			Buffer.BlockCopy(array, 0, array2, 0, this.aesProvider.IV.Length);
			byte[] array3 = new byte[array.Length - this.aesProvider.IV.Length];
			Buffer.BlockCopy(array, array2.Length, array3, 0, array.Length - array2.Length);
			using (ICryptoTransform cryptoTransform = this.aesProvider.CreateDecryptor(this.aesProvider.Key, array2))
			{
				using (MemoryStream memoryStream = new MemoryStream(array3))
				{
					using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
					{
						using (MemoryStream memoryStream2 = new MemoryStream())
						{
							cryptoStream.CopyTo(memoryStream2);
							text = Encoding.Unicode.GetString(memoryStream2.ToArray());
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0001A384 File Offset: 0x00018584
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x040002B6 RID: 694
		private readonly AesCryptoServiceProvider aesProvider;

		// Token: 0x040002B7 RID: 695
		private readonly int keyIdentifier;
	}
}
