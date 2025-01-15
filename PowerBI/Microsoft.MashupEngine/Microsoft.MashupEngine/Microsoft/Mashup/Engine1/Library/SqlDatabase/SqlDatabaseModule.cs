using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SqlDatabase
{
	// Token: 0x020003E0 RID: 992
	internal sealed class SqlDatabaseModule : Module
	{
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x0005F20E File Offset: 0x0005D40E
		public override string Name
		{
			get
			{
				return "SqlDatabase";
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0005F215 File Offset: 0x0005D415
		public override Keys ExportKeys
		{
			get
			{
				if (SqlDatabaseModule.exportKeys == null)
				{
					SqlDatabaseModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "SqlDatabase.View";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return SqlDatabaseModule.exportKeys;
			}
		}

		// Token: 0x06002256 RID: 8790 RVA: 0x0005F250 File Offset: 0x0005D450
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new SqlDatabaseModule.ViewFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04000D55 RID: 3413
		private static Keys exportKeys;

		// Token: 0x020003E1 RID: 993
		private enum Exports
		{
			// Token: 0x04000D57 RID: 3415
			View,
			// Token: 0x04000D58 RID: 3416
			Count
		}

		// Token: 0x020003E2 RID: 994
		public sealed class ViewFunctionValue : NativeFunctionValue1<TableValue, RecordValue>
		{
			// Token: 0x06002258 RID: 8792 RVA: 0x0005F281 File Offset: 0x0005D481
			public ViewFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Table, "descriptor", TypeValue.Record)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x06002259 RID: 8793 RVA: 0x0005F2A0 File Offset: 0x0005D4A0
			public override TableValue TypedInvoke(RecordValue descriptor)
			{
				IExtensibilityService extensibilityService = this.engineHost.QueryService<IExtensibilityService>();
				if (extensibilityService == null)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				if (!SqlDatabaseModule.ViewFunctionValue.resourceAllowList.Contains(extensibilityService.CurrentResource.Kind) && !this.engineHost.GetConfigurationProperty("EnableSqlDatabaseViewForTesting", false))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Extensibility_NotAvailable, null, null);
				}
				return new SqlDatabaseModule.SqlDatabaseEnvironment(this.engineHost, descriptor, extensibilityService.CurrentResource, descriptor["DataSourceName"].AsString, descriptor["Server"].AsString, descriptor["Database"].AsString).CreateCatalogTableValue(this.engineHost);
			}

			// Token: 0x04000D59 RID: 3417
			public const string DatabaseKey = "Database";

			// Token: 0x04000D5A RID: 3418
			public const string DataSourceNameKey = "DataSourceName";

			// Token: 0x04000D5B RID: 3419
			public const string DialectKey = "Dialect";

			// Token: 0x04000D5C RID: 3420
			public const string GeneratorVersionKey = "GeneratorVersion";

			// Token: 0x04000D5D RID: 3421
			public const string GetColumnsKey = "GetColumns";

			// Token: 0x04000D5E RID: 3422
			public const string GetForeignKeysChildKey = "GetForeignKeysChild";

			// Token: 0x04000D5F RID: 3423
			public const string GetForeignKeysParentKey = "GetForeignKeysParent";

			// Token: 0x04000D60 RID: 3424
			public const string GetIndexesKey = "GetIndexes";

			// Token: 0x04000D61 RID: 3425
			public const string GetNativeQueryKey = "GetNativeQuery";

			// Token: 0x04000D62 RID: 3426
			public const string GetProceduresKey = "GetProcedures";

			// Token: 0x04000D63 RID: 3427
			public const string GetRowsKey = "GetRows";

			// Token: 0x04000D64 RID: 3428
			public const string GetTablesKey = "GetTables";

			// Token: 0x04000D65 RID: 3429
			public const string NativeQueryOptions = "NativeQueryOptions";

			// Token: 0x04000D66 RID: 3430
			public const string ServerKey = "Server";

			// Token: 0x04000D67 RID: 3431
			public const string ServerVersionKey = "ServerVersion";

			// Token: 0x04000D68 RID: 3432
			private static readonly HashSet<string> resourceAllowList = new HashSet<string> { "PowerPlatformDataflows", "CommonDataService", "Synapse" };

			// Token: 0x04000D69 RID: 3433
			private readonly IEngineHost engineHost;
		}

		// Token: 0x020003E3 RID: 995
		private sealed class SqlDatabaseEnvironment : DbEnvironment
		{
			// Token: 0x0600225B RID: 8795 RVA: 0x0005F380 File Offset: 0x0005D580
			public SqlDatabaseEnvironment(IEngineHost engineHost, RecordValue descriptor, IResource resource, string dataSourceName, string server, string database)
				: base(engineHost, resource, dataSourceName, server, database, Value.Null, null, null)
			{
				Value value;
				if (!descriptor.TryGetValue("Dialect", out value) || !value.IsText || value.AsString != "T-SQL" || !descriptor.TryGetValue("GeneratorVersion", out value) || !value.IsNumber || value.AsNumber.AsInteger32 != 1)
				{
					throw this.NotImplementedException();
				}
				if (descriptor.TryGetValue("ServerVersion", out value) && value.IsText)
				{
					this.serverVersion = value.AsString;
				}
				if (descriptor.TryGetValue("EngineEdition", out value) && value.IsNumber)
				{
					this.engineEdition = new int?(value.AsNumber.AsInteger32);
				}
				IList<string> list;
				if (descriptor.TryGetValue("NativeQueryOptions", out value) && value.IsList && value.AsList.TryGetStringList(10, out list))
				{
					this.supportsNativeQueryFolding = list.Contains("EnableFolding");
					this.supportsNativeQueryTypePreservation = list.Contains("PreserveTypes");
				}
				this.descriptor = descriptor;
				this.baseEnvironment = SqlEnvironment.CreateForFolding(engineHost, resource, server, database, this.serverVersion, this.engineEdition);
			}

			// Token: 0x17000E7D RID: 3709
			// (get) Token: 0x0600225C RID: 8796 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
			public override bool UnsafeTypeConversions
			{
				get
				{
					if (this.unsafeTypeConversions == null)
					{
						Value value;
						this.unsafeTypeConversions = new bool?(this.descriptor.TryGetValue("UnsafeTypeConversions", out value) && value.IsLogical && value.AsBoolean);
					}
					return this.unsafeTypeConversions.Value;
				}
			}

			// Token: 0x17000E7E RID: 3710
			// (get) Token: 0x0600225D RID: 8797 RVA: 0x0005F50D File Offset: 0x0005D70D
			public override bool SupportsNativeQueryFolding
			{
				get
				{
					return this.supportsNativeQueryFolding;
				}
			}

			// Token: 0x17000E7F RID: 3711
			// (get) Token: 0x0600225E RID: 8798 RVA: 0x0005F515 File Offset: 0x0005D715
			public override bool SupportsNativeQueryTypePreservation
			{
				get
				{
					return this.supportsNativeQueryTypePreservation;
				}
			}

			// Token: 0x17000E80 RID: 3712
			// (get) Token: 0x0600225F RID: 8799 RVA: 0x0005F51D File Offset: 0x0005D71D
			public override OptionRecordDefinition ValidOptions
			{
				get
				{
					return OptionRecordDefinition.Empty;
				}
			}

			// Token: 0x17000E81 RID: 3713
			// (get) Token: 0x06002260 RID: 8800 RVA: 0x0005F524 File Offset: 0x0005D724
			public override Dictionary<string, TypeValue> NativeToClrTypeMapping
			{
				get
				{
					return this.baseEnvironment.NativeToClrTypeMapping;
				}
			}

			// Token: 0x17000E82 RID: 3714
			// (get) Token: 0x06002261 RID: 8801 RVA: 0x0005F531 File Offset: 0x0005D731
			public override HashSet<string> SearchableTypes
			{
				get
				{
					return this.baseEnvironment.SearchableTypes;
				}
			}

			// Token: 0x06002262 RID: 8802 RVA: 0x0005F53E File Offset: 0x0005D73E
			public override bool? IsVariableLengthType(string dataType)
			{
				return this.baseEnvironment.IsVariableLengthType(dataType);
			}

			// Token: 0x17000E83 RID: 3715
			// (get) Token: 0x06002263 RID: 8803 RVA: 0x0005F54C File Offset: 0x0005D74C
			protected override string ProviderName
			{
				get
				{
					throw this.NotImplementedException();
				}
			}

			// Token: 0x06002264 RID: 8804 RVA: 0x0005F54C File Offset: 0x0005D74C
			public override DataTable LoadSchemas(DbConnection connection)
			{
				throw this.NotImplementedException();
			}

			// Token: 0x06002265 RID: 8805 RVA: 0x00059643 File Offset: 0x00057843
			public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
			{
				return SqlAstCreator.Create(expression, cursor, this);
			}

			// Token: 0x06002266 RID: 8806 RVA: 0x00059526 File Offset: 0x00057726
			protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
			{
				SqlAstExpressionChecker.Check(expression, cursor, this);
			}

			// Token: 0x06002267 RID: 8807 RVA: 0x0005F554 File Offset: 0x0005D754
			public override SqlDataType GetSqlScalarType(TypeValue type)
			{
				return this.baseEnvironment.GetSqlScalarType(type);
			}

			// Token: 0x06002268 RID: 8808 RVA: 0x0005F562 File Offset: 0x0005D762
			protected override DbEnvironment.DbServerMetadata LoadServerMetadata()
			{
				return new DbEnvironment.DbServerMetadata
				{
					Version = this.serverVersion
				};
			}

			// Token: 0x06002269 RID: 8809 RVA: 0x0005F54C File Offset: 0x0005D74C
			protected override ConnectionStringResourceCredentialDispatcher CreateConnectionStringDispatcher()
			{
				throw this.NotImplementedException();
			}

			// Token: 0x0600226A RID: 8810 RVA: 0x0005F575 File Offset: 0x0005D775
			protected override ConnectionInfo CreateConnectionInfo()
			{
				return new ConnectionInfo(this.Resource.Path, "", () => null, false);
			}

			// Token: 0x0600226B RID: 8811 RVA: 0x0005F5AC File Offset: 0x0005D7AC
			public override bool OtherCanFoldToThis(EnvironmentBase other)
			{
				SqlDatabaseModule.SqlDatabaseEnvironment sqlDatabaseEnvironment = other as SqlDatabaseModule.SqlDatabaseEnvironment;
				return sqlDatabaseEnvironment != null && base.Server == sqlDatabaseEnvironment.Server && base.Database == sqlDatabaseEnvironment.Database;
			}

			// Token: 0x0600226C RID: 8812 RVA: 0x0005F54C File Offset: 0x0005D74C
			protected override ResourceExceptionKind GetResourceExceptionKind(DbException exception)
			{
				throw this.NotImplementedException();
			}

			// Token: 0x0600226D RID: 8813 RVA: 0x0005F5E9 File Offset: 0x0005D7E9
			protected override SqlSettings LoadSqlSettings()
			{
				return SqlEnvironment.CreateSettings(this.serverVersion, this.engineEdition);
			}

			// Token: 0x0600226E RID: 8814 RVA: 0x0005F5FC File Offset: 0x0005D7FC
			public override TableValue GetDirectQueryCapabilities()
			{
				return this.baseEnvironment.GetDirectQueryCapabilities();
			}

			// Token: 0x0600226F RID: 8815 RVA: 0x0005F54C File Offset: 0x0005D74C
			protected override DbProviderFactory CreateDbProviderFactory()
			{
				throw this.NotImplementedException();
			}

			// Token: 0x06002270 RID: 8816 RVA: 0x0005F609 File Offset: 0x0005D809
			private Exception NotImplementedException()
			{
				return DataSourceException.NewDataSourceError<Message0>(base.Host, Strings.GenericUnsupported, this.Resource, null, null);
			}

			// Token: 0x06002271 RID: 8817 RVA: 0x0005F624 File Offset: 0x0005D824
			private DataTable ConvertToDataTable(TableValue table)
			{
				DataTable dataTable2;
				using (IDataReader dataReader = new TableDataReader(table.Type.AsTableType, new TableValueDataReader(table, true), null))
				{
					DataTable dataTable = new DataTable();
					dataTable.Locale = CultureInfo.InvariantCulture;
					dataTable.Load(dataReader);
					dataTable2 = dataTable;
				}
				return dataTable2;
			}

			// Token: 0x06002272 RID: 8818 RVA: 0x0005F680 File Offset: 0x0005D880
			protected override DataTable GetTables(SchemaItem? itemFilter)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetTables", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke().AsTable);
				}
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture,
					Columns = 
					{
						{
							"TABLE_SCHEMA",
							typeof(string)
						},
						{
							"TABLE_NAME",
							typeof(string)
						},
						{
							"TABLE_TYPE",
							typeof(string)
						}
					}
				};
			}

			// Token: 0x06002273 RID: 8819 RVA: 0x0005F71C File Offset: 0x0005D91C
			protected override DataTable GetProcedures(SchemaItem? itemFilter)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetProcedures", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke().AsTable);
				}
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture,
					Columns = 
					{
						{
							"ROUTINE_SCHEMA",
							typeof(string)
						},
						{
							"ROUTINE_NAME",
							typeof(string)
						},
						{
							"ROUTINE_TYPE",
							typeof(string)
						}
					}
				};
			}

			// Token: 0x06002274 RID: 8820 RVA: 0x0005F7B8 File Offset: 0x0005D9B8
			protected override DataTable GetColumns(string schema, string table)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetColumns", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke(TextValue.New(schema), TextValue.New(table)).AsTable);
				}
				DataTable dataTable = new DataTable
				{
					Locale = CultureInfo.InvariantCulture
				};
				dataTable.Columns.Add("COLUMN_NAME", typeof(string));
				dataTable.Columns.Add("ORDINAL_POSITION", typeof(int));
				dataTable.Columns.Add("DATA_TYPE", typeof(string));
				dataTable.Columns.Add("IS_NULLABLE", typeof(bool));
				dataTable.Rows.Add(new object[]
				{
					table + "Id",
					0,
					"int",
					false
				});
				dataTable.Rows.Add(new object[] { "value", 1, "text", false });
				return dataTable;
			}

			// Token: 0x06002275 RID: 8821 RVA: 0x0005F8E8 File Offset: 0x0005DAE8
			protected override DataTable GetIndexes(string schema, string table)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetIndexes", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke(TextValue.New(schema), TextValue.New(table)).AsTable);
				}
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture,
					Columns = 
					{
						{
							"INDEX_NAME",
							typeof(string)
						},
						{
							"COLUMN_NAME",
							typeof(string)
						},
						{
							"ORDINAL_POSITION",
							typeof(int)
						},
						{
							"PRIMARY_KEY",
							typeof(bool)
						}
					}
				};
			}

			// Token: 0x06002276 RID: 8822 RVA: 0x0005F9A8 File Offset: 0x0005DBA8
			protected override DataTable GetForeignKeysParent(string schema, string table)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetForeignKeysParent", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke(TextValue.New(schema), TextValue.New(table)).AsTable);
				}
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture,
					Columns = 
					{
						{
							"FK_NAME",
							typeof(string)
						},
						{
							"ORDINAL",
							typeof(int)
						},
						{
							"FK_TABLE_SCHEMA",
							typeof(string)
						},
						{
							"FK_TABLE_NAME",
							typeof(string)
						},
						{
							"PK_COLUMN_NAME",
							typeof(string)
						},
						{
							"FK_COLUMN_NAME",
							typeof(string)
						}
					}
				};
			}

			// Token: 0x06002277 RID: 8823 RVA: 0x0005FAA0 File Offset: 0x0005DCA0
			protected override DataTable GetForeignKeysChild(string schema, string table)
			{
				Value value;
				if (this.descriptor.TryGetValue("GetForeignKeysChild", out value))
				{
					return this.ConvertToDataTable(value.AsFunction.Invoke(TextValue.New(schema), TextValue.New(table)).AsTable);
				}
				return new DataTable
				{
					Locale = CultureInfo.InvariantCulture,
					Columns = 
					{
						{
							"FK_NAME",
							typeof(string)
						},
						{
							"ORDINAL",
							typeof(int)
						},
						{
							"PK_TABLE_SCHEMA",
							typeof(string)
						},
						{
							"PK_TABLE_NAME",
							typeof(string)
						},
						{
							"PK_COLUMN_NAME",
							typeof(string)
						},
						{
							"FK_COLUMN_NAME",
							typeof(string)
						}
					}
				};
			}

			// Token: 0x06002278 RID: 8824 RVA: 0x0005FB96 File Offset: 0x0005DD96
			public override bool TryExecuteCommand(TableTypeValue resultType, Func<string> commandTextCtor, out TableValue result)
			{
				result = new SqlDatabaseModule.ResultTableValue(this, this.descriptor["GetRows"].AsFunction.Invoke(resultType, TextValue.New(commandTextCtor())).AsTable);
				return true;
			}

			// Token: 0x06002279 RID: 8825 RVA: 0x0005FBCC File Offset: 0x0005DDCC
			public override bool TryExecuteNativeQuery(string query, Value parameters, Value options, out TableValue result)
			{
				Value value;
				if (!this.descriptor.TryGetValue("GetNativeQuery", out value))
				{
					throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NotSupported, null, null);
				}
				result = new SqlDatabaseModule.ResultTableValue(this, this.descriptor["GetNativeQuery"].AsFunction.Invoke(TextValue.New(query), parameters, options).AsTable);
				return true;
			}

			// Token: 0x04000D6A RID: 3434
			private readonly RecordValue descriptor;

			// Token: 0x04000D6B RID: 3435
			private readonly DbEnvironment baseEnvironment;

			// Token: 0x04000D6C RID: 3436
			private readonly string serverVersion;

			// Token: 0x04000D6D RID: 3437
			private readonly int? engineEdition;

			// Token: 0x04000D6E RID: 3438
			private readonly bool supportsNativeQueryFolding;

			// Token: 0x04000D6F RID: 3439
			private readonly bool supportsNativeQueryTypePreservation;

			// Token: 0x04000D70 RID: 3440
			private bool? unsafeTypeConversions;
		}

		// Token: 0x020003E5 RID: 997
		private sealed class ResultTableValue : DelegatingTableValue
		{
			// Token: 0x0600227D RID: 8829 RVA: 0x0005FC37 File Offset: 0x0005DE37
			public ResultTableValue(DbEnvironment environment, TableValue table)
				: base(table)
			{
				this.environment = environment;
			}

			// Token: 0x0600227E RID: 8830 RVA: 0x0005FC47 File Offset: 0x0005DE47
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				this.environment.ReportFoldingFailure();
				return base.GetEnumerator();
			}

			// Token: 0x04000D73 RID: 3443
			private readonly DbEnvironment environment;
		}
	}
}
