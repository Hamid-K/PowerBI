using System;
using System.Text;

namespace Azure.Core
{
	// Token: 0x02000015 RID: 21
	internal static class Base64Url
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002AAC File Offset: 0x00000CAC
		public static byte[] Decode(string encoded)
		{
			encoded = new StringBuilder(encoded).Replace('-', '+').Replace('_', '/').Append('=', (encoded.Length % 4 == 0) ? 0 : (4 - encoded.Length % 4))
				.ToString();
			return Convert.FromBase64String(encoded);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static string Encode(byte[] bytes)
		{
			return new StringBuilder(Convert.ToBase64String(bytes)).Replace('+', '-').Replace('/', '_').Replace("=", "")
				.ToString();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B2F File Offset: 0x00000D2F
		internal static string DecodeString(string encoded)
		{
			return Encoding.UTF8.GetString(Base64Url.Decode(encoded));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B41 File Offset: 0x00000D41
		internal static string EncodeString(string value)
		{
			return Base64Url.Encode(Encoding.UTF8.GetBytes(value));
		}
	}
}
