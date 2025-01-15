using System;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQLADW
{
	// Token: 0x02000048 RID: 72
	internal sealed class MsSqlAdwSelectQuery : MsSqlSelectQuery
	{
		// Token: 0x06000350 RID: 848 RVA: 0x0000FEAC File Offset: 0x0000E0AC
		internal MsSqlAdwSelectQuery(ModelEntity primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000FEB6 File Offset: 0x0000E0B6
		internal MsSqlAdwSelectQuery(ISelectList primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		internal MsSqlAdwSelectQuery(SqlBatch sqlBatch)
			: base(sqlBatch)
		{
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000FEC9 File Offset: 0x0000E0C9
		protected override void CompileDualPrimaryTableSource(FormattedStringWriter fsw)
		{
			fsw.Write(" FROM sys.databases WHERE name='dwsys'");
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000FED6 File Offset: 0x0000E0D6
		protected override SqlExpression CreateSqlFunctionExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments)
		{
			return new MsSqlAdwFunctionExpression(qpInfo, functionContext, arguments, base.SqlBatch);
		}
	}
}
