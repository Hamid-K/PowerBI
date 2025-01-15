using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200065E RID: 1630
	internal class OdbcStatementBuilder : StatementBuilder
	{
		// Token: 0x06003385 RID: 13189 RVA: 0x000A4BA4 File Offset: 0x000A2DA4
		public OdbcStatementBuilder(TableValue table, OdbcQueryColumnInfo[] columnInfos)
			: base(table)
		{
			this.columnInfos = columnInfos;
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x06003386 RID: 13190 RVA: 0x000A4BB4 File Offset: 0x000A2DB4
		private OdbcQueryDomain Domain
		{
			get
			{
				return (OdbcQueryDomain)this.table.QueryDomain;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003387 RID: 13191 RVA: 0x000A4BC6 File Offset: 0x000A2DC6
		protected override IEngineHost Host
		{
			get
			{
				return this.Domain.DataSource.Host;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003388 RID: 13192 RVA: 0x00002139 File Offset: 0x00000339
		protected override int InsertBatchSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x000A4BD8 File Offset: 0x000A2DD8
		protected override void VerifyActionPermitted()
		{
			this.Host.IsActionPermitted(this.Domain.DataSource.Resource);
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x000A4BF6 File Offset: 0x000A2DF6
		protected override Keys GetKeys()
		{
			return this.table.Columns;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x000A4C04 File Offset: 0x000A2E04
		protected override bool TryGetBatchInsertAction(TableValue batch, bool countOnlyTable, out ActionValue action)
		{
			OutputClause outputClause = OutputClause.Null;
			OdbcStatementBuilder.OutputSupportType outputSupportType = OdbcStatementBuilder.OutputSupportType.NotSupported;
			TableReference tableReference;
			if (this.TryGetTableReference((OdbcQuery)this.table.Query, out tableReference))
			{
				OdbcQueryExpressionVisitor visitor = this.GetVisitor();
				int num = 0;
				List<ColumnReference> list = batch.Columns.Select((string c) => new ColumnReference(Alias.NewNativeAlias(c))).ToList<ColumnReference>();
				List<IList<OdbcScalarExpression>> list2 = new List<IList<OdbcScalarExpression>>();
				foreach (IValueReference valueReference in batch)
				{
					num++;
					RecordValue asRecord = valueReference.Value.AsRecord;
					OdbcScalarExpression[] array = new OdbcScalarExpression[list.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = visitor.VisitConstant(asRecord[i]);
					}
					list2.Add(array);
				}
				if (!countOnlyTable)
				{
					if (num <= 1)
					{
						outputSupportType = this.GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_SINGLE_ROWID, Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_SINGLE_ANY);
					}
					if (outputSupportType == OdbcStatementBuilder.OutputSupportType.NotSupported)
					{
						outputSupportType = this.GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_MULTIPLE_ROWID, Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_MULTIPLE_ANY);
					}
					if (outputSupportType == OdbcStatementBuilder.OutputSupportType.RowId)
					{
						outputClause = this.GetRowIdOutputClause(tableReference);
					}
					else
					{
						if (outputSupportType != OdbcStatementBuilder.OutputSupportType.Any)
						{
							action = null;
							return false;
						}
						outputClause = this.GetAnyOutputClause();
					}
				}
				OdbcStatementExpression odbcStatementExpression;
				if (this.TryGetOdbcExpression(new OdbcInsertStatement(tableReference, list2, outputClause, list), outputSupportType, tableReference, out odbcStatementExpression))
				{
					action = this.GetActionValue(odbcStatementExpression, countOnlyTable, outputClause);
					return true;
				}
			}
			action = null;
			return false;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x000A4D68 File Offset: 0x000A2F68
		protected override bool TryGetInsertAction(Query rowsToInsert, bool countOnlyTable, out ActionValue action)
		{
			OutputClause outputClause = OutputClause.Null;
			OdbcStatementBuilder.OutputSupportType outputSupportType = OdbcStatementBuilder.OutputSupportType.NotSupported;
			TableReference tableReference;
			OdbcQuery odbcQuery;
			if (this.TryGetTableReference((OdbcQuery)this.table.Query, out tableReference) && this.Domain.IsCompatibleWith(rowsToInsert.QueryDomain) && this.TryGetOdbcQuery(rowsToInsert, out odbcQuery))
			{
				List<ColumnReference> list = odbcQuery.QuerySpecification.SelectItems.Select((SelectItem s) => new ColumnReference(s.Alias ?? s.Name)).ToList<ColumnReference>();
				if (!countOnlyTable)
				{
					outputSupportType = this.GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_SELECT_ROWID, Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_INSERT_SELECT_ANY);
					if (outputSupportType == OdbcStatementBuilder.OutputSupportType.RowId)
					{
						outputClause = this.GetRowIdOutputClause(tableReference);
					}
					else
					{
						if (outputSupportType != OdbcStatementBuilder.OutputSupportType.Any)
						{
							action = null;
							return false;
						}
						outputClause = this.GetAnyOutputClause();
					}
				}
				OdbcStatementExpression odbcStatementExpression;
				if (this.TryGetOdbcExpression(new SqlInsertStatement(tableReference, odbcQuery.QuerySpecification, outputClause, list), outputSupportType, tableReference, out odbcStatementExpression))
				{
					action = this.GetActionValue(odbcStatementExpression, countOnlyTable, outputClause);
					return true;
				}
			}
			action = null;
			return false;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x000A4E50 File Offset: 0x000A3050
		protected override bool TryGetUpdateAction(FunctionValue filter, ColumnUpdates columnUpdates, bool countOnlyTable, out ActionValue action)
		{
			OdbcQuery odbcQuery = this.table.Query.SelectRows(filter) as OdbcQuery;
			OutputClause outputClause = OutputClause.Null;
			OdbcStatementBuilder.OutputSupportType outputSupportType = OdbcStatementBuilder.OutputSupportType.NotSupported;
			TableReference tableReference;
			if (this.TryGetTableReference(odbcQuery, out tableReference))
			{
				Condition whereClause = odbcQuery.QuerySpecification.WhereClause;
				Keys keys = this.GetKeys();
				List<SqlColumnUpdate> list = new List<SqlColumnUpdate>();
				foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
				{
					Alias alias = Alias.NewNativeAlias(keys[keyValuePair.Key]);
					SqlExpression updateExpression = this.GetUpdateExpression(keyValuePair.Value);
					if (updateExpression == null)
					{
						action = null;
						return false;
					}
					list.Add(new SqlColumnUpdate(new ColumnReference(alias), updateExpression));
				}
				if (!countOnlyTable)
				{
					outputSupportType = this.GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_UPDATE_ROWID, Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_UPDATE_ANY);
					if (outputSupportType == OdbcStatementBuilder.OutputSupportType.RowId)
					{
						outputClause = this.GetRowIdOutputClause(tableReference);
					}
					else
					{
						if (outputSupportType != OdbcStatementBuilder.OutputSupportType.Any)
						{
							action = null;
							return false;
						}
						outputClause = this.GetAnyOutputClause();
					}
				}
				OdbcStatementExpression odbcStatementExpression;
				if (this.TryGetOdbcExpression(new SqlUpdateStatement(tableReference, list, outputClause, whereClause), outputSupportType, tableReference, out odbcStatementExpression))
				{
					action = this.GetActionValue(odbcStatementExpression, countOnlyTable, outputClause);
					return true;
				}
			}
			IL_0118:
			action = null;
			return false;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x000A4F90 File Offset: 0x000A3190
		protected override bool TryGetDeleteAction(FunctionValue filter, bool countOnlyTable, out ActionValue action)
		{
			OdbcQuery odbcQuery = this.table.Query.SelectRows(filter) as OdbcQuery;
			OutputClause outputClause = OutputClause.Null;
			OdbcStatementBuilder.OutputSupportType outputSupportType = OdbcStatementBuilder.OutputSupportType.NotSupported;
			TableReference tableReference;
			if (this.TryGetTableReference(odbcQuery, out tableReference))
			{
				Condition whereClause = odbcQuery.QuerySpecification.WhereClause;
				if (!countOnlyTable)
				{
					outputSupportType = this.GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_DELETE_ROWID, Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_DELETE_ANY);
					if (outputSupportType == OdbcStatementBuilder.OutputSupportType.RowId)
					{
						if (odbcQuery.Columns.Any((string c) => !(from s in this.GetBestRowId(tableReference)
							select s.Name.Name).Contains(c)))
						{
							action = null;
							return false;
						}
						outputClause = this.GetAnyOutputClause();
						outputSupportType = OdbcStatementBuilder.OutputSupportType.Any;
					}
					else
					{
						if (outputSupportType != OdbcStatementBuilder.OutputSupportType.Any)
						{
							action = null;
							return false;
						}
						outputClause = this.GetAnyOutputClause();
					}
				}
				OdbcStatementExpression odbcStatementExpression;
				if (this.TryGetOdbcExpression(new SqlDeleteStatement(tableReference, outputClause, whereClause), outputSupportType, tableReference, out odbcStatementExpression))
				{
					action = this.GetActionValue(odbcStatementExpression, countOnlyTable, outputClause);
					return true;
				}
			}
			action = null;
			return false;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x000A506C File Offset: 0x000A326C
		private SqlExpression GetUpdateExpression(FunctionValue function)
		{
			try
			{
				OdbcQueryExpressionVisitor visitor = this.GetVisitor();
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.table.Type.AsTableType.ItemType, function);
				if (queryExpression != null)
				{
					OdbcScalarExpression odbcScalarExpression = visitor.Visit(queryExpression) as OdbcScalarExpression;
					if (odbcScalarExpression != null)
					{
						return odbcScalarExpression.Expression;
					}
				}
			}
			catch (NotSupportedException)
			{
			}
			return null;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x000A50D0 File Offset: 0x000A32D0
		private bool TryGetOdbcQuery(Query query, out OdbcQuery odbcQuery)
		{
			if (query is RetryQuery)
			{
				query = ((RetryQuery)query).OptimizedQuery;
			}
			if (query is OdbcQuery)
			{
				odbcQuery = (OdbcQuery)query;
				return true;
			}
			odbcQuery = null;
			return false;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x000A5100 File Offset: 0x000A3300
		private bool TryGetTableReference(OdbcQuery query, out TableReference tableReference)
		{
			if (query != null && query.QuerySpecification.GroupByClause == null && query.QuerySpecification.HavingClause == null && query.QuerySpecification.LimitClause == null && query.QuerySpecification.OrderByClause == null && query.QuerySpecification.FromItems != null && query.QuerySpecification.FromItems.Count == 1 && query.QuerySpecification.FromItems[0] is FromTable)
			{
				tableReference = ((FromTable)query.QuerySpecification.FromItems[0]).Table;
				return true;
			}
			tableReference = null;
			return false;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x000A51A4 File Offset: 0x000A33A4
		private OdbcStatementBuilder.OutputSupportType GetSupportLevel(Odbc32.SQL_RETURN_ESCAPE_CLAUSE rowIdFlag, Odbc32.SQL_RETURN_ESCAPE_CLAUSE anyFlag)
		{
			Odbc32.SQL_RETURN_ESCAPE_CLAUSE returnEscapeClause = this.Domain.DataSource.Info.ReturnEscapeClause;
			if ((returnEscapeClause & anyFlag) != Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_NONE)
			{
				return OdbcStatementBuilder.OutputSupportType.Any;
			}
			if ((returnEscapeClause & rowIdFlag) != Odbc32.SQL_RETURN_ESCAPE_CLAUSE.SQL_RC_NONE)
			{
				return OdbcStatementBuilder.OutputSupportType.RowId;
			}
			return OdbcStatementBuilder.OutputSupportType.NotSupported;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x000A51D6 File Offset: 0x000A33D6
		private OutputClause GetRowIdOutputClause(TableReference tableReference)
		{
			return new OdbcStatementBuilder.OdbcOutputClause(this.GetBestRowId(tableReference));
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x000A51E4 File Offset: 0x000A33E4
		private IList<SelectItem> GetBestRowId(TableReference tableReference)
		{
			OdbcTableInfo tableInfo = this.Domain.DataSource.GetOrCreateTableInfo(new OdbcIdentifier(tableReference), null);
			IEnumerable<string> primaryKeyColumns = tableInfo.RowId.Select((int i) => tableInfo.Columns[i].Name);
			return ((OdbcQuery)this.table.Query).QuerySpecification.SelectItems.Where((SelectItem s) => primaryKeyColumns.Contains(s.Name.Name)).ToList<SelectItem>();
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x000A5266 File Offset: 0x000A3466
		private OutputClause GetAnyOutputClause()
		{
			return new OdbcStatementBuilder.OdbcOutputClause(((OdbcQuery)this.table.Query).QuerySpecification.SelectItems);
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000A5288 File Offset: 0x000A3488
		private bool TryGetOdbcExpression(SqlStatement statement, OdbcStatementBuilder.OutputSupportType outputType, TableReference tableReference, out OdbcStatementExpression expression)
		{
			Func<TableValue, Query> func = null;
			if (outputType == OdbcStatementBuilder.OutputSupportType.RowId)
			{
				TableKey key = new TableKey(this.Domain.DataSource.GetOrCreateTableInfo(new OdbcIdentifier(tableReference), null).RowId, false);
				if (key.Columns.Length == 0)
				{
					expression = null;
					return false;
				}
				func = (TableValue t) => this.GetSelectionQuery(t, key);
			}
			expression = new OdbcStatementExpression(statement, func);
			return true;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x000A52FC File Offset: 0x000A34FC
		private ActionValue GetActionValue(OdbcStatementExpression statement, bool countOnly, OutputClause outputClause)
		{
			IList<SelectItem> list = outputClause.ColumnList ?? EmptyArray<SelectItem>.Instance;
			ActionValue actionValue = new OdbcStatementActionValue(this.Domain.DataSource, statement, list.Select((SelectItem c) => c.Name.Name).ToArray<string>(), countOnly);
			if (countOnly)
			{
				actionValue = actionValue.Bind(StatementBuilder.ReturnScalar);
				actionValue = actionValue.Bind(new ReturnTypedTableFromCountFunctionValue(this.table.Type.AsTableType));
			}
			return actionValue;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x000A5384 File Offset: 0x000A3584
		private OdbcQueryExpressionVisitor GetVisitor()
		{
			IList<SelectItem> list = (from k in this.GetKeys()
				select new SelectItem(new ColumnReference(Alias.NewNativeAlias(k)))).ToList<SelectItem>();
			return this.Domain.NewQueryExpressionVisitor(list, this.columnInfos, false, null);
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x000A53D5 File Offset: 0x000A35D5
		protected override IEnumerator<IValueReference> GetValueEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x040016ED RID: 5869
		private OdbcQueryColumnInfo[] columnInfos;

		// Token: 0x0200065F RID: 1631
		private enum OutputSupportType
		{
			// Token: 0x040016EF RID: 5871
			NotSupported,
			// Token: 0x040016F0 RID: 5872
			RowId,
			// Token: 0x040016F1 RID: 5873
			Any
		}

		// Token: 0x02000660 RID: 1632
		private class OdbcOutputClause : OutputClause
		{
			// Token: 0x0600339A RID: 13210 RVA: 0x00059155 File Offset: 0x00057355
			public OdbcOutputClause(IList<SelectItem> columnList)
				: base(columnList)
			{
			}

			// Token: 0x0600339B RID: 13211 RVA: 0x000A53E2 File Offset: 0x000A35E2
			public override void WritePrefixScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.OpenCurlyBraceSqlString);
				writer.WriteSpaceAfter(SqlLanguageStrings.ReturnSqlString);
				base.WriteColumns(writer);
				writer.WriteSpaceBefore(SqlLanguageStrings.FromSqlString);
				writer.WriteSpaceBefore(SqlLanguageSymbols.SingleQuoteSqlString);
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x000A5417 File Offset: 0x000A3617
			public override void WriteSuffixScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.SingleQuoteSqlString);
				writer.Write(SqlLanguageSymbols.CloseCurlyBraceSqlString);
			}
		}
	}
}
