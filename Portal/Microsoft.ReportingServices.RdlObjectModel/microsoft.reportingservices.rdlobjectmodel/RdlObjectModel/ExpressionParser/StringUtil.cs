using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200023D RID: 573
	internal static class StringUtil
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x0002DA97 File Offset: 0x0002BC97
		internal static bool EqualsIgnoreCase(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0002DAA1 File Offset: 0x0002BCA1
		internal static bool EqualsCaseSensitive(string str1, string str2)
		{
			return string.Equals(str1, str2, StringComparison.Ordinal);
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0002DAAB File Offset: 0x0002BCAB
		internal static StringComparer CaseSensitiveComparer
		{
			get
			{
				return StringComparer.Ordinal;
			}
		}

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0002DAB2 File Offset: 0x0002BCB2
		internal static StringComparer CaseInsensitiveComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}
	}
}
