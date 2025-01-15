using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000036 RID: 54
	internal sealed class SqlSelectTable : ISelectList, ISqlSnippet
	{
		// Token: 0x0600024A RID: 586 RVA: 0x0000B167 File Offset: 0x00009367
		internal SqlSelectTable(DsvTable dsvTable, SqlBatch sqlBatch)
		{
			this.Init(dsvTable, sqlBatch);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000B190 File Offset: 0x00009390
		internal SqlSelectTable(DsvColumn dsvColumn, SqlBatch sqlBatch)
		{
			if (dsvColumn == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("dsvColumn"));
			}
			this.m_dsvColumn = dsvColumn;
			this.Init(this.m_dsvColumn.Table, sqlBatch);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000B1E8 File Offset: 0x000093E8
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			if (this.m_dsvColumn != null && this.m_selectedColumns.Count == 0)
			{
				string text;
				this.CreateDsvColumnSqlExpression(this.m_dsvColumn, out text);
			}
			SqlTableSource sqlTableSource = this.m_tableSource;
			if (this.m_hasLogicalColumns)
			{
				SqlSelectTable.SqlSelectSnippet sqlSelectSnippet = new SqlSelectTable.SqlSelectSnippet(sqlTableSource);
				foreach (SqlSelectTable.ColumnInfo columnInfo in this.m_selectedColumns.Values)
				{
					sqlSelectSnippet.AddColumn(new SqlCompositeSnippet(new ISqlSnippet[]
					{
						SqlExpression.OpenParenSnippet,
						columnInfo.SqlExpression,
						SqlExpression.CloseParenSnippet,
						new SqlStringSnippet(" {0}", new object[] { this.m_sqlBatch.GetDelimitedIdentifier(columnInfo.Alias) })
					}));
				}
				sqlTableSource = new SqlTableSource(sqlSelectSnippet, sqlTableSource.Alias, this.m_sqlBatch);
			}
			if (this.m_dsvColumn != null)
			{
				SqlSelectTable.SqlSelectSnippet sqlSelectSnippet2 = new SqlSelectTable.SqlSelectSnippet(sqlTableSource);
				SqlSelectTable.ColumnInfo columnInfo2;
				if (!this.m_selectedColumns.TryGetValue(this.m_dsvColumn, out columnInfo2))
				{
					throw SQEAssert.AssertFalseAndThrow("Can not generate SqlSelectTable snippet for a column-bound entity: the column of the column-bound entity is not selected.", Array.Empty<object>());
				}
				sqlSelectSnippet2.AddColumn(new SqlStringSnippet("{0}.{1}", new object[]
				{
					this.m_sqlBatch.GetDelimitedIdentifier(sqlTableSource.Alias),
					this.m_sqlBatch.GetDelimitedIdentifier(columnInfo2.Alias)
				}));
				sqlSelectSnippet2.Distinct = true;
				sqlTableSource = new SqlTableSource(sqlSelectSnippet2, sqlTableSource.Alias, this.m_sqlBatch);
			}
			sqlTableSource.TuplesSource.Compile(fsw);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000B380 File Offset: 0x00009580
		SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
		{
			SqlSelectExpression sqlSelectExpression;
			if (this.m_columnExpressions.TryGetValue(expressionKey, out sqlSelectExpression))
			{
				return sqlSelectExpression;
			}
			if (expressionKey.NodeAsAttributeRef != null)
			{
				sqlSelectExpression = this.CreateAttributeRefSelectExpression(expressionKey.NodeAsAttributeRef);
			}
			else
			{
				if (expressionKey.NodeAsEntityRef == null)
				{
					throw SQEAssert.AssertFalseAndThrow("expressionKey must be either attribute or entity ref.", Array.Empty<object>());
				}
				sqlSelectExpression = this.CreateEntityRefSelectExpression(expressionKey.NodeAsEntityRef);
			}
			this.m_columnExpressions.Add(expressionKey, sqlSelectExpression);
			return sqlSelectExpression;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000B3EC File Offset: 0x000095EC
		SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
		{
			SqlSelectExpression sqlSelectExpression;
			if (this.m_columnExpressions.TryGetValue(expressionKey, out sqlSelectExpression))
			{
				return sqlSelectExpression;
			}
			sqlSelectExpression = this.CreateDsvColumnSelectExpression(expressionKey);
			this.m_columnExpressions.Add(expressionKey, sqlSelectExpression);
			return sqlSelectExpression;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B421 File Offset: 0x00009621
		SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B421 File Offset: 0x00009621
		SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
		{
			throw SQEAssert.AssertFalseAndThrow();
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000B428 File Offset: 0x00009628
		bool ISelectList.UseParensWhenNested
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dsvColumn != null || this.m_hasLogicalColumns || this.m_tableSource.TuplesSource.UseParensWhenNested;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000B44C File Offset: 0x0000964C
		internal string CandidateAlias
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_dsvColumn == null)
				{
					return this.m_tableSource.Alias;
				}
				if (!this.m_dsvColumn.IsLogical)
				{
					return this.m_dsvColumn.DbColumnName;
				}
				return this.m_dsvColumn.Name;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000B486 File Offset: 0x00009686
		internal DsvColumn DsvColumn
		{
			get
			{
				return this.m_dsvColumn;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000B48E File Offset: 0x0000968E
		internal DsvTable DsvTable
		{
			get
			{
				return this.m_dsvTable;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000B498 File Offset: 0x00009698
		private void Init(DsvTable dsvTable, SqlBatch sqlBatch)
		{
			if (dsvTable == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("dsvTable"));
			}
			this.m_dsvTable = dsvTable;
			this.m_sqlBatch = sqlBatch;
			if (dsvTable.IsLogical)
			{
				string text = ((dsvTable.Name != null && dsvTable.Name.Length > 0) ? dsvTable.Name : "LogicalTable");
				if (text.Length > sqlBatch.IdentifierMaxLength)
				{
					text = text.Substring(0, sqlBatch.IdentifierMaxLength);
				}
				this.m_tableSource = new SqlTableSource(new SqlSelectTable.SqlSelectLogicalTable(dsvTable.QueryDefinition), text, this.m_sqlBatch);
			}
			else
			{
				this.m_tableSource = new SqlTableSource(new SqlSelectTable.SqlSelectDatabaseTable(dsvTable.DbSchemaName, dsvTable.DbTableName, this.m_sqlBatch), dsvTable.DbTableName, this.m_sqlBatch);
			}
			this.m_logicalColumnAliasGen = this.m_sqlBatch.CreateSqlAliasGenerator("c0");
			for (int i = this.m_dsvTable.Columns.Count - 1; i >= 0; i--)
			{
				DsvColumn dsvColumn = this.m_dsvTable.Columns[i];
				if (!dsvColumn.IsLogical)
				{
					this.m_logicalColumnAliasGen.AddExistingName(dsvColumn.DbColumnName);
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000B5BC File Offset: 0x000097BC
		private SqlSelectExpression CreateAttributeRefSelectExpression(AttributeRefNode attributeRef)
		{
			if (attributeRef == null || attributeRef.ModelAttribute == null)
			{
				throw SQEAssert.AssertFalseAndThrow("attributeRef must point to a model attribute.", Array.Empty<object>());
			}
			DsvColumn dsvColumn = null;
			if (attributeRef.ModelAttribute.Binding != null)
			{
				dsvColumn = attributeRef.ModelAttribute.Binding.GetColumn();
			}
			return this.CreateDsvColumnSelectExpression(dsvColumn);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000B60C File Offset: 0x0000980C
		private SqlSelectExpression CreateEntityRefSelectExpression(EntityRefNode entityRef)
		{
			if (entityRef == null || entityRef.ModelEntity == null || entityRef.ModelEntity.Binding == null)
			{
				throw SQEAssert.AssertFalseAndThrow("The entityRef must point to a model entity with valid binding otherwise it can not be selected from a sql table.", Array.Empty<object>());
			}
			Binding binding = entityRef.ModelEntity.Binding;
			IList<DsvColumn> primaryKeyColumns = QueryPlanBuilder.GetPrimaryKeyColumns(binding);
			if (binding is TableBinding)
			{
				if (this.m_dsvColumn != null)
				{
					throw SQEAssert.AssertFalseAndThrow("Can not select table-bound entity ref. Current SqlSelectTable is created for a column-bound entity.", Array.Empty<object>());
				}
				return this.CreateDsvColumnsSelectExpression(primaryKeyColumns);
			}
			else
			{
				if (!(binding is ColumnBinding))
				{
					throw SQEAssert.AssertFalseAndThrow("Unknown or invalid entityRef.ModelEntity.Binding.", Array.Empty<object>());
				}
				DsvColumn dsvColumn = primaryKeyColumns[0];
				if ((this.m_dsvColumn != null && this.m_dsvColumn != dsvColumn) || !this.m_dsvTable.Columns.Contains(dsvColumn) || primaryKeyColumns.Count > 1)
				{
					throw SQEAssert.AssertFalseAndThrow("expression.NodeAsEntityRef must have valid binding.", Array.Empty<object>());
				}
				return this.CreateDsvColumnSelectExpression(dsvColumn);
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000B6E0 File Offset: 0x000098E0
		private SqlSelectExpression CreateDsvColumnSelectExpression(DsvColumn dsvColumn)
		{
			string text;
			return new SqlSelectExpression(this.CreateDsvColumnSqlExpression(dsvColumn, out text), new string[] { text }, this.m_sqlBatch);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000B70C File Offset: 0x0000990C
		private SqlSelectExpression CreateDsvColumnsSelectExpression(IList<DsvColumn> dsvColumns)
		{
			string[] array = new string[dsvColumns.Count];
			SqlTupleExpression sqlTupleExpression = new SqlTupleExpression();
			for (int i = 0; i < dsvColumns.Count; i++)
			{
				sqlTupleExpression.AddTupleValue(this.CreateDsvColumnSqlExpression(dsvColumns[i], out array[i]));
			}
			return new SqlSelectExpression(sqlTupleExpression, array, this.m_sqlBatch);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000B764 File Offset: 0x00009964
		private SqlExpression CreateDsvColumnSqlExpression(DsvColumn dsvColumn, out string alias)
		{
			if (dsvColumn == null)
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("dsvColumn"));
			}
			if (!this.m_dsvTable.Columns.Contains(dsvColumn))
			{
				throw SQEAssert.AssertFalseAndThrow("Specified dsvColumn does not belong to the current table.", Array.Empty<object>());
			}
			if (this.m_dsvColumn != null && this.m_dsvColumn != dsvColumn)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not select specified dsvColumn. The current SqlSelectTable is created for a column-bound entity and specified dsvColumn is different than the entity column.", Array.Empty<object>());
			}
			SqlSelectTable.ColumnInfo columnInfo;
			if (this.m_selectedColumns.TryGetValue(dsvColumn, out columnInfo))
			{
				alias = columnInfo.Alias;
				return columnInfo.SqlExpression;
			}
			SqlExpression sqlExpression;
			string text;
			if (dsvColumn.IsLogical)
			{
				sqlExpression = new SqlSnippetExpression(new SqlStringSnippet(dsvColumn.ComputedColumnExpression), dsvColumn.Nullable);
				text = this.m_logicalColumnAliasGen.CreateName(dsvColumn.Name, dsvColumn);
				this.m_hasLogicalColumns = true;
			}
			else
			{
				sqlExpression = new SqlColumnRefExpression(this.m_tableSource, dsvColumn.DbColumnName, this.m_sqlBatch, dsvColumn.Nullable);
				text = dsvColumn.DbColumnName;
			}
			this.m_selectedColumns.Add(dsvColumn, new SqlSelectTable.ColumnInfo(sqlExpression, text));
			alias = text;
			return sqlExpression;
		}

		// Token: 0x040000D6 RID: 214
		private DsvTable m_dsvTable;

		// Token: 0x040000D7 RID: 215
		private readonly DsvColumn m_dsvColumn;

		// Token: 0x040000D8 RID: 216
		private SqlBatch m_sqlBatch;

		// Token: 0x040000D9 RID: 217
		private SqlTableSource m_tableSource;

		// Token: 0x040000DA RID: 218
		private readonly Dictionary<object, SqlSelectExpression> m_columnExpressions = new Dictionary<object, SqlSelectExpression>();

		// Token: 0x040000DB RID: 219
		private readonly Dictionary<DsvColumn, SqlSelectTable.ColumnInfo> m_selectedColumns = new Dictionary<DsvColumn, SqlSelectTable.ColumnInfo>();

		// Token: 0x040000DC RID: 220
		private INameGenerator m_logicalColumnAliasGen;

		// Token: 0x040000DD RID: 221
		private bool m_hasLogicalColumns;

		// Token: 0x020000C2 RID: 194
		private struct ColumnInfo
		{
			// Token: 0x060006FE RID: 1790 RVA: 0x0001B89B File Offset: 0x00019A9B
			internal ColumnInfo(SqlExpression sqlExpression, string alias)
			{
				this.SqlExpression = sqlExpression;
				this.Alias = alias;
			}

			// Token: 0x04000381 RID: 897
			internal readonly SqlExpression SqlExpression;

			// Token: 0x04000382 RID: 898
			internal readonly string Alias;
		}

		// Token: 0x020000C3 RID: 195
		private sealed class SqlSelectDatabaseTable : ISelectList, ISqlSnippet
		{
			// Token: 0x060006FF RID: 1791 RVA: 0x0001B8AB File Offset: 0x00019AAB
			internal SqlSelectDatabaseTable(string schemaName, string tableName, SqlBatch sqlBatch)
			{
				if (tableName == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("tableName"));
				}
				this.m_schemaName = schemaName;
				this.m_tableName = tableName;
				this.m_sqlBatch = sqlBatch;
			}

			// Token: 0x06000700 RID: 1792 RVA: 0x0001B8DC File Offset: 0x00019ADC
			void ISqlSnippet.Compile(FormattedStringWriter fsw)
			{
				if (this.m_schemaName != null && this.m_schemaName.Length > 0)
				{
					fsw.Write("{0}.{1}", new object[]
					{
						this.m_sqlBatch.GetDelimitedIdentifier(this.m_schemaName),
						this.m_sqlBatch.GetDelimitedIdentifier(this.m_tableName)
					});
					return;
				}
				fsw.Write(this.m_sqlBatch.GetDelimitedIdentifier(this.m_tableName));
			}

			// Token: 0x06000701 RID: 1793 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000705 RID: 1797 RVA: 0x00004555 File Offset: 0x00002755
			bool ISelectList.UseParensWhenNested
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000383 RID: 899
			private readonly string m_schemaName;

			// Token: 0x04000384 RID: 900
			private readonly string m_tableName;

			// Token: 0x04000385 RID: 901
			private readonly SqlBatch m_sqlBatch;
		}

		// Token: 0x020000C4 RID: 196
		private sealed class SqlSelectLogicalTable : ISelectList, ISqlSnippet
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x0001B950 File Offset: 0x00019B50
			internal SqlSelectLogicalTable(string queryDefinition)
			{
				if (queryDefinition == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("queryDefinition"));
				}
				this.m_queryDefinition = queryDefinition;
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x0001B972 File Offset: 0x00019B72
			void ISqlSnippet.Compile(FormattedStringWriter fsw)
			{
				fsw.Write(this.m_queryDefinition);
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000709 RID: 1801 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x0600070A RID: 1802 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x0600070B RID: 1803 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x0600070C RID: 1804 RVA: 0x00004B5D File Offset: 0x00002D5D
			bool ISelectList.UseParensWhenNested
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000386 RID: 902
			private readonly string m_queryDefinition;
		}

		// Token: 0x020000C5 RID: 197
		private sealed class SqlSelectSnippet : ISelectList, ISqlSnippet
		{
			// Token: 0x0600070D RID: 1805 RVA: 0x0001B980 File Offset: 0x00019B80
			internal SqlSelectSnippet(SqlTableSource tableSource)
			{
				this.m_tableSource = tableSource;
			}

			// Token: 0x0600070E RID: 1806 RVA: 0x0001B99A File Offset: 0x00019B9A
			internal void AddColumn(ISqlSnippet column)
			{
				this.m_selectList.Add(column);
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x0600070F RID: 1807 RVA: 0x0001B9A8 File Offset: 0x00019BA8
			// (set) Token: 0x06000710 RID: 1808 RVA: 0x0001B9B0 File Offset: 0x00019BB0
			internal bool Distinct
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_distinct;
				}
				[DebuggerStepThrough]
				set
				{
					this.m_distinct = value;
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000711 RID: 1809 RVA: 0x0001B9B9 File Offset: 0x00019BB9
			// (set) Token: 0x06000712 RID: 1810 RVA: 0x0001B9C1 File Offset: 0x00019BC1
			internal string Where
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_whereClause;
				}
				[DebuggerStepThrough]
				set
				{
					this.m_whereClause = value;
				}
			}

			// Token: 0x06000713 RID: 1811 RVA: 0x0001B9CC File Offset: 0x00019BCC
			void ISqlSnippet.Compile(FormattedStringWriter fsw)
			{
				if (this.m_distinct)
				{
					fsw.Write("SELECT DISTINCT ");
				}
				else
				{
					fsw.Write("SELECT ");
				}
				for (int i = 0; i < this.m_selectList.Count; i++)
				{
					if (i > 0)
					{
						fsw.Write(", ");
					}
					this.m_selectList[i].Compile(fsw);
				}
				fsw.Write(" FROM ");
				this.m_tableSource.Compile(fsw);
				if (this.m_whereClause != null)
				{
					fsw.Write(" WHERE ");
					fsw.Write(this.m_whereClause);
				}
			}

			// Token: 0x06000714 RID: 1812 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000715 RID: 1813 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetSelectExpression(DsvColumn expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000716 RID: 1814 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFlagExpression(Expression expressionKey)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x06000717 RID: 1815 RVA: 0x0000B421 File Offset: 0x00009621
			SqlSelectExpression ISelectList.GetAggregationFieldCountExpression()
			{
				throw SQEAssert.AssertFalseAndThrow();
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000718 RID: 1816 RVA: 0x00004B5D File Offset: 0x00002D5D
			bool ISelectList.UseParensWhenNested
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000387 RID: 903
			private readonly ISqlSnippet m_tableSource;

			// Token: 0x04000388 RID: 904
			private bool m_distinct;

			// Token: 0x04000389 RID: 905
			private string m_whereClause;

			// Token: 0x0400038A RID: 906
			private readonly ReadOnlyItemCollectionBase<ISqlSnippet> m_selectList = new ReadOnlyItemCollectionBase<ISqlSnippet>();
		}
	}
}
