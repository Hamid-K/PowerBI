using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.AnalysisServices.MInterop
{
	// Token: 0x02000028 RID: 40
	public static class PrivateInformationUtility
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000494B File Offset: 0x00002B4B
		public static string MarkAsCustomerContent(this string plainString)
		{
			plainString = plainString ?? string.Empty;
			return PrivateInformationUtility.MarkWithTag(plainString, "ccon");
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004964 File Offset: 0x00002B64
		public static string MarkAsEUII(this string plainString)
		{
			plainString = plainString ?? string.Empty;
			return PrivateInformationUtility.MarkWithTag(plainString, "euii");
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004980 File Offset: 0x00002B80
		public static string RedactSensitiveStrings(this string plainString)
		{
			if (!string.IsNullOrEmpty(plainString))
			{
				Regex[] regexCredentials = PrivateInformationUtility.RegexCredentials;
				for (int i = 0; i < regexCredentials.Length; i++)
				{
					plainString = regexCredentials[i].Replace(plainString, "[CredentialRemoved]");
				}
			}
			return plainString;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000049BC File Offset: 0x00002BBC
		private static string MarkWithTag(string plainString, string tagName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<");
			stringBuilder.Append(tagName);
			stringBuilder.Append(">");
			stringBuilder.Append(plainString);
			stringBuilder.Append("</");
			stringBuilder.Append(tagName);
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x040000CB RID: 203
		private const string CustomerContentTagName = "ccon";

		// Token: 0x040000CC RID: 204
		private const string EUIITagName = "euii";

		// Token: 0x040000CD RID: 205
		private const string CredentialRemovedTag = "[CredentialRemoved]";

		// Token: 0x040000CE RID: 206
		private static readonly Regex[] RegexCredentials = new Regex[]
		{
			new Regex("sig=[a-zA-Z0-9-%_]+", RegexOptions.Compiled),
			new Regex("eyJ0eXAiOiJKV1Qi[a-zA-Z0-9-_]+?\\.eyJhdWQiOiJ[a-zA-Z0-9-_]+?\\.[a-zA-Z0-9-_]+?", RegexOptions.Compiled),
			new Regex("eyJ0eXAi[A-Za-z0-9-_.+/=$]+", RegexOptions.Compiled),
			new Regex("((pwd|password|secret|pass)\\s*(=|:|['\"]:).*?)(?=(\"[,} ])|<\\/ccon>|<\\/pi>|\\r\\n|\\n|$)", RegexOptions.Compiled)
		};
	}
}
