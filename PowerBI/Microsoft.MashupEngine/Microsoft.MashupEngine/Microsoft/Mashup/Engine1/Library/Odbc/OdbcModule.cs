using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Navigation;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000605 RID: 1541
	internal class OdbcModule : Module
	{
		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x00092F67 File Offset: 0x00091167
		public override string Name
		{
			get
			{
				return "Odbc";
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x00092F6E File Offset: 0x0009116E
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(9, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Odbc.Query";
						case 1:
							return "Odbc.DataSource";
						case 2:
							return "Odbc.InferOptions";
						case 3:
							return OdbcModule.LimitClauseKindValue.KindType.GetName();
						case 4:
							return OdbcModule.LimitClauseKindValue.None.GetName();
						case 5:
							return OdbcModule.LimitClauseKindValue.Top.GetName();
						case 6:
							return OdbcModule.LimitClauseKindValue.Limit.GetName();
						case 7:
							return OdbcModule.LimitClauseKindValue.LimitOffset.GetName();
						case 8:
							return OdbcModule.LimitClauseKindValue.AnsiSql2008.GetName();
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x00092FAA File Offset: 0x000911AA
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { OdbcModule.resourceKindInfo };
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x00092FBC File Offset: 0x000911BC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			bool isExtension = host.QueryService<IExtensibilityService>() != null;
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					if (isExtension)
					{
						return new OdbcModule.ExtensionQueryFunctionValue(host);
					}
					return new OdbcModule.QueryFunctionValue(host);
				case 1:
					return new OdbcModule.DataSourceFunctionValue(host);
				case 2:
					return new OdbcModule.InferOptionsFunctionValue(host);
				case 3:
					return OdbcModule.LimitClauseKindValue.KindType;
				case 4:
					return OdbcModule.LimitClauseKindValue.None;
				case 5:
					return OdbcModule.LimitClauseKindValue.Top;
				case 6:
					return OdbcModule.LimitClauseKindValue.Limit;
				case 7:
					return OdbcModule.LimitClauseKindValue.LimitOffset;
				case 8:
					return OdbcModule.LimitClauseKindValue.AnsiSql2008;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x00093001 File Offset: 0x00091201
		public static Message2 CreateDataSourceExceptionMessage(string message)
		{
			return DataSourceException.DataSourceMessage("ODBC", message);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x00093010 File Offset: 0x00091210
		public static bool TryGetLocation(Value argument, out IDataSourceLocation location)
		{
			if (!argument.IsText && !argument.IsRecord)
			{
				location = null;
				return false;
			}
			Dictionary<string, string> dictionary = ConnectionStringHandler.HandleFormatExceptions<Dictionary<string, string>>("ODBC", argument, () => OdbcConnectionStringHandler.Windows.GetKeyValuePairs(OdbcConnectionStringHandler.Windows.GetString(argument)));
			OdbcDataSourceLocation odbcDataSourceLocation = new OdbcDataSourceLocation();
			odbcDataSourceLocation.Options = dictionary.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Key, (KeyValuePair<string, string> kvp) => kvp.Value);
			location = odbcDataSourceLocation;
			return true;
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x000930B8 File Offset: 0x000912B8
		public static IResource CreateResource(IEngineHost host, Value connectionAttributes)
		{
			IExtensibilityService extensibilityService = host.QueryService<IExtensibilityService>();
			if (extensibilityService != null && extensibilityService.CurrentResource != null && extensibilityService.ImpersonateResource)
			{
				return extensibilityService.CurrentResource;
			}
			return ConnectionStringHandler.HandleFormatExceptions<IResource>("ODBC", connectionAttributes, () => Resource.New("Odbc", OdbcConnectionStringHandler.Windows.GetString(connectionAttributes)));
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x00093110 File Offset: 0x00091310
		private static OdbcDataSource CreateDataSource(IEngineHost host, Value connectionProperties, OdbcOptions options)
		{
			IOdbcService odbcService = host.Hook(delegate
			{
				if (!options.UseEmbeddedDriver)
				{
					return OdbcService.WindowsInstance;
				}
				if (options.ClientConnectionPooling)
				{
					return OdbcService.EmbeddedInstance;
				}
				return OdbcService.ManagerPoolingEmbeddedInstance;
			});
			OdbcConnectionStringHandler odbcConnectionStringHandler = new OdbcConnectionStringHandler(odbcService);
			OdbcExceptionHandler odbcExceptionHandler = (options.OnError.IsNull ? new OdbcExceptionHandler(host) : new UserOverridableOdbcExceptionHandler(host, options.OnError.AsFunction));
			return new OdbcDataSource(host, odbcService, odbcConnectionStringHandler, connectionProperties, options, odbcExceptionHandler, null);
		}

		// Token: 0x04001559 RID: 5465
		public const string DataSourceName = "ODBC";

		// Token: 0x0400155A RID: 5466
		private static readonly OptionRecordDefinition PublicOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, "ODBC"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("SqlCompatibleWindowsAuth", NullableTypeValue.Logical)
		});

		// Token: 0x0400155B RID: 5467
		private static readonly OptionRecordDefinition ExtensionOnlyOptions = new OptionRecordDefinition(new OptionItem[]
		{
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text),
			new OptionItem("ClientConnectionPooling", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("SqlCapabilities", NullableTypeValue.Record, RecordValue.Empty, OptionItemOption.None, null, null),
			new OptionItem("SoftNumbers", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("SQLGetInfo", NullableTypeValue.Function, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("ImplicitTypeConversions", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("HideNativeQuery", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("TolerateConcatOverflow", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("SQLGetFunctions", NullableTypeValue.Function, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("DefaultTypeParameters", NullableTypeValue.Record, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("CancelQueryExplicitly", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x0400155C RID: 5468
		public static readonly OptionRecordDefinition OptionRecord = OdbcModule.PublicOptionRecord.Concatenate(OdbcModule.ExtensionOnlyOptions);

		// Token: 0x0400155D RID: 5469
		public static readonly OptionRecordDefinition QueryOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, "ODBC"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("SqlCompatibleWindowsAuth", NullableTypeValue.Logical)
		});

		// Token: 0x0400155E RID: 5470
		private static readonly ResourceKindInfo resourceKindInfo = new GenericProviderResourceKindInfo("Odbc", Strings.OdbcChallengeTitle, OdbcConnectionStringHandler.Windows, new AuthenticationInfo[]
		{
			new UsernamePasswordAuthenticationInfo
			{
				Description = Strings.OdbcSqlAuth
			},
			new ImplicitAuthenticationInfo
			{
				Label = Strings.GenericProvidersNoneAuthLabel,
				Description = Strings.OdbcNoneAuth
			},
			new WindowsAuthenticationInfo
			{
				SupportsAlternateCredentials = true,
				Description = Strings.OdbcWindowsAuth
			}
		}, new DataSourceLocationFactory[] { OdbcDataSourceLocation.Factory });

		// Token: 0x0400155F RID: 5471
		private Keys exportKeys;

		// Token: 0x02000606 RID: 1542
		private enum Exports
		{
			// Token: 0x04001561 RID: 5473
			Query,
			// Token: 0x04001562 RID: 5474
			DataSource,
			// Token: 0x04001563 RID: 5475
			InferOptions,
			// Token: 0x04001564 RID: 5476
			LimitClauseKind_Type,
			// Token: 0x04001565 RID: 5477
			LimitClauseKind_None,
			// Token: 0x04001566 RID: 5478
			LimitClauseKind_Top,
			// Token: 0x04001567 RID: 5479
			LimitClauseKind_Limit,
			// Token: 0x04001568 RID: 5480
			LimitClauseKind_LimitOffset,
			// Token: 0x04001569 RID: 5481
			LimitClauseKind_AnsiSql2008,
			// Token: 0x0400156A RID: 5482
			Count
		}

		// Token: 0x02000607 RID: 1543
		private sealed class ExtensionQueryFunctionValue : NativeFunctionValue3<TableValue, Value, TextValue, Value>
		{
			// Token: 0x060030A5 RID: 12453 RVA: 0x00093444 File Offset: 0x00091644
			public ExtensionQueryFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "connectionString", TypeValue.Any, "query", TypeValue.Text, "options", OdbcModule.ExtensionQueryFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x170011EB RID: 4587
			// (get) Token: 0x060030A6 RID: 12454 RVA: 0x00092F67 File Offset: 0x00091167
			public override string PrimaryResourceKind
			{
				get
				{
					return "Odbc";
				}
			}

			// Token: 0x060030A7 RID: 12455 RVA: 0x00093484 File Offset: 0x00091684
			public override TableValue TypedInvoke(Value connectionProperties, TextValue query, Value options)
			{
				OdbcOptions odbcOptions = OdbcOptions.CreateQueryOptionsFromValue(options, this.host);
				OdbcDataSource odbcDataSource = OdbcModule.CreateDataSource(this.host, connectionProperties, odbcOptions);
				return new OdbcNativeQueryTableValue("ODBC", this.host, odbcDataSource, query.AsString, null, true, null, null);
			}

			// Token: 0x060030A8 RID: 12456 RVA: 0x000934C8 File Offset: 0x000916C8
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length >= 2 && argumentValues.Length <= 3)
				{
					Keys empty = Keys.Empty;
					if (argumentValues[0].IsRecord)
					{
						argumentValues[0] = ExpressionAnalysis.RemovePlaceholders(argumentValues[0].AsRecord, out empty);
					}
					if (empty.Length == 0 && OdbcModule.TryGetLocation(argumentValues[0], out location))
					{
						IExpression expression2;
						if (argumentValues.Length >= 3)
						{
							expression2 = ((IInvocationExpression)expression).Arguments[2];
						}
						else
						{
							IExpression @null = ConstantExpressionSyntaxNode.Null;
							expression2 = @null;
						}
						bool? flag;
						StaticAnalysisResolver.HandleOptions(expression2, location, out foundOptions, out unknownOptions, out flag);
						if (argumentValues[1].IsText)
						{
							location.Query = argumentValues[1].AsString;
						}
						else
						{
							unknownOptions = Keys.New(unknownOptions.Concat(StaticAnalysisResolver.NativeQueryKeys).ToArray<string>());
						}
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400156B RID: 5483
			private static readonly TypeValue optionsType = OdbcModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x0400156C RID: 5484
			private readonly IEngineHost host;
		}

		// Token: 0x02000608 RID: 1544
		private sealed class QueryFunctionValue : NativeFunctionValue3<TableValue, Value, TextValue, Value>
		{
			// Token: 0x060030AA RID: 12458 RVA: 0x000935B0 File Offset: 0x000917B0
			public QueryFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "connectionString", TypeValue.Any, "query", TypeValue.Text, "options", OdbcModule.QueryFunctionValue.optionsType)
			{
				this.host = host;
				this.innerFunctionValue = new OdbcModule.ExtensionQueryFunctionValue(host);
			}

			// Token: 0x170011EC RID: 4588
			// (get) Token: 0x060030AB RID: 12459 RVA: 0x00092F67 File Offset: 0x00091167
			public override string PrimaryResourceKind
			{
				get
				{
					return "Odbc";
				}
			}

			// Token: 0x060030AC RID: 12460 RVA: 0x000935FA File Offset: 0x000917FA
			public override TableValue TypedInvoke(Value connectionProperties, TextValue query, Value options)
			{
				return this.innerFunctionValue.TypedInvoke(connectionProperties, query, options);
			}

			// Token: 0x060030AD RID: 12461 RVA: 0x0009360A File Offset: 0x0009180A
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return this.innerFunctionValue.TryGetLocation(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x0400156D RID: 5485
			private static readonly TypeValue optionsType = OdbcModule.QueryOptionRecord.CreateRecordType().Nullable;

			// Token: 0x0400156E RID: 5486
			private readonly IEngineHost host;

			// Token: 0x0400156F RID: 5487
			private readonly OdbcModule.ExtensionQueryFunctionValue innerFunctionValue;
		}

		// Token: 0x02000609 RID: 1545
		private sealed class DataSourceFunctionValue : NativeFunctionValue2<TableValue, Value, Value>
		{
			// Token: 0x060030AF RID: 12463 RVA: 0x00093632 File Offset: 0x00091832
			public DataSourceFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "connectionString", TypeValue.Any, "options", OdbcModule.DataSourceFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x170011ED RID: 4589
			// (get) Token: 0x060030B0 RID: 12464 RVA: 0x00092F67 File Offset: 0x00091167
			public override string PrimaryResourceKind
			{
				get
				{
					return "Odbc";
				}
			}

			// Token: 0x060030B1 RID: 12465 RVA: 0x0009365C File Offset: 0x0009185C
			public override TableValue TypedInvoke(Value connectionProperties, Value options)
			{
				bool throwOnFoldingFailure = this.host.QueryService<IFoldingFailureService>().ThrowOnFoldingFailure;
				OdbcOptions odbcOptions = OdbcOptions.CreateDataSourceOptionsFromValue(options, this.host);
				OdbcDataSource odbcDataSource = OdbcModule.CreateDataSource(this.host, connectionProperties, odbcOptions);
				OdbcSqlExpressionGenerator odbcSqlExpressionGenerator = new UserOverrideOdbcSqlExpressionGenerator(odbcDataSource, odbcOptions);
				OdbcQueryExpressionVisitorFactory odbcQueryExpressionVisitorFactory = UserOverrideOdbcQueryExpressionFactory.New(odbcSqlExpressionGenerator);
				OdbcMultiLevelNavigationProvider odbcMultiLevelNavigationProvider = new OdbcMultiLevelNavigationProvider(new OdbcQueryDomain(odbcDataSource, odbcSqlExpressionGenerator, odbcQueryExpressionVisitorFactory, odbcOptions.UseSoftNumbers, odbcOptions.HideNativeQuery, throwOnFoldingFailure, odbcOptions.TolerateConcatOverflow, null), odbcDataSource.TableTypes, odbcOptions.CreateNavigationProperties, odbcOptions.SupportsIncrementalNavigation, !odbcOptions.HierarchicalNavigation);
				if (odbcOptions.HierarchicalNavigation)
				{
					return HierarchicalNavigationQuery.New(this.host, odbcMultiLevelNavigationProvider);
				}
				return new FlatNavigationTableValue(odbcMultiLevelNavigationProvider);
			}

			// Token: 0x060030B2 RID: 12466 RVA: 0x00093704 File Offset: 0x00091904
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				IExpression expression2;
				if (OdbcModule.DataSourceFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetValue("connection", out expression2))
				{
					Value value = ExpressionAnalysis.GetValue(expression2);
					Keys empty = Keys.Empty;
					if (value.IsRecord)
					{
						value = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out empty);
					}
					if (empty.Length == 0 && OdbcModule.TryGetLocation(value, out location))
					{
						location.TrySetAddressString("object", dictionary, "item");
						location.TrySetAddressString("schema", dictionary, "schema");
						location.TrySetAddressString("database", dictionary, "catalog");
						foundOptions = OdbcModule.DataSourceFunctionValue.GetOptions(dictionary, out unknownOptions);
						return true;
					}
				}
				else if (OdbcModule.DataSourceFunctionValue.hierarchicalPattern.TryMatch(expression, out dictionary) && dictionary.TryGetValue("connection", out expression2))
				{
					Value value2 = ExpressionAnalysis.GetValue(expression2);
					Keys empty2 = Keys.Empty;
					if (value2.IsRecord)
					{
						value2 = ExpressionAnalysis.RemovePlaceholders(value2.AsRecord, out empty2);
					}
					foundOptions = OdbcModule.DataSourceFunctionValue.GetOptions(dictionary, out unknownOptions);
					Value value3;
					if (empty2.Length == 0 && OdbcModule.TryGetLocation(value2, out location) && foundOptions.TryGetValue("HierarchicalNavigation", out value3) && value3.IsLogical && value3.AsBoolean && OdbcModule.DataSourceFunctionValue.TryAddToLocation(dictionary, "name0", "kind0", location, false) && OdbcModule.DataSourceFunctionValue.TryAddToLocation(dictionary, "name1", "kind1", location, true) && OdbcModule.DataSourceFunctionValue.TryAddToLocation(dictionary, "name2", "kind2", location, true))
					{
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x060030B3 RID: 12467 RVA: 0x00093888 File Offset: 0x00091A88
			private static RecordValue GetOptions(Dictionary<string, IExpression> captures, out Keys unknownOptions)
			{
				IExpression expression;
				if (captures.TryGetValue("options", out expression))
				{
					RecordValue recordValue = ExpressionAnalysis.GetRecord(expression);
					if (recordValue != null)
					{
						recordValue = ExpressionAnalysis.RemovePlaceholders(recordValue, out unknownOptions);
					}
					else
					{
						unknownOptions = Keys.Empty;
					}
					return recordValue;
				}
				unknownOptions = Keys.Empty;
				return RecordValue.Empty;
			}

			// Token: 0x060030B4 RID: 12468 RVA: 0x000938D0 File Offset: 0x00091AD0
			private static bool TryAddToLocation(Dictionary<string, IExpression> captures, string nameName, string kindName, IDataSourceLocation location, bool canBeMissing)
			{
				string text;
				Value value;
				if (!captures.TryGetStringConstant(nameName, out text) || !captures.TryGetConstant(kindName, out value))
				{
					return canBeMissing;
				}
				if (value.Equals(HierarchyItem.DatabaseKindValue))
				{
					location.Address["database"] = text;
				}
				else if (value.Equals(HierarchyItem.SchemaKindValue))
				{
					location.Address["schema"] = text;
				}
				else
				{
					if (!value.Equals(HierarchyItem.TableKindValue) && !value.Equals(HierarchyItem.ViewKindValue))
					{
						return false;
					}
					location.Address["object"] = text;
				}
				return true;
			}

			// Token: 0x04001570 RID: 5488
			private static readonly TypeValue optionsType = OdbcModule.PublicOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04001571 RID: 5489
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "Table.SelectRows(__func(__connection, _o_options), each _[Catalog]=__catalog and _[Schema]=__schema)", "Table.SelectRows(__func(__connection, _o_options), each _[Catalog]=__catalog)", "Table.SelectRows(__func(__connection, _o_options), each _[Schema]=__schema)", "__func(__connection, _o_options){[Catalog=_o_catalog,Schema=_o_schema,Item=__item]}[Data]", "__func(__connection, _o_options)" });

			// Token: 0x04001572 RID: 5490
			private static readonly ExpressionPattern hierarchicalPattern = new ExpressionPattern(new string[] { "__func(__connection, __options){[Name=__name0,Kind=__kind0]}[Data]{[Name=__name1,Kind=__kind1]}[Data]{[Name=__name2,Kind=__kind2]}[Data]", "__func(__connection, __options){[Name=__name0,Kind=__kind0]}[Data]{[Name=__name1,Kind=__kind1]}[Data]", "__func(__connection, __options){[Name=__name0,Kind=__kind0]}[Data]" });

			// Token: 0x04001573 RID: 5491
			private readonly IEngineHost host;
		}

		// Token: 0x0200060A RID: 1546
		public static class LimitClauseKindValue
		{
			// Token: 0x04001574 RID: 5492
			public static readonly IntEnumTypeValue<LimitClauseKind.LimitClauseKindType> KindType = new IntEnumTypeValue<LimitClauseKind.LimitClauseKindType>("LimitClauseKind.Type");

			// Token: 0x04001575 RID: 5493
			public static readonly NumberValue None = OdbcModule.LimitClauseKindValue.KindType.NewEnumValue("LimitClauseKind.None", 0, LimitClauseKind.LimitClauseKindType.None, null);

			// Token: 0x04001576 RID: 5494
			public static readonly NumberValue Top = OdbcModule.LimitClauseKindValue.KindType.NewEnumValue("LimitClauseKind.Top", 1, LimitClauseKind.LimitClauseKindType.Top, null);

			// Token: 0x04001577 RID: 5495
			public static readonly NumberValue LimitOffset = OdbcModule.LimitClauseKindValue.KindType.NewEnumValue("LimitClauseKind.LimitOffset", 2, LimitClauseKind.LimitClauseKindType.LimitOffset, null);

			// Token: 0x04001578 RID: 5496
			public static readonly NumberValue Limit = OdbcModule.LimitClauseKindValue.KindType.NewEnumValue("LimitClauseKind.Limit", 3, LimitClauseKind.LimitClauseKindType.Limit, null);

			// Token: 0x04001579 RID: 5497
			public static readonly NumberValue AnsiSql2008 = OdbcModule.LimitClauseKindValue.KindType.NewEnumValue("LimitClauseKind.AnsiSql2008", 4, LimitClauseKind.LimitClauseKindType.AnsiSql2008, null);
		}

		// Token: 0x0200060B RID: 1547
		private sealed class InferOptionsFunctionValue : NativeFunctionValue1<RecordValue, Value>
		{
			// Token: 0x060030B7 RID: 12471 RVA: 0x00093A7B File Offset: 0x00091C7B
			public InferOptionsFunctionValue(IEngineHost host)
				: base(TypeValue.Record, "connectionString", TypeValue.Any)
			{
				this.host = host;
			}

			// Token: 0x170011EE RID: 4590
			// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00092F67 File Offset: 0x00091167
			public override string PrimaryResourceKind
			{
				get
				{
					return "Odbc";
				}
			}

			// Token: 0x060030B9 RID: 12473 RVA: 0x00093A9C File Offset: 0x00091C9C
			public override RecordValue TypedInvoke(Value connectionProperties)
			{
				OdbcDataSource odbcDataSource = OdbcModule.CreateDataSource(this.host, connectionProperties, OdbcOptions.Empty);
				return RecordValue.New(Keys.New("SqlCapabilities"), new Value[] { RecordValue.New(Keys.New("LimitClauseKind"), new Value[] { LimitClauseKind.InferLimitClauseKind(odbcDataSource) }) });
			}

			// Token: 0x060030BA RID: 12474 RVA: 0x00093AF4 File Offset: 0x00091CF4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length == 1)
				{
					Keys empty = Keys.Empty;
					if (argumentValues[0].IsRecord)
					{
						argumentValues[0] = ExpressionAnalysis.RemovePlaceholders(argumentValues[0].AsRecord, out empty);
					}
					if (empty.Length == 0 && OdbcModule.TryGetLocation(argumentValues[0], out location))
					{
						foundOptions = RecordValue.Empty;
						unknownOptions = Keys.Empty;
						return true;
					}
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x0400157A RID: 5498
			private readonly IEngineHost host;
		}
	}
}
