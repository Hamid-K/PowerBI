using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x0200188F RID: 6287
	internal abstract class QueryExpressionVisitor
	{
		// Token: 0x06009F7D RID: 40829 RVA: 0x0020EF58 File Offset: 0x0020D158
		public QueryExpression Visit(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return this.VisitConstant((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
			case QueryExpressionKind.If:
				return this.VisitIf((IfQueryExpression)expression);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)expression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)expression);
			case QueryExpressionKind.ArgumentAccess:
				return this.VisitArgumentAccess((ArgumentAccessQueryExpression)expression);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06009F7E RID: 40830 RVA: 0x0020EFF0 File Offset: 0x0020D1F0
		protected virtual QueryExpression VisitBinary(BinaryQueryExpression binary)
		{
			QueryExpression queryExpression = this.Visit(binary.Left);
			QueryExpression queryExpression2 = this.Visit(binary.Right);
			return new BinaryQueryExpression(binary.Operator, queryExpression, queryExpression2);
		}

		// Token: 0x06009F7F RID: 40831 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			return columnAccess;
		}

		// Token: 0x06009F80 RID: 40832 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual QueryExpression VisitConstant(ConstantQueryExpression constant)
		{
			return constant;
		}

		// Token: 0x06009F81 RID: 40833 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected virtual QueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression argument)
		{
			return argument;
		}

		// Token: 0x06009F82 RID: 40834 RVA: 0x0020F024 File Offset: 0x0020D224
		protected virtual QueryExpression VisitIf(IfQueryExpression ifExpr)
		{
			QueryExpression queryExpression = this.Visit(ifExpr.Condition);
			QueryExpression queryExpression2 = this.Visit(ifExpr.TrueCase);
			QueryExpression queryExpression3 = this.Visit(ifExpr.FalseCase);
			return new IfQueryExpression(queryExpression, queryExpression2, queryExpression3);
		}

		// Token: 0x06009F83 RID: 40835 RVA: 0x0020F060 File Offset: 0x0020D260
		protected virtual QueryExpression VisitInvocation(InvocationQueryExpression invocation)
		{
			QueryExpression queryExpression = this.Visit(invocation.Function);
			QueryExpression[] array = new QueryExpression[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(invocation.Arguments[i]);
			}
			return new InvocationQueryExpression(queryExpression, array);
		}

		// Token: 0x06009F84 RID: 40836 RVA: 0x0020F0B8 File Offset: 0x0020D2B8
		protected virtual QueryExpression VisitUnary(UnaryQueryExpression unary)
		{
			QueryExpression queryExpression = this.Visit(unary.Expression);
			return new UnaryQueryExpression(unary.Operator, queryExpression);
		}
	}
}
