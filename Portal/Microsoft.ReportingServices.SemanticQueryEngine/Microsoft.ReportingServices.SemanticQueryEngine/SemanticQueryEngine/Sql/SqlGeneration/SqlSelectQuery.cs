using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000037 RID: 55
	internal abstract class SqlSelectQuery : ISelectList, ISqlSnippet
	{
		// Token: 0x0600025B RID: 603 RVA: 0x0000B864 File Offset: 0x00009A64
		protected SqlSelectQuery(ModelEntity primaryTableSource, SqlBatch sqlBatch)
			: this(sqlBatch)
		{
			if (primaryTableSource.Binding == null)
			{
				throw new NotImplementedException("Calculated entities are not supported in SQL 2005.");
			}
			if (primaryTableSource.Binding is TableBinding)
			{
				SqlSelectTable sqlSelectTable = new SqlSelectTable(((TableBinding)primaryTableSource.Binding).GetTable(), sqlBatch);
				this.m_primaryTableSource = new SqlTableSource(sqlSelectTable, this.m_tableSourceAliasGen.CreateName(sqlSelectTable.CandidateAlias), this.m_sqlBatch);
				return;
			}
			if (primaryTableSource.Binding is ColumnBinding)
			{
				SqlSelectTable sqlSelectTable2 = new SqlSelectTable(((ColumnBinding)primaryTableSource.Binding).GetColumn(), sqlBatch);
				this.m_primaryTableSource = new SqlTableSource(sqlSelectTable2, this.m_tableSourceAliasGen.CreateName(sqlSelectTable2.CandidateAlias), this.m_sqlBatch);
				return;
			}
			throw SQEAssert.AssertFalseAndThrow("Unknown or invalid ((ModelEntity)primaryTableSource).Binding.", Array.Empty<object>());
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000B92C File Offset: 0x00009B2C
		protected SqlSelectQuery(ISelectList primaryTableSource, SqlBatch sqlBatch)
			: this(sqlBatch)
		{
			string text = null;
			SqlSelectQuery sqlSelectQuery = primaryTableSource as SqlSelectQuery;
			if (sqlSelectQuery != null && sqlSelectQuery.PrimaryTableSource != null)
			{
				text = sqlSelectQuery.PrimaryTableSource.Alias;
			}
			this.m_primaryTableSource = new SqlTableSource(primaryTableSource, this.m_tableSourceAliasGen.CreateName(text), this.m_sqlBatch);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000B980 File Offset: 0x00009B80
		protected SqlSelectQuery(SqlBatch sqlBatch)
		{
			if (sqlBatch == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("sqlBatch"));
			}
			this.m_sqlBatch = sqlBatch;
			this.m_selectColumnAliasGen = sqlBatch.CreateSqlAliasGenerator("c0");
			this.m_tableSourceAliasGen = sqlBatch.CreateSqlAliasGenerator("q0");
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000BA08 File Offset: 0x00009C08
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			if (this.m_selectList.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not compile when select list is empty.", Array.Empty<object>());
			}
			fsw.Write("SELECT");
			this.CompileSelectModifiers(fsw);
			bool flag = this.m_groupBy;
			if (flag && !this.m_sqlAggregatesGenerated)
			{
				fsw.Write(" DISTINCT");
				flag = false;
			}
			int num = fsw.IndentationLevel + 1;
			fsw.IndentationLevel = num;
			fsw.WriteLineIndent();
			for (int i = 0; i < this.m_selectList.Count; i++)
			{
				if (i > 0)
				{
					fsw.WriteLineIndent(",");
				}
				this.m_selectList[i].Compile(fsw);
			}
			num = fsw.IndentationLevel - 1;
			fsw.IndentationLevel = num;
			if (this.m_primaryTableSource != null)
			{
				fsw.WriteLineIndent();
				fsw.Write("FROM");
				num = fsw.IndentationLevel + 1;
				fsw.IndentationLevel = num;
				fsw.WriteLineIndent();
				((ISqlSnippet)this.m_primaryTableSource).Compile(fsw);
				for (int j = 0; j < this.m_joins.Count; j++)
				{
					fsw.WriteLineIndent();
					((ISqlSnippet)this.m_joins[j]).Compile(fsw);
				}
				num = fsw.IndentationLevel - 1;
				fsw.IndentationLevel = num;
				if (this.m_filterExpressions.Count > 0)
				{
					fsw.WriteLineIndent();
					fsw.Write("WHERE");
					num = fsw.IndentationLevel + 1;
					fsw.IndentationLevel = num;
					fsw.WriteLineIndent();
					for (int k = 0; k < this.m_filterExpressions.Count; k++)
					{
						if (k > 0)
						{
							fsw.WriteLineIndent(" AND");
						}
						this.m_filterExpressions[k].Compile(fsw);
					}
					num = fsw.IndentationLevel - 1;
					fsw.IndentationLevel = num;
				}
				if (flag)
				{
					bool flag2 = true;
					foreach (SqlSelectExpression sqlSelectExpression in this.m_selectExpressions.Values)
					{
						if (sqlSelectExpression.SqlExpression.CanGroupBy)
						{
							if (flag2)
							{
								fsw.WriteLineIndent();
								num = fsw.IndentationLevel + 1;
								fsw.IndentationLevel = num;
								fsw.WriteLineIndent("GROUP BY");
							}
							else
							{
								fsw.Write(", ");
							}
							((ISqlSnippet)sqlSelectExpression.SqlExpression).Compile(fsw);
							flag2 = false;
						}
					}
					if (!flag2)
					{
						num = fsw.IndentationLevel - 1;
						fsw.IndentationLevel = num;
					}
				}
				if (this.m_sortExpressions != null && this.m_sortExpressions.Count > 0)
				{
					fsw.WriteLineIndent();
					num = fsw.IndentationLevel + 1;
					fsw.IndentationLevel = num;
					fsw.WriteLineIndent("ORDER BY");
					this.CompileOrderByColumnClause(fsw, this.m_sortExpressions, this.m_selectList);
					num = fsw.IndentationLevel - 1;
					fsw.IndentationLevel = num;
					return;
				}
			}
			else
			{
				this.CompileDualPrimaryTableSource(fsw);
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
		{
			return this.FindSqlSelectExpressionInSelectList(expressionKey);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
		{
			return this.FindSqlSelectExpressionInSelectList(expressionKey);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
		{
			SqlSelectExpression sqlSelectExpression;
			if (this.m_aggregationFlags.TryGetValue(expressionKey, out sqlSelectExpression))
			{
				return sqlSelectExpression;
			}
			return null;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000BD00 File Offset: 0x00009F00
		SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
		{
			return this.m_aggregationFieldCountExpression;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00004B5D File Offset: 0x00002D5D
		bool ISelectList.UseParensWhenNested
		{
			[DebuggerStepThrough]
			get
			{
				return true;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BD08 File Offset: 0x00009F08
		internal void SelectExpression(SqlExpression sqlExpression, IQPExpressionInfo qpInfo)
		{
			this.SelectExpressionImpl(sqlExpression, qpInfo.Expression);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BD17 File Offset: 0x00009F17
		internal void SelectExpression(SqlExpression sqlExpression, DsvColumn dsvColumn)
		{
			this.SelectExpressionImpl(sqlExpression, dsvColumn);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BD24 File Offset: 0x00009F24
		internal void SelectAggregationFlag(IQPExpressionInfo info, bool value)
		{
			if (this.m_aggregationFieldCountExpression != null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			SqlSnippetExpression sqlSnippetExpression = new SqlSnippetExpression(value ? this.m_sqlBatch.SqlBitTrueSnippet : this.m_sqlBatch.SqlBitFalseSnippet, false);
			if (sqlSnippetExpression.Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			string text = this.m_selectColumnAliasGen.CreateName(SqlExpression.GetCandidateAlias(info)) + "_is_agg";
			this.m_selectColumnAliasGen.AddExistingName(text);
			SqlSelectExpression sqlSelectExpression = new SqlSelectExpression(sqlSnippetExpression, new string[] { text }, this.m_sqlBatch);
			this.m_selectList.Add(sqlSelectExpression);
			this.m_aggregationFlags.Add(info.Expression, sqlSelectExpression);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		internal void SelectAggregationFieldCount(int aggregationFlagsCount)
		{
			if (this.m_aggregationFieldCountExpression != null)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			SqlExpression sqlExpression = new SqlSnippetExpression(this.SqlBatch.CreateSqlSnippetForInteger((long)aggregationFlagsCount), false);
			if (sqlExpression.Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			string text = this.m_selectColumnAliasGen.CreateName("agg_row_count");
			this.m_aggregationFieldCountExpression = new SqlSelectExpression(sqlExpression, new string[] { text }, this.m_sqlBatch);
			this.m_selectList.Add(this.m_aggregationFieldCountExpression);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BE51 File Offset: 0x0000A051
		internal void AddFilterExpression(SqlExpression sqlExpression)
		{
			if (sqlExpression.IsLogicalBooleanValue)
			{
				this.m_filterExpressions.Add(sqlExpression);
				return;
			}
			this.m_filterExpressions.Add(SqlFunctionExpression.ConvertToBoolean(sqlExpression));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BE7C File Offset: 0x0000A07C
		internal void OrderBy(object expressionKey)
		{
			if (this.m_sortExpressions == null)
			{
				this.m_sortExpressions = new List<SqlSelectExpression>();
			}
			SqlSelectExpression sqlSelectExpression = this.FindSqlSelectExpressionInSelectList(expressionKey);
			if (sqlSelectExpression == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not find select expression.", Array.Empty<object>());
			}
			this.m_sortExpressions.Add(sqlSelectExpression);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		internal virtual SqlExpression CreateSqlPassThruExpression(IQPExpressionInfo qpInfo, SqlTableSource tableSource)
		{
			if (qpInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("qpInfo"));
			}
			return new SqlPassThruExpression(tableSource, qpInfo.Expression, this.m_sqlBatch, qpInfo.Nullable);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		internal SqlExpression CreateSqlLiteralExpression(IQPExpressionInfo qpInfo)
		{
			return this.CreateSqlLiteralExpressionObject(qpInfo);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000BEF9 File Offset: 0x0000A0F9
		internal SqlExpression CreateSqlNullExpression(IQPExpressionInfo qpInfo)
		{
			if (qpInfo == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("qpInfo"));
			}
			return new SqlNullExpression(qpInfo);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000BF14 File Offset: 0x0000A114
		internal SqlExpression CreateSqlAggregateExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument)
		{
			SqlAggregateExpression sqlAggregateExpression = this.CreateSqlAggregateExpressionObject(qpInfo, functionContext, argument);
			this.m_sqlAggregatesGenerated |= sqlAggregateExpression.IsSqlAggregateGenerated;
			return sqlAggregateExpression;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000BF3F File Offset: 0x0000A13F
		internal SqlExpression CreateSqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments)
		{
			return this.CreateSqlFunctionExpressionObject(qpInfo, functionContext, arguments);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000BF4C File Offset: 0x0000A14C
		internal virtual SqlExpression CreateSqlExpressionAsNull(SqlExpression original, IQPExpressionInfo info)
		{
			if (original == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("original"));
			}
			SqlTupleExpression sqlTupleExpression = new SqlTupleExpression();
			for (int i = 0; i < original.Values.Count; i++)
			{
				sqlTupleExpression.AddTupleValue(SqlExpression.NullSnippet, true);
			}
			return sqlTupleExpression;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000BF98 File Offset: 0x0000A198
		internal SqlTableSource Join(IList<DsvColumn> parentJoinColumns, SqlSelectQuery nestedQuery, IList<DsvColumn> nestedJoinColumns, bool optional)
		{
			if (this.m_primaryTableSource == null || nestedQuery.PrimaryTableSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Primary table source is not specified.", Array.Empty<object>());
			}
			if (parentJoinColumns.Count != nestedJoinColumns.Count || parentJoinColumns.Count == 0)
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid join columns.", Array.Empty<object>());
			}
			SqlTableSource sqlTableSource = new SqlTableSource(nestedQuery, this.m_tableSourceAliasGen.CreateName(nestedQuery.PrimaryTableSource.Alias), this.m_sqlBatch);
			SqlExpression[] array = new SqlExpression[parentJoinColumns.Count];
			SqlExpression[] array2 = new SqlExpression[nestedJoinColumns.Count];
			for (int i = 0; i < parentJoinColumns.Count; i++)
			{
				array[i] = new SqlPassThruExpression(this.m_primaryTableSource, parentJoinColumns[i], this.m_sqlBatch, parentJoinColumns[i].Nullable);
				array2[i] = new SqlPassThruExpression(sqlTableSource, nestedJoinColumns[i], this.m_sqlBatch, nestedJoinColumns[i].Nullable);
			}
			this.m_joins.Add(new SqlSelectQuery.SqlJoin(array, sqlTableSource, array2, optional));
			nestedQuery.SetParentQuery(this);
			return sqlTableSource;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		internal void SelectJoinColumns(IList<DsvColumn> joinColumns)
		{
			if (this.m_primaryTableSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Primary table source is not specified.", Array.Empty<object>());
			}
			for (int i = 0; i < joinColumns.Count; i++)
			{
				DsvColumn dsvColumn = joinColumns[i];
				SqlExpression sqlExpression = this.CreateSqlPassThruExpressionForJoinColumn(dsvColumn);
				this.SelectExpression(sqlExpression, dsvColumn);
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000C0F0 File Offset: 0x0000A2F0
		internal SqlTableSource Join(SqlSelectQuery nestedQuery, IQPExpressionInfoCollection keyExpressions, bool optional)
		{
			if (this.m_primaryTableSource == null || nestedQuery.PrimaryTableSource == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Primary table source is not specified.", Array.Empty<object>());
			}
			if (keyExpressions == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("keyExpressions"));
			}
			SqlTableSource sqlTableSource = new SqlTableSource(nestedQuery, this.m_tableSourceAliasGen.CreateName(nestedQuery.PrimaryTableSource.Alias), this.m_sqlBatch);
			if (keyExpressions.Count == 0)
			{
				this.m_joins.Add(new SqlSelectQuery.SqlJoin(sqlTableSource));
			}
			else
			{
				SqlExpression[] array = new SqlExpression[keyExpressions.Count];
				SqlExpression[] array2 = new SqlExpression[keyExpressions.Count];
				for (int i = 0; i < keyExpressions.Count; i++)
				{
					Expression expression = keyExpressions[i].Expression;
					bool nullable = expression.GetResultType().Nullable;
					array[i] = new SqlPassThruExpression(this.m_primaryTableSource, expression, this.m_sqlBatch, nullable);
					array2[i] = new SqlPassThruExpression(sqlTableSource, expression, this.m_sqlBatch, nullable);
				}
				this.m_joins.Add(new SqlSelectQuery.SqlJoin(array, sqlTableSource, array2, optional));
			}
			nestedQuery.SetParentQuery(this);
			return sqlTableSource;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000C201 File Offset: 0x0000A401
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000C209 File Offset: 0x0000A409
		internal bool GroupBy
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_groupBy;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_groupBy = value;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000C212 File Offset: 0x0000A412
		internal SqlTableSource PrimaryTableSource
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_primaryTableSource;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000C21A File Offset: 0x0000A41A
		internal ReadOnlyItemCollectionBase<SqlSelectQuery.SqlJoin> Joins
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_joins;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000C222 File Offset: 0x0000A422
		internal SqlSelectQuery ParentQuery
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_parentQuery;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000C22A File Offset: 0x0000A42A
		internal SqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_sqlBatch;
			}
		}

		// Token: 0x06000279 RID: 633
		protected abstract SqlExpression CreateSqlLiteralExpressionObject(IQPExpressionInfo qpInfo);

		// Token: 0x0600027A RID: 634
		protected abstract SqlAggregateExpression CreateSqlAggregateExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression argument);

		// Token: 0x0600027B RID: 635
		protected abstract SqlExpression CreateSqlFunctionExpressionObject(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments);

		// Token: 0x0600027C RID: 636 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected virtual void CompileSelectModifiers(FormattedStringWriter fsw)
		{
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected virtual void CompileDualPrimaryTableSource(FormattedStringWriter fsw)
		{
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C234 File Offset: 0x0000A434
		protected virtual void CompileOrderByColumnClause(FormattedStringWriter fsw, List<SqlSelectExpression> sortExpressions, SqlSnippetCollection selectList)
		{
			for (int i = 0; i < sortExpressions.Count; i++)
			{
				SqlSelectExpression sqlSelectExpression = sortExpressions[i];
				for (int j = 0; j < sqlSelectExpression.Aliases.Length; j++)
				{
					if (i > 0 || j > 0)
					{
						fsw.Write(", ");
					}
					fsw.Write(this.m_sqlBatch.GetDelimitedIdentifier(sqlSelectExpression.Aliases[j]));
				}
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C299 File Offset: 0x0000A499
		private SqlExpression CreateSqlPassThruExpressionForJoinColumn(DsvColumn joinColumn)
		{
			return new SqlPassThruExpression(this.m_primaryTableSource, joinColumn, this.m_sqlBatch, joinColumn.Nullable);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000C2B4 File Offset: 0x0000A4B4
		private SqlSelectExpression FindSqlSelectExpressionInSelectList(object expressionKey)
		{
			SqlSelectExpression sqlSelectExpression;
			if (this.m_selectExpressions.TryGetValue(expressionKey, out sqlSelectExpression))
			{
				return sqlSelectExpression;
			}
			return null;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		private void SelectExpressionImpl(SqlExpression sqlExpression, object expressionKey)
		{
			string[] array = new string[sqlExpression.Values.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.m_selectColumnAliasGen.CreateName(SqlExpression.GetCandidateAlias(expressionKey));
			}
			SqlSelectExpression sqlSelectExpression = new SqlSelectExpression(sqlExpression, array, this.m_sqlBatch);
			this.m_selectList.Add(sqlSelectExpression);
			this.m_selectExpressions.Add(expressionKey, sqlSelectExpression);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C33B File Offset: 0x0000A53B
		private void SetParentQuery(SqlSelectQuery parentQuery)
		{
			if (parentQuery == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("parentQuery"));
			}
			if (this.m_parentQuery != null)
			{
				throw SQEAssert.AssertFalseAndThrow("Attempt to reset m_parentQuery.", Array.Empty<object>());
			}
			this.m_parentQuery = parentQuery;
		}

		// Token: 0x040000DE RID: 222
		private readonly SqlTableSource m_primaryTableSource;

		// Token: 0x040000DF RID: 223
		private bool m_groupBy;

		// Token: 0x040000E0 RID: 224
		private readonly SqlSnippetCollection m_selectList = new SqlSnippetCollection();

		// Token: 0x040000E1 RID: 225
		private readonly Dictionary<object, SqlSelectExpression> m_selectExpressions = new Dictionary<object, SqlSelectExpression>();

		// Token: 0x040000E2 RID: 226
		private readonly ReadOnlyItemCollectionBase<SqlSelectQuery.SqlJoin> m_joins = new ReadOnlyItemCollectionBase<SqlSelectQuery.SqlJoin>();

		// Token: 0x040000E3 RID: 227
		private SqlSelectQuery m_parentQuery;

		// Token: 0x040000E4 RID: 228
		private readonly Dictionary<Expression, SqlSelectExpression> m_aggregationFlags = new Dictionary<Expression, SqlSelectExpression>();

		// Token: 0x040000E5 RID: 229
		private SqlSelectExpression m_aggregationFieldCountExpression;

		// Token: 0x040000E6 RID: 230
		private readonly SqlSnippetCollection m_filterExpressions = new SqlSnippetCollection();

		// Token: 0x040000E7 RID: 231
		private List<SqlSelectExpression> m_sortExpressions;

		// Token: 0x040000E8 RID: 232
		private readonly INameGenerator m_selectColumnAliasGen;

		// Token: 0x040000E9 RID: 233
		private readonly INameGenerator m_tableSourceAliasGen;

		// Token: 0x040000EA RID: 234
		private readonly SqlBatch m_sqlBatch;

		// Token: 0x040000EB RID: 235
		private bool m_sqlAggregatesGenerated;

		// Token: 0x020000C6 RID: 198
		internal sealed class SqlJoin : ISqlSnippet
		{
			// Token: 0x06000719 RID: 1817 RVA: 0x0001BA66 File Offset: 0x00019C66
			internal SqlJoin(SqlTableSource nestedTableSource)
			{
				if (nestedTableSource == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("nestedTableSource"));
				}
				this.m_nestedTableSource = nestedTableSource;
			}

			// Token: 0x0600071A RID: 1818 RVA: 0x0001BA88 File Offset: 0x00019C88
			internal SqlJoin(SqlExpression[] parentJoinValues, SqlTableSource nestedTableSource, SqlExpression[] nestedJoinValues, bool optional)
				: this(nestedTableSource)
			{
				if (parentJoinValues == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("parentJoinValues"));
				}
				if (nestedJoinValues == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("nestedJoinValues"));
				}
				this.m_parentJoinValues = parentJoinValues;
				this.m_nestedJoinValues = nestedJoinValues;
				this.m_optional = optional;
			}

			// Token: 0x0600071B RID: 1819 RVA: 0x0001BAD8 File Offset: 0x00019CD8
			void ISqlSnippet.Compile(FormattedStringWriter fsw)
			{
				if (this.m_parentJoinValues == null && this.m_nestedJoinValues == null)
				{
					fsw.Write("CROSS JOIN ");
					((ISqlSnippet)this.m_nestedTableSource).Compile(fsw);
					return;
				}
				if (this.m_optional)
				{
					fsw.Write("LEFT OUTER JOIN ");
				}
				else
				{
					fsw.Write("INNER JOIN ");
				}
				((ISqlSnippet)this.m_nestedTableSource).Compile(fsw);
				fsw.Write(" ON ");
				if (this.m_parentJoinValues.Length != this.m_nestedJoinValues.Length || this.m_parentJoinValues.Length == 0)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				for (int i = 0; i < this.m_parentJoinValues.Length; i++)
				{
					if (i > 0)
					{
						fsw.Write(" AND ");
					}
					SqlExpression sqlExpression = this.m_parentJoinValues[i];
					SqlExpression sqlExpression2 = this.m_nestedJoinValues[i];
					IList<ISqlSnippet> values = sqlExpression.Values;
					IList<ISqlSnippet> values2 = sqlExpression2.Values;
					if (values.Count != values2.Count)
					{
						throw SQEAssert.AssertFalseAndThrow("Found a pair of composite expressions with different number of nested values.", Array.Empty<object>());
					}
					bool flag = sqlExpression.IsNullable && sqlExpression2.IsNullable;
					for (int j = 0; j < values.Count; j++)
					{
						if (j > 0)
						{
							fsw.Write(" AND ");
						}
						if (flag)
						{
							fsw.Write("(");
						}
						ISqlSnippet sqlSnippet = values[j];
						ISqlSnippet sqlSnippet2 = values2[j];
						sqlSnippet.Compile(fsw);
						fsw.Write(" = ");
						sqlSnippet2.Compile(fsw);
						if (flag)
						{
							fsw.Write(" OR ");
							sqlSnippet.Compile(fsw);
							fsw.Write(" IS NULL");
							fsw.Write(" AND ");
							sqlSnippet2.Compile(fsw);
							fsw.Write(" IS NULL");
							fsw.Write(")");
						}
					}
				}
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001BC90 File Offset: 0x00019E90
			internal SqlTableSource NestedTableSource
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_nestedTableSource;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001BC98 File Offset: 0x00019E98
			internal bool Optional
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_optional;
				}
			}

			// Token: 0x0400038B RID: 907
			private readonly SqlTableSource m_nestedTableSource;

			// Token: 0x0400038C RID: 908
			private readonly SqlExpression[] m_parentJoinValues;

			// Token: 0x0400038D RID: 909
			private readonly SqlExpression[] m_nestedJoinValues;

			// Token: 0x0400038E RID: 910
			private readonly bool m_optional;
		}
	}
}
