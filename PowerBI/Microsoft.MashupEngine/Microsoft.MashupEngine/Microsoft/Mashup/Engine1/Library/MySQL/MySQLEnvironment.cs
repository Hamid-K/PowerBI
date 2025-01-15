using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x02000912 RID: 2322
	internal class MySQLEnvironment : DbEnvironment
	{
		// Token: 0x06004226 RID: 16934 RVA: 0x000DEFA0 File Offset: 0x000DD1A0
		private MySQLEnvironment(IEngineHost host, string server, string database, Value options)
			: base(host, DatabaseResource.New("MySql", server, database), "MySQL", server, database, options, null, null)
		{
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x000DEFCB File Offset: 0x000DD1CB
		public static MySQLEnvironment Create(IEngineHost host, string server, string database, Value options)
		{
			return new MySQLEnvironment(host, server, database, options);
		}

		// Token: 0x17001513 RID: 5395
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x000DEFD6 File Offset: 0x000DD1D6
		public static string ClientSoftwareNotFoundExceptionMessage
		{
			get
			{
				return DbEnvironment.GetClientSoftwareNotFoundExceptionMessage("MySQL Connector/Net", "https://go.microsoft.com/fwlink/?LinkId=278885");
			}
		}

		// Token: 0x17001514 RID: 5396
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x000DEFEC File Offset: 0x000DD1EC
		public static string ProviderMissingErrorMessage
		{
			get
			{
				return Strings.DatabaseProviderMissingExceptionMessage("MySql.Data.MySqlClient");
			}
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x000DEFFD File Offset: 0x000DD1FD
		public static string ProviderConfigurationErrorMessage(string message)
		{
			return Strings.DatabaseProviderConfigurationErrorExceptionMessage("MySql.Data.MySqlClient", message);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x000DF00F File Offset: 0x000DD20F
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return MySQLAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x000DF019 File Offset: 0x000DD219
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			MySQLAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x000DF024 File Offset: 0x000DD224
		public static string TryGetVersionFromProviderFactory(DbProviderFactory dbFactory)
		{
			string text = string.Empty;
			MatchCollection matchCollection = Regex.Matches(dbFactory.GetType().AssemblyQualifiedName, "Version=([0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+),");
			if (matchCollection.Count > 0)
			{
				text = matchCollection[0].Groups[1].Value;
			}
			return text;
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x000DF070 File Offset: 0x000DD270
		public bool TryGetFactoryFromPrivateProviderManager(out DbProviderFactory dbFactory)
		{
			dbFactory = null;
			bool flag;
			try
			{
				flag = DbEnvironment.privateProviderManager.Value.TryCreateFactory(base.Host, this.ProviderName, out dbFactory, false);
			}
			catch (ValueException)
			{
				dbFactory = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x000DF0BC File Offset: 0x000DD2BC
		public bool TryGetFactoryFromGAC(out DbProviderFactory dbFactory)
		{
			dbFactory = null;
			try
			{
				dbFactory = DbProviderFactories.GetFactory(this.ProviderName);
			}
			catch (ArgumentException)
			{
				dbFactory = null;
				return false;
			}
			return true;
		}

		// Token: 0x17001515 RID: 5397
		// (get) Token: 0x06004230 RID: 16944 RVA: 0x000DF0F8 File Offset: 0x000DD2F8
		private DbProviderFactory MySQLFactory
		{
			get
			{
				if (this.mySQLFactory == null)
				{
					string text = string.Empty;
					bool flag = false;
					if (this.TryGetFactoryFromPrivateProviderManager(out this.mySQLFactory))
					{
						text = MySQLEnvironment.TryGetVersionFromProviderFactory(this.mySQLFactory);
					}
					else
					{
						if (!this.TryGetFactoryFromGAC(out this.mySQLFactory))
						{
							throw DataSourceException.NewMissingClientLibraryError<Message0>(base.Host, new Message0(MySQLEnvironment.ProviderMissingErrorMessage), this.Resource, null, null, null);
						}
						flag = true;
						text = MySQLEnvironment.TryGetVersionFromProviderFactory(this.mySQLFactory);
					}
					using (IHostTrace hostTrace = base.Tracer.CreateTrace("MySQLFactory", TraceEventType.Information))
					{
						string text2 = "Using " + this.ProviderName;
						if (text != string.Empty)
						{
							text2 = text2 + " " + text;
						}
						text2 += (flag ? " GAC installation" : " built-in installation");
						hostTrace.Add("MySQLProviderInstallation", text2, false);
					}
				}
				return this.mySQLFactory;
			}
		}

		// Token: 0x06004231 RID: 16945 RVA: 0x000DF1F4 File Offset: 0x000DD3F4
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			DbProviderFactory dbProviderFactory;
			try
			{
				dbProviderFactory = this.MySQLFactory;
			}
			catch (ConfigurationErrorsException ex)
			{
				throw DataSourceException.NewDataSourceError<Message2>(base.Host, Strings.ConfigurationErrorsExceptionMessage(ex.Message, DbEnvironment.MachineConfigPath), this.Resource, null, ex);
			}
			return dbProviderFactory;
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x000DF240 File Offset: 0x000DD440
		protected override SqlSettings LoadSqlSettings()
		{
			bool flag = this.IsHighVersion();
			return new SqlSettings
			{
				MaxIdentifierLength = 128,
				InvalidIdentifierCharacters = EmptyArray<char>.Instance,
				QuoteIdentifier = SqlSettings.StandardQuote("`"),
				QuoteAnsiStringLiteral = new Func<string, string>(MySQLEnvironment.QuoteUnicodeLiteral),
				QuoteNationalStringLiteral = new Func<string, string>(MySQLEnvironment.QuoteUnicodeLiteral),
				RequiresAsForFromAlias = false,
				IntervalPrefix = "INTERVAL '",
				IntervalSuffix = "' DAY_MICROSECOND",
				DatePrefix = "'",
				DateSuffix = "'",
				PagingStrategy = PagingStrategy.Limit,
				SupportsStoredFunctions = flag,
				SupportsForeignKeys = flag,
				SupportsCaseExpression = true,
				SupportsFullOuterJoinExpression = true,
				SupportsIntervalConstants = true,
				DeleteCommand = SqlLanguageStrings.DeleteFromSqlString
			};
		}

		// Token: 0x17001516 RID: 5398
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x000DF30F File Offset: 0x000DD50F
		protected override string ProviderName
		{
			get
			{
				return "MySql.Data.MySqlClient";
			}
		}

		// Token: 0x17001517 RID: 5399
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000DF316 File Offset: 0x000DD516
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/?LinkId=278885";
			}
		}

		// Token: 0x17001518 RID: 5400
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x000DF31D File Offset: 0x000DD51D
		protected override string ProviderLibraryName
		{
			get
			{
				return "MySQL Connector/Net";
			}
		}

		// Token: 0x17001519 RID: 5401
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x000DF324 File Offset: 0x000DD524
		protected override int MaxTimeoutSeconds
		{
			get
			{
				return 2147483;
			}
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x000DF32B File Offset: 0x000DD52B
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return MySQLModule.OptionRecord;
			}
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000DF334 File Offset: 0x000DD534
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			int num = 0;
			if (exception.GetType().FullName == "MySql.Data.MySqlClient.MySqlException")
			{
				PropertyInfo property = exception.GetType().GetProperty("Number");
				if (property != null)
				{
					num = (int)property.GetGetMethod().Invoke(exception, new object[0]);
				}
				ResourceExceptionKind resourceExceptionKind2;
				using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
				{
					hostTrace.Add("Exception", exception, true);
					hostTrace.Add("ExceptionNumber", num, false);
					ResourceExceptionKind resourceExceptionKind;
					if (num != 0)
					{
						if (num - 1044 > 1)
						{
							resourceExceptionKind = ResourceExceptionKind.None;
						}
						else
						{
							resourceExceptionKind = ResourceExceptionKind.InvalidCredentials;
						}
					}
					else
					{
						DbException ex = exception.InnerException as DbException;
						if (ex != null)
						{
							return this.GetResourceExceptionKind(ex);
						}
						resourceExceptionKind = ResourceExceptionKind.SecureConnectionFailed;
					}
					hostTrace.Add("ResourceExceptionKind", resourceExceptionKind, false);
					resourceExceptionKind2 = resourceExceptionKind;
				}
				return resourceExceptionKind2;
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000DF42C File Offset: 0x000DD62C
		private bool TreatTinyAsBoolean
		{
			get
			{
				return base.UserOptions.GetBool("TreatTinyAsBoolean", true);
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000DF43F File Offset: 0x000DD63F
		private bool OldGuids
		{
			get
			{
				return base.UserOptions.GetBool("OldGuids", false);
			}
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x000DF454 File Offset: 0x000DD654
		private string Encoding
		{
			get
			{
				string text;
				base.UserOptions.TryGetString("Encoding", out text);
				return text;
			}
		}

		// Token: 0x1700151E RID: 5406
		// (get) Token: 0x0600423C RID: 16956 RVA: 0x000DF475 File Offset: 0x000DD675
		private bool ReturnSingleDatabase
		{
			get
			{
				return base.UserOptions.GetBool("ReturnSingleDatabase", false);
			}
		}

		// Token: 0x0600423D RID: 16957 RVA: 0x000DF488 File Offset: 0x000DD688
		protected override bool TryGetDataTypeValue(DataColumnCollection columns, DataRow schemaRow, out TypeValue clrDataType, out bool isSearchable)
		{
			if (this.TreatTinyAsBoolean)
			{
				string text;
				string text2;
				if (schemaRow.Table.Columns.Contains("COLUMN_TYPE"))
				{
					text = DbEnvironment.GetStringSchemaColumn(schemaRow, "COLUMN_TYPE");
					text2 = "tinyint(1)";
				}
				else
				{
					text = (schemaRow.Table.Columns.Contains("DATA_TYPE") ? DbEnvironment.GetStringSchemaColumn(schemaRow, "DATA_TYPE") : null);
					text2 = "tinyint";
				}
				if (string.Equals(text, text2, StringComparison.OrdinalIgnoreCase))
				{
					clrDataType = TypeValue.Logical;
					isSearchable = true;
					return true;
				}
			}
			return base.TryGetDataTypeValue(columns, schemaRow, out clrDataType, out isSearchable);
		}

		// Token: 0x0600423E RID: 16958 RVA: 0x000DF518 File Offset: 0x000DD718
		public override DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			reader = new MySQLWrappingDbDataReader(reader);
			reader = base.WrapDbDataReader(reader);
			reader = UnsignedPromotingDbDataReader.New(reader);
			Func<object, object>[] array = new Func<object, object>[reader.FieldCount];
			Type[] array2 = new Type[reader.FieldCount];
			bool flag = false;
			for (int i = 0; i < reader.FieldCount; i++)
			{
				Type fieldType = reader.GetFieldType(i);
				array2[i] = fieldType;
				if (Type.GetTypeCode(fieldType) == TypeCode.Boolean)
				{
					flag = true;
					array[i] = new Func<object, object>(this.ConvertBoolean);
				}
				else if (fieldType == typeof(Guid))
				{
					flag = true;
				}
			}
			if (flag)
			{
				reader = new MySQLReader(reader, array2, array);
			}
			return reader;
		}

		// Token: 0x0600423F RID: 16959 RVA: 0x000DF5B7 File Offset: 0x000DD7B7
		private object ConvertBoolean(object obj)
		{
			if (obj is bool)
			{
				return obj;
			}
			return Convert.ToBoolean(obj, CultureInfo.InvariantCulture);
		}

		// Token: 0x06004240 RID: 16960 RVA: 0x000DF5D4 File Offset: 0x000DD7D4
		private bool IsHighVersion()
		{
			string[] array = base.ServerVersion.Split(new char[] { '.' });
			int num;
			int num2;
			return array.Length >= 2 && int.TryParse(array[0], out num) && int.TryParse(array[1], out num2) && (num > 5 || (num == 5 && num2 >= 5));
		}

		// Token: 0x06004241 RID: 16961 RVA: 0x000DF62B File Offset: 0x000DD82B
		private static string QuoteUnicodeLiteral(string literal)
		{
			return "'" + literal.Replace("'", "''").Replace("\\", "\\\\") + "'";
		}

		// Token: 0x1700151F RID: 5407
		// (get) Token: 0x06004242 RID: 16962 RVA: 0x000DF65B File Offset: 0x000DD85B
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return MySQLEnvironment.searchableTypes;
			}
		}

		// Token: 0x17001520 RID: 5408
		// (get) Token: 0x06004243 RID: 16963 RVA: 0x000DF662 File Offset: 0x000DD862
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return MySQLEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x06004244 RID: 16964 RVA: 0x000DF669 File Offset: 0x000DD869
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(MySQLEnvironment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x000DF67C File Offset: 0x000DD87C
		protected override DataTable GetSchemas()
		{
			return base.GetSchemaTable((DbConnection connection) => this.dbService.LoadSchemas(connection), true, "Schemas", new string[] { this.ReturnSingleDatabase.ToString(CultureInfo.InvariantCulture) });
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x000DF6BD File Offset: 0x000DD8BD
		public override DataTable LoadSchemas(DbConnection connection)
		{
			if (this.ReturnSingleDatabase)
			{
				return base.LoadData("Schemas", connection, "select SCHEMA_NAME from INFORMATION_SCHEMA.SCHEMATA where SCHEMA_NAME = {0}", new string[] { connection.Database });
			}
			return base.LoadData("Schemas", connection, "select SCHEMA_NAME from INFORMATION_SCHEMA.SCHEMATA order by SCHEMA_NAME");
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x000DF6FC File Offset: 0x000DD8FC
		protected override DataTable GetProcedures(SchemaItem? itemFilter)
		{
			return base.GetSchemaTable((DbConnection connection) => this.dbService.LoadProcedures(connection, null, null), true, "Procedures", new string[] { this.ReturnSingleDatabase.ToString(CultureInfo.InvariantCulture) });
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x000DF73D File Offset: 0x000DD93D
		public override DataTable LoadProcedures(DbConnection connection, string schemaFilter, string procedureFilter)
		{
			if (!this.ReturnSingleDatabase)
			{
				return base.LoadData("Procedures", connection, "select\r\n    ROUTINE_SCHEMA,\r\n    ROUTINE_NAME,\r\n    ROUTINE_TYPE,\r\n    CREATED as CREATED_DATE,\r\n    LAST_ALTERED as MODIFIED_DATE,\r\n    ROUTINE_COMMENT as DESCRIPTION\r\nfrom INFORMATION_SCHEMA.ROUTINES\r\nwhere ROUTINE_TYPE = 'FUNCTION'");
			}
			return base.LoadData("Procedures", connection, "select\r\n    ROUTINE_SCHEMA,\r\n    ROUTINE_NAME,\r\n    ROUTINE_TYPE,\r\n    CREATED as CREATED_DATE,\r\n    LAST_ALTERED as MODIFIED_DATE,\r\n    ROUTINE_COMMENT as DESCRIPTION\r\nfrom INFORMATION_SCHEMA.ROUTINES\r\nwhere ROUTINE_TYPE = 'FUNCTION' and ROUTINE_SCHEMA = {0}", new string[] { connection.Database });
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x000DF77C File Offset: 0x000DD97C
		protected override DataTable GetTables(SchemaItem? itemFilter)
		{
			return base.GetSchemaTable((DbConnection connection) => this.dbService.LoadTables(connection, null, null), true, "Tables", new string[] { this.ReturnSingleDatabase.ToString(CultureInfo.InvariantCulture) });
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x000DF7BD File Offset: 0x000DD9BD
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			if (!this.ReturnSingleDatabase)
			{
				return base.LoadData("Tables", connection, "select TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE, CREATE_TIME AS CREATED_DATE, UPDATE_TIME AS MODIFIED_DATE, TABLE_COMMENT AS DESCRIPTION\r\nfrom information_schema.TABLES ");
			}
			return base.LoadData("Tables", connection, "select TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE, CREATE_TIME AS CREATED_DATE, UPDATE_TIME AS MODIFIED_DATE, TABLE_COMMENT AS DESCRIPTION\r\nfrom information_schema.TABLES  where TABLE_SCHEMA = {0}", new string[] { connection.Database });
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x000DF7F9 File Offset: 0x000DD9F9
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Indexes", connection, "select\r\n    concat(i.CONSTRAINT_SCHEMA, '_', i.CONSTRAINT_NAME) as INDEX_NAME,\r\n    ii.COLUMN_NAME,\r\n    ii.ORDINAL_POSITION,\r\n    case when i.CONSTRAINT_TYPE = 'PRIMARY KEY' then 'Y' else 'N' end as PRIMARY_KEY\r\nfrom\r\n    INFORMATION_SCHEMA.table_constraints i inner join INFORMATION_SCHEMA.key_column_usage ii on\r\n        i.CONSTRAINT_SCHEMA = ii.CONSTRAINT_SCHEMA and\r\n        i.CONSTRAINT_NAME = ii.CONSTRAINT_NAME and\r\n        i.TABLE_SCHEMA = ii.TABLE_SCHEMA and\r\n        i.TABLE_NAME = ii.TABLE_NAME\r\nwhere i.TABLE_SCHEMA = {0} and i.TABLE_NAME = {1}\r\n    and i.CONSTRAINT_TYPE in ('PRIMARY KEY', 'UNIQUE')\r\n    and ii.POSITION_IN_UNIQUE_CONSTRAINT is null\r\norder by concat(i.CONSTRAINT_SCHEMA, '_', i.CONSTRAINT_NAME), ii.TABLE_SCHEMA, ii.TABLE_NAME, ii.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x000DF81A File Offset: 0x000DDA1A
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysParent", connection, "select\r\n    fkcol.REFERENCED_COLUMN_NAME as PK_COLUMN_NAME,\r\n    fkcol.TABLE_SCHEMA AS FK_TABLE_SCHEMA,\r\n    fkcol.TABLE_NAME AS FK_TABLE_NAME,\r\n    fkcol.COLUMN_NAME as FK_COLUMN_NAME,\r\n    fkcol.ORDINAL_POSITION as ORDINAL,\r\n    concat(fkcon.CONSTRAINT_SCHEMA, '_', fkcon.CONSTRAINT_NAME) as FK_NAME\r\nfrom\r\n    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS fkcon\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE fkcol\r\n        on fkcon.CONSTRAINT_SCHEMA = fkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME\r\nwhere fkcol.REFERENCED_TABLE_SCHEMA = {0} and fkcol.REFERENCED_TABLE_NAME = {1}\r\n    and fkcol.ORDINAL_POSITION = fkcol.POSITION_IN_UNIQUE_CONSTRAINT\r\norder by concat(fkcon.CONSTRAINT_SCHEMA, '_', fkcon.CONSTRAINT_NAME), fkcol.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x0600424D RID: 16973 RVA: 0x000DF83B File Offset: 0x000DDA3B
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysChild", connection, "select\r\n    fkcol.REFERENCED_TABLE_SCHEMA AS PK_TABLE_SCHEMA,\r\n    fkcol.REFERENCED_TABLE_NAME AS PK_TABLE_NAME,\r\n    fkcol.REFERENCED_COLUMN_NAME as PK_COLUMN_NAME,\r\n    fkcol.COLUMN_NAME as FK_COLUMN_NAME,\r\n    fkcol.ORDINAL_POSITION as ORDINAL,\r\n    concat(fkcon.CONSTRAINT_SCHEMA, '_', fkcon.CONSTRAINT_NAME) as FK_NAME\r\nfrom\r\n    INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS fkcon\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE fkcol\r\n        on fkcon.CONSTRAINT_SCHEMA = fkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME\r\nwhere fkcol.TABLE_SCHEMA = {0} and fkcol.TABLE_NAME = {1}\r\n    and fkcol.ORDINAL_POSITION = fkcol.POSITION_IN_UNIQUE_CONSTRAINT\r\norder by concat(fkcon.CONSTRAINT_SCHEMA, '_', fkcon.CONSTRAINT_NAME), fkcol.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x000DF85C File Offset: 0x000DDA5C
		public override DataTable LoadProcedureParameters(DbConnection connection, string schema, string function)
		{
			return base.LoadData("ProcedureParameters", connection, "select PARAMETER_NAME, ORDINAL_POSITION, DATA_TYPE\r\nfrom INFORMATION_SCHEMA.PARAMETERS\r\nwhere ROUTINE_TYPE = 'FUNCTION' and SPECIFIC_SCHEMA = {0} and SPECIFIC_NAME = {1}\r\norder by ORDINAL_POSITION", new string[] { schema, function });
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x000DF87D File Offset: 0x000DDA7D
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Columns", connection, "select COLUMN_NAME, ORDINAL_POSITION, IS_NULLABLE, DATA_TYPE, case when NUMERIC_PRECISION is null then null when DATA_TYPE in ('FLOAT', 'DOUBLE') then 2 else 10 end AS NUMERIC_PRECISION_RADIX, NUMERIC_PRECISION, NUMERIC_SCALE, CHARACTER_MAXIMUM_LENGTH, COLUMN_DEFAULT, COLUMN_COMMENT AS DESCRIPTION, COLUMN_TYPE\r\nfrom INFORMATION_SCHEMA.COLUMNS\r\nwhere table_schema = {0} and table_name = {1}", new string[] { schema, table });
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x000DF89E File Offset: 0x000DDA9E
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ResourceInformation", connection, "select sum(DATA_LENGTH + INDEX_LENGTH) as TOTAL_BYTES\r\nfrom information_schema.TABLES\r\nwhere TABLE_SCHEMA = {0}\r\nand TABLE_NAME = {1}", new string[] { schema, table });
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x000DF8BF File Offset: 0x000DDABF
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new MySQLEnvironment.MySQLConnectionStringBuilder(base.Host, this.Resource, this.TreatTinyAsBoolean, this.OldGuids, this.Encoding);
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x000DF8E4 File Offset: 0x000DDAE4
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return base.CreateUpdatableCatalogTableValue(tableValue, schema);
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x000DF902 File Offset: 0x000DDB02
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			MySQLAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x000DF90C File Offset: 0x000DDB0C
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return MySQLEnvironment.TimeType;
			case ValueKind.Date:
				return MySQLEnvironment.DateType;
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
				return MySQLEnvironment.DatetimeType;
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
					return MySQLEnvironment.SingleType;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return DbEnvironment.DecimalType;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return DbEnvironment.CurrencyType;
				}
				return MySQLEnvironment.DoubleType;
			case ValueKind.Logical:
				return MySQLEnvironment.BoolType;
			case ValueKind.Text:
			{
				long num = type.Facets.MaxLength.GetValueOrDefault(256L);
				bool? flag = type.Facets.IsVariableLength;
				bool flag2 = false;
				string text = (((flag.GetValueOrDefault() == flag2) & (flag != null)) ? string.Format(CultureInfo.InvariantCulture, "nchar({0})", num) : string.Format(CultureInfo.InvariantCulture, "nvarchar({0})", num));
				return new SqlDataType(type, new ConstantSqlString(text));
			}
			case ValueKind.Binary:
			{
				long num = type.Facets.MaxLength.GetValueOrDefault(1024L);
				bool? flag = type.Facets.IsVariableLength;
				bool flag2 = false;
				string text2 = (((flag.GetValueOrDefault() == flag2) & (flag != null)) ? string.Format(CultureInfo.InvariantCulture, "binary({0})", num) : string.Format(CultureInfo.InvariantCulture, "varbinary({0})", num));
				return new SqlDataType(type, new ConstantSqlString(text2));
			}
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x040022B5 RID: 8885
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=278885";

		// Token: 0x040022B6 RID: 8886
		private const string ClientLibraryName = "MySQL Connector/Net";

		// Token: 0x040022B7 RID: 8887
		private const int MillisecondsPerSecond = 1000;

		// Token: 0x040022B8 RID: 8888
		private static readonly SqlDataType SingleType = new SqlDataType(TypeValue.Single, new ConstantSqlString("float(24, 7)"));

		// Token: 0x040022B9 RID: 8889
		private static readonly SqlDataType DoubleType = new SqlDataType(TypeValue.Double, new ConstantSqlString("double"));

		// Token: 0x040022BA RID: 8890
		private static readonly SqlDataType DateType = new SqlDataType(TypeValue.Date, new ConstantSqlString("date"));

		// Token: 0x040022BB RID: 8891
		private static readonly SqlDataType DatetimeType = new SqlDataType(TypeValue.DateTime, new ConstantSqlString("datetime"));

		// Token: 0x040022BC RID: 8892
		private static readonly SqlDataType TimeType = new SqlDataType(TypeValue.Time, new ConstantSqlString("time"));

		// Token: 0x040022BD RID: 8893
		private static readonly SqlDataType BoolType = new SqlDataType(TypeValue.Logical, new ConstantSqlString("bool"));

		// Token: 0x040022BE RID: 8894
		private const string BinaryType = "binary({0})";

		// Token: 0x040022BF RID: 8895
		private const string VarbinaryType = "varbinary({0})";

		// Token: 0x040022C0 RID: 8896
		private const string NVarcharType = "nvarchar({0})";

		// Token: 0x040022C1 RID: 8897
		private const string NCharType = "nchar({0})";

		// Token: 0x040022C2 RID: 8898
		private const string StandardLiteralQuote = "'";

		// Token: 0x040022C3 RID: 8899
		private const long DefaultBinaryMaxLength = 1024L;

		// Token: 0x040022C4 RID: 8900
		private const long DefaultTextMaxLength = 256L;

		// Token: 0x040022C5 RID: 8901
		public const string DataSourceName = "MySQL";

		// Token: 0x040022C6 RID: 8902
		public const string MySQLProviderName = "MySql.Data.MySqlClient";

		// Token: 0x040022C7 RID: 8903
		private DbProviderFactory mySQLFactory;

		// Token: 0x040022C8 RID: 8904
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"bit", "date", "datetime", "timestamp", "time", "char", "nchar", "varchar", "nvarchar", "set",
			"enum", "tinytext", "text", "mediumtext", "longtext", "double", "float", "tinyint", "smallint", "int",
			"year", "mediumint", "bigint", "decimal", "tinyint unsigned", "smallint unsigned", "mediumint unsigned", "int unsigned", "bigint unsigned"
		};

		// Token: 0x040022C9 RID: 8905
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"bit",
				TypeValue.Int64
			},
			{
				"blob",
				TypeValue.Binary
			},
			{
				"tinyblob",
				TypeValue.Binary
			},
			{
				"mediumblob",
				TypeValue.Binary
			},
			{
				"longblob",
				TypeValue.Binary
			},
			{
				"binary",
				TypeValue.Binary
			},
			{
				"varbinary",
				TypeValue.Binary
			},
			{
				"date",
				TypeValue.Date
			},
			{
				"datetime",
				TypeValue.DateTime
			},
			{
				"timestamp",
				TypeValue.DateTime
			},
			{
				"time",
				TypeValue.Time
			},
			{
				"char",
				TypeValue.Text
			},
			{
				"nchar",
				TypeValue.Text
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"nvarchar",
				TypeValue.Text
			},
			{
				"set",
				TypeValue.Text
			},
			{
				"enum",
				TypeValue.Text
			},
			{
				"tinytext",
				TypeValue.Text
			},
			{
				"text",
				TypeValue.Text
			},
			{
				"mediumtext",
				TypeValue.Text
			},
			{
				"longtext",
				TypeValue.Text
			},
			{
				"double",
				TypeValue.Double
			},
			{
				"float",
				TypeValue.Single
			},
			{
				"tinyint",
				TypeValue.Int8
			},
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"int",
				TypeValue.Int32
			},
			{
				"year",
				TypeValue.Int32
			},
			{
				"mediumint",
				TypeValue.Int32
			},
			{
				"bigint",
				TypeValue.Int64
			},
			{
				"decimal",
				TypeValue.Decimal
			},
			{
				"tinyint unsigned",
				TypeValue.Byte
			},
			{
				"smallint unsigned",
				TypeValue.Int32
			},
			{
				"mediumint unsigned",
				TypeValue.Int64
			},
			{
				"int unsigned",
				TypeValue.Int64
			},
			{
				"bigint unsigned",
				TypeValue.Decimal
			}
		};

		// Token: 0x040022CA RID: 8906
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "varchar", "varbinary", "tinyblob", "blob", "mediumblob", "longblob", "tinytext", "text", "mediumtext", "longtext" };

		// Token: 0x02000913 RID: 2323
		private sealed class MySQLConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06004259 RID: 16985 RVA: 0x000DFFFC File Offset: 0x000DE1FC
			public MySQLConnectionStringBuilder(IEngineHost host, IResource resource, bool treatTinyAsBoolean, bool oldGuids, string encoding)
				: base(host, resource)
			{
				this.treatTinyAsBoolean = treatTinyAsBoolean;
				this.oldGuids = oldGuids;
				this.encoding = encoding;
			}

			// Token: 0x17001521 RID: 5409
			// (get) Token: 0x0600425A RID: 16986 RVA: 0x000E001D File Offset: 0x000DE21D
			protected override string UserNameKey
			{
				get
				{
					return "Uid";
				}
			}

			// Token: 0x17001522 RID: 5410
			// (get) Token: 0x0600425B RID: 16987 RVA: 0x000E0024 File Offset: 0x000DE224
			protected override string PasswordKey
			{
				get
				{
					return "Pwd";
				}
			}

			// Token: 0x17001523 RID: 5411
			// (get) Token: 0x0600425C RID: 16988 RVA: 0x000831E5 File Offset: 0x000813E5
			protected override string ServerKey
			{
				get
				{
					return "Server";
				}
			}

			// Token: 0x17001524 RID: 5412
			// (get) Token: 0x0600425D RID: 16989 RVA: 0x000831EC File Offset: 0x000813EC
			protected override string PortKey
			{
				get
				{
					return "Port";
				}
			}

			// Token: 0x17001525 RID: 5413
			// (get) Token: 0x0600425E RID: 16990 RVA: 0x000831F3 File Offset: 0x000813F3
			protected override string DatabaseKey
			{
				get
				{
					return "Database";
				}
			}

			// Token: 0x17001526 RID: 5414
			// (get) Token: 0x0600425F RID: 16991 RVA: 0x000E002B File Offset: 0x000DE22B
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "IntegratedSecurity";
				}
			}

			// Token: 0x17001527 RID: 5415
			// (get) Token: 0x06004260 RID: 16992 RVA: 0x000E0032 File Offset: 0x000DE232
			protected override string EncryptKey
			{
				get
				{
					return "SslMode";
				}
			}

			// Token: 0x17001528 RID: 5416
			// (get) Token: 0x06004261 RID: 16993 RVA: 0x00047CAD File Offset: 0x00045EAD
			protected override object AuthenticationTypeValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001529 RID: 5417
			// (get) Token: 0x06004262 RID: 16994 RVA: 0x00050F19 File Offset: 0x0004F119
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "ConnectionTimeout";
				}
			}

			// Token: 0x06004263 RID: 16995 RVA: 0x000E0039 File Offset: 0x000DE239
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				this.builder[this.EncryptKey] = (credential.RequireEncryption ? "Required" : "None");
				return true;
			}

			// Token: 0x06004264 RID: 16996 RVA: 0x000E0064 File Offset: 0x000DE264
			protected override void AddOptions()
			{
				this.builder["Allow User Variables"] = true;
				this.builder["Convert Zero Datetime"] = true;
				this.builder["Treat Tiny As Boolean"] = this.treatTinyAsBoolean;
				this.builder["Old Guids"] = this.oldGuids;
				if (!string.IsNullOrEmpty(this.encoding))
				{
					this.builder["CharSet"] = this.encoding;
				}
			}

			// Token: 0x06004265 RID: 16997 RVA: 0x000E00F8 File Offset: 0x000DE2F8
			protected override bool Apply(ConnectionStringPropertiesAdornment credential)
			{
				string text;
				if (credential.Properties.TryGetValue("EffectiveUserName", out text))
				{
					this.builder[this.UserNameKey] = text;
				}
				return true;
			}

			// Token: 0x040022CB RID: 8907
			private readonly bool treatTinyAsBoolean;

			// Token: 0x040022CC RID: 8908
			private readonly bool oldGuids;

			// Token: 0x040022CD RID: 8909
			private readonly string encoding;
		}
	}
}
