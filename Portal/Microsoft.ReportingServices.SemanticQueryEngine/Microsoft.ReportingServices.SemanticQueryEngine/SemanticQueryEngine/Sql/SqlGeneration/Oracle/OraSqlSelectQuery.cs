using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle
{
	// Token: 0x02000046 RID: 70
	internal sealed class OraSqlSelectQuery : SqlSelectQuery
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000E24D File Offset: 0x0000C44D
		internal OraSqlSelectQuery(ModelEntity primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E257 File Offset: 0x0000C457
		internal OraSqlSelectQuery(ISelectList primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E261 File Offset: 0x0000C461
		internal OraSqlSelectQuery(SqlBatch sqlBatch)
			: base(sqlBatch)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000FBD0 File Offset: 0x0000DDD0
		protected override SqlExpression CreateSqlLiteralExpressionObject(IQPExpressionInfo qpInfo)
		{
			LiteralNode nodeAsLiteral = qpInfo.Expression.NodeAsLiteral;
			if (nodeAsLiteral != null && nodeAsLiteral.DataType == DataType.String && nodeAsLiteral.Cardinality == Cardinality.One && nodeAsLiteral.ValueAsString == "")
			{
				return SqlNullExpression.SqlNull;
			}
			return new OraSqlLiteralExpression(qpInfo, this.SqlBatch);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000FC20 File Offset: 0x0000DE20
		protected override SqlAggregateExpression CreateSqlAggregateExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument)
		{
			return new OraSqlAggregateExpression(qpInfo, functionContext, argument, this.SqlBatch);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000FC30 File Offset: 0x0000DE30
		protected override SqlExpression CreateSqlFunctionExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments)
		{
			return new OraSqlFunctionExpression(qpInfo, functionContext, arguments, this.SqlBatch);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000FC40 File Offset: 0x0000DE40
		protected override void CompileSelectModifiers(FormattedStringWriter fsw)
		{
			if (this.SqlBatch.EnableNO_MERGEInLeftOuters && (this.SqlBatch.ServerMajorVersion == 9 || this.SqlBatch.ServerMajorVersion == 10) && OraSqlSelectQuery.IsLeftOuterQuery(this))
			{
				bool flag = true;
				if (this.SqlBatch.ServerMajorVersion == 9)
				{
					flag = !OraSqlSelectQuery.HasLeftOuterJoins(this);
				}
				if (flag)
				{
					fsw.Write(" /*+ NO_MERGE */");
				}
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000FCA9 File Offset: 0x0000DEA9
		protected override void CompileDualPrimaryTableSource(FormattedStringWriter fsw)
		{
			fsw.Write(" FROM DUAL");
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000FCB6 File Offset: 0x0000DEB6
		private new OraSqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return (OraSqlBatch)base.SqlBatch;
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		private static bool IsLeftOuterQuery(OraSqlSelectQuery query)
		{
			if (query.ParentQuery != null)
			{
				if (query.SqlBatch.LeftOuterInfo.ContainsKey(query))
				{
					return true;
				}
				foreach (SqlSelectQuery.SqlJoin sqlJoin in query.ParentQuery.Joins)
				{
					if (sqlJoin.Optional && sqlJoin.NestedTableSource.TuplesSource == query)
					{
						query.SqlBatch.LeftOuterInfo.Add(query, null);
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000FD64 File Offset: 0x0000DF64
		private static bool HasLeftOuterJoins(OraSqlSelectQuery query)
		{
			bool? flag = null;
			bool flag2 = OraSqlSelectQuery.IsLeftOuterQuery(query);
			if (flag2 && !query.SqlBatch.LeftOuterInfo.TryGetValue(query, out flag))
			{
				throw SQEAssert.AssertFalseAndThrow("Left outer info is missing for the current left outer query.", Array.Empty<object>());
			}
			if (flag == null)
			{
				flag = new bool?(false);
				foreach (SqlSelectQuery.SqlJoin sqlJoin in query.Joins)
				{
					if (sqlJoin.Optional)
					{
						flag = new bool?(true);
						break;
					}
					OraSqlSelectQuery oraSqlSelectQuery = sqlJoin.NestedTableSource.TuplesSource as OraSqlSelectQuery;
					if (oraSqlSelectQuery != null && OraSqlSelectQuery.HasLeftOuterJoins(oraSqlSelectQuery))
					{
						flag = new bool?(true);
						break;
					}
				}
				if (flag2)
				{
					query.SqlBatch.LeftOuterInfo[query] = flag;
				}
			}
			return flag.Value;
		}
	}
}
