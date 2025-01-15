using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FAD RID: 4013
	internal class AdobeAnalyticsReportDescriptionV1
	{
		// Token: 0x0600698B RID: 27019 RVA: 0x0016AD88 File Offset: 0x00168F88
		public AdobeAnalyticsReportDescriptionV1(string reportSuiteId, IList<string> measures, IList<string> dimensions, IDictionary<string, int> dimensionToTop, IList<string> segments, string dateStart, string dateEnd, string sortBy, AdobeAnalyticsFilterExpression filter, long? skip)
		{
			this.reportSuiteId = reportSuiteId;
			this.measures = measures;
			this.dimensions = dimensions;
			this.dimensionToTop = dimensionToTop;
			this.segments = segments;
			this.dateStart = dateStart;
			this.dateEnd = dateEnd;
			this.sortBy = sortBy;
			this.filterExpression = filter;
			this.skip = skip;
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x0600698C RID: 27020 RVA: 0x0016ADE8 File Offset: 0x00168FE8
		public IEnumerable<string> GranularityLevels
		{
			get
			{
				return this.dimensions.Where((string d) => AdobeAnalyticsDateGranularityHierarchyV1.IsGranularity(d));
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x0600698D RID: 27021 RVA: 0x0016AE14 File Offset: 0x00169014
		public IEnumerable<string> Dimensions
		{
			get
			{
				return from d in this.dimensions
					where !AdobeAnalyticsDateGranularityHierarchyV1.IsGranularity(d)
					orderby d
					select d;
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x0016AE6F File Offset: 0x0016906F
		public IEnumerable<string> Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x17001E5D RID: 7773
		// (get) Token: 0x0600698F RID: 27023 RVA: 0x0016AE78 File Offset: 0x00169078
		public Value Value
		{
			get
			{
				AdobeAnalyticsReportDescriptionV1.<>c__DisplayClass27_0 CS$<>8__locals1 = new AdobeAnalyticsReportDescriptionV1.<>c__DisplayClass27_0();
				CS$<>8__locals1.<>4__this = this;
				if (this.value == null)
				{
					Value value = TextValue.New(this.reportSuiteId);
					AdobeAnalyticsReportDescriptionV1.<>c__DisplayClass27_0 CS$<>8__locals2 = CS$<>8__locals1;
					IDictionary<string, Value> dictionary2;
					if (this.filterExpression == null)
					{
						IDictionary<string, Value> dictionary = new Dictionary<string, Value>();
						dictionary2 = dictionary;
					}
					else
					{
						dictionary2 = this.filterExpression.GetFilterRecords();
					}
					CS$<>8__locals2.dimToFilter = dictionary2;
					ListValue listValue = ListValue.New((from d in this.dimensions
						where !AdobeAnalyticsDateGranularityHierarchyV1.IsGranularity(d)
						orderby d
						select d).Select(delegate(string d)
					{
						Value value2 = RecordValue.New(AdobeAnalyticsReportDescriptionV1.IdKey, new Value[] { TextValue.New(d) });
						Value value3;
						if (CS$<>8__locals1.dimToFilter.TryGetValue(d, out value3))
						{
							value2 = value2.Concatenate(value3);
						}
						int num;
						if (CS$<>8__locals1.<>4__this.dimensionToTop.TryGetValue(d, out num))
						{
							value2 = value2.Concatenate(RecordValue.New(AdobeAnalyticsReportDescriptionV1.TopKey, new Value[] { NumberValue.New(num) }));
						}
						return value2;
					}));
					ListValue listValue2 = ListValue.New(this.measures.Select((string d) => RecordValue.New(AdobeAnalyticsReportDescriptionV1.IdKey, new Value[] { TextValue.New(d) })));
					RecordValue recordValue = RecordValue.New(AdobeAnalyticsReportDescriptionV1.DescriptionKeys, new Value[]
					{
						value,
						listValue,
						listValue2,
						TextValue.New("subrelation")
					});
					if (this.dateStart != null && this.dateEnd != null)
					{
						recordValue = recordValue.Concatenate(RecordValue.New(AdobeAnalyticsReportDescriptionV1.DateRangeKeys, new Value[]
						{
							TextValue.New(this.dateStart),
							TextValue.New(this.dateEnd)
						})).AsRecord;
					}
					string finestGranularity = AdobeAnalyticsDateGranularityHierarchyV1.GetFinestGranularity(this.dimensions);
					if (finestGranularity != null)
					{
						recordValue = recordValue.Concatenate(RecordValue.New(AdobeAnalyticsReportDescriptionV1.DateGranularityKey, new Value[] { TextValue.New(finestGranularity) })).AsRecord;
					}
					if (this.sortBy != null)
					{
						recordValue = recordValue.Concatenate(RecordValue.New(AdobeAnalyticsReportDescriptionV1.SortByKey, new Value[] { TextValue.New(this.sortBy) })).AsRecord;
					}
					if (this.segments.Count > 0)
					{
						ListValue listValue3 = ListValue.New(this.segments.Select((string s) => RecordValue.New(AdobeAnalyticsReportDescriptionV1.IdKey, new Value[] { TextValue.New(s) })));
						recordValue = recordValue.Concatenate(RecordValue.New(AdobeAnalyticsReportDescriptionV1.SegmentsKey, new Value[] { listValue3 })).AsRecord;
					}
					this.value = RecordValue.New(AdobeAnalyticsReportDescriptionV1.ReportDescriptionKey, new Value[] { recordValue });
				}
				return this.value;
			}
		}

		// Token: 0x04003A4D RID: 14925
		private string reportSuiteId;

		// Token: 0x04003A4E RID: 14926
		private IList<string> measures;

		// Token: 0x04003A4F RID: 14927
		private IList<string> dimensions;

		// Token: 0x04003A50 RID: 14928
		private IDictionary<string, int> dimensionToTop;

		// Token: 0x04003A51 RID: 14929
		private IList<string> segments;

		// Token: 0x04003A52 RID: 14930
		private string dateStart;

		// Token: 0x04003A53 RID: 14931
		private string dateEnd;

		// Token: 0x04003A54 RID: 14932
		private string sortBy;

		// Token: 0x04003A55 RID: 14933
		private AdobeAnalyticsFilterExpression filterExpression;

		// Token: 0x04003A56 RID: 14934
		private long? skip;

		// Token: 0x04003A57 RID: 14935
		private Value value;

		// Token: 0x04003A58 RID: 14936
		private static readonly Keys IdKey = Keys.New("id");

		// Token: 0x04003A59 RID: 14937
		private static readonly Keys DescriptionKeys = Keys.New("reportSuiteID", "elements", "metrics", "reportDescriptionBreakdownType");

		// Token: 0x04003A5A RID: 14938
		private static readonly Keys DateRangeKeys = Keys.New("dateFrom", "dateTo");

		// Token: 0x04003A5B RID: 14939
		private static readonly Keys ReportDescriptionKey = Keys.New("reportDescription");

		// Token: 0x04003A5C RID: 14940
		private static readonly Keys DateGranularityKey = Keys.New("dateGranularity");

		// Token: 0x04003A5D RID: 14941
		private static readonly Keys SortByKey = Keys.New("sortBy");

		// Token: 0x04003A5E RID: 14942
		private static readonly Keys TopKey = Keys.New("top");

		// Token: 0x04003A5F RID: 14943
		private static readonly Keys SegmentsKey = Keys.New("segments");
	}
}
