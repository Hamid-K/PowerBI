using System;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle
{
	// Token: 0x02000044 RID: 68
	internal sealed class OraSqlAggregateExpression : SqlAggregateExpression
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000C7A3 File Offset: 0x0000A9A3
		internal OraSqlAggregateExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument, OraSqlBatch sqlBatch)
			: base(qpInfo, functionContext, argument, sqlBatch)
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000E778 File Offset: 0x0000C978
		protected override SqlExpression GetEntityRefCountArgument()
		{
			return OraSqlAggregateExpression.EntityRefCountArgument;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000E77F File Offset: 0x0000C97F
		protected override ISqlSnippet CreateBasicSqlSnippetForStDev(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				pop ? OraSqlAggregateExpression.StdDevPopOpenParenSnippet : OraSqlAggregateExpression.StdDevSampOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		protected override ISqlSnippet CreateBasicSqlSnippetForVar(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				pop ? OraSqlAggregateExpression.VarPopOpenParenSnippet : OraSqlAggregateExpression.VarSampOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x04000144 RID: 324
		private static readonly ISqlSnippet VarSampOpenParenSnippet = new SqlStringSnippet("VAR_SAMP(");

		// Token: 0x04000145 RID: 325
		private static readonly ISqlSnippet VarPopOpenParenSnippet = new SqlStringSnippet("VAR_POP(");

		// Token: 0x04000146 RID: 326
		private static readonly ISqlSnippet StdDevSampOpenParenSnippet = new SqlStringSnippet("STDDEV_SAMP(");

		// Token: 0x04000147 RID: 327
		private static readonly ISqlSnippet StdDevPopOpenParenSnippet = new SqlStringSnippet("STDDEV_POP(");

		// Token: 0x04000148 RID: 328
		private static readonly SqlExpression EntityRefCountArgument = new SqlSnippetExpression(new SqlStringSnippet("ROWNUM"), false);
	}
}
