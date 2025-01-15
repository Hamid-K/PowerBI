using System;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL
{
	// Token: 0x0200004C RID: 76
	internal sealed class MsSqlAggregateExpression : SqlAggregateExpression
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0000C7A3 File Offset: 0x0000A9A3
		internal MsSqlAggregateExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument, SqlBatch sqlBatch)
			: base(qpInfo, functionContext, argument, sqlBatch)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000101F7 File Offset: 0x0000E3F7
		protected override ISqlSnippet CreateBasicSqlSnippetForMax(ISqlSnippet argument, bool degenerate)
		{
			return base.CreateBasicSqlSnippetForMax(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument), degenerate);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010217 File Offset: 0x0000E417
		protected override ISqlSnippet CreateBasicSqlSnippetForMin(ISqlSnippet argument, bool degenerate)
		{
			return base.CreateBasicSqlSnippetForMin(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument), degenerate);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010238 File Offset: 0x0000E438
		protected override ISqlSnippet CreateBasicSqlSnippetForCount(ISqlSnippet argument, bool distinct)
		{
			argument = MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument);
			if (distinct)
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					MsSqlAggregateExpression.CountBigOpenParenDistinctSnippet,
					argument,
					SqlExpression.CloseParenSnippet
				});
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlAggregateExpression.CountBigOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x04000184 RID: 388
		private static readonly ISqlSnippet CountBigOpenParenSnippet = new SqlStringSnippet("COUNT_BIG(");

		// Token: 0x04000185 RID: 389
		private static readonly ISqlSnippet CountBigOpenParenDistinctSnippet = new SqlStringSnippet("COUNT_BIG(DISTINCT ");
	}
}
