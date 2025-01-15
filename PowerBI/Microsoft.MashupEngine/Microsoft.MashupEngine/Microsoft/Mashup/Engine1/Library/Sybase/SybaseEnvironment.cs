using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Sybase
{
	// Token: 0x02000370 RID: 880
	internal class SybaseEnvironment : DbEnvironment
	{
		// Token: 0x06001F23 RID: 7971 RVA: 0x0005054C File Offset: 0x0004E74C
		static SybaseEnvironment()
		{
			SybaseEnvironment.sybaseTableTypes.Add("USER", new TableType("USER", "Table"));
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00050B20 File Offset: 0x0004ED20
		private SybaseEnvironment(IEngineHost host, string server, string database, Value options)
			: base(host, DatabaseResource.New("Sybase", server, database), "Sybase SQL Anywhere", server, database, options, null, null)
		{
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00050B4B File Offset: 0x0004ED4B
		public static SybaseEnvironment Create(IEngineHost host, string server, string database, Value options)
		{
			return new SybaseEnvironment(host, server, database, options);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00050B56 File Offset: 0x0004ED56
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return SybaseAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06001F27 RID: 7975 RVA: 0x00050B60 File Offset: 0x0004ED60
		protected override IDictionary<string, TableType> TableTypes
		{
			get
			{
				return SybaseEnvironment.sybaseTableTypes;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x00050B67 File Offset: 0x0004ED67
		public static string ClientSoftwareNotFoundExceptionMessage
		{
			get
			{
				return DbEnvironment.GetClientSoftwareNotFoundExceptionMessage("Sybase SQL Anywhere Client Software", "https://go.microsoft.com/fwlink/?LinkId=324846");
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06001F29 RID: 7977 RVA: 0x00050B7D File Offset: 0x0004ED7D
		public static string ProviderMissingErrorMessage
		{
			get
			{
				return Strings.DatabaseProviderMissingExceptionMessage("Sap.Data.SQLAnywhere");
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00050B8E File Offset: 0x0004ED8E
		public static string ProviderConfigurationErrorMessage(string message)
		{
			return Strings.DatabaseProviderConfigurationErrorExceptionMessage("Sap.Data.SQLAnywhere", message);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00050BA0 File Offset: 0x0004EDA0
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			SybaseAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00050BAA File Offset: 0x0004EDAA
		protected override SqlSettings LoadSqlSettings()
		{
			return SybaseEnvironment.sqlSettings;
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00050BB4 File Offset: 0x0004EDB4
		private bool UsesOldProvider
		{
			get
			{
				if (SybaseEnvironment.usesOldProvider == null)
				{
					using (IHostTrace hostTrace = base.Tracer.CreateTrace("UsesOldProvider", TraceEventType.Information))
					{
						if (base.IsProviderInstalled("Sap.Data.SQLAnywhere"))
						{
							SybaseEnvironment.usesOldProvider = new bool?(false);
							hostTrace.Add("DbProviderName", "Sap.Data.SQLAnywhere", false);
						}
						else if (base.IsProviderInstalled("iAnywhere.Data.SQLAnywhere"))
						{
							SybaseEnvironment.usesOldProvider = new bool?(true);
							hostTrace.Add("DbProviderName", "iAnywhere.Data.SQLAnywhere", false);
						}
						else
						{
							SybaseEnvironment.usesOldProvider = new bool?(false);
						}
					}
				}
				return SybaseEnvironment.usesOldProvider.Value;
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00050C68 File Offset: 0x0004EE68
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			string fullName = exception.GetType().FullName;
			if (fullName == "Sap.Data.SQLAnywhere.SAException" || fullName == "iAnywhere.Data.SQLAnywhere.SAException")
			{
				PropertyInfo property = exception.GetType().GetProperty("Errors");
				if (property != null)
				{
					using (IEnumerator enumerator = ((IEnumerable)property.GetGetMethod().Invoke(exception, new object[0])).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							PropertyInfo property2 = obj.GetType().GetProperty("NativeError");
							if (property2 != null)
							{
								using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
								{
									int num = (int)property2.GetGetMethod().Invoke(obj, new object[0]);
									hostTrace.Add("Exception", exception, true);
									hostTrace.Add("ExceptionNumber", num, false);
									ResourceExceptionKind resourceExceptionKind;
									if (num == -1070 || num == -313 || num == -103)
									{
										resourceExceptionKind = ResourceExceptionKind.InvalidCredentials;
									}
									else
									{
										resourceExceptionKind = ResourceExceptionKind.None;
									}
									hostTrace.Add("ResourceExceptionKind", resourceExceptionKind.ToString(), false);
									return resourceExceptionKind;
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

		// Token: 0x06001F2F RID: 7983 RVA: 0x00050DE4 File Offset: 0x0004EFE4
		public override DbDataReaderWithTableSchema WrapDbDataReader(DbDataReaderWithTableSchema reader)
		{
			reader = base.WrapDbDataReader(reader);
			reader = UnsignedPromotingDbDataReader.New(reader);
			return reader;
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00050DF8 File Offset: 0x0004EFF8
		protected override string ProviderName
		{
			get
			{
				if (!this.UsesOldProvider)
				{
					return "Sap.Data.SQLAnywhere";
				}
				return "iAnywhere.Data.SQLAnywhere";
			}
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06001F31 RID: 7985 RVA: 0x00050E0D File Offset: 0x0004F00D
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/?LinkId=324846";
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06001F32 RID: 7986 RVA: 0x00050E14 File Offset: 0x0004F014
		protected override string ProviderLibraryName
		{
			get
			{
				return "Sybase SQL Anywhere Client Software";
			}
		}

		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00050E1B File Offset: 0x0004F01B
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return SybaseModule.OptionRecord;
			}
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00050E22 File Offset: 0x0004F022
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return SybaseEnvironment.searchableTypes;
			}
		}

		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x00050E29 File Offset: 0x0004F029
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return SybaseEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00050E30 File Offset: 0x0004F030
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(SybaseEnvironment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00050E42 File Offset: 0x0004F042
		public override DataTable LoadSchemas(DbConnection connection)
		{
			return base.LoadData("Schemas", connection, "select user_name as SCHEMA_NAME from SYS.SYSUSER order by user_name");
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00050E58 File Offset: 0x0004F058
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			string[] array = new string[3];
			array[0] = schema;
			array[1] = table;
			string[] array2 = array;
			return connection.GetSchema("Columns", array2);
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00050E80 File Offset: 0x0004F080
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Indexes", connection, "select i.index_name as INDEX_NAME, u.user_name as TABLE_SCHEMA, t.table_name as TABLE_NAME, c.cname as COLUMN_NAME, case when i.index_category = 1 then 'Y' else 'N' end as PRIMARY_KEY, ii.[sequence] as ORDINAL_POSITION\r\nfrom SYS.SYSIDX i key join SYS.SYSIDXCOL ii key join SYS.SYSCOLUMNS c key join SYS.SYSTABLE t key join SYS.SYSUSER u\r\nwhere i.[unique] in (1, 2)\r\n    and u.user_name = {0}\r\n    and t.table_name = {1}", new string[] { schema, table });
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00050EA1 File Offset: 0x0004F0A1
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysParent", connection, "select cp.column_name as PK_COLUMN_NAME,\r\n       uf.user_name as FK_TABLE_SCHEMA, tf.table_name as FK_TABLE_NAME, cf.column_name as FK_COLUMN_NAME,\r\n       uf.user_name || tf.table_name || fk.foreign_index_id as FK_NAME, ic.[sequence] as ORDINAL\r\nfrom sys.SYSFKEY fk\r\n    join sys.SYSFKCOL fkc on fk.foreign_index_id = fkc.foreign_key_id and fk.foreign_table_id = fkc.foreign_table_id\r\n    join sys.SYSIDXCOL ic on fkc.foreign_table_id = ic.table_id and fkc.foreign_key_id = ic.index_id and fkc.foreign_column_id = ic.column_id\r\n    join sys.SYSTAB tf on fk.foreign_table_id = tf.table_id\r\n    join sys.SYSTAB tp on fk.primary_table_id = tp.table_id\r\n    join sys.SYSUSER uf on tf.creator = uf.user_id\r\n    join sys.SYSUSER up on tp.creator = up.user_id\r\n    join sys.SYSTABCOL cf on fkc.foreign_column_id = cf.column_id and tf.table_id = cf.table_id\r\n    join sys.SYSTABCOL cp on fkc.primary_column_id = cp.column_id and tp.table_id = cp.table_id\r\nwhere up.user_name = {0} and tp.table_name = {1}\r\norder by uf.user_name || tf.table_name || fk.foreign_index_id, ic.[sequence]", new string[] { schema, table });
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00050EC2 File Offset: 0x0004F0C2
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysChild", connection, "select up.user_name as PK_TABLE_SCHEMA, tp.table_name as PK_TABLE_NAME, cp.column_name as PK_COLUMN_NAME,\r\n       cf.column_name as FK_COLUMN_NAME,\r\n       uf.user_name || tf.table_name || fk.foreign_index_id as FK_NAME, ic.[sequence] as ORDINAL\r\nfrom sys.SYSFKEY fk\r\n    join sys.SYSFKCOL fkc on fk.foreign_index_id = fkc.foreign_key_id and fk.foreign_table_id = fkc.foreign_table_id\r\n    join sys.SYSIDXCOL ic on fkc.foreign_table_id = ic.table_id and fkc.foreign_key_id = ic.index_id and fkc.foreign_column_id = ic.column_id\r\n    join sys.SYSTAB tf on fk.foreign_table_id = tf.table_id\r\n    join sys.SYSTAB tp on fk.primary_table_id = tp.table_id\r\n    join sys.SYSUSER uf on tf.creator = uf.user_id\r\n    join sys.SYSUSER up on tp.creator = up.user_id\r\n    join sys.SYSTABCOL cf on fkc.foreign_column_id = cf.column_id and tf.table_id = cf.table_id\r\n    join sys.SYSTABCOL cp on fkc.primary_column_id = cp.column_id and tp.table_id = cp.table_id\r\nwhere uf.user_name = {0} and tf.table_name = {1}\r\norder by uf.user_name || tf.table_name || fk.foreign_index_id, ic.[sequence]", new string[] { schema, table });
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00050EE3 File Offset: 0x0004F0E3
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new SybaseEnvironment.SybaseConnectionStringBuilder(base.Host, this.Resource);
		}

		// Token: 0x04000B4D RID: 2893
		public const string DataSourceName = "Sybase SQL Anywhere";

		// Token: 0x04000B4E RID: 2894
		public const string SybaseProviderNameOld = "iAnywhere.Data.SQLAnywhere";

		// Token: 0x04000B4F RID: 2895
		public const string SybaseProviderName = "Sap.Data.SQLAnywhere";

		// Token: 0x04000B50 RID: 2896
		private const string DownloadLink = "https://go.microsoft.com/fwlink/?LinkId=324846";

		// Token: 0x04000B51 RID: 2897
		private const string ClientLibraryName = "Sybase SQL Anywhere Client Software";

		// Token: 0x04000B52 RID: 2898
		private const string StandardLiteralQuote = "'";

		// Token: 0x04000B53 RID: 2899
		internal static bool? usesOldProvider;

		// Token: 0x04000B54 RID: 2900
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = EmptyArray<char>.Instance,
			MaxIdentifierLength = 128,
			QuoteIdentifier = new Func<string, string>(DbEnvironment.BracketQuoteIdentifier),
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			RequiresAsForFromAlias = false,
			DatePrefix = "'",
			DateSuffix = "'",
			PagingStrategy = PagingStrategy.TopAndRowCount,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsCaseExpression = true
		};

		// Token: 0x04000B55 RID: 2901
		private static readonly IDictionary<string, TableType> sybaseTableTypes = new Dictionary<string, TableType>(DbEnvironment.defaultTableTypes, StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000B56 RID: 2902
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"smallint", "unsigned smallint", "int", "unsigned int", "integer", "unsigned integer", "real", "float", "double", "money",
			"smallmoney", "bit", "tinyint", "unsigned tinyint", "bigint", "unsigned bigint", "binary", "decimal", "numeric", "datetime",
			"smalldatetime", "varchar", "char", "nchar", "nvarchar", "varbinary", "uniqueidentifier", "varbit", "uniqueidentifierstr", "date",
			"time", "timestamp", "datetimezone", "timestamp with time zone", "sysname"
		};

		// Token: 0x04000B57 RID: 2903
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"bigint",
				TypeValue.Int64
			},
			{
				"binary",
				TypeValue.Binary
			},
			{
				"bit",
				TypeValue.Logical
			},
			{
				"char",
				TypeValue.Text
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
				"datetimeoffset",
				TypeValue.DateTimeZone
			},
			{
				"decimal",
				TypeValue.Decimal
			},
			{
				"double",
				TypeValue.Double
			},
			{
				"float",
				TypeValue.Double
			},
			{
				"image",
				TypeValue.Binary
			},
			{
				"int",
				TypeValue.Int32
			},
			{
				"integer",
				TypeValue.Int32
			},
			{
				"long binary",
				TypeValue.Binary
			},
			{
				"long nvarchar",
				TypeValue.Text
			},
			{
				"long varbit",
				TypeValue.Text
			},
			{
				"long varchar",
				TypeValue.Text
			},
			{
				"money",
				TypeValue.Currency
			},
			{
				"nchar",
				TypeValue.Text
			},
			{
				"ntext",
				TypeValue.Text
			},
			{
				"numeric",
				TypeValue.Decimal
			},
			{
				"nvarchar",
				TypeValue.Text
			},
			{
				"real",
				TypeValue.Single
			},
			{
				"smalldatetime",
				TypeValue.DateTime
			},
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"smallmoney",
				TypeValue.Currency
			},
			{
				"sysname",
				TypeValue.Text
			},
			{
				"text",
				TypeValue.Text
			},
			{
				"time",
				TypeValue.Time
			},
			{
				"timestamp",
				TypeValue.DateTime
			},
			{
				"timestamp with time zone",
				TypeValue.DateTimeZone
			},
			{
				"tinyint",
				TypeValue.Byte
			},
			{
				"uniqueidentifier",
				TypeValue.Guid
			},
			{
				"uniqueidentifierstr",
				TypeValue.Text
			},
			{
				"unsigned bigint",
				TypeValue.Decimal
			},
			{
				"unsigned int",
				TypeValue.Int64
			},
			{
				"unsigned integer",
				TypeValue.Int64
			},
			{
				"unsigned smallint",
				TypeValue.Int32
			},
			{
				"unsigned tinyint",
				TypeValue.Byte
			},
			{
				"varbinary",
				TypeValue.Binary
			},
			{
				"varbit",
				TypeValue.Text
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"xml",
				TypeValue.Text
			}
		};

		// Token: 0x04000B58 RID: 2904
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"binary", "image", "long binary", "long nvarchar", "long varbit", "long varchar", "ntext", "text", "nvarchar", "varbinary",
			"varbit", "varchar", "xml"
		};

		// Token: 0x02000371 RID: 881
		private sealed class SybaseConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06001F3D RID: 7997 RVA: 0x00047C79 File Offset: 0x00045E79
			public SybaseConnectionStringBuilder(IEngineHost host, IResource resource)
				: base(host, resource)
			{
			}

			// Token: 0x17000DBF RID: 3519
			// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00050EF6 File Offset: 0x0004F0F6
			protected override string UserNameKey
			{
				get
				{
					return "UserID";
				}
			}

			// Token: 0x17000DC0 RID: 3520
			// (get) Token: 0x06001F3F RID: 7999 RVA: 0x00047C8A File Offset: 0x00045E8A
			protected override string PasswordKey
			{
				get
				{
					return "Password";
				}
			}

			// Token: 0x17000DC1 RID: 3521
			// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00050EFD File Offset: 0x0004F0FD
			protected override string ServerKey
			{
				get
				{
					return "Host";
				}
			}

			// Token: 0x17000DC2 RID: 3522
			// (get) Token: 0x06001F41 RID: 8001 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string PortKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000DC3 RID: 3523
			// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00050F04 File Offset: 0x0004F104
			protected override string DatabaseKey
			{
				get
				{
					return "DatabaseName";
				}
			}

			// Token: 0x17000DC4 RID: 3524
			// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00050F0B File Offset: 0x0004F10B
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Integrated";
				}
			}

			// Token: 0x17000DC5 RID: 3525
			// (get) Token: 0x06001F44 RID: 8004 RVA: 0x00050F12 File Offset: 0x0004F112
			protected override string EncryptKey
			{
				get
				{
					return "Encryption";
				}
			}

			// Token: 0x17000DC6 RID: 3526
			// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00047CAD File Offset: 0x00045EAD
			protected override object AuthenticationTypeValue
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000DC7 RID: 3527
			// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00050F19 File Offset: 0x0004F119
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "ConnectionTimeout";
				}
			}

			// Token: 0x17000DC8 RID: 3528
			// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00050F20 File Offset: 0x0004F120
			private string PoolingKey
			{
				get
				{
					return "Pooling";
				}
			}

			// Token: 0x17000DC9 RID: 3529
			// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00050F27 File Offset: 0x0004F127
			private object PoolingValue
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001F49 RID: 8009 RVA: 0x00050F2F File Offset: 0x0004F12F
			protected override bool ApplyWindowsCredential(WindowsCredential credential)
			{
				this.builder[this.IntegratedSecurityKey] = this.AuthenticationTypeValue;
				this.builder[this.PoolingKey] = this.PoolingValue;
				return true;
			}

			// Token: 0x06001F4A RID: 8010 RVA: 0x00050F60 File Offset: 0x0004F160
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					this.builder[this.EncryptKey] = "SIMPLE";
				}
				return true;
			}
		}
	}
}
