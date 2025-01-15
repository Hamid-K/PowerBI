using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B2 RID: 690
	public class DefaultQueryExpressionVisitor<T> : QueryExpressionVisitor<T>
	{
		// Token: 0x060016C0 RID: 5824 RVA: 0x00028E32 File Offset: 0x00027032
		protected internal override T Visit(QuerySourceRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C1 RID: 5825 RVA: 0x00028E3B File Offset: 0x0002703B
		protected internal override T Visit(QueryPropertyExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C2 RID: 5826 RVA: 0x00028E44 File Offset: 0x00027044
		protected internal override T Visit(QueryColumnExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C3 RID: 5827 RVA: 0x00028E4D File Offset: 0x0002704D
		protected internal override T Visit(QueryMeasureExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x00028E56 File Offset: 0x00027056
		protected internal override T Visit(QueryHierarchyExpression expression)
		{
			return this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016C5 RID: 5829 RVA: 0x00028E64 File Offset: 0x00027064
		protected internal override T Visit(QueryHierarchyLevelExpression expression)
		{
			return this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016C6 RID: 5830 RVA: 0x00028E72 File Offset: 0x00027072
		protected internal override T Visit(QueryPropertyVariationSourceExpression expression)
		{
			return this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016C7 RID: 5831 RVA: 0x00028E80 File Offset: 0x00027080
		protected internal override T Visit(QueryAggregationExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x00028E89 File Offset: 0x00027089
		protected internal override T Visit(QueryDatePartExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x00028E92 File Offset: 0x00027092
		protected internal override T Visit(QueryPercentileExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x00028E9B File Offset: 0x0002709B
		protected internal override T Visit(QueryMinExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00028EA4 File Offset: 0x000270A4
		protected internal override T Visit(QueryMaxExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00028EAD File Offset: 0x000270AD
		protected internal override T Visit(QueryFloorExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x00028EB6 File Offset: 0x000270B6
		protected internal override T Visit(QueryDiscretizeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x00028EBF File Offset: 0x000270BF
		protected internal override T Visit(QueryMemberExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00028EC8 File Offset: 0x000270C8
		protected internal override T Visit(QueryNativeFormatExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00028ED1 File Offset: 0x000270D1
		protected internal override T Visit(QueryNativeMeasureExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00028EDA File Offset: 0x000270DA
		protected internal override T Visit(QueryExistsExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00028EE3 File Offset: 0x000270E3
		protected internal override T Visit(QueryNotExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00028EEC File Offset: 0x000270EC
		protected internal override T Visit(QueryAndExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00028EF5 File Offset: 0x000270F5
		protected internal override T Visit(QueryOrExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00028EFE File Offset: 0x000270FE
		protected internal override T Visit(QueryComparisonExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00028F07 File Offset: 0x00027107
		protected internal override T Visit(QueryContainsExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00028F10 File Offset: 0x00027110
		protected internal override T Visit(QueryStartsWithExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00028F19 File Offset: 0x00027119
		protected internal override T Visit(QueryArithmeticExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00028F22 File Offset: 0x00027122
		protected internal override T Visit(QueryEndsWithExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x00028F2B File Offset: 0x0002712B
		protected internal override T Visit(QueryBetweenExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00028F34 File Offset: 0x00027134
		protected internal override T Visit(QueryInExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x00028F3D File Offset: 0x0002713D
		protected internal override T Visit(QueryScopedEvalExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00028F46 File Offset: 0x00027146
		protected internal override T Visit(QueryFilteredEvalExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00028F4F File Offset: 0x0002714F
		protected internal override T Visit(QuerySparklineDataExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00028F58 File Offset: 0x00027158
		protected internal override T Visit(QueryBooleanConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00028F61 File Offset: 0x00027161
		protected internal override T Visit(QueryDateConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00028F6A File Offset: 0x0002716A
		protected internal override T Visit(QueryDateTimeConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00028F73 File Offset: 0x00027173
		protected internal override T Visit(QueryDateTimeSecondConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00028F7C File Offset: 0x0002717C
		protected internal override T Visit(QueryDecadeConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00028F85 File Offset: 0x00027185
		protected internal override T Visit(QueryDecimalConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00028F8E File Offset: 0x0002718E
		protected internal override T Visit(QueryIntegerConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00028F97 File Offset: 0x00027197
		protected internal override T Visit(QueryNullConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00028FA0 File Offset: 0x000271A0
		protected internal override T Visit(QueryStringConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00028FA9 File Offset: 0x000271A9
		protected internal override T Visit(QueryNumberConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00028FB2 File Offset: 0x000271B2
		protected internal override T Visit(QueryYearAndMonthConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00028FBB File Offset: 0x000271BB
		protected internal override T Visit(QueryYearAndWeekConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00028FC4 File Offset: 0x000271C4
		protected internal override T Visit(QueryYearConstantExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00028FCD File Offset: 0x000271CD
		protected internal override T Visit(QueryLiteralExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00028FD6 File Offset: 0x000271D6
		protected internal override T Visit(QueryNowExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00028FDF File Offset: 0x000271DF
		protected internal override T Visit(QueryDateAddExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00028FE8 File Offset: 0x000271E8
		protected internal override T Visit(QueryDateSpanExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00028FF1 File Offset: 0x000271F1
		protected internal override T Visit(QueryDefaultValueExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F1 RID: 5873 RVA: 0x00028FFA File Offset: 0x000271FA
		protected internal override T Visit(QueryAnyValueExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00029003 File Offset: 0x00027203
		protected internal override T Visit(QueryTransformOutputRoleRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0002900C File Offset: 0x0002720C
		protected internal override T Visit(QueryTransformTableRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00029015 File Offset: 0x00027215
		protected internal override T Visit(QuerySubqueryExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0002901E File Offset: 0x0002721E
		protected internal override T Visit(QueryLetRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00029027 File Offset: 0x00027227
		protected internal override T Visit(QueryRoleRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00029030 File Offset: 0x00027230
		protected internal override T Visit(QuerySummaryValueRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00029039 File Offset: 0x00027239
		protected internal override T Visit(QueryParameterRefExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00029042 File Offset: 0x00027242
		protected internal override T Visit(QueryPrimitiveTypeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0002904B File Offset: 0x0002724B
		protected internal override T Visit(QueryTableTypeExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x00029054 File Offset: 0x00027254
		protected internal override T Visit(QueryTypeOfExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x0002905D File Offset: 0x0002725D
		protected internal override T Visit(QueryNativeVisualCalculationExpression expression)
		{
			return this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00029068 File Offset: 0x00027268
		protected internal virtual T VisitUnhandledExpression(QueryExpression expression)
		{
			return default(T);
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x0002907E File Offset: 0x0002727E
		public virtual T VisitExpression(QueryExpression expression)
		{
			return expression.Accept<T>(this);
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00029087 File Offset: 0x00027287
		public virtual T VisitExpression(QueryExpressionContainer expression)
		{
			return this.VisitExpression(expression.Expression);
		}
	}
}
