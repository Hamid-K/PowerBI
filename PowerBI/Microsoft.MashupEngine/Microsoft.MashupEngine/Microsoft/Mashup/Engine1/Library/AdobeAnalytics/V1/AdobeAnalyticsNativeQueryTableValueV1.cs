using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1
{
	// Token: 0x02000FA9 RID: 4009
	internal sealed class AdobeAnalyticsNativeQueryTableValueV1 : TableValue
	{
		// Token: 0x06006972 RID: 26994 RVA: 0x0016A6E7 File Offset: 0x001688E7
		public AdobeAnalyticsNativeQueryTableValueV1(TextValue nativeQuery, AdobeAnalyticsServiceV1 service)
		{
			this.nativeQuery = nativeQuery;
			this.service = service;
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06006973 RID: 26995 RVA: 0x0016A700 File Offset: 0x00168900
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					RecordBuilder recordBuilder = new RecordBuilder(this.GranularityLevels.Count + this.Dimensions.Count + this.Measures.Count);
					foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in this.GranularityLevels)
					{
						recordBuilder.Add(adobeAnalyticsDimension.Name, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Number,
							LogicalValue.False
						}), TypeValue.Record);
					}
					foreach (AdobeAnalyticsDimension adobeAnalyticsDimension2 in this.Dimensions)
					{
						recordBuilder.Add(adobeAnalyticsDimension2.Name, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Text,
							LogicalValue.False
						}), TypeValue.Record);
					}
					foreach (AdobeAnalyticsMeasure adobeAnalyticsMeasure in this.Measures)
					{
						recordBuilder.Add(adobeAnalyticsMeasure.Name, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Number,
							LogicalValue.False
						}), TypeValue.Record);
					}
					this.type = TableTypeValue.New(RecordTypeValue.New(recordBuilder.ToRecord()));
				}
				return this.type;
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06006974 RID: 26996 RVA: 0x0016A8A0 File Offset: 0x00168AA0
		private Value Result
		{
			get
			{
				if (this.result == null)
				{
					this.result = this.service.GetReport(AdobeAnalyticsRequestV1.NewReportRequest(this.nativeQuery, string.Empty), null);
				}
				return this.result;
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06006975 RID: 26997 RVA: 0x0016A8D4 File Offset: 0x00168AD4
		private IList<AdobeAnalyticsDimension> Dimensions
		{
			get
			{
				if (this.dimensions == null)
				{
					this.dimensions = new List<AdobeAnalyticsDimension>();
					Value value;
					if (this.Result["report"].AsRecord.TryGetValue("elements", out value))
					{
						foreach (IValueReference valueReference in value.AsList)
						{
							Value value2 = (Value)valueReference;
							if (value2["id"].AsString != "datetime")
							{
								this.dimensions.Add(AdobeAnalyticsDimension.New(value2["name"].AsString, value2["id"].AsString));
							}
						}
					}
				}
				return this.dimensions;
			}
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x06006976 RID: 26998 RVA: 0x0016A9AC File Offset: 0x00168BAC
		private IList<AdobeAnalyticsMeasure> Measures
		{
			get
			{
				if (this.measures == null)
				{
					this.measures = new List<AdobeAnalyticsMeasure>();
					Value value;
					if (this.Result["report"].AsRecord.TryGetValue("metrics", out value))
					{
						foreach (IValueReference valueReference in value.AsList)
						{
							Value value2 = (Value)valueReference;
							this.measures.Add(AdobeAnalyticsMeasure.New(value2["name"].AsString, value2["id"].AsString));
						}
					}
				}
				return this.measures;
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06006977 RID: 26999 RVA: 0x0016AA68 File Offset: 0x00168C68
		private IList<AdobeAnalyticsDimension> GranularityLevels
		{
			get
			{
				if (this.granularityLevels == null)
				{
					this.granularityLevels = new List<AdobeAnalyticsDimension>();
					Value value;
					if (this.Result["report"].AsRecord.TryGetValue("data", out value))
					{
						Value value2 = value.AsList[0];
						foreach (AdobeAnalyticsDimension adobeAnalyticsDimension in AdobeAnalyticsDateGranularityHierarchyV1.Hierarchy)
						{
							Value value3;
							if (value2.AsRecord.TryGetValue(adobeAnalyticsDimension.Id, out value3))
							{
								this.granularityLevels.Add(adobeAnalyticsDimension);
							}
						}
					}
				}
				return this.granularityLevels;
			}
		}

		// Token: 0x06006978 RID: 27000 RVA: 0x0016AAFA File Offset: 0x00168CFA
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new AdobeAnalyticsNativeQueryTableValueV1.AdobeAnalyticsNativeQueryEnumerator(this);
		}

		// Token: 0x04003A3E RID: 14910
		private readonly TextValue nativeQuery;

		// Token: 0x04003A3F RID: 14911
		private readonly AdobeAnalyticsServiceV1 service;

		// Token: 0x04003A40 RID: 14912
		private Value result;

		// Token: 0x04003A41 RID: 14913
		private IList<AdobeAnalyticsDimension> dimensions;

		// Token: 0x04003A42 RID: 14914
		private IList<AdobeAnalyticsMeasure> measures;

		// Token: 0x04003A43 RID: 14915
		private IList<AdobeAnalyticsDimension> granularityLevels;

		// Token: 0x04003A44 RID: 14916
		private TypeValue type;

		// Token: 0x02000FAA RID: 4010
		private class AdobeAnalyticsNativeQueryEnumerator : AdobeAnalyticsResultEnumeratorV1
		{
			// Token: 0x06006979 RID: 27001 RVA: 0x0016AB02 File Offset: 0x00168D02
			public AdobeAnalyticsNativeQueryEnumerator(AdobeAnalyticsNativeQueryTableValueV1 tableValue)
			{
				this.tableValue = tableValue;
			}

			// Token: 0x0600697A RID: 27002 RVA: 0x0016AB11 File Offset: 0x00168D11
			protected override Value GetResult()
			{
				return this.tableValue.Result;
			}

			// Token: 0x0600697B RID: 27003 RVA: 0x0016AB1E File Offset: 0x00168D1E
			protected override IList<string> GetGranularityLevels()
			{
				return this.tableValue.GranularityLevels.Select((AdobeAnalyticsDimension l) => l.Id).ToList<string>();
			}

			// Token: 0x0600697C RID: 27004 RVA: 0x0016AB54 File Offset: 0x00168D54
			protected override IList<string> GetDimensions()
			{
				return this.tableValue.Dimensions.Select((AdobeAnalyticsDimension d) => d.Id).ToList<string>();
			}

			// Token: 0x0600697D RID: 27005 RVA: 0x0016AB8A File Offset: 0x00168D8A
			protected override IList<string> GetMeasures()
			{
				return this.tableValue.Measures.Select((AdobeAnalyticsMeasure m) => m.Id).ToList<string>();
			}

			// Token: 0x0600697E RID: 27006 RVA: 0x0016ABC0 File Offset: 0x00168DC0
			protected override Keys GetKeys()
			{
				return Keys.New(this.tableValue.GranularityLevels.Select((AdobeAnalyticsDimension l) => l.Id).Concat(this.tableValue.Dimensions.Select((AdobeAnalyticsDimension d) => d.Id)).Concat(this.tableValue.Measures.Select((AdobeAnalyticsMeasure m) => m.Id))
					.ToArray<string>());
			}

			// Token: 0x04003A45 RID: 14917
			private AdobeAnalyticsNativeQueryTableValueV1 tableValue;
		}
	}
}
