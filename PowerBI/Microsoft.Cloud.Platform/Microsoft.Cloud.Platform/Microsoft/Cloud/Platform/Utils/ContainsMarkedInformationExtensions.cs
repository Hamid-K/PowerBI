using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200022C RID: 556
	public static class ContainsMarkedInformationExtensions
	{
		// Token: 0x06000E8A RID: 3722 RVA: 0x000330D9 File Offset: 0x000312D9
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
