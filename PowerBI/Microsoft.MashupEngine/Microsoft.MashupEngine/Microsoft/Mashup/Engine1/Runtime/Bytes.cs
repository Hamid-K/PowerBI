using System;
using System.Text;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001556 RID: 5462
	internal static class Bytes
	{
		// Token: 0x060087E0 RID: 34784 RVA: 0x001CD238 File Offset: 0x001CB438
		public static byte[] FromHexString(string hex)
		{
			if (hex.Length % 2 != 0)
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.Bytes_FromHexString_HexStringRequiresEvenLength, TextValue.New(hex), null);
			}
			byte[] array = new byte[hex.Length / 2];
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				int hexDigit = Bytes.GetHexDigit(hex[num++]);
				int hexDigit2 = Bytes.GetHexDigit(hex[num++]);
				array[i] = (byte)((hexDigit << 4) | hexDigit2);
			}
			return array;
		}

		// Token: 0x060087E1 RID: 34785 RVA: 0x001CD2B0 File Offset: 0x001CB4B0
		public static string ToHexString(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in bytes)
			{
				Bytes.AppendHexDigit(stringBuilder, num >> 4);
				Bytes.AppendHexDigit(stringBuilder, num & 15);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060087E2 RID: 34786 RVA: 0x001CD2F0 File Offset: 0x001CB4F0
		private static int GetHexDigit(char ch)
		{
			if (ch >= '0' && ch <= '9')
			{
				return (int)(ch - '0');
			}
			if (ch >= 'a' && ch <= 'f')
			{
				return (int)('\n' + ch - 'a');
			}
			if (ch >= 'A' && ch <= 'F')
			{
				return (int)('\n' + ch - 'A');
			}
			throw ValueException.NewDataFormatError<Message0>(Strings.Bytes_GetHexDigit_InvalidHexDigit, TextValue.New(ch), null);
		}

		// Token: 0x060087E3 RID: 34787 RVA: 0x001CD341 File Offset: 0x001CB541
		private static void AppendHexDigit(StringBuilder stringBuilder, int value)
		{
			if (value < 10)
			{
				stringBuilder.Append((char)(48 + value));
				return;
			}
			stringBuilder.Append((char)(97 + value - 10));
		}
	}
}
