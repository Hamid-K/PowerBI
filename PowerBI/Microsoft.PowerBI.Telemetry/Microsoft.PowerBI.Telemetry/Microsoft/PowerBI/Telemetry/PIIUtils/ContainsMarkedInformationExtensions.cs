using System;

namespace Microsoft.PowerBI.Telemetry.PIIUtils
{
	// Token: 0x02000035 RID: 53
	public static class ContainsMarkedInformationExtensions
	{
		// Token: 0x06000135 RID: 309 RVA: 0x000048B2 File Offset: 0x00002AB2
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
