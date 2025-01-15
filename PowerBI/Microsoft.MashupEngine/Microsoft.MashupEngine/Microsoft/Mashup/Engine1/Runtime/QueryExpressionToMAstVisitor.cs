using System;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001581 RID: 5505
	internal class QueryExpressionToMAstVisitor
	{
		// Token: 0x0600891A RID: 35098 RVA: 0x001D0617 File Offset: 0x001CE817
		public QueryExpressionToMAstVisitor(Keys keys)
		{
			this.keys = keys;
		}

		// Token: 0x0600891B RID: 35099 RVA: 0x001D0628 File Offset: 0x001CE828
		public Value Visit(QueryExpression expression)
		{
			switch (expression.Kind)
			{
			case QueryExpressionKind.Binary:
				return this.VisitBinary((BinaryQueryExpression)expression);
			case QueryExpressionKind.Constant:
				return QueryExpressionToMAstVisitor.VisitConstant((ConstantQueryExpression)expression);
			case QueryExpressionKind.ColumnAccess:
				return this.VisitColumnAccess((ColumnAccessQueryExpression)expression);
			case QueryExpressionKind.If:
				return this.VisitIf((IfQueryExpression)expression);
			case QueryExpressionKind.Invocation:
				return this.VisitInvocation((InvocationQueryExpression)expression);
			case QueryExpressionKind.Unary:
				return this.VisitUnary((UnaryQueryExpression)expression);
			case QueryExpressionKind.ArgumentAccess:
				return QueryExpressionToMAstVisitor.VisitArgumentAccess((ArgumentAccessQueryExpression)expression);
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600891C RID: 35100 RVA: 0x001D06C0 File Offset: 0x001CE8C0
		private Value VisitBinary(BinaryQueryExpression binary)
		{
			return RecordValue.New(MAst.BinaryKeys, new Value[]
			{
				MAst.BinaryKind,
				TextValue.New(binary.Operator.ToString()),
				this.Visit(binary.Left),
				this.Visit(binary.Right)
			});
		}

		// Token: 0x0600891D RID: 35101 RVA: 0x001D071F File Offset: 0x001CE91F
		private Value VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
		{
			return RecordValue.New(SimplifiedMAst.FieldAccessKeys, new Value[]
			{
				MAst.FieldAccessKind,
				TextValue.New(this.keys[columnAccess.Column]),
				TableModule.ItemExpression.Item
			});
		}

		// Token: 0x0600891E RID: 35102 RVA: 0x001D075A File Offset: 0x001CE95A
		private static Value VisitConstant(ConstantQueryExpression constant)
		{
			return QueryExpressionToMAstVisitor.Constant(constant.Value);
		}

		// Token: 0x0600891F RID: 35103 RVA: 0x001D0768 File Offset: 0x001CE968
		private Value VisitIf(IfQueryExpression ifExpr)
		{
			return RecordValue.New(MAst.IfKeys, new Value[]
			{
				MAst.IfKind,
				this.Visit(ifExpr.Condition),
				this.Visit(ifExpr.TrueCase),
				this.Visit(ifExpr.FalseCase)
			});
		}

		// Token: 0x06008920 RID: 35104 RVA: 0x001D07BC File Offset: 0x001CE9BC
		private Value VisitInvocation(InvocationQueryExpression invocation)
		{
			return RecordValue.New(MAst.InvocationKeys, new Value[]
			{
				MAst.InvocationKind,
				this.Visit(invocation.Function),
				ListValue.New(invocation.Arguments.Select(new Func<QueryExpression, Value>(this.Visit)).ToArray<Value>())
			});
		}

		// Token: 0x06008921 RID: 35105 RVA: 0x001D0814 File Offset: 0x001CEA14
		private static Value VisitArgumentAccess(ArgumentAccessQueryExpression argumentAccess)
		{
			return TableModule.ItemExpression.Item;
		}

		// Token: 0x06008922 RID: 35106 RVA: 0x001D081C File Offset: 0x001CEA1C
		private Value VisitUnary(UnaryQueryExpression unary)
		{
			return RecordValue.New(MAst.UnaryKeys, new Value[]
			{
				MAst.UnaryKind,
				TextValue.New(unary.Operator.ToString()),
				this.Visit(unary.Expression)
			});
		}

		// Token: 0x06008923 RID: 35107 RVA: 0x001D086C File Offset: 0x001CEA6C
		private static Value Constant(Value value)
		{
			return RecordValue.New(MAst.ConstantKeys, new Value[]
			{
				MAst.ConstantKind,
				value
			});
		}

		// Token: 0x04004BBF RID: 19391
		private readonly Keys keys;
	}
}
