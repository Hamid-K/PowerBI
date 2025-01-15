using System;
using System.Security.Cryptography;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000010 RID: 16
	internal static class UrlFriendlyUIDGenerator
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000023A8 File Offset: 0x000005A8
		static UrlFriendlyUIDGenerator()
		{
			for (int i = UrlFriendlyUIDGenerator.s_encoding.Length - 1; i >= 0; i--)
			{
				char c = UrlFriendlyUIDGenerator.s_encoding[i];
				UrlFriendlyUIDGenerator.s_legalchars[(int)c] = true;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002400 File Offset: 0x00000600
		internal static bool IsLegit(string s)
		{
			if (string.IsNullOrEmpty(s) || s.Length != 24)
			{
				return false;
			}
			int num = 24;
			while (--num >= 0)
			{
				char c = s[num];
				if ((int)c >= UrlFriendlyUIDGenerator.s_legalchars.Length || !UrlFriendlyUIDGenerator.s_legalchars[(int)c])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000244C File Offset: 0x0000064C
		private static string Encode(byte[] buffer)
		{
			char[] array = new char[24];
			if (buffer.Length != 15)
			{
				throw new Exception("Unexpected session id buffer");
			}
			int num = 0;
			for (int i = 0; i < 15; i += 5)
			{
				int num2 = (int)buffer[i] | ((int)buffer[i + 1] << 8) | ((int)buffer[i + 2] << 16) | ((int)buffer[i + 3] << 24);
				int num3 = num2 & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num2 >> 5) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num2 >> 10) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num2 >> 15) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num2 >> 20) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num2 >> 25) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				int num4 = (num2 >> 30) | ((int)buffer[i + 4] << 2);
				num3 = num4 & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
				num3 = (num4 >> 5) & 31;
				array[num++] = UrlFriendlyUIDGenerator.s_encoding[num3];
			}
			if (num != 24)
			{
				throw new Exception("Unexpected error generating session id");
			}
			return new string(array);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002570 File Offset: 0x00000770
		public static string Create()
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[15];
			randomNumberGenerator.GetBytes(array);
			return UrlFriendlyUIDGenerator.Encode(array);
		}

		// Token: 0x04000039 RID: 57
		internal const int NUM_CHARS_IN_ENCODING = 32;

		// Token: 0x0400003A RID: 58
		internal const int ENCODING_BITS_PER_CHAR = 5;

		// Token: 0x0400003B RID: 59
		internal const int ID_LENGTH_BITS = 120;

		// Token: 0x0400003C RID: 60
		internal const int ID_LENGTH_BYTES = 15;

		// Token: 0x0400003D RID: 61
		internal const int ID_LENGTH_CHARS = 24;

		// Token: 0x0400003E RID: 62
		private static char[] s_encoding = new char[]
		{
			'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
			'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
			'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3',
			'4', '5'
		};

		// Token: 0x0400003F RID: 63
		private static bool[] s_legalchars = new bool[128];
	}
}
