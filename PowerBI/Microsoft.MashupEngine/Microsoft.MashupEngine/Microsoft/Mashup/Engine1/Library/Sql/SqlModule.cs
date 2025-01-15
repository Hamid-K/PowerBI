using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003CC RID: 972
	public sealed class SqlModule : Module
	{
		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x0005DC80 File Offset: 0x0005BE80
		public override string Name
		{
			get
			{
				return "Sql";
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x0005DC87 File Offset: 0x0005BE87
		public override Keys ExportKeys
		{
			get
			{
				if (SqlModule.exportKeys == null)
				{
					SqlModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Sql.Database";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "Sql.Databases";
					});
				}
				return SqlModule.exportKeys;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x0005DCBF File Offset: 0x0005BEBF
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SqlResource.ResourceKindInfo };
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0005DCD0 File Offset: 0x0005BED0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new SqlModule.DatabaseFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new SqlModule.DatabasesFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x04000D19 RID: 3353
		public const string DatabaseQuery = "select d.name as Name from sys.databases d where d.name not in ('master', 'model', 'msdb', 'tempdb') and d.is_distributor = 0 and d.state = 0";

		// Token: 0x04000D1A RID: 3354
		internal static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			Options.MaxDegreeOfParallelismOption,
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration, new DurationValue(0, 0, 0, 30.0), OptionItemOption.None, null, null),
			new OptionItem("EnableBulkInsert", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.RequiresActions, null, null),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("MultiSubnetFailover", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("UnsafeTypeConversions", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("ContextInfo", NullableTypeValue.Binary, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("OmitSRID", NullableTypeValue.Binary, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("LegacyExtension", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.ForExtensibilityOnly, null, null),
			new OptionItem("IncludeFieldCaptions", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.ForExtensibilityOnly, null, null),
			new OptionItem("EnableCrossDatabaseFolding", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x04000D1B RID: 3355
		private static Keys exportKeys;

		// Token: 0x04000D1C RID: 3356
		public const string SqlDatabase = "Sql.Database";

		// Token: 0x04000D1D RID: 3357
		private const string LocalDomain = "localhost";

		// Token: 0x020003CD RID: 973
		private enum Exports
		{
			// Token: 0x04000D1F RID: 3359
			Database,
			// Token: 0x04000D20 RID: 3360
			Databases,
			// Token: 0x04000D21 RID: 3361
			Count
		}

		// Token: 0x020003CE RID: 974
		public sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x060021FB RID: 8699 RVA: 0x0005DE94 File Offset: 0x0005C094
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", SqlModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17000E67 RID: 3687
			// (get) Token: 0x060021FC RID: 8700 RVA: 0x0005DED2 File Offset: 0x0005C0D2
			public override IFunctionIdentity FunctionIdentity
			{
				get
				{
					return new FunctionTypeIdentity(base.GetType());
				}
			}

			// Token: 0x060021FD RID: 8701 RVA: 0x0005DEE0 File Offset: 0x0005C0E0
			public override TableValue TypedInvoke(TextValue server, TextValue database, Value options)
			{
				IDataSourceLocation dataSourceLocation = new SqlDataSourceLocation();
				dataSourceLocation.Address["server"] = server.String;
				dataSourceLocation.Address["database"] = database.String;
				return SqlEnvironment.Create(this.host, server.String, database.String, options, dataSourceLocation).CreateTable();
			}

			// Token: 0x17000E68 RID: 3688
			// (get) Token: 0x060021FE RID: 8702 RVA: 0x0005DF3D File Offset: 0x0005C13D
			public override string PrimaryResourceKind
			{
				get
				{
					return "SQL";
				}
			}

			// Token: 0x060021FF RID: 8703 RVA: 0x0005DF44 File Offset: 0x0005C144
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerDatabaseLocation<SqlDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x04000D22 RID: 3362
			private static readonly TypeValue optionsType = SqlModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000D23 RID: 3363
			private readonly IEngineHost host;
		}

		// Token: 0x020003CF RID: 975
		private sealed class DatabasesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06002201 RID: 8705 RVA: 0x0005DF66 File Offset: 0x0005C166
			public DatabasesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "server", TypeValue.Text, "options", SqlModule.DatabasesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06002202 RID: 8706 RVA: 0x0005DF90 File Offset: 0x0005C190
			public override TableValue TypedInvoke(TextValue server, Value options)
			{
				IDataSourceLocation dataSourceLocation = new SqlDataSourceLocation();
				dataSourceLocation.Address["server"] = server.String;
				return new SqlModule.DatabasesFunctionValue.DatabasesTableValue(this.host, server, dataSourceLocation, options);
			}

			// Token: 0x17000E69 RID: 3689
			// (get) Token: 0x06002203 RID: 8707 RVA: 0x0005DF3D File Offset: 0x0005C13D
			public override string PrimaryResourceKind
			{
				get
				{
					return "SQL";
				}
			}

			// Token: 0x06002204 RID: 8708 RVA: 0x0005DFC8 File Offset: 0x0005C1C8
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				int num;
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (SqlModule.DatabasesFunctionValue.pattern.TryMatch(expression, out num, out dictionary) && dictionary.TryGetConstant("server", out value))
				{
					IExpression @null;
					if (!dictionary.TryGetValue("options", out @null))
					{
						@null = ConstantExpressionSyntaxNode.Null;
					}
					bool? flag;
					if (StaticAnalysisResolver.TryGetRelationalLocation<SqlDataSourceLocation>(value, @null, out location, out foundOptions, out unknownOptions, out flag) && string.IsNullOrEmpty(location.Query) && (flag != null || num <= 1))
					{
						if (flag != null && num > 1)
						{
							bool? flag2 = flag;
							bool flag3 = num == 3;
							if (!((flag2.GetValueOrDefault() == flag3) & (flag2 != null)))
							{
								return false;
							}
						}
						if (location.TrySetAddressString("database", dictionary, "database") && flag != null && flag.Value == num >= 3 && location.TrySetAddressString("object", dictionary, "item"))
						{
							location.TrySetAddressString("schema", dictionary, "schema");
						}
						return true;
					}
					return false;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04000D24 RID: 3364
			private static readonly TypeValue optionsType = SqlModule.OptionRecord.Remove(new string[] { "Query" }).CreateRecordType().Nullable;

			// Token: 0x04000D25 RID: 3365
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, _o_options)", "__func(__server, _o_options){[Name=__database]}[Data]", "__func(__server, _o_options){[Name=__database]}[Data]{[Schema=_o_schema, Item=__item]}[Data]", "__func(__server, _o_options){[Name=__database]}[Data]{[Schema=_o_schema]}[Data]{[Name=__item]}[Data]" });

			// Token: 0x04000D26 RID: 3366
			private const int databaseOnlyIndex = 1;

			// Token: 0x04000D27 RID: 3367
			private const int hierarchicalIndex = 3;

			// Token: 0x04000D28 RID: 3368
			private readonly IEngineHost host;

			// Token: 0x020003D0 RID: 976
			private sealed class DatabasesTableValue : TableValue
			{
				// Token: 0x17000E6A RID: 3690
				// (get) Token: 0x06002206 RID: 8710 RVA: 0x0005E130 File Offset: 0x0005C330
				private static TableSortOrder _SortOrder
				{
					get
					{
						if (SqlModule.DatabasesFunctionValue.DatabasesTableValue.sortOrder == null)
						{
							SqlModule.DatabasesFunctionValue.DatabasesTableValue.sortOrder = new TableSortOrder(new SortOrder[]
							{
								new SortOrder(new TableValue.ColumnSelectorFunctionValue(NavigationTableServices.MetadataValuesWithKind[0], 0), null, true)
							});
						}
						return SqlModule.DatabasesFunctionValue.DatabasesTableValue.sortOrder;
					}
				}

				// Token: 0x06002207 RID: 8711 RVA: 0x0005E170 File Offset: 0x0005C370
				public DatabasesTableValue(IEngineHost host, TextValue server, IDataSourceLocation location, Value options)
				{
					this.host = host;
					this.server = server;
					this.location = location;
					this.options = options;
					this.hierarchicalNavigation = LogicalValue.False;
					if (options.IsRecord)
					{
						Value value;
						if (options.AsRecord.TryGetValue("Query", out value))
						{
							throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage("Microsoft SQL", Strings.UnsupportedQueryOption("Query", value)), Value.Null, null);
						}
						if (options.AsRecord.TryGetValue("HierarchicalNavigation", out value))
						{
							this.hierarchicalNavigation = value.AsLogical;
						}
					}
				}

				// Token: 0x17000E6B RID: 3691
				// (get) Token: 0x06002208 RID: 8712 RVA: 0x0005E210 File Offset: 0x0005C410
				public override TypeValue Type
				{
					get
					{
						return SqlModule.DatabasesFunctionValue.DatabasesTableValue.type;
					}
				}

				// Token: 0x17000E6C RID: 3692
				// (get) Token: 0x06002209 RID: 8713 RVA: 0x0005E217 File Offset: 0x0005C417
				public override TableSortOrder SortOrder
				{
					get
					{
						return SqlModule.DatabasesFunctionValue.DatabasesTableValue._SortOrder;
					}
				}

				// Token: 0x17000E6D RID: 3693
				// (get) Token: 0x0600220A RID: 8714 RVA: 0x0005E21E File Offset: 0x0005C41E
				public override Keys Columns
				{
					get
					{
						return NavigationTableServices.MetadataValuesWithKind;
					}
				}

				// Token: 0x17000E6E RID: 3694
				// (get) Token: 0x0600220B RID: 8715 RVA: 0x0005E225 File Offset: 0x0005C425
				private FunctionValue SqlDatabase
				{
					get
					{
						if (this.sqlDatabase == null)
						{
							this.sqlDatabase = new SqlModule.DatabaseFunctionValue(this.host);
						}
						return this.sqlDatabase;
					}
				}

				// Token: 0x0600220C RID: 8716 RVA: 0x0005E248 File Offset: 0x0005C448
				public override TableValue SelectRows(FunctionValue condition)
				{
					RecordValue recordValue;
					Value value;
					if (NavigationTableServices.TryGetIndexRecord(this.Type.AsTableType.ItemType, condition, out recordValue) && recordValue.Keys.Length == 1 && recordValue.TryGetValue(this.Columns[0], out value) && value.IsText)
					{
						return new ListTableValue(ListValue.New(new Value[] { this.CreateDatabaseRecord(value.AsString) }), this.Type.AsTableType);
					}
					return base.SelectRows(condition);
				}

				// Token: 0x0600220D RID: 8717 RVA: 0x0005E2D0 File Offset: 0x0005C4D0
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					if (this.databases == null)
					{
						string[] array = this.GetDatabases();
						Value[] array2 = new Value[array.Length];
						int num = 0;
						foreach (string text in array)
						{
							array2[num++] = this.CreateDatabaseRecord(text);
						}
						this.databases = new ListTableValue(ListValue.New(array2), this.Type.AsTableType).AsTable.Sort(SqlModule.DatabasesFunctionValue.DatabasesTableValue._SortOrder);
					}
					return this.databases.GetEnumerator();
				}

				// Token: 0x0600220E RID: 8718 RVA: 0x0005E34F File Offset: 0x0005C54F
				public override void TestConnection()
				{
					this.GetDatabasesTable().TestConnection();
				}

				// Token: 0x0600220F RID: 8719 RVA: 0x0005E35C File Offset: 0x0005C55C
				private TableValue GetDatabasesTable()
				{
					RecordValue recordValue = RecordValue.New(SqlModule.DatabasesFunctionValue.DatabasesTableValue.optionKeys, new Value[]
					{
						TextValue.New("select d.name as Name from sys.databases d where d.name not in ('master', 'model', 'msdb', 'tempdb') and d.is_distributor = 0 and d.state = 0"),
						this.hierarchicalNavigation
					});
					recordValue = (this.options.IsNull ? recordValue : this.options.Concatenate(recordValue).AsRecord);
					return SqlEnvironment.Create(this.host, this.server.String, null, recordValue, this.location).CreateTable();
				}

				// Token: 0x06002210 RID: 8720 RVA: 0x0005E3D5 File Offset: 0x0005C5D5
				private string[] GetDatabases()
				{
					return (from row in this.GetDatabasesTable()
						select row.Value.AsRecord.Item0.AsText.AsString).ToArray<string>();
				}

				// Token: 0x06002211 RID: 8721 RVA: 0x0005E408 File Offset: 0x0005C608
				private RecordValue CreateDatabaseRecord(string database)
				{
					return RecordValue.New(NavigationTableServices.MetadataValuesWithKind, delegate(int i)
					{
						if (i == 0)
						{
							return TextValue.New(database);
						}
						if (i != 1)
						{
							return SqlModule.DatabasesFunctionValue.DatabasesTableValue.databaseLinkKind;
						}
						return this.SqlDatabase.Invoke(this.server, TextValue.New(database), this.options);
					});
				}

				// Token: 0x04000D29 RID: 3369
				private static readonly Keys optionKeys = Keys.New("Query", "HierarchicalNavigation");

				// Token: 0x04000D2A RID: 3370
				private static readonly TextValue databaseLinkKind = TextValue.New("Database");

				// Token: 0x04000D2B RID: 3371
				private static TableSortOrder sortOrder;

				// Token: 0x04000D2C RID: 3372
				private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValuesWithKind, new Value[]
				{
					RecordTypeAlgebra.NewField(TypeValue.Text, false),
					RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Database", false), false),
					RecordTypeAlgebra.NewField(TypeValue.Text, false)
				})), new TableKey[]
				{
					new TableKey(new int[1], true)
				}));

				// Token: 0x04000D2D RID: 3373
				private readonly IEngineHost host;

				// Token: 0x04000D2E RID: 3374
				private readonly TextValue server;

				// Token: 0x04000D2F RID: 3375
				private readonly IDataSourceLocation location;

				// Token: 0x04000D30 RID: 3376
				private readonly Value options;

				// Token: 0x04000D31 RID: 3377
				private readonly LogicalValue hierarchicalNavigation;

				// Token: 0x04000D32 RID: 3378
				private FunctionValue sqlDatabase;

				// Token: 0x04000D33 RID: 3379
				private TableValue databases;
			}
		}
	}
}
