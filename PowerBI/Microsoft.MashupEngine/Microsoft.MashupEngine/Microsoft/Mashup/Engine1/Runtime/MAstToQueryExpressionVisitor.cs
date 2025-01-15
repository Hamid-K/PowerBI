using System;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001580 RID: 5504
	internal class MAstToQueryExpressionVisitor
	{
		// Token: 0x06008911 RID: 35089 RVA: 0x001D0389 File Offset: 0x001CE589
		public MAstToQueryExpressionVisitor(Keys keys)
		{
			this.keys = keys;
		}

		// Token: 0x06008912 RID: 35090 RVA: 0x001D0398 File Offset: 0x001CE598
		public QueryExpression Visit(RecordValue ast)
		{
			string asString = ast["Kind"].AsString;
			if (asString == "Binary")
			{
				return this.VisitBinary(ast);
			}
			if (asString == "FieldAccess")
			{
				return this.VisitFieldAccess(ast);
			}
			if (asString == "Constant")
			{
				return MAstToQueryExpressionVisitor.VisitConstant(ast);
			}
			if (asString == "If")
			{
				return this.VisitIf(ast);
			}
			if (asString == "Invocation")
			{
				return this.VisitInvocation(ast);
			}
			if (!(asString == "Unary"))
			{
				throw new InvalidOperationException();
			}
			return this.VisitUnary(ast);
		}

		// Token: 0x06008913 RID: 35091 RVA: 0x001D043C File Offset: 0x001CE63C
		private QueryExpression VisitBinary(RecordValue binary)
		{
			return new BinaryQueryExpression((BinaryOperator2)Enum.Parse(typeof(BinaryOperator2), binary["Operator"].AsString), this.Visit(binary["Left"].AsRecord), this.Visit(binary["Right"].AsRecord));
		}

		// Token: 0x06008914 RID: 35092 RVA: 0x001D04A0 File Offset: 0x001CE6A0
		private QueryExpression VisitFieldAccess(RecordValue columnAccess)
		{
			int num;
			if (this.Visit(columnAccess["Expression"].AsRecord).Kind == QueryExpressionKind.ArgumentAccess && this.keys.TryGetKeyIndex(columnAccess["MemberName"].AsString, out num))
			{
				return new ColumnAccessQueryExpression(num);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06008915 RID: 35093 RVA: 0x001D04F6 File Offset: 0x001CE6F6
		private static QueryExpression VisitConstant(RecordValue constant)
		{
			return new ConstantQueryExpression(constant["Value"]);
		}

		// Token: 0x06008916 RID: 35094 RVA: 0x001D0508 File Offset: 0x001CE708
		private QueryExpression VisitIf(RecordValue ifExpr)
		{
			return new IfQueryExpression(this.Visit(ifExpr["Condition"].AsRecord), this.Visit(ifExpr["TrueCase"].AsRecord), this.Visit(ifExpr["FalseCase"].AsRecord));
		}

		// Token: 0x06008917 RID: 35095 RVA: 0x001D055C File Offset: 0x001CE75C
		private QueryExpression VisitInvocation(RecordValue invocation)
		{
			if (invocation == TableModule.ItemExpression.Item)
			{
				return ArgumentAccessQueryExpression.Instance;
			}
			return new InvocationQueryExpression(this.Visit(invocation["Function"].AsRecord), invocation["Arguments"].AsList.Select((IValueReference a) => this.Visit(a.Value.AsRecord)).ToArray<QueryExpression>());
		}

		// Token: 0x06008918 RID: 35096 RVA: 0x001D05B8 File Offset: 0x001CE7B8
		private QueryExpression VisitUnary(RecordValue unary)
		{
			return new UnaryQueryExpression((UnaryOperator2)Enum.Parse(typeof(UnaryOperator2), unary["Operator"].AsString), this.Visit(unary["Expression"].AsRecord));
		}

		// Token: 0x04004BBE RID: 19390
		private readonly Keys keys;
	}
}
