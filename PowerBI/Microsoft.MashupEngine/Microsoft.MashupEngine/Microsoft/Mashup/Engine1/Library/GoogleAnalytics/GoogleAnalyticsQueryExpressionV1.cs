using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B10 RID: 2832
	internal sealed class GoogleAnalyticsQueryExpressionV1 : GoogleAnalyticsQueryExpression
	{
		// Token: 0x06004E77 RID: 20087 RVA: 0x00104084 File Offset: 0x00102284
		public GoogleAnalyticsQueryExpressionV1(IGoogleAnalyticsCube cube, IList<GoogleAnalyticsExpression> measures, IList<GoogleAnalyticsExpression> dimensions, string filter, bool shouldFilterLocally, CubeExpression filterExpression, IList<GoogleAnalyticsExpression> sorts, RowRange range, DateTime startDate, DateTime endDate, bool isFalse)
			: base(cube, measures, dimensions, shouldFilterLocally, filterExpression, sorts, range, startDate, endDate, isFalse)
		{
			this.filter = filter;
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x001040B0 File Offset: 0x001022B0
		protected override RecordValue CreateBody()
		{
			return RecordValue.New(Keys.New("reportRequests"), new Value[] { ListValue.New(new Value[] { this.CreateReportBody() }) });
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x001040E0 File Offset: 0x001022E0
		private Value CreateDateRange()
		{
			return RecordValue.New(Keys.New("startDate", "endDate"), new Value[]
			{
				TextValue.New(this.startDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
				TextValue.New(this.endDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
			});
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x00104147 File Offset: 0x00102347
		private IEnumerable<IValueReference> CreateDimensions()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.dimensions)
			{
				yield return RecordValue.New(Keys.New("name"), new Value[] { TextValue.New(googleAnalyticsExpression.QueryString) });
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x00104157 File Offset: 0x00102357
		private IEnumerable<IValueReference> CreateMetrics()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.measures)
			{
				yield return RecordValue.New(Keys.New("expression"), new Value[] { TextValue.New(googleAnalyticsExpression.QueryString) });
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x00104167 File Offset: 0x00102367
		private IEnumerable<IValueReference> CreateOrderBys()
		{
			foreach (GoogleAnalyticsExpression googleAnalyticsExpression in this.sorts)
			{
				GoogleAnalyticsSortOrder googleAnalyticsSortOrder = googleAnalyticsExpression as GoogleAnalyticsSortOrder;
				if (googleAnalyticsSortOrder != null)
				{
					if (googleAnalyticsSortOrder.Ascending)
					{
						yield return RecordValue.New(Keys.New("fieldName"), new Value[] { TextValue.New(googleAnalyticsSortOrder.Identifier) });
					}
					else
					{
						yield return RecordValue.New(Keys.New("fieldName", "sortOrder"), new Value[]
						{
							TextValue.New(googleAnalyticsSortOrder.Identifier),
							TextValue.New("DESCENDING")
						});
					}
				}
				else
				{
					yield return RecordValue.New(Keys.New("fieldName"), new Value[] { TextValue.New(googleAnalyticsExpression.QueryString) });
				}
			}
			IEnumerator<GoogleAnalyticsExpression> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x00104178 File Offset: 0x00102378
		private RecordValue CreateReportBody()
		{
			RecordValue recordValue = RecordValue.New(Keys.New(new string[] { "viewId", "dateRanges", "dimensions", "metrics", "orderBys", "pageToken", "pageSize", "includeEmptyRows" }), new Value[]
			{
				TextValue.New(this.cube.ViewId),
				ListValue.New(new Value[] { this.CreateDateRange() }),
				ListValue.New(this.CreateDimensions()),
				ListValue.New(this.CreateMetrics()),
				ListValue.New(this.CreateOrderBys()),
				TextValue.New(this.range.SkipCount.Value.ToString(CultureInfo.InvariantCulture)),
				TextValue.New((this.range.TakeCount.IsInfinite ? 100000L : this.range.TakeCount.Value).ToString(CultureInfo.InvariantCulture)),
				LogicalValue.True
			});
			if (!string.IsNullOrEmpty(this.filter))
			{
				recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("filtersExpression"), new Value[] { TextValue.New(this.filter) })).AsRecord;
			}
			return recordValue;
		}

		// Token: 0x04002A3A RID: 10810
		private const string DateQueryFormat = "yyyy-MM-dd";

		// Token: 0x04002A3B RID: 10811
		private readonly string filter;
	}
}
