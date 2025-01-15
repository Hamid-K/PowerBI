using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200000C RID: 12
	public static class Obfuscation
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020AD File Offset: 0x000002AD
		private static HashAlgorithm Obfuscator
		{
			get
			{
				HashAlgorithm hashAlgorithm = Obfuscation.s_HashAlgorithm;
				if (hashAlgorithm == null)
				{
					hashAlgorithm = new SHA256CryptoServiceProvider();
					hashAlgorithm.Initialize();
					Obfuscation.Obfuscator = hashAlgorithm;
				}
				return hashAlgorithm;
			}
			set
			{
				Obfuscation.s_HashAlgorithm = value;
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020B8 File Offset: 0x000002B8
		public static string Obfuscate(string value, bool caseSensitive)
		{
			string text = value;
			if (!caseSensitive)
			{
				text = value.ToUpperInvariant();
			}
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			return Obfuscation.ByteArrayToStringHex(Obfuscation.Obfuscator.ComputeHash(bytes));
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000020F0 File Offset: 0x000002F0
		public static string Obfuscate(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return Obfuscation.ByteArrayToStringHex(Obfuscation.Obfuscator.ComputeHash(bytes));
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002114 File Offset: 0x00000314
		public static string ShortObfuscate(string value, bool caseSensitive)
		{
			string text = value;
			if (!caseSensitive)
			{
				text = value.ToUpperInvariant();
			}
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			byte[] array = Obfuscation.Obfuscator.ComputeHash(bytes);
			for (int i = 8; i < array.Length; i += 8)
			{
				Obfuscation.XorInPlace(array, 0, 0, i, 8);
			}
			return Obfuscation.ByteArrayToStringHex(array, 0, 8);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002165 File Offset: 0x00000365
		private static string ByteArrayToStringHex(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", string.Empty);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000217C File Offset: 0x0000037C
		private static string ByteArrayToStringHex(byte[] buffer, int startIndex, int length)
		{
			return BitConverter.ToString(buffer, startIndex, length).Replace("-", string.Empty);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002198 File Offset: 0x00000398
		private static void XorInPlace(byte[] arrayBytes, int targetLocation, int firstLocation, int secondLocation, int length)
		{
			for (int i = 0; i < length; i++)
			{
				arrayBytes[i + targetLocation] = arrayBytes[i + firstLocation] ^ arrayBytes[i + secondLocation];
			}
		}

		// Token: 0x04000033 RID: 51
		private const int c_shortLength = 8;

		// Token: 0x04000034 RID: 52
		[ThreadStatic]
		private static HashAlgorithm s_HashAlgorithm;
	}
}
