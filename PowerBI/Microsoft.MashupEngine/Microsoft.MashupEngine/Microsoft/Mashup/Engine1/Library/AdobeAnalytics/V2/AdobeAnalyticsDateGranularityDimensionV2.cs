using System;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F89 RID: 3977
	internal class AdobeAnalyticsDateGranularityDimensionV2 : AdobeAnalyticsDimension
	{
		// Token: 0x060068C4 RID: 26820 RVA: 0x00167E95 File Offset: 0x00166095
		public AdobeAnalyticsDateGranularityDimensionV2(string name, string id, Func<string, int> numberParser)
			: base(name, id)
		{
			this.numberParser = numberParser;
		}

		// Token: 0x060068C5 RID: 26821 RVA: 0x00167EA6 File Offset: 0x001660A6
		public int GetNumber(string dateText)
		{
			return this.numberParser(dateText);
		}

		// Token: 0x040039B6 RID: 14774
		private readonly Func<string, int> numberParser;
	}
}
