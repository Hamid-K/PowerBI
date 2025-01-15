using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002B1 RID: 689
	public class DefaultQueryExpressionVisitor : QueryExpressionVisitor
	{
		// Token: 0x0600167B RID: 5755 RVA: 0x00028A22 File Offset: 0x00026C22
		protected internal override void Visit(QuerySourceRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x00028A2B File Offset: 0x00026C2B
		protected internal override void Visit(QueryPropertyExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00028A39 File Offset: 0x00026C39
		protected internal override void Visit(QueryColumnExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00028A47 File Offset: 0x00026C47
		protected internal override void Visit(QueryMeasureExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00028A55 File Offset: 0x00026C55
		protected internal override void Visit(QueryHierarchyExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00028A63 File Offset: 0x00026C63
		protected internal override void Visit(QueryHierarchyLevelExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00028A71 File Offset: 0x00026C71
		protected internal override void Visit(QueryPropertyVariationSourceExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00028A7F File Offset: 0x00026C7F
		protected internal override void Visit(QueryAggregationExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00028A8D File Offset: 0x00026C8D
		protected internal override void Visit(QueryDatePartExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001684 RID: 5764 RVA: 0x00028A9B File Offset: 0x00026C9B
		protected internal override void Visit(QueryPercentileExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x00028AA9 File Offset: 0x00026CA9
		protected internal override void Visit(QueryMinExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00028AB7 File Offset: 0x00026CB7
		protected internal override void Visit(QueryMaxExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00028AC5 File Offset: 0x00026CC5
		protected internal override void Visit(QueryFloorExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x00028AD3 File Offset: 0x00026CD3
		protected internal override void Visit(QueryDiscretizeExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00028AE1 File Offset: 0x00026CE1
		protected internal override void Visit(QueryMemberExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00028AEF File Offset: 0x00026CEF
		protected internal override void Visit(QueryNativeFormatExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00028AFD File Offset: 0x00026CFD
		protected internal override void Visit(QueryNativeMeasureExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00028B06 File Offset: 0x00026D06
		protected internal override void Visit(QueryExistsExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x00028B14 File Offset: 0x00026D14
		protected internal override void Visit(QueryNotExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00028B22 File Offset: 0x00026D22
		protected internal override void Visit(QueryAndExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00028B2B File Offset: 0x00026D2B
		protected internal override void Visit(QueryOrExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00028B34 File Offset: 0x00026D34
		protected internal override void Visit(QueryComparisonExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00028B3D File Offset: 0x00026D3D
		protected internal override void Visit(QueryContainsExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00028B46 File Offset: 0x00026D46
		protected internal override void Visit(QueryStartsWithExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00028B4F File Offset: 0x00026D4F
		protected internal override void Visit(QueryArithmeticExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00028B58 File Offset: 0x00026D58
		protected internal override void Visit(QueryEndsWithExpression expression)
		{
			this.VisitBinaryExpression(expression);
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00028B61 File Offset: 0x00026D61
		protected internal override void Visit(QueryBetweenExpression expression)
		{
			this.VisitExpression(expression.LowerBound);
			this.VisitExpression(expression.Expression);
			this.VisitExpression(expression.UpperBound);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x00028B88 File Offset: 0x00026D88
		protected internal override void Visit(QueryInExpression expression)
		{
			this.VisitExpressionList(expression.Expressions);
			if (expression.HasValues)
			{
				List<List<QueryExpressionContainer>> values = expression.Values;
				for (int i = 0; i < values.Count; i++)
				{
					this.VisitExpressionList(values[i]);
				}
				return;
			}
			this.VisitExpression(expression.Table);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x00028BDB File Offset: 0x00026DDB
		protected internal override void Visit(QueryScopedEvalExpression expression)
		{
			this.VisitExpression(expression.Expression);
			this.VisitExpressionList(expression.Scope);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x00028BF5 File Offset: 0x00026DF5
		protected internal override void Visit(QueryFilteredEvalExpression expression)
		{
			this.VisitExpression(expression.Expression);
			this.VisitQueryFilterList(expression.Filters);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00028C0F File Offset: 0x00026E0F
		protected internal override void Visit(QuerySparklineDataExpression expression)
		{
			this.VisitExpression(expression.Measure);
			this.VisitExpressionList(expression.Groupings);
			if (expression.ScalarKey != null)
			{
				this.VisitExpression(expression.ScalarKey);
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x00028C43 File Offset: 0x00026E43
		protected internal override void Visit(QueryBooleanConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00028C4C File Offset: 0x00026E4C
		protected internal override void Visit(QueryDateConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00028C55 File Offset: 0x00026E55
		protected internal override void Visit(QueryDateTimeConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x00028C5E File Offset: 0x00026E5E
		protected internal override void Visit(QueryDateTimeSecondConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00028C67 File Offset: 0x00026E67
		protected internal override void Visit(QueryDecadeConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x00028C70 File Offset: 0x00026E70
		protected internal override void Visit(QueryDecimalConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00028C79 File Offset: 0x00026E79
		protected internal override void Visit(QueryIntegerConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00028C82 File Offset: 0x00026E82
		protected internal override void Visit(QueryNullConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00028C8B File Offset: 0x00026E8B
		protected internal override void Visit(QueryStringConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00028C94 File Offset: 0x00026E94
		protected internal override void Visit(QueryNumberConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00028C9D File Offset: 0x00026E9D
		protected internal override void Visit(QueryYearAndMonthConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00028CA6 File Offset: 0x00026EA6
		protected internal override void Visit(QueryYearAndWeekConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00028CAF File Offset: 0x00026EAF
		protected internal override void Visit(QueryYearConstantExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00028CB8 File Offset: 0x00026EB8
		protected internal override void Visit(QueryLiteralExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00028CC1 File Offset: 0x00026EC1
		protected void VisitBinaryExpression(QueryBinaryExpression expression)
		{
			this.VisitExpression(expression.Left);
			this.VisitExpression(expression.Right);
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00028CDB File Offset: 0x00026EDB
		protected internal override void Visit(QueryNowExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00028CE4 File Offset: 0x00026EE4
		protected internal override void Visit(QueryDateAddExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00028CF2 File Offset: 0x00026EF2
		protected internal override void Visit(QueryDateSpanExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00028D00 File Offset: 0x00026F00
		protected internal override void Visit(QueryDefaultValueExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00028D09 File Offset: 0x00026F09
		protected internal override void Visit(QueryAnyValueExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00028D12 File Offset: 0x00026F12
		protected internal override void Visit(QueryTransformOutputRoleRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00028D1B File Offset: 0x00026F1B
		protected internal override void Visit(QueryTransformTableRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00028D24 File Offset: 0x00026F24
		protected internal override void Visit(QuerySubqueryExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00028D2D File Offset: 0x00026F2D
		protected internal override void Visit(QueryLetRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00028D36 File Offset: 0x00026F36
		protected internal override void Visit(QueryRoleRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x00028D3F File Offset: 0x00026F3F
		protected internal override void Visit(QuerySummaryValueRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x00028D48 File Offset: 0x00026F48
		protected internal override void Visit(QueryParameterRefExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x00028D51 File Offset: 0x00026F51
		protected internal override void Visit(QueryPrimitiveTypeExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00028D5A File Offset: 0x00026F5A
		protected internal override void Visit(QueryTypeOfExpression expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x00028D68 File Offset: 0x00026F68
		protected internal override void Visit(QueryTableTypeExpression expression)
		{
			if (expression.Columns != null)
			{
				this.VisitExpressionList(expression.Columns);
			}
		}

		// Token: 0x060016B8 RID: 5816 RVA: 0x00028D7E File Offset: 0x00026F7E
		protected internal override void Visit(QueryNativeVisualCalculationExpression expression)
		{
			this.VisitUnhandledExpression(expression);
		}

		// Token: 0x060016B9 RID: 5817 RVA: 0x00028D87 File Offset: 0x00026F87
		protected internal virtual void VisitUnhandledExpression(QueryExpression expression)
		{
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x00028D89 File Offset: 0x00026F89
		public virtual void VisitExpression(QueryExpression expression)
		{
			expression.Accept(this);
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x00028D92 File Offset: 0x00026F92
		public virtual void VisitExpression(QueryExpressionContainer expression)
		{
			this.VisitExpression(expression.Expression);
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x00028DA0 File Offset: 0x00026FA0
		private void VisitExpressionList(List<QueryExpressionContainer> expressions)
		{
			for (int i = 0; i < expressions.Count; i++)
			{
				this.VisitExpression(expressions[i]);
			}
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x00028DCC File Offset: 0x00026FCC
		private void VisitQueryFilterList(List<QueryFilter> filters)
		{
			if (filters != null)
			{
				for (int i = 0; i < filters.Count; i++)
				{
					this.VisitQueryFilter(filters[i]);
				}
			}
		}

		// Token: 0x060016BE RID: 5822 RVA: 0x00028DFA File Offset: 0x00026FFA
		private void VisitQueryFilter(QueryFilter filter)
		{
			if (filter.Target != null)
			{
				this.VisitExpressionList(filter.Target);
			}
			if (filter.Condition != null)
			{
				this.VisitExpression(filter.Condition);
			}
		}
	}
}
