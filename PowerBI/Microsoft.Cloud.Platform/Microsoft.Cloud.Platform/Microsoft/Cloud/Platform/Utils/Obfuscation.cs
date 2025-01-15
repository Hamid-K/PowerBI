using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000263 RID: 611
	public static class Obfuscation
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00037358 File Offset: 0x00035558
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00037381 File Offset: 0x00035581
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

		// Token: 0x06001016 RID: 4118 RVA: 0x0003738C File Offset: 0x0003558C
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

		// Token: 0x06001017 RID: 4119 RVA: 0x000373C4 File Offset: 0x000355C4
		public static string Obfuscate(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return Obfuscation.ByteArrayToStringHex(Obfuscation.Obfuscator.ComputeHash(bytes));
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x000373E8 File Offset: 0x000355E8
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

		// Token: 0x06001019 RID: 4121 RVA: 0x00037439 File Offset: 0x00035639
		private static string ByteArrayToStringHex(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", string.Empty);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00037450 File Offset: 0x00035650
		private static string ByteArrayToStringHex(byte[] buffer, int startIndex, int length)
		{
			return BitConverter.ToString(buffer, startIndex, length).Replace("-", string.Empty);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0003746C File Offset: 0x0003566C
		private static void XorInPlace(byte[] arrayBytes, int targetLocation, int firstLocation, int secondLocation, int length)
		{
			for (int i = 0; i < length; i++)
			{
				arrayBytes[i + targetLocation] = arrayBytes[i + firstLocation] ^ arrayBytes[i + secondLocation];
			}
		}

		// Token: 0x040005FF RID: 1535
		private const int c_shortLength = 8;

		// Token: 0x04000600 RID: 1536
		[ThreadStatic]
		private static HashAlgorithm s_HashAlgorithm;
	}
}
