using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.Serialization;
using Microsoft.Internal;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.OleDb
{
	// Token: 0x02000579 RID: 1401
	internal class OleDbEnvironment : DbEnvironment, IOleDbDataSource
	{
		// Token: 0x06002C97 RID: 11415 RVA: 0x0008785E File Offset: 0x00085A5E
		public OleDbEnvironment(IEngineHost host, IResource resource, Value connectionProperties, Value options, OptionRecordDefinition optionRecord)
			: this(host, resource, connectionProperties, null, options, optionRecord)
		{
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x00087870 File Offset: 0x00085A70
		private OleDbEnvironment(IEngineHost host, IResource resource, Value connectionProperties, string catalogName, Value options, OptionRecordDefinition optionRecord)
			: base(host, resource, "OLE DB", null, catalogName, options, null, null)
		{
			this.connectionProperties = connectionProperties;
			this.validOptions = optionRecord;
			this.connectionString = OleDbEnvironment.ConnectionString.GetValidatedString(connectionProperties, "OLE DB", host);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x000878B7 File Offset: 0x00085AB7
		public static OleDbEnvironment CreateForCatalog(IEngineHost host, IResource resource, Value connectionProperties, string catalogName, Value options, OptionRecordDefinition optionRecord, OleDbDataSourceInfo info = null)
		{
			return new OleDbEnvironment(host, resource, connectionProperties, catalogName, options, optionRecord)
			{
				info = info
			};
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000878CE File Offset: 0x00085ACE
		protected override string ProviderName
		{
			get
			{
				return "System.Data.OleDb";
			}
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x000878D5 File Offset: 0x00085AD5
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			return OleDbFactory.Instance;
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x000878DC File Offset: 0x00085ADC
		string IOleDbDataSource.Provider
		{
			get
			{
				if (this.provider == null)
				{
					OleDbEnvironment.GenericOleDbClient genericOleDbClient = new OleDbEnvironment.GenericOleDbClient(this);
					this.provider = genericOleDbClient.ProgID;
				}
				return this.provider;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06002C9D RID: 11421 RVA: 0x0008790A File Offset: 0x00085B0A
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return this.validOptions;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06002C9E RID: 11422 RVA: 0x00087912 File Offset: 0x00085B12
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return OleDbEnvironment.searchableTypes;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06002C9F RID: 11423 RVA: 0x00087919 File Offset: 0x00085B19
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return OleDbEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06002CA0 RID: 11424 RVA: 0x00087920 File Offset: 0x00085B20
		protected bool SqlCompatibleWindowsAuth
		{
			get
			{
				return base.UserOptions.GetBool("SqlCompatibleWindowsAuth", true);
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06002CA1 RID: 11425 RVA: 0x00087933 File Offset: 0x00085B33
		public OleDbDataSourceInfo OleDbDataSourceInfo
		{
			get
			{
				if (this.info == null)
				{
					this.TestConnection();
					this.info = OleDbDataSourceInfo.Load(this);
				}
				return this.info;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06002CA2 RID: 11426 RVA: 0x00087955 File Offset: 0x00085B55
		public override IDataSourceCapabilities DataSourceCapabilities
		{
			get
			{
				return this.OleDbDataSourceInfo;
			}
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0008795D File Offset: 0x00085B5D
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new OleDbEnvironment.OleDbConnectionStringBuilder(base.Host, base.DataSourceNameString, OleDbEnvironment.ConnectionString, this.connectionString, this.Resource, this.SqlCompatibleWindowsAuth);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x00087987 File Offset: 0x00085B87
		protected override bool TryGetDataTypeValue(DataColumnCollection columns, DataRow schemaRow, out TypeValue clrDataType, out bool isSearchable)
		{
			if (DbEnvironment.GetStringSchemaColumn(schemaRow, "DATA_TYPE") == DBTYPE.IDISPATCH.ToString("d"))
			{
				clrDataType = null;
				isSearchable = false;
				return false;
			}
			return base.TryGetDataTypeValue(columns, schemaRow, out clrDataType, out isSearchable);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000879C0 File Offset: 0x00085BC0
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			if (exception.ErrorCode == -2147217843)
			{
				return ResourceExceptionKind.InvalidCredentials;
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000091AE File Offset: 0x000073AE
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000091AE File Offset: 0x000073AE
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000091AE File Offset: 0x000073AE
		protected override SqlSettings LoadSqlSettings()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000879D2 File Offset: 0x00085BD2
		public override DataTable LoadSchemas(DbConnection connection)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Schemata, new object[] { base.Database });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "SCHEMA_NAME", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "CATALOG_NAME", base.Database));
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00087A0C File Offset: 0x00085C0C
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Tables, new object[] { base.Database });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "TABLE_SCHEMA", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "TABLE_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "TABLE_TYPE", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "TABLE_CATALOG", base.Database));
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x00087A6C File Offset: 0x00085C6C
		public override DataTable LoadProcedures(DbConnection connection, string schemaFilter, string procedureFilter)
		{
			DataTable dataTable = OleDbEnvironment.FilterTable(this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Procedures, new object[] { base.Database }), (DataRow row) => OleDbEnvironment.Matches(row, "PROCEDURE_CATALOG", base.Database));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PROCEDURE_SCHEMA", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PROCEDURE_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PROCEDURE_TYPE", null);
			dataTable.Columns["PROCEDURE_SCHEMA"].ColumnName = "ROUTINE_SCHEMA";
			dataTable.Columns["PROCEDURE_NAME"].ColumnName = "ROUTINE_NAME";
			dataTable.Columns["PROCEDURE_TYPE"].ColumnName = "ROUTINE_TYPE";
			return dataTable;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x00087B18 File Offset: 0x00085D18
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Columns, new object[] { base.Database, schema, table });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "COLUMN_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL_POSITION", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "DATA_TYPE", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "IS_NULLABLE", typeof(bool));
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "TABLE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "TABLE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "TABLE_NAME", table));
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x00087BC0 File Offset: 0x00085DC0
		public override DataTable LoadProcedureColumns(DbConnection connection, string schema, string procedure)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Procedure_Columns, new object[] { base.Database, schema, procedure });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "COLUMN_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL_POSITION", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "DATA_TYPE", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "IS_NULLABLE", typeof(bool));
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "PROCEDURE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "PROCEDURE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "PROCEDURE_NAME", procedure));
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x00087C68 File Offset: 0x00085E68
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Indexes, new object[] { base.Database, schema, null, null, table });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "INDEX_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "COLUMN_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL_POSITION", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PRIMARY_KEY", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "TABLE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "TABLE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "TABLE_NAME", table));
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x00087D08 File Offset: 0x00085F08
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Foreign_Keys, new object[] { base.Database, schema, table, base.Database });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_TABLE_SCHEMA", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_TABLE_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PK_COLUMN_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_COLUMN_NAME", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "PK_TABLE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "PK_TABLE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "PK_TABLE_NAME", table) && OleDbEnvironment.Matches(row, "FK_TABLE_CATALOG", this.Database));
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x00087DC8 File Offset: 0x00085FC8
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Foreign_Keys, new object[] { base.Database, null, null, base.Database, schema, table });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PK_TABLE_SCHEMA", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PK_TABLE_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PK_COLUMN_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "FK_COLUMN_NAME", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "PK_TABLE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "FK_TABLE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "FK_TABLE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "FK_TABLE_NAME", table));
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x00087E88 File Offset: 0x00086088
		public override DataTable LoadProcedureParameters(DbConnection connection, string schema, string procedure)
		{
			DataTable dataTable = this.LoadOleDbSchemaTableSafe(connection, OleDbSchemaGuid.Procedure_Parameters, new object[] { base.Database, schema, procedure });
			OleDbEnvironment.AddColumnIfMissing(dataTable, "PARAMETER_NAME", null);
			OleDbEnvironment.AddColumnIfMissing(dataTable, "ORDINAL_POSITION", typeof(long));
			OleDbEnvironment.AddColumnIfMissing(dataTable, "DATA_TYPE", null);
			return OleDbEnvironment.FilterTable(dataTable, (DataRow row) => OleDbEnvironment.Matches(row, "PROCEDURE_CATALOG", this.Database) && OleDbEnvironment.Matches(row, "PROCEDURE_SCHEMA", schema) && OleDbEnvironment.Matches(row, "PROCEDURE_NAME", procedure));
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x00087F1B File Offset: 0x0008611B
		private DataTable LoadOleDbSchemaTable(DbConnection connection, Guid schema, params object[] restrictions)
		{
			DataTable dataTable;
			if ((dataTable = ((OleDbConnection)DbEnvironment.GetUnwrappedConnection(connection)).GetOleDbSchemaTable(schema, restrictions)) == null)
			{
				(dataTable = new DataTable()).Locale = CultureInfo.InvariantCulture;
			}
			return dataTable;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x00087F44 File Offset: 0x00086144
		private DataTable LoadOleDbSchemaTableSafe(DbConnection connection, Guid schema, params object[] restrictions)
		{
			return this.GetSchemaTableSafe((Guid g, object[] r) => this.LoadOleDbSchemaTable(connection, g, r), schema, restrictions);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00087F7C File Offset: 0x0008617C
		public DataTable GetSchemaTable(Guid schema, params object[] restrictions)
		{
			return base.GetSchemaTable((DbConnection connection) => this.LoadOleDbSchemaTable(connection, schema, restrictions), true, schema.ToString() + OleDbEnvironment.RestrictionCacheKeyVersion, OleDbEnvironment.GetRestrictionCacheKeyParts(restrictions));
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00087FE0 File Offset: 0x000861E0
		public DataTable GetSchemaTableSafe(Func<Guid, object[], DataTable> getSchemaTable, Guid schema, params object[] restrictions)
		{
			if (!this.OleDbDataSourceInfo.SupportsSchema(schema, Array.Empty<int>()))
			{
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
			}
			object[] array = new object[restrictions.Length];
			for (int i = 0; i < restrictions.Length; i++)
			{
				array[i] = (this.OleDbDataSourceInfo.SupportsSchema(schema, new int[] { i }) ? restrictions[i] : null);
			}
			return getSchemaTable(schema, array);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x00088051 File Offset: 0x00086251
		DataTable IOleDbDataSource.GetSchemas()
		{
			return this.GetSchemaTable(OleDbSchemaGuid.SchemaGuids, Array.Empty<object>());
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x00088063 File Offset: 0x00086263
		public DataTable GetLiteralInfo()
		{
			return this.GetSchemaTable(OleDbSchemaGuid.DbInfoLiterals, Array.Empty<object>());
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x00088075 File Offset: 0x00086275
		public bool TryGetProperty(Guid propertyGroup, DBPROPID propertyId, out object value)
		{
			return new OleDbEnvironment.GenericOleDbClient(this).TryGetDbProperty(propertyGroup, propertyId, out value);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x00088088 File Offset: 0x00086288
		public IPageReader OpenTable(string tableIdentifier)
		{
			return this.ConvertDbExceptions<DbEnvironment.WrappedPageReader>(delegate
			{
				IHostTrace hostTrace = this.Tracer.CreateTrace("OleDbClient/ReadTable", TraceEventType.Information);
				DbEnvironment.WrappedPageReader wrappedPageReader;
				try
				{
					hostTrace.Add("TableName", tableIdentifier, true);
					IPageReader pageReader = new TracingPageReader(new OleDbEnvironment.GenericOleDbClient(this).ReadTable(tableIdentifier), hostTrace, 1).AfterDispose(new Action(hostTrace.Dispose));
					wrappedPageReader = new DbEnvironment.WrappedPageReader(this, pageReader);
				}
				catch (Exception ex)
				{
					hostTrace.Add(ex, true);
					hostTrace.Dispose();
					throw;
				}
				return wrappedPageReader;
			});
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x000880BC File Offset: 0x000862BC
		protected override IDictionary<SchemaItem, Value> LoadCatalog(SchemaItem? itemFilter)
		{
			IDictionary<SchemaItem, Value> dictionary = new SortedDictionary<SchemaItem, Value>(SchemaItem.Comparer);
			foreach (KeyValuePair<HierarchyTableItem, TypeValue> keyValuePair in base.GetTableTypes(null))
			{
				OleDbIdentifier oleDbIdentifier = new OleDbIdentifier(keyValuePair.Key.CatalogName, keyValuePair.Key.SchemaName, keyValuePair.Key.Name);
				SchemaItem schemaItem = new SchemaItem(keyValuePair.Key.SchemaName, keyValuePair.Key.Name, keyValuePair.Key.Kind.AsString);
				DictionaryTracing.AddWithTracing<SchemaItem, Value>(dictionary, schemaItem, base.AddColumnIdentity(schemaItem, new OleDbTableValue(this, oleDbIdentifier, keyValuePair.Value, base.Host)), base.Host, true, true);
			}
			return dictionary;
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x000881A8 File Offset: 0x000863A8
		private static string[] GetRestrictionCacheKeyParts(object[] restrictions)
		{
			return restrictions.Select(delegate(object r)
			{
				if (r is DBNull)
				{
					return "null";
				}
				if (r == null)
				{
					return null;
				}
				return "{" + r.ToString() + "}";
			}).ToArray<string>();
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x000881D4 File Offset: 0x000863D4
		private static DataTable FilterTable(DataTable table, Func<DataRow, bool> condition)
		{
			DataTable dataTable = table.Clone();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (condition(dataRow))
				{
					dataTable.ImportRow(dataRow);
				}
			}
			return dataTable;
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00088240 File Offset: 0x00086440
		private static void AddColumnIfMissing(DataTable table, string columnName, Type dataType = null)
		{
			if (!table.Columns.Contains(columnName))
			{
				table.Columns.Add(columnName, dataType ?? typeof(string));
			}
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0008826C File Offset: 0x0008646C
		private static bool Matches(DataRow row, string columnName, string expected)
		{
			return expected == null || string.Equals(DbEnvironment.GetSchemaColumn<string>(row, columnName), expected, StringComparison.Ordinal);
		}

		// Token: 0x04001361 RID: 4961
		public static readonly ConnectionStringHandler ConnectionString = new OleDbConnectionStringHandler();

		// Token: 0x04001362 RID: 4962
		private static readonly string RestrictionCacheKeyVersion = "1";

		// Token: 0x04001363 RID: 4963
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<DBTYPE, TypeValue>
		{
			{
				DBTYPE.EMPTY,
				TypeValue.Null
			},
			{
				DBTYPE.NULL,
				TypeValue.None
			},
			{
				DBTYPE.I2,
				TypeValue.Int16
			},
			{
				DBTYPE.I4,
				TypeValue.Int32
			},
			{
				DBTYPE.R4,
				TypeValue.Single
			},
			{
				DBTYPE.R8,
				TypeValue.Double
			},
			{
				DBTYPE.CY,
				TypeValue.Currency
			},
			{
				DBTYPE.DATE,
				TypeValue.DateTime
			},
			{
				DBTYPE.BSTR,
				TypeValue.Text
			},
			{
				DBTYPE.ERROR,
				TypeValue.Int32
			},
			{
				DBTYPE.BOOL,
				TypeValue.Logical
			},
			{
				DBTYPE.DECIMAL,
				TypeValue.Decimal
			},
			{
				DBTYPE.I1,
				TypeValue.Int8
			},
			{
				DBTYPE.UI1,
				TypeValue.Byte
			},
			{
				DBTYPE.UI2,
				TypeValue.Int32
			},
			{
				DBTYPE.UI4,
				TypeValue.Int64
			},
			{
				DBTYPE.I8,
				TypeValue.Int64
			},
			{
				DBTYPE.UI8,
				TypeValue.Number
			},
			{
				DBTYPE.FILETIME,
				TypeValue.DateTime
			},
			{
				DBTYPE.GUID,
				TypeValue.Guid
			},
			{
				DBTYPE.BYTES,
				TypeValue.Binary
			},
			{
				DBTYPE.STR,
				TypeValue.Text
			},
			{
				DBTYPE.WSTR,
				TypeValue.Text
			},
			{
				DBTYPE.NUMERIC,
				TypeValue.Decimal
			},
			{
				DBTYPE.DBDATE,
				TypeValue.Date
			},
			{
				DBTYPE.DBTIME,
				TypeValue.Time
			},
			{
				DBTYPE.DBTIMESTAMP,
				TypeValue.DateTime
			},
			{
				DBTYPE.VARNUMERIC,
				TypeValue.Decimal
			},
			{
				DBTYPE.XML,
				TypeValue.Text
			},
			{
				DBTYPE.DBTIME2,
				TypeValue.Time
			},
			{
				DBTYPE.DBTIMESTAMPOFFSET,
				TypeValue.DateTimeZone
			},
			{
				DBTYPE.DBDURATION,
				TypeValue.Duration
			}
		}.ToDictionary((KeyValuePair<DBTYPE, TypeValue> kvp) => kvp.Key.ToString("d"), (KeyValuePair<DBTYPE, TypeValue> kvp) => kvp.Value);

		// Token: 0x04001364 RID: 4964
		private static readonly HashSet<string> searchableTypes = new HashSet<string>();

		// Token: 0x04001365 RID: 4965
		private OleDbDataSourceInfo info;

		// Token: 0x04001366 RID: 4966
		private readonly OptionRecordDefinition validOptions;

		// Token: 0x04001367 RID: 4967
		protected readonly Value connectionProperties;

		// Token: 0x04001368 RID: 4968
		protected string connectionString;

		// Token: 0x04001369 RID: 4969
		private string provider;

		// Token: 0x0200057A RID: 1402
		private class OleDbConnectionStringBuilder : GenericConnectionStringBuilder
		{
			// Token: 0x06002CC3 RID: 11459 RVA: 0x000884D4 File Offset: 0x000866D4
			public OleDbConnectionStringBuilder(IEngineHost host, string dataSourceName, ConnectionStringHandler connectionStringHandler, string sourceConnectionString, IResource resource, bool sqlCompatibleWindowsAuth)
				: base(host, dataSourceName, connectionStringHandler, sourceConnectionString, resource, sqlCompatibleWindowsAuth)
			{
				DbConnectionStringBuilder dbConnectionStringBuilder = new global::System.Data.OleDb.OleDbConnectionStringBuilder(sourceConnectionString);
				this.providerName = ((string)dbConnectionStringBuilder["Provider"]).Trim();
				if ("MSDASQL".Equals(this.providerName, StringComparison.OrdinalIgnoreCase))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.OleDbMsdaSqlNotSupported, null, null);
				}
				HashSet<string> hashSet = new HashSet<string>(new global::System.Data.OleDb.OleDbConnectionStringBuilder
				{
					Provider = this.providerName
				}.Keys.Cast<string>(), StringComparer.OrdinalIgnoreCase);
				if (hashSet.Count > 5)
				{
					this.supportsUsernamePassword = hashSet.Contains(connectionStringHandler.UserNameKey) && hashSet.Contains(connectionStringHandler.PasswordKey);
					this.supportsIntegratedSecurity = hashSet.Contains(connectionStringHandler.IntegratedSecurityKey);
					return;
				}
				this.supportsUsernamePassword = true;
				this.supportsIntegratedSecurity = true;
			}

			// Token: 0x1700108D RID: 4237
			// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000885A6 File Offset: 0x000867A6
			protected override object AuthenticationTypeValue
			{
				get
				{
					return "SSPI";
				}
			}

			// Token: 0x1700108E RID: 4238
			// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x0005C658 File Offset: 0x0005A858
			protected override string DatabaseKey
			{
				get
				{
					return "Initial Catalog";
				}
			}

			// Token: 0x1700108F RID: 4239
			// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x0005C66D File Offset: 0x0005A86D
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connect Timeout";
				}
			}

			// Token: 0x17001090 RID: 4240
			// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000885AD File Offset: 0x000867AD
			protected override string UserNameKey
			{
				get
				{
					if (!this.supportsUsernamePassword)
					{
						throw this.UnsupportedAuthenticationException();
					}
					return base.UserNameKey;
				}
			}

			// Token: 0x17001091 RID: 4241
			// (get) Token: 0x06002CC8 RID: 11464 RVA: 0x000885C4 File Offset: 0x000867C4
			protected override string PasswordKey
			{
				get
				{
					if (!this.supportsUsernamePassword)
					{
						throw this.UnsupportedAuthenticationException();
					}
					return base.PasswordKey;
				}
			}

			// Token: 0x17001092 RID: 4242
			// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x000885DB File Offset: 0x000867DB
			protected override string IntegratedSecurityKey
			{
				get
				{
					if (!this.supportsIntegratedSecurity)
					{
						throw this.UnsupportedAuthenticationException();
					}
					return base.IntegratedSecurityKey;
				}
			}

			// Token: 0x06002CCA RID: 11466 RVA: 0x000885F4 File Offset: 0x000867F4
			private ResourceSecurityException UnsupportedAuthenticationException()
			{
				string text;
				if (this.supportsIntegratedSecurity)
				{
					text = Strings.UsernamePasswordNotSupported(this.providerName);
				}
				else if (this.supportsUsernamePassword)
				{
					text = Strings.WindowsNotSupported(this.providerName);
				}
				else
				{
					text = Strings.OnlyAnonymousSupported(this.providerName);
				}
				return DataSourceException.NewInvalidCredentialsError(base.Host, base.Resource, text, text, null);
			}

			// Token: 0x0400136A RID: 4970
			private readonly string providerName;

			// Token: 0x0400136B RID: 4971
			private readonly bool supportsUsernamePassword;

			// Token: 0x0400136C RID: 4972
			private readonly bool supportsIntegratedSecurity;
		}

		// Token: 0x0200057B RID: 1403
		private class GenericOleDbClient
		{
			// Token: 0x06002CCB RID: 11467 RVA: 0x0008865C File Offset: 0x0008685C
			public GenericOleDbClient(OleDbEnvironment environment)
			{
				this.environment = environment;
				this.hostProgress = ProgressService.GetHostProgress(environment.Host, environment.Resource.Kind, environment.Resource.Path);
				using (this.environment.ConnectionInfo.Impersonate())
				{
					this.dataSource = OleDbEnvironment.GenericOleDbClient.Initializer.GetDataSource(environment.ConnectionInfo.ConnectionString);
					((IDBInitialize)this.dataSource).Initialize();
				}
			}

			// Token: 0x06002CCC RID: 11468 RVA: 0x00088700 File Offset: 0x00086900
			public IPageReader ReadTable(string name)
			{
				IDisposable handle = HostResourcePermissionService.WaitForGovernedHandle(this.environment.Host, this.environment.Resource);
				object session = null;
				IRowset rowset = null;
				IPageReader pageReader;
				try
				{
					using (this.environment.ConnectionInfo.Impersonate())
					{
						IDBCreateSession idbcreateSession = (IDBCreateSession)this.dataSource;
						session = idbcreateSession.CreateSession();
						IOpenRowset openRowset = (IOpenRowset)session;
						using (new ProgressRequest(this.hostProgress))
						{
							DbPropertySet[] array = EmptyArray<DbPropertySet>.Instance;
							if (this.environment.CommandTimeout != null)
							{
								array = new DbPropertySet[]
								{
									new DbPropertySet(DBPROPGROUP.Rowset, new DbProperty[]
									{
										new DbProperty(DBPROPID.COMMANDTIMEOUT, this.environment.CommandTimeout.Value)
									})
								};
							}
							rowset = openRowset.OpenRowset(name, array);
							pageReader = new ProgressPageReader(new RowsetPageReader(rowset, SqlOleDbErrorHandler.Instance, new Func<DBSTATUS, ISerializedException>(OleDbCellErrorHandler.ConvertError), new Func<Exception, ISerializedException>(this.environment.GetPageReaderExceptionProperties)), this.hostProgress).AfterDispose(delegate
							{
								Marshal.FinalReleaseComObject(rowset);
								Marshal.FinalReleaseComObject(session);
								handle.Dispose();
							});
						}
					}
				}
				catch
				{
					if (rowset != null)
					{
						Marshal.FinalReleaseComObject(rowset);
					}
					if (session != null)
					{
						Marshal.FinalReleaseComObject(session);
					}
					handle.Dispose();
					throw;
				}
				return pageReader;
			}

			// Token: 0x06002CCD RID: 11469 RVA: 0x000888E0 File Offset: 0x00086AE0
			public bool TryGetDbProperty(Guid propertyGroup, DBPROPID propertyId, out object value)
			{
				bool flag;
				using (this.environment.ConnectionInfo.Impersonate())
				{
					flag = ((IDBProperties)this.dataSource).TryGetValue(propertyGroup, propertyId, out value);
				}
				return flag;
			}

			// Token: 0x17001093 RID: 4243
			// (get) Token: 0x06002CCE RID: 11470 RVA: 0x00088938 File Offset: 0x00086B38
			public string ProgID
			{
				get
				{
					string text;
					using (this.environment.ConnectionInfo.Impersonate())
					{
						Guid guid;
						((IPersist)this.dataSource).GetClassID(out guid);
						text = Com.ProgIDFromCLSID(guid);
					}
					return text;
				}
			}

			// Token: 0x0400136D RID: 4973
			private static readonly IDataInitialize Initializer = (IDataInitialize)new OleDbEnvironment.GenericOleDbClient.MsdaInitialize();

			// Token: 0x0400136E RID: 4974
			private readonly OleDbEnvironment environment;

			// Token: 0x0400136F RID: 4975
			private readonly object dataSource;

			// Token: 0x04001370 RID: 4976
			private readonly IHostProgress hostProgress;

			// Token: 0x0200057C RID: 1404
			[Guid("2206CDB0-19C1-11D1-89E0-00C04FD7A829")]
			[ComImport]
			private class MsdaInitialize
			{
				// Token: 0x06002CD0 RID: 11472
				[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
				public extern MsdaInitialize();
			}
		}
	}
}
