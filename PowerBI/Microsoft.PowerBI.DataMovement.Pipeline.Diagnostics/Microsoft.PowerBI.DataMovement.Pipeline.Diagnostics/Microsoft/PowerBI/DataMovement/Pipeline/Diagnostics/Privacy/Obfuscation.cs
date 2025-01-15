using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Privacy
{
	// Token: 0x020000D3 RID: 211
	[NullableContext(1)]
	[Nullable(0)]
	public static class Obfuscation
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x000456BC File Offset: 0x000438BC
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x000456E5 File Offset: 0x000438E5
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

		// Token: 0x06001082 RID: 4226 RVA: 0x000456F0 File Offset: 0x000438F0
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

		// Token: 0x06001083 RID: 4227 RVA: 0x00045728 File Offset: 0x00043928
		public static string Obfuscate(long value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			return Obfuscation.ByteArrayToStringHex(Obfuscation.Obfuscator.ComputeHash(bytes));
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004574C File Offset: 0x0004394C
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

		// Token: 0x06001085 RID: 4229 RVA: 0x0004579D File Offset: 0x0004399D
		private static string ByteArrayToStringHex(byte[] buffer)
		{
			return BitConverter.ToString(buffer).Replace("-", string.Empty);
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000457B4 File Offset: 0x000439B4
		private static string ByteArrayToStringHex(byte[] buffer, int startIndex, int length)
		{
			return BitConverter.ToString(buffer, startIndex, length).Replace("-", string.Empty);
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000457D0 File Offset: 0x000439D0
		private static void XorInPlace(byte[] arrayBytes, int targetLocation, int firstLocation, int secondLocation, int length)
		{
			for (int i = 0; i < length; i++)
			{
				arrayBytes[i + targetLocation] = arrayBytes[i + firstLocation] ^ arrayBytes[i + secondLocation];
			}
		}

		// Token: 0x0400034A RID: 842
		private const int c_shortLength = 8;

		// Token: 0x0400034B RID: 843
		[ThreadStatic]
		private static HashAlgorithm s_HashAlgorithm;
	}
}
