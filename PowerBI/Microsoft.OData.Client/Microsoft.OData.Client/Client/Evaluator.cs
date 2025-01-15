using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Microsoft.OData.Client
{
	// Token: 0x0200009C RID: 156
	internal static class Evaluator
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x00011B58 File Offset: 0x0000FD58
		internal static Expression PartialEval(Expression expression, Func<Expression, bool> canBeEvaluated)
		{
			Evaluator.Nominator nominator = new Evaluator.Nominator(canBeEvaluated);
			HashSet<Expression> hashSet = nominator.Nominate(expression);
			return new Evaluator.SubtreeEvaluator(hashSet).Eval(expression);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00011B80 File Offset: 0x0000FD80
		internal static Expression PartialEval(Expression expression)
		{
			return Evaluator.PartialEval(expression, new Func<Expression, bool>(Evaluator.CanBeEvaluatedLocally));
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00011B94 File Offset: 0x0000FD94
		private static bool CanBeEvaluatedLocally(Expression expression)
		{
			return expression.NodeType != ExpressionType.Parameter && expression.NodeType != ExpressionType.Lambda && expression.NodeType != (ExpressionType)10000 && expression.NodeType != (ExpressionType)10001;
		}

		// Token: 0x0200017A RID: 378
		internal class SubtreeEvaluator : DataServiceALinqExpressionVisitor
		{
			// Token: 0x06000DC3 RID: 3523 RVA: 0x0002F83D File Offset: 0x0002DA3D
			internal SubtreeEvaluator(HashSet<Expression> candidates)
			{
				this.candidates = candidates;
			}

			// Token: 0x06000DC4 RID: 3524 RVA: 0x0002F84C File Offset: 0x0002DA4C
			internal Expression Eval(Expression exp)
			{
				return this.Visit(exp);
			}

			// Token: 0x06000DC5 RID: 3525 RVA: 0x0002F855 File Offset: 0x0002DA55
			internal override Expression Visit(Expression exp)
			{
				if (exp == null)
				{
					return null;
				}
				if (this.candidates.Contains(exp))
				{
					return Evaluator.SubtreeEvaluator.Evaluate(exp);
				}
				return base.Visit(exp);
			}

			// Token: 0x06000DC6 RID: 3526 RVA: 0x0002F878 File Offset: 0x0002DA78
			private static Expression Evaluate(Expression e)
			{
				if (e.NodeType == ExpressionType.Constant)
				{
					return e;
				}
				LambdaExpression lambdaExpression = Expression.Lambda(e, new ParameterExpression[0]);
				Delegate @delegate = lambdaExpression.Compile();
				object obj = @delegate.DynamicInvoke(null);
				Type type = e.Type;
				if (obj != null && type.IsArray && type.GetElementType() == obj.GetType().GetElementType())
				{
					type = obj.GetType();
				}
				return Expression.Constant(obj, type);
			}

			// Token: 0x0400073E RID: 1854
			private HashSet<Expression> candidates;
		}

		// Token: 0x0200017B RID: 379
		internal class Nominator : DataServiceALinqExpressionVisitor
		{
			// Token: 0x06000DC7 RID: 3527 RVA: 0x0002F8E5 File Offset: 0x0002DAE5
			internal Nominator(Func<Expression, bool> functionCanBeEvaluated)
			{
				this.functionCanBeEvaluated = functionCanBeEvaluated;
			}

			// Token: 0x06000DC8 RID: 3528 RVA: 0x0002F8F4 File Offset: 0x0002DAF4
			internal HashSet<Expression> Nominate(Expression expression)
			{
				this.candidates = new HashSet<Expression>(EqualityComparer<Expression>.Default);
				this.Visit(expression);
				return this.candidates;
			}

			// Token: 0x06000DC9 RID: 3529 RVA: 0x0002F914 File Offset: 0x0002DB14
			internal override Expression Visit(Expression expression)
			{
				if (expression != null)
				{
					bool flag = this.cannotBeEvaluated;
					this.cannotBeEvaluated = false;
					base.Visit(expression);
					if (!this.cannotBeEvaluated)
					{
						if (this.functionCanBeEvaluated(expression))
						{
							this.candidates.Add(expression);
						}
						else
						{
							this.cannotBeEvaluated = true;
						}
					}
					this.cannotBeEvaluated = this.cannotBeEvaluated || flag;
				}
				return expression;
			}

			// Token: 0x0400073F RID: 1855
			private Func<Expression, bool> functionCanBeEvaluated;

			// Token: 0x04000740 RID: 1856
			private HashSet<Expression> candidates;

			// Token: 0x04000741 RID: 1857
			private bool cannotBeEvaluated;
		}
	}
}
