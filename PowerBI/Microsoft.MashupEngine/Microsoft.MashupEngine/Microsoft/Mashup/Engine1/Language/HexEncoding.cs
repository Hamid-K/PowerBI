using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x0200172D RID: 5933
	internal class HexEncoding
	{
		// Token: 0x060096D8 RID: 38616 RVA: 0x0003B455 File Offset: 0x00039655
		public static bool IsDigit(char ch)
		{
			return (ch >= '0' && ch <= '9') || (ch >= 'A' && ch <= 'F') || (ch >= 'a' && ch <= 'f');
		}

		// Token: 0x060096D9 RID: 38617 RVA: 0x001F4767 File Offset: 0x001F2967
		public static byte GetByte(char ch)
		{
			if (ch <= '9')
			{
				return (byte)(ch - '0');
			}
			if (ch <= 'F')
			{
				return (byte)(ch - 'A' + '\n');
			}
			return (byte)(ch - 'a' + '\n');
		}

		// Token: 0x060096DA RID: 38618 RVA: 0x001F478A File Offset: 0x001F298A
		public static byte GetByte(char ch1, char ch2)
		{
			return HexEncoding.GetByte(ch1) * 16 + HexEncoding.GetByte(ch2);
		}

		// Token: 0x060096DB RID: 38619 RVA: 0x001F47A0 File Offset: 0x001F29A0
		public static byte[] GetBytes(string s, int offset, int length)
		{
			byte[] array = new byte[length / 2];
			for (int i = 0; i < length; i += 2)
			{
				array[i / 2] = HexEncoding.GetByte(s[offset + i], s[offset + i + 1]);
			}
			return array;
		}

		// Token: 0x060096DC RID: 38620 RVA: 0x001F47E4 File Offset: 0x001F29E4
		public static double GetDouble(string s, int offset, int length)
		{
			double num = 0.0;
			for (int i = 0; i < length; i++)
			{
				num = num * 16.0 + (double)HexEncoding.GetByte(s[offset + i]);
			}
			return num;
		}

		// Token: 0x060096DD RID: 38621 RVA: 0x001F4824 File Offset: 0x001F2A24
		private static char GetChar(int b)
		{
			if (b < 10)
			{
				return (char)(48 + b);
			}
			return (char)(55 + b);
		}

		// Token: 0x060096DE RID: 38622 RVA: 0x001F4838 File Offset: 0x001F2A38
		public static string GetString(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			stringBuilder.Length = bytes.Length * 2;
			for (int i = 0; i < bytes.Length; i++)
			{
				byte b = bytes[i];
				stringBuilder[i * 2] = HexEncoding.GetChar(b >> 4);
				stringBuilder[i * 2 + 1] = HexEncoding.GetChar((int)(b & 15));
			}
			return stringBuilder.ToString();
		}
	}
}
