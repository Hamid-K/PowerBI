using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E53 RID: 3667
	internal abstract class QueryExpressionVisitor2<T>
	{
		// Token: 0x0600628D RID: 25229 RVA: 0x00152410 File Offset: 0x00150610
		public T Visit(QueryExpression expression)
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

		// Token: 0x0600628E RID: 25230
		protected abstract T NewBinary(BinaryOperator2 op, T left, T right);

		// Token: 0x0600628F RID: 25231
		protected abstract T NewColumnAccess(int column);

		// Token: 0x06006290 RID: 25232
		protected abstract T NewConstant(Value value);

		// Token: 0x06006291 RID: 25233
		protected abstract T NewArgumentAccess();

		// Token: 0x06006292 RID: 25234
		protected abstract T NewIf(T condition, T trueCase, T falseCase);

		// Token: 0x06006293 RID: 25235
		protected abstract T NewInvocation(T function, IList<T> arguments);

		// Token: 0x06006294 RID: 25236
		protected abstract T NewUnary(UnaryOperator2 op, T operand);

		// Token: 0x06006295 RID: 25237 RVA: 0x001524A8 File Offset: 0x001506A8
		protected virtual T VisitBinary(BinaryQueryExpression binary)
		{
			T t = this.Visit(binary.Left);
			T t2 = this.Visit(binary.Right);
			return this.NewBinary(binary.Operator, t, t2);
		}

		// Token: 0x06006296 RID: 25238 RVA: 0x001524DD File Offset: 0x001506DD
		protected virtual T VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			return this.NewColumnAccess(columnAccess.Column);
		}

		// Token: 0x06006297 RID: 25239 RVA: 0x001524EB File Offset: 0x001506EB
		protected virtual T VisitConstant(ConstantQueryExpression constant)
		{
			return this.NewConstant(constant.Value);
		}

		// Token: 0x06006298 RID: 25240 RVA: 0x001524F9 File Offset: 0x001506F9
		protected virtual T VisitArgumentAccess(ArgumentAccessQueryExpression argument)
		{
			return this.NewArgumentAccess();
		}

		// Token: 0x06006299 RID: 25241 RVA: 0x00152504 File Offset: 0x00150704
		protected virtual T VisitIf(IfQueryExpression ifExpr)
		{
			T t = this.Visit(ifExpr.Condition);
			T t2 = this.Visit(ifExpr.TrueCase);
			T t3 = this.Visit(ifExpr.FalseCase);
			return this.NewIf(t, t2, t3);
		}

		// Token: 0x0600629A RID: 25242 RVA: 0x00152544 File Offset: 0x00150744
		protected virtual T VisitInvocation(InvocationQueryExpression invocation)
		{
			T t = this.Visit(invocation.Function);
			T[] array = new T[invocation.Arguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.Visit(invocation.Arguments[i]);
			}
			return this.NewInvocation(t, array);
		}

		// Token: 0x0600629B RID: 25243 RVA: 0x001525A0 File Offset: 0x001507A0
		protected virtual T VisitUnary(UnaryQueryExpression unary)
		{
			T t = this.Visit(unary.Expression);
			return this.NewUnary(unary.Operator, t);
		}
	}
}
