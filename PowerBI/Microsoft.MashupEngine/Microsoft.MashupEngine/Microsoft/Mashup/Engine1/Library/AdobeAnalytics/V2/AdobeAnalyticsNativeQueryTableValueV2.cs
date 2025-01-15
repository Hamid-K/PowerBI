using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2
{
	// Token: 0x02000F8E RID: 3982
	internal sealed class AdobeAnalyticsNativeQueryTableValueV2 : TableValue
	{
		// Token: 0x060068DB RID: 26843 RVA: 0x001682F3 File Offset: 0x001664F3
		public AdobeAnalyticsNativeQueryTableValueV2(TextValue nativeQuery, AdobeAnalyticsServiceV2 service, string companyId)
		{
			this.nativeQuery = nativeQuery;
			this.service = service;
			this.companyId = companyId;
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x060068DC RID: 26844 RVA: 0x00168310 File Offset: 0x00166510
		public override TypeValue Type
		{
			get
			{
				if (this.type == null)
				{
					RecordBuilder recordBuilder;
					if (this.Dimension == null)
					{
						recordBuilder = new RecordBuilder(this.MetricColumns.Count);
					}
					else
					{
						recordBuilder = new RecordBuilder(this.MetricColumns.Count + 1);
						recordBuilder.Add(this.dimension, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Text,
							LogicalValue.False
						}), TypeValue.Record);
					}
					foreach (string text in this.MetricColumns)
					{
						recordBuilder.Add(text, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
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

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x060068DD RID: 26845 RVA: 0x00168410 File Offset: 0x00166610
		private Value Result
		{
			get
			{
				if (this.result == null)
				{
					this.result = this.service.GetReport(AdobeAnalyticsRequestV2.NewReportRequest(this.service.ClientId, this.nativeQuery, this.companyId));
				}
				return this.result;
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x060068DE RID: 26846 RVA: 0x0016844D File Offset: 0x0016664D
		private string Dimension
		{
			get
			{
				this.EnsureInitialized();
				return this.dimension;
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x060068DF RID: 26847 RVA: 0x0016845B File Offset: 0x0016665B
		private IList<string> MetricColumns
		{
			get
			{
				this.EnsureInitialized();
				return this.metricColumns;
			}
		}

		// Token: 0x060068E0 RID: 26848 RVA: 0x0016846C File Offset: 0x0016666C
		private void EnsureInitialized()
		{
			if (!this.isInitialized)
			{
				Value value = this.Result["columns"];
				Value value2;
				if (value.TryGetValue("dimension", out value2))
				{
					this.dimension = value2["id"].AsString;
				}
				ListValue asList = value["columnIds"].AsList;
				this.metricColumns = asList.Select((IValueReference v) => v.Value.AsString).ToList<string>();
				this.isInitialized = true;
			}
		}

		// Token: 0x060068E1 RID: 26849 RVA: 0x001684FD File Offset: 0x001666FD
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new AdobeAnalyticsNativeQueryTableValueV2.AdobeAnalyticsNativeQueryEnumerator(this);
		}

		// Token: 0x040039C7 RID: 14791
		private readonly string companyId;

		// Token: 0x040039C8 RID: 14792
		private readonly TextValue nativeQuery;

		// Token: 0x040039C9 RID: 14793
		private readonly AdobeAnalyticsServiceV2 service;

		// Token: 0x040039CA RID: 14794
		private Value result;

		// Token: 0x040039CB RID: 14795
		private string dimension;

		// Token: 0x040039CC RID: 14796
		private IList<string> metricColumns;

		// Token: 0x040039CD RID: 14797
		private TypeValue type;

		// Token: 0x040039CE RID: 14798
		private bool isInitialized;

		// Token: 0x02000F8F RID: 3983
		private class AdobeAnalyticsNativeQueryEnumerator : AdobeAnalyticsResultEnumeratorV2
		{
			// Token: 0x060068E2 RID: 26850 RVA: 0x00168505 File Offset: 0x00166705
			public AdobeAnalyticsNativeQueryEnumerator(AdobeAnalyticsNativeQueryTableValueV2 tableValue)
			{
				this.tableValue = tableValue;
			}

			// Token: 0x060068E3 RID: 26851 RVA: 0x00168514 File Offset: 0x00166714
			protected override Value GetResult()
			{
				return this.tableValue.Result["rows"];
			}

			// Token: 0x060068E4 RID: 26852 RVA: 0x0016852C File Offset: 0x0016672C
			protected override IList<string> GetDimensions()
			{
				IList<string> list = new List<string>();
				if (this.tableValue.Dimension != null)
				{
					list.Add(this.tableValue.Dimension);
				}
				return list;
			}

			// Token: 0x060068E5 RID: 26853 RVA: 0x0016855E File Offset: 0x0016675E
			protected override IList<string> GetMeasures()
			{
				return this.tableValue.MetricColumns;
			}

			// Token: 0x060068E6 RID: 26854 RVA: 0x0016856C File Offset: 0x0016676C
			protected override Keys GetKeys()
			{
				IList<string> list = new List<string>();
				if (this.tableValue.Dimension != null)
				{
					list.Add(this.tableValue.Dimension);
				}
				return Keys.New(list.Concat(this.tableValue.MetricColumns).ToArray<string>());
			}

			// Token: 0x040039CF RID: 14799
			private readonly AdobeAnalyticsNativeQueryTableValueV2 tableValue;
		}
	}
}
