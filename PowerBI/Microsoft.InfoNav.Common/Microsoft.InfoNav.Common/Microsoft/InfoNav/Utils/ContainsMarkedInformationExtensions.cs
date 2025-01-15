using System;

namespace Microsoft.InfoNav.Utils
{
	// Token: 0x02000030 RID: 48
	public static class ContainsMarkedInformationExtensions
	{
		// Token: 0x0600023D RID: 573 RVA: 0x000070F1 File Offset: 0x000052F1
		public static string ToMarkedString(this IContainsMarkedInformation markedInfo)
		{
			if (markedInfo == null)
			{
				return string.Empty;
			}
			return markedInfo.BuildMarkedString();
		}
	}
}
