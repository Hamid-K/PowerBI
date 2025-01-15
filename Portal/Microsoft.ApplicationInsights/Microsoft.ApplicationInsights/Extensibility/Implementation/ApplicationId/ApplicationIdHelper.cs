using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.ApplicationInsights.Extensibility.Implementation.ApplicationId
{
	// Token: 0x020000C4 RID: 196
	internal static class ApplicationIdHelper
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x000177D6 File Offset: 0x000159D6
		internal static string ApplyFormatting(string applicationId)
		{
			applicationId = ApplicationIdHelper.EnforceMaxLength(applicationId, 50);
			if (string.IsNullOrWhiteSpace(applicationId))
			{
				return null;
			}
			applicationId = ApplicationIdHelper.SanitizeString(applicationId);
			return string.Format(CultureInfo.InvariantCulture, "cid-v1:{0}", new object[] { applicationId });
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00017810 File Offset: 0x00015A10
		internal static string SanitizeString(string input)
		{
			if (input == null)
			{
				return null;
			}
			for (int i = 0; i < input.Length; i++)
			{
				if (!ApplicationIdHelper.IsCharHeaderSafe(input[i]))
				{
					return Regex.Replace(input, "[^\\u0020-\\u007F]", string.Empty);
				}
			}
			return input;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00017855 File Offset: 0x00015A55
		private static string EnforceMaxLength(string input, int maxLength)
		{
			if (input != null && input.Length > maxLength)
			{
				input = input.Substring(0, maxLength);
			}
			return input;
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001786E File Offset: 0x00015A6E
		private static bool IsCharHeaderSafe(char ch)
		{
			return ch - ' ' <= '_';
		}

		// Token: 0x04000297 RID: 663
		private const string Format = "cid-v1:{0}";

		// Token: 0x04000298 RID: 664
		private const int ApplicationIdMaxLength = 50;
	}
}
