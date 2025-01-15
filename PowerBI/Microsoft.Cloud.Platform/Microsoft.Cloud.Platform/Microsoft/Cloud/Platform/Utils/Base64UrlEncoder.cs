using System;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200019C RID: 412
	public static class Base64UrlEncoder
	{
		// Token: 0x06000A91 RID: 2705 RVA: 0x000245D8 File Offset: 0x000227D8
		public static string DecodeAsString(string base64String)
		{
			byte[] array = Base64UrlEncoder.DecodeAsByteArray(base64String);
			return Base64UrlEncoder.s_utf8Encoding.GetString(array);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x000245F7 File Offset: 0x000227F7
		public static byte[] DecodeAsByteArray([NotNull] string base64String)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(base64String, "base64String");
			return Convert.FromBase64String(Base64UrlEncoder.Base64UrlToBase64(base64String));
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002460F File Offset: 0x0002280F
		public static string Encode([NotNull] byte[] buffer)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<byte[]>(buffer, "buffer");
			return Base64UrlEncoder.Base64ToBase64Url(Convert.ToBase64String(buffer));
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00024627 File Offset: 0x00022827
		public static string Encode([NotNull] string inputString)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(inputString, "inputString");
			return Base64UrlEncoder.Encode(Base64UrlEncoder.s_utf8Encoding.GetBytes(inputString));
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00024644 File Offset: 0x00022844
		private static string Base64ToBase64Url(string plainBase64String)
		{
			StringBuilder stringBuilder = new StringBuilder(plainBase64String.Length);
			foreach (char c in plainBase64String)
			{
				if (c == '+')
				{
					stringBuilder.Append('-');
				}
				else if (c == '/')
				{
					stringBuilder.Append('_');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000246A8 File Offset: 0x000228A8
		private static string Base64UrlToBase64(string base64UrlFriendlyString)
		{
			StringBuilder stringBuilder = new StringBuilder(base64UrlFriendlyString.Length);
			foreach (char c in base64UrlFriendlyString)
			{
				if (c == '-')
				{
					stringBuilder.Append('+');
				}
				else if (c == '_')
				{
					stringBuilder.Append('/');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000428 RID: 1064
		private const char c_base64Character62 = '+';

		// Token: 0x04000429 RID: 1065
		private const char c_base64UrlCharacter62 = '-';

		// Token: 0x0400042A RID: 1066
		private const char c_base64Character63 = '/';

		// Token: 0x0400042B RID: 1067
		private const char c_base64UrlCharacter63 = '_';

		// Token: 0x0400042C RID: 1068
		private static readonly Encoding s_utf8Encoding = Encoding.UTF8;
	}
}
