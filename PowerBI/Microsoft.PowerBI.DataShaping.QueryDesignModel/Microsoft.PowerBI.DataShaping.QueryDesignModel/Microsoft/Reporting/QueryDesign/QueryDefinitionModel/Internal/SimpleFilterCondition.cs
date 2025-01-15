using System;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000D7 RID: 215
	internal sealed class SimpleFilterCondition : FilterCondition
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x00022AE3 File Offset: 0x00020CE3
		internal SimpleFilterCondition(QueryExpression expression, bool not = false)
			: this(expression, not, FilterOperator.Equal, null)
		{
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00022AEF File Offset: 0x00020CEF
		internal SimpleFilterCondition(QueryExpression leftExpression, FilterOperator @operator, QueryExpression rightExpression)
			: this(leftExpression, false, @operator, rightExpression)
		{
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00022AFB File Offset: 0x00020CFB
		internal SimpleFilterCondition(QueryExpression leftExpression, FilterOperator @operator)
			: this(leftExpression, false, @operator, null)
		{
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00022B08 File Offset: 0x00020D08
		internal SimpleFilterCondition(QueryExpression leftExpression, bool not, FilterOperator @operator, QueryExpression rightExpression)
		{
			this._leftExpression = ArgumentValidation.CheckNotNull<QueryExpression>(leftExpression, "leftExpression");
			this._operator = @operator;
			if (rightExpression == null)
			{
				ArgumentValidation.CheckCondition(@operator == FilterOperator.Equal || @operator == FilterOperator.AllValues, "rightExpression");
			}
			if (@operator == FilterOperator.AllValues)
			{
				ArgumentValidation.CheckCondition(leftExpression is QueryFieldExpression, "leftExpression");
				ArgumentValidation.CheckCondition(!not, "not");
			}
			this._rightExpression = rightExpression;
			this._not = not;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000D93 RID: 3475 RVA: 0x00022B7F File Offset: 0x00020D7F
		public QueryExpression LeftExpression
		{
			get
			{
				return this._leftExpression;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x00022B87 File Offset: 0x00020D87
		public bool Not
		{
			get
			{
				return this._not;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x00022B8F File Offset: 0x00020D8F
		public FilterOperator Operator
		{
			get
			{
				return this._operator;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00022B97 File Offset: 0x00020D97
		public QueryExpression RightExpression
		{
			get
			{
				return this._rightExpression;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x00022BA0 File Offset: 0x00020DA0
		internal override QueryExpression ToPredicate()
		{
			bool flag = this.Not;
			QueryExpression queryExpression;
			switch (this.Operator)
			{
			case FilterOperator.Equal:
			case FilterOperator.EqualIdentity:
			{
				bool flag2 = this.Operator == FilterOperator.EqualIdentity;
				if (this.RightExpression == null)
				{
					queryExpression = this.LeftExpression;
					goto IL_019C;
				}
				if (this.RightExpression is QueryNullExpression)
				{
					queryExpression = this.LeftExpression.IsNull();
					goto IL_019C;
				}
				if (this.Not)
				{
					if (flag2)
					{
						queryExpression = this.LeftExpression.NotEqualIdentity(this.RightExpression);
					}
					else
					{
						queryExpression = this.LeftExpression.NotEqual(this.RightExpression);
					}
					flag = false;
					goto IL_019C;
				}
				if (flag2)
				{
					queryExpression = this.LeftExpression.EqualIdentity(this.RightExpression);
					goto IL_019C;
				}
				queryExpression = this.LeftExpression.Equal(this.RightExpression);
				goto IL_019C;
			}
			case FilterOperator.GreaterThan:
				queryExpression = this.LeftExpression.GreaterThan(this.RightExpression);
				goto IL_019C;
			case FilterOperator.GreaterThanOrEqual:
				queryExpression = this.LeftExpression.GreaterThanOrEqual(this.RightExpression);
				goto IL_019C;
			case FilterOperator.LessThanOrEqual:
				queryExpression = this.LeftExpression.LessThanOrEqual(this.RightExpression);
				goto IL_019C;
			case FilterOperator.LessThan:
				queryExpression = this.LeftExpression.LessThan(this.RightExpression);
				goto IL_019C;
			case FilterOperator.Contains:
				queryExpression = this.LeftExpression.TextContains(this.RightExpression);
				goto IL_019C;
			case FilterOperator.StartsWith:
				queryExpression = this.LeftExpression.StartsWith(this.RightExpression);
				goto IL_019C;
			case FilterOperator.DateTimeEqualToSecond:
				queryExpression = this.LeftExpression.DateTimeEqualToSecond(this.RightExpression);
				goto IL_019C;
			case FilterOperator.EndsWith:
				queryExpression = this.LeftExpression.EndsWith(this.RightExpression);
				goto IL_019C;
			}
			throw new NotImplementedException("Cannot generate Predicate for this FilterCondition.");
			IL_019C:
			if (!flag)
			{
				return queryExpression;
			}
			return queryExpression.Not();
		}

		// Token: 0x04000993 RID: 2451
		private readonly QueryExpression _leftExpression;

		// Token: 0x04000994 RID: 2452
		private readonly QueryExpression _rightExpression;

		// Token: 0x04000995 RID: 2453
		private readonly FilterOperator _operator;

		// Token: 0x04000996 RID: 2454
		private readonly bool _not;
	}
}
