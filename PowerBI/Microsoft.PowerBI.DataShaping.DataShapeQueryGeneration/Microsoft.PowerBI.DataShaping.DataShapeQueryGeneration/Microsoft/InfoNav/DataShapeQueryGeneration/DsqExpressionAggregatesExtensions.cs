using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000081 RID: 129
	internal static class DsqExpressionAggregatesExtensions
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00012B5D File Offset: 0x00010D5D
		internal static bool IsAverage(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate.Kind == DsqExpressionAggregateKind.Average;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00012B68 File Offset: 0x00010D68
		internal static bool IsCount(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate.Kind == DsqExpressionAggregateKind.Count;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00012B73 File Offset: 0x00010D73
		internal static bool IsMax(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate.Kind == DsqExpressionAggregateKind.Max;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00012B7E File Offset: 0x00010D7E
		internal static bool IsMin(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate.Kind == DsqExpressionAggregateKind.Min;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012B89 File Offset: 0x00010D89
		internal static bool IsMedian(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate.Kind == DsqExpressionAggregateKind.Median;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00012B94 File Offset: 0x00010D94
		internal static bool IsSubtotal(this DsqExpressionAggregateBase aggregate)
		{
			return aggregate == DsqExpressionSubtotalAggregate.Instance;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00012B9E File Offset: 0x00010D9E
		internal static bool HasAverage(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Average);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00012BA7 File Offset: 0x00010DA7
		internal static bool HasCount(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Count);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00012BB0 File Offset: 0x00010DB0
		internal static bool HasMax(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Max);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00012BB9 File Offset: 0x00010DB9
		internal static bool HasMin(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Min);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00012BC2 File Offset: 0x00010DC2
		internal static bool HasMedian(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Median);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00012BCB File Offset: 0x00010DCB
		internal static bool HasSubtotal(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Subtotal);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00012BD4 File Offset: 0x00010DD4
		internal static bool HasPercentile(this IReadOnlyList<DsqExpressionAggregateKind> aggregates)
		{
			return DsqExpressionAggregatesExtensions.HasAggregate(aggregates, DsqExpressionAggregateKind.Percentile);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00012BDD File Offset: 0x00010DDD
		private static bool HasAggregate(IReadOnlyList<DsqExpressionAggregateKind> aggregates, DsqExpressionAggregateKind aggregate)
		{
			return aggregates != null && aggregates.Contains(aggregate);
		}
	}
}
