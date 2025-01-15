using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000008 RID: 8
	public static class ContainsMarkedInformationExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002070 File Offset: 0x00000270
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
