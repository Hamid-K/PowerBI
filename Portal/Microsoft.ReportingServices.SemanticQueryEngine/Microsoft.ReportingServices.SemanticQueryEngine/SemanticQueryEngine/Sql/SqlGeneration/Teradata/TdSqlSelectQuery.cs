using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata
{
	// Token: 0x02000041 RID: 65
	internal sealed class TdSqlSelectQuery : SqlSelectQuery
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000E24D File Offset: 0x0000C44D
		internal TdSqlSelectQuery(ModelEntity primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E257 File Offset: 0x0000C457
		internal TdSqlSelectQuery(ISelectList primaryTableSource, SqlBatch sqlBatch)
			: base(primaryTableSource, sqlBatch)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000E261 File Offset: 0x0000C461
		internal TdSqlSelectQuery(SqlBatch sqlBatch)
			: base(sqlBatch)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000E26C File Offset: 0x0000C46C
		internal override SqlExpression CreateSqlPassThruExpression(IQPExpressionInfo qpInfo, SqlTableSource tableSource)
		{
			SqlExpression sqlExpression = base.CreateSqlPassThruExpression(qpInfo, tableSource);
			if (this.SqlBatch.ServerMajorVersion < 12 && qpInfo.Expression.GetResultType().DataType == DataType.DateTime && tableSource.TuplesSource is SqlSelectTable && qpInfo.Expression.NodeAsAttributeRef != null && qpInfo.Expression.NodeAsAttributeRef.Attribute != null && qpInfo.Expression.NodeAsAttributeRef.Attribute.ModelAttribute != null)
			{
				DsvColumn column = qpInfo.Expression.NodeAsAttributeRef.Attribute.ModelAttribute.Binding.GetColumn();
				if (column != null && string.Compare(column.DbDataType, "TIMESTAMP", StringComparison.OrdinalIgnoreCase) != 0)
				{
					SqlPassThruExpression sqlPassThruExpression = sqlExpression as SqlPassThruExpression;
					if (sqlPassThruExpression != null && sqlPassThruExpression.Values.Count == 1)
					{
						sqlExpression = new SqlSnippetExpression(TdSqlFunctionExpression.CastAsTimestamp(sqlPassThruExpression), sqlPassThruExpression.IsNullable);
					}
				}
			}
			return sqlExpression;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000E357 File Offset: 0x0000C557
		protected override SqlExpression CreateSqlLiteralExpressionObject(IQPExpressionInfo qpInfo)
		{
			return new TdSqlLiteralExpression(qpInfo, this.SqlBatch);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000E365 File Offset: 0x0000C565
		protected override SqlAggregateExpression CreateSqlAggregateExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument)
		{
			return new TdSqlAggregateExpression(qpInfo, functionContext, argument, this.SqlBatch);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000E375 File Offset: 0x0000C575
		protected override SqlExpression CreateSqlFunctionExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments)
		{
			return new TdSqlFunctionExpression(qpInfo, functionContext, arguments, this.SqlBatch);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000E388 File Offset: 0x0000C588
		internal override SqlExpression CreateSqlExpressionAsNull(SqlExpression original, IQPExpressionInfo info)
		{
			if (original == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("original"));
			}
			DataType dataType = info.Expression.GetResultType().DataType;
			SqlCompositeSnippet sqlCompositeSnippet;
			if (dataType != DataType.String)
			{
				if (dataType != DataType.DateTime)
				{
					return base.CreateSqlExpressionAsNull(original, info);
				}
				sqlCompositeSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					SqlExpression.NullSnippet,
					TdSqlFunctionExpression.AsTimestamp0CloseParenSnippet
				});
			}
			else
			{
				sqlCompositeSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					SqlExpression.NullSnippet,
					TdSqlFunctionExpression.AsVarChar2CloseParenSnippet
				});
			}
			SqlTupleExpression sqlTupleExpression = new SqlTupleExpression();
			for (int i = 0; i < original.Values.Count; i++)
			{
				sqlTupleExpression.AddTupleValue(sqlCompositeSnippet, true);
			}
			return sqlTupleExpression;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E441 File Offset: 0x0000C641
		protected override void CompileDualPrimaryTableSource(FormattedStringWriter fsw)
		{
			fsw.Write(" FROM (SELECT 1 AS A_DUMMY_COLUMN_X_) AS A_DUMMY_TABLE_X_ ");
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E450 File Offset: 0x0000C650
		protected override void CompileOrderByColumnClause(FormattedStringWriter fsw, List<SqlSelectExpression> sortExpressions, SqlSnippetCollection selectList)
		{
			for (int i = 0; i < sortExpressions.Count; i++)
			{
				SqlSelectExpression sqlSelectExpression = sortExpressions[i];
				bool flag = false;
				for (int j = 0; j < selectList.Count; j++)
				{
					if (sqlSelectExpression == selectList[j])
					{
						if (i > 0)
						{
							fsw.Write(", ");
						}
						fsw.Write(" {0}", new object[] { j + 1 });
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int k = 0; k < sqlSelectExpression.Aliases.Length; k++)
					{
						if (i > 0 || k > 0)
						{
							fsw.Write(", ");
						}
						fsw.Write(this.SqlBatch.GetDelimitedIdentifier(sqlSelectExpression.Aliases[k]));
					}
				}
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000E510 File Offset: 0x0000C710
		private new TdSqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return (TdSqlBatch)base.SqlBatch;
			}
		}
	}
}
