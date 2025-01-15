using System;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQLADW
{
	// Token: 0x02000049 RID: 73
	internal class MsSqlAdwFunctionExpression : MsSqlFunctionExpression
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000FEE6 File Offset: 0x0000E0E6
		internal MsSqlAdwFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, SqlBatch sqlBatch)
			: base(qpInfo, functionContext, arguments, sqlBatch)
		{
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfWeek(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlAdwFunctionExpression.DPDayOfWeekOpenSnippet,
				argument,
				MsSqlAdwFunctionExpression.DayOfWeekCloseSnippet
			});
		}

		// Token: 0x04000180 RID: 384
		private static readonly ISqlSnippet DPDayOfWeekOpenSnippet = new SqlStringSnippet("((DATEPART(WEEKDAY, ");

		// Token: 0x04000181 RID: 385
		private static readonly ISqlSnippet DayOfWeekCloseSnippet = new SqlStringSnippet(") + 1 - 2) % 7 + 1)");
	}
}
