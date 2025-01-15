using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B14 RID: 2836
	internal sealed class GoogleAnalyticsQueryExpressionV2 : GoogleAnalyticsQueryExpression
	{
		// Token: 0x06004E99 RID: 20121 RVA: 0x0010486C File Offset: 0x00102A6C
		public GoogleAnalyticsQueryExpressionV2(IGoogleAnalyticsCube cube, IList<GoogleAnalyticsExpression> measures, IList<GoogleAnalyticsExpression> dimensions, GoogleAnalyticsFilterExpression[] cnfFilter, bool shouldFilterLocally, CubeExpression filterExpression, IList<GoogleAnalyticsExpression> sorts, RowRange range, DateTime startDate, DateTime endDate, bool isFalse)
			: base(cube, measures, dimensions, shouldFilterLocally, filterExpression, sorts, range, startDate, endDate, isFalse)
		{
			this.cnfFilter = cnfFilter;
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x00104898 File Offset: 0x00102A98
		private Value CreateDateRangeV2()
		{
			return RecordValue.New(Keys.New("startDate", "endDate"), new Value[]
			{
				TextValue.New(this.startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
				TextValue.New(this.endDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x001048FF File Offset: 0x00102AFF
		private IEnumerable<IValueReference> CreateDimensionsV2()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.dimensions)
			{
				yield return RecordValue.New(Keys.New("name"), new Value[] { TextValue.New(googleAnalyticsExpression.QueryString) });
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0010490F File Offset: 0x00102B0F
		private IEnumerable<IValueReference> CreateMetricsV2()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.measures)
			{
				yield return RecordValue.New(Keys.New("name"), new Value[] { TextValue.New(googleAnalyticsExpression.QueryString) });
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0010491F File Offset: 0x00102B1F
		private IEnumerable<IValueReference> CreateOrderBysV2()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.sorts)
			{
				GoogleAnalyticsSortOrder googleAnalyticsSortOrder = googleAnalyticsExpression as GoogleAnalyticsSortOrder;
				if (googleAnalyticsSortOrder != null)
				{
					GoogleAnalyticsIdentifierExpression identifierExpression = googleAnalyticsSortOrder.IdentifierExpression;
					bool flag = !googleAnalyticsSortOrder.Ascending;
					if (identifierExpression.ColumnKind == GoogleAnalyticsCubeObjectKind.Dimension)
					{
						yield return RecordValue.New(Keys.New("desc", "dimension"), new Value[]
						{
							LogicalValue.New(flag),
							RecordValue.New(Keys.New("dimensionName", "orderType"), new Value[]
							{
								TextValue.New(identifierExpression.Identifier),
								TextValue.New("ALPHANUMERIC")
							})
						});
					}
					else
					{
						yield return RecordValue.New(Keys.New("desc", "metric"), new Value[]
						{
							LogicalValue.New(flag),
							RecordValue.New(Keys.New("metricName"), new Value[] { TextValue.New(identifierExpression.Identifier) })
						});
					}
				}
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x00104930 File Offset: 0x00102B30
		private RecordValue CreateFiltersV2()
		{
			if (this.cnfFilter.Length == 0)
			{
				return RecordValue.Empty;
			}
			IList<IValueReference> list = new List<IValueReference>();
			IList<IValueReference> list2 = new List<IValueReference>();
			foreach (GoogleAnalyticsFilterExpression googleAnalyticsFilterExpression in this.cnfFilter)
			{
				if (googleAnalyticsFilterExpression.ColumnKind == GoogleAnalyticsCubeObjectKind.Dimension)
				{
					list.Add(googleAnalyticsFilterExpression.V2Filter);
				}
				else
				{
					list2.Add(googleAnalyticsFilterExpression.V2Filter);
				}
			}
			RecordValue recordValue = RecordValue.Empty;
			if (list.Count > 0)
			{
				recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("dimensionFilter"), new Value[] { this.WrapFilterList(list) })).AsRecord;
			}
			if (list2.Count > 0)
			{
				recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("metricFilter"), new Value[] { this.WrapFilterList(list2) })).AsRecord;
			}
			return recordValue;
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x00104A0C File Offset: 0x00102C0C
		private Value WrapFilterList(IList<IValueReference> filters)
		{
			return RecordValue.New(Keys.New("andGroup"), new Value[] { RecordValue.New(Keys.New("expressions"), new Value[] { ListValue.New(filters) }) });
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x00104A50 File Offset: 0x00102C50
		protected override RecordValue CreateBody()
		{
			return RecordValue.New(Keys.New(new string[] { "dateRanges", "dimensions", "metrics", "orderBys", "offset", "limit", "keepEmptyRows" }), new Value[]
			{
				ListValue.New(new Value[] { this.CreateDateRangeV2() }),
				ListValue.New(this.CreateDimensionsV2()),
				ListValue.New(this.CreateMetricsV2()),
				ListValue.New(this.CreateOrderBysV2()),
				TextValue.New(this.range.SkipCount.Value.ToString(CultureInfo.InvariantCulture)),
				TextValue.New((this.range.TakeCount.IsInfinite ? 100000L : this.range.TakeCount.Value).ToString(CultureInfo.InvariantCulture)),
				LogicalValue.True
			}).Concatenate(this.CreateFiltersV2()).AsRecord;
		}

		// Token: 0x04002A4B RID: 10827
		private const string DateQueryFormat = "yyyy-MM-dd";

		// Token: 0x04002A4C RID: 10828
		private readonly GoogleAnalyticsFilterExpression[] cnfFilter;
	}
}
