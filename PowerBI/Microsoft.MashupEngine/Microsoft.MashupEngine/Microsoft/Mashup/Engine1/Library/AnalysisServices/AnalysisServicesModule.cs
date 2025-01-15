using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F21 RID: 3873
	public sealed class AnalysisServicesModule : Module
	{
		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06006692 RID: 26258 RVA: 0x00160F2B File Offset: 0x0015F12B
		public override string Name
		{
			get
			{
				return "AnalysisServices";
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06006693 RID: 26259 RVA: 0x00160F32 File Offset: 0x0015F132
		public override Keys ExportKeys
		{
			get
			{
				if (AnalysisServicesModule.exportKeys == null)
				{
					AnalysisServicesModule.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "AnalysisServices.Databases";
						}
						if (index != 1)
						{
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
						return "AnalysisServices.Database";
					});
				}
				return AnalysisServicesModule.exportKeys;
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06006694 RID: 26260 RVA: 0x00160F6A File Offset: 0x0015F16A
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AnalysisServicesResource.ResourceKindInfo };
			}
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x00160F7C File Offset: 0x0015F17C
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AnalysisServicesModule.DatabasesFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
				return new AnalysisServicesModule.DatabaseFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x00160FB0 File Offset: 0x0015F1B0
		private static bool TryGetKeyValue(Value key, out string value)
		{
			Value value2;
			if (key.IsRecord && key.AsRecord.TryGetValue("Name", out value2) && value2.IsText)
			{
				value = value2.AsString;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x00160FEF File Offset: 0x0015F1EF
		private static AnalysisServicesService GetService(IEngineHost host, OptionsRecord options, string serverName, string catalogName)
		{
			return new AnalysisServicesService(host, options, serverName, catalogName);
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x00160FFC File Offset: 0x0015F1FC
		private static bool TryConvertSubQueries(Value optionValue, out object value)
		{
			int num;
			if (optionValue.IsNumber && optionValue.AsNumber.TryGetInt32(out num) && num >= 0 && num <= 2)
			{
				value = num;
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x04003859 RID: 14425
		public const string DataSourceName = "AnalysisServices";

		// Token: 0x0400385A RID: 14426
		public const string DatabaseFunctionName = "AnalysisServices.Database";

		// Token: 0x0400385B RID: 14427
		public const string DatabasesFunctionName = "AnalysisServices.Databases";

		// Token: 0x0400385C RID: 14428
		internal static readonly OptionRecordDefinition DatabaseOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "MDX"),
			new OptionItem("TypedMeasureColumns", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null),
			new OptionItem("Culture", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "AS"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, "AS"),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("SubQueries", NullableTypeValue.Number, NumberValue.New(2), OptionItemOption.None, new TryConvertOption(AnalysisServicesModule.TryConvertSubQueries), null),
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "AS"),
			new OptionItem("SupportsProperties", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x0400385D RID: 14429
		internal static readonly OptionRecordDefinition DatabasesOptionRecord = AnalysisServicesModule.DatabaseOptionRecord.Remove(new string[] { "Query" });

		// Token: 0x0400385E RID: 14430
		internal static readonly TypeValue PublicDatabaseOptionsType = AnalysisServicesModule.DatabaseOptionRecord.Remove(new string[] { "SupportsProperties" }).CreateRecordType().Nullable;

		// Token: 0x0400385F RID: 14431
		internal static readonly TypeValue PublicDatabasesOptionsType = AnalysisServicesModule.DatabasesOptionRecord.Remove(new string[] { "SupportsProperties" }).CreateRecordType().Nullable;

		// Token: 0x04003860 RID: 14432
		private static Keys exportKeys;

		// Token: 0x02000F22 RID: 3874
		private enum Exports
		{
			// Token: 0x04003862 RID: 14434
			AnalysisServices_Databases,
			// Token: 0x04003863 RID: 14435
			AnalysisServices_Database,
			// Token: 0x04003864 RID: 14436
			Count
		}

		// Token: 0x02000F23 RID: 3875
		private sealed class DatabasesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x0600669B RID: 26267 RVA: 0x001611A4 File Offset: 0x0015F3A4
			public DatabasesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "server", TypeValue.Text, "options", AnalysisServicesModule.PublicDatabasesOptionsType)
			{
				this.host = host;
			}

			// Token: 0x0600669C RID: 26268 RVA: 0x001611D0 File Offset: 0x0015F3D0
			public override TableValue TypedInvoke(TextValue server, Value options)
			{
				OptionsRecord optionsRecord = AnalysisServicesModule.DatabasesOptionRecord.CreateOptions("AnalysisServices", options);
				return new AnalysisServicesModule.DatabasesTableValue(this.host, optionsRecord, server.AsString);
			}

			// Token: 0x17001DB9 RID: 7609
			// (get) Token: 0x0600669D RID: 26269 RVA: 0x00160F2B File Offset: 0x0015F12B
			public override string PrimaryResourceKind
			{
				get
				{
					return "AnalysisServices";
				}
			}

			// Token: 0x0600669E RID: 26270 RVA: 0x00161200 File Offset: 0x0015F400
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (!AnalysisServicesModule.DatabasesFunctionValue.pattern.TryMatch(expression, out dictionary) || !dictionary.TryGetConstant("server", out value))
				{
					location = null;
					foundOptions = null;
					unknownOptions = null;
					return false;
				}
				IExpression @null;
				if (!dictionary.TryGetValue("options", out @null))
				{
					@null = ConstantExpressionSyntaxNode.Null;
				}
				bool? flag;
				if (!StaticAnalysisResolver.TryGetRelationalLocation<AnalysisServicesDataSourceLocation>(value, @null, out location, out foundOptions, out unknownOptions, out flag))
				{
					return false;
				}
				Value value2;
				if (dictionary.TryGetConstant("database", out value2) && value2.IsText)
				{
					location.Address["database"] = value2.AsString;
				}
				return true;
			}

			// Token: 0x04003865 RID: 14437
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, _o_options)", "__func(__server, _o_options){[Name=__database]}[Data]" });

			// Token: 0x04003866 RID: 14438
			private readonly IEngineHost host;
		}

		// Token: 0x02000F24 RID: 3876
		private sealed class DatabaseFunctionValue : NativeFunctionValue3<TableValue, TextValue, TextValue, Value>
		{
			// Token: 0x060066A0 RID: 26272 RVA: 0x001612B0 File Offset: 0x0015F4B0
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 2, "server", TypeValue.Text, "database", TypeValue.Text, "options", AnalysisServicesModule.PublicDatabaseOptionsType)
			{
				this.host = host;
			}

			// Token: 0x060066A1 RID: 26273 RVA: 0x001612F0 File Offset: 0x0015F4F0
			public override TableValue TypedInvoke(TextValue server, TextValue database, Value options)
			{
				OptionsRecord optionsRecord = AnalysisServicesModule.DatabaseOptionRecord.CreateOptions("AnalysisServices", options);
				Func<TableValue> nonQueryCreator = delegate
				{
					RecordValue recordValue = RecordValue.New(AnalysisServicesModule.DatabaseFunctionValue.keys, new Value[] { database.AsText });
					return new AnalysisServicesModule.DatabasesTableValue(this.host, optionsRecord, server.AsString)[recordValue]["Data"].AsTable;
				};
				string text;
				if (!optionsRecord.TryGetString("Query", out text))
				{
					return nonQueryCreator();
				}
				AnalysisServicesService service = AnalysisServicesModule.GetService(this.host, optionsRecord, server.AsString, database.AsString);
				return new AnalysisServicesNativeQueryTableValue(this.host, text, service, delegate
				{
					nonQueryCreator().TestConnection();
				});
			}

			// Token: 0x17001DBA RID: 7610
			// (get) Token: 0x060066A2 RID: 26274 RVA: 0x00160F2B File Offset: 0x0015F12B
			public override string PrimaryResourceKind
			{
				get
				{
					return "AnalysisServices";
				}
			}

			// Token: 0x060066A3 RID: 26275 RVA: 0x001613A4 File Offset: 0x0015F5A4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				Value value2;
				if (AnalysisServicesModule.DatabaseFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("server", out value) && dictionary.TryGetConstant("database", out value2))
				{
					IExpression @null;
					if (!dictionary.TryGetValue("options", out @null))
					{
						@null = ConstantExpressionSyntaxNode.Null;
					}
					bool? flag;
					return StaticAnalysisResolver.TryGetRelationalLocation<AnalysisServicesDataSourceLocation>(value, value2, @null, out location, out foundOptions, out unknownOptions, out flag);
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04003867 RID: 14439
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__server, __database, _o_options)" });

			// Token: 0x04003868 RID: 14440
			private static readonly Keys keys = Keys.New("Name");

			// Token: 0x04003869 RID: 14441
			private readonly IEngineHost host;
		}

		// Token: 0x02000F26 RID: 3878
		private sealed class DatabasesTableValue : TableValue
		{
			// Token: 0x060066A8 RID: 26280 RVA: 0x001614B0 File Offset: 0x0015F6B0
			public DatabasesTableValue(IEngineHost host, OptionsRecord options, string serverName)
			{
				this.host = host;
				this.serverName = serverName;
				this.service = AnalysisServicesModule.GetService(host, options, serverName, null);
				this.options = options;
			}

			// Token: 0x17001DBB RID: 7611
			// (get) Token: 0x060066A9 RID: 26281 RVA: 0x001614DC File Offset: 0x0015F6DC
			public override TypeValue Type
			{
				get
				{
					return AnalysisServicesModule.DatabasesTableValue.type;
				}
			}

			// Token: 0x060066AA RID: 26282 RVA: 0x001614E4 File Offset: 0x0015F6E4
			public override bool TryGetValue(Value key, out Value value)
			{
				string text;
				if (AnalysisServicesModule.TryGetKeyValue(key, out text))
				{
					value = this.CreateRow(this.service.GetCatalog(text));
					return true;
				}
				return base.TryGetValue(key, out value);
			}

			// Token: 0x060066AB RID: 26283 RVA: 0x00161519 File Offset: 0x0015F719
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (AnalysisServicesCatalog analysisServicesCatalog in this.service.Catalogs)
				{
					yield return this.CreateRow(analysisServicesCatalog);
				}
				IEnumerator<AnalysisServicesCatalog> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x060066AC RID: 26284 RVA: 0x00161528 File Offset: 0x0015F728
			private RecordValue CreateRow(AnalysisServicesCatalog catalog)
			{
				return RecordValue.New(this.Columns, new IValueReference[]
				{
					TextValue.New(catalog.Name),
					this.CreateCatalog(catalog),
					TextValue.New("Database")
				});
			}

			// Token: 0x060066AD RID: 26285 RVA: 0x00161560 File Offset: 0x0015F760
			private IValueReference CreateCatalog(AnalysisServicesCatalog catalog)
			{
				bool flag = false;
				string text;
				if (this.options.TryGetString("Implementation", out text))
				{
					flag = text == AnalysisServicesModule.DatabasesTableValue.semanticSetModelVersionString;
					if (!flag)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("Implementation"), null, null);
					}
				}
				return new AnalysisServicesModule.CatalogTableValue(this.host, catalog, this.options.GetBool("TypedMeasureColumns", false), flag);
			}

			// Token: 0x0400386F RID: 14447
			private static readonly string semanticSetModelVersionString = "2.0";

			// Token: 0x04003870 RID: 14448
			private readonly string serverName;

			// Token: 0x04003871 RID: 14449
			private readonly IEngineHost host;

			// Token: 0x04003872 RID: 14450
			private readonly AnalysisServicesService service;

			// Token: 0x04003873 RID: 14451
			private readonly OptionsRecord options;

			// Token: 0x04003874 RID: 14452
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValuesWithKind, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Database", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));
		}

		// Token: 0x02000F28 RID: 3880
		private class CatalogTableValue : TableValue
		{
			// Token: 0x060066B6 RID: 26294 RVA: 0x00161764 File Offset: 0x0015F964
			public CatalogTableValue(IEngineHost engineHost, AnalysisServicesCatalog catalog, bool typedMeasureColumns, bool useSemanticSetModel)
			{
				this.engineHost = engineHost;
				this.catalog = catalog;
				this.typedMeasureColumns = typedMeasureColumns;
				this.useSemanticSetModel = useSemanticSetModel;
			}

			// Token: 0x17001DBE RID: 7614
			// (get) Token: 0x060066B7 RID: 26295 RVA: 0x00161789 File Offset: 0x0015F989
			public override TypeValue Type
			{
				get
				{
					return this.CubeObjectTable.Type;
				}
			}

			// Token: 0x17001DBF RID: 7615
			// (get) Token: 0x060066B8 RID: 26296 RVA: 0x00161796 File Offset: 0x0015F996
			public override long LargeCount
			{
				get
				{
					return this.CubeObjectTable.LargeCount;
				}
			}

			// Token: 0x17001DC0 RID: 7616
			// (get) Token: 0x060066B9 RID: 26297 RVA: 0x001617A3 File Offset: 0x0015F9A3
			private TableValue CubeObjectTable
			{
				get
				{
					if (this.cubeObjectTable == null)
					{
						this.cubeObjectTable = this.CreateTable();
					}
					return this.cubeObjectTable;
				}
			}

			// Token: 0x060066BA RID: 26298 RVA: 0x001617C0 File Offset: 0x0015F9C0
			private TableValue CreateTable()
			{
				Dictionary<string, CubeObjectTableBuilder> dictionary = new Dictionary<string, CubeObjectTableBuilder>();
				foreach (AnalysisServicesMdxCube analysisServicesMdxCube in this.catalog.Cubes)
				{
					bool flag;
					string text;
					if (analysisServicesMdxCube.BaseCubeName == null)
					{
						flag = false;
						text = analysisServicesMdxCube.Name;
					}
					else
					{
						flag = true;
						text = analysisServicesMdxCube.BaseCubeName;
					}
					CubeObjectTableBuilder cubeObjectTableBuilder;
					if (!dictionary.TryGetValue(text, out cubeObjectTableBuilder))
					{
						cubeObjectTableBuilder = CubeObjectTableBuilder.New();
						dictionary[text] = cubeObjectTableBuilder;
					}
					IValueReference valueReference = this.CreateCube(this.catalog.Service, analysisServicesMdxCube);
					if (flag)
					{
						cubeObjectTableBuilder.AddSubcube(analysisServicesMdxCube.Name, analysisServicesMdxCube.Caption, valueReference);
					}
					else
					{
						cubeObjectTableBuilder.AddCube(analysisServicesMdxCube.Name, analysisServicesMdxCube.Caption, valueReference);
					}
				}
				CubeObjectTableBuilder cubeObjectTableBuilder2 = CubeObjectTableBuilder.New();
				foreach (KeyValuePair<string, CubeObjectTableBuilder> keyValuePair in dictionary)
				{
					cubeObjectTableBuilder2.AddFolder(keyValuePair.Key, keyValuePair.Value.ToTable());
				}
				return cubeObjectTableBuilder2.ToTable();
			}

			// Token: 0x060066BB RID: 26299 RVA: 0x001618F8 File Offset: 0x0015FAF8
			private IValueReference CreateCube(AnalysisServicesService service, AnalysisServicesMdxCube cube)
			{
				return new DelayedValue(delegate
				{
					AnalysisServicesCubeContextProvider analysisServicesCubeContextProvider = new AnalysisServicesCubeContextProvider(service, cube, this.typedMeasureColumns, this.useSemanticSetModel);
					return CubeContextCubeValue.New(analysisServicesCubeContextProvider, analysisServicesCubeContextProvider.DefaultContext, Keys.Empty);
				});
			}

			// Token: 0x060066BC RID: 26300 RVA: 0x00161924 File Offset: 0x0015FB24
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.CubeObjectTable.GetEnumerator();
			}

			// Token: 0x060066BD RID: 26301 RVA: 0x00161934 File Offset: 0x0015FB34
			public override Value NativeQuery(TextValue query, Value parameters, Value options)
			{
				if (!parameters.IsNull)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.NativeQuery_NoParameters, parameters, null);
				}
				if (!options.IsNull && options.AsRecord.Count > 0)
				{
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionWithNoValidOptions(options.AsRecord.Keys[0]), options, null);
				}
				return new AnalysisServicesNativeQueryTableValue(this.engineHost, query.AsString, this.catalog.Service, new Action(this.TestConnection));
			}

			// Token: 0x04003879 RID: 14457
			private readonly IEngineHost engineHost;

			// Token: 0x0400387A RID: 14458
			private readonly AnalysisServicesCatalog catalog;

			// Token: 0x0400387B RID: 14459
			private readonly bool typedMeasureColumns;

			// Token: 0x0400387C RID: 14460
			private readonly bool useSemanticSetModel;

			// Token: 0x0400387D RID: 14461
			private TableValue cubeObjectTable;
		}
	}
}
