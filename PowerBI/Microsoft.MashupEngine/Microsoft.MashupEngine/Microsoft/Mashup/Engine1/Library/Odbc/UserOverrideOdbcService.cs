using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000681 RID: 1665
	internal sealed class UserOverrideOdbcService : OdbcDelegatingService
	{
		// Token: 0x06003440 RID: 13376 RVA: 0x000A802C File Offset: 0x000A622C
		public UserOverrideOdbcService(bool isPrivileged, IOdbcService service, ILifetimeService lifetimeService, RecordValue sqlGetInfoRecord, Value sqlGetTypeInfo, RecordValue sqlGetFunctions, Value sqlGetColumns, Value sqlGetTables)
			: base(service)
		{
			this.isPrivileged = isPrivileged;
			this.lifetimeService = lifetimeService;
			this.sqlGetTypeInfo = sqlGetTypeInfo;
			this.sqlGetFunctions = sqlGetFunctions;
			this.sqlGetColumns = sqlGetColumns;
			this.sqlGetTables = sqlGetTables;
			this.sqlGetInfo = new Dictionary<Odbc32.SQL_INFO, Value>(sqlGetInfoRecord.Count);
			for (int i = 0; i < sqlGetInfoRecord.Count; i++)
			{
				string text = sqlGetInfoRecord.Keys[i];
				Odbc32.SQL_INFO sql_INFO;
				if (!EnumHelper.TryParse<Odbc32.SQL_INFO>(text, out sql_INFO) || (!UserOverrideOdbcService.allowedOverrides.Contains(sql_INFO) && (!this.isPrivileged || !UserOverrideOdbcService.privilegedAllowedOverride.Contains(sql_INFO))))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Odbc_InvalidSQLGetInfoOverride, TextValue.New(text), null);
				}
				Value value = sqlGetInfoRecord[i];
				this.sqlGetInfo.Add(sql_INFO, value);
			}
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000A80F4 File Offset: 0x000A62F4
		public override IOdbcConnection CreateConnection(OdbcConnectionProperties args)
		{
			return new UserOverrideOdbcService.UserOverrideOdbcConnection(this, base.CreateConnection(args));
		}

		// Token: 0x04001779 RID: 6009
		private static readonly HashSet<Odbc32.SQL_INFO> allowedOverrides = new HashSet<Odbc32.SQL_INFO>
		{
			Odbc32.SQL_INFO.SQL_ACTIVE_CONNECTIONS,
			Odbc32.SQL_INFO.SQL_MAX_CONCURRENT_ACTIVITIES,
			Odbc32.SQL_INFO.SQL_ACCESSIBLE_TABLES,
			Odbc32.SQL_INFO.SQL_ACCESSIBLE_PROCEDURES,
			Odbc32.SQL_INFO.SQL_PROCEDURES,
			Odbc32.SQL_INFO.SQL_CONCAT_NULL_BEHAVIOR,
			Odbc32.SQL_INFO.SQL_CURSOR_COMMIT_BEHAVIOR,
			Odbc32.SQL_INFO.SQL_CURSOR_ROLLBACK_BEHAVIOR,
			Odbc32.SQL_INFO.SQL_DEFAULT_TXN_ISOLATION,
			Odbc32.SQL_INFO.SQL_EXPRESSIONS_IN_ORDERBY,
			Odbc32.SQL_INFO.SQL_IDENTIFIER_CASE,
			Odbc32.SQL_INFO.SQL_MAX_COLUMN_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_CURSOR_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_OWNER_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_OWNER_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_PROCEDURE_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_QUALIFIER_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_QUALIFIER_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_TABLE_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MULT_RESULT_SETS,
			Odbc32.SQL_INFO.SQL_MULTIPLE_ACTIVE_TXN,
			Odbc32.SQL_INFO.SQL_OUTER_JOINS,
			Odbc32.SQL_INFO.SQL_SCHEMA_TERM,
			Odbc32.SQL_INFO.SQL_PROCEDURE_TERM,
			Odbc32.SQL_INFO.SQL_CATALOG_NAME_SEPARATOR,
			Odbc32.SQL_INFO.SQL_CATALOG_TERM,
			Odbc32.SQL_INFO.SQL_SCROLL_CONCURRENCY,
			Odbc32.SQL_INFO.SQL_SCROLL_OPTIONS,
			Odbc32.SQL_INFO.SQL_TABLE_TERM,
			Odbc32.SQL_INFO.SQL_TXN_CAPABLE,
			Odbc32.SQL_INFO.SQL_USER_NAME,
			Odbc32.SQL_INFO.SQL_CONVERT_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_NUMERIC_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_STRING_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_SYSTEM_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_TIMEDATE_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_CONVERT_BIGINT,
			Odbc32.SQL_INFO.SQL_CONVERT_BINARY,
			Odbc32.SQL_INFO.SQL_CONVERT_BIT,
			Odbc32.SQL_INFO.SQL_CONVERT_CHAR,
			Odbc32.SQL_INFO.SQL_CONVERT_DATE,
			Odbc32.SQL_INFO.SQL_CONVERT_DECIMAL,
			Odbc32.SQL_INFO.SQL_CONVERT_DOUBLE,
			Odbc32.SQL_INFO.SQL_CONVERT_FLOAT,
			Odbc32.SQL_INFO.SQL_CONVERT_INTEGER,
			Odbc32.SQL_INFO.SQL_CONVERT_LONGVARCHAR,
			Odbc32.SQL_INFO.SQL_CONVERT_NUMERIC,
			Odbc32.SQL_INFO.SQL_CONVERT_REAL,
			Odbc32.SQL_INFO.SQL_CONVERT_SMALLINT,
			Odbc32.SQL_INFO.SQL_CONVERT_TIME,
			Odbc32.SQL_INFO.SQL_CONVERT_TIMESTAMP,
			Odbc32.SQL_INFO.SQL_CONVERT_TINYINT,
			Odbc32.SQL_INFO.SQL_CONVERT_VARBINARY,
			Odbc32.SQL_INFO.SQL_CONVERT_VARCHAR,
			Odbc32.SQL_INFO.SQL_CONVERT_LONGVARBINARY,
			Odbc32.SQL_INFO.SQL_TXN_ISOLATION_OPTION,
			Odbc32.SQL_INFO.SQL_ODBC_SQL_OPT_IEF,
			Odbc32.SQL_INFO.SQL_CORRELATION_NAME,
			Odbc32.SQL_INFO.SQL_NON_NULLABLE_COLUMNS,
			Odbc32.SQL_INFO.SQL_DRIVER_ODBC_VER,
			Odbc32.SQL_INFO.SQL_LOCK_TYPES,
			Odbc32.SQL_INFO.SQL_POS_OPERATIONS,
			Odbc32.SQL_INFO.SQL_POSITIONED_STATEMENTS,
			Odbc32.SQL_INFO.SQL_GETDATA_EXTENSIONS,
			Odbc32.SQL_INFO.SQL_BOOKMARK_PERSISTENCE,
			Odbc32.SQL_INFO.SQL_STATIC_SENSITIVITY,
			Odbc32.SQL_INFO.SQL_FILE_USAGE,
			Odbc32.SQL_INFO.SQL_NULL_COLLATION,
			Odbc32.SQL_INFO.SQL_ALTER_TABLE,
			Odbc32.SQL_INFO.SQL_COLUMN_ALIAS,
			Odbc32.SQL_INFO.SQL_GROUP_BY,
			Odbc32.SQL_INFO.SQL_KEYWORDS,
			Odbc32.SQL_INFO.SQL_ORDER_BY_COLUMNS_IN_SELECT,
			Odbc32.SQL_INFO.SQL_SCHEMA_USAGE,
			Odbc32.SQL_INFO.SQL_CATALOG_USAGE,
			Odbc32.SQL_INFO.SQL_QUOTED_IDENTIFIER_CASE,
			Odbc32.SQL_INFO.SQL_SUBQUERIES,
			Odbc32.SQL_INFO.SQL_UNION_STATEMENT,
			Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_GROUP_BY,
			Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_INDEX,
			Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_ORDER_BY,
			Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_SELECT,
			Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_TABLE,
			Odbc32.SQL_INFO.SQL_MAX_INDEX_SIZE,
			Odbc32.SQL_INFO.SQL_MAX_ROW_SIZE_INCLUDES_LONG,
			Odbc32.SQL_INFO.SQL_MAX_ROW_SIZE,
			Odbc32.SQL_INFO.SQL_MAX_STATEMENT_LEN,
			Odbc32.SQL_INFO.SQL_MAX_TABLES_IN_SELECT,
			Odbc32.SQL_INFO.SQL_MAX_USER_NAME_LEN,
			Odbc32.SQL_INFO.SQL_MAX_CHAR_LITERAL_LEN,
			Odbc32.SQL_INFO.SQL_TIMEDATE_ADD_INTERVALS,
			Odbc32.SQL_INFO.SQL_TIMEDATE_DIFF_INTERVALS,
			Odbc32.SQL_INFO.SQL_NEED_LONG_DATA_LEN,
			Odbc32.SQL_INFO.SQL_MAX_BINARY_LITERAL_LEN,
			Odbc32.SQL_INFO.SQL_LIKE_ESCAPE_CLAUSE,
			Odbc32.SQL_INFO.SQL_CATALOG_LOCATION,
			Odbc32.SQL_INFO.SQL_OJ_CAPABILITIES,
			Odbc32.SQL_INFO.SQL_ACTIVE_ENVIRONMENTS,
			Odbc32.SQL_INFO.SQL_ALTER_DOMAIN,
			Odbc32.SQL_INFO.SQL_SQL_CONFORMANCE,
			Odbc32.SQL_INFO.SQL_DATETIME_LITERALS,
			Odbc32.SQL_INFO.SQL_BATCH_ROW_COUNT,
			Odbc32.SQL_INFO.SQL_BATCH_SUPPORT,
			Odbc32.SQL_INFO.SQL_CONVERT_WCHAR,
			Odbc32.SQL_INFO.SQL_CONVERT_INTERVAL_DAY_TIME,
			Odbc32.SQL_INFO.SQL_CONVERT_INTERVAL_YEAR_MONTH,
			Odbc32.SQL_INFO.SQL_CONVERT_WLONGVARCHAR,
			Odbc32.SQL_INFO.SQL_CONVERT_WVARCHAR,
			Odbc32.SQL_INFO.SQL_CREATE_ASSERTION,
			Odbc32.SQL_INFO.SQL_CREATE_CHARACTER_SET,
			Odbc32.SQL_INFO.SQL_CREATE_COLLATION,
			Odbc32.SQL_INFO.SQL_CREATE_DOMAIN,
			Odbc32.SQL_INFO.SQL_CREATE_SCHEMA,
			Odbc32.SQL_INFO.SQL_CREATE_TABLE,
			Odbc32.SQL_INFO.SQL_CREATE_TRANSLATION,
			Odbc32.SQL_INFO.SQL_CREATE_VIEW,
			Odbc32.SQL_INFO.SQL_DRIVER_HDESC,
			Odbc32.SQL_INFO.SQL_DROP_ASSERTION,
			Odbc32.SQL_INFO.SQL_DROP_CHARACTER_SET,
			Odbc32.SQL_INFO.SQL_DROP_COLLATION,
			Odbc32.SQL_INFO.SQL_DROP_DOMAIN,
			Odbc32.SQL_INFO.SQL_DROP_SCHEMA,
			Odbc32.SQL_INFO.SQL_DROP_TABLE,
			Odbc32.SQL_INFO.SQL_DROP_TRANSLATION,
			Odbc32.SQL_INFO.SQL_DROP_VIEW,
			Odbc32.SQL_INFO.SQL_DYNAMIC_CURSOR_ATTRIBUTES1,
			Odbc32.SQL_INFO.SQL_DYNAMIC_CURSOR_ATTRIBUTES2,
			Odbc32.SQL_INFO.SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES1,
			Odbc32.SQL_INFO.SQL_FORWARD_ONLY_CURSOR_ATTRIBUTES2,
			Odbc32.SQL_INFO.SQL_INDEX_KEYWORDS,
			Odbc32.SQL_INFO.SQL_INFO_SCHEMA_VIEWS,
			Odbc32.SQL_INFO.SQL_KEYSET_CURSOR_ATTRIBUTES1,
			Odbc32.SQL_INFO.SQL_KEYSET_CURSOR_ATTRIBUTES2,
			Odbc32.SQL_INFO.SQL_ODBC_INTERFACE_CONFORMANCE,
			Odbc32.SQL_INFO.SQL_PARAM_ARRAY_ROW_COUNTS,
			Odbc32.SQL_INFO.SQL_PARAM_ARRAY_SELECTS,
			Odbc32.SQL_INFO.SQL_SQL92_DATETIME_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_SQL92_FOREIGN_KEY_DELETE_RULE,
			Odbc32.SQL_INFO.SQL_SQL92_FOREIGN_KEY_UPDATE_RULE,
			Odbc32.SQL_INFO.SQL_SQL92_GRANT,
			Odbc32.SQL_INFO.SQL_SQL92_NUMERIC_VALUE_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_SQL92_PREDICATES,
			Odbc32.SQL_INFO.SQL_SQL92_RELATIONAL_JOIN_OPERATORS,
			Odbc32.SQL_INFO.SQL_SQL92_REVOKE,
			Odbc32.SQL_INFO.SQL_SQL92_ROW_VALUE_CONSTRUCTOR,
			Odbc32.SQL_INFO.SQL_SQL92_STRING_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_SQL92_VALUE_EXPRESSIONS,
			Odbc32.SQL_INFO.SQL_STANDARD_CLI_CONFORMANCE,
			Odbc32.SQL_INFO.SQL_STATIC_CURSOR_ATTRIBUTES1,
			Odbc32.SQL_INFO.SQL_STATIC_CURSOR_ATTRIBUTES2,
			Odbc32.SQL_INFO.SQL_AGGREGATE_FUNCTIONS,
			Odbc32.SQL_INFO.SQL_DDL_INDEX,
			Odbc32.SQL_INFO.SQL_DM_VER,
			Odbc32.SQL_INFO.SQL_INSERT_STATEMENT,
			Odbc32.SQL_INFO.SQL_CONVERT_GUID,
			Odbc32.SQL_INFO.SQL_XOPEN_CLI_YEAR,
			Odbc32.SQL_INFO.SQL_CURSOR_SENSITIVITY,
			Odbc32.SQL_INFO.SQL_DESCRIBE_PARAMETER,
			Odbc32.SQL_INFO.SQL_CATALOG_NAME,
			Odbc32.SQL_INFO.SQL_COLLATION_SEQ,
			Odbc32.SQL_INFO.SQL_MAX_IDENTIFIER_LEN,
			Odbc32.SQL_INFO.SQL_ASYNC_MODE,
			Odbc32.SQL_INFO.SQL_MAX_ASYNC_CONCURRENT_STATEMENTS
		};

		// Token: 0x0400177A RID: 6010
		private static readonly HashSet<Odbc32.SQL_INFO> privilegedAllowedOverride = new HashSet<Odbc32.SQL_INFO> { Odbc32.SQL_INFO.SQL_IDENTIFIER_QUOTE_CHAR };

		// Token: 0x0400177B RID: 6011
		private readonly bool isPrivileged;

		// Token: 0x0400177C RID: 6012
		private readonly ILifetimeService lifetimeService;

		// Token: 0x0400177D RID: 6013
		private readonly Value sqlGetTypeInfo;

		// Token: 0x0400177E RID: 6014
		private readonly Dictionary<Odbc32.SQL_INFO, Value> sqlGetInfo;

		// Token: 0x0400177F RID: 6015
		private readonly RecordValue sqlGetFunctions;

		// Token: 0x04001780 RID: 6016
		private readonly Value sqlGetColumns;

		// Token: 0x04001781 RID: 6017
		private readonly Value sqlGetTables;

		// Token: 0x02000682 RID: 1666
		private class UserOverrideOdbcConnection : OdbcDelegatingConnection
		{
			// Token: 0x06003443 RID: 13379 RVA: 0x000A8789 File Offset: 0x000A6989
			public UserOverrideOdbcConnection(UserOverrideOdbcService service, IOdbcConnection connection)
				: base(connection)
			{
				this.service = service;
			}

			// Token: 0x06003444 RID: 13380 RVA: 0x000A879C File Offset: 0x000A699C
			public override int GetInfoInt32(Odbc32.SQL_INFO infoType)
			{
				Value value;
				if (this.service.sqlGetInfo.TryGetValue(infoType, out value))
				{
					return value.AsInteger32;
				}
				return base.GetInfoInt32(infoType);
			}

			// Token: 0x06003445 RID: 13381 RVA: 0x000A87CC File Offset: 0x000A69CC
			public override string GetInfoString(Odbc32.SQL_INFO infoType)
			{
				Value value;
				if (this.service.sqlGetInfo.TryGetValue(infoType, out value))
				{
					return value.AsString;
				}
				return base.GetInfoString(infoType);
			}

			// Token: 0x06003446 RID: 13382 RVA: 0x000A87FC File Offset: 0x000A69FC
			public override bool GetFunctions(Odbc32.SQL_API functionId)
			{
				Value value;
				if (this.service.sqlGetFunctions.TryGetValue(functionId.ToString(), out value) && !value.IsNull)
				{
					return value.AsBoolean;
				}
				return base.GetFunctions(functionId);
			}

			// Token: 0x06003447 RID: 13383 RVA: 0x000A8840 File Offset: 0x000A6A40
			public override IDataReaderWithTableSchema GetTypeInfo(short dataType)
			{
				if (this.service.sqlGetTypeInfo.IsFunction)
				{
					TableValue tableValue = DataReaderTableValue.New(this.service.lifetimeService, () => this.<>n__0(dataType));
					TableValue asTable = this.service.sqlGetTypeInfo.AsFunction.Invoke(tableValue).AsTable;
					return new TableDataReader(asTable.Type.AsTableType, new TableValueDataReader(asTable, true), null);
				}
				if (this.service.sqlGetTypeInfo.IsNull)
				{
					return base.GetTypeInfo(dataType);
				}
				TableValue asTable2 = this.service.sqlGetTypeInfo.AsTable;
				return new TableDataReader(asTable2.Type.AsTableType, new TableValueDataReader(asTable2, true), null);
			}

			// Token: 0x06003448 RID: 13384 RVA: 0x000A8910 File Offset: 0x000A6B10
			public override IDataReaderWithTableSchema GetColumns(string catalogName, string schemaName, string tableName)
			{
				if (!this.service.sqlGetColumns.IsNull)
				{
					TableValue tableValue = DataReaderTableValue.New(this.service.lifetimeService, () => this.<>n__1(catalogName, schemaName, tableName));
					Value value = this.service.sqlGetColumns.AsFunction.Invoke(TextValue.NewOrNull(catalogName), TextValue.NewOrNull(schemaName), TextValue.New(tableName), Value.Null, tableValue);
					if (!value.IsNull)
					{
						TableValueDataReader tableValueDataReader = new TableValueDataReader(value.AsTable, true);
						return new TableDataReader(value.Type.AsTableType, tableValueDataReader, null);
					}
				}
				return base.GetColumns(catalogName, schemaName, tableName);
			}

			// Token: 0x06003449 RID: 13385 RVA: 0x000A89F0 File Offset: 0x000A6BF0
			public override IDataReaderWithTableSchema GetTables(string catalogName, string schemaName, string tableName, string tableType)
			{
				if (!this.service.sqlGetTables.IsNull)
				{
					TableValue tableValue = DataReaderTableValue.New(this.service.lifetimeService, () => this.<>n__2(catalogName, schemaName, tableName, tableType));
					Value value = this.service.sqlGetTables.AsFunction.Invoke(TextValue.NewOrNull(catalogName), TextValue.NewOrNull(schemaName), TextValue.NewOrNull(tableName), TextValue.NewOrNull(tableType), tableValue);
					if (!value.IsNull)
					{
						TableValueDataReader tableValueDataReader = new TableValueDataReader(value.AsTable, true);
						return new TableDataReader(value.Type.AsTableType, tableValueDataReader, null);
					}
				}
				return base.GetTables(catalogName, schemaName, tableName, tableType);
			}

			// Token: 0x04001782 RID: 6018
			private readonly UserOverrideOdbcService service;
		}
	}
}
