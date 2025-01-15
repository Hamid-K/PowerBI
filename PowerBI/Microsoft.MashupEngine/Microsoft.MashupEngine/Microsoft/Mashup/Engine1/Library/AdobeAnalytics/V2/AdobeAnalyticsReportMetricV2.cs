using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F99 RID: 3993
	internal class AdobeAnalyticsReportMetricV2
	{
		// Token: 0x0600692D RID: 26925 RVA: 0x00169606 File Offset: 0x00167806
		public static RecordValue New(string columnId, string metricId)
		{
			return RecordValue.New(Keys.New("columnId", "id"), new Value[]
			{
				TextValue.New(columnId),
				TextValue.New(metricId)
			});
		}

		// Token: 0x0600692E RID: 26926 RVA: 0x00169634 File Offset: 0x00167834
		public static RecordValue New(string columnid, string metricId, ListValue filters)
		{
			return RecordValue.New(Keys.New("columnId", "id", "filters"), new Value[]
			{
				TextValue.New(columnid),
				TextValue.New(metricId),
				filters
			});
		}

		// Token: 0x04003A1A RID: 14874
		public const string ColumnIdKey = "columnId";

		// Token: 0x04003A1B RID: 14875
		public const string MetricIdKey = "id";

		// Token: 0x04003A1C RID: 14876
		public const string FiltersKey = "filters";
	}
}
