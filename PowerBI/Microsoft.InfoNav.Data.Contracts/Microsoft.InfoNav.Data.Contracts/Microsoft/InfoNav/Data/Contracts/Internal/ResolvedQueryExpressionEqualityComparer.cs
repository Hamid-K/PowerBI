using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000205 RID: 517
	public abstract class ResolvedQueryExpressionEqualityComparer : IEqualityComparer<ResolvedQueryExpression>, IEqualityComparer<ResolvedQueryTableTypeColumn>
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x0001C1C8 File Offset: 0x0001A3C8
		public virtual bool Equals(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryExpression>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.AcceptEquals(this, right);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0001C1F6 File Offset: 0x0001A3F6
		public virtual int GetHashCode(ResolvedQueryExpression obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return obj.AcceptGetHashCode(this);
		}

		// Token: 0x06000E54 RID: 3668
		public abstract bool VisitEquals(ResolvedQuerySourceRefExpression left, ResolvedQuerySourceRefExpression right);

		// Token: 0x06000E55 RID: 3669
		public abstract int VisitGetHashCode(ResolvedQuerySourceRefExpression obj);

		// Token: 0x06000E56 RID: 3670
		public abstract bool VisitEquals(ResolvedQueryExpressionSourceRefExpression left, ResolvedQueryExpressionSourceRefExpression right);

		// Token: 0x06000E57 RID: 3671
		public abstract int VisitGetHashCode(ResolvedQueryExpressionSourceRefExpression obj);

		// Token: 0x06000E58 RID: 3672
		public abstract bool VisitEquals(ResolvedQuerySubqueryExpression left, ResolvedQuerySubqueryExpression right);

		// Token: 0x06000E59 RID: 3673
		public abstract int VisitGetHashCode(ResolvedQuerySubqueryExpression obj);

		// Token: 0x06000E5A RID: 3674
		public abstract bool VisitEquals(ResolvedQueryColumnExpression left, ResolvedQueryColumnExpression right);

		// Token: 0x06000E5B RID: 3675
		public abstract int VisitGetHashCode(ResolvedQueryColumnExpression obj);

		// Token: 0x06000E5C RID: 3676
		public abstract bool VisitEquals(ResolvedQueryMeasureExpression left, ResolvedQueryMeasureExpression right);

		// Token: 0x06000E5D RID: 3677
		public abstract int VisitGetHashCode(ResolvedQueryMeasureExpression obj);

		// Token: 0x06000E5E RID: 3678
		public abstract bool VisitEquals(ResolvedQueryHierarchyExpression left, ResolvedQueryHierarchyExpression right);

		// Token: 0x06000E5F RID: 3679
		public abstract int VisitGetHashCode(ResolvedQueryHierarchyExpression obj);

		// Token: 0x06000E60 RID: 3680
		public abstract bool VisitEquals(ResolvedQueryHierarchyLevelExpression left, ResolvedQueryHierarchyLevelExpression right);

		// Token: 0x06000E61 RID: 3681
		public abstract int VisitGetHashCode(ResolvedQueryHierarchyLevelExpression obj);

		// Token: 0x06000E62 RID: 3682
		public abstract bool VisitEquals(ResolvedQueryPropertyVariationSourceExpression left, ResolvedQueryPropertyVariationSourceExpression right);

		// Token: 0x06000E63 RID: 3683
		public abstract int VisitGetHashCode(ResolvedQueryPropertyVariationSourceExpression obj);

		// Token: 0x06000E64 RID: 3684
		public abstract bool VisitEquals(ResolvedQueryColumnReferenceExpression left, ResolvedQueryColumnReferenceExpression right);

		// Token: 0x06000E65 RID: 3685
		public abstract int VisitGetHashCode(ResolvedQueryColumnReferenceExpression obj);

		// Token: 0x06000E66 RID: 3686
		public abstract bool VisitEquals(ResolvedQueryNotExpression left, ResolvedQueryNotExpression right);

		// Token: 0x06000E67 RID: 3687
		public abstract int VisitGetHashCode(ResolvedQueryNotExpression obj);

		// Token: 0x06000E68 RID: 3688
		public abstract bool VisitEquals(ResolvedQueryAndExpression left, ResolvedQueryAndExpression right);

		// Token: 0x06000E69 RID: 3689
		public abstract int VisitGetHashCode(ResolvedQueryAndExpression obj);

		// Token: 0x06000E6A RID: 3690
		public abstract bool VisitEquals(ResolvedQueryOrExpression left, ResolvedQueryOrExpression right);

		// Token: 0x06000E6B RID: 3691
		public abstract int VisitGetHashCode(ResolvedQueryOrExpression obj);

		// Token: 0x06000E6C RID: 3692
		public abstract bool VisitEquals(ResolvedQueryAggregationExpression left, ResolvedQueryAggregationExpression right);

		// Token: 0x06000E6D RID: 3693
		public abstract int VisitGetHashCode(ResolvedQueryAggregationExpression obj);

		// Token: 0x06000E6E RID: 3694
		public abstract bool VisitEquals(ResolvedQueryArithmeticExpression left, ResolvedQueryArithmeticExpression right);

		// Token: 0x06000E6F RID: 3695
		public abstract int VisitGetHashCode(ResolvedQueryArithmeticExpression obj);

		// Token: 0x06000E70 RID: 3696
		public abstract bool VisitEquals(ResolvedQueryBetweenExpression left, ResolvedQueryBetweenExpression right);

		// Token: 0x06000E71 RID: 3697
		public abstract int VisitGetHashCode(ResolvedQueryBetweenExpression obj);

		// Token: 0x06000E72 RID: 3698
		public abstract bool VisitEquals(ResolvedQueryComparisonExpression left, ResolvedQueryComparisonExpression right);

		// Token: 0x06000E73 RID: 3699
		public abstract int VisitGetHashCode(ResolvedQueryComparisonExpression obj);

		// Token: 0x06000E74 RID: 3700
		public abstract bool VisitEquals(ResolvedQueryContainsExpression left, ResolvedQueryContainsExpression right);

		// Token: 0x06000E75 RID: 3701
		public abstract int VisitGetHashCode(ResolvedQueryContainsExpression obj);

		// Token: 0x06000E76 RID: 3702
		public abstract bool VisitEquals(ResolvedQueryDateAddExpression left, ResolvedQueryDateAddExpression right);

		// Token: 0x06000E77 RID: 3703
		public abstract int VisitGetHashCode(ResolvedQueryDateAddExpression obj);

		// Token: 0x06000E78 RID: 3704
		public abstract bool VisitEquals(ResolvedQueryDateSpanExpression left, ResolvedQueryDateSpanExpression right);

		// Token: 0x06000E79 RID: 3705
		public abstract int VisitGetHashCode(ResolvedQueryDateSpanExpression obj);

		// Token: 0x06000E7A RID: 3706
		public abstract bool VisitEquals(ResolvedQueryExistsExpression left, ResolvedQueryExistsExpression right);

		// Token: 0x06000E7B RID: 3707
		public abstract int VisitGetHashCode(ResolvedQueryExistsExpression obj);

		// Token: 0x06000E7C RID: 3708
		public abstract bool VisitEquals(ResolvedQueryFloorExpression left, ResolvedQueryFloorExpression right);

		// Token: 0x06000E7D RID: 3709
		public abstract int VisitGetHashCode(ResolvedQueryFloorExpression obj);

		// Token: 0x06000E7E RID: 3710
		public abstract bool VisitEquals(ResolvedQueryInExpression left, ResolvedQueryInExpression right);

		// Token: 0x06000E7F RID: 3711
		public abstract int VisitGetHashCode(ResolvedQueryInExpression obj);

		// Token: 0x06000E80 RID: 3712
		public abstract bool VisitEquals(ResolvedQueryScopedEvalExpression left, ResolvedQueryScopedEvalExpression right);

		// Token: 0x06000E81 RID: 3713
		public abstract int VisitGetHashCode(ResolvedQueryScopedEvalExpression obj);

		// Token: 0x06000E82 RID: 3714
		public abstract bool VisitEquals(ResolvedQueryFilteredEvalExpression left, ResolvedQueryFilteredEvalExpression right);

		// Token: 0x06000E83 RID: 3715
		public abstract int VisitGetHashCode(ResolvedQueryFilteredEvalExpression obj);

		// Token: 0x06000E84 RID: 3716
		public abstract bool VisitEquals(ResolvedQueryLiteralExpression left, ResolvedQueryLiteralExpression right);

		// Token: 0x06000E85 RID: 3717
		public abstract int VisitGetHashCode(ResolvedQueryLiteralExpression obj);

		// Token: 0x06000E86 RID: 3718
		public abstract bool VisitEquals(ResolvedQueryNowExpression left, ResolvedQueryNowExpression right);

		// Token: 0x06000E87 RID: 3719
		public abstract int VisitGetHashCode(ResolvedQueryNowExpression obj);

		// Token: 0x06000E88 RID: 3720
		public abstract bool VisitEquals(ResolvedQueryPercentileExpression left, ResolvedQueryPercentileExpression right);

		// Token: 0x06000E89 RID: 3721
		public abstract int VisitGetHashCode(ResolvedQueryPercentileExpression obj);

		// Token: 0x06000E8A RID: 3722
		public abstract bool VisitEquals(ResolvedQueryMinExpression left, ResolvedQueryMinExpression right);

		// Token: 0x06000E8B RID: 3723
		public abstract int VisitGetHashCode(ResolvedQueryMinExpression obj);

		// Token: 0x06000E8C RID: 3724
		public abstract bool VisitEquals(ResolvedQueryMaxExpression left, ResolvedQueryMaxExpression right);

		// Token: 0x06000E8D RID: 3725
		public abstract int VisitGetHashCode(ResolvedQueryMaxExpression obj);

		// Token: 0x06000E8E RID: 3726
		public abstract bool VisitEquals(ResolvedQueryStartsWithExpression left, ResolvedQueryStartsWithExpression right);

		// Token: 0x06000E8F RID: 3727
		public abstract int VisitGetHashCode(ResolvedQueryStartsWithExpression obj);

		// Token: 0x06000E90 RID: 3728
		public abstract bool VisitEquals(ResolvedQueryEndsWithExpression left, ResolvedQueryEndsWithExpression right);

		// Token: 0x06000E91 RID: 3729
		public abstract int VisitGetHashCode(ResolvedQueryEndsWithExpression obj);

		// Token: 0x06000E92 RID: 3730
		public abstract bool VisitEquals(ResolvedQueryDefaultValueExpression left, ResolvedQueryDefaultValueExpression right);

		// Token: 0x06000E93 RID: 3731
		public abstract int VisitGetHashCode(ResolvedQueryDefaultValueExpression obj);

		// Token: 0x06000E94 RID: 3732
		public abstract bool VisitEquals(ResolvedQueryAnyValueExpression left, ResolvedQueryAnyValueExpression right);

		// Token: 0x06000E95 RID: 3733
		public abstract int VisitGetHashCode(ResolvedQueryAnyValueExpression obj);

		// Token: 0x06000E96 RID: 3734
		public abstract bool VisitEquals(ResolvedQueryTransformOutputRoleRefExpression left, ResolvedQueryTransformOutputRoleRefExpression right);

		// Token: 0x06000E97 RID: 3735
		public abstract int VisitGetHashCode(ResolvedQueryTransformOutputRoleRefExpression obj);

		// Token: 0x06000E98 RID: 3736
		public abstract bool VisitEquals(ResolvedQueryTransformTableColumnExpression left, ResolvedQueryTransformTableColumnExpression right);

		// Token: 0x06000E99 RID: 3737
		public abstract int VisitGetHashCode(ResolvedQueryTransformTableColumnExpression obj);

		// Token: 0x06000E9A RID: 3738
		public abstract bool VisitEquals(ResolvedQueryDiscretizeExpression left, ResolvedQueryDiscretizeExpression right);

		// Token: 0x06000E9B RID: 3739
		public abstract int VisitGetHashCode(ResolvedQueryDiscretizeExpression obj);

		// Token: 0x06000E9C RID: 3740
		public abstract bool VisitEquals(ResolvedQuerySparklineDataExpression left, ResolvedQuerySparklineDataExpression right);

		// Token: 0x06000E9D RID: 3741
		public abstract int VisitGetHashCode(ResolvedQuerySparklineDataExpression obj);

		// Token: 0x06000E9E RID: 3742
		public abstract bool VisitEquals(ResolvedQueryMemberExpression left, ResolvedQueryMemberExpression right);

		// Token: 0x06000E9F RID: 3743
		public abstract int VisitGetHashCode(ResolvedQueryMemberExpression obj);

		// Token: 0x06000EA0 RID: 3744
		public abstract bool VisitEquals(ResolvedQueryNativeFormatExpression left, ResolvedQueryNativeFormatExpression right);

		// Token: 0x06000EA1 RID: 3745
		public abstract int VisitGetHashCode(ResolvedQueryNativeFormatExpression obj);

		// Token: 0x06000EA2 RID: 3746
		public abstract bool VisitEquals(ResolvedQueryNativeMeasureExpression left, ResolvedQueryNativeMeasureExpression right);

		// Token: 0x06000EA3 RID: 3747
		public abstract int VisitGetHashCode(ResolvedQueryNativeMeasureExpression obj);

		// Token: 0x06000EA4 RID: 3748
		public abstract bool VisitEquals(ResolvedQueryLetRefExpression left, ResolvedQueryLetRefExpression right);

		// Token: 0x06000EA5 RID: 3749
		public abstract int VisitGetHashCode(ResolvedQueryLetRefExpression obj);

		// Token: 0x06000EA6 RID: 3750
		public abstract bool VisitEquals(ResolvedQueryRoleRefExpression left, ResolvedQueryRoleRefExpression right);

		// Token: 0x06000EA7 RID: 3751
		public abstract int VisitGetHashCode(ResolvedQueryRoleRefExpression obj);

		// Token: 0x06000EA8 RID: 3752
		public abstract bool VisitEquals(ResolvedSummaryValueRefExpression left, ResolvedSummaryValueRefExpression right);

		// Token: 0x06000EA9 RID: 3753
		public abstract int VisitGetHashCode(ResolvedSummaryValueRefExpression obj);

		// Token: 0x06000EAA RID: 3754
		public abstract bool VisitEquals(ResolvedQueryParameterRefExpression left, ResolvedQueryParameterRefExpression right);

		// Token: 0x06000EAB RID: 3755
		public abstract int VisitGetHashCode(ResolvedQueryParameterRefExpression obj);

		// Token: 0x06000EAC RID: 3756
		public abstract bool VisitEquals(ResolvedQueryTypeOfExpression left, ResolvedQueryTypeOfExpression right);

		// Token: 0x06000EAD RID: 3757
		public abstract int VisitGetHashCode(ResolvedQueryTypeOfExpression obj);

		// Token: 0x06000EAE RID: 3758
		public abstract bool VisitEquals(ResolvedQueryTableTypeExpression left, ResolvedQueryTableTypeExpression right);

		// Token: 0x06000EAF RID: 3759
		public abstract int VisitGetHashCode(ResolvedQueryTableTypeExpression obj);

		// Token: 0x06000EB0 RID: 3760
		public abstract bool VisitEquals(ResolvedQueryPrimitiveTypeExpression left, ResolvedQueryPrimitiveTypeExpression right);

		// Token: 0x06000EB1 RID: 3761
		public abstract int VisitGetHashCode(ResolvedQueryPrimitiveTypeExpression obj);

		// Token: 0x06000EB2 RID: 3762
		public abstract bool VisitEquals(ResolvedQueryNativeVisualCalculationExpression left, ResolvedQueryNativeVisualCalculationExpression right);

		// Token: 0x06000EB3 RID: 3763
		public abstract int VisitGetHashCode(ResolvedQueryNativeVisualCalculationExpression obj);

		// Token: 0x06000EB4 RID: 3764
		public abstract bool Equals(ResolvedQueryTableTypeColumn left, ResolvedQueryTableTypeColumn right);

		// Token: 0x06000EB5 RID: 3765
		public abstract int GetHashCode(ResolvedQueryTableTypeColumn obj);
	}
}
