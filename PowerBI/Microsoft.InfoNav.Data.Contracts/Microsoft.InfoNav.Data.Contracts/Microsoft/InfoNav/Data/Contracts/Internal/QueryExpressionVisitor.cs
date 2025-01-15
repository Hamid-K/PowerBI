using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AF RID: 687
	public abstract class QueryExpressionVisitor
	{
		// Token: 0x060015FF RID: 5631
		protected internal abstract void Visit(QuerySourceRefExpression expression);

		// Token: 0x06001600 RID: 5632
		protected internal abstract void Visit(QueryPropertyExpression expression);

		// Token: 0x06001601 RID: 5633
		protected internal abstract void Visit(QueryColumnExpression expression);

		// Token: 0x06001602 RID: 5634
		protected internal abstract void Visit(QueryMeasureExpression expression);

		// Token: 0x06001603 RID: 5635
		protected internal abstract void Visit(QueryHierarchyExpression expression);

		// Token: 0x06001604 RID: 5636
		protected internal abstract void Visit(QueryHierarchyLevelExpression expression);

		// Token: 0x06001605 RID: 5637
		protected internal abstract void Visit(QueryPropertyVariationSourceExpression expression);

		// Token: 0x06001606 RID: 5638
		protected internal abstract void Visit(QueryAggregationExpression expression);

		// Token: 0x06001607 RID: 5639
		protected internal abstract void Visit(QueryDatePartExpression expression);

		// Token: 0x06001608 RID: 5640
		protected internal abstract void Visit(QueryPercentileExpression expression);

		// Token: 0x06001609 RID: 5641
		protected internal abstract void Visit(QueryMinExpression expression);

		// Token: 0x0600160A RID: 5642
		protected internal abstract void Visit(QueryMaxExpression expression);

		// Token: 0x0600160B RID: 5643
		protected internal abstract void Visit(QueryFloorExpression expression);

		// Token: 0x0600160C RID: 5644
		protected internal abstract void Visit(QueryDiscretizeExpression expression);

		// Token: 0x0600160D RID: 5645
		protected internal abstract void Visit(QueryMemberExpression expression);

		// Token: 0x0600160E RID: 5646
		protected internal abstract void Visit(QueryNativeFormatExpression expression);

		// Token: 0x0600160F RID: 5647
		protected internal abstract void Visit(QueryNativeMeasureExpression expression);

		// Token: 0x06001610 RID: 5648
		protected internal abstract void Visit(QueryExistsExpression expression);

		// Token: 0x06001611 RID: 5649
		protected internal abstract void Visit(QueryNotExpression expression);

		// Token: 0x06001612 RID: 5650
		protected internal abstract void Visit(QueryAndExpression expression);

		// Token: 0x06001613 RID: 5651
		protected internal abstract void Visit(QueryOrExpression expression);

		// Token: 0x06001614 RID: 5652
		protected internal abstract void Visit(QueryComparisonExpression expression);

		// Token: 0x06001615 RID: 5653
		protected internal abstract void Visit(QueryContainsExpression expression);

		// Token: 0x06001616 RID: 5654
		protected internal abstract void Visit(QueryStartsWithExpression expression);

		// Token: 0x06001617 RID: 5655
		protected internal abstract void Visit(QueryArithmeticExpression expression);

		// Token: 0x06001618 RID: 5656
		protected internal abstract void Visit(QueryEndsWithExpression expression);

		// Token: 0x06001619 RID: 5657
		protected internal abstract void Visit(QueryBetweenExpression expression);

		// Token: 0x0600161A RID: 5658
		protected internal abstract void Visit(QueryInExpression expression);

		// Token: 0x0600161B RID: 5659
		protected internal abstract void Visit(QueryScopedEvalExpression expression);

		// Token: 0x0600161C RID: 5660
		protected internal abstract void Visit(QueryFilteredEvalExpression expression);

		// Token: 0x0600161D RID: 5661
		protected internal abstract void Visit(QuerySparklineDataExpression expression);

		// Token: 0x0600161E RID: 5662
		protected internal abstract void Visit(QueryBooleanConstantExpression expression);

		// Token: 0x0600161F RID: 5663
		protected internal abstract void Visit(QueryDateConstantExpression expression);

		// Token: 0x06001620 RID: 5664
		protected internal abstract void Visit(QueryDateTimeConstantExpression expression);

		// Token: 0x06001621 RID: 5665
		protected internal abstract void Visit(QueryDateTimeSecondConstantExpression expression);

		// Token: 0x06001622 RID: 5666
		protected internal abstract void Visit(QueryDecadeConstantExpression expression);

		// Token: 0x06001623 RID: 5667
		protected internal abstract void Visit(QueryDecimalConstantExpression expression);

		// Token: 0x06001624 RID: 5668
		protected internal abstract void Visit(QueryIntegerConstantExpression expression);

		// Token: 0x06001625 RID: 5669
		protected internal abstract void Visit(QueryNullConstantExpression expression);

		// Token: 0x06001626 RID: 5670
		protected internal abstract void Visit(QueryStringConstantExpression expression);

		// Token: 0x06001627 RID: 5671
		protected internal abstract void Visit(QueryNumberConstantExpression expression);

		// Token: 0x06001628 RID: 5672
		protected internal abstract void Visit(QueryYearAndMonthConstantExpression expression);

		// Token: 0x06001629 RID: 5673
		protected internal abstract void Visit(QueryYearAndWeekConstantExpression expression);

		// Token: 0x0600162A RID: 5674
		protected internal abstract void Visit(QueryYearConstantExpression expression);

		// Token: 0x0600162B RID: 5675
		protected internal abstract void Visit(QueryLiteralExpression expression);

		// Token: 0x0600162C RID: 5676
		protected internal abstract void Visit(QueryDefaultValueExpression expression);

		// Token: 0x0600162D RID: 5677
		protected internal abstract void Visit(QueryAnyValueExpression expression);

		// Token: 0x0600162E RID: 5678
		protected internal abstract void Visit(QueryNowExpression expression);

		// Token: 0x0600162F RID: 5679
		protected internal abstract void Visit(QueryDateAddExpression expression);

		// Token: 0x06001630 RID: 5680
		protected internal abstract void Visit(QueryDateSpanExpression expression);

		// Token: 0x06001631 RID: 5681
		protected internal abstract void Visit(QueryTransformOutputRoleRefExpression expression);

		// Token: 0x06001632 RID: 5682
		protected internal abstract void Visit(QueryTransformTableRefExpression expression);

		// Token: 0x06001633 RID: 5683
		protected internal abstract void Visit(QuerySubqueryExpression expression);

		// Token: 0x06001634 RID: 5684
		protected internal abstract void Visit(QueryLetRefExpression expression);

		// Token: 0x06001635 RID: 5685
		protected internal abstract void Visit(QueryRoleRefExpression expression);

		// Token: 0x06001636 RID: 5686
		protected internal abstract void Visit(QuerySummaryValueRefExpression expression);

		// Token: 0x06001637 RID: 5687
		protected internal abstract void Visit(QueryParameterRefExpression expression);

		// Token: 0x06001638 RID: 5688
		protected internal abstract void Visit(QueryTypeOfExpression expression);

		// Token: 0x06001639 RID: 5689
		protected internal abstract void Visit(QueryPrimitiveTypeExpression expression);

		// Token: 0x0600163A RID: 5690
		protected internal abstract void Visit(QueryTableTypeExpression expression);

		// Token: 0x0600163B RID: 5691
		protected internal abstract void Visit(QueryNativeVisualCalculationExpression expression);
	}
}
