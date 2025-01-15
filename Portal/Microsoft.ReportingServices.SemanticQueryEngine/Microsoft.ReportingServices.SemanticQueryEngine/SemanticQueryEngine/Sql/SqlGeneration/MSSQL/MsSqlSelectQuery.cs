using System;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL
{
	// Token: 0x0200004E RID: 78
	internal class MsSqlSelectQuery : SqlSelectQuery
	{
		// Token: 0x06000398 RID: 920 RVA: 0x0000E24D File Offset: 0x0000C44D
		internal MsSqlSelectQuery(ModelEntity primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000E257 File Offset: 0x0000C457
		internal MsSqlSelectQuery(ISelectList primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0000E261 File Offset: 0x0000C461
		internal MsSqlSelectQuery(SqlBatch sqlBatch)
			: base(sqlBatch)
		{
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000111B3 File Offset: 0x0000F3B3
		protected override SqlExpression CreateSqlLiteralExpressionObject(IQPExpressionInfo qpInfo)
		{
			return new MsSqlLiteralExpression(qpInfo, base.SqlBatch);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000111C1 File Offset: 0x0000F3C1
		protected override SqlAggregateExpression CreateSqlAggregateExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument)
		{
			return new MsSqlAggregateExpression(qpInfo, functionContext, argument, base.SqlBatch);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000111D1 File Offset: 0x0000F3D1
		protected override SqlExpression CreateSqlFunctionExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments)
		{
			return new MsSqlFunctionExpression(qpInfo, functionContext, arguments, base.SqlBatch);
		}
	}
}
