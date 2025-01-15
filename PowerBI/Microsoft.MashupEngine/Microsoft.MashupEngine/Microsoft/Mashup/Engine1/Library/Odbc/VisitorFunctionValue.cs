using System;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000659 RID: 1625
	internal class VisitorFunctionValue : NativeFunctionValue1<RecordValue, RecordValue>
	{
		// Token: 0x0600337A RID: 13178 RVA: 0x000A47F6 File Offset: 0x000A29F6
		public VisitorFunctionValue(Keys keys, OdbcQueryExpressionVisitor visitor)
			: base(TypeValue.Any, "ast", TypeValue.Record)
		{
			this.keys = keys;
			this.visitor = visitor;
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x000A481C File Offset: 0x000A2A1C
		public override RecordValue TypedInvoke(RecordValue ast)
		{
			QueryExpression queryExpression = new MAstToQueryExpressionVisitor(this.keys).Visit(ast);
			OdbcSqlExpression odbcSqlExpression = this.visitor.Visit(queryExpression);
			OdbcSqlExpressionKind kind = odbcSqlExpression.Kind;
			SqlExpression sqlExpression;
			if (kind != OdbcSqlExpressionKind.Condition)
			{
				if (kind != OdbcSqlExpressionKind.Scalar)
				{
					throw new FoldingFailureException("Folding failed. More details are available in the trace.");
				}
				sqlExpression = odbcSqlExpression.AsScalar.AsScalar.Expression;
			}
			else
			{
				sqlExpression = odbcSqlExpression.AsCondition.AsCondition.Expression;
			}
			return SqlExpressionToMAstVisitor.ToMAst(sqlExpression).Concatenate(RecordValue.New(Keys.New("Type"), new Value[] { odbcSqlExpression.TypeValue })).AsRecord;
		}

		// Token: 0x040016E3 RID: 5859
		private readonly Keys keys;

		// Token: 0x040016E4 RID: 5860
		private readonly OdbcQueryExpressionVisitor visitor;
	}
}
