using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQuery
{
	// Token: 0x0200000A RID: 10
	internal abstract class FilterExpressionVisitor : FilterVisitor<FilterCondition>
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002775 File Offset: 0x00000975
		protected FilterExpressionVisitor(VisitDataShapeDelegate visitDataShape = null)
			: base(visitDataShape)
		{
			this.m_isNegated = false;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002785 File Offset: 0x00000985
		internal override FilterCondition Visit(UnaryFilterCondition condition)
		{
			this.VisitNegatableCondition<UnaryFilterCondition>(condition, delegate(UnaryFilterCondition c)
			{
				this.VisitExpression(c.Expression, c, "Expression");
			});
			return condition;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000279B File Offset: 0x0000099B
		internal override FilterCondition Visit(BinaryFilterCondition condition)
		{
			this.VisitNegatableCondition<BinaryFilterCondition>(condition, delegate(BinaryFilterCondition c)
			{
				this.VisitExpression(c.LeftExpression, c, "LeftExpression");
				this.VisitExpression(c.RightExpression, c, "RightExpression");
			});
			return condition;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027B1 File Offset: 0x000009B1
		internal override FilterCondition Visit(CompoundFilterCondition condition)
		{
			this.VisitNegatableCondition<CompoundFilterCondition>(condition, delegate(CompoundFilterCondition c)
			{
				this.Visit(c.Conditions, c.ObjectType, "Conditions");
			});
			return condition;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027C8 File Offset: 0x000009C8
		internal override FilterCondition Visit(InFilterCondition condition)
		{
			this.VisitExpressions(condition.Expressions, condition, "Expressions");
			List<List<Expression>> values = condition.Values;
			if (values != null)
			{
				for (int i = 0; i < values.Count; i++)
				{
					List<Expression> list = values[i];
					this.VisitExpressions(list, condition, "Values");
				}
			}
			if (condition.Table != null)
			{
				this.VisitExpression(condition.Table, condition, "Table");
			}
			return condition;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002832 File Offset: 0x00000A32
		internal override FilterCondition Visit(AnyValueFilterCondition condition)
		{
			this.VisitExpressions(condition.Targets, condition, "Targets");
			return condition;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002847 File Offset: 0x00000A47
		internal override FilterCondition Visit(DefaultValueFilterCondition condition)
		{
			this.VisitExpressions(condition.Targets, condition, "Targets");
			return condition;
		}

		// Token: 0x0600002E RID: 46
		internal abstract void VisitExpression(Expression expression, FilterCondition owner, string propertyName);

		// Token: 0x0600002F RID: 47 RVA: 0x0000285C File Offset: 0x00000A5C
		internal virtual void VisitExpressions(IReadOnlyList<Expression> expressions, FilterCondition owner, string propertyName)
		{
			for (int i = 0; i < expressions.Count; i++)
			{
				this.VisitExpression(expressions[i], owner, propertyName);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000288C File Offset: 0x00000A8C
		protected virtual void VisitNegatableCondition<T, R>(T condition, Func<T, R> visitFunc) where T : FilterCondition, INegatableCondition
		{
			this.VisitNegatableCondition<T>(condition, delegate(T c)
			{
				visitFunc(condition);
			});
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028C8 File Offset: 0x00000AC8
		protected virtual void VisitNegatableCondition<T>(T condition, Action<T> visitAction) where T : FilterCondition, INegatableCondition
		{
			bool isNegated = condition.IsNegated;
			if (isNegated)
			{
				this.m_isNegated ^= isNegated;
			}
			visitAction(condition);
			if (isNegated)
			{
				this.m_isNegated ^= isNegated;
			}
		}

		// Token: 0x04000031 RID: 49
		protected bool m_isNegated;
	}
}
