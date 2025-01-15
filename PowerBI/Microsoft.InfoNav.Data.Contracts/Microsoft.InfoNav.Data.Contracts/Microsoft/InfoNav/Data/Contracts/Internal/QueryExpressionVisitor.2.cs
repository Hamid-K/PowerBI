using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B0 RID: 688
	public abstract class QueryExpressionVisitor<T>
	{
		// Token: 0x0600163D RID: 5693
		protected internal abstract T Visit(QuerySourceRefExpression expression);

		// Token: 0x0600163E RID: 5694
		protected internal abstract T Visit(QueryPropertyExpression expression);

		// Token: 0x0600163F RID: 5695
		protected internal abstract T Visit(QueryColumnExpression expression);

		// Token: 0x06001640 RID: 5696
		protected internal abstract T Visit(QueryMeasureExpression expression);

		// Token: 0x06001641 RID: 5697
		protected internal abstract T Visit(QueryHierarchyExpression expression);

		// Token: 0x06001642 RID: 5698
		protected internal abstract T Visit(QueryHierarchyLevelExpression expression);

		// Token: 0x06001643 RID: 5699
		protected internal abstract T Visit(QueryPropertyVariationSourceExpression expression);

		// Token: 0x06001644 RID: 5700
		protected internal abstract T Visit(QueryAggregationExpression expression);

		// Token: 0x06001645 RID: 5701
		protected internal abstract T Visit(QueryDatePartExpression expression);

		// Token: 0x06001646 RID: 5702
		protected internal abstract T Visit(QueryPercentileExpression expression);

		// Token: 0x06001647 RID: 5703
		protected internal abstract T Visit(QueryMinExpression expression);

		// Token: 0x06001648 RID: 5704
		protected internal abstract T Visit(QueryMaxExpression expression);

		// Token: 0x06001649 RID: 5705
		protected internal abstract T Visit(QueryFloorExpression expression);

		// Token: 0x0600164A RID: 5706
		protected internal abstract T Visit(QueryDiscretizeExpression expression);

		// Token: 0x0600164B RID: 5707
		protected internal abstract T Visit(QueryMemberExpression expression);

		// Token: 0x0600164C RID: 5708
		protected internal abstract T Visit(QueryNativeFormatExpression expression);

		// Token: 0x0600164D RID: 5709
		protected internal abstract T Visit(QueryNativeMeasureExpression expression);

		// Token: 0x0600164E RID: 5710
		protected internal abstract T Visit(QueryExistsExpression expression);

		// Token: 0x0600164F RID: 5711
		protected internal abstract T Visit(QueryNotExpression expression);

		// Token: 0x06001650 RID: 5712
		protected internal abstract T Visit(QueryAndExpression expression);

		// Token: 0x06001651 RID: 5713
		protected internal abstract T Visit(QueryOrExpression expression);

		// Token: 0x06001652 RID: 5714
		protected internal abstract T Visit(QueryComparisonExpression expression);

		// Token: 0x06001653 RID: 5715
		protected internal abstract T Visit(QueryContainsExpression expression);

		// Token: 0x06001654 RID: 5716
		protected internal abstract T Visit(QueryStartsWithExpression expression);

		// Token: 0x06001655 RID: 5717
		protected internal abstract T Visit(QueryArithmeticExpression expression);

		// Token: 0x06001656 RID: 5718
		protected internal abstract T Visit(QueryEndsWithExpression expression);

		// Token: 0x06001657 RID: 5719
		protected internal abstract T Visit(QueryBetweenExpression expression);

		// Token: 0x06001658 RID: 5720
		protected internal abstract T Visit(QueryInExpression expression);

		// Token: 0x06001659 RID: 5721
		protected internal abstract T Visit(QueryScopedEvalExpression expression);

		// Token: 0x0600165A RID: 5722
		protected internal abstract T Visit(QueryFilteredEvalExpression expression);

		// Token: 0x0600165B RID: 5723
		protected internal abstract T Visit(QuerySparklineDataExpression expression);

		// Token: 0x0600165C RID: 5724
		protected internal abstract T Visit(QueryLiteralExpression expression);

		// Token: 0x0600165D RID: 5725
		protected internal abstract T Visit(QueryDefaultValueExpression expression);

		// Token: 0x0600165E RID: 5726
		protected internal abstract T Visit(QueryAnyValueExpression expression);

		// Token: 0x0600165F RID: 5727
		protected internal abstract T Visit(QueryBooleanConstantExpression expression);

		// Token: 0x06001660 RID: 5728
		protected internal abstract T Visit(QueryDateConstantExpression expression);

		// Token: 0x06001661 RID: 5729
		protected internal abstract T Visit(QueryDateTimeConstantExpression expression);

		// Token: 0x06001662 RID: 5730
		protected internal abstract T Visit(QueryDateTimeSecondConstantExpression expression);

		// Token: 0x06001663 RID: 5731
		protected internal abstract T Visit(QueryDecadeConstantExpression expression);

		// Token: 0x06001664 RID: 5732
		protected internal abstract T Visit(QueryDecimalConstantExpression expression);

		// Token: 0x06001665 RID: 5733
		protected internal abstract T Visit(QueryIntegerConstantExpression expression);

		// Token: 0x06001666 RID: 5734
		protected internal abstract T Visit(QueryNullConstantExpression expression);

		// Token: 0x06001667 RID: 5735
		protected internal abstract T Visit(QueryStringConstantExpression expression);

		// Token: 0x06001668 RID: 5736
		protected internal abstract T Visit(QueryNumberConstantExpression expression);

		// Token: 0x06001669 RID: 5737
		protected internal abstract T Visit(QueryYearAndMonthConstantExpression expression);

		// Token: 0x0600166A RID: 5738
		protected internal abstract T Visit(QueryYearAndWeekConstantExpression expression);

		// Token: 0x0600166B RID: 5739
		protected internal abstract T Visit(QueryYearConstantExpression expression);

		// Token: 0x0600166C RID: 5740
		protected internal abstract T Visit(QueryNowExpression expression);

		// Token: 0x0600166D RID: 5741
		protected internal abstract T Visit(QueryDateAddExpression expression);

		// Token: 0x0600166E RID: 5742
		protected internal abstract T Visit(QueryDateSpanExpression expression);

		// Token: 0x0600166F RID: 5743
		protected internal abstract T Visit(QueryTransformOutputRoleRefExpression expression);

		// Token: 0x06001670 RID: 5744
		protected internal abstract T Visit(QueryTransformTableRefExpression expression);

		// Token: 0x06001671 RID: 5745
		protected internal abstract T Visit(QuerySubqueryExpression expression);

		// Token: 0x06001672 RID: 5746
		protected internal abstract T Visit(QueryLetRefExpression expression);

		// Token: 0x06001673 RID: 5747
		protected internal abstract T Visit(QueryRoleRefExpression expression);

		// Token: 0x06001674 RID: 5748
		protected internal abstract T Visit(QuerySummaryValueRefExpression expression);

		// Token: 0x06001675 RID: 5749
		protected internal abstract T Visit(QueryParameterRefExpression expression);

		// Token: 0x06001676 RID: 5750
		protected internal abstract T Visit(QueryTypeOfExpression expression);

		// Token: 0x06001677 RID: 5751
		protected internal abstract T Visit(QueryPrimitiveTypeExpression expression);

		// Token: 0x06001678 RID: 5752
		protected internal abstract T Visit(QueryTableTypeExpression expression);

		// Token: 0x06001679 RID: 5753
		protected internal abstract T Visit(QueryNativeVisualCalculationExpression expression);
	}
}
