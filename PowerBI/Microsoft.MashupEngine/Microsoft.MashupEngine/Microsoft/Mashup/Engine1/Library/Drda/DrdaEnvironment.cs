using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000C9E RID: 3230
	internal abstract class DrdaEnvironment : DbEnvironment
	{
		// Token: 0x06005733 RID: 22323 RVA: 0x0012EE24 File Offset: 0x0012D024
		static DrdaEnvironment()
		{
			try
			{
				AssemblyName assemblyName = new AssemblyName("Microsoft.HostIntegration.Connectors");
				assemblyName.SetPublicKeyToken(Assembly.GetExecutingAssembly().GetName().GetPublicKeyToken());
				Assembly assembly = Assembly.Load(assemblyName);
				DrdaEnvironment.drdaFactory = assembly.GetType("Microsoft.HostIntegration.DrdaClient.DrdaFactory").GetField("Instance").GetValue(null) as DbProviderFactory;
				DrdaEnvironment.connectionServerClassInfo = assembly.GetType("Microsoft.HostIntegration.DrdaClient.DrdaConnection").GetProperty("ServerClass");
				DrdaEnvironment.getInformixDatetimeTypeName = assembly.GetType("Microsoft.HostIntegration.Drda.Requester.InformixDataTypeHelper").GetMethod("GetInformixDatetimeTypeName");
				DrdaEnvironment.drdaTableTypes = new Dictionary<string, TableType>(DbEnvironment.defaultTableTypes, StringComparer.OrdinalIgnoreCase);
				DrdaEnvironment.drdaTableTypes.Add("ALIAS", new TableType("ALIAS", "Table"));
				DrdaEnvironment.drdaTableTypes.Add("NICKNAME", new TableType("NICKNAME", "Table"));
				DrdaEnvironment.drdaTableTypes.Add("SYNONYM", new TableType("SYNONYM", "Table"));
			}
			catch (FileLoadException ex)
			{
				DrdaEnvironment.exceptionWhenLoadingAssembly = ex;
			}
			catch (BadImageFormatException ex2)
			{
				DrdaEnvironment.exceptionWhenLoadingAssembly = ex2;
			}
		}

		// Token: 0x06005734 RID: 22324 RVA: 0x0012EF5C File Offset: 0x0012D15C
		public DrdaEnvironment(IEngineHost host, string resourceKind, string dataSourceName, string server, string database, Value options)
			: base(host, DatabaseResource.New(resourceKind, server, database), dataSourceName, server, database, options, null, null)
		{
		}

		// Token: 0x17001A41 RID: 6721
		// (get) Token: 0x06005735 RID: 22325 RVA: 0x0012EF83 File Offset: 0x0012D183
		protected override IDictionary<string, TableType> TableTypes
		{
			get
			{
				return DrdaEnvironment.drdaTableTypes;
			}
		}

		// Token: 0x17001A42 RID: 6722
		// (get) Token: 0x06005736 RID: 22326
		protected abstract int DefaultPort { get; }

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06005737 RID: 22327
		protected abstract DrdaFlavor Flavor { get; }

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06005738 RID: 22328
		protected abstract int BinaryCodePage { get; }

		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x06005739 RID: 22329 RVA: 0x000020FA File Offset: 0x000002FA
		protected virtual string PackageCollection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x0600573A RID: 22330 RVA: 0x0012EF8A File Offset: 0x0012D18A
		// (set) Token: 0x0600573B RID: 22331 RVA: 0x0012EF92 File Offset: 0x0012D192
		protected bool? UseDb2ConnectGateway { get; set; }

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x0600573C RID: 22332 RVA: 0x0012EF9B File Offset: 0x0012D19B
		// (set) Token: 0x0600573D RID: 22333 RVA: 0x0012EFA3 File Offset: 0x0012D1A3
		protected bool ForceUseDb2ConnectGateway
		{
			get
			{
				return this.forceUseDb2ConnectGateway;
			}
			set
			{
				if (value != this.forceUseDb2ConnectGateway)
				{
					base.ClearConnectionInfo();
				}
				this.forceUseDb2ConnectGateway = value;
			}
		}

		// Token: 0x0600573E RID: 22334 RVA: 0x0012EFBB File Offset: 0x0012D1BB
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			if (DrdaEnvironment.drdaFactory == null)
			{
				throw DataSourceException.NewMissingClientLibraryError<Message2>(base.Host, DbEnvironment.GetClientSoftwareNotFoundExceptionMessage(this.ProviderLibraryName, this.ProviderDownloadLink), null, this.ProviderLibraryName, this.ProviderDownloadLink, DrdaEnvironment.exceptionWhenLoadingAssembly);
			}
			return DrdaEnvironment.drdaFactory;
		}

		// Token: 0x17001A48 RID: 6728
		// (get) Token: 0x0600573F RID: 22335 RVA: 0x0012EFF8 File Offset: 0x0012D1F8
		protected override string ProviderName
		{
			get
			{
				return "Microsoft.HostIntegration.DrdaClient";
			}
		}

		// Token: 0x06005740 RID: 22336 RVA: 0x0012EFFF File Offset: 0x0012D1FF
		protected override string NormalizeDataType(string dataType)
		{
			return dataType.ToLowerInvariant();
		}

		// Token: 0x06005741 RID: 22337 RVA: 0x0012F008 File Offset: 0x0012D208
		private static int? GetSqlCode(DbException exception)
		{
			if (string.Equals(exception.GetType().FullName, "Microsoft.HostIntegration.DrdaClient.DrdaException"))
			{
				PropertyInfo property = exception.GetType().GetProperty("SqlCode");
				if (property != null)
				{
					return new int?((int)property.GetValue(exception, EmptyArray<object>.Instance));
				}
			}
			return null;
		}

		// Token: 0x06005742 RID: 22338 RVA: 0x0012F068 File Offset: 0x0012D268
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			int? sqlCode = DrdaEnvironment.GetSqlCode(exception);
			if (sqlCode != null)
			{
				using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
				{
					ResourceExceptionKind resourceExceptionKind = ResourceExceptionKind.None;
					hostTrace.Add("Exception", exception, true);
					hostTrace.Add("SqlCode", sqlCode.Value, false);
					int value = sqlCode.Value;
					if (value <= -1006)
					{
						if (value <= -1036)
						{
							if (value == -1038)
							{
								resourceExceptionKind = ResourceExceptionKind.SecureConnectionFailed;
								goto IL_00C2;
							}
							if (value != -1036)
							{
								goto IL_00C2;
							}
						}
						else if (value != -1030 && value - -1007 > 1)
						{
							goto IL_00C2;
						}
					}
					else if (value <= -922)
					{
						if (value != -1004 && value != -922)
						{
							goto IL_00C2;
						}
					}
					else if (value != -606 && value != -567 && value != -551)
					{
						goto IL_00C2;
					}
					resourceExceptionKind = ResourceExceptionKind.InvalidCredentials;
					IL_00C2:
					hostTrace.Add("ResourceExceptionKind", resourceExceptionKind.ToString(), false);
					return resourceExceptionKind;
				}
				return ResourceExceptionKind.None;
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x17001A49 RID: 6729
		// (get) Token: 0x06005743 RID: 22339 RVA: 0x0012F174 File Offset: 0x0012D374
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/?LinkId=528259";
			}
		}

		// Token: 0x17001A4A RID: 6730
		// (get) Token: 0x06005744 RID: 22340 RVA: 0x0012F17B File Offset: 0x0012D37B
		protected override string ProviderLibraryName
		{
			get
			{
				return "Microsoft .NET Framework 4.6";
			}
		}

		// Token: 0x06005745 RID: 22341 RVA: 0x0012F184 File Offset: 0x0012D384
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			DataTable schema = connection.GetSchema("Tables", DrdaEnvironment.tablesGetSchemaParams);
			schema.Columns["TableType"].ColumnName = "TABLE_TYPE";
			schema.Columns["TableSchema"].ColumnName = "TABLE_SCHEMA";
			schema.Columns["TableName"].ColumnName = "TABLE_NAME";
			return schema;
		}

		// Token: 0x06005746 RID: 22342 RVA: 0x0012F1F0 File Offset: 0x0012D3F0
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			DataTable dataTable = new DataTable
			{
				Locale = CultureInfo.InvariantCulture
			};
			using (DbCommand dbCommand = connection.CreateCommand())
			{
				dbCommand.CommandText = "CALL SYSIBM.SQLCOLUMNS (?, ?, ?, ?, ?)";
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters[1].Value = schema;
				dbCommand.Parameters[2].Value = table;
				dbCommand.Parameters[4].Value = "SUPPORTEDNEWTYPES=XML,DECFLOAT;DATATYPE='JDBC'";
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					dataTable.Load(dbDataReader);
					int count = dataTable.Columns.Count;
					dataTable.Columns.Add("CHARACTER_MAXIMUM_LENGTH", typeof(int));
					int count2 = dataTable.Columns.Count;
					dataTable.Columns.Add("NUMERIC_PRECISION", typeof(int));
					int count3 = dataTable.Columns.Count;
					dataTable.Columns.Add("NUMERIC_SCALE", typeof(int));
					int count4 = dataTable.Columns.Count;
					dataTable.Columns.Add("IS_WRITABLE", typeof(bool));
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						string text;
						if (this.Flavor == DrdaFlavor.Db2)
						{
							text = dataRow[3].ToString();
							if (text == "DB2_GENERATED_ROWID_FOR_LOBS" || text == "DB2_GENERATED_DOCID_FOR_XML")
							{
								dataRow.Delete();
								continue;
							}
						}
						else if (string.Equals(dataRow[5].ToString(), "DATETIME", StringComparison.Ordinal))
						{
							int? num = dataRow[6] as int?;
							if (num != null)
							{
								string text2 = DrdaEnvironment.getInformixDatetimeTypeName.Invoke(null, new object[] { num.Value }) as string;
								if (!string.IsNullOrEmpty(text2))
								{
									dataRow[5] = text2;
								}
							}
						}
						text = dataRow[12].ToString();
						if (!(text == "IDENTITY GENERATED BY DEFAULT"))
						{
							if (!(text == "IDENTITY GENERATED ALWAYS"))
							{
								dataRow[count4] = !dataTable.Columns.Contains("IS_AUTOINCREMENT") || !string.Equals(dataRow["IS_AUTOINCREMENT"].ToString(), "YES", StringComparison.Ordinal);
							}
							else
							{
								dataRow[count4] = false;
							}
						}
						else
						{
							dataRow[count4] = true;
						}
						dataRow[count] = DrdaEnvironment.ConvertLength(dataRow[6], dataRow[5].ToString());
						dataRow[count2] = DrdaEnvironment.ConvertPrecision(dataRow[6], dataRow[5].ToString());
						dataRow[count3] = DrdaEnvironment.ConvertScale(dataRow[8], dataRow[5].ToString());
					}
					string columnName = dataTable.Columns[11].ColumnName;
					string columnName2 = dataTable.Columns[5].ColumnName;
					string columnName3 = dataTable.Columns[9].ColumnName;
					int i = dataTable.Columns.Count - 1;
					while (i >= 0)
					{
						if (i <= 5)
						{
							if (i != 3 && i != 5)
							{
								goto IL_03D1;
							}
						}
						else if (i != 9 && i != 11 && i - 16 > 1)
						{
							goto IL_03D1;
						}
						IL_03F5:
						i--;
						continue;
						IL_03D1:
						if (i != count && i != count2 && i != count3 && i != count4)
						{
							dataTable.Columns.RemoveAt(i);
							goto IL_03F5;
						}
						goto IL_03F5;
					}
					if (columnName != "DESCRIPTION")
					{
						dataTable.Columns[columnName].ColumnName = "DESCRIPTION";
					}
					if (columnName2 != "DATA_TYPE")
					{
						dataTable.Columns[columnName2].ColumnName = "DATA_TYPE";
					}
					if (columnName3 != "NUMERIC_PRECISION_RADIX")
					{
						dataTable.Columns[columnName3].ColumnName = "NUMERIC_PRECISION_RADIX";
					}
					dataTable.AcceptChanges();
				}
			}
			return dataTable;
		}

		// Token: 0x06005747 RID: 22343 RVA: 0x0012F6D8 File Offset: 0x0012D8D8
		public static int ConvertLength(object length, string type)
		{
			int num = 0;
			if (length is short)
			{
				num = (int)((short)length);
			}
			else if (length is int)
			{
				num = (int)length;
			}
			if (!(type == "BIGINT"))
			{
				if (!(type == "INTEGER"))
				{
					if (type == "SMALLINT")
					{
						num = 2;
					}
				}
				else
				{
					num = 4;
				}
			}
			else
			{
				num = 8;
			}
			return num;
		}

		// Token: 0x06005748 RID: 22344 RVA: 0x0012F73C File Offset: 0x0012D93C
		public static int ConvertPrecision(object precision, string type)
		{
			int num = 0;
			if (precision is short)
			{
				num = (int)((short)precision);
			}
			else if (precision is int)
			{
				num = (int)precision;
			}
			if (!(type == "NUMERIC") && !(type == "DECIMAL"))
			{
				if (!(type == "INTEGER"))
				{
					if (!(type == "SMALLINT"))
					{
						if (!(type == "BIGINT"))
						{
							num = 0;
						}
						else
						{
							num = 20;
						}
					}
					else
					{
						num = 5;
					}
				}
				else
				{
					num = 10;
				}
			}
			return num;
		}

		// Token: 0x06005749 RID: 22345 RVA: 0x0012F7C0 File Offset: 0x0012D9C0
		public static int ConvertScale(object scale, string type)
		{
			int num = 0;
			if (scale is short)
			{
				num = (int)((short)scale);
			}
			else if (scale is int)
			{
				num = (int)scale;
			}
			if (!(type == "NUMERIC") && !(type == "DECIMAL"))
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x0600574A RID: 22346 RVA: 0x0012F80C File Offset: 0x0012DA0C
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			DataTable schema2 = connection.GetSchema("PrimaryKeys", new string[] { null, schema, table });
			string text = null;
			int num = schema2.Columns.IndexOf("Keyname");
			int num2 = schema2.Columns.IndexOf("ColumnName");
			HashSet<string> primaryKeySet = new HashSet<string>();
			foreach (object obj in schema2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				primaryKeySet.Add(dataRow[num2] as string);
				if (text == null)
				{
					string text2 = dataRow[num] as string;
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
					}
				}
			}
			DataTable schema3 = connection.GetSchema("Indexes", new string[] { null, schema, table });
			int num3 = schema3.Columns.IndexOf("IndexName");
			int num4 = schema3.Columns.IndexOf("Unique");
			num2 = schema3.Columns.IndexOf("ColumnName");
			schema3.Columns[num3].ColumnName = "INDEX_NAME";
			schema3.Columns[num2].ColumnName = "COLUMN_NAME";
			schema3.Columns["Ordinal"].ColumnName = "ORDINAL_POSITION";
			schema3.Columns[num4].ColumnName = "UNIQUE";
			DataColumn dataColumn = schema3.Columns.Add("PRIMARY_KEY", typeof(bool));
			bool flag = primaryKeySet.Count > 0;
			bool flag2 = false;
			if (flag)
			{
				Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
				foreach (object obj2 in schema3.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					string text3 = dataRow2[num3] as string;
					if (text != null && string.Equals(text3, text))
					{
						flag2 = true;
						break;
					}
					if (dataRow2[num4] != null && dataRow2[num4] != DBNull.Value && (bool)dataRow2[num4] && !string.IsNullOrEmpty(text3))
					{
						string text4 = dataRow2[num2] as string;
						HashSet<string> hashSet = null;
						if (!dictionary.TryGetValue(text3, out hashSet))
						{
							hashSet = new HashSet<string>();
							dictionary.Add(text3, hashSet);
						}
						hashSet.Add(text4);
					}
				}
				if (!flag2)
				{
					KeyValuePair<string, HashSet<string>> keyValuePair = dictionary.FirstOrDefault((KeyValuePair<string, HashSet<string>> indexColumnsSet) => indexColumnsSet.Value.SetEquals(primaryKeySet));
					if (keyValuePair.Key == null)
					{
						flag = false;
					}
					else
					{
						text = keyValuePair.Key;
					}
				}
			}
			for (int i = schema3.Rows.Count - 1; i >= 0; i--)
			{
				DataRow dataRow3 = schema3.Rows[i];
				string text5 = dataRow3[num3] as string;
				if (string.IsNullOrEmpty(text5))
				{
					dataRow3.Delete();
				}
				else
				{
					dataRow3[dataColumn] = flag && string.Equals(text5, text);
				}
			}
			schema3.AcceptChanges();
			return schema3;
		}

		// Token: 0x0600574B RID: 22347 RVA: 0x0012FB68 File Offset: 0x0012DD68
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			DataTable schema2 = connection.GetSchema("ForeignKeys", new string[] { null, schema, table });
			schema2.Columns["ForeignKeyName"].ColumnName = "FK_NAME";
			schema2.Columns["KeySequence"].ColumnName = "ORDINAL";
			schema2.Columns["FKTableSchema"].ColumnName = "FK_TABLE_SCHEMA";
			schema2.Columns["FKTableName"].ColumnName = "FK_TABLE_NAME";
			schema2.Columns["PKColumnName"].ColumnName = "PK_COLUMN_NAME";
			schema2.Columns["FKColumnName"].ColumnName = "FK_COLUMN_NAME";
			return schema2;
		}

		// Token: 0x0600574C RID: 22348 RVA: 0x0012FC2C File Offset: 0x0012DE2C
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			DataTable schema2 = connection.GetSchema("ForeignKeys", new string[] { null, null, null, null, schema, table });
			schema2.Columns["ForeignKeyName"].ColumnName = "FK_NAME";
			schema2.Columns["KeySequence"].ColumnName = "ORDINAL";
			schema2.Columns["PKTableSchema"].ColumnName = "PK_TABLE_SCHEMA";
			schema2.Columns["PKTableName"].ColumnName = "PK_TABLE_NAME";
			schema2.Columns["PKColumnName"].ColumnName = "PK_COLUMN_NAME";
			schema2.Columns["FKColumnName"].ColumnName = "FK_COLUMN_NAME";
			return schema2;
		}

		// Token: 0x0600574D RID: 22349 RVA: 0x0012FCF0 File Offset: 0x0012DEF0
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			long num = 0L;
			DataTable schema2 = connection.GetSchema("Indexes", new string[] { null, schema, table });
			int num2 = schema2.Columns.IndexOf("Pages");
			foreach (object obj in schema2.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!dataRow.IsNull(num2))
				{
					num += (long)((int)dataRow[num2]);
				}
			}
			DataTable dataTable = new DataTable
			{
				Locale = CultureInfo.InvariantCulture
			};
			dataTable.Columns.Add("TOTAL_BYTES", typeof(long));
			dataTable.Rows.Add(new object[] { num });
			return dataTable;
		}

		// Token: 0x0600574E RID: 22350 RVA: 0x0012FDD8 File Offset: 0x0012DFD8
		public override DataTable LoadProcedures(DbConnection connection, string schemaFilter, string procedureFilter)
		{
			DataTable dataTable = new DataTable
			{
				Locale = CultureInfo.InvariantCulture
			};
			using (DbCommand dbCommand = connection.CreateCommand())
			{
				dbCommand.CommandText = "CALL SYSIBM.SQLFUNCTIONS(?, ?, ?, ?)";
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				dbCommand.Parameters.Add(dbCommand.CreateParameter());
				using (DbDataReader dbDataReader = dbCommand.ExecuteReader())
				{
					dataTable.Load(dbDataReader);
					dataTable.Columns["FUNCTION_SCHEM"].ColumnName = "ROUTINE_SCHEMA";
					dataTable.Columns["FUNCTION_NAME"].ColumnName = "ROUTINE_NAME";
					DataColumn dataColumn = dataTable.Columns.Add("ROUTINE_TYPE", typeof(string));
					foreach (object obj in dataTable.Rows)
					{
						((DataRow)obj)[dataColumn] = "FUNCTION";
					}
					dataTable.AcceptChanges();
				}
			}
			return dataTable;
		}

		// Token: 0x0600574F RID: 22351 RVA: 0x0012FF5C File Offset: 0x0012E15C
		public override DataTable LoadSchemas(DbConnection connection)
		{
			DataTable schema = connection.GetSchema("Schemas");
			schema.Columns[0].ColumnName = "SCHEMA_NAME";
			this.TrimSchemaTable(schema);
			return schema;
		}

		// Token: 0x06005750 RID: 22352 RVA: 0x0012FF93 File Offset: 0x0012E193
		protected override DbEnvironment.DbServerMetadata LoadServerMetadataFromStream(Stream s)
		{
			DrdaEnvironment.DrdaServerMetadata drdaServerMetadata = new DrdaEnvironment.DrdaServerMetadata();
			drdaServerMetadata.Deserialize(s);
			if (drdaServerMetadata.ForceUseDb2ConnectGateway)
			{
				this.ForceUseDb2ConnectGateway = true;
			}
			return drdaServerMetadata;
		}

		// Token: 0x06005751 RID: 22353 RVA: 0x0012FFB0 File Offset: 0x0012E1B0
		protected override DbEnvironment.DbServerMetadata LoadServerMetadata()
		{
			DbEnvironment.DbServerMetadata dbServerMetadata;
			try
			{
				dbServerMetadata = this.ConvertDbExceptions<DrdaEnvironment.DrdaServerMetadata>(delegate
				{
					DrdaEnvironment.DrdaServerMetadata drdaServerMetadata;
					using (DbConnection dbConnection = base.CreateConnection())
					{
						dbConnection.Open(this);
						DbConnection unwrappedConnection = DbEnvironment.GetUnwrappedConnection(dbConnection);
						object value = DrdaEnvironment.connectionServerClassInfo.GetValue(unwrappedConnection, EmptyArray<object>.Instance);
						string text = ((value != null) ? value.ToString() : null) + " " + unwrappedConnection.ServerVersion;
						drdaServerMetadata = new DrdaEnvironment.DrdaServerMetadata
						{
							Version = text,
							ForceUseDb2ConnectGateway = this.ForceUseDb2ConnectGateway
						};
					}
					return drdaServerMetadata;
				});
			}
			catch (RebuildConnectionException)
			{
				if (this.UseDb2ConnectGateway != null || this.ForceUseDb2ConnectGateway)
				{
					throw;
				}
				this.ForceUseDb2ConnectGateway = true;
				dbServerMetadata = this.LoadServerMetadata();
			}
			return dbServerMetadata;
		}

		// Token: 0x06005752 RID: 22354 RVA: 0x00130010 File Offset: 0x0012E210
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			DbProviderFactory dbProviderFactory = this.CreateDbProviderFactory();
			return new DrdaEnvironment.DrdaConnectionStringBuilder(base.Host, this.Resource, dbProviderFactory.CreateConnectionStringBuilder(), this.DefaultPort, this.BinaryCodePage, this.PackageCollection, this.UseDb2ConnectGateway ?? this.ForceUseDb2ConnectGateway);
		}

		// Token: 0x06005753 RID: 22355 RVA: 0x0013006C File Offset: 0x0012E26C
		protected void TrimSchemaTable(DataTable table)
		{
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = dataRow[0] as string;
				if (text != null)
				{
					dataRow[0] = text.Trim();
				}
			}
			table.AcceptChanges();
		}

		// Token: 0x06005754 RID: 22356 RVA: 0x001300E4 File Offset: 0x0012E2E4
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return base.CreateUpdatableCatalogTableValue(tableValue, schema);
		}

		// Token: 0x06005755 RID: 22357 RVA: 0x00130102 File Offset: 0x0012E302
		public override void AbortCommand(DbCommand command, DbDataReader reader)
		{
			if (!reader.IsClosed)
			{
				reader.Close();
			}
			reader.Dispose();
		}

		// Token: 0x04003141 RID: 12609
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=528259";

		// Token: 0x04003142 RID: 12610
		private const string ClientLibraryName = "Microsoft .NET Framework 4.6";

		// Token: 0x04003143 RID: 12611
		private const string DrdaProviderName = "Microsoft.HostIntegration.DrdaClient";

		// Token: 0x04003144 RID: 12612
		private const string DrdaExceptionName = "Microsoft.HostIntegration.DrdaClient.DrdaException";

		// Token: 0x04003145 RID: 12613
		private const string DrdaFactoryName = "Microsoft.HostIntegration.DrdaClient.DrdaFactory";

		// Token: 0x04003146 RID: 12614
		private const string DrdaConnectionName = "Microsoft.HostIntegration.DrdaClient.DrdaConnection";

		// Token: 0x04003147 RID: 12615
		private const string HisConnectorsAssemblyName = "Microsoft.HostIntegration.Connectors";

		// Token: 0x04003148 RID: 12616
		private const string InformixDataTypeHelperName = "Microsoft.HostIntegration.Drda.Requester.InformixDataTypeHelper";

		// Token: 0x04003149 RID: 12617
		private const string ColumnsSchemaIsAutoincrementName = "IS_AUTOINCREMENT";

		// Token: 0x0400314A RID: 12618
		private static readonly DbProviderFactory drdaFactory;

		// Token: 0x0400314B RID: 12619
		private static readonly SystemException exceptionWhenLoadingAssembly;

		// Token: 0x0400314C RID: 12620
		private static readonly PropertyInfo connectionServerClassInfo;

		// Token: 0x0400314D RID: 12621
		private static readonly MethodInfo getInformixDatetimeTypeName;

		// Token: 0x0400314E RID: 12622
		private static readonly string[] tablesGetSchemaParams = new string[] { null, null, null, "ALIAS,NICKNAME,SYNONYM,TABLE,VIEW" };

		// Token: 0x0400314F RID: 12623
		private static readonly IDictionary<string, TableType> drdaTableTypes;

		// Token: 0x04003151 RID: 12625
		private bool forceUseDb2ConnectGateway;

		// Token: 0x02000C9F RID: 3231
		protected class DrdaServerMetadata : DbEnvironment.DbServerMetadata
		{
			// Token: 0x17001A4B RID: 6731
			// (get) Token: 0x06005757 RID: 22359 RVA: 0x001301A0 File Offset: 0x0012E3A0
			// (set) Token: 0x06005758 RID: 22360 RVA: 0x001301A8 File Offset: 0x0012E3A8
			public bool ForceUseDb2ConnectGateway { get; set; }

			// Token: 0x06005759 RID: 22361 RVA: 0x001301B1 File Offset: 0x0012E3B1
			protected override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(base.Version);
				writer.WriteBool(this.ForceUseDb2ConnectGateway);
			}

			// Token: 0x0600575A RID: 22362 RVA: 0x001301CB File Offset: 0x0012E3CB
			protected override void Deserialize(BinaryReader reader)
			{
				base.Version = reader.ReadNullableString();
				this.ForceUseDb2ConnectGateway = reader.ReadBool();
			}
		}

		// Token: 0x02000CA0 RID: 3232
		private sealed class DrdaConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x0600575C RID: 22364 RVA: 0x001301E5 File Offset: 0x0012E3E5
			public DrdaConnectionStringBuilder(IEngineHost host, IResource resource, DbConnectionStringBuilder builder, int defaultPort, int binaryCodepage, string packageCollection, bool useDb2ConnectGateway)
				: base(host, resource, builder)
			{
				this.binaryCodepage = binaryCodepage;
				this.defaultPort = defaultPort;
				this.packageCollection = packageCollection;
				this.useDb2ConnectGateway = useDb2ConnectGateway;
			}

			// Token: 0x17001A4C RID: 6732
			// (get) Token: 0x0600575D RID: 22365 RVA: 0x00047C83 File Offset: 0x00045E83
			protected override string UserNameKey
			{
				get
				{
					return "User ID";
				}
			}

			// Token: 0x17001A4D RID: 6733
			// (get) Token: 0x0600575E RID: 22366 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17001A4E RID: 6734
			// (get) Token: 0x0600575F RID: 22367 RVA: 0x00130210 File Offset: 0x0012E410
			protected override string ServerKey
			{
				get
				{
					return "Network Address";
				}
			}

			// Token: 0x17001A4F RID: 6735
			// (get) Token: 0x06005760 RID: 22368 RVA: 0x00130217 File Offset: 0x0012E417
			protected override string PortKey
			{
				get
				{
					return "Network Port";
				}
			}

			// Token: 0x17001A50 RID: 6736
			// (get) Token: 0x06005761 RID: 22369 RVA: 0x0005C658 File Offset: 0x0005A858
			protected override string DatabaseKey
			{
				get
				{
					return "Initial Catalog";
				}
			}

			// Token: 0x17001A51 RID: 6737
			// (get) Token: 0x06005762 RID: 22370 RVA: 0x00047C9F File Offset: 0x00045E9F
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated Security";
				}
			}

			// Token: 0x17001A52 RID: 6738
			// (get) Token: 0x06005763 RID: 22371 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string EncryptKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001A53 RID: 6739
			// (get) Token: 0x06005764 RID: 22372 RVA: 0x000885A6 File Offset: 0x000867A6
			protected override object AuthenticationTypeValue
			{
				get
				{
					return "SSPI";
				}
			}

			// Token: 0x17001A54 RID: 6740
			// (get) Token: 0x06005765 RID: 22373 RVA: 0x0005C66D File Offset: 0x0005A86D
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connect Timeout";
				}
			}

			// Token: 0x06005766 RID: 22374 RVA: 0x0013021E File Offset: 0x0012E41E
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					throw DataSourceException.NewEncryptedConnectionError(base.Host, base.Resource, null, null, null);
				}
				return true;
			}

			// Token: 0x06005767 RID: 22375 RVA: 0x0013023E File Offset: 0x0012E43E
			protected override bool ApplyWindowsCredential(WindowsCredential credential)
			{
				base.ApplyWindowsCredential(credential);
				this.builder["Principle Name"] = this.builder[this.ServerKey];
				return true;
			}

			// Token: 0x06005768 RID: 22376 RVA: 0x0013026C File Offset: 0x0012E46C
			protected override void AddOptions()
			{
				this.builder[this.PortKey] = this.defaultPort.ToString(CultureInfo.InvariantCulture);
				this.builder["Pooling"] = bool.TrueString;
				if (this.binaryCodepage != 0 && this.binaryCodepage != 65535)
				{
					this.builder["Binary Codepage"] = this.binaryCodepage.ToString(CultureInfo.InvariantCulture);
				}
				if (!string.IsNullOrEmpty(this.packageCollection))
				{
					this.builder["Package Collection"] = this.packageCollection;
				}
				if (this.useDb2ConnectGateway)
				{
					this.builder["Extended Properties"] = string.Format(CultureInfo.InvariantCulture, "{0}={1}", "Gateway", this.useDb2ConnectGateway);
				}
			}

			// Token: 0x04003153 RID: 12627
			private readonly int binaryCodepage;

			// Token: 0x04003154 RID: 12628
			private readonly int defaultPort;

			// Token: 0x04003155 RID: 12629
			private readonly string packageCollection;

			// Token: 0x04003156 RID: 12630
			private readonly bool useDb2ConnectGateway;

			// Token: 0x04003157 RID: 12631
			private const string DefaultPrincipleKey = "Principle Name";

			// Token: 0x04003158 RID: 12632
			private const string ConnectionPoolingKey = "Pooling";

			// Token: 0x04003159 RID: 12633
			private const string BinaryCodePageKey = "Binary Codepage";

			// Token: 0x0400315A RID: 12634
			private const string PackageCollectionKey = "Package Collection";

			// Token: 0x0400315B RID: 12635
			private const string ExtendedPropertiesKey = "Extended Properties";

			// Token: 0x0400315C RID: 12636
			private const string GatewaySubKey = "Gateway";
		}

		// Token: 0x02000CA1 RID: 3233
		protected static class DrdaErrorCodes
		{
			// Token: 0x0400315D RID: 12637
			public const int SQL0357N = -357;

			// Token: 0x0400315E RID: 12638
			public const int SQL0551N = -551;

			// Token: 0x0400315F RID: 12639
			public const int SQL0567N = -567;

			// Token: 0x04003160 RID: 12640
			public const int SQL0922N = -922;

			// Token: 0x04003161 RID: 12641
			public const int ESSO_FAILURE = -606;

			// Token: 0x04003162 RID: 12642
			public const int AUTH_NOT_SUPPORT = -1004;

			// Token: 0x04003163 RID: 12643
			public const int KERBEROS_INITIALIZE_ERROR = -1006;

			// Token: 0x04003164 RID: 12644
			public const int KERBEROS_VALIDATE_ERROR = -1007;

			// Token: 0x04003165 RID: 12645
			public const int UNEXPECTED_SECCHKCD = -1030;

			// Token: 0x04003166 RID: 12646
			public const int AUTHENTICATION_FAILURE = -1036;

			// Token: 0x04003167 RID: 12647
			public const int CONNECTION_FAILURE = -1037;

			// Token: 0x04003168 RID: 12648
			public const int TLS_FAILURE = -1038;

			// Token: 0x04003169 RID: 12649
			public const int CONNECTION_TIMEOUT = -7049;
		}
	}
}
