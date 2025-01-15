using System;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x02000039 RID: 57
	public static class Obfuscation
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000048C4 File Offset: 0x00002AC4
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000048ED File Offset: 0x00002AED
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

		// Token: 0x0600013E RID: 318 RVA: 0x000048F8 File Offset: 0x00002AF8
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

		// Token: 0x0600013F RID: 319 RVA: 0x00004930 File Offset: 0x00002B30
		public static string Obfuscate(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return Obfuscation.ByteArrayToStringHex(Obfuscation.Obfuscator.ComputeHash(bytes));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004954 File Offset: 0x00002B54
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

		// Token: 0x06000141 RID: 321 RVA: 0x000049A5 File Offset: 0x00002BA5
		private static string ByteArrayToStringHex(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", string.Empty);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000049BC File Offset: 0x00002BBC
		private static string ByteArrayToStringHex(byte[] buffer, int startIndex, int length)
		{
			return BitConverter.ToString(buffer, startIndex, length).Replace("-", string.Empty);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000049D8 File Offset: 0x00002BD8
		private static void XorInPlace(byte[] arrayBytes, int targetLocation, int firstLocation, int secondLocation, int length)
		{
			for (int i = 0; i < length; i++)
			{
				arrayBytes[i + targetLocation] = arrayBytes[i + firstLocation] ^ arrayBytes[i + secondLocation];
			}
		}

		// Token: 0x040000D6 RID: 214
		private const int c_shortLength = 8;

		// Token: 0x040000D7 RID: 215
		[ThreadStatic]
		private static HashAlgorithm s_HashAlgorithm;
	}
}
