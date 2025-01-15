using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core
{
	// Token: 0x0200006E RID: 110
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Base64Url
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x0000AD50 File Offset: 0x00008F50
		public static byte[] Decode(string encoded)
		{
			encoded = new StringBuilder(encoded).Replace('-', '+').Replace('_', '/').Append('=', (encoded.Length % 4 == 0) ? 0 : (4 - encoded.Length % 4))
				.ToString();
			return Convert.FromBase64String(encoded);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public static string Encode(byte[] bytes)
		{
			return new StringBuilder(Convert.ToBase64String(bytes)).Replace('+', '-').Replace('/', '_').Replace("=", "")
				.ToString();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000ADD3 File Offset: 0x00008FD3
		internal static string DecodeString(string encoded)
		{
			return Encoding.UTF8.GetString(Base64Url.Decode(encoded));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		internal static string EncodeString(string value)
		{
			return Base64Url.Encode(Encoding.UTF8.GetBytes(value));
		}
	}
}
