using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D5 RID: 213
	internal class CompoundFilterCondition : FilterCondition
	{
		// Token: 0x06000D86 RID: 3462 RVA: 0x00022950 File Offset: 0x00020B50
		internal CompoundFilterCondition(CompoundFilterOperator @operator, IEnumerable<FilterCondition> conditions)
		{
			this.Operator = @operator;
			Func<FilterCondition, FilterCondition> func;
			if ((func = CompoundFilterCondition.<>O.<0>__ValidateItem) == null)
			{
				func = (CompoundFilterCondition.<>O.<0>__ValidateItem = new Func<FilterCondition, FilterCondition>(CompoundFilterCondition.ValidateItem));
			}
			this.Conditions = new ReadOnlyCollection<FilterCondition>(conditions.Select(func).ToArray<FilterCondition>());
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00022990 File Offset: 0x00020B90
		internal CompoundFilterCondition(params FilterCondition[] conditions)
			: this(CompoundFilterOperator.All, conditions)
		{
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0002299A File Offset: 0x00020B9A
		internal CompoundFilterCondition(CompoundFilterOperator @operator, params FilterCondition[] conditions)
			: this(@operator, conditions)
		{
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x000229A4 File Offset: 0x00020BA4
		public CompoundFilterOperator Operator { get; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x000229AC File Offset: 0x00020BAC
		public ReadOnlyCollection<FilterCondition> Conditions { get; }

		// Token: 0x06000D8B RID: 3467 RVA: 0x000229B4 File Offset: 0x00020BB4
		internal override QueryExpression ToPredicate()
		{
			if (this.Conditions.Count <= 0)
			{
				return null;
			}
			QueryExpression queryExpression = (from c in this.Conditions
				select c.ToPredicate() into c
				where c != null
				select c).Aggregate(new Func<QueryExpression, QueryExpression, QueryExpression>(this.AggregateConditions));
			if (queryExpression != null && (this.Operator == CompoundFilterOperator.NotAll || this.Operator == CompoundFilterOperator.NotAny))
			{
				queryExpression = queryExpression.Not();
			}
			return queryExpression;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00022A50 File Offset: 0x00020C50
		private QueryExpression AggregateConditions(QueryExpression expr1, QueryExpression expr2)
		{
			ArgumentValidation.CheckNotNull<QueryExpression>(expr1, "expr1");
			ArgumentValidation.CheckNotNull<QueryExpression>(expr2, "expr2");
			switch (this.Operator)
			{
			case CompoundFilterOperator.Any:
			case CompoundFilterOperator.NotAny:
				return expr1.Or(expr2);
			}
			return expr1.And(expr2);
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00022AA3 File Offset: 0x00020CA3
		private static FilterCondition ValidateItem(FilterCondition item)
		{
			ArgumentValidation.CheckNotNull<FilterCondition>(item, "item");
			if (item is Filter)
			{
				throw new ArgumentException("Cannot nest a Filter in another Filter.", "item");
			}
			return item;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00022ACA File Offset: 0x00020CCA
		internal FilterCondition RewriteNestedConditions(IList<FilterCondition> newConditionsList)
		{
			if (newConditionsList == this.Conditions)
			{
				return this;
			}
			return new CompoundFilterCondition(this.Operator, newConditionsList);
		}

		// Token: 0x020002F0 RID: 752
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040010BF RID: 4287
			public static Func<FilterCondition, FilterCondition> <0>__ValidateItem;
		}
	}
}
