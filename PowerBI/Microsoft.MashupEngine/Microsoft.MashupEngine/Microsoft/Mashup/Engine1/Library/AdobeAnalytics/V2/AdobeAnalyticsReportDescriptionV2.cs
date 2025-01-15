using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F92 RID: 3986
	internal abstract class AdobeAnalyticsReportDescriptionV2
	{
		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x060068EE RID: 26862 RVA: 0x00168738 File Offset: 0x00166938
		public IEnumerable<string> Dimensions
		{
			get
			{
				return from d in this.GetDimensions()
					where !AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(d)
					orderby d
					select d;
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x060068EF RID: 26863 RVA: 0x00168793 File Offset: 0x00166993
		public IEnumerable<string> GranularityLevels
		{
			get
			{
				return from d in this.GetDimensions()
					where AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(d)
					select d;
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x060068F0 RID: 26864
		public abstract IEnumerable<string> Metrics { get; }

		// Token: 0x060068F1 RID: 26865
		public abstract IEnumerable<IValueReference> GetReport(AdobeAnalyticsServiceV2 service, string companyId);

		// Token: 0x060068F2 RID: 26866
		protected abstract IEnumerable<string> GetDimensions();

		// Token: 0x060068F3 RID: 26867 RVA: 0x001687BF File Offset: 0x001669BF
		public static bool IsSegmentDimension(string dimension)
		{
			return dimension == AdobeAnalyticsReportDescriptionV2.SegmentDimension.Id;
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x001687D4 File Offset: 0x001669D4
		public static AdobeAnalyticsReportDescriptionV2 New(AdobeAnalyticsReportBuilder builder)
		{
			IEnumerable<string> enumerable = builder.Dimensions.Where((string d) => AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(d));
			IEnumerable<string> enumerable2 = from d in builder.Dimensions
				where !AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(d)
				orderby d
				select d;
			if (enumerable2.Contains(AdobeAnalyticsReportDescriptionV2.SegmentDimension.Id))
			{
				return new AdobeAnalyticsReportDescriptionV2.AdobeAnalyticsReportDescriptionV2Segment(builder);
			}
			IEnumerable<IValueReference> enumerable3 = builder.Segments.Select(new Func<string, RecordValue>(AdobeAnalyticsMetricFilterV2.NewSegmentFilter)).OfType<IValueReference>();
			RecordValue recordValue = AdobeAnalyticsMetricFilterV2.NewDateRangeFilter(builder.DateStart, builder.DateEnd);
			ListValue asList = ListValue.New(new Value[] { recordValue }).Concatenate(ListValue.New(enumerable3)).AsList;
			return new AdobeAnalyticsReportDescriptionV2.AdobeAnalyticsReportDescriptionV2NonSegment(builder.ReportSuiteId, enumerable.Concat(enumerable2), asList, builder.Measures, builder.DimensionToTop, 0, builder.Filter, builder.SortBy, new Dictionary<string, string>(), 0);
		}

		// Token: 0x040039D3 RID: 14803
		private const string RequestRsidKey = "rsid";

		// Token: 0x040039D4 RID: 14804
		private const string RequestSettingsKey = "settings";

		// Token: 0x040039D5 RID: 14805
		private const string RequestDimensioKey = "dimension";

		// Token: 0x040039D6 RID: 14806
		private const string RequestMetricsKey = "metricContainer";

		// Token: 0x040039D7 RID: 14807
		private const string RequestSearchKey = "search";

		// Token: 0x040039D8 RID: 14808
		private const string RequestGlobalFiltersKey = "globalFilters";

		// Token: 0x040039D9 RID: 14809
		private const string RequestSettingsPageSizeKey = "limit";

		// Token: 0x040039DA RID: 14810
		private const string RequestSettingsPageKey = "page";

		// Token: 0x040039DB RID: 14811
		private const string RequestSortKey = "dimensionSort";

		// Token: 0x040039DC RID: 14812
		private const string RequestMetricsMetricsKey = "metrics";

		// Token: 0x040039DD RID: 14813
		private const string RequestMetricsFilterKey = "metricFilters";

		// Token: 0x040039DE RID: 14814
		private const string RequestSearchClauseKey = "clause";

		// Token: 0x040039DF RID: 14815
		private const string ReportRowsKey = "rows";

		// Token: 0x040039E0 RID: 14816
		private const string ReportSummaryKey = "summaryData";

		// Token: 0x040039E1 RID: 14817
		private const string ReportTotalsKey = "filteredTotals";

		// Token: 0x040039E2 RID: 14818
		private const string ReportItemIdKey = "itemId";

		// Token: 0x040039E3 RID: 14819
		private const string ReportLastPageKey = "lastPage";

		// Token: 0x040039E4 RID: 14820
		private const string ReportRowValueKey = "value";

		// Token: 0x040039E5 RID: 14821
		private const string ReportRowDataKey = "data";

		// Token: 0x040039E6 RID: 14822
		private const string ReportRowBreakdownKey = "breakdown";

		// Token: 0x040039E7 RID: 14823
		private const int MaxPageSize = 50000;

		// Token: 0x040039E8 RID: 14824
		public static readonly AdobeAnalyticsDimension SegmentDimension = AdobeAnalyticsDimension.New("Segment", "SegmentDimension");

		// Token: 0x02000F93 RID: 3987
		private sealed class AdobeAnalyticsReportDescriptionV2NonSegment : AdobeAnalyticsReportDescriptionV2
		{
			// Token: 0x060068F7 RID: 26871 RVA: 0x00168910 File Offset: 0x00166B10
			public AdobeAnalyticsReportDescriptionV2NonSegment(string rsid, IEnumerable<string> dimensions, ListValue globalFilters, IEnumerable<string> metrics, IDictionary<string, int> dimensionToTop, int nextBreakdownFilterId, AdobeAnalyticsFilterExpression filter, string sortBy, IDictionary<string, string> dimensionBreakdownIds, int page = 0)
			{
				this.rsid = rsid;
				this.dimensions = dimensions;
				this.globalFilters = globalFilters;
				this.metrics = metrics;
				this.dimensionToTop = dimensionToTop;
				this.nextBreakdownFilterId = nextBreakdownFilterId;
				this.page = page;
				this.filter = filter;
				this.sortBy = sortBy;
				this.dimensionBreakdownIds = dimensionBreakdownIds;
			}

			// Token: 0x17001E46 RID: 7750
			// (get) Token: 0x060068F8 RID: 26872 RVA: 0x00168970 File Offset: 0x00166B70
			public override IEnumerable<string> Metrics
			{
				get
				{
					return this.metrics;
				}
			}

			// Token: 0x17001E47 RID: 7751
			// (get) Token: 0x060068F9 RID: 26873 RVA: 0x00168978 File Offset: 0x00166B78
			private string CurrentDimension
			{
				get
				{
					return this.dimensions.FirstOrDefault<string>();
				}
			}

			// Token: 0x17001E48 RID: 7752
			// (get) Token: 0x060068FA RID: 26874 RVA: 0x00168988 File Offset: 0x00166B88
			private int ResultsPerPage
			{
				get
				{
					string currentDimension = this.CurrentDimension;
					int num;
					if (currentDimension != null && this.dimensionToTop.TryGetValue(currentDimension, out num))
					{
						return Math.Min(num, 50000);
					}
					return 50000;
				}
			}

			// Token: 0x17001E49 RID: 7753
			// (get) Token: 0x060068FB RID: 26875 RVA: 0x001689C0 File Offset: 0x00166BC0
			private int ResultsToTake
			{
				get
				{
					int num;
					if (this.dimensionToTop.TryGetValue(this.CurrentDimension, out num))
					{
						return num - this.ResultsPerPage * this.page;
					}
					return int.MaxValue;
				}
			}

			// Token: 0x17001E4A RID: 7754
			// (get) Token: 0x060068FC RID: 26876 RVA: 0x001689F8 File Offset: 0x00166BF8
			private RecordValue MetricContainer
			{
				get
				{
					if (this.metricContainer == null)
					{
						IList<Value> list = new List<Value>();
						IList<Value> list2 = new List<Value>();
						int num = 0;
						int num2 = 0;
						foreach (string text in this.metrics)
						{
							IList<Value> list3 = new List<Value>();
							foreach (KeyValuePair<string, string> keyValuePair in this.dimensionBreakdownIds)
							{
								Value value = AdobeAnalyticsMetricFilterV2.NewBreakdownFilter(Convert.ToString(num2, CultureInfo.InvariantCulture), keyValuePair.Value, keyValuePair.Key);
								list3.Add(TextValue.New(Convert.ToString(num2, CultureInfo.InvariantCulture)));
								list.Add(value);
								num2++;
							}
							Value value2 = AdobeAnalyticsReportMetricV2.New(Convert.ToString(num, CultureInfo.InvariantCulture), text, ListValue.New(list3.ToArray<Value>()));
							list2.Add(value2);
						}
						this.metricContainer = RecordValue.New(Keys.New("metrics", "metricFilters"), new Value[]
						{
							ListValue.New(list2.ToArray<Value>()),
							ListValue.New(list.ToArray<Value>())
						});
					}
					return this.metricContainer;
				}
			}

			// Token: 0x060068FD RID: 26877 RVA: 0x00168B54 File Offset: 0x00166D54
			private Value RequestValue()
			{
				RecordBuilder recordBuilder = new RecordBuilder(8);
				recordBuilder.Add("rsid", TextValue.New(this.rsid), TypeValue.Text);
				recordBuilder.Add("globalFilters", this.globalFilters, TypeValue.Record);
				recordBuilder.Add("metricContainer", this.MetricContainer, TypeValue.Record);
				string currentDimension = this.CurrentDimension;
				if (currentDimension != null)
				{
					RecordBuilder recordBuilder2 = new RecordBuilder(2);
					recordBuilder2.Add("page", NumberValue.New(this.page), TypeValue.Number);
					recordBuilder2.Add("limit", NumberValue.New(this.ResultsPerPage), TypeValue.Number);
					recordBuilder.Add("settings", recordBuilder2.ToRecord(), TypeValue.Record);
					recordBuilder.Add("dimension", TextValue.New(currentDimension), TypeValue.Text);
					string text;
					if (this.filter != null && this.filter.TryGetClause(currentDimension, out text))
					{
						recordBuilder.Add("search", RecordValue.New(Keys.New("clause"), new Value[] { TextValue.New(text) }), TypeValue.Record);
					}
				}
				return recordBuilder.ToRecord();
			}

			// Token: 0x060068FE RID: 26878 RVA: 0x00168C7D File Offset: 0x00166E7D
			public override IEnumerable<IValueReference> GetReport(AdobeAnalyticsServiceV2 service, string companyId)
			{
				if (!this.dimensions.Any<string>())
				{
					return this.GetReportZeroDimensions(service, companyId);
				}
				return this.GetReportNonZeroDimensions(service, companyId);
			}

			// Token: 0x060068FF RID: 26879 RVA: 0x00168C9D File Offset: 0x00166E9D
			protected override IEnumerable<string> GetDimensions()
			{
				return this.dimensions;
			}

			// Token: 0x06006900 RID: 26880 RVA: 0x00168CA5 File Offset: 0x00166EA5
			private RecordValue GetReportResponse(AdobeAnalyticsServiceV2 service, string companyId)
			{
				return service.GetReport(AdobeAnalyticsRequestV2.NewReportRequest(service.ClientId, this.RequestValue(), companyId)).AsRecord;
			}

			// Token: 0x06006901 RID: 26881 RVA: 0x00168CC4 File Offset: 0x00166EC4
			private IEnumerable<IValueReference> GetReportZeroDimensions(AdobeAnalyticsServiceV2 service, string companyId)
			{
				RecordValue asRecord = this.GetReportResponse(service, companyId)["summaryData"].AsRecord;
				ListValue listValue;
				if (!asRecord.Keys.Contains("filteredTotals"))
				{
					listValue = ListValue.Empty;
				}
				else
				{
					listValue = asRecord["filteredTotals"].AsList;
				}
				yield return RecordValue.New(Keys.New("data"), new Value[] { listValue });
				yield break;
			}

			// Token: 0x06006902 RID: 26882 RVA: 0x00168CE2 File Offset: 0x00166EE2
			private IEnumerable<IValueReference> GetReportNonZeroDimensions(AdobeAnalyticsServiceV2 service, string companyId)
			{
				RecordValue responseValue = this.GetReportResponse(service, companyId);
				ListValue asList = responseValue["rows"].AsList;
				foreach (IValueReference valueReference in asList)
				{
					Value value = (Value)valueReference;
					yield return this.GetReportRows(value.AsRecord, service, companyId);
				}
				IEnumerator<IValueReference> enumerator = null;
				if (!responseValue["lastPage"].AsBoolean && this.ResultsPerPage < this.ResultsToTake)
				{
					AdobeAnalyticsReportDescriptionV2.AdobeAnalyticsReportDescriptionV2NonSegment adobeAnalyticsReportDescriptionV2NonSegment = new AdobeAnalyticsReportDescriptionV2.AdobeAnalyticsReportDescriptionV2NonSegment(this.rsid, this.dimensions, this.globalFilters, this.metrics, this.dimensionToTop, this.nextBreakdownFilterId, this.filter, this.sortBy, this.dimensionBreakdownIds, this.page + 1);
					foreach (IValueReference valueReference2 in adobeAnalyticsReportDescriptionV2NonSegment.GetReportNonZeroDimensions(service, companyId))
					{
						Value value2 = (Value)valueReference2;
						yield return value2;
					}
					enumerator = null;
				}
				yield break;
				yield break;
			}

			// Token: 0x06006903 RID: 26883 RVA: 0x00168D00 File Offset: 0x00166F00
			private Value GetReportRows(RecordValue dataRecord, AdobeAnalyticsServiceV2 service, string companyId)
			{
				Value value;
				dataRecord.TryGetValue("value", out value);
				if (AdobeAnalyticsDateGranularityHierarchyV2.IsGranularity(this.CurrentDimension))
				{
					value = NumberValue.New(AdobeAnalyticsDateGranularityHierarchyV2.GetNumberForDateString(this.CurrentDimension, value.AsString));
				}
				if (this.dimensions.Count<string>() == 1)
				{
					return RecordValue.New(Keys.New("value", "data"), new Value[]
					{
						value,
						dataRecord["data"]
					});
				}
				string asString = dataRecord["itemId"].AsText.AsString;
				int num = this.nextBreakdownFilterId;
				ListValue empty = ListValue.Empty;
				Value value2;
				if (this.metricContainer.TryGetValue("metricFilters", out value2))
				{
					ListValue asList = value2.AsList;
				}
				else
				{
					ListValue empty2 = ListValue.Empty;
				}
				IDictionary<string, string> dictionary = new Dictionary<string, string>(this.dimensionBreakdownIds);
				dictionary.Add(this.CurrentDimension, asString);
				Value value3 = ListValue.New(new AdobeAnalyticsReportDescriptionV2.AdobeAnalyticsReportDescriptionV2NonSegment(this.rsid, this.dimensions.Skip(1), this.globalFilters, this.metrics, this.dimensionToTop, num, this.filter, this.sortBy, dictionary, 0).GetReportNonZeroDimensions(service, companyId));
				return RecordValue.New(Keys.New("value", "breakdown"), new Value[] { value, value3 });
			}

			// Token: 0x040039E9 RID: 14825
			private readonly string rsid;

			// Token: 0x040039EA RID: 14826
			private readonly IEnumerable<string> dimensions;

			// Token: 0x040039EB RID: 14827
			private readonly IEnumerable<string> metrics;

			// Token: 0x040039EC RID: 14828
			private readonly IDictionary<string, string> dimensionBreakdownIds;

			// Token: 0x040039ED RID: 14829
			private readonly ListValue globalFilters;

			// Token: 0x040039EE RID: 14830
			private readonly AdobeAnalyticsFilterExpression filter;

			// Token: 0x040039EF RID: 14831
			private readonly string sortBy;

			// Token: 0x040039F0 RID: 14832
			private readonly int nextBreakdownFilterId;

			// Token: 0x040039F1 RID: 14833
			private readonly int page;

			// Token: 0x040039F2 RID: 14834
			private readonly IDictionary<string, int> dimensionToTop;

			// Token: 0x040039F3 RID: 14835
			private RecordValue metricContainer;
		}

		// Token: 0x02000F96 RID: 3990
		private sealed class AdobeAnalyticsReportDescriptionV2Segment : AdobeAnalyticsReportDescriptionV2
		{
			// Token: 0x06006916 RID: 26902 RVA: 0x0016926B File Offset: 0x0016746B
			public AdobeAnalyticsReportDescriptionV2Segment(AdobeAnalyticsReportBuilder builder)
			{
				this.builder = builder;
			}

			// Token: 0x17001E4F RID: 7759
			// (get) Token: 0x06006917 RID: 26903 RVA: 0x0016927A File Offset: 0x0016747A
			public override IEnumerable<string> Metrics
			{
				get
				{
					return this.builder.Measures;
				}
			}

			// Token: 0x06006918 RID: 26904 RVA: 0x00169287 File Offset: 0x00167487
			public override IEnumerable<IValueReference> GetReport(AdobeAnalyticsServiceV2 service, string companyId)
			{
				int segmentCount = this.builder.Segments.Count;
				int limit;
				if (!this.builder.DimensionToTop.TryGetValue(AdobeAnalyticsReportDescriptionV2.SegmentDimension.Id, out limit))
				{
					limit = int.MaxValue;
				}
				int count = 0;
				this.builder.Dimensions.Remove(AdobeAnalyticsReportDescriptionV2.SegmentDimension.Id);
				foreach (AdobeAnalyticsSegment adobeAnalyticsSegment in service.GetSegments(new AdobeAnalyticsCube(service, this.builder.ReportSuiteId, null, companyId)))
				{
					if (count >= limit)
					{
						break;
					}
					if (!this.builder.Segments.Contains(adobeAnalyticsSegment.Id))
					{
						int num = count;
						count = num + 1;
						this.builder.Segments.Add(adobeAnalyticsSegment.Id);
						IEnumerable<IValueReference> report = AdobeAnalyticsReportDescriptionV2.New(this.builder).GetReport(service, companyId);
						yield return (base.Dimensions.Count<string>() == 0) ? this.ProcessSubReportOnlyDimension(adobeAnalyticsSegment.Name, report) : this.ProcessSubReportOtherDimensions(adobeAnalyticsSegment.Name, report);
						this.builder.Segments.RemoveAt(segmentCount);
					}
				}
				IEnumerator<AdobeAnalyticsSegment> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06006919 RID: 26905 RVA: 0x001692A5 File Offset: 0x001674A5
			protected override IEnumerable<string> GetDimensions()
			{
				return this.builder.Dimensions;
			}

			// Token: 0x0600691A RID: 26906 RVA: 0x001692B2 File Offset: 0x001674B2
			private Value ProcessSubReportOnlyDimension(string segmentName, IEnumerable<IValueReference> rows)
			{
				return RecordValue.New(Keys.New("value", "data"), new Value[]
				{
					TextValue.New(segmentName),
					rows.First<IValueReference>().Value["data"]
				});
			}

			// Token: 0x0600691B RID: 26907 RVA: 0x001692EF File Offset: 0x001674EF
			private Value ProcessSubReportOtherDimensions(string segmentName, IEnumerable<IValueReference> rows)
			{
				return RecordValue.New(Keys.New("value", "breakdown"), new Value[]
				{
					TextValue.New(segmentName),
					ListValue.New(rows)
				});
			}

			// Token: 0x04003A06 RID: 14854
			private readonly AdobeAnalyticsReportBuilder builder;
		}
	}
}
