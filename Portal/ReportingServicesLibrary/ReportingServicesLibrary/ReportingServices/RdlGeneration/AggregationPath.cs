using System;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x02000380 RID: 896
	internal class AggregationPath
	{
		// Token: 0x06001DA4 RID: 7588 RVA: 0x000792BB File Offset: 0x000774BB
		private AggregationPath(Expression startExpr, Expression endExpr)
		{
			if (startExpr == null)
			{
				throw new ArgumentNullException();
			}
			if (endExpr != null && endExpr.Node != null)
			{
				throw new ArgumentException();
			}
			this.m_startExpr = startExpr;
			this.m_endExpr = endExpr ?? startExpr;
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x000792F0 File Offset: 0x000774F0
		public Expression StartExpr
		{
			get
			{
				return this.m_startExpr;
			}
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x000792F8 File Offset: 0x000774F8
		public Expression EndExpr
		{
			get
			{
				return this.m_endExpr;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00079300 File Offset: 0x00077500
		public bool IsEmpty
		{
			get
			{
				return this.m_startExpr.Path.IsEmpty && this.m_startExpr.Node == null;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00079324 File Offset: 0x00077524
		public bool AreSubtotalsNonUnique
		{
			get
			{
				bool hasDown = true;
				bool hasUpAfterDown = false;
				AggregationPath.Traverse(this.m_startExpr, delegate(Expression e)
				{
					if (hasUpAfterDown)
					{
						return;
					}
					foreach (PathItem pathItem in e.Path)
					{
						if (pathItem.Cardinality == Cardinality.Many)
						{
							hasDown = true;
						}
						if (hasDown && pathItem.ReverseCardinality == Cardinality.Many)
						{
							hasUpAfterDown = true;
							return;
						}
					}
					if (e.NodeAsFunction != null && e.NodeAsFunction.FunctionName == FunctionName.Evaluate)
					{
						hasDown = false;
					}
				});
				return hasUpAfterDown;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00079362 File Offset: 0x00077562
		public static AggregationPath Empty
		{
			get
			{
				return new AggregationPath(new Expression(), null);
			}
		}

		// Token: 0x06001DAA RID: 7594 RVA: 0x0007936F File Offset: 0x0007756F
		public AggregationPath Clone()
		{
			Expression expression = this.m_startExpr.Clone();
			return new AggregationPath(expression, AggregationPath.FindEndExpr(expression));
		}

		// Token: 0x06001DAB RID: 7595 RVA: 0x00079388 File Offset: 0x00077588
		public static AggregationPath FromExpression(Expression expr, out Expression inputExpr)
		{
			if (expr.NodeAsFunction == null || expr.NodeAsFunction.IsAggregate == null || !expr.NodeAsFunction.IsAggregate.Value)
			{
				throw new ArgumentException("expr must be an aggregate function");
			}
			inputExpr = expr.NodeAsFunction.Arguments[0];
			Expression expression = new Expression();
			Expression expression2 = expression;
			expression2.Path.AddRange(inputExpr.Path);
			while (inputExpr.NodeAsFunction != null)
			{
				FunctionNode nodeAsFunction = inputExpr.NodeAsFunction;
				if (nodeAsFunction.FunctionName == FunctionName.Filter)
				{
					AggregationPath.AddPassthroughFunction(ref inputExpr, ref expression2, 0);
				}
				else
				{
					if (nodeAsFunction.FunctionName != FunctionName.Evaluate)
					{
						break;
					}
					AggregationPath.AddPassthroughFunction(ref inputExpr, ref expression2, 0);
				}
			}
			if (!inputExpr.Path.IsEmpty)
			{
				inputExpr = inputExpr.Clone();
				inputExpr.Path.Clear();
			}
			return new AggregationPath(expression, expression2);
		}

		// Token: 0x06001DAC RID: 7596 RVA: 0x00079464 File Offset: 0x00077664
		private static void AddPassthroughFunction(ref Expression inputExpr, ref Expression endExpr, int inputArgIndex)
		{
			FunctionNode functionNode = new FunctionNode(inputExpr.NodeAsFunction.FunctionName);
			functionNode.Arguments.AddRange(inputExpr.NodeAsFunction.Arguments);
			inputExpr = functionNode.Arguments[inputArgIndex];
			Expression expression = new Expression();
			expression.Path.AddRange(inputExpr.Path);
			functionNode.Arguments.RemoveAt(inputArgIndex);
			functionNode.Arguments.Insert(inputArgIndex, expression);
			endExpr.Node = functionNode;
			endExpr = expression;
		}

		// Token: 0x06001DAD RID: 7597 RVA: 0x000794E4 File Offset: 0x000776E4
		public void Append(AggregationPath aggPath)
		{
			this.m_endExpr.Path.AddRange(aggPath.StartExpr.Path);
			if (aggPath.StartExpr.Node != null)
			{
				this.m_endExpr.Node = aggPath.StartExpr.Node.Clone();
				this.m_endExpr = AggregationPath.FindEndExpr(this.m_endExpr);
			}
		}

		// Token: 0x06001DAE RID: 7598 RVA: 0x00079545 File Offset: 0x00077745
		public static AggregationPath Combine(AggregationPath path1, AggregationPath path2)
		{
			AggregationPath aggregationPath = path1.Clone();
			aggregationPath.Append(path2);
			return aggregationPath;
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00079554 File Offset: 0x00077754
		public Expression AddToExpression(Expression inputExpr)
		{
			Expression expression = this.m_startExpr.Clone();
			Expression expression2 = AggregationPath.FindEndExpr(expression);
			expression2.Path.AddRange(inputExpr.Path);
			expression2.Node = inputExpr.Node.Clone();
			return expression;
		}

		// Token: 0x06001DB0 RID: 7600 RVA: 0x00079588 File Offset: 0x00077788
		private static Expression FindEndExpr(Expression expr)
		{
			Expression endExpr = expr;
			AggregationPath.Traverse(expr, delegate(Expression e)
			{
				endExpr = e;
			});
			return endExpr;
		}

		// Token: 0x06001DB1 RID: 7601 RVA: 0x000795BC File Offset: 0x000777BC
		private static void Traverse(Expression startExpr, Action<Expression> action)
		{
			Expression expression = startExpr;
			if (action != null)
			{
				action(expression);
			}
			while (expression.Node != null)
			{
				FunctionNode nodeAsFunction = expression.NodeAsFunction;
				if (nodeAsFunction == null)
				{
					throw new ArgumentException();
				}
				if (nodeAsFunction.FunctionName == FunctionName.Filter)
				{
					expression = nodeAsFunction.Arguments[0];
				}
				else
				{
					if (nodeAsFunction.FunctionName != FunctionName.Evaluate)
					{
						throw new ArgumentException();
					}
					expression = nodeAsFunction.Arguments[0];
				}
				if (action != null)
				{
					action(expression);
				}
			}
		}

		// Token: 0x04000C75 RID: 3189
		private readonly Expression m_startExpr;

		// Token: 0x04000C76 RID: 3190
		private Expression m_endExpr;
	}
}
