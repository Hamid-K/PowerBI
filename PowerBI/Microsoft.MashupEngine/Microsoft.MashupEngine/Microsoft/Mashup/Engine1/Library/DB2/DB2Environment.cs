using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Drda;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DB2
{
	// Token: 0x02000CC6 RID: 3270
	internal class DB2Environment : DbEnvironment
	{
		// Token: 0x0600585C RID: 22620 RVA: 0x00134A04 File Offset: 0x00132C04
		private DB2Environment(IEngineHost host, string server, string database, Value options)
			: base(host, DatabaseResource.New("DB2", server, database), "IBM DB2", server, database, options, null, null)
		{
		}

		// Token: 0x0600585D RID: 22621 RVA: 0x00134A2F File Offset: 0x00132C2F
		public static DB2Environment Create(IEngineHost host, string server, string database, Value options)
		{
			return new DB2Environment(host, server, database, options);
		}

		// Token: 0x17001A7C RID: 6780
		// (get) Token: 0x0600585E RID: 22622 RVA: 0x00134A3A File Offset: 0x00132C3A
		public static string ClientSoftwareNotFoundExceptionMessage
		{
			get
			{
				return DbEnvironment.GetClientSoftwareNotFoundExceptionMessage("IBM Data Server Driver Package (DS Driver)", "https://go.microsoft.com/fwlink/p/?LinkID=274911");
			}
		}

		// Token: 0x17001A7D RID: 6781
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x00134A50 File Offset: 0x00132C50
		public static string ProviderMissingErrorMessage
		{
			get
			{
				return Strings.DatabaseProviderMissingExceptionMessage("IBM.Data.DB2");
			}
		}

		// Token: 0x06005860 RID: 22624 RVA: 0x00134A61 File Offset: 0x00132C61
		public static string ProviderConfigurationErrorMessage(string message)
		{
			return Strings.DatabaseProviderConfigurationErrorExceptionMessage("IBM.Data.DB2", message);
		}

		// Token: 0x06005861 RID: 22625 RVA: 0x00134A74 File Offset: 0x00132C74
		public override void TestConnection()
		{
			if (FxVersionDetector.InstalledFxVersion < ClrVersion.Net45)
			{
				base.TestConnection();
				return;
			}
			RecordValue recordValue = RecordValue.New(Keys.New("Implementation"), new Value[] { TextValue.New("Microsoft") });
			RecordValue recordValue2 = (base.OptionsRecord.IsNull ? recordValue : base.OptionsRecord.Concatenate(recordValue).AsRecord);
			new MsDb2Module().CreateDbEnvironment(base.Host, base.Server, base.Database, recordValue2).TestConnection();
		}

		// Token: 0x06005862 RID: 22626 RVA: 0x00134AF7 File Offset: 0x00132CF7
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return DB2AstCreator.Create(expression, cursor, this);
		}

		// Token: 0x06005863 RID: 22627 RVA: 0x00134B01 File Offset: 0x00132D01
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			DB2AstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x06005864 RID: 22628 RVA: 0x00134B0B File Offset: 0x00132D0B
		protected override SqlSettings LoadSqlSettings()
		{
			return DB2Environment.sqlSettings;
		}

		// Token: 0x06005865 RID: 22629 RVA: 0x00134B14 File Offset: 0x00132D14
		private static int? GetNativeError(DbException exception)
		{
			if (exception.GetType().FullName == "IBM.Data.DB2.DB2Exception")
			{
				PropertyInfo property = exception.GetType().GetProperty("Errors");
				if (property != null)
				{
					foreach (object obj in ((IEnumerable)property.GetGetMethod().Invoke(exception, new object[0])))
					{
						PropertyInfo property2 = obj.GetType().GetProperty("NativeError");
						if (property2 != null)
						{
							return new int?((int)property2.GetGetMethod().Invoke(obj, new object[0]));
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06005866 RID: 22630 RVA: 0x00134BF0 File Offset: 0x00132DF0
		protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
		{
			int? nativeError = DB2Environment.GetNativeError(exception);
			if (nativeError != null)
			{
				using (IHostTrace hostTrace = base.Tracer.CreateTrace("AuthorizationError", TraceEventType.Information))
				{
					hostTrace.Add("Exception", exception, true);
					hostTrace.Add("NativeError", nativeError.Value, false);
					int value = nativeError.Value;
					ResourceExceptionKind resourceExceptionKind;
					if (value <= -30081)
					{
						if (value != -30082)
						{
							if (value != -30081)
							{
								goto IL_00A5;
							}
							resourceExceptionKind = (exception.Message.Contains("\"202\"") ? ResourceExceptionKind.SecureConnectionFailed : ResourceExceptionKind.None);
							goto IL_00A7;
						}
					}
					else
					{
						if (value == -1109)
						{
							resourceExceptionKind = ResourceExceptionKind.SecureConnectionFailed;
							goto IL_00A7;
						}
						if (value != -567 && value != -551)
						{
							goto IL_00A5;
						}
					}
					resourceExceptionKind = ResourceExceptionKind.InvalidCredentials;
					goto IL_00A7;
					IL_00A5:
					resourceExceptionKind = ResourceExceptionKind.None;
					IL_00A7:
					hostTrace.Add("ResourceExceptionKind", resourceExceptionKind.ToString(), false);
					return resourceExceptionKind;
				}
				return ResourceExceptionKind.None;
			}
			return ResourceExceptionKind.None;
		}

		// Token: 0x06005867 RID: 22631 RVA: 0x00134CE0 File Offset: 0x00132EE0
		protected override RetryBehavior RetryAfterSqlError(DbException error)
		{
			if (!(DB2Environment.GetNativeError(error) != -30081) && error.Message.Contains("\"TCP/IP\""))
			{
				return new RetryBehavior(DB2Environment.RetryableTCPErrors.Any(new Func<string, bool>(error.Message.Contains)), DbEnvironment.RetryDelay);
			}
			return new RetryBehavior(false, DbEnvironment.RetryDelay);
		}

		// Token: 0x17001A7E RID: 6782
		// (get) Token: 0x06005868 RID: 22632 RVA: 0x00134D4E File Offset: 0x00132F4E
		protected override string ProviderName
		{
			get
			{
				return "IBM.Data.DB2";
			}
		}

		// Token: 0x17001A7F RID: 6783
		// (get) Token: 0x06005869 RID: 22633 RVA: 0x00134D55 File Offset: 0x00132F55
		protected override string ProviderDownloadLink
		{
			get
			{
				return "https://go.microsoft.com/fwlink/p/?LinkID=274911";
			}
		}

		// Token: 0x17001A80 RID: 6784
		// (get) Token: 0x0600586A RID: 22634 RVA: 0x00134D5C File Offset: 0x00132F5C
		protected override string ProviderLibraryName
		{
			get
			{
				return "IBM Data Server Driver Package (DS Driver)";
			}
		}

		// Token: 0x0600586B RID: 22635 RVA: 0x00134D64 File Offset: 0x00132F64
		public override bool SupportsTake(TableTypeValue type)
		{
			string text;
			return !base.UserOptions.TryGetString("Query", out text) && type.GetPrimaryKey() != null;
		}

		// Token: 0x17001A81 RID: 6785
		// (get) Token: 0x0600586C RID: 22636 RVA: 0x00134D90 File Offset: 0x00132F90
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return DB2Environment.searchableTypes;
			}
		}

		// Token: 0x17001A82 RID: 6786
		// (get) Token: 0x0600586D RID: 22637 RVA: 0x00134D97 File Offset: 0x00132F97
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return DB2Environment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x0600586E RID: 22638 RVA: 0x00134D9E File Offset: 0x00132F9E
		public override bool? IsVariableLengthType(string dataType)
		{
			return new bool?(DB2Environment.variableLengthTypes.Contains(dataType));
		}

		// Token: 0x0600586F RID: 22639 RVA: 0x00134DB0 File Offset: 0x00132FB0
		public override DataTable LoadSchemas(DbConnection connection)
		{
			DataTable schema = connection.GetSchema("Schemas");
			schema.Columns["TABLE_SCHEMA"].ColumnName = "SCHEMA_NAME";
			schema.DefaultView.Sort = "SCHEMA_NAME ASC";
			return schema.DefaultView.ToTable();
		}

		// Token: 0x06005870 RID: 22640 RVA: 0x00134DFC File Offset: 0x00132FFC
		public override DataTable LoadColumns(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Columns", connection, "select colname as COLUMN_NAME, colno as ORDINAL_POSITION, nulls as IS_NULLABLE, typename as DATA_TYPE\r\nfrom syscat.columns\r\nwhere tabschema = {0} and tabname = {1}\r\norder by colno", new string[] { schema, table });
		}

		// Token: 0x06005871 RID: 22641 RVA: 0x00134E1D File Offset: 0x0013301D
		public override DataTable LoadIndexes(DbConnection connection, string schema, string table)
		{
			return base.LoadData("Indexes", connection, "select i.indschema || '_' || i.indname as INDEX_NAME, i.tabschema as TABLE_SCHEMA, i.tabname as TABLE_NAME, ii.colname as COLUMN_NAME, ii.colseq as ORDINAL_POSITION, case when i.uniquerule = 'P' then 'Y' else 'N' end as PRIMARY_KEY\r\nfrom syscat.indexes i inner join syscat.indexcoluse ii on i.indschema = ii.indschema and i.indname = ii.indname\r\nwhere i.tabschema = {0}\r\nand i.tabname = {1}\r\nand i.uniquerule <> 'D'\r\norder by i.indschema || '_' || i.indname, ii.colseq", new string[] { schema, table });
		}

		// Token: 0x06005872 RID: 22642 RVA: 0x00134E3E File Offset: 0x0013303E
		public override DataTable LoadForeignKeysParent(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysParent", connection, "select\r\n    pkcol.colname as PK_COLUMN_NAME,\r\n    fkcon.tabschema AS FK_TABLE_SCHEMA,\r\n    fkcon.tabname AS FK_TABLE_NAME,\r\n    fkcol.colname as FK_COLUMN_NAME,\r\n    fkcol.colseq as ORDINAL,\r\n    fkcon.tabschema || '.' || fkcon.tabname || '.' || fkcon.constname as FK_NAME\r\nfrom\r\n    syscat.references fkcon\r\n        inner join\r\n    syscat.keycoluse fkcol on fkcon.constname = fkcol.constname and fkcon.tabschema = fkcol.tabschema and fkcon.tabname = fkcol.tabname\r\n        inner join\r\n    syscat.keycoluse pkcol on fkcon.refkeyname = pkcol.constname and fkcon.reftabschema = pkcol.tabschema and fkcon.reftabname = pkcol.tabname\r\nwhere\r\n    fkcol.colseq = pkcol.colseq\r\n  and\r\n    pkcol.tabschema = {0} and pkcol.tabname = {1}\r\norder by fkcon.tabschema || '.' || fkcon.tabname || '.' || fkcon.constname, fkcol.colseq", new string[] { schema, table });
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x00134E5F File Offset: 0x0013305F
		public override DataTable LoadForeignKeysChild(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ForeignKeysChild", connection, "select\r\n    pkcol.tabschema AS PK_TABLE_SCHEMA,\r\n    pkcol.tabname AS PK_TABLE_NAME,\r\n    pkcol.colname as PK_COLUMN_NAME,\r\n    fkcol.colname as FK_COLUMN_NAME,\r\n    fkcol.colseq as ORDINAL,\r\n    fkcon.tabschema || '.' || fkcon.tabname || '.' || fkcon.constname as FK_NAME\r\nfrom\r\n    syscat.references fkcon\r\n        inner join\r\n    syscat.keycoluse fkcol on fkcon.constname = fkcol.constname and fkcon.tabschema = fkcol.tabschema and fkcon.tabname = fkcol.tabname\r\n        inner join\r\n    syscat.keycoluse pkcol on fkcon.refkeyname = pkcol.constname and fkcon.reftabschema = pkcol.tabschema and fkcon.reftabname = pkcol.tabname\r\nwhere\r\n    fkcol.colseq = pkcol.colseq\r\n  and\r\n    fkcol.tabschema = {0} and fkcol.tabname = {1}\r\norder by fkcon.tabschema || '.' || fkcon.tabname || '.' || fkcon.constname, fkcol.colseq", new string[] { schema, table });
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x00134E80 File Offset: 0x00133080
		public override DataTable LoadResourceInformation(DbConnection connection, string schema, string table)
		{
			return base.LoadData("ResourceInformation", connection, "select\r\n    1024 * sum(data_object_l_size + index_object_l_size + long_object_l_size + lob_object_l_size + xml_object_l_size) as TOTAL_BYTES\r\nfrom \r\n    sysibmadm.admintabinfo\r\nwhere \r\n    tabschema = {0} and tabname = {1}", new string[] { schema, table });
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x00134EA1 File Offset: 0x001330A1
		protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
		{
			return new DB2Environment.DB2ConnectionStringBuilder(base.Host, this.Resource);
		}

		// Token: 0x17001A83 RID: 6787
		// (get) Token: 0x06005876 RID: 22646 RVA: 0x00134EB4 File Offset: 0x001330B4
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return DB2Environment.LegacyOptionRecord;
			}
		}

		// Token: 0x040031CE RID: 12750
		private const string DownloadLink = "https://go.microsoft.com/fwlink/p/?LinkID=274911";

		// Token: 0x040031CF RID: 12751
		private const string ClientLibraryName = "IBM Data Server Driver Package (DS Driver)";

		// Token: 0x040031D0 RID: 12752
		private static readonly OptionRecordDefinition LegacyOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("Implementation", NullableTypeValue.Text)
		});

		// Token: 0x040031D1 RID: 12753
		public const string DataSourceName = "IBM DB2";

		// Token: 0x040031D2 RID: 12754
		public const string DB2ProviderName = "IBM.Data.DB2";

		// Token: 0x040031D3 RID: 12755
		private static readonly List<string> RetryableTCPErrors = new List<string> { "\"10054\"", "\"10060\"", "\"10061\"" };

		// Token: 0x040031D4 RID: 12756
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = EmptyArray<char>.Instance,
			MaxIdentifierLength = 128,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			RequiresAsForFromAlias = false,
			DateTimePrefix = "timestamp('",
			DateTimeSuffix = "')",
			DateTimeOffsetPrefix = "timestamp_tz('",
			DateTimeOffsetSuffix = "')",
			PagingStrategy = PagingStrategy.RowCountOnly,
			SelectItemNull = SqlLanguageStrings.NullWithTrivialCastSqlString,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			TimePrefix = "time('",
			TimeSuffix = "')",
			IsMaxPrecision = true
		};

		// Token: 0x040031D5 RID: 12757
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"char () for bit data", "varchar () for bit data", "bigint", "boolean", "char", "character", "date", "decfloat", "decimal", "double precision",
			"double", "float", "graphic", "int", "integer", "long varchar", "long vargraphic", "numeric", "real", "rowid",
			"smallint", "time", "timestamp", "timestamp with time zone", "varchar", "vargraphic", "nchar", "nvarchar", "long nvarchar"
		};

		// Token: 0x040031D6 RID: 12758
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
				"blob",
				TypeValue.Binary
			},
			{
				"boolean",
				TypeValue.Int16
			},
			{
				"char () for bit data",
				TypeValue.Binary
			},
			{
				"char",
				TypeValue.Text
			},
			{
				"character",
				TypeValue.Text
			},
			{
				"clob",
				TypeValue.Text
			},
			{
				"date",
				TypeValue.Date
			},
			{
				"dbclob",
				TypeValue.Text
			},
			{
				"decfloat",
				TypeValue.Decimal
			},
			{
				"decimal",
				TypeValue.Decimal
			},
			{
				"double precision",
				TypeValue.Double
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
				"graphic",
				TypeValue.Text
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
				"long nvarchar",
				TypeValue.Text
			},
			{
				"long varbinary",
				TypeValue.Binary
			},
			{
				"long varchar () for bit data",
				TypeValue.Binary
			},
			{
				"long varchar",
				TypeValue.Text
			},
			{
				"long vargraphic",
				TypeValue.Text
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
				"rowid",
				TypeValue.Text
			},
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"time",
				TypeValue.Time
			},
			{
				"timestamp with time zone",
				TypeValue.DateTime
			},
			{
				"timestamp",
				TypeValue.DateTime
			},
			{
				"varbinary",
				TypeValue.Binary
			},
			{
				"varchar () for bit data",
				TypeValue.Binary
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"vargraphic",
				TypeValue.Text
			},
			{
				"xml",
				TypeValue.Text
			}
		};

		// Token: 0x040031D7 RID: 12759
		private static readonly HashSet<string> variableLengthTypes = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"blob", "clob", "dbclob", "long nvarchar", "long varbinary", "long varchar () for bit data", "long varchar", "long vargraphic", "nclob", "nvarchar",
			"varbinary", "varchar () for bit data", "varchar", "vargraphic"
		};

		// Token: 0x02000CC7 RID: 3271
		private sealed class DB2ConnectionStringBuilder : ConnectionStringResourceCredentialDispatcher
		{
			// Token: 0x06005878 RID: 22648 RVA: 0x00047C79 File Offset: 0x00045E79
			public DB2ConnectionStringBuilder(IEngineHost host, IResource resource)
				: base(host, resource)
			{
			}

			// Token: 0x17001A84 RID: 6788
			// (get) Token: 0x06005879 RID: 22649 RVA: 0x001354D6 File Offset: 0x001336D6
			protected override string UserNameKey
			{
				get
				{
					return "UID";
				}
			}

			// Token: 0x17001A85 RID: 6789
			// (get) Token: 0x0600587A RID: 22650 RVA: 0x001354DD File Offset: 0x001336DD
			protected override string PasswordKey
			{
				get
				{
					return "PWD";
				}
			}

			// Token: 0x17001A86 RID: 6790
			// (get) Token: 0x0600587B RID: 22651 RVA: 0x000831E5 File Offset: 0x000813E5
			protected override string ServerKey
			{
				get
				{
					return "Server";
				}
			}

			// Token: 0x17001A87 RID: 6791
			// (get) Token: 0x0600587C RID: 22652 RVA: 0x000020FA File Offset: 0x000002FA
			protected override string PortKey
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17001A88 RID: 6792
			// (get) Token: 0x0600587D RID: 22653 RVA: 0x000831F3 File Offset: 0x000813F3
			protected override string DatabaseKey
			{
				get
				{
					return "Database";
				}
			}

			// Token: 0x17001A89 RID: 6793
			// (get) Token: 0x0600587E RID: 22654 RVA: 0x001354E4 File Offset: 0x001336E4
			protected override string IntegratedSecurityKey
			{
				get
				{
					return "Authentication";
				}
			}

			// Token: 0x17001A8A RID: 6794
			// (get) Token: 0x0600587F RID: 22655 RVA: 0x001354EB File Offset: 0x001336EB
			protected override string EncryptKey
			{
				get
				{
					return "Security";
				}
			}

			// Token: 0x17001A8B RID: 6795
			// (get) Token: 0x06005880 RID: 22656 RVA: 0x001354F2 File Offset: 0x001336F2
			protected override object AuthenticationTypeValue
			{
				get
				{
					return "KERBEROS";
				}
			}

			// Token: 0x17001A8C RID: 6796
			// (get) Token: 0x06005881 RID: 22657 RVA: 0x00047CB5 File Offset: 0x00045EB5
			protected override string ConnectionTimeoutKey
			{
				get
				{
					return "Connection Timeout";
				}
			}

			// Token: 0x06005882 RID: 22658 RVA: 0x001354F9 File Offset: 0x001336F9
			protected override bool ApplyEncryptedCredentialAdornment(EncryptedConnectionAdornment credential)
			{
				if (credential.RequireEncryption)
				{
					this.builder[this.EncryptKey] = "SSL";
				}
				return true;
			}
		}

		// Token: 0x02000CC8 RID: 3272
		private static class DB2NativeErrorCodes
		{
			// Token: 0x040031D8 RID: 12760
			public const int SQL0551N = -551;

			// Token: 0x040031D9 RID: 12761
			public const int SQL0567N = -567;

			// Token: 0x040031DA RID: 12762
			public const int SQL1109N = -1109;

			// Token: 0x040031DB RID: 12763
			public const int SQL30081N = -30081;

			// Token: 0x040031DC RID: 12764
			public const int SQL30082N = -30082;
		}
	}
}
