using System;
using System.Globalization;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F8D RID: 3981
	internal class AdobeAnalyticsMetricFilterV2
	{
		// Token: 0x060068D7 RID: 26839 RVA: 0x00168218 File Offset: 0x00166418
		public static RecordValue NewDateRangeFilter(string start, string end)
		{
			return RecordValue.New(Keys.New("type", "dateRange"), new Value[]
			{
				TextValue.New("dateRange"),
				TextValue.New(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", start, end))
			});
		}

		// Token: 0x060068D8 RID: 26840 RVA: 0x00168268 File Offset: 0x00166468
		public static RecordValue NewBreakdownFilter(string filterId, string itemId, string dimension)
		{
			return RecordValue.New(Keys.New("id", "type", "itemId", "dimension"), new Value[]
			{
				TextValue.New(filterId),
				TextValue.New("breakdown"),
				TextValue.New(itemId),
				TextValue.New(dimension)
			});
		}

		// Token: 0x060068D9 RID: 26841 RVA: 0x001682C1 File Offset: 0x001664C1
		public static RecordValue NewSegmentFilter(string segmentId)
		{
			return RecordValue.New(Keys.New("type", "segmentId"), new Value[]
			{
				TextValue.New("segment"),
				TextValue.New(segmentId)
			});
		}

		// Token: 0x040039BE RID: 14782
		private const string FilterIdKey = "id";

		// Token: 0x040039BF RID: 14783
		private const string TypeKey = "type";

		// Token: 0x040039C0 RID: 14784
		private const string DateRangeKey = "dateRange";

		// Token: 0x040039C1 RID: 14785
		private const string ItemIdKey = "itemId";

		// Token: 0x040039C2 RID: 14786
		private const string DimensionKey = "dimension";

		// Token: 0x040039C3 RID: 14787
		private const string SegmentIdKey = "segmentId";

		// Token: 0x040039C4 RID: 14788
		private const string DateRangeType = "dateRange";

		// Token: 0x040039C5 RID: 14789
		private const string BreakdownType = "breakdown";

		// Token: 0x040039C6 RID: 14790
		private const string SegmentType = "segment";
	}
}
