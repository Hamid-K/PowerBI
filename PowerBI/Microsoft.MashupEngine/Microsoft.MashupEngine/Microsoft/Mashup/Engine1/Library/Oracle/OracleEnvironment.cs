using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OleDb;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x0200055D RID: 1373
	internal class OracleEnvironment : DbEnvironment
	{
		// Token: 0x06002BD8 RID: 11224 RVA: 0x00085194 File Offset: 0x00083394
		private OracleEnvironment(IEngineHost host, string server, Value options)
			: base(host, DatabaseResource.New("Oracle", server, null), "Oracle", server, null, options, null, null)
		{
			this.batchSchemaLoader = new OracleEnvironment.OracleBatchSchemaLoader(this);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000851CA File Offset: 0x000833CA
		public static OracleEnvironment Create(IEngineHost host, string server, Value options)
		{
			return new OracleEnvironment(host, server, options);
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000851D4 File Offset: 0x000833D4
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			OracleAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000851DE File Offset: 0x000833DE
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			OracleAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000851E8 File Offset: 0x000833E8
		protected override SqlSettings LoadSqlSettings()
		{
			if (this.IsVersionGreaterOrEqual(12, 2))
			{
				return OracleEnvironment.sql122Settings;
			}
			if (this.IsVersionGreaterOrEqual(11, 0))
			{
				return OracleEnvironment.sql11Settings;
			}
			return OracleEnvironment.sqlSettings;
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x00085211 File Offset: 0x00083411
		private void EnsureOdacDriverIsInstalled()
		{
			if (!this.IsOdacDriverInstalled && !OracleEnvironment.disableProviderCheck)
			{
				throw new NotSupportedException(Strings.NoOdacDriverIsFound("https://go.microsoft.com/fwlink/p/?LinkID=272376"));
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00085238 File Offset: 0x00083438
		private bool IsVersionGreaterOrEqual(int majorVersionIn, int minorVersionIn = 0)
		{
			string serverVersion = base.ServerVersion;
			if (serverVersion != null)
			{
				string[] array = serverVersion.Split(new char[] { '.' });
				int num = 0;
				if (array.Length >= 2 && int.TryParse(array[0], out num))
				{
					if (num > majorVersionIn)
					{
						return true;
					}
					if (num == majorVersionIn)
					{
						if (minorVersionIn == 0)
						{
							return true;
						}
						int num2 = 0;
						if (int.TryParse(array[1], out num2))
						{
							return num2 >= minorVersionIn;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06002BDF RID: 11231 RVA: 0x0008529C File Offset: 0x0008349C
		internal bool IsOdacDriverInstalled
		{
			get
			{
				if (OracleEnvironment.isOdacDriverInstalled == null)
				{
					using (IHostTrace hostTrace = base.Tracer.CreateTrace("IsOdacDriverInstalled", TraceEventType.Information))
					{
						OracleEnvironment.isOdacDriverInstalled = new bool?(base.IsProviderInstalled("Oracle.DataAccess.Client"));
						hostTrace.Add("IsOdacDriverInstalled", OracleEnvironment.isOdacDriverInstalled, false);
					}
				}
				return OracleEnvironment.isOdacDriverInstalled.Value;
			}
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x00085318 File Offset: 0x00083518
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return OracleAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x00085324 File Offset: 0x00083524
		protected override ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			OracleAstCreator oracleAstCreator = OracleAstCreator.Create(expression, cursor, this);
			DbQueryPlan dbQueryPlan = oracleAstCreator.Create(expression);
			return new DbValueBuilder(this, dbQueryPlan, ErrorColumnMappingDbDataReader.CreateWrapper(this, oracleAstCreator.GetErrorColumns(dbQueryPlan.Query)));
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x0008535C File Offset: 0x0008355C
		protected override ActionValue CompileStatement(Query targetQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, string statementType)
		{
			DbStatementPlan dbStatementPlan = this.NewAstCreator(expression, cursor).CreateStatementPlan();
			OutputClause outputClause = this.GetOutputClause(dbStatementPlan.Statement);
			OutputParameterCollection outputParameterCollection = null;
			if (outputClause != OutputClause.Null)
			{
				outputParameterCollection = new OracleOutputParameterCollection(targetQuery, outputClause.ColumnList.Select((SelectItem c) => c.Name).ToList<Alias>(), outputClause.OutputParameters, this);
			}
			return DbStatementActionValue.New(this, targetQuery, dbStatementPlan, statementType, outputParameterCollection);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x000853D8 File Offset: 0x000835D8
		private OutputClause GetOutputClause(SqlStatement statement)
		{
			if (statement is SqlInsertStatement)
			{
				return ((SqlInsertStatement)statement).OutputClause;
			}
			if (statement is SqlUpdateStatement)
			{
				return ((SqlUpdateStatement)statement).OutputClause;
			}
			if (statement is SqlDeleteStatement)
			{
				return ((SqlDeleteStatement)statement).OutputClause;
			}
			return OutputClause.Null;
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00085428 File Offset: 0x00083628
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			int num = 0;
			ResourceExceptionKind resourceExceptionKind;
			using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
			{
				if (!OracleExceptionHelper.TryGetErrorCode(exception, out num, hostTrace))
				{
					resourceExceptionKind = ResourceExceptionKind.None;
				}
				else
				{
					hostTrace.Add("Exception", exception, true);
					hostTrace.Add("ExceptionNumber", num, false);
					ResourceExceptionKind resourceExceptionKind2;
					if (num == 1017)
					{
						resourceExceptionKind2 = ResourceExceptionKind.InvalidCredentials;
					}
					else
					{
						resourceExceptionKind2 = ResourceExceptionKind.None;
					}
					hostTrace.Add("ResourceExceptionKind", resourceExceptionKind2, false);
					resourceExceptionKind = resourceExceptionKind2;
				}
			}
			return resourceExceptionKind;
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x000854B8 File Offset: 0x000836B8
		protected override DbConnection WrapConnection(DbConnection baseConnection)
		{
			this.EnsureOdacDriverIsInstalled();
			return base.WrapConnection(baseConnection);
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x000854C7 File Offset: 0x000836C7
		public override DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			return new OracleDbDataReaderWrapper(reader, this);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x000854D0 File Offset: 0x000836D0
		public string GetSessionSeparator()
		{
			IPersistentCache metadataCache = base.Host.GetMetadataCache();
			string text = PersistentCacheKey.ServerCatalog.Qualify(base.ConnectionInfo.CacheKey, "Nls_Numeric_Characters");
			Stream stream;
			string text2;
			if (!metadataCache.TryGetValue(text, out stream))
			{
				using (DbConnection dbConnection = base.CreateConnection())
				{
					dbConnection.Open(this);
					using (DbCommand dbCommand = dbConnection.CreateCommand())
					{
						dbCommand.CommandText = "select value from nls_session_parameters where parameter = 'NLS_NUMERIC_CHARACTERS'";
						text2 = (string)dbCommand.ExecuteScalar();
						try
						{
							stream = metadataCache.BeginAdd();
							new BinaryWriter(stream).WriteNullableString(text2);
							metadataCache.EndAdd(text, stream).Close();
							return text2;
						}
						catch (PersistentCacheException)
						{
							return text2;
						}
					}
				}
			}
			text2 = new BinaryReader(stream).ReadNullableString();
			stream.Close();
			return text2;
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06002BE8 RID: 11240 RVA: 0x000855CC File Offset: 0x000837CC
		protected override string ProviderName
		{
			get
			{
				return "Oracle.DataAccess.Client";
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x000855D3 File Offset: 0x000837D3
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/p/?LinkID=272376";
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000855DA File Offset: 0x000837DA
		protected override string ProviderLibraryName
		{
			get
			{
				return "Basic Instant Client for Microsoft Windows";
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000855E1 File Offset: 0x000837E1
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return OracleModule.OptionRecord;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000855E8 File Offset: 0x000837E8
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return OracleEnvironment.searchableTypes;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x000855EF File Offset: 0x000837EF
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return OracleEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000855F6 File Offset: 0x000837F6
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(OracleEnvironment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x00085608 File Offset: 0x00083808
		public override DataTable LoadSchemas(DbConnection connection)
		{
			return base.LoadData("Schemas", connection, string.Format(CultureInfo.InvariantCulture, "select USERNAME as SCHEMA_NAME \r\nfrom all_users \r\nwhere USERNAME not in {0}\r\norder by SCHEMA_NAME", "('SYS', 'SYSTEM', 'SYSMAN', 'CTXSYS', 'EXFSYS', 'MDSYS', 'OLAPSYS', 'ORDSYS', 'OUTLN', 'WKSYS', 'WMSYS', 'XDB', 'ORDDATA', 'ORDPLUGINS')"));
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0008562C File Offset: 0x0008382C
		protected override DataTable GetTables(SchemaItem? itemFilter)
		{
			DataTable tables = base.GetTables(null);
			this.batchSchemaLoader.SetTables(tables);
			return tables;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00085658 File Offset: 0x00083858
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			DataTable dataTable = base.LoadData("Tables", connection, string.Format(CultureInfo.InvariantCulture, "select OWNER, TABLE_NAME, TABLE_TYPE as TYPE\r\nfrom all_catalog\r\nwhere OWNER not in {0}\r\nand TABLE_TYPE != 'SYNONYM'", "('SYS', 'SYSTEM', 'SYSMAN', 'CTXSYS', 'EXFSYS', 'MDSYS', 'OLAPSYS', 'ORDSYS', 'OUTLN', 'WKSYS', 'WMSYS', 'XDB', 'ORDDATA', 'ORDPLUGINS')"));
			dataTable.Columns["OWNER"].ColumnName = "TABLE_SCHEMA";
			dataTable.Columns["TYPE"].ColumnName = "TABLE_TYPE";
			return dataTable;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000856BC File Offset: 0x000838BC
		public override DataTable LoadProcedures(DbConnection connection, string schemaFilter, string tableFilter)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "select OWNER as ROUTINE_SCHEMA, OBJECT_NAME as ROUTINE_NAME, OBJECT_TYPE as ROUTINE_TYPE, CREATED as CREATED_DATE, LAST_DDL_TIME as MODIFIED_DATE, OBJECT_NAME as DESCRIPTION\r\nfrom all_objects\r\nwhere OBJECT_TYPE in {0}\r\nand OWNER not in {1}", "('PROCEDURE')", "('SYS', 'SYSTEM', 'SYSMAN', 'CTXSYS', 'EXFSYS', 'MDSYS', 'OLAPSYS', 'ORDSYS', 'OUTLN', 'WKSYS', 'WMSYS', 'XDB', 'ORDDATA', 'ORDPLUGINS')");
			return base.LoadData("Procedures", connection, text);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000856F0 File Offset: 0x000838F0
		public override DataTable LoadProcedureParameters(DbConnection connection, string schema, string function)
		{
			return base.LoadData("ProcedureParameters", connection, "select ARGUMENT_NAME as PARAMETER_NAME, POSITION as ORDINAL_POSITION, DATA_TYPE\r\nfrom all_arguments\r\nwhere POSITION > 0 and OWNER = {0} and OBJECT_NAME = {1}\r\norder by ORDINAL_POSITION", new string[] { schema, function });
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00085714 File Offset: 0x00083914
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			string[] array = new string[3];
			array[0] = schema;
			array[1] = table;
			string[] array2 = array;
			DataTable schema2 = connection.GetSchema("Columns", array2);
			schema2.Columns["ID"].ColumnName = "ORDINAL_POSITION";
			schema2.Columns["DATATYPE"].ColumnName = "DATA_TYPE";
			schema2.Columns["NULLABLE"].ColumnName = "IS_NULLABLE";
			this.columnTypeSchemaTable = schema2;
			return schema2;
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00085794 File Offset: 0x00083994
		protected override DataTable GetIndexes(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadIndexes(connection, schema, table), false, "Indexes", new string[] { schema, table });
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000857EC File Offset: 0x000839EC
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return this.batchSchemaLoader.LoadIndexes(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00085808 File Offset: 0x00083A08
		protected override DataTable GetForeignKeysParent(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadForeignKeysParent(connection, schema, table), false, "ForeignKeysParent", new string[] { schema, table });
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00085860 File Offset: 0x00083A60
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return this.batchSchemaLoader.LoadForeignKeysParent(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0008587C File Offset: 0x00083A7C
		protected override DataTable GetForeignKeysChild(string schema, string table)
		{
			return base.GetSchemaTable((DbConnection connection) => this.DbService.LoadForeignKeysChild(connection, schema, table), false, "ForeignKeysChild", new string[] { schema, table });
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000858D4 File Offset: 0x00083AD4
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return this.batchSchemaLoader.LoadForeignKeysChild(connection, new SchemaItem(schema, table, string.Empty));
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000858EE File Offset: 0x00083AEE
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ResourceInformation", connection, "select \r\n  sum(s.BYTES) as TOTAL_BYTES\r\nfrom DBA_INDEXES i right outer join DBA_SEGMENTS s \r\n  on s.SEGMENT_NAME = i.INDEX_NAME and s.OWNER = i.OWNER\r\nwhere \r\n    s.OWNER = {0} \r\n  and\r\n    (s.SEGMENT_NAME = {1} or i.TABLE_NAME = {1})", new string[] { schema, table });
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00085910 File Offset: 0x00083B10
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				throw ValueException.NewExpressionError<Message1>(Strings.Catalog_UnsupportedColumnType(type.ToSource()), null, null);
			case ValueKind.Date:
				return new SqlDataType(type, new ConstantSqlString("date"));
			case ValueKind.DateTime:
				return new SqlDataType(type, new ConstantSqlString("timestamp(7)"));
			case ValueKind.DateTimeZone:
				return new SqlDataType(type, new ConstantSqlString("timestamp(7) with time zone"));
			case ValueKind.Duration:
				return new SqlDataType(type, new ConstantSqlString("interval day to second"));
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return DbEnvironment.Int8Type;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return DbEnvironment.Int8Type;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return DbEnvironment.Int16Type;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return DbEnvironment.Int32Type;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return DbEnvironment.Int64Type;
				}
				if (type.Equals(TypeValue.Single))
				{
					return OracleEnvironment.SingleType;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return DbEnvironment.DecimalType;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return DbEnvironment.CurrencyType;
				}
				return OracleEnvironment.DoubleType;
			case ValueKind.Logical:
				return DbEnvironment.NumericBitType;
			case ValueKind.Text:
				return new SqlDataType(type, new ConstantSqlString("nvarchar2(2000)"));
			case ValueKind.Binary:
				return OracleEnvironment.BlobType;
			default:
				return base.GetSqlScalarType(type);
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00085A70 File Offset: 0x00083C70
		public override Number GetNumeric(IDataReader reader, int index)
		{
			Number number;
			try
			{
				number = new Number(reader.GetDecimal(index));
			}
			catch (OverflowException)
			{
				DbDataReader dbDataReader = reader as DbDataReader;
				if (dbDataReader == null)
				{
					throw;
				}
				object value = dbDataReader.GetValue(index);
				number = ((value is decimal) ? new Number((decimal)value) : new Number((double)value));
			}
			return number;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00085AD8 File Offset: 0x00083CD8
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return base.CreateUpdatableCatalogTableValue(tableValue, schema);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x00085AF6 File Offset: 0x00083CF6
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new OracleEnvironment.OracleConnectionStringBuilder(this);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x00085B00 File Offset: 0x00083D00
		public override DbConnection CreateDbConnection()
		{
			this.EnsureOdacDriverIsInstalled();
			DbConnection dbConnection = base.CreateDbConnection();
			if (this.aadCredential != null)
			{
				this.aadCredential = this.aadCredential.RefreshTokenAsNeeded(base.Host, this.Resource, false);
				object obj = OracleEnvironment.accessTokenCache.FindToken(this.aadCredential.AccessToken);
				if (obj == null)
				{
					if (OracleEnvironment.oracleAccessTokenConstructor == null)
					{
						OracleEnvironment.oracleAccessTokenConstructor = Assembly.GetAssembly(dbConnection.GetType()).GetType("Oracle.DataAccess.Client.OracleAccessToken").GetConstructor(new Type[] { typeof(char[]) });
					}
					obj = OracleEnvironment.oracleAccessTokenConstructor.Invoke(new object[] { this.aadCredential.AccessToken.ToCharArray() });
					OracleEnvironment.accessTokenCache.Cache(this.aadCredential.AccessToken, obj);
				}
				if (obj != null)
				{
					if (OracleEnvironment.accessTokenPropertySetter == null)
					{
						OracleEnvironment.accessTokenPropertySetter = dbConnection.GetType().GetProperty("AccessToken");
					}
					OracleEnvironment.accessTokenPropertySetter.SetValue(dbConnection, obj, null);
				}
			}
			return dbConnection;
		}

		// Token: 0x04001306 RID: 4870
		private const string ClientLibraryName = "Basic Instant Client for Microsoft Windows";

		// Token: 0x04001307 RID: 4871
		public const string DataSourceName = "Oracle";

		// Token: 0x04001308 RID: 4872
		public const string DownloadLink = "https://go.microsoft.com/fwlink/p/?LinkID=272376";

		// Token: 0x04001309 RID: 4873
		public const string OdpNetProviderName = "Oracle.DataAccess.Client";

		// Token: 0x0400130A RID: 4874
		private const string SystemUsers = "('SYS', 'SYSTEM', 'SYSMAN', 'CTXSYS', 'EXFSYS', 'MDSYS', 'OLAPSYS', 'ORDSYS', 'OUTLN', 'WKSYS', 'WMSYS', 'XDB', 'ORDDATA', 'ORDPLUGINS')";

		// Token: 0x0400130B RID: 4875
		private const string RoutineType = "('PROCEDURE')";

		// Token: 0x0400130C RID: 4876
		private static readonly SqlDataType BlobType = new SqlDataType(TypeValue.Binary, new ConstantSqlString("blob"));

		// Token: 0x0400130D RID: 4877
		private static readonly SqlDataType SingleType = new SqlDataType(TypeValue.Single, new ConstantSqlString("binary_float"));

		// Token: 0x0400130E RID: 4878
		private static readonly SqlDataType DoubleType = new SqlDataType(TypeValue.Double, new ConstantSqlString("binary_double"));

		// Token: 0x0400130F RID: 4879
		private static readonly SqlSettings sql122Settings = new SqlSettings
		{
			InvalidIdentifierCharacters = new char[] { '"' },
			MaxIdentifierLength = 128,
			RequiresAsForFromAlias = false,
			MaxVariableStringLength = 4000,
			PagingStrategy = PagingStrategy.AnsiSql2008,
			DateTimePrefix = "TO_TIMESTAMP('",
			DateTimeSuffix = "','YYYY-MM-DD HH24:MI:SS.FF')",
			DatePrefix = "TO_DATE('",
			DateSuffix = "','YYYY-MM-DD HH24:MI:SS')",
			DateTimeOffsetPrefix = "TO_TIMESTAMP_TZ('",
			DateTimeOffsetSuffix = "','YYYY-MM-DD HH24:MI:SS.FF TZH:TZM')",
			IntervalPrefix = "INTERVAL '",
			IntervalSuffix = "' DAY(9) TO SECOND(7)",
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredProcedures = true,
			SupportsOutputClause = true,
			SupportsIntervalConstants = true,
			BinaryPrefix = "'",
			BinarySuffix = "'"
		};

		// Token: 0x04001310 RID: 4880
		private static readonly SqlSettings sql11Settings = new SqlSettings
		{
			InvalidIdentifierCharacters = new char[] { '"' },
			MaxIdentifierLength = 30,
			RequiresAsForFromAlias = false,
			MaxVariableStringLength = 4000,
			PagingStrategy = PagingStrategy.RowCountWithoutOrder,
			DateTimePrefix = "TO_TIMESTAMP('",
			DateTimeSuffix = "','YYYY-MM-DD HH24:MI:SS.FF')",
			DatePrefix = "TO_DATE('",
			DateSuffix = "','YYYY-MM-DD')",
			DateTimeOffsetPrefix = "TO_TIMESTAMP_TZ('",
			DateTimeOffsetSuffix = "','YYYY-MM-DD HH24:MI:SS.FF TZH:TZM')",
			IntervalPrefix = "INTERVAL '",
			IntervalSuffix = "' DAY(9) TO SECOND(7)",
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = true,
			SupportsStoredProcedures = true,
			SupportsOutputClause = true,
			SupportsIntervalConstants = true,
			BinaryPrefix = "'",
			BinarySuffix = "'"
		};

		// Token: 0x04001311 RID: 4881
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = new char[] { '"' },
			MaxIdentifierLength = 30,
			RequiresAsForFromAlias = false,
			MaxVariableStringLength = 4000,
			PagingStrategy = PagingStrategy.RowCountWithoutOrder,
			DateTimePrefix = "TO_TIMESTAMP('",
			DateTimeSuffix = "','YYYY-MM-DD HH24:MI:SS.FF')",
			DatePrefix = "TO_DATE('",
			DateSuffix = "','YYYY-MM-DD HH24:MI:SS')",
			DateTimeOffsetPrefix = "TO_TIMESTAMP_TZ('",
			DateTimeOffsetSuffix = "','YYYY-MM-DD HH24:MI:SS.FF TZH:TZM')",
			IntervalPrefix = "INTERVAL '",
			IntervalSuffix = "' DAY(9) TO SECOND(7)",
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsTableRotationFunctions = false,
			SupportsStoredProcedures = true,
			SupportsOutputClause = true,
			SupportsIntervalConstants = true,
			BinaryPrefix = "'",
			BinarySuffix = "'"
		};

		// Token: 0x04001312 RID: 4882
		internal static bool? isOdacDriverInstalled;

		// Token: 0x04001313 RID: 4883
		internal static bool disableProviderCheck = false;

		// Token: 0x04001314 RID: 4884
		private readonly OracleEnvironment.OracleBatchSchemaLoader batchSchemaLoader;

		// Token: 0x04001315 RID: 4885
		private OAuthCredential aadCredential;

		// Token: 0x04001316 RID: 4886
		private static readonly OracleEnvironment.AccessTokenCache accessTokenCache = new OracleEnvironment.AccessTokenCache();

		// Token: 0x04001317 RID: 4887
		private static ConstructorInfo oracleAccessTokenConstructor = null;

		// Token: 0x04001318 RID: 4888
		private static global::System.Reflection.PropertyInfo accessTokenPropertySetter = null;

		// Token: 0x04001319 RID: 4889
		private DataTable columnTypeSchemaTable;

		// Token: 0x0400131A RID: 4890
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"binary_double", "binary_float", "char", "date", "float", "interval day to second", "interval year to month", "nchar", "number", "nvarchar",
			"nvarchar2", "raw", "timestamp", "timestamp with local time zone", "timestamp with time zone", "varchar", "varchar2"
		};

		// Token: 0x0400131B RID: 4891
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"bfile",
				TypeValue.Binary
			},
			{
				"binary_double",
				TypeValue.Double
			},
			{
				"binary_float",
				TypeValue.Single
			},
			{
				"blob",
				TypeValue.Binary
			},
			{
				"char",
				TypeValue.Text
			},
			{
				"clob",
				TypeValue.Text
			},
			{
				"date",
				TypeValue.DateTime
			},
			{
				"float",
				TypeValue.Decimal
			},
			{
				"interval day to second",
				TypeValue.Duration
			},
			{
				"interval year to month",
				TypeValue.Int64
			},
			{
				"long",
				TypeValue.Text
			},
			{
				"long raw",
				TypeValue.Binary
			},
			{
				"nchar",
				TypeValue.Text
			},
			{
				"nclob",
				TypeValue.Text
			},
			{
				"number",
				TypeValue.Decimal
			},
			{
				"nvarchar",
				TypeValue.Text
			},
			{
				"nvarchar2",
				TypeValue.Text
			},
			{
				"raw",
				TypeValue.Binary
			},
			{
				"timestamp",
				TypeValue.DateTime
			},
			{
				"timestamp with local time zone",
				TypeValue.DateTime
			},
			{
				"timestamp with time zone",
				TypeValue.DateTime
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"varchar2",
				TypeValue.Text
			},
			{
				"xmltype",
				TypeValue.Text
			},
			{
				"rowid",
				TypeValue.Text
			}
		};

		// Token: 0x0400131C RID: 4892
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"blob", "clob", "long", "long raw", "nclob", "nvarchar", "nvarchar2", "raw", "long", "number",
			"varchar", "varchar2"
		};

		// Token: 0x0200055E RID: 1374
		private sealed class OracleBatchSchemaLoader : MultipleQueueBatchSchemaLoader
		{
			// Token: 0x06002C02 RID: 11266 RVA: 0x0008626A File Offset: 0x0008446A
			public OracleBatchSchemaLoader(OracleEnvironment environment)
				: base(environment, true, environment.PrefetchMetadata)
			{
			}

			// Token: 0x06002C03 RID: 11267 RVA: 0x000091AE File Offset: 0x000073AE
			protected override string GetColumnsQuery(SchemaItem[] items)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06002C04 RID: 11268 RVA: 0x000091AE File Offset: 0x000073AE
			protected override string GetForeignKeyQuery(SchemaItem[] items)
			{
				throw new NotImplementedException();
			}

			// Token: 0x17001051 RID: 4177
			// (get) Token: 0x06002C05 RID: 11269 RVA: 0x00002105 File Offset: 0x00000305
			protected override int SmallDatabase
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17001052 RID: 4178
			// (get) Token: 0x06002C06 RID: 11270 RVA: 0x0008627A File Offset: 0x0008447A
			protected override int MaxItemsInList
			{
				get
				{
					return 999;
				}
			}

			// Token: 0x06002C07 RID: 11271 RVA: 0x00086284 File Offset: 0x00084484
			protected override DataTable GetForeignKeysTable(DbConnection connection, SchemaItem[] items)
			{
				string text = "select\r\n                        FKCON.CONSTRAINT_NAME as FK_NAME,\r\n                        fkcol.POSITION as ORDINAL,\r\n                        FKCON.OWNER AS TABLE_SCHEMA,\r\n                        FKCON.TABLE_NAME AS TABLE_NAME,\r\n                        fkcol.COLUMN_NAME as TABLE_COLUMN,\r\n                        FKCON.R_OWNER as TARGETED_SCHEMA,\r\n                        FKCON.R_CONSTRAINT_NAME as TARGETED_INDEX\r\n                    from ALL_CONSTRAINTS FKCON, all_cons_columns fkcol\r\n                    where\r\n                        FKCON.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME and FKCON.owner = fkcol.owner and FKCON.CONSTRAINT_TYPE = 'R' \r\n                    and " + base.GenerateClause("FKCON.OWNER", "FKCON.TABLE_NAME", items) + global::System.Environment.NewLine + "order by TABLE_SCHEMA, FK_NAME, ORDINAL";
				DataTable dataTable = base.Environment.LoadData("ForeignKeysOutgoingSource", connection, text);
				SchemaItem[] array = OracleEnvironment.OracleBatchSchemaLoader.IndexesAsSchemaItems(dataTable, 5, 6);
				string text2 = "select \r\n                        OWNER as PK_SCHEMA,\r\n                        CONSTRAINT_NAME as PK_NAME,\r\n                        TABLE_NAME as PK_TABLE,\r\n                        COLUMN_NAME as PK_COLUMN,\r\n                        POSITION as ORDINAL\r\n                    from all_cons_columns\r\n                    where " + base.GenerateClause("OWNER", "CONSTRAINT_NAME", array) + global::System.Environment.NewLine + "order by OWNER, CONSTRAINT_NAME, POSITION";
				DataTable dataTable2 = base.Environment.LoadData("ForeignKeysOutgoingTarget", connection, text2);
				string text3 = "select \r\n                        C.owner as TABLE_SCHEMA,\r\n                        C.table_name as TABLE_NAME,\r\n                        C.constraint_name as UNIQUE_INDEX_NAME, \r\n                        CC.column_name as COLUMN_NAME, \r\n                        CC.position as ORDINAL_POSITION,\r\n                        case when C.constraint_type = 'P' then 'Y' else 'N' end as PRIMARY_KEY,\r\n                        C.constraint_name as INDEX_NAME\r\n                    from \r\n                          (select distinct constraint_name, table_name, owner, constraint_type \r\n                          from all_constraints \r\n                          where " + base.GenerateClause("owner", "table_name", items) + " \r\n                          and constraint_type in ('P', 'U')) C\r\n                       inner join \r\n                          all_cons_columns CC \r\n                       on C.constraint_name = CC.constraint_name and C.owner = CC.owner\r\n                    order by INDEX_NAME, ORDINAL_POSITION";
				DataTable dataTable3 = base.Environment.LoadData("ForeignKeysIncomingSource", connection, text3);
				SchemaItem[] array2 = OracleEnvironment.OracleBatchSchemaLoader.IndexesAsSchemaItems(dataTable3, 0, 2);
				string text4 = "select\r\n                        FKCON.CONSTRAINT_NAME as FK_NAME,\r\n                        fkcol.POSITION as ORDINAL,\r\n                        FKCON.OWNER AS TABLE_SCHEMA,\r\n                        FKCON.TABLE_NAME AS TABLE_NAME,\r\n                        fkcol.COLUMN_NAME as TABLE_COLUMN,\r\n                        FKCON.R_OWNER as TARGETED_SCHEMA,\r\n                        FKCON.R_CONSTRAINT_NAME as TARGETED_INDEX\r\n                    from ALL_CONSTRAINTS FKCON, all_cons_columns fkcol\r\n                    where\r\n                        FKCON.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME and FKCON.owner = fkcol.owner and FKCON.CONSTRAINT_TYPE = 'R' \r\n                    and " + base.GenerateClause("FKCON.R_OWNER", "FKCON.R_CONSTRAINT_NAME", array2) + global::System.Environment.NewLine + "order by FK_NAME, ORDINAL";
				DataTable dataTable4 = base.Environment.LoadData("ForeignKeysIncomingTarget", connection, text4);
				Dictionary<string, Dictionary<string, object>> dictionary = OracleEnvironment.OracleBatchSchemaLoader.IndexByConstraintColumns(dataTable3, 0, 2, 4);
				Dictionary<string, Dictionary<string, object>> dictionary2 = OracleEnvironment.OracleBatchSchemaLoader.IndexByConstraintColumns(dataTable2, 0, 1, 4);
				DataTable dataTable5 = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				dataTable5.Columns.Add("FK_NAME", typeof(string));
				dataTable5.Columns.Add("ORDINAL", typeof(long));
				dataTable5.Columns.Add("TABLE_SCHEMA_1", typeof(string));
				dataTable5.Columns.Add("TABLE_NAME_1", typeof(string));
				dataTable5.Columns.Add("TABLE_COLUMN_1", typeof(string));
				dataTable5.Columns.Add("TABLE_SCHEMA_2", typeof(string));
				dataTable5.Columns.Add("TABLE_NAME_2", typeof(string));
				dataTable5.Columns.Add("TABLE_COLUMN_2", typeof(string));
				HashSet<object[]> hashSet = new HashSet<object[]>(new OracleEnvironment.OracleBatchSchemaLoader.ObjectArrayComparer());
				using (IEnumerator enumerator = dataTable.Rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object[] array3;
						if (OracleEnvironment.OracleBatchSchemaLoader.TryGetJoinedRow((DataRow)enumerator.Current, dictionary2, "PK_TABLE", "PK_COLUMN", out array3))
						{
							hashSet.Add(array3);
						}
					}
				}
				using (IEnumerator enumerator = dataTable4.Rows.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object[] array4;
						if (OracleEnvironment.OracleBatchSchemaLoader.TryGetJoinedRow((DataRow)enumerator.Current, dictionary, "TABLE_NAME", "COLUMN_NAME", out array4))
						{
							hashSet.Add(array4);
						}
					}
				}
				foreach (object[] array5 in hashSet.OrderBy((object[] row) => row[0].ToString() + row[1].ToString()))
				{
					dataTable5.Rows.Add(new object[]
					{
						array5[0],
						array5[1],
						array5[2],
						array5[3],
						array5[4],
						array5[5],
						array5[6],
						array5[7]
					});
				}
				return dataTable5;
			}

			// Token: 0x06002C08 RID: 11272 RVA: 0x00086610 File Offset: 0x00084810
			protected override string GetIndexesQuery(SchemaItem[] items)
			{
				return "select \r\n    C.owner as TABLE_SCHEMA,\r\n    C.table_name as TABLE_NAME,\r\n    C.owner || '_' || C.constraint_name as INDEX_NAME, \r\n    CC.column_name as COLUMN_NAME, \r\n    CC.position as ORDINAL_POSITION,\r\n    case when C.constraint_type = 'P' then 'Y' else 'N' end as PRIMARY_KEY\r\nfrom \r\n      (select distinct constraint_name, table_name, owner, constraint_type \r\n      from all_constraints \r\n      where " + base.GenerateClause("owner", "table_name", items) + " \r\n      and constraint_type in ('P', 'U')) C\r\n   inner join \r\n      all_cons_columns CC \r\n   on C.constraint_name = CC.constraint_name and C.owner = CC.owner\r\norder by INDEX_NAME, ORDINAL_POSITION";
			}

			// Token: 0x06002C09 RID: 11273 RVA: 0x00086634 File Offset: 0x00084834
			private static bool TryGetJoinedRow(DataRow row, Dictionary<string, Dictionary<string, object>> dictionaryToJoin, string otherTableColumnName, string otherColumnColumnName, out object[] result)
			{
				object obj = row["TABLE_SCHEMA"];
				object obj2 = row["TARGETED_SCHEMA"];
				object obj3 = row["TARGETED_INDEX"];
				object obj4 = row["ORDINAL"];
				Dictionary<string, object> dictionary;
				if (dictionaryToJoin.TryGetValue(OracleEnvironment.OracleBatchSchemaLoader.GetKey(obj2, obj3, obj4), out dictionary))
				{
					object[] array = new object[8];
					int num = 0;
					string text = ((obj != null) ? obj.ToString() : null);
					string text2 = "_";
					object obj5 = row["FK_NAME"];
					array[num] = text + text2 + ((obj5 != null) ? obj5.ToString() : null);
					array[1] = obj4;
					array[2] = obj;
					array[3] = row["TABLE_NAME"];
					array[4] = row["TABLE_COLUMN"];
					array[5] = obj2;
					array[6] = dictionary[otherTableColumnName];
					array[7] = dictionary[otherColumnColumnName];
					result = array;
					return true;
				}
				result = null;
				return false;
			}

			// Token: 0x06002C0A RID: 11274 RVA: 0x00086704 File Offset: 0x00084904
			private static SchemaItem[] IndexesAsSchemaItems(DataTable table, int schemaColumnIndex, int itemColumnIndex)
			{
				HashSet<SchemaItem> hashSet = new HashSet<SchemaItem>();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					hashSet.Add(new SchemaItem(dataRow[schemaColumnIndex] as string, dataRow[itemColumnIndex] as string, string.Empty));
				}
				return hashSet.ToArray<SchemaItem>();
			}

			// Token: 0x06002C0B RID: 11275 RVA: 0x0008678C File Offset: 0x0008498C
			private static Dictionary<string, Dictionary<string, object>> IndexByConstraintColumns(DataTable table, int schemaIndex, int itemIndex, int ordinalIndex)
			{
				Dictionary<string, Dictionary<string, object>> dictionary = new Dictionary<string, Dictionary<string, object>>();
				foreach (object obj in table.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string key = OracleEnvironment.OracleBatchSchemaLoader.GetKey(dataRow[schemaIndex], dataRow[itemIndex], dataRow[ordinalIndex]);
					Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
					for (int i = 0; i < table.Columns.Count; i++)
					{
						dictionary2.Add(table.Columns[i].ColumnName, dataRow[i]);
					}
					dictionary.Add(key, dictionary2);
				}
				return dictionary;
			}

			// Token: 0x06002C0C RID: 11276 RVA: 0x00086850 File Offset: 0x00084A50
			private static string GetKey(object schema, object item, object ordinal)
			{
				return string.Concat(new string[]
				{
					(schema != null) ? schema.ToString() : null,
					", ",
					(item != null) ? item.ToString() : null,
					", ",
					(ordinal != null) ? ordinal.ToString() : null
				});
			}

			// Token: 0x0200055F RID: 1375
			private class ObjectArrayComparer : IEqualityComparer<object[]>
			{
				// Token: 0x06002C0D RID: 11277 RVA: 0x000868A8 File Offset: 0x00084AA8
				public bool Equals(object[] x, object[] y)
				{
					if (x.Length != y.Length)
					{
						return false;
					}
					for (int i = 0; i < x.Length; i++)
					{
						if (!x[i].Equals(y[i]))
						{
							return false;
						}
					}
					return true;
				}

				// Token: 0x06002C0E RID: 11278 RVA: 0x000868E0 File Offset: 0x00084AE0
				public int GetHashCode(object[] obj)
				{
					if (obj == null)
					{
						return 0;
					}
					HashBuilder hashBuilder = default(HashBuilder);
					for (int i = 0; i < obj.Length; i++)
					{
						hashBuilder.Add(obj[i].GetHashCode());
					}
					return hashBuilder.ToHash();
				}
			}
		}

		// Token: 0x02000561 RID: 1377
		private sealed class OracleConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06002C13 RID: 11283 RVA: 0x00086941 File Offset: 0x00084B41
			public OracleConnectionStringBuilder(OracleEnvironment environment)
				: base(environment.Host, environment.Resource)
			{
				this.environment = environment;
			}

			// Token: 0x17001053 RID: 4179
			// (get) Token: 0x06002C14 RID: 11284 RVA: 0x0005C651 File Offset: 0x0005A851
			protected override string UserNameKey
			{
				get
				{
					return "User Id";
				}
			}

			// Token: 0x17001054 RID: 4180
			// (get) Token: 0x06002C15 RID: 11285 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17001055 RID: 4181
			// (get) Token: 0x06002C16 RID: 11286 RVA: 0x00047C91 File Offset: 0x00045E91
			protected override string ServerKey
			{
				get
				{
					return "Data Source";
				}
			}

			// Token: 0x17001056 RID: 4182
			// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string PortKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001057 RID: 4183
			// (get) Token: 0x06002C18 RID: 11288 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string DatabaseKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001058 RID: 4184
			// (get) Token: 0x06002C19 RID: 11289 RVA: 0x00047C9F File Offset: 0x00045E9F
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated Security";
				}
			}

			// Token: 0x17001059 RID: 4185
			// (get) Token: 0x06002C1A RID: 11290 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string EncryptKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700105A RID: 4186
			// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000020FA File Offset: 0x000002FA
			protected override object AuthenticationTypeValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700105B RID: 4187
			// (get) Token: 0x06002C1C RID: 11292 RVA: 0x00047CB5 File Offset: 0x00045EB5
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connection Timeout";
				}
			}

			// Token: 0x06002C1D RID: 11293 RVA: 0x0008695C File Offset: 0x00084B5C
			protected override bool ApplyWindowsCredential(WindowsCredential credential)
			{
				this.builder[this.UserNameKey] = "/";
				return true;
			}

			// Token: 0x06002C1E RID: 11294 RVA: 0x00086975 File Offset: 0x00084B75
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					throw new NotSupportedException();
				}
				return true;
			}

			// Token: 0x06002C1F RID: 11295 RVA: 0x00086986 File Offset: 0x00084B86
			protected override bool Apply(OAuthCredential credential)
			{
				base.SetOAuthIdentity(credential);
				this.environment.aadCredential = credential;
				this.builder[this.UserNameKey] = "/";
				return true;
			}

			// Token: 0x0400131F RID: 4895
			private readonly OracleEnvironment environment;
		}

		// Token: 0x02000562 RID: 1378
		internal sealed class AccessTokenCache
		{
			// Token: 0x06002C20 RID: 11296 RVA: 0x000869B2 File Offset: 0x00084BB2
			public AccessTokenCache()
				: this(TimeSpan.FromMinutes(30.0))
			{
			}

			// Token: 0x06002C21 RID: 11297 RVA: 0x000869C8 File Offset: 0x00084BC8
			public AccessTokenCache(TimeSpan maxTokenAge)
			{
				this.cache = new LruCache<string, KeyValuePair<object, DateTime>>(new Func<bool>(this.TrimOldTokens), null);
				this.maxTokenAge = maxTokenAge;
			}

			// Token: 0x1700105C RID: 4188
			// (get) Token: 0x06002C22 RID: 11298 RVA: 0x000869EF File Offset: 0x00084BEF
			public int Count
			{
				get
				{
					return this.cache.Count;
				}
			}

			// Token: 0x06002C23 RID: 11299 RVA: 0x000869FC File Offset: 0x00084BFC
			public object FindToken(string accessToken)
			{
				KeyValuePair<object, DateTime> keyValuePair;
				if (this.cache.TryGetValue(accessToken, out keyValuePair))
				{
					return keyValuePair.Key;
				}
				return null;
			}

			// Token: 0x06002C24 RID: 11300 RVA: 0x00086A24 File Offset: 0x00084C24
			public bool Cache(string accessToken, object oracleAccessToken)
			{
				KeyValuePair<object, DateTime> keyValuePair;
				if (!this.cache.TryGetValue(accessToken, out keyValuePair))
				{
					this.cache.Add(accessToken, new KeyValuePair<object, DateTime>(oracleAccessToken, DateTime.UtcNow));
					return true;
				}
				return false;
			}

			// Token: 0x06002C25 RID: 11301 RVA: 0x00086A5C File Offset: 0x00084C5C
			private bool TrimOldTokens()
			{
				KeyValuePair<string, KeyValuePair<object, DateTime>>? oldest = this.cache.Oldest;
				return oldest != null && oldest.Value.Value.Value + this.maxTokenAge < DateTime.UtcNow;
			}

			// Token: 0x04001320 RID: 4896
			private readonly LruCache<string, KeyValuePair<object, DateTime>> cache;

			// Token: 0x04001321 RID: 4897
			private readonly TimeSpan maxTokenAge;
		}
	}
}
