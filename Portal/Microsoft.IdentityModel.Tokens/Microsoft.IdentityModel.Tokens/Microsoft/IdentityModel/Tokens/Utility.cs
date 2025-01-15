using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Logging;

namespace Microsoft.IdentityModel.Tokens
{
	// Token: 0x02000192 RID: 402
	public static class Utility
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x000433D8 File Offset: 0x000415D8
		public static byte[] CloneByteArray(this byte[] src)
		{
			if (src == null)
			{
				throw LogHelper.LogArgumentNullException("src");
			}
			return (byte[])src.Clone();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000433F4 File Offset: 0x000415F4
		internal static string SerializeAsSingleCommaDelimitedString(IEnumerable<string> strings)
		{
			if (strings == null)
			{
				return "null";
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (string text in strings)
			{
				if (flag)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}", text ?? "null");
					flag = false;
				}
				else
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, ", {0}", text ?? "null");
				}
			}
			if (flag)
			{
				return "empty";
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00043494 File Offset: 0x00041694
		public static bool IsHttps(string address)
		{
			if (string.IsNullOrEmpty(address))
			{
				return false;
			}
			bool flag;
			try
			{
				flag = Utility.IsHttps(new Uri(address));
			}
			catch (UriFormatException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000434D0 File Offset: 0x000416D0
		public static bool IsHttps(Uri uri)
		{
			return !(uri == null) && uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x000434F0 File Offset: 0x000416F0
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static bool AreEqual(byte[] a, byte[] b)
		{
			byte[] array = new byte[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
				30, 31
			};
			byte[] array2 = new byte[]
			{
				31, 30, 29, 28, 27, 26, 25, 24, 23, 22,
				21, 20, 19, 18, 17, 16, 15, 14, 13, 12,
				11, 10, 9, 8, 7, 6, 5, 4, 3, 2,
				1, 0
			};
			int num = 0;
			byte[] array3;
			byte[] array4;
			if (a == null || b == null || a.Length != b.Length)
			{
				array3 = array;
				array4 = array2;
			}
			else
			{
				array3 = a;
				array4 = b;
			}
			for (int i = 0; i < array3.Length; i++)
			{
				num |= (int)(array3[i] ^ array4[i]);
			}
			return num == 0;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00043564 File Offset: 0x00041764
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal static bool AreEqual(byte[] a, byte[] b, int length)
		{
			byte[] array = new byte[]
			{
				0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
				10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
				20, 21, 22, 23, 24, 25, 26, 27, 28, 29,
				30, 31
			};
			byte[] array2 = new byte[]
			{
				31, 30, 29, 28, 27, 26, 25, 24, 23, 22,
				21, 20, 19, 18, 17, 16, 15, 14, 13, 12,
				11, 10, 9, 8, 7, 6, 5, 4, 3, 2,
				1, 0
			};
			int num = 0;
			byte[] array3;
			byte[] array4;
			int num2;
			if (a == null || b == null || a.Length < length || b.Length < length)
			{
				array3 = array;
				array4 = array2;
				num2 = array3.Length;
			}
			else
			{
				array3 = a;
				array4 = b;
				num2 = length;
			}
			for (int i = 0; i < num2; i++)
			{
				num |= (int)(array3[i] ^ array4[i]);
			}
			return num == 0;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000435E4 File Offset: 0x000417E4
		internal static byte[] ConvertToBigEndian(long i)
		{
			byte[] bytes = BitConverter.GetBytes(i);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00043608 File Offset: 0x00041808
		internal static byte[] Xor(byte[] a, byte[] b, int offset, bool inPlace)
		{
			if (inPlace)
			{
				for (int i = 0; i < a.Length; i++)
				{
					a[i] ^= b[offset + i];
				}
				return a;
			}
			byte[] array = new byte[a.Length];
			for (int j = 0; j < a.Length; j++)
			{
				array[j] = a[j] ^ b[offset + j];
			}
			return array;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0004365C File Offset: 0x0004185C
		internal static void Zero(byte[] byteArray)
		{
			for (int i = 0; i < byteArray.Length; i++)
			{
				byteArray[i] = 0;
			}
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0004367C File Offset: 0x0004187C
		internal static byte[] GenerateSha256Hash(string input)
		{
			byte[] array;
			using (SHA256 sha = SHA256.Create())
			{
				array = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
			}
			return array;
		}

		// Token: 0x040006E2 RID: 1762
		public const string Empty = "empty";

		// Token: 0x040006E3 RID: 1763
		public const string Null = "null";
	}
}
