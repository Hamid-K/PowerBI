using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Extensions;
using Microsoft.ReportingServices.SemanticQueryEngine;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x02000005 RID: 5
	internal abstract class SqlDsvGenerator : ITraceableComponent
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected SqlDsvGenerator(IDbConnection connection)
		{
			this.m_conn = connection;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002060 File Offset: 0x00000260
		internal void Generate(XmlWriter xmlWriter, DataSourceView oldDsv)
		{
			this.Process(delegate
			{
				this.TraceMessage(TraceLevel.Verbose, "Generating DSV...", Array.Empty<object>());
				this.m_dataSet = new DataSet(this.GetDsvName());
				DsvCompareInfo compareInfo = this.GetCompareInfo();
				DataSourceView.SetExtendedProperty(this.m_dataSet.ExtendedProperties, "CompareInfo", compareInfo);
				this.m_dbObjectNameEqComp = new SqlDsvGenerator.DbObjectNameEqComp(compareInfo);
				this.ProcessTables(this.GetTables());
				this.GenerateDSV(xmlWriter, oldDsv);
				this.TraceMessage(TraceLevel.Verbose, "DSV generation complete.", Array.Empty<object>());
			});
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000209C File Offset: 0x0000029C
		internal SqlDsvStatisticsProvider.IGetColumnDbTypeName GenerateColumnsExtendedInfo(DsvCompareInfo dci)
		{
			SqlDsvGenerator.ColumnsExtendedInfoDictionary colExtInfoDict = null;
			try
			{
				this.Process(delegate
				{
					this.TraceMessage(TraceLevel.Verbose, "Generating Columns Extened Info...", Array.Empty<object>());
					this.m_dbObjectNameEqComp = new SqlDsvGenerator.DbObjectNameEqComp(dci);
					this.LoadColumnsExtendedInfo();
					this.TraceMessage(TraceLevel.Verbose, "Columns Extened Info generation complete.", Array.Empty<object>());
					colExtInfoDict = this.m_colExtSchema;
				});
			}
			catch (Exception ex)
			{
				this.TraceMessage(TraceLevel.Error, "Error generating Columns Extened Info: {0}", new object[] { ex.ToString() });
			}
			return colExtInfoDict;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000210C File Offset: 0x0000030C
		void ITraceableComponent.SetTraceLog(ITraceLog traceLog)
		{
			this.m_traceLog = traceLog;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002115 File Offset: 0x00000315
		protected IDbConnection Connection
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_conn;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211D File Offset: 0x0000031D
		protected SqlDsvGenerator.DbObjectNameEqComp DbObjectNameEqComparer
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_dbObjectNameEqComp;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002125 File Offset: 0x00000325
		protected Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>> ColumnsExtendedSchema
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_colExtSchema.Dictionary;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002132 File Offset: 0x00000332
		protected ITraceLog TraceLog
		{
			get
			{
				return this.m_traceLog;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		protected DataColumn FindColumn(DataTable table, string columnDbName)
		{
			DataColumn dataColumn;
			if (this.m_tableColumns[table].TryGetValue(columnDbName, out dataColumn))
			{
				return dataColumn;
			}
			return null;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002162 File Offset: 0x00000362
		protected void TraceWarning(string format, params object[] args)
		{
			this.TraceMessage(TraceLevel.Warning, format, args);
		}

		// Token: 0x0600000B RID: 11
		protected abstract string GetDsvName();

		// Token: 0x0600000C RID: 12
		protected abstract DsvCompareInfo GetCompareInfo();

		// Token: 0x0600000D RID: 13
		protected abstract SqlDsvGenerator.DbObjectName CreateDbObjectName(string schemaName, string objectName);

		// Token: 0x0600000E RID: 14 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual string GetTableColumnsSelectList(SqlDsvGenerator.DbObjectName tableName)
		{
			return "*";
		}

		// Token: 0x0600000F RID: 15
		protected abstract SqlDsvGenerator.DbObjectName[] GetTables();

		// Token: 0x06000010 RID: 16
		protected abstract IEnumerable<SqlDsvGenerator.ColExtendedInfo> GetColExtInfo();

		// Token: 0x06000011 RID: 17
		protected abstract IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetPKInfo();

		// Token: 0x06000012 RID: 18
		protected abstract IEnumerable<SqlDsvGenerator.UniqueConstraintInfo> GetUCInfo();

		// Token: 0x06000013 RID: 19
		protected abstract IEnumerable<SqlDsvGenerator.FKConstraintInfo> GetFKInfo();

		// Token: 0x06000014 RID: 20
		protected abstract IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetPrimaryUniqueKeyColumnsInfo();

		// Token: 0x06000015 RID: 21
		protected abstract IEnumerable<SqlDsvGenerator.ConstraintColumnInfo> GetForeignKeyColumnsInfo();

		// Token: 0x06000016 RID: 22 RVA: 0x00002174 File Offset: 0x00000374
		private void Process(Microsoft.ReportingServices.Common.Operation operation)
		{
			bool flag = false;
			if (this.m_conn.State != ConnectionState.Open)
			{
				this.m_conn.Open();
				flag = true;
			}
			try
			{
				operation();
			}
			finally
			{
				if (flag)
				{
					this.m_conn.Close();
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021C8 File Offset: 0x000003C8
		private void GenerateDSV(XmlWriter xmlWriter, DataSourceView oldDsv)
		{
			string text = ((oldDsv == null) ? this.m_dataSet.DataSetName : oldDsv.Name);
			string text2 = ((oldDsv == null) ? this.m_dataSet.DataSetName : oldDsv.ID);
			string text3 = ((oldDsv == null) ? this.m_dataSet.DataSetName : oldDsv.DataSourceID);
			xmlWriter.WriteStartElement("DataSourceView", "http://schemas.microsoft.com/analysisservices/2003/engine");
			xmlWriter.WriteAttributeString("xmlns", "http://schemas.microsoft.com/analysisservices/2003/engine");
			xmlWriter.WriteAttributeString("xsi", "type", "http://www.w3.org/2001/XMLSchema-instance", "RelationalDataSourceView");
			xmlWriter.WriteElementString("Name", "http://schemas.microsoft.com/analysisservices/2003/engine", text);
			xmlWriter.WriteElementString("ID", "http://schemas.microsoft.com/analysisservices/2003/engine", text2);
			xmlWriter.WriteElementString("DataSourceID", "http://schemas.microsoft.com/analysisservices/2003/engine", text3);
			xmlWriter.WriteStartElement("Schema", "http://schemas.microsoft.com/analysisservices/2003/engine");
			this.m_dataSet.WriteXmlSchema(xmlWriter);
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000022B0 File Offset: 0x000004B0
		private void ProcessTables(SqlDsvGenerator.DbObjectName[] tables)
		{
			this.LoadColumnsExtendedInfo();
			this.m_tables = new Dictionary<SqlDsvGenerator.DbObjectName, DataTable>(this.m_dbObjectNameEqComp);
			this.m_tableColumns = new Dictionary<DataTable, Dictionary<string, DataColumn>>();
			foreach (SqlDsvGenerator.DbObjectName dbObjectName in tables)
			{
				if (this.m_tables.ContainsKey(dbObjectName))
				{
					this.TraceWarning("Found non-unique table: {0}. It will be ignored.", new object[] { dbObjectName.ToString() });
				}
				else
				{
					try
					{
						Dictionary<string, DataColumn> dictionary;
						DataTable dataTable = this.ProcessTable(dbObjectName, out dictionary);
						if (dataTable != null)
						{
							this.m_dataSet.Tables.Add(dataTable);
							this.m_tables.Add(dbObjectName, dataTable);
							this.m_tableColumns.Add(dataTable, dictionary);
						}
					}
					catch (Exception ex)
					{
						this.TraceWarning("Exception while processing schema of the table {0} : {1}", new object[]
						{
							dbObjectName.ToString(),
							ex.ToString()
						});
					}
				}
			}
			this.ProcessConstraints();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023A4 File Offset: 0x000005A4
		private DataTable ProcessTable(SqlDsvGenerator.DbObjectName tableName, out Dictionary<string, DataColumn> tableColumns)
		{
			tableColumns = null;
			List<string> list;
			DataTable initialColumnsSchema;
			try
			{
				initialColumnsSchema = this.GetInitialColumnsSchema(tableName, out list);
				if (initialColumnsSchema == null)
				{
					return null;
				}
			}
			catch (Exception ex)
			{
				this.TraceWarning("Exception while getting columns schema for the table {0} : {1}", new object[]
				{
					tableName.ToString(),
					ex.ToString()
				});
				return null;
			}
			DataSourceView.SetExtendedProperty(initialColumnsSchema.ExtendedProperties, "TableType", "Table");
			DataSourceView.SetExtendedProperty(initialColumnsSchema.ExtendedProperties, "FriendlyName", tableName.ObjectName);
			DataSourceView.SetExtendedProperty(initialColumnsSchema.ExtendedProperties, "DbSchemaName", tableName.SchemaName);
			DataSourceView.SetExtendedProperty(initialColumnsSchema.ExtendedProperties, "DbTableName", tableName.ObjectName);
			tableColumns = new Dictionary<string, DataColumn>(list.Count, this.m_dbObjectNameEqComp);
			for (int i = 0; i < initialColumnsSchema.Columns.Count; i++)
			{
				string text = list[i];
				DataColumn dataColumn = initialColumnsSchema.Columns[i];
				tableColumns.Add(text, dataColumn);
				DataSourceView.SetExtendedProperty(dataColumn.ExtendedProperties, "DbColumnName", text);
				SqlDsvGenerator.ColExtSchema colExtSchema = default(SqlDsvGenerator.ColExtSchema);
				if (this.m_colExtSchema.TryGetColExtSchema(tableName, text, out colExtSchema))
				{
					DataSourceView.SetExtendedProperty(dataColumn.ExtendedProperties, "DbDataType", colExtSchema.DbTypeName);
				}
				if (dataColumn.DataType == typeof(byte[]))
				{
					if (colExtSchema.Length != null)
					{
						DataSourceView.SetExtendedProperty(dataColumn.ExtendedProperties, "DataSize", colExtSchema.Length);
					}
					else
					{
						this.TraceWarning("Can not get data size of the column: {0}.{1}", new object[]
						{
							tableName.ToString(),
							text
						});
					}
				}
			}
			return initialColumnsSchema;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002558 File Offset: 0x00000758
		private DataTable GetInitialColumnsSchema(SqlDsvGenerator.DbObjectName tableName, out List<string> dbColumnNames)
		{
			dbColumnNames = null;
			IDbCommand dbCommand = null;
			IDataReader dataReader = null;
			DataTable dataTable;
			try
			{
				dbCommand = this.m_conn.CreateCommand();
				string tableColumnsSelectList = this.GetTableColumnsSelectList(tableName);
				if (tableColumnsSelectList == null)
				{
					dataTable = null;
				}
				else
				{
					dbCommand.CommandText = "SELECT " + tableColumnsSelectList + " FROM " + tableName.ToString();
					dbCommand.CommandType = CommandType.Text;
					dataReader = dbCommand.ExecuteReader(CommandBehavior.SchemaOnly);
					DataTable dataTable2 = new DataTable(this.GenerateUniqueTableID(tableName));
					dbColumnNames = new List<string>(dataReader.FieldCount);
					for (int i = 0; i < dataReader.FieldCount; i++)
					{
						dbColumnNames.Add(dataReader.GetName(i));
					}
					dataTable2.Load(dataReader);
					dataTable = dataTable2;
				}
			}
			finally
			{
				if (dataReader != null)
				{
					dataReader.Close();
				}
				if (dbCommand != null)
				{
					dbCommand.Dispose();
				}
			}
			return dataTable;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002628 File Offset: 0x00000828
		private void ProcessConstraints()
		{
			this.LoadConstraintsInfo();
			foreach (KeyValuePair<SqlDsvGenerator.DbObjectName, DataTable> keyValuePair in this.m_tables)
			{
				this.ProcessUniqueConstraints(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (KeyValuePair<SqlDsvGenerator.DbObjectName, DataTable> keyValuePair2 in this.m_tables)
			{
				this.ProcessFKConstraints(keyValuePair2.Key, keyValuePair2.Value);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000026E0 File Offset: 0x000008E0
		private void ProcessUniqueConstraints(SqlDsvGenerator.DbObjectName tableName, DataTable dataTable)
		{
			KeyValuePair<string, List<DataColumn>> keyValuePair;
			if (this.GetPrimaryKeyConstraint(tableName, out keyValuePair))
			{
				this.ProcessUniqueConstraint(tableName, dataTable, keyValuePair.Key, keyValuePair.Value, true);
			}
			foreach (KeyValuePair<string, List<DataColumn>> keyValuePair2 in this.GetUniqueConstraints(tableName, dataTable))
			{
				this.ProcessUniqueConstraint(tableName, dataTable, keyValuePair2.Key, keyValuePair2.Value, false);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002764 File Offset: 0x00000964
		private void ProcessUniqueConstraint(SqlDsvGenerator.DbObjectName tableName, DataTable dataTable, string constraintDbName, List<DataColumn> columns, bool primaryKey)
		{
			if (columns.Count == 0 || columns.Contains(null))
			{
				return;
			}
			try
			{
				UniqueConstraint uniqueConstraint = SqlDsvGenerator.FindUniqueConstraint(dataTable, columns);
				if (uniqueConstraint != null && uniqueConstraint.IsPrimaryKey != primaryKey)
				{
					dataTable.Constraints.Remove(uniqueConstraint);
					uniqueConstraint = null;
				}
				if (uniqueConstraint == null)
				{
					uniqueConstraint = this.CreateUniqueConstraint(dataTable, constraintDbName, columns, primaryKey);
					dataTable.Constraints.Add(uniqueConstraint);
				}
			}
			catch (Exception ex)
			{
				this.TraceWarning("Exception while processing constraint {0} of the table {1} : {2}", new object[]
				{
					constraintDbName,
					tableName.ToString(),
					ex.ToString()
				});
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002804 File Offset: 0x00000A04
		private static UniqueConstraint FindUniqueConstraint(DataTable dataTable, List<DataColumn> columns)
		{
			foreach (object obj in dataTable.Constraints)
			{
				UniqueConstraint uniqueConstraint = ((Constraint)obj) as UniqueConstraint;
				if (uniqueConstraint != null && uniqueConstraint.Columns.Length == columns.Count)
				{
					bool flag = true;
					for (int i = 0; i < columns.Count; i++)
					{
						if (!columns.Contains(uniqueConstraint.Columns[i]))
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return uniqueConstraint;
					}
				}
			}
			return null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000028A4 File Offset: 0x00000AA4
		private UniqueConstraint CreateUniqueConstraint(DataTable dataTable, string constraintName, List<DataColumn> columns, bool isPrimaryKey)
		{
			return new UniqueConstraint(this.GenerateUniqueConstraintID(dataTable, constraintName), columns.ToArray(), isPrimaryKey);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028BC File Offset: 0x00000ABC
		private void ProcessFKConstraints(SqlDsvGenerator.DbObjectName tableName, DataTable dataTable)
		{
			foreach (KeyValuePair<string, List<DataColumn[]>> keyValuePair in this.GetFKConstraints(tableName, dataTable))
			{
				if (keyValuePair.Value.Count != 0 && !keyValuePair.Value.Contains(null))
				{
					try
					{
						DataRelation dataRelation = this.CreateDataRelation(dataTable, keyValuePair.Key, keyValuePair.Value);
						if (dataRelation != null)
						{
							dataTable.DataSet.Relations.Add(dataRelation);
						}
					}
					catch (Exception ex)
					{
						this.TraceWarning("Exception while processing constraint {0} of the table {1} : {2}", new object[]
						{
							keyValuePair.Key.ToString(),
							tableName.ToString(),
							ex.ToString()
						});
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002998 File Offset: 0x00000B98
		private DataRelation CreateDataRelation(DataTable dataTable, string relationName, List<DataColumn[]> columnPairs)
		{
			List<DataColumn> list = new List<DataColumn>(columnPairs.Count);
			List<DataColumn> list2 = new List<DataColumn>(columnPairs.Count);
			for (int i = 0; i < columnPairs.Count; i++)
			{
				DataColumn[] array = columnPairs[i];
				if (array.Length != 2 || array[0] == null || array[1] == null)
				{
					return null;
				}
				DataColumn dataColumn = array[0];
				DataColumn dataColumn2 = array[1];
				if (dataColumn.DataType != dataColumn2.DataType)
				{
					if (dataColumn.DataType == typeof(byte) && dataColumn2.DataType == typeof(int))
					{
						dataColumn.DataType = typeof(int);
					}
					else
					{
						if (!(dataColumn.DataType == typeof(int)) || !(dataColumn2.DataType == typeof(byte)))
						{
							this.TraceWarning("Type mismatch between primary key '{0}':{1} and foreign key '{2}':{3} columns. Constraint {4} of the table {5}.", new object[] { dataColumn.ColumnName, dataColumn.DataType, dataColumn2.ColumnName, dataColumn2.DataType, dataTable.TableName, relationName });
							return null;
						}
						dataColumn2.DataType = typeof(int);
					}
				}
				list.Add(dataColumn);
				list2.Add(dataColumn2);
			}
			UniqueConstraint uniqueConstraint = SqlDsvGenerator.FindUniqueConstraint(list[0].Table, list);
			if (uniqueConstraint != null)
			{
				List<DataColumn> list3 = new List<DataColumn>(columnPairs.Count);
				List<DataColumn> list4 = new List<DataColumn>(columnPairs.Count);
				for (int j = 0; j < uniqueConstraint.Columns.Length; j++)
				{
					int num = list.IndexOf(uniqueConstraint.Columns[j]);
					list3.Add(list[num]);
					list4.Add(list2[num]);
				}
				list = list3;
				list2 = list4;
			}
			return new DataRelation(this.GenerateUniqueConstraintID(dataTable, relationName), list.ToArray(), list2.ToArray());
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B8C File Offset: 0x00000D8C
		private string GenerateUniqueTableID(SqlDsvGenerator.DbObjectName tableName)
		{
			string text;
			if (tableName.SchemaName == null || tableName.SchemaName.Length == 0)
			{
				text = tableName.ObjectName;
			}
			else
			{
				text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}_{1}", new object[] { tableName.SchemaName, tableName.ObjectName });
			}
			string text2 = text;
			int num = 0;
			while (this.m_dataSet.Tables.Contains(text2))
			{
				num++;
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
			}
			return text2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002C10 File Offset: 0x00000E10
		private string GenerateUniqueConstraintID(DataTable dataTable, string constraintName)
		{
			string text = Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("{0}_{1}", new object[] { dataTable.TableName, constraintName });
			string text2 = text;
			int num = 0;
			while (dataTable.Constraints.Contains(text2))
			{
				num++;
				text2 = text + num.ToString(CultureInfo.InvariantCulture);
			}
			return text2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002C67 File Offset: 0x00000E67
		private bool GetPrimaryKeyConstraint(SqlDsvGenerator.DbObjectName tableName, out KeyValuePair<string, List<DataColumn>> pkInfo)
		{
			return this.m_primaryKeys.TryGetValue(tableName, out pkInfo);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002C76 File Offset: 0x00000E76
		private IEnumerable<KeyValuePair<string, List<DataColumn>>> GetUniqueConstraints(SqlDsvGenerator.DbObjectName tableName, DataTable dataTable)
		{
			Microsoft.ReportingServices.Common.Bag<string> constraints = new Microsoft.ReportingServices.Common.Bag<string>(this.m_dbObjectNameEqComp);
			Dictionary<string, List<DataColumn>> dictionary;
			if (this.m_uniqueKeys.TryGetValue(tableName, out dictionary))
			{
				foreach (KeyValuePair<string, List<DataColumn>> keyValuePair in dictionary)
				{
					if (constraints.Contains(keyValuePair.Key))
					{
						this.TraceWarning("Table {0} already has a unique constraint with the name {1}. Last instance of the constraint will be ignored.", new object[]
						{
							tableName.ToString(),
							keyValuePair.Key
						});
					}
					else
					{
						yield return keyValuePair;
					}
				}
				Dictionary<string, List<DataColumn>>.Enumerator enumerator = default(Dictionary<string, List<DataColumn>>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C8D File Offset: 0x00000E8D
		private IEnumerable<KeyValuePair<string, List<DataColumn[]>>> GetFKConstraints(SqlDsvGenerator.DbObjectName tableName, DataTable dataTable)
		{
			Microsoft.ReportingServices.Common.Bag<string> constraints = new Microsoft.ReportingServices.Common.Bag<string>(this.m_dbObjectNameEqComp);
			Dictionary<string, SqlDsvGenerator.FKSchema> dictionary;
			if (this.m_foreignKeys.TryGetValue(tableName, out dictionary))
			{
				foreach (KeyValuePair<string, SqlDsvGenerator.FKSchema> keyValuePair in dictionary)
				{
					List<DataColumn> list;
					if (constraints.Contains(keyValuePair.Key))
					{
						this.TraceWarning("Table {0} already has a constraint with the name {1}. Last instance of the constraint will be ignored.", new object[]
						{
							tableName.ToString(),
							keyValuePair.Key
						});
					}
					else if (!this.TryGetPrimaryUniqueKeyConstraintColumns(keyValuePair.Value.PkTableName, keyValuePair.Value.PkConstraintName, out list))
					{
						this.TraceWarning("Can not find primary/unique key {0} in table {1}. DataRelation {2} will not be created.", new object[]
						{
							keyValuePair.Value.PkConstraintName,
							keyValuePair.Value.PkTableName.ToString(),
							keyValuePair.Key
						});
					}
					else if (list.Count != keyValuePair.Value.FkColumns.Count)
					{
						this.TraceWarning("Number of primary key columns does not match with the number of foreign key columns in the constraint {0} in table {1}. DataRelation {0} will not be created.", new object[]
						{
							keyValuePair.Key,
							tableName.ToString()
						});
					}
					else
					{
						List<DataColumn[]> list2 = new List<DataColumn[]>(list.Count);
						for (int i = 0; i < list.Count; i++)
						{
							list2.Add(new DataColumn[]
							{
								list[i],
								keyValuePair.Value.FkColumns[i]
							});
						}
						constraints.Add(keyValuePair.Key);
						yield return new KeyValuePair<string, List<DataColumn[]>>(keyValuePair.Key, list2);
					}
				}
				Dictionary<string, SqlDsvGenerator.FKSchema>.Enumerator enumerator = default(Dictionary<string, SqlDsvGenerator.FKSchema>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CA4 File Offset: 0x00000EA4
		private void LoadColumnsExtendedInfo()
		{
			Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>> dictionary = new Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>>(this.m_dbObjectNameEqComp);
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName("", "");
			foreach (SqlDsvGenerator.ColExtendedInfo colExtendedInfo in this.GetColExtInfo())
			{
				if (dbObjectName.SchemaName != colExtendedInfo.SchemaName || dbObjectName.ObjectName != colExtendedInfo.TableName)
				{
					dbObjectName = this.CreateDbObjectName(colExtendedInfo.SchemaName, colExtendedInfo.TableName);
				}
				Dictionary<string, SqlDsvGenerator.ColExtSchema> dictionary2;
				if (!dictionary.TryGetValue(dbObjectName, out dictionary2))
				{
					dictionary2 = new Dictionary<string, SqlDsvGenerator.ColExtSchema>(this.m_dbObjectNameEqComp);
					dictionary.Add(dbObjectName, dictionary2);
				}
				if (dictionary2.ContainsKey(colExtendedInfo.ColumnName))
				{
					this.TraceWarning("Table {0} already has a column with the name {1}. Last instance of the column will be ignored.", new object[]
					{
						dbObjectName.ToString(),
						colExtendedInfo.ColumnName
					});
				}
				else
				{
					dictionary2.Add(colExtendedInfo.ColumnName, new SqlDsvGenerator.ColExtSchema(colExtendedInfo.DbTypeName, colExtendedInfo.Length));
				}
			}
			this.m_colExtSchema = new SqlDsvGenerator.ColumnsExtendedInfoDictionary(dictionary, this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002DC8 File Offset: 0x00000FC8
		private void LoadConstraintsInfo()
		{
			this.m_primaryKeys = new Dictionary<SqlDsvGenerator.DbObjectName, KeyValuePair<string, List<DataColumn>>>(this.m_dbObjectNameEqComp);
			foreach (SqlDsvGenerator.UniqueConstraintInfo uniqueConstraintInfo in this.GetPKInfo())
			{
				this.AddPrimaryKeyConstraintInfo(uniqueConstraintInfo.SchemaName, uniqueConstraintInfo.TableName, uniqueConstraintInfo.ConstraintName);
			}
			this.m_uniqueKeys = new Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, List<DataColumn>>>(this.m_dbObjectNameEqComp);
			foreach (SqlDsvGenerator.UniqueConstraintInfo uniqueConstraintInfo2 in this.GetUCInfo())
			{
				this.AddUniqueKeyConstraintInfo(uniqueConstraintInfo2.SchemaName, uniqueConstraintInfo2.TableName, uniqueConstraintInfo2.ConstraintName);
			}
			this.LoadPrimaryUniqueKeyColumnsInfo();
			this.m_foreignKeys = new Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.FKSchema>>(this.m_dbObjectNameEqComp);
			foreach (SqlDsvGenerator.FKConstraintInfo fkconstraintInfo in this.GetFKInfo())
			{
				this.AddForeignKeyConstraintInfo(fkconstraintInfo.FKSchemaName, fkconstraintInfo.FKTableName, fkconstraintInfo.FKConstraintName, fkconstraintInfo.PKSchemaName, fkconstraintInfo.PKTableName, fkconstraintInfo.PKConstraintName);
			}
			this.LoadForeignKeyColumnsInfo();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002F18 File Offset: 0x00001118
		private void LoadPrimaryUniqueKeyColumnsInfo()
		{
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName("", "");
			foreach (SqlDsvGenerator.ConstraintColumnInfo constraintColumnInfo in this.GetPrimaryUniqueKeyColumnsInfo())
			{
				if (dbObjectName.SchemaName != constraintColumnInfo.SchemaName || dbObjectName.ObjectName != constraintColumnInfo.TableName)
				{
					dbObjectName = this.CreateDbObjectName(constraintColumnInfo.SchemaName, constraintColumnInfo.TableName);
				}
				DataColumn dataColumn = null;
				DataTable dataTable;
				if (this.m_tables.TryGetValue(dbObjectName, out dataTable))
				{
					dataColumn = this.FindColumn(dataTable, constraintColumnInfo.ColumnName);
				}
				List<DataColumn> list;
				if (this.TryGetPrimaryUniqueKeyConstraintColumns(dbObjectName, constraintColumnInfo.ConstraintName, out list))
				{
					list.Add(dataColumn);
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002FE8 File Offset: 0x000011E8
		private void LoadForeignKeyColumnsInfo()
		{
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName("", "");
			foreach (SqlDsvGenerator.ConstraintColumnInfo constraintColumnInfo in this.GetForeignKeyColumnsInfo())
			{
				if (dbObjectName.SchemaName != constraintColumnInfo.SchemaName || dbObjectName.ObjectName != constraintColumnInfo.TableName)
				{
					dbObjectName = this.CreateDbObjectName(constraintColumnInfo.SchemaName, constraintColumnInfo.TableName);
				}
				DataColumn dataColumn = null;
				DataTable dataTable;
				if (this.m_tables.TryGetValue(dbObjectName, out dataTable))
				{
					dataColumn = this.FindColumn(dataTable, constraintColumnInfo.ColumnName);
				}
				List<DataColumn> list;
				if (this.TryGetForeignKeyConstraintColumns(dbObjectName, constraintColumnInfo.ConstraintName, out list))
				{
					list.Add(dataColumn);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000030B8 File Offset: 0x000012B8
		private void AddPrimaryKeyConstraintInfo(string tableOwner, string tableName, string constraintName)
		{
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName(tableOwner, tableName);
			if (this.m_primaryKeys.ContainsKey(dbObjectName))
			{
				this.TraceWarning("Table {0} already has a primary key constraint. Constraint {1} will be ignored.", new object[]
				{
					dbObjectName.ToString(),
					constraintName
				});
				return;
			}
			this.m_primaryKeys.Add(dbObjectName, new KeyValuePair<string, List<DataColumn>>(constraintName, new List<DataColumn>()));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003114 File Offset: 0x00001314
		private void AddUniqueKeyConstraintInfo(string tableOwner, string tableName, string constraintName)
		{
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName(tableOwner, tableName);
			Dictionary<string, List<DataColumn>> dictionary;
			if (!this.m_uniqueKeys.TryGetValue(dbObjectName, out dictionary))
			{
				dictionary = new Dictionary<string, List<DataColumn>>(this.m_dbObjectNameEqComp);
				this.m_uniqueKeys.Add(dbObjectName, dictionary);
			}
			if (dictionary.ContainsKey(constraintName))
			{
				this.TraceWarning("Table {0} already has a unique constraint with the name {1}. Last instance of the constraint will be ignored.", new object[]
				{
					dbObjectName.ToString(),
					constraintName
				});
				return;
			}
			dictionary.Add(constraintName, new List<DataColumn>());
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003188 File Offset: 0x00001388
		private bool TryGetPrimaryUniqueKeyConstraintColumns(SqlDsvGenerator.DbObjectName tableName, string constraintName, out List<DataColumn> columns)
		{
			columns = null;
			Dictionary<string, List<DataColumn>> dictionary;
			if (this.m_uniqueKeys.TryGetValue(tableName, out dictionary) && dictionary.TryGetValue(constraintName, out columns))
			{
				return true;
			}
			KeyValuePair<string, List<DataColumn>> keyValuePair;
			if (this.m_primaryKeys.TryGetValue(tableName, out keyValuePair) && this.m_dbObjectNameEqComp.Equals(keyValuePair.Key, constraintName))
			{
				columns = keyValuePair.Value;
				return true;
			}
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000031E8 File Offset: 0x000013E8
		private void AddForeignKeyConstraintInfo(string fkTableOwner, string fkTableName, string fkConstraintName, string pkTableOwner, string pkTableName, string pkConstraintName)
		{
			SqlDsvGenerator.DbObjectName dbObjectName = this.CreateDbObjectName(fkTableOwner, fkTableName);
			SqlDsvGenerator.DbObjectName dbObjectName2 = this.CreateDbObjectName(pkTableOwner, pkTableName);
			Dictionary<string, SqlDsvGenerator.FKSchema> dictionary;
			if (!this.m_foreignKeys.TryGetValue(dbObjectName, out dictionary))
			{
				dictionary = new Dictionary<string, SqlDsvGenerator.FKSchema>(this.m_dbObjectNameEqComp);
				this.m_foreignKeys.Add(dbObjectName, dictionary);
			}
			if (dictionary.ContainsKey(fkConstraintName))
			{
				this.TraceWarning("Table {0} already has a foreign constraint with the name {1}. Last instance of the constraint will be ignored.", new object[]
				{
					dbObjectName.ToString(),
					fkConstraintName
				});
				return;
			}
			dictionary.Add(fkConstraintName, new SqlDsvGenerator.FKSchema(dbObjectName2, pkConstraintName));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000326C File Offset: 0x0000146C
		private bool TryGetForeignKeyConstraintColumns(SqlDsvGenerator.DbObjectName fkTableName, string fkConstraintName, out List<DataColumn> columns)
		{
			columns = null;
			Dictionary<string, SqlDsvGenerator.FKSchema> dictionary;
			SqlDsvGenerator.FKSchema fkschema;
			if (this.m_foreignKeys.TryGetValue(fkTableName, out dictionary) && dictionary.TryGetValue(fkConstraintName, out fkschema))
			{
				columns = fkschema.FkColumns;
				return true;
			}
			return false;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000032A4 File Offset: 0x000014A4
		private void TraceMessage(TraceLevel level, string format, params object[] args)
		{
			if (this.m_traceLog != null)
			{
				switch (level)
				{
				case TraceLevel.Error:
					if (!this.m_traceLog.TraceError)
					{
						return;
					}
					break;
				case TraceLevel.Warning:
					if (!this.m_traceLog.TraceWarning)
					{
						return;
					}
					break;
				case TraceLevel.Info:
					if (!this.m_traceLog.TraceInfo)
					{
						return;
					}
					break;
				case TraceLevel.Verbose:
					if (!this.m_traceLog.TraceVerbose)
					{
						return;
					}
					break;
				}
				this.m_traceLog.WriteTrace(Microsoft.ReportingServices.Common.StringUtil.FormatInvariant(format, args), level);
			}
		}

		// Token: 0x04000035 RID: 53
		private readonly IDbConnection m_conn;

		// Token: 0x04000036 RID: 54
		private ITraceLog m_traceLog;

		// Token: 0x04000037 RID: 55
		private DataSet m_dataSet;

		// Token: 0x04000038 RID: 56
		private SqlDsvGenerator.DbObjectNameEqComp m_dbObjectNameEqComp;

		// Token: 0x04000039 RID: 57
		private Dictionary<SqlDsvGenerator.DbObjectName, DataTable> m_tables;

		// Token: 0x0400003A RID: 58
		private Dictionary<DataTable, Dictionary<string, DataColumn>> m_tableColumns;

		// Token: 0x0400003B RID: 59
		private SqlDsvGenerator.ColumnsExtendedInfoDictionary m_colExtSchema;

		// Token: 0x0400003C RID: 60
		private Dictionary<SqlDsvGenerator.DbObjectName, KeyValuePair<string, List<DataColumn>>> m_primaryKeys;

		// Token: 0x0400003D RID: 61
		private Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, List<DataColumn>>> m_uniqueKeys;

		// Token: 0x0400003E RID: 62
		private Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.FKSchema>> m_foreignKeys;

		// Token: 0x0200007E RID: 126
		protected struct ColExtendedInfo
		{
			// Token: 0x06000545 RID: 1349 RVA: 0x00015F72 File Offset: 0x00014172
			internal ColExtendedInfo(string schemaName, string tableName, string columnName, string dbTypeName, string length)
			{
				this.SchemaName = schemaName;
				this.TableName = tableName;
				this.ColumnName = columnName;
				this.DbTypeName = dbTypeName;
				this.Length = length;
			}

			// Token: 0x04000211 RID: 529
			internal readonly string SchemaName;

			// Token: 0x04000212 RID: 530
			internal readonly string TableName;

			// Token: 0x04000213 RID: 531
			internal readonly string ColumnName;

			// Token: 0x04000214 RID: 532
			internal readonly string DbTypeName;

			// Token: 0x04000215 RID: 533
			internal readonly string Length;
		}

		// Token: 0x0200007F RID: 127
		protected struct UniqueConstraintInfo
		{
			// Token: 0x06000546 RID: 1350 RVA: 0x00015F99 File Offset: 0x00014199
			internal UniqueConstraintInfo(string schemaName, string tableName, string constraintName)
			{
				this.SchemaName = schemaName;
				this.TableName = tableName;
				this.ConstraintName = constraintName;
			}

			// Token: 0x04000216 RID: 534
			internal readonly string SchemaName;

			// Token: 0x04000217 RID: 535
			internal readonly string TableName;

			// Token: 0x04000218 RID: 536
			internal readonly string ConstraintName;
		}

		// Token: 0x02000080 RID: 128
		protected struct FKConstraintInfo
		{
			// Token: 0x06000547 RID: 1351 RVA: 0x00015FB0 File Offset: 0x000141B0
			internal FKConstraintInfo(string fkSchemaName, string fkTableName, string fkConstraintName, string pkSchemaName, string pkTableName, string pkConstraintName)
			{
				this.FKSchemaName = fkSchemaName;
				this.FKTableName = fkTableName;
				this.FKConstraintName = fkConstraintName;
				this.PKSchemaName = pkSchemaName;
				this.PKTableName = pkTableName;
				this.PKConstraintName = pkConstraintName;
			}

			// Token: 0x04000219 RID: 537
			internal readonly string FKSchemaName;

			// Token: 0x0400021A RID: 538
			internal readonly string FKTableName;

			// Token: 0x0400021B RID: 539
			internal readonly string FKConstraintName;

			// Token: 0x0400021C RID: 540
			internal readonly string PKSchemaName;

			// Token: 0x0400021D RID: 541
			internal readonly string PKTableName;

			// Token: 0x0400021E RID: 542
			internal readonly string PKConstraintName;
		}

		// Token: 0x02000081 RID: 129
		protected struct ConstraintColumnInfo
		{
			// Token: 0x06000548 RID: 1352 RVA: 0x00015FDF File Offset: 0x000141DF
			internal ConstraintColumnInfo(string schemaName, string tableName, string constraintName, string columnName)
			{
				this.SchemaName = schemaName;
				this.TableName = tableName;
				this.ConstraintName = constraintName;
				this.ColumnName = columnName;
			}

			// Token: 0x0400021F RID: 543
			internal readonly string SchemaName;

			// Token: 0x04000220 RID: 544
			internal readonly string TableName;

			// Token: 0x04000221 RID: 545
			internal readonly string ConstraintName;

			// Token: 0x04000222 RID: 546
			internal readonly string ColumnName;
		}

		// Token: 0x02000082 RID: 130
		internal abstract class DbObjectName
		{
			// Token: 0x06000549 RID: 1353 RVA: 0x00015FFE File Offset: 0x000141FE
			protected DbObjectName(string schemaName, string objectName)
			{
				if (schemaName == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("schemaName"));
				}
				if (objectName == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("objectName"));
				}
				this.SchemaName = schemaName;
				this.ObjectName = objectName;
			}

			// Token: 0x0600054A RID: 1354 RVA: 0x0001603A File Offset: 0x0001423A
			public override string ToString()
			{
				throw new NotImplementedException();
			}

			// Token: 0x04000223 RID: 547
			internal readonly string SchemaName;

			// Token: 0x04000224 RID: 548
			internal readonly string ObjectName;
		}

		// Token: 0x02000083 RID: 131
		internal sealed class DbObjectNameEqComp : IEqualityComparer<SqlDsvGenerator.DbObjectName>, IEqualityComparer<string>
		{
			// Token: 0x0600054B RID: 1355 RVA: 0x00016044 File Offset: 0x00014244
			internal DbObjectNameEqComp(DsvCompareInfo dci)
			{
				if (dci != null)
				{
					this.m_comparer = dci.CreateComparer();
					this.m_caseSensitive = !dci.IgnoreCase;
					this.m_cultureInfo = dci.Culture;
					return;
				}
				this.m_comparer = StringComparer.InvariantCulture;
				this.m_caseSensitive = true;
				this.m_cultureInfo = CultureInfo.InvariantCulture;
			}

			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x0600054C RID: 1356 RVA: 0x0001609F File Offset: 0x0001429F
			internal bool CaseSensitive
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_caseSensitive;
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x0600054D RID: 1357 RVA: 0x000160A7 File Offset: 0x000142A7
			internal CultureInfo CultureInfo
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_cultureInfo;
				}
			}

			// Token: 0x0600054E RID: 1358 RVA: 0x000160B0 File Offset: 0x000142B0
			public bool Equals(SqlDsvGenerator.DbObjectName x, SqlDsvGenerator.DbObjectName y)
			{
				return x == y || (x != null && y != null && this.m_comparer.Equals(x.ObjectName, y.ObjectName) && this.m_comparer.Equals(x.SchemaName, y.SchemaName));
			}

			// Token: 0x0600054F RID: 1359 RVA: 0x000160FD File Offset: 0x000142FD
			public int GetHashCode(SqlDsvGenerator.DbObjectName obj)
			{
				return this.m_comparer.GetHashCode((obj != null) ? obj.ObjectName : string.Empty);
			}

			// Token: 0x06000550 RID: 1360 RVA: 0x0001611A File Offset: 0x0001431A
			public bool Equals(string x, string y)
			{
				return this.m_comparer.Equals(x, y);
			}

			// Token: 0x06000551 RID: 1361 RVA: 0x00016129 File Offset: 0x00014329
			public int GetHashCode(string obj)
			{
				return this.m_comparer.GetHashCode(obj ?? string.Empty);
			}

			// Token: 0x04000225 RID: 549
			private readonly IEqualityComparer<string> m_comparer;

			// Token: 0x04000226 RID: 550
			private readonly bool m_caseSensitive;

			// Token: 0x04000227 RID: 551
			private readonly CultureInfo m_cultureInfo;
		}

		// Token: 0x02000084 RID: 132
		protected sealed class ColumnsExtendedInfoDictionary : SqlDsvStatisticsProvider.IGetColumnDbTypeName
		{
			// Token: 0x06000552 RID: 1362 RVA: 0x00016140 File Offset: 0x00014340
			internal ColumnsExtendedInfoDictionary(Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>> colExtSchemaDict, SqlDsvGenerator dsvGen)
			{
				this.m_colExtSchemaDict = colExtSchemaDict;
				this.m_dsvGen = dsvGen;
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x06000553 RID: 1363 RVA: 0x00016156 File Offset: 0x00014356
			internal Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>> Dictionary
			{
				get
				{
					return this.m_colExtSchemaDict;
				}
			}

			// Token: 0x06000554 RID: 1364 RVA: 0x00016160 File Offset: 0x00014360
			public string GetColumnDbTypeName(string tableSchema, string tableName, string columnDbName)
			{
				SqlDsvGenerator.ColExtSchema colExtSchema;
				if (this.TryGetColExtSchema(this.m_dsvGen.CreateDbObjectName(tableSchema, tableName), columnDbName, out colExtSchema))
				{
					return colExtSchema.DbTypeName;
				}
				return null;
			}

			// Token: 0x06000555 RID: 1365 RVA: 0x00016190 File Offset: 0x00014390
			internal bool TryGetColExtSchema(SqlDsvGenerator.DbObjectName tableName, string columnDbName, out SqlDsvGenerator.ColExtSchema colExtSchema)
			{
				Dictionary<string, SqlDsvGenerator.ColExtSchema> dictionary;
				if (this.m_colExtSchemaDict.TryGetValue(tableName, out dictionary) && dictionary.TryGetValue(columnDbName, out colExtSchema))
				{
					return true;
				}
				colExtSchema = default(SqlDsvGenerator.ColExtSchema);
				return false;
			}

			// Token: 0x04000228 RID: 552
			private readonly Dictionary<SqlDsvGenerator.DbObjectName, Dictionary<string, SqlDsvGenerator.ColExtSchema>> m_colExtSchemaDict;

			// Token: 0x04000229 RID: 553
			private readonly SqlDsvGenerator m_dsvGen;
		}

		// Token: 0x02000085 RID: 133
		internal struct ColExtSchema
		{
			// Token: 0x06000556 RID: 1366 RVA: 0x000161C1 File Offset: 0x000143C1
			internal ColExtSchema(string dbTypeName, string length)
			{
				this.DbTypeName = dbTypeName;
				this.Length = length;
			}

			// Token: 0x0400022A RID: 554
			internal readonly string DbTypeName;

			// Token: 0x0400022B RID: 555
			internal readonly string Length;
		}

		// Token: 0x02000086 RID: 134
		private sealed class FKSchema
		{
			// Token: 0x06000557 RID: 1367 RVA: 0x000161D1 File Offset: 0x000143D1
			internal FKSchema(SqlDsvGenerator.DbObjectName pkTableName, string pkConstraintName)
			{
				this.PkTableName = pkTableName;
				this.PkConstraintName = pkConstraintName;
			}

			// Token: 0x0400022C RID: 556
			internal readonly SqlDsvGenerator.DbObjectName PkTableName;

			// Token: 0x0400022D RID: 557
			internal readonly string PkConstraintName;

			// Token: 0x0400022E RID: 558
			internal List<DataColumn> FkColumns = new List<DataColumn>();
		}
	}
}
