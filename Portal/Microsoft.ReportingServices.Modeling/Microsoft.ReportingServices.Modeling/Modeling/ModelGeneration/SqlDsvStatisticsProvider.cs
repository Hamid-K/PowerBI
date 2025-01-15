using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000104 RID: 260
	public abstract class SqlDsvStatisticsProvider : IDsvStatisticsProvider
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002ABCC File Offset: 0x00028DCC
		protected SqlDsvStatisticsProvider(IDbConnection connection)
		{
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			this.m_connection = connection;
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002ABF4 File Offset: 0x00028DF4
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0002ABFC File Offset: 0x00028DFC
		public int CommandTimeout
		{
			get
			{
				return this.m_commandTimeout;
			}
			set
			{
				this.m_commandTimeout = value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000CD6 RID: 3286 RVA: 0x0002AC08 File Offset: 0x00028E08
		// (remove) Token: 0x06000CD7 RID: 3287 RVA: 0x0002AC40 File Offset: 0x00028E40
		public event EventHandler<ProgressEventArgs> Progress;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000CD8 RID: 3288 RVA: 0x0002AC78 File Offset: 0x00028E78
		// (remove) Token: 0x06000CD9 RID: 3289 RVA: 0x0002ACB0 File Offset: 0x00028EB0
		public event EventHandler<LogEventArgs> Log;

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002ACE8 File Offset: 0x00028EE8
		public void Fill(DataSourceView dataSourceView, IDsvItemFilter filter, bool overwrite, ICancelEvent cancel)
		{
			if (dataSourceView == null)
			{
				throw new ArgumentNullException("dataSourceView");
			}
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			if (cancel == null)
			{
				throw new ArgumentNullException("cancel");
			}
			this.m_dsv = dataSourceView;
			this.m_filter = filter;
			this.m_overwrite = overwrite;
			this.m_cancelEvent = cancel;
			List<DsvTable> list = new List<DsvTable>(dataSourceView.Tables);
			this.m_tables = list.FindAll(new Predicate<DsvTable>(this.m_filter.Evaluate));
			List<DsvRelation> list2 = new List<DsvRelation>(dataSourceView.Relations);
			this.m_relations = list2.FindAll(new Predicate<DsvRelation>(this.m_filter.Evaluate));
			bool flag = false;
			if (this.m_connection.State != ConnectionState.Open)
			{
				this.m_connection.Open();
				flag = true;
			}
			try
			{
				this.SetNoLock();
				if (this.m_dsv.CompareInfo == null || this.m_overwrite)
				{
					this.m_dsv.CompareInfo = this.GetCompareInfo();
				}
				this.CreateDbObjectNameEqComparer();
				this.GetTableRowCounts();
				this.GetColumnGroups();
				this.GetColumnUniquenessAndExtProperties();
				this.GetColumnWidths();
			}
			finally
			{
				if (flag)
				{
					this.m_connection.Close();
				}
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002AE1C File Offset: 0x0002901C
		protected IDbConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_connection;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002AE24 File Offset: 0x00029024
		protected IEqualityComparer<string> DBObjectNameComparer
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dbObjectNameEqComp;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002AE2C File Offset: 0x0002902C
		protected DataSourceView DataSourceView
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dsv;
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002AE34 File Offset: 0x00029034
		protected IDbCommand CreateCommand(string commandText)
		{
			IDbCommand dbCommand = this.m_connection.CreateCommand();
			dbCommand.CommandType = CommandType.Text;
			dbCommand.CommandText = commandText;
			dbCommand.CommandTimeout = this.m_commandTimeout;
			return dbCommand;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002AE5B File Offset: 0x0002905B
		protected string GetSqlForColumnSelectExpression(DsvColumn column)
		{
			if (column.IsLogical)
			{
				return column.ComputedColumnExpression;
			}
			return this.GetDelimitedIdentifier(column.DbColumnName);
		}

		// Token: 0x06000CE0 RID: 3296
		protected abstract bool IsBlob(DsvColumn column);

		// Token: 0x06000CE1 RID: 3297
		protected abstract DsvCompareInfo GetCompareInfo();

		// Token: 0x06000CE2 RID: 3298
		protected abstract void SetNoLock();

		// Token: 0x06000CE3 RID: 3299
		protected abstract SqlDsvStatisticsProvider.IColumnIndexesReader GetColumnIndexesReader();

		// Token: 0x06000CE4 RID: 3300
		protected abstract SqlDsvStatisticsProvider.IGetColumnDbTypeName GetColumnDbTypeNameDictionary();

		// Token: 0x06000CE5 RID: 3301
		protected abstract string GetDelimitedIdentifier(string name);

		// Token: 0x06000CE6 RID: 3302
		protected abstract string GetSqlForSelectSampledTableSource(long actualRowCount, long targetRowCount, string tableSource, bool isTable);

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002AE78 File Offset: 0x00029078
		protected virtual string WrapForCountExpression(DsvColumn column)
		{
			string sqlForColumnSelectExpression = this.GetSqlForColumnSelectExpression(column);
			if (column.ModelingDataType == null)
			{
				return null;
			}
			if (this.IsBlob(column))
			{
				return null;
			}
			return sqlForColumnSelectExpression;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x0002AEAB File Offset: 0x000290AB
		protected virtual string GetSqlForCountExpression(string expression)
		{
			return StringUtil.FormatInvariant("COUNT({0})", new object[] { expression });
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002AEC1 File Offset: 0x000290C1
		protected virtual string GetSqlForCountDistinctExpression(string expression)
		{
			return StringUtil.FormatInvariant("COUNT(DISTINCT {0})", new object[] { expression });
		}

		// Token: 0x06000CEA RID: 3306
		protected abstract string GetSqlForStringAvgWidthExpression(DsvColumn column);

		// Token: 0x06000CEB RID: 3307
		protected abstract string GetSqlForStringStDevWidthExpression(DsvColumn column);

		// Token: 0x06000CEC RID: 3308
		protected abstract string GetSqlForStringMaxWidthExpression(DsvColumn column);

		// Token: 0x06000CED RID: 3309
		protected abstract string GetSqlForIntAvgWidthExpression(DsvColumn column);

		// Token: 0x06000CEE RID: 3310
		protected abstract string GetSqlForIntStDevWidthExpression(DsvColumn column);

		// Token: 0x06000CEF RID: 3311
		protected abstract string GetSqlForIntMaxWidthExpression(DsvColumn column);

		// Token: 0x06000CF0 RID: 3312
		protected abstract string GetSqlForRealAvgWidthExpression(DsvColumn column);

		// Token: 0x06000CF1 RID: 3313
		protected abstract string GetSqlForRealStDevWidthExpression(DsvColumn column);

		// Token: 0x06000CF2 RID: 3314
		protected abstract string GetSqlForRealMaxWidthExpression(DsvColumn column);

		// Token: 0x06000CF3 RID: 3315
		protected abstract string GetSqlForRealAvgScaleExpression(DsvColumn column);

		// Token: 0x06000CF4 RID: 3316
		protected abstract string GetSqlForRealStDevScaleExpression(DsvColumn column);

		// Token: 0x06000CF5 RID: 3317
		protected abstract string GetSqlForRealMaxScaleExpression(DsvColumn column);

		// Token: 0x06000CF6 RID: 3318 RVA: 0x0002AED8 File Offset: 0x000290D8
		private void CreateDbObjectNameEqComparer()
		{
			IEqualityComparer<string> equalityComparer;
			if (this.m_dsv.CompareInfo == null)
			{
				IEqualityComparer<string> invariantCulture = StringComparer.InvariantCulture;
				equalityComparer = invariantCulture;
			}
			else
			{
				equalityComparer = this.m_dsv.CompareInfo.CreateComparer();
			}
			this.m_dbObjectNameEqComp = equalityComparer;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0002AF14 File Offset: 0x00029114
		private void GetTableRowCounts()
		{
			int num = 0;
			while (num < this.m_tables.Count && !this.m_cancelEvent.CancelRequested)
			{
				DsvTable dsvTable = this.m_tables[num];
				if (dsvTable.RowCount == null || this.m_overwrite)
				{
					this.RaiseProgress(SR.SqlDsvStatisticsProvider_CalculatingTableRowCounts, dsvTable.Name, num + 1, this.m_tables.Count);
					dsvTable.RowCount = new long?(Convert.ToInt64(this.ExecuteScalar(this.GetSqlForTableRowCount(dsvTable)), CultureInfo.InvariantCulture));
				}
				num++;
			}
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0002AFB0 File Offset: 0x000291B0
		private void GetColumnGroups()
		{
			this.m_indexedColumns = new Bag<DsvColumn>();
			using (SqlDsvStatisticsProvider.IColumnIndexesReader columnIndexesReader = this.GetColumnIndexesReader())
			{
				List<DsvColumn> list = new List<DsvColumn>();
				while (columnIndexesReader.Read() && !this.m_cancelEvent.CancelRequested)
				{
					DsvTable dsvTable = this.FindTable(columnIndexesReader.TableSchemaName, columnIndexesReader.TableName);
					list.Clear();
					foreach (string text in columnIndexesReader.IndexColumns)
					{
						DsvColumn dsvColumn = this.FindColumn(dsvTable, text);
						list.Add(dsvColumn);
						if (dsvColumn != null)
						{
							this.m_indexedColumns.Add(dsvColumn, true);
						}
					}
					SqlDsvStatisticsProvider.AddColumnGroupsFromIndex(columnIndexesReader.IndexSchemaName, columnIndexesReader.IndexName, list);
				}
			}
			foreach (DsvRelation dsvRelation in this.m_relations)
			{
				SqlDsvStatisticsProvider.AddColumnGroupsFromRelation(dsvRelation.Name, dsvRelation.SourceColumns);
			}
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x0002B0E4 File Offset: 0x000292E4
		private static void AddColumnGroupsFromIndex(string indexSchemaName, string indexName, IList<DsvColumn> indexCols)
		{
			if (indexCols.Count <= 1 || indexCols.Contains(null))
			{
				return;
			}
			string text = "IX_";
			if (!string.IsNullOrEmpty(indexSchemaName))
			{
				text = text + indexSchemaName + "_";
			}
			text += indexName;
			foreach (DsvColumn dsvColumn in indexCols)
			{
				dsvColumn.ColumnGroups.Add(text);
			}
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x0002B168 File Offset: 0x00029368
		private static void AddColumnGroupsFromRelation(string relationName, IList<DsvColumn> sourceCols)
		{
			if (sourceCols.Count <= 1)
			{
				return;
			}
			foreach (DsvColumn dsvColumn in sourceCols)
			{
				dsvColumn.ColumnGroups.Add("FK_" + relationName);
			}
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x0002B1C8 File Offset: 0x000293C8
		private void GetColumnUniquenessAndExtProperties()
		{
			SqlDsvStatisticsProvider.IGetColumnDbTypeName getColumnDbTypeName = null;
			bool flag = false;
			int num = 0;
			while (num < this.m_tables.Count && !this.m_cancelEvent.CancelRequested)
			{
				DsvTable dsvTable = this.m_tables[num];
				this.RaiseProgress(SR.SqlDsvStatisticsProvider_CalculatingUniqueness, dsvTable.Name, num + 1, this.m_tables.Count);
				List<DsvColumn> list = new List<DsvColumn>();
				foreach (DsvColumn dsvColumn in Iterators.Filter<DsvColumn>(dsvTable.Columns, new Predicate<DsvColumn>(this.m_filter.Evaluate)))
				{
					if (dsvColumn.DataType == typeof(string) || dsvColumn.DataType == typeof(byte[]))
					{
						if (this.IsBlob(dsvColumn))
						{
							dsvColumn.IsBlob = new bool?(true);
						}
						else
						{
							bool? isBlob = dsvColumn.IsBlob;
							if (isBlob != null && isBlob.Value)
							{
								dsvColumn.IsBlob = null;
							}
						}
					}
					if (!dsvColumn.IsLogical && !dsvTable.IsLogical && string.IsNullOrEmpty(dsvColumn.DbDataType))
					{
						if (!flag)
						{
							getColumnDbTypeName = this.GetColumnDbTypeNameDictionary();
							flag = true;
						}
						if (getColumnDbTypeName != null)
						{
							dsvColumn.DbDataType = getColumnDbTypeName.GetColumnDbTypeName(dsvTable.DbSchemaName, dsvTable.DbTableName, dsvColumn.DbColumnName);
						}
					}
					if (dsvColumn.UniqueValueCount == null || dsvColumn.UniqueValuePercent == null || this.m_overwrite)
					{
						if (dsvColumn.Unique || this.WrapForCountExpression(dsvColumn) == null)
						{
							dsvColumn.UniqueValuePercent = new int?(100);
							dsvColumn.UniqueValueCount = dsvTable.RowCount;
						}
						else if (this.m_indexedColumns.Contains(dsvColumn))
						{
							this.GetColumnUniquenessCore(dsvTable, new DsvColumn[] { dsvColumn }, false);
						}
						else
						{
							list.Add(dsvColumn);
						}
					}
				}
				if (list.Count > 0)
				{
					for (int i = 0; i < list.Count; i += 15)
					{
						int num2 = Math.Min(list.Count - i, 15);
						this.GetColumnUniquenessCore(dsvTable, list.GetRange(i, num2), true);
					}
				}
				this.GetColumnGroupUniqueness(dsvTable);
				num++;
			}
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x0002B438 File Offset: 0x00029638
		private void GetColumnUniquenessCore(DsvTable table, IList<DsvColumn> columns, bool allowSampling)
		{
			if (table == null)
			{
				throw new InternalModelingException("table is null");
			}
			if (columns == null || columns.Count == 0)
			{
				throw new InternalModelingException("columns is null/empty");
			}
			List<string> list = new List<string>(columns.Count);
			Dictionary<DsvColumn, int> dictionary = new Dictionary<DsvColumn, int>(columns.Count);
			foreach (DsvColumn dsvColumn in columns)
			{
				string text = this.WrapForCountExpression(dsvColumn);
				if (text == null)
				{
					throw new InternalModelingException("Unexpected null columnExpr");
				}
				dictionary.Add(dsvColumn, list.Count);
				list.Add(this.GetSqlForCountExpression(text));
				list.Add(this.GetSqlForCountDistinctExpression(text));
			}
			string text2 = this.GetSqlForTableSourceExpression(table);
			long? rowCount = table.RowCount;
			long num = 100000L;
			if (((rowCount.GetValueOrDefault() > num) & (rowCount != null)) && allowSampling)
			{
				text2 = this.GetSqlForSelectSampledTableSource(table.RowCount.Value, 100000L, text2, SqlDsvStatisticsProvider.IsPhysicalTable(table));
			}
			using (SqlDsvStatisticsProvider.SchemaReader schemaReader = this.ExecuteQuery(this.GetSqlForSelect(list, text2)))
			{
				if (schemaReader.Read())
				{
					using (Dictionary<DsvColumn, int>.Enumerator enumerator2 = dictionary.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<DsvColumn, int> keyValuePair = enumerator2.Current;
							long @int = schemaReader.GetInt64(keyValuePair.Value);
							long int2 = schemaReader.GetInt64(keyValuePair.Value + 1);
							keyValuePair.Key.UniqueValueCount = new long?(int2);
							keyValuePair.Key.UniqueValuePercent = new int?((@int == 0L) ? 0 : ((int)((double)int2 * 100.0 / (double)@int)));
						}
						return;
					}
				}
				throw new InternalModelingException("Unexpected end of data for column uniqueness query");
			}
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x0002B624 File Offset: 0x00029824
		private static bool IsPhysicalTable(DsvTable table)
		{
			return !table.IsLogical && table.TableType == DsvTableType.Table;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002B63C File Offset: 0x0002983C
		private void GetColumnGroupUniqueness(DsvTable table)
		{
			foreach (KeyValuePair<string, List<DsvColumn>> keyValuePair in table.GetColumnGroupsDictionary())
			{
				if (table.AreColumnsUnique(keyValuePair.Value))
				{
					table.ColumnGroupUniqueRows[keyValuePair.Key] = table.RowCount;
				}
				else if (!table.ColumnGroupUniqueRows.Contains(keyValuePair.Key) || this.m_overwrite)
				{
					List<string> list = new List<string>();
					bool flag = true;
					foreach (DsvColumn dsvColumn in keyValuePair.Value)
					{
						string text = this.WrapForCountExpression(dsvColumn);
						if (text == null)
						{
							break;
						}
						list.Add(text);
						flag &= this.m_indexedColumns.Contains(dsvColumn);
					}
					if (list.Count >= keyValuePair.Value.Count)
					{
						string text2 = this.GetSqlForTableSourceExpression(table);
						if (!flag)
						{
							long? rowCount = table.RowCount;
							long num = 100000L;
							if ((rowCount.GetValueOrDefault() > num) & (rowCount != null))
							{
								text2 = this.GetSqlForSelectSampledTableSource(table.RowCount.Value, 100000L, text2, SqlDsvStatisticsProvider.IsPhysicalTable(table));
							}
						}
						string sqlForSelectCountDistinctComposite = this.GetSqlForSelectCountDistinctComposite(list, text2);
						int num2 = Convert.ToInt32(this.ExecuteScalar(sqlForSelectCountDistinctComposite), CultureInfo.InvariantCulture);
						table.ColumnGroupUniqueRows[keyValuePair.Key] = new long?((long)num2);
					}
				}
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002B808 File Offset: 0x00029A08
		private void GetColumnWidths()
		{
			int num = 0;
			while (num < this.m_tables.Count && !this.m_cancelEvent.CancelRequested)
			{
				DsvTable dsvTable = this.m_tables[num];
				List<DsvColumn> list = new List<DsvColumn>();
				this.RaiseProgress(SR.SqlDsvStatisticsProvider_CalculatingColumnWidths, dsvTable.Name, num + 1, this.m_tables.Count);
				foreach (DsvColumn dsvColumn in Iterators.Filter<DsvColumn>(dsvTable.Columns, new Predicate<DsvColumn>(this.m_filter.Evaluate)))
				{
					if ((dsvColumn.AvgWidth == null || dsvColumn.StDevWidth == null || dsvColumn.MaxWidth == null || this.m_overwrite) && dsvColumn.ModelingDataType != null && !this.IsBlob(dsvColumn))
					{
						DataType value = dsvColumn.ModelingDataType.Value;
						if (value <= DataType.Float)
						{
							list.Add(dsvColumn);
						}
					}
				}
				if (list.Count > 0)
				{
					for (int i = 0; i < list.Count; i += 15)
					{
						int num2 = Math.Min(list.Count - i, 15);
						this.GetColumnWidthsCore(dsvTable, list.GetRange(i, num2), true);
					}
				}
				num++;
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002B984 File Offset: 0x00029B84
		private void GetColumnWidthsCore(DsvTable table, IList<DsvColumn> columns, bool allowSampling)
		{
			if (table == null)
			{
				throw new InternalModelingException("table is null");
			}
			if (columns == null || columns.Count == 0)
			{
				throw new InternalModelingException("columns is null/empty");
			}
			List<string> list = new List<string>(columns.Count);
			Dictionary<DsvColumn, int> dictionary = new Dictionary<DsvColumn, int>(columns.Count);
			foreach (DsvColumn dsvColumn in columns)
			{
				dictionary.Add(dsvColumn, list.Count);
				switch (dsvColumn.ModelingDataType.Value)
				{
				case DataType.String:
					list.Add(this.GetSqlForStringAvgWidthExpression(dsvColumn));
					list.Add(this.GetSqlForStringStDevWidthExpression(dsvColumn));
					list.Add(this.GetSqlForStringMaxWidthExpression(dsvColumn));
					break;
				case DataType.Integer:
					list.Add(this.GetSqlForIntAvgWidthExpression(dsvColumn));
					list.Add(this.GetSqlForIntStDevWidthExpression(dsvColumn));
					list.Add(this.GetSqlForIntMaxWidthExpression(dsvColumn));
					break;
				case DataType.Decimal:
				case DataType.Float:
					list.Add(this.GetSqlForRealAvgWidthExpression(dsvColumn));
					list.Add(this.GetSqlForRealStDevWidthExpression(dsvColumn));
					list.Add(this.GetSqlForRealMaxWidthExpression(dsvColumn));
					list.Add(this.GetSqlForRealAvgScaleExpression(dsvColumn));
					list.Add(this.GetSqlForRealStDevScaleExpression(dsvColumn));
					list.Add(this.GetSqlForRealMaxScaleExpression(dsvColumn));
					break;
				default:
					throw new InternalModelingException("Unexpected column DataType in GetColumnWidthsCore: " + dsvColumn.ModelingDataType.ToString());
				}
			}
			string text = this.GetSqlForTableSourceExpression(table);
			long? rowCount = table.RowCount;
			long num = 100000L;
			if (((rowCount.GetValueOrDefault() > num) & (rowCount != null)) && allowSampling)
			{
				text = this.GetSqlForSelectSampledTableSource(table.RowCount.Value, 100000L, text, SqlDsvStatisticsProvider.IsPhysicalTable(table));
			}
			using (SqlDsvStatisticsProvider.SchemaReader schemaReader = this.ExecuteQuery(this.GetSqlForSelect(list, text)))
			{
				if (schemaReader.Read())
				{
					using (Dictionary<DsvColumn, int>.Enumerator enumerator2 = dictionary.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<DsvColumn, int> keyValuePair = enumerator2.Current;
							DsvColumn key = keyValuePair.Key;
							key.AvgWidth = new float?(schemaReader.GetSingle(keyValuePair.Value));
							key.StDevWidth = new float?(schemaReader.GetSingle(keyValuePair.Value + 1));
							key.MaxWidth = new int?(schemaReader.GetInt32(keyValuePair.Value + 2));
							DataType value = key.ModelingDataType.Value;
							if (value - DataType.Decimal <= 1)
							{
								key.AvgScale = new float?(schemaReader.GetSingle(keyValuePair.Value + 3));
								key.StDevScale = new float?(schemaReader.GetSingle(keyValuePair.Value + 4));
								key.MaxScale = new int?(schemaReader.GetInt32(keyValuePair.Value + 5));
							}
						}
						return;
					}
				}
				throw new InternalModelingException("Unexpected end of data for column width query");
			}
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002BCD8 File Offset: 0x00029ED8
		private SqlDsvStatisticsProvider.SchemaReader ExecuteQuery(string commandText)
		{
			IDbCommand dbCommand = null;
			SqlDsvStatisticsProvider.SchemaReader schemaReader;
			try
			{
				dbCommand = this.CreateCommand(commandText);
				schemaReader = new SqlDsvStatisticsProvider.SchemaReader(dbCommand.ExecuteReader(CommandBehavior.SingleResult));
			}
			catch (Exception ex)
			{
				throw this.WrapCommandException(ex, commandText);
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			return schemaReader;
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002BD30 File Offset: 0x00029F30
		private object ExecuteScalar(string commandText)
		{
			IDbCommand dbCommand = null;
			object obj;
			try
			{
				dbCommand = this.CreateCommand(commandText);
				obj = dbCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw this.WrapCommandException(ex, commandText);
			}
			finally
			{
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			return obj;
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002BD84 File Offset: 0x00029F84
		private Exception WrapCommandException(Exception ex, string commandText)
		{
			return new SqlDsvStatisticsProviderException(SR.SqlDsvStatisticsProvider_CommandException(ex.Message, commandText));
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002BD98 File Offset: 0x00029F98
		private DsvTable FindTable(string dbSchemaName, string dbTableName)
		{
			foreach (DsvTable dsvTable in this.m_tables)
			{
				if (this.m_dbObjectNameEqComp.Equals(dsvTable.DbSchemaName, dbSchemaName) && this.m_dbObjectNameEqComp.Equals(dsvTable.DbTableName, dbTableName))
				{
					return dsvTable;
				}
			}
			return null;
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002BE14 File Offset: 0x0002A014
		private DsvColumn FindColumn(DsvTable table, string dbColumnName)
		{
			if (table != null)
			{
				foreach (DsvColumn dsvColumn in table.Columns)
				{
					if (this.m_dbObjectNameEqComp.Equals(dsvColumn.DbColumnName, dbColumnName))
					{
						return dsvColumn;
					}
				}
			}
			return null;
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002BE80 File Offset: 0x0002A080
		private string GetSqlForTableSourceExpression(DsvTable table)
		{
			if (table.IsLogical)
			{
				return StringUtil.FormatInvariant("({0})", new object[] { table.QueryDefinition });
			}
			return this.GetDelimitedIdentifier(table.DbSchemaName, table.DbTableName);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002BEB6 File Offset: 0x0002A0B6
		private string GetSqlForSelect(IList<string> selectExpressions, string tableSource)
		{
			return StringUtil.FormatInvariant("SELECT\n\t{0}\nFROM {1} t", new object[]
			{
				StringUtil.Join(",\n\t", selectExpressions),
				tableSource
			});
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002BEDA File Offset: 0x0002A0DA
		private string GetSqlForTableRowCount(DsvTable table)
		{
			return StringUtil.FormatInvariant("SELECT " + this.GetSqlForCountExpression("*") + " FROM {0} t", new object[] { this.GetSqlForTableSourceExpression(table) });
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002BF0C File Offset: 0x0002A10C
		private string GetSqlForSelectCountDistinctComposite(IList<string> selectExpressions, string tableSource)
		{
			string[] array = new string[selectExpressions.Count];
			for (int i = 0; i < selectExpressions.Count; i++)
			{
				array[i] = StringUtil.FormatInvariant("{0} {1}{2}", new object[]
				{
					selectExpressions[i],
					"c",
					i
				});
			}
			return StringUtil.FormatInvariant("SELECT " + this.GetSqlForCountExpression("*") + " FROM (SELECT DISTINCT {0} FROM {1} t) t", new object[]
			{
				StringUtil.Join(", ", array),
				tableSource
			});
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002BF9C File Offset: 0x0002A19C
		private string[] GetSqlForColumnSelectExpressions(IList<DsvColumn> columns)
		{
			string[] array = new string[columns.Count];
			for (int i = 0; i < columns.Count; i++)
			{
				array[i] = this.GetSqlForColumnSelectExpression(columns[i]);
			}
			return array;
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002BFD7 File Offset: 0x0002A1D7
		private string GetDelimitedIdentifier(string qualifier, string name)
		{
			if (string.IsNullOrEmpty(qualifier))
			{
				return this.GetDelimitedIdentifier(name);
			}
			return StringUtil.FormatInvariant("{0}.{1}", new object[]
			{
				this.GetDelimitedIdentifier(qualifier),
				this.GetDelimitedIdentifier(name)
			});
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002C00D File Offset: 0x0002A20D
		private void RaiseProgress(string step, string objectName, int progress, int total)
		{
			if (this.Progress != null)
			{
				this.Progress(this, new ProgressEventArgs(step, objectName, progress, total));
			}
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002C02D File Offset: 0x0002A22D
		private void RaiseLog(LogEntryType type, string source, string message)
		{
			if (this.Log != null)
			{
				this.Log(this, new LogEventArgs(type, source, message));
			}
		}

		// Token: 0x04000558 RID: 1368
		private const int DefaultCommandTimeout = 300;

		// Token: 0x04000559 RID: 1369
		private const int ColumnUniquenessSampleRows = 100000;

		// Token: 0x0400055A RID: 1370
		private const int ColumnUniquenessMaxColumns = 15;

		// Token: 0x0400055B RID: 1371
		private const int ColumnWidthSampleRows = 100000;

		// Token: 0x0400055C RID: 1372
		private const int ColumnWidthMaxColumns = 15;

		// Token: 0x0400055D RID: 1373
		private readonly IDbConnection m_connection;

		// Token: 0x0400055E RID: 1374
		private int m_commandTimeout = 300;

		// Token: 0x0400055F RID: 1375
		private DataSourceView m_dsv;

		// Token: 0x04000560 RID: 1376
		private IEqualityComparer<string> m_dbObjectNameEqComp;

		// Token: 0x04000561 RID: 1377
		private IDsvItemFilter m_filter;

		// Token: 0x04000562 RID: 1378
		private bool m_overwrite;

		// Token: 0x04000563 RID: 1379
		private ICancelEvent m_cancelEvent;

		// Token: 0x04000564 RID: 1380
		private List<DsvTable> m_tables;

		// Token: 0x04000565 RID: 1381
		private List<DsvRelation> m_relations;

		// Token: 0x04000566 RID: 1382
		private Bag<DsvColumn> m_indexedColumns;

		// Token: 0x020001DB RID: 475
		private class SchemaReader : IDisposable
		{
			// Token: 0x060011B3 RID: 4531 RVA: 0x00037154 File Offset: 0x00035354
			internal SchemaReader(IDataReader reader)
			{
				if (reader == null)
				{
					throw new ArgumentNullException("reader");
				}
				this.m_reader = reader;
			}

			// Token: 0x060011B4 RID: 4532 RVA: 0x00037171 File Offset: 0x00035371
			internal bool Read()
			{
				return this.m_reader.Read();
			}

			// Token: 0x060011B5 RID: 4533 RVA: 0x0003717E File Offset: 0x0003537E
			internal int GetOrdinal(string name)
			{
				return this.m_reader.GetOrdinal(name);
			}

			// Token: 0x060011B6 RID: 4534 RVA: 0x0003718C File Offset: 0x0003538C
			internal object GetValue(int index)
			{
				object value = this.m_reader.GetValue(index);
				if (value is DBNull)
				{
					return null;
				}
				return value;
			}

			// Token: 0x060011B7 RID: 4535 RVA: 0x000371B4 File Offset: 0x000353B4
			internal T GetValueAs<T>(int index)
			{
				return this.GetValueAs<T>(index, default(T));
			}

			// Token: 0x060011B8 RID: 4536 RVA: 0x000371D4 File Offset: 0x000353D4
			internal T GetValueAs<T>(int index, T defaultValue)
			{
				object value = this.GetValue(index);
				if (value == null)
				{
					return defaultValue;
				}
				return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
			}

			// Token: 0x060011B9 RID: 4537 RVA: 0x00037208 File Offset: 0x00035408
			internal string GetString(int index)
			{
				return this.GetValueAs<string>(index);
			}

			// Token: 0x060011BA RID: 4538 RVA: 0x00037211 File Offset: 0x00035411
			internal int GetInt32(int index)
			{
				return this.GetValueAs<int>(index);
			}

			// Token: 0x060011BB RID: 4539 RVA: 0x0003721A File Offset: 0x0003541A
			internal long GetInt64(int index)
			{
				return this.GetValueAs<long>(index);
			}

			// Token: 0x060011BC RID: 4540 RVA: 0x00037223 File Offset: 0x00035423
			internal float GetSingle(int index)
			{
				return this.GetValueAs<float>(index);
			}

			// Token: 0x060011BD RID: 4541 RVA: 0x0003722C File Offset: 0x0003542C
			public void Dispose()
			{
				this.m_reader.Dispose();
			}

			// Token: 0x0400080B RID: 2059
			private readonly IDataReader m_reader;
		}

		// Token: 0x020001DC RID: 476
		public interface IColumnIndexesReader : IDisposable
		{
			// Token: 0x060011BE RID: 4542
			bool Read();

			// Token: 0x17000410 RID: 1040
			// (get) Token: 0x060011BF RID: 4543
			string IndexSchemaName { get; }

			// Token: 0x17000411 RID: 1041
			// (get) Token: 0x060011C0 RID: 4544
			string IndexName { get; }

			// Token: 0x17000412 RID: 1042
			// (get) Token: 0x060011C1 RID: 4545
			string TableSchemaName { get; }

			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x060011C2 RID: 4546
			string TableName { get; }

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x060011C3 RID: 4547
			IList<string> IndexColumns { get; }
		}

		// Token: 0x020001DD RID: 477
		public interface IGetColumnDbTypeName
		{
			// Token: 0x060011C4 RID: 4548
			string GetColumnDbTypeName(string tableSchema, string tableName, string columnDbName);
		}
	}
}
