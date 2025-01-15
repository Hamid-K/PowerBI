using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Teradata
{
	// Token: 0x020002D6 RID: 726
	internal class TeradataEnvironment : DbEnvironment
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00046BEC File Offset: 0x00044DEC
		private TeradataEnvironment(IEngineHost host, string server, Value options)
			: base(host, DatabaseResource.New("Teradata", server, null), "Teradata", server, null, options, null, null)
		{
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x00046C16 File Offset: 0x00044E16
		public static TeradataEnvironment Create(IEngineHost host, string server, Value options)
		{
			return new TeradataEnvironment(host, server, options);
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00046C20 File Offset: 0x00044E20
		public static string ClientSoftwareNotFoundExceptionMessage
		{
			get
			{
				return DbEnvironment.GetClientSoftwareNotFoundExceptionMessage(".NET Data Provider for Teradata", "https://go.microsoft.com/fwlink/?LinkId=278886");
			}
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00046C36 File Offset: 0x00044E36
		public static Message1 ProviderMissingErrorMessage
		{
			get
			{
				return Strings.DatabaseProviderMissingExceptionMessage("Teradata.Client.Provider");
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x00046C42 File Offset: 0x00044E42
		public static Message2 ProviderConfigurationErrorMessage(string message)
		{
			return Strings.DatabaseProviderConfigurationErrorExceptionMessage("Teradata.Client.Provider", message);
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x00046C4F File Offset: 0x00044E4F
		public static Message3 ProviderUpgradeMessage(string message)
		{
			return Strings.DatabaseProviderUpgradeMessage("Teradata.Client.Provider", message, "https://go.microsoft.com/fwlink/?LinkId=278886");
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00046C61 File Offset: 0x00044E61
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return TeradataAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x00046C6B File Offset: 0x00044E6B
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			TeradataAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x00046C75 File Offset: 0x00044E75
		protected override SqlSettings LoadSqlSettings()
		{
			if (this.IsVersionGreaterOrEqual(14))
			{
				return TeradataEnvironment.sql14Settings;
			}
			return TeradataEnvironment.sqlSettings;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x00046C8C File Offset: 0x00044E8C
		private bool IsVersionGreaterOrEqual(int majorVersionIn)
		{
			string serverVersion = base.ServerVersion;
			if (serverVersion != null)
			{
				string[] array = serverVersion.Split(new char[] { '.' });
				int num = 0;
				if (array.Length >= 2 && int.TryParse(array[0], out num))
				{
					return num >= majorVersionIn;
				}
			}
			return false;
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00046CD2 File Offset: 0x00044ED2
		protected override string ProviderName
		{
			get
			{
				return "Teradata.Client.Provider";
			}
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00046CD9 File Offset: 0x00044ED9
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/?LinkId=278886";
			}
		}

		// Token: 0x17000D4C RID: 3404
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x00046CE0 File Offset: 0x00044EE0
		protected override string ProviderLibraryName
		{
			get
			{
				return ".NET Data Provider for Teradata";
			}
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00046CE7 File Offset: 0x00044EE7
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return TeradataModule.OptionRecord;
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00046CF0 File Offset: 0x00044EF0
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			if (exception.GetType().FullName == "Teradata.Client.Provider.TdException")
			{
				PropertyInfo property = exception.GetType().GetProperty("Errors");
				if (property != null)
				{
					using (IEnumerator enumerator = ((IEnumerable)property.GetGetMethod().Invoke(exception, new object[0])).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							PropertyInfo property2 = obj.GetType().GetProperty("Number");
							if (property2 != null)
							{
								using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
								{
									hostTrace.Add("Exception", exception, true);
									int num = (int)property2.GetGetMethod().Invoke(obj, new object[0]);
									hostTrace.Add("ExceptionNumber", num, false);
									if (TeradataEnvironment.AuthorizationErrorNumbers.Contains(num))
									{
										ResourceExceptionKind resourceExceptionKind = ResourceExceptionKind.InvalidCredentials;
										hostTrace.Add("ResourceExceptionKind", resourceExceptionKind, false);
										return resourceExceptionKind;
									}
								}
							}
						}
						return ResourceExceptionKind.None;
					}
					return ResourceExceptionKind.None;
				}
				return ResourceExceptionKind.None;
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x00046E48 File Offset: 0x00045048
		public override Exception ProcessDbException(DbException dbException)
		{
			Type type = dbException.GetType();
			if (type.FullName == "Teradata.Client.Provider.TdException")
			{
				PropertyInfo property = type.GetProperty("Errors");
				if (property != null)
				{
					foreach (object obj in ((IEnumerable)property.GetGetMethod().Invoke(dbException, EmptyArray<object>.Instance)))
					{
						PropertyInfo property2 = obj.GetType().GetProperty("Number");
						if (property2 != null)
						{
							int num = (int)property2.GetValue(obj, null);
							if (num == TeradataEnvironment.UnsupportedTypeErrorCode)
							{
								return DataSourceException.NewDataSourceError<Message3>(base.Host, TeradataEnvironment.ProviderUpgradeMessage(dbException.Message), this.Resource, "ErrorCode", NumberValue.New(num), TypeValue.Number, dbException);
							}
						}
					}
				}
			}
			return base.ProcessDbException(dbException);
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00046F50 File Offset: 0x00045150
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return TeradataEnvironment.searchableTypes;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00046F57 File Offset: 0x00045157
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return TeradataEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x00046F5E File Offset: 0x0004515E
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(TeradataEnvironment.variableLengthTypes.Contains(this.GetNativeTypeName(dataType)));
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00046F78 File Offset: 0x00045178
		public override string GetNativeTypeName(string tdType)
		{
			if (tdType != null)
			{
				string text;
				if (TeradataEnvironment.TdTypeToName.TryGetValue(tdType, out text))
				{
					return text;
				}
				int num;
				if (!int.TryParse(tdType, out num))
				{
					return tdType;
				}
			}
			return null;
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x00046FA6 File Offset: 0x000451A6
		public override DataTable LoadSchemas(DbConnection connection)
		{
			return base.LoadData("Schemas", connection, "LOCK VIEW DBC.IndicesVX FOR ACCESS\r\nselect Trim(Trailing FROM DatabaseName) as \"SCHEMA_NAME\" \r\nfrom dbc.DatabasesVX\r\norder by \"SCHEMA_NAME\"");
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00046FBC File Offset: 0x000451BC
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			string[] array = new string[3];
			array[0] = schema;
			array[1] = table;
			string[] array2 = array;
			DataTable schema2 = connection.GetSchema("Columns", array2);
			schema2.Columns["PROVIDERDBTYPE"].ColumnName = "DATA_TYPE";
			return schema2;
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00046FFE File Offset: 0x000451FE
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Indexes", connection, "LOCK VIEW DBC.IndicesVX FOR ACCESS\r\nselect Coalesce(Trim(Trailing FROM i.IndexName), '') || '.' || Trim(Trailing FROM i.DatabaseName) || '.' || Trim(Trailing FROM i.TableName) || '.' || Trim(Trailing FROM i.IndexType) as \"INDEX_NAME\",\r\n       Trim(Trailing FROM i.ColumnName) as \"COLUMN_NAME\",\r\n       Trim(Trailing FROM i.ColumnPosition) as \"ORDINAL_POSITION\",\r\n       case when Trim(Trailing FROM i.IndexType) in ('U', 'S') then 'N' else 'Y' end as \"PRIMARY_KEY\"\r\nfrom DBC.IndicesVX i\r\nwhere Trim(Trailing FROM i.DatabaseName) not in ('DBC', 'SYSLIB', 'SYSUDTLIB') and\r\n      i.UniqueFlag = 'Y' and\r\n      (i.IndexName is not null or i.IndexType <> 'S') and\r\n      Trim(Trailing FROM i.DatabaseName) = {0} and\r\n      Trim(Trailing FROM i.TableName) = {1}\r\norder by \"INDEX_NAME\", ColumnPosition", new string[] { schema, table });
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0004701F File Offset: 0x0004521F
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysParent", connection, "LOCK VIEW DBC.ALL_RI_PARENTSX FOR ACCESS\r\nselect Trim(Trailing FROM ParentKeyColumn) AS \"PK_COLUMN_NAME\",\r\n       Trim(Trailing FROM ChildDB) AS \"FK_TABLE_SCHEMA\",\r\n       Trim(Trailing FROM ChildTable) AS \"FK_TABLE_NAME\",\r\n       Trim(Trailing FROM ChildKeyColumn) AS \"FK_COLUMN_NAME\",\r\n       Cast((ROW_NUMBER() OVER (PARTITION By ChildDb, ChildTable, IndexId Order by Cast(ChildDb AS CASESPECIFIC), Cast(ChildTable AS CASESPECIFIC), IndexId)) as SmallInt) AS \"ORDINAL\",\r\n       Cast(IndexID as varchar(5)) || '_' || Coalesce(Trim(Trailing FROM IndexName), '') || '_' || Trim(Trailing FROM ParentDB) || '_' || Trim(Trailing FROM ParentTable) || '_' || Trim(Trailing FROM ChildDB) || '_' || Trim(Trailing FROM ChildTable) AS \"FK_NAME\"\r\nfrom DBC.ALL_RI_PARENTSVX\r\nwhere Trim(Trailing FROM ParentDB) = {0} and Trim(Trailing FROM ParentTable) = {1}\r\norder by \"FK_NAME\", \"ORDINAL\"", new string[] { schema, table });
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x00047040 File Offset: 0x00045240
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysChild", connection, "LOCK VIEW DBC.ALL_RI_PARENTSX FOR ACCESS\r\nselect Trim(Trailing FROM ParentDB) AS \"PK_TABLE_SCHEMA\",\r\n       Trim(Trailing FROM ParentTable) AS \"PK_TABLE_NAME\",\r\n       Trim(Trailing FROM ParentKeyColumn) AS \"PK_COLUMN_NAME\",\r\n       Trim(Trailing FROM ChildKeyColumn) AS \"FK_COLUMN_NAME\",\r\n       Cast((ROW_NUMBER() OVER (PARTITION By ChildDb, ChildTable, IndexId Order by Cast(ChildDb AS CASESPECIFIC), Cast(ChildTable AS CASESPECIFIC), IndexId)) as SmallInt) AS \"ORDINAL\",\r\n       Cast(IndexID as varchar(5)) || '_' || Coalesce(Trim(Trailing FROM IndexName), '') || '_' || Trim(Trailing FROM ParentDB) || '_' || Trim(Trailing FROM ParentTable) || '_' || Trim(Trailing FROM ChildDB) || '_' || Trim(Trailing FROM ChildTable) AS \"FK_NAME\"\r\nfrom DBC.ALL_RI_PARENTSVX\r\nwhere Trim(Trailing FROM ChildDB) = {0} and Trim(Trailing FROM ChildTable) = {1}\r\norder by \"FK_NAME\", \"ORDINAL\"", new string[] { schema, table });
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x00047061 File Offset: 0x00045261
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ResourceInformation", connection, "select Sum(CurrentPerm) AS \"TOTAL_BYTES\" \r\nfrom DBC.ALLSPACE \r\nwhere DatabaseName = {0} AND TableName = {1};", new string[] { schema, table });
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x00047082 File Offset: 0x00045282
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new TeradataEnvironment.TeradataConnectionStringBuilder(base.Host, this.Resource);
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x00047098 File Offset: 0x00045298
		public override TableValue CreateCatalogTableValue(IEngineHost host, string schema)
		{
			TableValue tableValue = base.CreateCatalogTableValue(host, schema);
			return new QueryTableValue(new DbEnvironment.UpdatableCatalogQuery(this, tableValue, schema), tableValue.Type);
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x000470C1 File Offset: 0x000452C1
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			TeradataAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x000470CC File Offset: 0x000452CC
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return TeradataEnvironment.TimeType;
			case ValueKind.Date:
				return TeradataEnvironment.DateType;
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
				return TeradataEnvironment.DatetimeType;
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return TeradataEnvironment.ByteType;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return DbEnvironment.Int8Type;
				}
				if (type.Equals(TypeValue.Int16))
				{
					return TeradataEnvironment.SmallIntType;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return TeradataEnvironment.IntegerType;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return TeradataEnvironment.BigIntType;
				}
				if (type.Equals(TypeValue.Single))
				{
					return TeradataEnvironment.SingleType;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return DbEnvironment.DecimalType;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return DbEnvironment.CurrencyType;
				}
				return TeradataEnvironment.DoubleType;
			case ValueKind.Logical:
				return TeradataEnvironment.BoolType;
			case ValueKind.Text:
			{
				long num = type.Facets.MaxLength.GetValueOrDefault(256L);
				bool? flag = type.Facets.IsVariableLength;
				bool flag2 = false;
				string text = (((flag.GetValueOrDefault() == flag2) & (flag != null)) ? string.Format(CultureInfo.InvariantCulture, "Char({0})", num) : string.Format(CultureInfo.InvariantCulture, "VarChar({0})", num));
				return new SqlDataType(type, new ConstantSqlString(text));
			}
			case ValueKind.Binary:
			{
				long num = type.Facets.MaxLength.GetValueOrDefault(1024L);
				bool? flag = type.Facets.IsVariableLength;
				bool flag2 = false;
				string text2 = (((flag.GetValueOrDefault() == flag2) & (flag != null)) ? string.Format(CultureInfo.InvariantCulture, "Byte({0})", num) : string.Format(CultureInfo.InvariantCulture, "VarByte({0})", num));
				return new SqlDataType(type, new ConstantSqlString(text2));
			}
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x040009AC RID: 2476
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=278886";

		// Token: 0x040009AD RID: 2477
		private const string ClientLibraryName = ".NET Data Provider for Teradata";

		// Token: 0x040009AE RID: 2478
		public const string DataSourceName = "Teradata";

		// Token: 0x040009AF RID: 2479
		public const string LdapAuthentication = "Ldap";

		// Token: 0x040009B0 RID: 2480
		public const string LdapUsername = "Username";

		// Token: 0x040009B1 RID: 2481
		public const string LdapPassword = "Password";

		// Token: 0x040009B2 RID: 2482
		private const string ErrorCode = "ErrorCode";

		// Token: 0x040009B3 RID: 2483
		private static readonly int UnsupportedTypeErrorCode = 100047;

		// Token: 0x040009B4 RID: 2484
		private static readonly SqlDataType SingleType = new SqlDataType(TypeValue.Single, new ConstantSqlString("FLOAT"));

		// Token: 0x040009B5 RID: 2485
		private static readonly SqlDataType DoubleType = new SqlDataType(TypeValue.Double, new ConstantSqlString("DOUBLE PRECISION"));

		// Token: 0x040009B6 RID: 2486
		private static readonly SqlDataType DateType = new SqlDataType(TypeValue.Date, new ConstantSqlString("DATE"));

		// Token: 0x040009B7 RID: 2487
		private static readonly SqlDataType DatetimeType = new SqlDataType(TypeValue.DateTime, new ConstantSqlString("Timestamp"));

		// Token: 0x040009B8 RID: 2488
		private static readonly SqlDataType TimeType = new SqlDataType(TypeValue.Time, new ConstantSqlString("Time"));

		// Token: 0x040009B9 RID: 2489
		private static readonly SqlDataType BoolType = new SqlDataType(TypeValue.Logical, new ConstantSqlString("ByteInt"));

		// Token: 0x040009BA RID: 2490
		private static readonly SqlDataType ByteType = new SqlDataType(TypeValue.Int8, new ConstantSqlString("Byte"));

		// Token: 0x040009BB RID: 2491
		private static readonly SqlDataType SmallIntType = new SqlDataType(TypeValue.Int16, new ConstantSqlString("SmallInt"));

		// Token: 0x040009BC RID: 2492
		private static readonly SqlDataType IntegerType = new SqlDataType(TypeValue.Int32, new ConstantSqlString("Integer"));

		// Token: 0x040009BD RID: 2493
		private static readonly SqlDataType BigIntType = new SqlDataType(TypeValue.Int64, new ConstantSqlString("BigInt"));

		// Token: 0x040009BE RID: 2494
		private const string BinaryType = "Byte({0})";

		// Token: 0x040009BF RID: 2495
		private const string VarbinaryType = "VarByte({0})";

		// Token: 0x040009C0 RID: 2496
		private const string NVarcharType = "VarChar({0})";

		// Token: 0x040009C1 RID: 2497
		private const string NCharType = "Char({0})";

		// Token: 0x040009C2 RID: 2498
		private const long DefaultBinaryMaxLength = 1024L;

		// Token: 0x040009C3 RID: 2499
		private const long DefaultTextMaxLength = 256L;

		// Token: 0x040009C4 RID: 2500
		private const string StandardLiteralQuote = "'";

		// Token: 0x040009C5 RID: 2501
		private static readonly HashSet<int> AuthorizationErrorNumbers = new HashSet<int> { 3003, 3004, 8017, 115022 };

		// Token: 0x040009C6 RID: 2502
		private static readonly SqlSettings sql14Settings = new SqlSettings
		{
			MaxIdentifierLength = 128,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			RequiresAsForFromAlias = false,
			DateTimePrefix = "TIMESTAMP'",
			DatePrefix = "DATE'",
			DateSuffix = "'",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsCaseExpression = true,
			TimePrefix = "'",
			IntervalPrefix = "INTERVAL '",
			IntervalSuffix = "' DAY TO SECOND",
			BinaryPrefix = "'",
			BinarySuffix = "'xb",
			CreateTable = SqlLanguageStrings.CreateMultisetTableSqlString,
			DeleteCommand = SqlLanguageStrings.DeleteFromSqlString,
			IsMaxPrecision = false,
			SupportsIntervalConstants = true
		};

		// Token: 0x040009C7 RID: 2503
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			MaxIdentifierLength = 30,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			RequiresAsForFromAlias = false,
			DateTimePrefix = "TIMESTAMP'",
			DatePrefix = "DATE'",
			DateSuffix = "'",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsCaseExpression = true,
			TimePrefix = "'",
			IntervalPrefix = "INTERVAL '",
			IntervalSuffix = "' DAY TO SECOND",
			BinaryPrefix = "'",
			BinarySuffix = "'xb",
			CreateTable = SqlLanguageStrings.CreateMultisetTableSqlString,
			DeleteCommand = SqlLanguageStrings.DeleteFromSqlString,
			IsMaxPrecision = false,
			SupportsIntervalConstants = true
		};

		// Token: 0x040009C8 RID: 2504
		public const string TeradataProviderName = "Teradata.Client.Provider";

		// Token: 0x040009C9 RID: 2505
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"90", "110", "120", "130", "150", "160", "170", "180", "190", "200",
			"210", "220", "230", "240", "250", "260", "270", "280", "290", "300",
			"310", "320", "330", "340", "350", "360", "370", "380", "390", "400",
			"410", "450", "440", "420", "430"
		};

		// Token: 0x040009CA RID: 2506
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"90",
				TypeValue.Int64
			},
			{
				"100",
				TypeValue.Binary
			},
			{
				"110",
				TypeValue.Binary
			},
			{
				"120",
				TypeValue.Int16
			},
			{
				"130",
				TypeValue.Text
			},
			{
				"140",
				TypeValue.Text
			},
			{
				"150",
				TypeValue.Date
			},
			{
				"160",
				TypeValue.Decimal
			},
			{
				"170",
				TypeValue.Double
			},
			{
				"180",
				TypeValue.Text
			},
			{
				"190",
				TypeValue.Int32
			},
			{
				"200",
				TypeValue.Duration
			},
			{
				"210",
				TypeValue.Duration
			},
			{
				"220",
				TypeValue.Duration
			},
			{
				"230",
				TypeValue.Duration
			},
			{
				"240",
				TypeValue.Duration
			},
			{
				"250",
				TypeValue.Duration
			},
			{
				"260",
				TypeValue.Duration
			},
			{
				"270",
				TypeValue.Duration
			},
			{
				"280",
				TypeValue.Duration
			},
			{
				"290",
				TypeValue.Duration
			},
			{
				"300",
				TypeValue.Any
			},
			{
				"310",
				TypeValue.Any
			},
			{
				"320",
				TypeValue.Any
			},
			{
				"330",
				TypeValue.Int16
			},
			{
				"340",
				TypeValue.Time
			},
			{
				"350",
				TypeValue.Any
			},
			{
				"360",
				TypeValue.DateTime
			},
			{
				"370",
				TypeValue.Any
			},
			{
				"380",
				TypeValue.Binary
			},
			{
				"390",
				TypeValue.Text
			},
			{
				"400",
				TypeValue.Text
			},
			{
				"410",
				TypeValue.Any
			},
			{
				"450",
				TypeValue.Any
			},
			{
				"440",
				TypeValue.Any
			},
			{
				"420",
				TypeValue.Any
			},
			{
				"430",
				TypeValue.Any
			}
		};

		// Token: 0x040009CB RID: 2507
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Blob", "Clob", "VarByte", "VarChar", "VarGraphic" };

		// Token: 0x040009CC RID: 2508
		private static readonly Dictionary<string, string> TdTypeToName = new Dictionary<string, string>
		{
			{ "0xffff", "AnyType" },
			{ "90", "BigInt" },
			{ "100", "Blob" },
			{ "110", "Byte" },
			{ "120", "ByteInt" },
			{ "130", "Char" },
			{ "140", "Clob" },
			{ "150", "Date" },
			{ "160", "Decimal" },
			{ "170", "Double" },
			{ "180", "Graphic" },
			{ "190", "Integer" },
			{ "200", "IntervalDay" },
			{ "210", "IntervalDayToHour" },
			{ "220", "IntervalDayToMinute" },
			{ "230", "IntervalDayToSecond" },
			{ "240", "IntervalHour" },
			{ "250", "IntervalHourToMinute" },
			{ "260", "IntervalHourToSecond" },
			{ "270", "IntervalMinute" },
			{ "280", "IntervalMinuteToSecond" },
			{ "290", "IntervalSecond" },
			{ "300", "IntervalYear" },
			{ "310", "IntervalYearToMonth" },
			{ "320", "IntervalMonth" },
			{ "330", "SmallInt" },
			{ "340", "Time" },
			{ "360", "Timestamp" },
			{ "370", "TimestampWithZone" },
			{ "380", "VarByte" },
			{ "390", "VarChar" },
			{ "400", "VarGraphic" },
			{ "410", "PeriodDate" },
			{ "420", "PeriodTime" },
			{ "430", "PeriodTimeWithTimeZone" },
			{ "440", "PeriodTimestamp" },
			{ "450", "PeriodTimestampWithTimeZone" },
			{ "460", "Number" },
			{ "480", "Xml" },
			{ "500", "Json" }
		};

		// Token: 0x020002D7 RID: 727
		private sealed class TeradataConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06001CEE RID: 7406 RVA: 0x00047C79 File Offset: 0x00045E79
			public TeradataConnectionStringBuilder(IEngineHost host, IResource resource)
				: base(host, resource)
			{
			}

			// Token: 0x17000D50 RID: 3408
			// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00047C83 File Offset: 0x00045E83
			protected override string UserNameKey
			{
				get
				{
					return "User ID";
				}
			}

			// Token: 0x17000D51 RID: 3409
			// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17000D52 RID: 3410
			// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00047C91 File Offset: 0x00045E91
			protected override string ServerKey
			{
				get
				{
					return "Data Source";
				}
			}

			// Token: 0x17000D53 RID: 3411
			// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00047C98 File Offset: 0x00045E98
			protected override string PortKey
			{
				get
				{
					return "Port Number";
				}
			}

			// Token: 0x17000D54 RID: 3412
			// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string DatabaseKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000D55 RID: 3413
			// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00047C9F File Offset: 0x00045E9F
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated Security";
				}
			}

			// Token: 0x17000D56 RID: 3414
			// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00047CA6 File Offset: 0x00045EA6
			protected override string EncryptKey
			{
				get
				{
					return "Data Encryption";
				}
			}

			// Token: 0x17000D57 RID: 3415
			// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00047CAD File Offset: 0x00045EAD
			protected override object AuthenticationTypeValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000D58 RID: 3416
			// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x00047CB5 File Offset: 0x00045EB5
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connection Timeout";
				}
			}

			// Token: 0x17000D59 RID: 3417
			// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00047CBC File Offset: 0x00045EBC
			protected override int? DefaultConnectionTimeout
			{
				get
				{
					return new int?(60);
				}
			}

			// Token: 0x06001CF9 RID: 7417 RVA: 0x00047CC8 File Offset: 0x00045EC8
			protected override bool Apply(ParameterizedCredential credential)
			{
				if (credential.Name == "Ldap")
				{
					this.builder["AuthenticationMechanism"] = "LDAP";
					this.builder[this.UserNameKey] = credential.GetValue("Username", string.Empty);
					this.builder[this.PasswordKey] = credential.GetValue("Password", string.Empty);
					return true;
				}
				return base.Apply(credential);
			}

			// Token: 0x06001CFA RID: 7418 RVA: 0x00047D47 File Offset: 0x00045F47
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					this.builder[this.EncryptKey] = true;
				}
				return true;
			}

			// Token: 0x06001CFB RID: 7419 RVA: 0x00047D69 File Offset: 0x00045F69
			protected override void AddOptions()
			{
				this.builder["UseXViews"] = true;
				this.builder["SESSIONCHARACTERSET"] = "UTF16";
			}
		}
	}
}
