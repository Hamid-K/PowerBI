using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.PostgreSQL
{
	// Token: 0x02000539 RID: 1337
	internal class PostgreSQLEnvironment : DbEnvironment
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x00081BA0 File Offset: 0x0007FDA0
		private PostgreSQLEnvironment(IEngineHost host, string server, string database, Value options)
			: base(host, DatabaseResource.New("PostgreSQL", server, database), "PostgreSQL", server, database, options, null, null)
		{
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00081BCB File Offset: 0x0007FDCB
		public static PostgreSQLEnvironment Create(IEngineHost host, string server, string database, Value options)
		{
			return new PostgreSQLEnvironment(host, server, database, options);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x00081BD6 File Offset: 0x0007FDD6
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			PostgreSQLAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x00081BE0 File Offset: 0x0007FDE0
		public static string ClientSoftwareNotFoundExceptionMessage
		{
			get
			{
				return DbEnvironment.GetClientSoftwareNotFoundExceptionMessage("Npgsql version 2.0.12", "https://go.microsoft.com/fwlink/?LinkID=282716");
			}
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x00081BF6 File Offset: 0x0007FDF6
		public static string ProviderMissingErrorMessage
		{
			get
			{
				return Strings.DatabaseProviderMissingExceptionMessage("Npgsql");
			}
		}

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x00081C07 File Offset: 0x0007FE07
		public static string ProviderIncompatibleNETVersionMessage
		{
			get
			{
				return Strings.DatabaseProviderIncompatibleNETVersionMessage("Npgsql");
			}
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x00081C18 File Offset: 0x0007FE18
		public static string ProviderDowngradeMessage
		{
			get
			{
				return Strings.DatabaseProviderDowngradeMessage("Npgsql", "4.0.10.0");
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x00081C2E File Offset: 0x0007FE2E
		public static string ProviderConfigurationErrorMessage(string message)
		{
			return Strings.DatabaseProviderConfigurationErrorExceptionMessage("Npgsql", message);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x00081C40 File Offset: 0x0007FE40
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return PostgreSQLAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x00081C4C File Offset: 0x0007FE4C
		public override void AbortCommand(DbCommand command, DbDataReader reader)
		{
			if (!reader.IsClosed)
			{
				command.Cancel();
			}
			try
			{
				reader.Dispose();
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = base.Tracer.CreateTrace("AbortCommand", TraceEventType.Information))
				{
					base.TraceException(hostTrace, ex);
				}
				if (!(ex is DbException) && !(ex is IOException))
				{
					throw;
				}
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00081CC8 File Offset: 0x0007FEC8
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return PostgreSQLEnvironment.TimeType;
			case ValueKind.Date:
				return PostgreSQLEnvironment.DateType;
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
				return PostgreSQLEnvironment.DatetimeType;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return SqlDataType.Int;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return SqlDataType.BigInt;
				}
				if (type.Equals(TypeValue.Single))
				{
					return SqlDataType.Real;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return DbEnvironment.DecimalType;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return SqlDataType.Money;
				}
				return SqlDataType.Float;
			case ValueKind.Logical:
				return PostgreSQLEnvironment.BoolType;
			case ValueKind.Text:
			{
				long valueOrDefault = type.Facets.MaxLength.GetValueOrDefault(256L);
				bool? isVariableLength = type.Facets.IsVariableLength;
				bool flag = false;
				string text = (((isVariableLength.GetValueOrDefault() == flag) & (isVariableLength != null)) ? string.Format(CultureInfo.InvariantCulture, "char({0})", valueOrDefault) : string.Format(CultureInfo.InvariantCulture, "character varying({0})", valueOrDefault));
				return new SqlDataType(type, new ConstantSqlString(text));
			}
			case ValueKind.Binary:
				return PostgreSQLEnvironment.BinaryType;
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x00081E4C File Offset: 0x0008004C
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return base.CreateUpdatableCatalogTableValue(tableValue, schema);
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x00081E6A File Offset: 0x0008006A
		public string DatabaseCharacterSet
		{
			get
			{
				return (base.ServerMetadata as PostgreSQLEnvironment.PostgreSQLServerMetadata).DatabaseCharacterSet;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x00081E7C File Offset: 0x0008007C
		public bool DatabaseCharacterSetIsUTF8
		{
			get
			{
				return string.Equals(this.DatabaseCharacterSet, "UTF8", StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06002AFD RID: 11005 RVA: 0x00081E90 File Offset: 0x00080090
		private bool IsHighVersion
		{
			get
			{
				if (this.isHighVersion == null)
				{
					string[] array = base.ServerVersion.Split(new char[] { '.' });
					int num;
					this.isHighVersion = new bool?(array.Length > 1 && int.TryParse(array[0], out num) && num >= 8);
				}
				return this.isHighVersion.Value;
			}
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00081EF4 File Offset: 0x000800F4
		public static string QuoteUnicodeLiteral(bool supportsEscapeConstants, bool utf8Database, string literal)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in literal)
			{
				if (c == ' ')
				{
					stringBuilder.Append(" ");
				}
				else if (utf8Database && (char.IsControl(c) || char.IsWhiteSpace(c)))
				{
					StringBuilder stringBuilder2 = stringBuilder.Append("\\");
					int num = (int)c;
					stringBuilder2.Append(num.ToString("X4", CultureInfo.InvariantCulture));
				}
				else if (c == '\'')
				{
					stringBuilder.Append("''");
				}
				else if (c == '\\')
				{
					stringBuilder.Append("\\\\");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			string text = stringBuilder.ToString();
			bool flag = text.Length > literal.Length;
			string text2 = (utf8Database ? "U&'" : "E'");
			return ((flag && supportsEscapeConstants) ? text2 : "'") + text + "'";
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x00081FEC File Offset: 0x000801EC
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			PostgreSQLAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x00081FF6 File Offset: 0x000801F6
		protected override string ProviderName
		{
			get
			{
				return "Npgsql";
			}
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x06002B01 RID: 11009 RVA: 0x00081FFD File Offset: 0x000801FD
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/?LinkID=282716";
			}
		}

		// Token: 0x17001027 RID: 4135
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x00082004 File Offset: 0x00080204
		protected override string ProviderLibraryName
		{
			get
			{
				return "Npgsql version 2.0.12";
			}
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06002B03 RID: 11011 RVA: 0x00002139 File Offset: 0x00000339
		public override bool SupportsNativeQueryFolding
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0008200B File Offset: 0x0008020B
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return PostgreSQLModule.OptionRecord;
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x00082014 File Offset: 0x00080214
		private static bool TryGetVersionFromProviderFactory(DbProviderFactory dbFactory, out string versionStr)
		{
			versionStr = string.Empty;
			MatchCollection matchCollection = Regex.Matches(dbFactory.GetType().AssemblyQualifiedName, "Version=([0-9]+\\.[0-9]+\\.[0-9]+\\.[0-9]+),");
			if (matchCollection.Count > 0)
			{
				versionStr = matchCollection[0].Groups[1].Value;
				return true;
			}
			return false;
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x00082063 File Offset: 0x00080263
		private bool TryGetFactoryFromPrivateProviderManager(out DbProviderFactory dbFactory)
		{
			dbFactory = null;
			return DbEnvironment.privateProviderManager.Value.TryCreateFactory(base.Host, this.ProviderName, out dbFactory, false);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00082088 File Offset: 0x00080288
		private bool TryGetFactoryFromGAC(out DbProviderFactory dbFactory)
		{
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

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06002B08 RID: 11016 RVA: 0x000820C0 File Offset: 0x000802C0
		private DbProviderFactory PostgreSQLFactory
		{
			get
			{
				if (this.postgreSQLFactory == null)
				{
					string empty = string.Empty;
					bool flag = this.TryGetFactoryFromGAC(out this.postgreSQLFactory);
					if (flag)
					{
						if (PostgreSQLEnvironment.TryGetVersionFromProviderFactory(this.postgreSQLFactory, out empty) && new Version(empty) >= new Version("4.1.0.0"))
						{
							if (!this.TryGetFactoryFromPrivateProviderManager(out this.postgreSQLFactory))
							{
								using (IHostTrace hostTrace = base.Tracer.CreateTrace("PostgreSQLFactory", TraceEventType.Error))
								{
									hostTrace.Add("ProviderVersion", empty, false);
								}
								throw DataSourceException.NewMissingClientLibraryError<Message0>(base.Host, new Message0(PostgreSQLEnvironment.ProviderIncompatibleNETVersionMessage + " " + PostgreSQLEnvironment.ProviderDowngradeMessage), this.Resource, null, null, null);
							}
							flag = false;
							PostgreSQLEnvironment.TryGetVersionFromProviderFactory(this.postgreSQLFactory, out empty);
						}
					}
					else
					{
						if (!this.TryGetFactoryFromPrivateProviderManager(out this.postgreSQLFactory))
						{
							using (IHostTrace hostTrace2 = base.Tracer.CreateTrace("PostgreSQLFactory", TraceEventType.Error))
							{
								hostTrace2.Add("ProviderVersion", "Not installed", false);
							}
							throw DataSourceException.NewMissingClientLibraryError<Message0>(base.Host, new Message0(PostgreSQLEnvironment.ProviderDowngradeMessage), this.Resource, null, null, null);
						}
						PostgreSQLEnvironment.TryGetVersionFromProviderFactory(this.postgreSQLFactory, out empty);
					}
					using (IHostTrace hostTrace3 = base.Tracer.CreateTrace("PostgreSQLFactory", TraceEventType.Information))
					{
						string text = "Using " + this.ProviderName;
						if (empty != string.Empty)
						{
							text = text + " " + empty;
						}
						text += (flag ? " GAC installation" : " built-in installation");
						hostTrace3.Add("PostgreSQLProviderInstallation", text, false);
					}
				}
				return this.postgreSQLFactory;
			}
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000822AC File Offset: 0x000804AC
		protected override DbProviderFactory CreateDbProviderFactory()
		{
			DbProviderFactory dbProviderFactory;
			try
			{
				dbProviderFactory = this.PostgreSQLFactory;
			}
			catch (ConfigurationErrorsException ex)
			{
				throw DataSourceException.NewDataSourceError<Message2>(base.Host, Strings.ConfigurationErrorsExceptionMessage(ex.Message, DbEnvironment.MachineConfigPath), this.Resource, null, ex);
			}
			return dbProviderFactory;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000822F8 File Offset: 0x000804F8
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			ResourceExceptionKind resourceExceptionKind2;
			using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
			{
				ResourceExceptionKind resourceExceptionKind;
				if (exception.InnerException is InvalidOperationException || PostgreSQLEnvironment.IsSSLExceptionMessage(exception.Message))
				{
					resourceExceptionKind = ResourceExceptionKind.SecureConnectionFailed;
				}
				else
				{
					string text;
					resourceExceptionKind = (PostgreSQLEnvironment.TestErrorCode(exception, PostgreSQLEnvironment.AuthenticationErrorCodes, out text) ? ResourceExceptionKind.InvalidCredentials : ResourceExceptionKind.None);
					hostTrace.Add("ExceptionCode", text, false);
				}
				hostTrace.Add("Exception", exception, true);
				hostTrace.Add("ResourceExceptionKind", resourceExceptionKind, false);
				resourceExceptionKind2 = resourceExceptionKind;
			}
			return resourceExceptionKind2;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x00082394 File Offset: 0x00080594
		private static bool IsSSLExceptionMessage(string message)
		{
			return string.Equals(message, "SSL connection requested. No SSL enabled connection from this host is configured.", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000823A2 File Offset: 0x000805A2
		protected override Exception ProcessException(Exception e)
		{
			if (PostgreSQLEnvironment.IsSSLExceptionMessage(e.Message))
			{
				return DataSourceException.NewEncryptedConnectionError(base.Host, this.Resource, e.Message, null, e);
			}
			return base.ProcessException(e);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000823D2 File Offset: 0x000805D2
		public override DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			return new PostgreSQLReader(base.WrapDbDataReader(reader), this);
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000823E1 File Offset: 0x000805E1
		protected override RetryBehavior RetryAfterSqlError(DbException exception)
		{
			return PostgreSQLEnvironment.RetryAfterSqlErrorStatic(exception);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000823EC File Offset: 0x000805EC
		protected override SqlSettings LoadSqlSettings()
		{
			Func<string, string> func = (string s) => PostgreSQLEnvironment.QuoteUnicodeLiteral(this.IsHighVersion, this.DatabaseCharacterSetIsUTF8, s);
			return new SqlSettings
			{
				MaxIdentifierLength = 63,
				InvalidIdentifierCharacters = EmptyArray<char>.Instance,
				PagingStrategy = PagingStrategy.Limit,
				RequiresAsForFromAlias = false,
				DateTimePrefix = "timestamp '",
				DatePrefix = "date '",
				DateSuffix = "'",
				QuoteAnsiStringLiteral = func,
				QuoteNationalStringLiteral = func,
				SupportsFullOuterJoinExpression = true,
				SupportsForeignKeys = true,
				SupportsCaseExpression = true,
				SelectItemNull = SqlLanguageStrings.NullWithTrivialCastSqlString,
				TimePrefix = "time '",
				IntervalPrefix = "interval '",
				IntervalSuffix = "' day to second",
				SupportsOutputClause = true,
				SupportsIntervalConstants = true,
				DeleteCommand = SqlLanguageStrings.DeleteFromSqlString,
				BinaryPrefix = "decode('",
				BinarySuffix = "','hex')",
				IsMaxPrecision = true
			};
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000824D6 File Offset: 0x000806D6
		public static RetryBehavior RetryAfterSqlErrorStatic(DbException exception)
		{
			return new RetryBehavior(PostgreSQLEnvironment.TestErrorCode(exception, PostgreSQLEnvironment.RetryableErrorCodes), DbEnvironment.RetryDelay);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000824F0 File Offset: 0x000806F0
		private static bool TestErrorCode(DbException exception, HashSet<string> errorCodes)
		{
			string text;
			return PostgreSQLEnvironment.TestErrorCode(exception, errorCodes, out text);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x00082508 File Offset: 0x00080708
		private static bool TestErrorCode(DbException exception, HashSet<string> errorCodes, out string exceptionCode)
		{
			exceptionCode = string.Empty;
			Type type = exception.GetType();
			string fullName = type.FullName;
			if (fullName == "Npgsql.NpgsqlException" || fullName == "Npgsql.PostgresException")
			{
				PropertyInfo property = type.GetProperty("Code");
				if (property != null)
				{
					exceptionCode = (string)property.GetGetMethod().Invoke(exception, null);
				}
				return errorCodes.Contains(exceptionCode);
			}
			return false;
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06002B13 RID: 11027 RVA: 0x00082579 File Offset: 0x00080779
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return PostgreSQLEnvironment.searchableTypes;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x00082580 File Offset: 0x00080780
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return PostgreSQLEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x00082587 File Offset: 0x00080787
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(PostgreSQLEnvironment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00082599 File Offset: 0x00080799
		protected override DbEnvironment.DbServerMetadata LoadServerMetadataFromStream(Stream s)
		{
			PostgreSQLEnvironment.PostgreSQLServerMetadata postgreSQLServerMetadata = new PostgreSQLEnvironment.PostgreSQLServerMetadata();
			postgreSQLServerMetadata.Deserialize(s);
			return postgreSQLServerMetadata;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000825A7 File Offset: 0x000807A7
		protected override DbEnvironment.DbServerMetadata LoadServerMetadata()
		{
			return this.ConvertDbExceptions<PostgreSQLEnvironment.PostgreSQLServerMetadata>(delegate
			{
				PostgreSQLEnvironment.PostgreSQLServerMetadata postgreSQLServerMetadata;
				using (DbConnection dbConnection = base.CreateConnection())
				{
					dbConnection.Open(this);
					using (DbCommand dbCommand = dbConnection.CreateCommand())
					{
						dbCommand.CommandType = CommandType.Text;
						dbCommand.CommandText = "select character_set_name from INFORMATION_SCHEMA.character_sets";
						string text;
						try
						{
							text = dbCommand.ExecuteScalar().ToString();
						}
						catch (Exception ex) when (Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							text = "SQL_ASCII";
						}
						postgreSQLServerMetadata = new PostgreSQLEnvironment.PostgreSQLServerMetadata
						{
							Version = dbConnection.ServerVersion,
							DatabaseCharacterSet = text
						};
					}
				}
				return postgreSQLServerMetadata;
			});
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000825BB File Offset: 0x000807BB
		public override DataTable LoadSchemas(DbConnection connection)
		{
			return base.LoadData("Schemas", connection, "select SCHEMA_NAME from INFORMATION_SCHEMA.SCHEMATA order by SCHEMA_NAME");
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000825CE File Offset: 0x000807CE
		public override DataTable LoadTables(DbConnection connection, string schemaFilter, string tableFilter)
		{
			return base.LoadData("Tables", connection, "select TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE\r\nfrom INFORMATION_SCHEMA.tables\r\nwhere TABLE_SCHEMA not in ('information_schema', 'pg_catalog')\r\norder by TABLE_SCHEMA, TABLE_NAME");
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000825E1 File Offset: 0x000807E1
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Columns", connection, "select COLUMN_NAME, ORDINAL_POSITION, IS_NULLABLE, case when (data_type like '%unsigned%') then DATA_TYPE || ' unsigned' else DATA_TYPE end as DATA_TYPE\r\nfrom INFORMATION_SCHEMA.columns\r\nwhere TABLE_SCHEMA = {0} and TABLE_NAME = {1}\r\norder by TABLE_SCHEMA, TABLE_NAME, ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x00082602 File Offset: 0x00080802
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Indexes", connection, "select i.CONSTRAINT_SCHEMA || '_' || i.CONSTRAINT_NAME as INDEX_NAME, ii.COLUMN_NAME, ii.ORDINAL_POSITION, case when i.CONSTRAINT_TYPE = 'PRIMARY KEY' then 'Y' else 'N' end as PRIMARY_KEY\r\nfrom INFORMATION_SCHEMA.table_constraints i inner join INFORMATION_SCHEMA.key_column_usage ii on i.CONSTRAINT_SCHEMA = ii.CONSTRAINT_SCHEMA and i.CONSTRAINT_NAME = ii.CONSTRAINT_NAME and i.TABLE_SCHEMA = ii.TABLE_SCHEMA and i.TABLE_NAME = ii.TABLE_NAME\r\nwhere i.TABLE_SCHEMA = {0} and i.TABLE_NAME = {1}\r\nand i.CONSTRAINT_TYPE in ('PRIMARY KEY', 'UNIQUE')\r\norder by i.CONSTRAINT_SCHEMA || '_' || i.CONSTRAINT_NAME, ii.TABLE_SCHEMA, ii.TABLE_NAME, ii.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x00082623 File Offset: 0x00080823
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysParent", connection, "select\r\n    pkcol.COLUMN_NAME as PK_COLUMN_NAME,\r\n    fkcol.TABLE_SCHEMA AS FK_TABLE_SCHEMA,\r\n    fkcol.TABLE_NAME AS FK_TABLE_NAME,\r\n    fkcol.COLUMN_NAME as FK_COLUMN_NAME,\r\n    fkcol.ORDINAL_POSITION as ORDINAL,\r\n    fkcon.CONSTRAINT_SCHEMA || '_' || fkcol.TABLE_NAME || '_' || {1} || '_' || fkcon.CONSTRAINT_NAME as FK_NAME\r\nfrom\r\n    (select distinct constraint_catalog, constraint_schema, unique_constraint_schema, constraint_name, unique_constraint_name\r\n        from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS) fkcon\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE fkcol\r\n        on fkcon.CONSTRAINT_SCHEMA = fkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE pkcol\r\n        on fkcon.UNIQUE_CONSTRAINT_SCHEMA = pkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.UNIQUE_CONSTRAINT_NAME = pkcol.CONSTRAINT_NAME\r\nwhere pkcol.TABLE_SCHEMA = {0} and pkcol.TABLE_NAME = {1}\r\n        and pkcol.ORDINAL_POSITION = fkcol.ORDINAL_POSITION\r\norder by FK_NAME, fkcol.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x00082644 File Offset: 0x00080844
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysChild", connection, "select\r\n    pkcol.TABLE_SCHEMA AS PK_TABLE_SCHEMA,\r\n    pkcol.TABLE_NAME AS PK_TABLE_NAME,\r\n    pkcol.COLUMN_NAME as PK_COLUMN_NAME,\r\n    fkcol.COLUMN_NAME as FK_COLUMN_NAME,\r\n    fkcol.ORDINAL_POSITION as ORDINAL,\r\n    fkcon.CONSTRAINT_SCHEMA || '_' || {1} || '_' || pkcol.TABLE_NAME || '_' || fkcon.CONSTRAINT_NAME as FK_NAME\r\nfrom\r\n    (select distinct constraint_catalog, constraint_schema, unique_constraint_schema, constraint_name, unique_constraint_name\r\n        from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS) fkcon\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE fkcol\r\n        on fkcon.CONSTRAINT_SCHEMA = fkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.CONSTRAINT_NAME = fkcol.CONSTRAINT_NAME\r\n        inner join\r\n    INFORMATION_SCHEMA.KEY_COLUMN_USAGE pkcol\r\n        on fkcon.UNIQUE_CONSTRAINT_SCHEMA = pkcol.CONSTRAINT_SCHEMA\r\n        and fkcon.UNIQUE_CONSTRAINT_NAME = pkcol.CONSTRAINT_NAME\r\nwhere fkcol.TABLE_SCHEMA = {0} and fkcol.TABLE_NAME = {1}\r\n        and pkcol.ORDINAL_POSITION = fkcol.ORDINAL_POSITION\r\norder by FK_NAME, fkcol.ORDINAL_POSITION", new string[] { schema, table });
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00082665 File Offset: 0x00080865
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			if (!this.IsHighVersion)
			{
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
			}
			return base.LoadData("ResourceInformation", connection, "\r\nSELECT PG_TOTAL_RELATION_SIZE(C.OID) AS TOTAL_BYTES\r\nFROM PG_CLASS C JOIN PG_NAMESPACE N\r\n    ON (N.OID = C.RELNAMESPACE)\r\nWHERE N.NSPNAME = {0} AND C.RELNAME = {1}", new string[] { schema, table });
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000826A0 File Offset: 0x000808A0
		public override TableValue GetDirectQueryCapabilities()
		{
			if (this.capabilities == null)
			{
				List<Value> list = new List<Value>();
				list.Add(CapabilityModule.NewCapability("Core", Value.Null));
				list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(2100)));
				list.AddRange(DbEnvironment.Capabilities.TableFunctions.Select((string tableFunction) => CapabilityModule.NewCapability(tableFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.DateFunctions.Select((string dateFunction) => CapabilityModule.NewCapability(dateFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.NumericFunctions.Select((string numericFunction) => CapabilityModule.NewCapability(numericFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.StringFunctions.Select((string stringFunction) => CapabilityModule.NewCapability(stringFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.ListFunctions.Select((string listFunction) => CapabilityModule.NewCapability(listFunction, Value.Null)).Cast<Value>());
				TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
				this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
			}
			return this.capabilities;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x00082828 File Offset: 0x00080A28
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new PostgreSQLEnvironment.PostgreSQLConnectionStringBuilder(base.Host, this.Resource);
		}

		// Token: 0x040012A1 RID: 4769
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkID=282716";

		// Token: 0x040012A2 RID: 4770
		private const string ClientLibraryName = "Npgsql version 2.0.12";

		// Token: 0x040012A3 RID: 4771
		private TableValue capabilities;

		// Token: 0x040012A4 RID: 4772
		private static readonly SqlDataType DateType = new SqlDataType(TypeValue.Date, new ConstantSqlString("date"));

		// Token: 0x040012A5 RID: 4773
		private static readonly SqlDataType DatetimeType = new SqlDataType(TypeValue.DateTime, new ConstantSqlString("timestamp"));

		// Token: 0x040012A6 RID: 4774
		private static readonly SqlDataType TimeType = new SqlDataType(TypeValue.Time, new ConstantSqlString("time"));

		// Token: 0x040012A7 RID: 4775
		private static readonly SqlDataType BinaryType = new SqlDataType(TypeValue.Binary, new ConstantSqlString("bytea"));

		// Token: 0x040012A8 RID: 4776
		private static readonly SqlDataType BoolType = new SqlDataType(TypeValue.Logical, new ConstantSqlString("boolean"));

		// Token: 0x040012A9 RID: 4777
		private const string CharacterVaryingType = "character varying({0})";

		// Token: 0x040012AA RID: 4778
		private const string CharType = "char({0})";

		// Token: 0x040012AB RID: 4779
		private const long DefaultTextMaxLength = 256L;

		// Token: 0x040012AC RID: 4780
		public const string DataSourceName = "PostgreSQL";

		// Token: 0x040012AD RID: 4781
		public const string PostgreSQLProviderName = "Npgsql";

		// Token: 0x040012AE RID: 4782
		private const string MaxNET45PostgreSQLVersion = "4.0.10.0";

		// Token: 0x040012AF RID: 4783
		private const string MinNET46PostgreSQLVersion = "4.1.0.0";

		// Token: 0x040012B0 RID: 4784
		private bool? isHighVersion;

		// Token: 0x040012B1 RID: 4785
		private const string StandardLiteralQuote = "'";

		// Token: 0x040012B2 RID: 4786
		private static readonly HashSet<string> AuthenticationErrorCodes = new HashSet<string> { "28P01", "28000" };

		// Token: 0x040012B3 RID: 4787
		private static readonly HashSet<string> RetryableErrorCodes = new HashSet<string>();

		// Token: 0x040012B4 RID: 4788
		private DbProviderFactory postgreSQLFactory;

		// Token: 0x040012B5 RID: 4789
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"abstime", "bigint", "bit", "bit varying", "boolean", "box", "\"char\"", "character", "character varying", "cidr",
			"circle", "date", "daterange", "double precision", "inet", "int4range", "int8range", "integer", "interval", "line",
			"lseg", "macaddr", "money", "name", "numeric", "numrange", "oid", "path", "pg_node_tree", "point",
			"polygon", "real", "refcursor", "regclass", "regconfig", "regdictionary", "regoper", "regoperator", "regproc", "regprocedure",
			"regtype", "reltime", "smallint", "smgr", "text", "tid", "time with time zone", "time without time zone", "timestamp with time zone", "timestamp without time zone",
			"tinterval", "tsquery", "tsrange", "tstzrange", "tsvector", "txid_snapshot", "uuid", "xid", "xml"
		};

		// Token: 0x040012B6 RID: 4790
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"abstime",
				TypeValue.DateTime
			},
			{
				"aclitem",
				TypeValue.Text
			},
			{
				"bigint",
				TypeValue.Int64
			},
			{
				"bit",
				TypeValue.SerializedText
			},
			{
				"bit varying",
				TypeValue.SerializedText
			},
			{
				"boolean",
				TypeValue.Logical
			},
			{
				"box",
				TypeValue.SerializedText
			},
			{
				"bytea",
				TypeValue.Binary
			},
			{
				"\"char\"",
				TypeValue.Text
			},
			{
				"character",
				TypeValue.Text
			},
			{
				"character varying",
				TypeValue.Text
			},
			{
				"cid",
				TypeValue.Text
			},
			{
				"cidr",
				TypeValue.Text
			},
			{
				"circle",
				TypeValue.SerializedText
			},
			{
				"date",
				TypeValue.Date
			},
			{
				"daterange",
				TypeValue.Text
			},
			{
				"double precision",
				TypeValue.Double
			},
			{
				"gtsvector",
				TypeValue.Text
			},
			{
				"inet",
				TypeValue.SerializedText
			},
			{
				"array",
				TypeValue.SerializedText
			},
			{
				"int4range",
				TypeValue.Text
			},
			{
				"int8range",
				TypeValue.Text
			},
			{
				"integer",
				TypeValue.Int32
			},
			{
				"interval",
				TypeValue.Duration
			},
			{
				"json",
				TypeValue.Text
			},
			{
				"line",
				TypeValue.SerializedText
			},
			{
				"lseg",
				TypeValue.SerializedText
			},
			{
				"macaddr",
				TypeValue.SerializedText
			},
			{
				"money",
				TypeValue.Currency
			},
			{
				"name",
				TypeValue.Text
			},
			{
				"numeric",
				TypeValue.Decimal
			},
			{
				"numrange",
				TypeValue.Text
			},
			{
				"oid",
				TypeValue.Int64
			},
			{
				"path",
				TypeValue.SerializedText
			},
			{
				"pg_node_tree",
				TypeValue.Text
			},
			{
				"point",
				TypeValue.SerializedText
			},
			{
				"polygon",
				TypeValue.SerializedText
			},
			{
				"real",
				TypeValue.Single
			},
			{
				"refcursor",
				TypeValue.Text
			},
			{
				"regclass",
				TypeValue.Text
			},
			{
				"regconfig",
				TypeValue.Text
			},
			{
				"regdictionary",
				TypeValue.Text
			},
			{
				"regoper",
				TypeValue.Text
			},
			{
				"regoperator",
				TypeValue.Text
			},
			{
				"regproc",
				TypeValue.Text
			},
			{
				"regprocedure",
				TypeValue.Text
			},
			{
				"regtype",
				TypeValue.Text
			},
			{
				"reltime",
				TypeValue.Text
			},
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"smgr",
				TypeValue.Text
			},
			{
				"text",
				TypeValue.Text
			},
			{
				"tid",
				TypeValue.Text
			},
			{
				"time with time zone",
				TypeValue.Time
			},
			{
				"time without time zone",
				TypeValue.Time
			},
			{
				"timestamp with time zone",
				TypeValue.DateTime
			},
			{
				"timestamp without time zone",
				TypeValue.DateTime
			},
			{
				"tinterval",
				TypeValue.Text
			},
			{
				"tsquery",
				TypeValue.Text
			},
			{
				"tsrange",
				TypeValue.Text
			},
			{
				"tstzrange",
				TypeValue.Text
			},
			{
				"tsvector",
				TypeValue.Text
			},
			{
				"txid_snapshot",
				TypeValue.Text
			},
			{
				"uuid",
				TypeValue.SerializedText
			},
			{
				"xid",
				TypeValue.Text
			},
			{
				"xml",
				TypeValue.SerializedText
			}
		};

		// Token: 0x040012B7 RID: 4791
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"bit varying", "character varying", "text", "varchar", "bytea", "json", "xml", "path", "polygon", "inet",
			"cidr", "bit", "int4range", "int8range", "tsrange", "numeric", "tsvector", "tstzrange", "daterange"
		};

		// Token: 0x0200053A RID: 1338
		protected class PostgreSQLServerMetadata : DbEnvironment.DbServerMetadata
		{
			// Token: 0x1700102D RID: 4141
			// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000831A0 File Offset: 0x000813A0
			// (set) Token: 0x06002B25 RID: 11045 RVA: 0x000831A8 File Offset: 0x000813A8
			public string DatabaseCharacterSet { get; set; }

			// Token: 0x06002B26 RID: 11046 RVA: 0x000831B1 File Offset: 0x000813B1
			protected override void Serialize(BinaryWriter writer)
			{
				writer.WriteNullableString(base.Version);
				writer.WriteNullableString(this.DatabaseCharacterSet);
			}

			// Token: 0x06002B27 RID: 11047 RVA: 0x000831CB File Offset: 0x000813CB
			protected override void Deserialize(BinaryReader reader)
			{
				base.Version = reader.ReadNullableString();
				this.DatabaseCharacterSet = reader.ReadNullableString();
			}
		}

		// Token: 0x0200053B RID: 1339
		private sealed class PostgreSQLConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06002B29 RID: 11049 RVA: 0x00047C79 File Offset: 0x00045E79
			public PostgreSQLConnectionStringBuilder(IEngineHost host, IResource resource)
				: base(host, resource)
			{
			}

			// Token: 0x1700102E RID: 4142
			// (get) Token: 0x06002B2A RID: 11050 RVA: 0x0005C651 File Offset: 0x0005A851
			protected override string UserNameKey
			{
				get
				{
					return "User Id";
				}
			}

			// Token: 0x1700102F RID: 4143
			// (get) Token: 0x06002B2B RID: 11051 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17001030 RID: 4144
			// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000831E5 File Offset: 0x000813E5
			protected override string ServerKey
			{
				get
				{
					return "Server";
				}
			}

			// Token: 0x17001031 RID: 4145
			// (get) Token: 0x06002B2D RID: 11053 RVA: 0x000831EC File Offset: 0x000813EC
			protected override string PortKey
			{
				get
				{
					return "Port";
				}
			}

			// Token: 0x17001032 RID: 4146
			// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000831F3 File Offset: 0x000813F3
			protected override string DatabaseKey
			{
				get
				{
					return "Database";
				}
			}

			// Token: 0x17001033 RID: 4147
			// (get) Token: 0x06002B2F RID: 11055 RVA: 0x00047C9F File Offset: 0x00045E9F
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated Security";
				}
			}

			// Token: 0x17001034 RID: 4148
			// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000831FA File Offset: 0x000813FA
			protected override string EncryptKey
			{
				get
				{
					return "Sslmode";
				}
			}

			// Token: 0x17001035 RID: 4149
			// (get) Token: 0x06002B31 RID: 11057 RVA: 0x00047CAD File Offset: 0x00045EAD
			protected override object AuthenticationTypeValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001036 RID: 4150
			// (get) Token: 0x06002B32 RID: 11058 RVA: 0x00083201 File Offset: 0x00081401
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "timeout";
				}
			}

			// Token: 0x17001037 RID: 4151
			// (get) Token: 0x06002B33 RID: 11059 RVA: 0x00047CBC File Offset: 0x00045EBC
			protected override int? DefaultConnectionTimeout
			{
				get
				{
					return new int?(60);
				}
			}

			// Token: 0x06002B34 RID: 11060 RVA: 0x00083208 File Offset: 0x00081408
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				this.builder[this.EncryptKey] = (credential.RequireEncryption ? "Require" : "Disable");
				return true;
			}

			// Token: 0x06002B35 RID: 11061 RVA: 0x00083230 File Offset: 0x00081430
			protected override void AddOptions()
			{
				this.builder["MaxPoolSize"] = 1024;
			}
		}
	}
}
