using System;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata
{
	// Token: 0x0200003F RID: 63
	internal sealed class TdSqlAggregateExpression : SqlAggregateExpression
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000C7A3 File Offset: 0x0000A9A3
		internal TdSqlAggregateExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument, TdSqlBatch sqlBatch)
			: base(qpInfo, functionContext, argument, sqlBatch)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C7B0 File Offset: 0x0000A9B0
		protected override ISqlSnippet CreateBasicSqlSnippetForStDev(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				pop ? TdSqlAggregateExpression.StdDevPopOpenParenSnippet : TdSqlAggregateExpression.StdDevSampOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet,
				SqlExpression.AsFloatCloseParenSnippet
			});
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C7EB File Offset: 0x0000A9EB
		protected override ISqlSnippet CreateBasicSqlSnippetForVar(ISqlSnippet argument, bool pop)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				pop ? TdSqlAggregateExpression.VarPopOpenParenSnippet : TdSqlAggregateExpression.VarSampOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet,
				SqlExpression.AsFloatCloseParenSnippet
			});
		}

		// Token: 0x040000F6 RID: 246
		private static readonly ISqlSnippet VarSampOpenParenSnippet = new SqlStringSnippet("VAR_SAMP(");

		// Token: 0x040000F7 RID: 247
		private static readonly ISqlSnippet VarPopOpenParenSnippet = new SqlStringSnippet("VAR_POP(");

		// Token: 0x040000F8 RID: 248
		private static readonly ISqlSnippet StdDevSampOpenParenSnippet = new SqlStringSnippet("STDDEV_SAMP(");

		// Token: 0x040000F9 RID: 249
		private static readonly ISqlSnippet StdDevPopOpenParenSnippet = new SqlStringSnippet("STDDEV_POP(");
	}
}
