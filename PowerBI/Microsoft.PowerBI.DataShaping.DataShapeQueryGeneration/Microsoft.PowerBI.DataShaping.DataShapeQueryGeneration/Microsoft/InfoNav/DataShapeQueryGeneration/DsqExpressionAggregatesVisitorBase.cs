using System;
using System.Globalization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000082 RID: 130
	internal abstract class DsqExpressionAggregatesVisitorBase
	{
		// Token: 0x06000523 RID: 1315
		internal abstract void Visit(DsqExpressionMaxAggregate aggregate);

		// Token: 0x06000524 RID: 1316
		internal abstract void Visit(DsqExpressionMinAggregate aggregate);

		// Token: 0x06000525 RID: 1317
		internal abstract void Visit(DsqExpressionMedianAggregate aggregate);

		// Token: 0x06000526 RID: 1318
		internal abstract void Visit(DsqExpressionAverageAggregate aggregate);

		// Token: 0x06000527 RID: 1319
		internal abstract void Visit(DsqExpressionPercentileAggregate aggregate);

		// Token: 0x06000528 RID: 1320
		internal abstract void Visit(DsqExpressionCountAggregate aggregate);

		// Token: 0x06000529 RID: 1321
		internal abstract void Visit(DsqExpressionSubtotalAggregate aggregate);

		// Token: 0x0600052A RID: 1322 RVA: 0x00012BEC File Offset: 0x00010DEC
		internal virtual void Visit(DsqExpressionAggregateBase aggregate)
		{
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unsupported aggregate type: {0}.", aggregate.Kind.ToString()));
		}
	}
}
