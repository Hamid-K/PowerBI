using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Excel
{
	// Token: 0x02000C30 RID: 3120
	internal static class ExcelReaderAce
	{
		// Token: 0x060054CB RID: 21707 RVA: 0x00123670 File Offset: 0x00121870
		public static TableValue ReadTables(IEngineHost host, ExcelFile workbook, bool useFirstRowAsHeader)
		{
			AceSourceFile aceSourceFile = new AceSourceFile(host, workbook.ActualContent, ".XLS", false);
			host.QueryService<ILifetimeService>().Register(aceSourceFile);
			host.QueryService<IFeatureLoggingService>().LogFeature("Microsoft ACE OLEDB 12.0");
			OleDbConnectionStringBuilder oleDbConnectionStringBuilder = new OleDbConnectionStringBuilder();
			oleDbConnectionStringBuilder.DataSource = aceSourceFile.Path;
			oleDbConnectionStringBuilder.Provider = AccessDatabaseEngine.ProviderName;
			oleDbConnectionStringBuilder["Extended Properties"] = "Excel 12.0;IMEX=1;READONLY=TRUE;HDR=" + (useFirstRowAsHeader ? "YES" : "NO");
			string text = oleDbConnectionStringBuilder.ToString();
			TableValue tableValue;
			using (new AceMutex(AceMutex.GetMutexName(aceSourceFile.Path), host))
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(host, "Engine/IO/Excel/OleDbDataReader", TraceEventType.Information, null))
				{
					using (OleDbConnection oleDbConnection = ExcelReaderAce.OpenConnection(text, aceSourceFile.Path, aceSourceFile.IsTempFile, hostTrace))
					{
						string[] tableNames = ExcelReaderAce.GetTableNames(oleDbConnection);
						HashSet<string> hashSet = new HashSet<string>();
						List<TableValue> tables = new List<TableValue>(tableNames.Length);
						List<string> uniqueTableNames = new List<string>(tableNames.Length);
						foreach (string text2 in tableNames)
						{
							ExcelReaderAce.AceTableValue aceTableValue = new ExcelReaderAce.AceTableValue(host, aceSourceFile.Path, aceSourceFile.IsTempFile, text, text2, useFirstRowAsHeader);
							tables.Add(aceTableValue);
							uniqueTableNames.Add(ExcelReaderAce.GetUniqueKey(text2, hashSet));
						}
						tableValue = ListValue.New(uniqueTableNames.Count, (int i) => RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
						{
							TextValue.New(uniqueTableNames[i]),
							tables[i]
						})).ToTable(NavigationTableServices.DefaultTypeValue);
					}
				}
			}
			return tableValue;
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x00123834 File Offset: 0x00121A34
		private static string[] GetTableNames(OleDbConnection connection)
		{
			DataRow[] array = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[4]).Select(string.Format(CultureInfo.InvariantCulture, "{0} <> '{1}' and {0} <> '{2}'", "TABLE_TYPE", "ACCESS TABLE", "SYSTEM TABLE"));
			HashSet<string> hashSet = new HashSet<string>();
			List<string> list = new List<string>(array.Length);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = DbEnvironment.GetSchemaColumn<string>(array2[i], "TABLE_NAME").Replace("''", "'");
				if (text != null && hashSet.Add(text))
				{
					list.Add(text);
				}
			}
			return list.ToArray();
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x001238D0 File Offset: 0x00121AD0
		private static Identifier GetUniqueKey(string keyBase, HashSet<string> keySet)
		{
			if (keyBase[0] == '\'' && keyBase[keyBase.Length - 1] == '\'')
			{
				keyBase = keyBase.Substring(1, keyBase.Length - 2);
			}
			if (keyBase[keyBase.Length - 1] == '$')
			{
				keyBase = keyBase.Substring(0, keyBase.Length - 1);
			}
			int num = 2;
			string text = keyBase;
			while (keySet.Contains(text))
			{
				text = keyBase + num.ToString();
				num++;
			}
			keySet.Add(text);
			return text;
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x00123960 File Offset: 0x00121B60
		private static OleDbConnection OpenConnection(string connectionString, string filePath, bool isTempFile, IHostTrace trace)
		{
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			OleDbConnection oleDbConnection2;
			try
			{
				oleDbConnection.Open();
				oleDbConnection2 = oleDbConnection;
			}
			catch (OleDbException ex)
			{
				oleDbConnection.Dispose();
				string fileName = Path.GetFileName(filePath);
				string text;
				if (ex.Errors.OfType<OleDbError>().Any((OleDbError error) => error.NativeError == -327947149))
				{
					text = Strings.CannotImportDataFromAPasswordProtectedWorkbook;
				}
				else
				{
					text = ex.Message.Replace(filePath, fileName);
				}
				throw ValueException.NewDataFormatError(text, TextValue.New(fileName), ex);
			}
			catch (SEHException ex2)
			{
				oleDbConnection.Dispose();
				string fileName2 = Path.GetFileName(filePath);
				AceSourceFile.ThrowProviderBitnessMismatch(ex2, "Excel Workbook", fileName2, isTempFile);
				throw;
			}
			catch (Exception ex3)
			{
				oleDbConnection.Dispose();
				if (Microsoft.Mashup.Common.SafeExceptions.TraceIsSafeException(trace, ex3))
				{
					string fileName3 = Path.GetFileName(filePath);
					AceSourceFile.ThrowIfProviderMissing(ex3, "Excel Workbook", fileName3, isTempFile);
				}
				throw;
			}
			return oleDbConnection2;
		}

		// Token: 0x04002F0B RID: 12043
		private const int CouldNotDecryptFileErrorCode = -327947149;

		// Token: 0x04002F0C RID: 12044
		private const string providerName = "Microsoft ACE OLEDB 12.0";

		// Token: 0x04002F0D RID: 12045
		public const string XLSExtension = ".XLS";

		// Token: 0x04002F0E RID: 12046
		private const string SchemaTablesTableName = "TABLE_NAME";

		// Token: 0x04002F0F RID: 12047
		private const string SchemaTablesTableType = "TABLE_TYPE";

		// Token: 0x04002F10 RID: 12048
		private const string SchemaTablesTableTypeAccessTable = "ACCESS TABLE";

		// Token: 0x04002F11 RID: 12049
		private const string SchemaTablesTableTypeSystemTable = "SYSTEM TABLE";

		// Token: 0x02000C31 RID: 3121
		private class AceTableValue : TableValue
		{
			// Token: 0x060054CF RID: 21711 RVA: 0x00123A60 File Offset: 0x00121C60
			public AceTableValue(IEngineHost host, string filePath, bool isTempFile, string connectionString, string tableName, bool useFirstRowAsHeader)
			{
				this.host = host;
				this.filePath = filePath;
				this.isTempFile = isTempFile;
				this.connectionString = connectionString;
				this.tableName = tableName;
				this.useFirstRowAsHeader = useFirstRowAsHeader;
			}

			// Token: 0x170019D1 RID: 6609
			// (get) Token: 0x060054D0 RID: 21712 RVA: 0x00123A95 File Offset: 0x00121C95
			public override RecordValue MetaValue
			{
				get
				{
					this.EnsureInitialized();
					return this.metaValue;
				}
			}

			// Token: 0x170019D2 RID: 6610
			// (get) Token: 0x060054D1 RID: 21713 RVA: 0x00123AA3 File Offset: 0x00121CA3
			public override TypeValue Type
			{
				get
				{
					this.EnsureInitialized();
					return this.tableType;
				}
			}

			// Token: 0x060054D2 RID: 21714 RVA: 0x00123AB1 File Offset: 0x00121CB1
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				this.EnsureInitialized();
				return this.records.GetEnumerator();
			}

			// Token: 0x060054D3 RID: 21715 RVA: 0x00123ACC File Offset: 0x00121CCC
			private static TableTypeValue LoadTableType(string tableName, DataTable schemaTable, string defaultColumnPrefix)
			{
				List<int> list2;
				List<NamedValue> list = ExcelReaderAce.AceTableValue.LoadColumns(schemaTable, defaultColumnPrefix, out list2);
				return ExcelReaderAce.AceTableValue.BuildTableTypeValue(tableName, list, (list2 != null) ? list2.ToArray() : null);
			}

			// Token: 0x060054D4 RID: 21716 RVA: 0x00123AF8 File Offset: 0x00121CF8
			private static TableTypeValue BuildTableTypeValue(string tableName, List<NamedValue> columnList, int[] primaryKeys)
			{
				RecordTypeValue recordTypeValue = RecordTypeValue.New(RecordValue.New(columnList.ToArray()));
				List<NamedValue> list = new List<NamedValue>();
				list.Add(new NamedValue("Sql.Schema", Value.Null));
				list.Add(new NamedValue("Sql.Table", TextValue.New(tableName)));
				IList<TableKey> list2 = null;
				if (primaryKeys != null)
				{
					list2 = new TableKey[]
					{
						new TableKey(primaryKeys, true)
					};
				}
				TableTypeValue tableTypeValue = TableTypeValue.New(recordTypeValue, list2);
				return BinaryOperator.AddMeta.Invoke(tableTypeValue, RecordValue.New(list.ToArray())).AsType.AsTableType;
			}

			// Token: 0x060054D5 RID: 21717 RVA: 0x00123B88 File Offset: 0x00121D88
			private static List<NamedValue> LoadColumns(DataTable schemaTable, string defaultColumnPrefix, out List<int> primaryKeys)
			{
				primaryKeys = null;
				DataRow[] array = schemaTable.Select("", SchemaTableColumn.ColumnOrdinal);
				List<NamedValue> list = new List<NamedValue>();
				for (int i = 0; i < array.Length; i++)
				{
					DataRow dataRow = array[i];
					string text = ((defaultColumnPrefix != null) ? (defaultColumnPrefix + (i + 1).ToString()) : DbEnvironment.GetSchemaColumn<string>(dataRow, SchemaTableColumn.ColumnName));
					if (DbEnvironment.GetSchemaColumn<bool>(dataRow, SchemaTableColumn.IsKey))
					{
						if (primaryKeys == null)
						{
							primaryKeys = new List<int>();
						}
						primaryKeys.Add(i);
					}
					bool schemaColumn = DbEnvironment.GetSchemaColumn<bool>(dataRow, SchemaTableColumn.AllowDBNull);
					TypeValue typeValue = DbTypeServices.CreateTypeValue(ExcelReaderAce.AceTableValue.GetSqlTypeValue(DbEnvironment.GetSchemaColumn<Type>(dataRow, SchemaTableColumn.DataType).FullName), schemaColumn, true);
					list.Add(new NamedValue(text, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
					{
						typeValue,
						LogicalValue.False
					})));
				}
				return list;
			}

			// Token: 0x060054D6 RID: 21718 RVA: 0x00123C64 File Offset: 0x00121E64
			private static TypeValue GetSqlTypeValue(string systemType)
			{
				if (systemType != null)
				{
					switch (systemType.Length)
					{
					case 11:
						if (systemType == "System.Byte")
						{
							return TypeValue.Byte;
						}
						break;
					case 12:
					{
						char c = systemType[10];
						if (c != '1')
						{
							if (c != '3')
							{
								if (c == '6')
								{
									if (systemType == "System.Int64")
									{
										return TypeValue.Int64;
									}
								}
							}
							else if (systemType == "System.Int32")
							{
								return TypeValue.Int32;
							}
						}
						else if (systemType == "System.Int16")
						{
							return TypeValue.Int16;
						}
						break;
					}
					case 13:
					{
						char c = systemType[8];
						if (c != 'i')
						{
							if (c != 'o')
							{
								if (c == 't')
								{
									if (systemType == "System.String")
									{
										return TypeValue.Text;
									}
								}
							}
							else if (systemType == "System.Double")
							{
								return TypeValue.Double;
							}
						}
						else if (systemType == "System.Single")
						{
							return TypeValue.Single;
						}
						break;
					}
					case 14:
					{
						char c = systemType[7];
						if (c != 'B')
						{
							if (c == 'D')
							{
								if (systemType == "System.Decimal")
								{
									return TypeValue.Decimal;
								}
							}
						}
						else if (systemType == "System.Boolean")
						{
							return TypeValue.Logical;
						}
						break;
					}
					case 15:
						if (systemType == "System.DateTime")
						{
							return TypeValue.DateTime;
						}
						break;
					}
				}
				return TypeValue.SerializedText;
			}

			// Token: 0x060054D7 RID: 21719 RVA: 0x00123DF0 File Offset: 0x00121FF0
			private static bool TryGetValue(DbDataReader reader, int ordinal, out object value, out ExceptionValueReference providerGetValueError)
			{
				bool flag;
				try
				{
					value = reader.GetValue(ordinal);
					providerGetValueError = null;
					flag = true;
				}
				catch (InvalidOperationException ex)
				{
					providerGetValueError = new ExceptionValueReference(ValueException.NewDataSourceError<Message2>(ExcelReaderAce.AceTableValue.FormatDataSourceExceptionMessage(ex.Message), Value.Null, ex));
					value = null;
					flag = false;
				}
				catch (ArgumentException ex2)
				{
					providerGetValueError = new ExceptionValueReference(ValueException.NewDataSourceError<Message2>(ExcelReaderAce.AceTableValue.FormatDataSourceExceptionMessage(ex2.Message), Value.Null, ex2));
					value = null;
					flag = false;
				}
				return flag;
			}

			// Token: 0x060054D8 RID: 21720 RVA: 0x00123E74 File Offset: 0x00122074
			private static Message2 FormatDataSourceExceptionMessage(string message)
			{
				return DataSourceException.DataSourceMessage("Microsoft ACE OLEDB 12.0", message);
			}

			// Token: 0x060054D9 RID: 21721 RVA: 0x00123E84 File Offset: 0x00122084
			private void EnsureInitialized()
			{
				if (this.isInitialized)
				{
					return;
				}
				using (new AceMutex(AceMutex.GetMutexName(this.filePath), this.host))
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/Excel/OleDbDataReader", TraceEventType.Information, null))
					{
						using (OleDbConnection oleDbConnection = ExcelReaderAce.OpenConnection(this.connectionString, this.filePath, this.isTempFile, hostTrace))
						{
							using (DbCommand dbCommand = oleDbConnection.CreateCommand())
							{
								dbCommand.CommandText = "select * from " + DbEnvironment.BracketQuoteIdentifier(this.tableName);
								dbCommand.CommandTimeout = 0;
								hostTrace.Add("CommandText", dbCommand.CommandText, true);
								IHostProgress hostProgress = ProgressService.GetHostProgress(this.host, "OleDb", this.tableName);
								DbDataReader dbDataReader;
								using (new ProgressRequest(hostProgress))
								{
									try
									{
										dbDataReader = new ProgressDbDataReader(dbCommand.ExecuteReader(CommandBehavior.KeyInfo).WithTableSchema(), hostProgress);
									}
									catch (OleDbException ex)
									{
										throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex.InnerException);
									}
								}
								using (dbDataReader)
								{
									try
									{
										this.tableType = ExcelReaderAce.AceTableValue.LoadTableType(this.tableName, dbDataReader.GetSchemaTable(), this.useFirstRowAsHeader ? null : "Column");
										RecordTypeValue asRecordType = this.tableType.ItemType.AsRecordType;
										this.records = new List<IValueReference>();
										while (dbDataReader.Read())
										{
											IValueReference[] array = new IValueReference[dbDataReader.FieldCount];
											for (int i = 0; i < array.Length; i++)
											{
												object obj = null;
												ExceptionValueReference exceptionValueReference = null;
												if (ExcelReaderAce.AceTableValue.TryGetValue(dbDataReader, i, out obj, out exceptionValueReference))
												{
													array[i] = ValueMarshaller.MarshalFromClr(obj);
												}
												else
												{
													array[i] = exceptionValueReference;
												}
											}
											this.records.Add(RecordValue.New(asRecordType, array));
										}
										hostTrace.Add("RecordsCount", this.records.Count, false);
										this.metaValue = ValueServices.AddShouldInferTableTypeMeta(base.MetaValue);
										if (!this.useFirstRowAsHeader)
										{
											this.metaValue = ValueServices.AddFirstRowMayContainHeadersMeta(this.metaValue);
										}
										this.isInitialized = true;
									}
									catch (OleDbException ex2)
									{
										throw ValueException.NewDataSourceError(ex2.Message, Value.Null, ex2.InnerException);
									}
								}
							}
						}
					}
				}
			}

			// Token: 0x04002F12 RID: 12050
			private readonly IEngineHost host;

			// Token: 0x04002F13 RID: 12051
			private readonly string filePath;

			// Token: 0x04002F14 RID: 12052
			private readonly bool isTempFile;

			// Token: 0x04002F15 RID: 12053
			private readonly string connectionString;

			// Token: 0x04002F16 RID: 12054
			private readonly string tableName;

			// Token: 0x04002F17 RID: 12055
			private readonly bool useFirstRowAsHeader;

			// Token: 0x04002F18 RID: 12056
			private bool isInitialized;

			// Token: 0x04002F19 RID: 12057
			private TableTypeValue tableType;

			// Token: 0x04002F1A RID: 12058
			private List<IValueReference> records;

			// Token: 0x04002F1B RID: 12059
			private RecordValue metaValue;
		}
	}
}
