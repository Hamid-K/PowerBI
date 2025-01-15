using System;
using Microsoft.PowerBI.Telemetry.PIIUtils;

namespace Microsoft.PowerBI.Telemetry
{
	// Token: 0x0200002A RID: 42
	public static class PrivacyUtils
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00003F44 File Offset: 0x00002144
		public static string RemoveMarkupAndObfuscate(this string input, bool caseSensitive = false)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			Func<string, string> func = (caseSensitive ? PrivacyUtils.CaseSensitiveTransform : PrivacyUtils.CaseInsensitiveTransform);
			return input.RemovePrivateInternalAndCustomMarkup(PrivacyUtils.CustomTagNames, func, func, func);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003F7D File Offset: 0x0000217D
		public static string Obfuscate(this string input, bool caseSensitive = false)
		{
			if (string.IsNullOrEmpty(input))
			{
				return string.Empty;
			}
			return (caseSensitive ? PrivacyUtils.CaseSensitiveTransform : PrivacyUtils.CaseInsensitiveTransform)(input);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003FA2 File Offset: 0x000021A2
		public static string ToOriginalString(this string input)
		{
			return input.RemovePrivateAndInternalMarkup();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00003FAC File Offset: 0x000021AC
		public static string ToTraceString(this Exception e)
		{
			string message = e.Message;
			string fullName = e.GetType().FullName;
			string text;
			if (message == null || message.Length <= 0)
			{
				text = fullName;
			}
			else
			{
				text = fullName + ": " + message.RemovePrivateInternalAndCustomMarkup(PrivacyUtils.CustomTagNames, null, null, null).MarkAsCustomerContent();
			}
			if (e.InnerException != null)
			{
				text = text + " ---> " + e.InnerException.ToTraceString() + "\r\n   --- End of inner exception stack trace ---";
			}
			if (e.StackTrace != null)
			{
				text = text + "\r\n" + e.StackTrace;
			}
			return text;
		}

		// Token: 0x040000A6 RID: 166
		private static Func<string, string> CaseInsensitiveTransform = (string input) => Obfuscation.Obfuscate(input, false);

		// Token: 0x040000A7 RID: 167
		private static Func<string, string> CaseSensitiveTransform = (string input) => Obfuscation.Obfuscate(input, true);

		// Token: 0x040000A8 RID: 168
		private static readonly string[] CustomTagNames = new string[] { "pii" };
	}
}
