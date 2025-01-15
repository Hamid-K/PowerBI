using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Essbase
{
	// Token: 0x02000C71 RID: 3185
	internal sealed class EssbaseModule : Module
	{
		// Token: 0x17001A25 RID: 6693
		// (get) Token: 0x0600566A RID: 22122 RVA: 0x0012BB63 File Offset: 0x00129D63
		public override string Name
		{
			get
			{
				return "Essbase";
			}
		}

		// Token: 0x17001A26 RID: 6694
		// (get) Token: 0x0600566B RID: 22123 RVA: 0x0012BB6A File Offset: 0x00129D6A
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Essbase.Cubes";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001A27 RID: 6695
		// (get) Token: 0x0600566C RID: 22124 RVA: 0x0012BBA5 File Offset: 0x00129DA5
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { EssbaseModule.resourceKindInfo };
			}
		}

		// Token: 0x0600566D RID: 22125 RVA: 0x0012BBB8 File Offset: 0x00129DB8
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new EssbaseModule.CubesFunctionValue(host);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x0600566E RID: 22126 RVA: 0x0012BBE9 File Offset: 0x00129DE9
		private static string FormatServerInfo(string serverName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Provider=Essbase;Data Source={0}", serverName);
		}

		// Token: 0x0400309C RID: 12444
		public const string DataSourceName = "Essbase";

		// Token: 0x0400309D RID: 12445
		public const string CubesFunctionName = "Essbase.Cubes";

		// Token: 0x0400309E RID: 12446
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CommandTimeout", NullableTypeValue.Duration)
		});

		// Token: 0x0400309F RID: 12447
		private static readonly ResourceKindInfo resourceKindInfo = new UriRootResourceKindInfo("Essbase", Strings.EssbaseChallengeTitle, new AuthenticationInfo[] { ResourceHelpers.BasicAuth }, new DataSourceLocationFactory[] { EssbaseDataSourceLocation.Factory }, true);

		// Token: 0x040030A0 RID: 12448
		private Keys exportKeys;

		// Token: 0x02000C72 RID: 3186
		private enum Exports
		{
			// Token: 0x040030A2 RID: 12450
			Cubes,
			// Token: 0x040030A3 RID: 12451
			Count
		}

		// Token: 0x02000C73 RID: 3187
		private sealed class CubesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06005671 RID: 22129 RVA: 0x0012BC61 File Offset: 0x00129E61
			public CubesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "url", TypeValue.Text, "options", EssbaseModule.CubesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x17001A28 RID: 6696
			// (get) Token: 0x06005672 RID: 22130 RVA: 0x0012BB63 File Offset: 0x00129D63
			public override string PrimaryResourceKind
			{
				get
				{
					return "Essbase";
				}
			}

			// Token: 0x06005673 RID: 22131 RVA: 0x0012BC8C File Offset: 0x00129E8C
			public override TableValue TypedInvoke(TextValue url, Value options)
			{
				OptionsRecord optionsRecord = EssbaseModule.OptionRecord.CreateOptions("Essbase", options);
				IResource resource;
				this.GetApplicationHostLocation(new Value[] { url }).TryGetResource(out resource);
				return new EssbaseModule.EssbaseServersTableValue(new EssbaseService(this.host, optionsRecord, url.AsString, resource));
			}

			// Token: 0x06005674 RID: 22132 RVA: 0x0012BCDC File Offset: 0x00129EDC
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (EssbaseModule.CubesFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("url", out value))
				{
					EssbaseDataSourceLocation applicationHostLocation = this.GetApplicationHostLocation(new Value[] { value });
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2) && applicationHostLocation != null)
					{
						bool? flag;
						StaticAnalysisResolver.HandleOptions(expression2, applicationHostLocation, out foundOptions, out unknownOptions, out flag);
					}
					else
					{
						foundOptions = null;
						unknownOptions = null;
					}
					string text;
					if (dictionary.TryGetStringConstant("servername", out text) && applicationHostLocation != null)
					{
						applicationHostLocation.ServerName = text;
					}
					string text2;
					if (dictionary.TryGetStringConstant("applicationname", out text2) && applicationHostLocation != null)
					{
						applicationHostLocation.ApplicationName = text2;
					}
					location = applicationHostLocation;
					return applicationHostLocation != null;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x06005675 RID: 22133 RVA: 0x0012BD89 File Offset: 0x00129F89
			private EssbaseDataSourceLocation GetApplicationHostLocation(params Value[] args)
			{
				if (args.Length != 0)
				{
					return new EssbaseDataSourceLocation
					{
						ProviderUrl = args[0].AsString
					};
				}
				return null;
			}

			// Token: 0x040030A4 RID: 12452
			private static readonly TypeValue optionsType = EssbaseModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x040030A5 RID: 12453
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__url, _o_options)", "__func(__url, _o_options){[Name=__servername]}[Data]{[Name=__applicationname]}[Data]" });

			// Token: 0x040030A6 RID: 12454
			private readonly IEngineHost host;
		}

		// Token: 0x02000C74 RID: 3188
		private sealed class NativeQueryTableValue : TableValue
		{
			// Token: 0x06005677 RID: 22135 RVA: 0x0012BDDA File Offset: 0x00129FDA
			public NativeQueryTableValue(IEngineHost host, EssbaseService service, EssbaseServer server, string applicationName, string query)
			{
				this.host = host;
				this.service = service;
				this.server = server;
				this.applicationName = applicationName;
				this.query = query;
			}

			// Token: 0x17001A29 RID: 6697
			// (get) Token: 0x06005678 RID: 22136 RVA: 0x0012BE07 File Offset: 0x0012A007
			public override TypeValue Type
			{
				get
				{
					if (this.type == null)
					{
						this.type = DataReaderSchemaTableTableTypeValue.New(this.GetReaderCore());
					}
					return this.type;
				}
			}

			// Token: 0x06005679 RID: 22137 RVA: 0x0012BE28 File Offset: 0x0012A028
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				IEnumerator<IValueReference> enumerator;
				try
				{
					TableTypeValue asTableType = this.Type.AsTableType;
					IDataReader readerCore = this.GetReaderCore();
					this.reader = null;
					enumerator = new DbDataReaderEnumerator(readerCore, true, asTableType.ItemType, this.server.Info, null);
				}
				catch
				{
					if (this.reader != null)
					{
						this.reader.Dispose();
						this.reader = null;
					}
					throw;
				}
				return enumerator;
			}

			// Token: 0x0600567A RID: 22138 RVA: 0x0012BE98 File Offset: 0x0012A098
			private IDataReader GetReaderCore()
			{
				if (this.reader == null)
				{
					HostResourceQueryPermissionService.VerifyQueryPermission(this.host, this.service.Resource, QueryPermissionChallengeType.EvaluateNativeQueryUnpermitted, this.query);
					return this.service.ExecuteMdx(this.server.Info, this.applicationName, this.query, null);
				}
				return this.reader;
			}

			// Token: 0x040030A7 RID: 12455
			private readonly IEngineHost host;

			// Token: 0x040030A8 RID: 12456
			private readonly EssbaseService service;

			// Token: 0x040030A9 RID: 12457
			private readonly EssbaseServer server;

			// Token: 0x040030AA RID: 12458
			private readonly string applicationName;

			// Token: 0x040030AB RID: 12459
			private readonly string query;

			// Token: 0x040030AC RID: 12460
			private IDataReader reader;

			// Token: 0x040030AD RID: 12461
			private TableTypeValue type;
		}

		// Token: 0x02000C75 RID: 3189
		private sealed class EssbaseServersTableValue : TableValue
		{
			// Token: 0x17001A2A RID: 6698
			// (get) Token: 0x0600567B RID: 22139 RVA: 0x0012BEF4 File Offset: 0x0012A0F4
			public override TypeValue Type
			{
				get
				{
					return EssbaseModule.EssbaseServersTableValue.type;
				}
			}

			// Token: 0x0600567C RID: 22140 RVA: 0x0012BEFB File Offset: 0x0012A0FB
			public EssbaseServersTableValue(EssbaseService service)
			{
				this.service = service;
			}

			// Token: 0x0600567D RID: 22141 RVA: 0x0012BF0A File Offset: 0x0012A10A
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				List<Tuple<string, string, string>> list = EssbaseXmlaParser.ParseSourcesResponse(this.service.ExecuteDiscoverRequest("DISCOVER_DATASOURCES", "", "", "", null));
				if (list.Count == 0)
				{
					list.Add(new Tuple<string, string, string>(EssbaseModule.FormatServerInfo("localhost"), "OAC", null));
				}
				IEnumerable<EssbaseServer> enumerable = list.Select((Tuple<string, string, string> server) => new EssbaseServer(server.Item2, server.Item1, this.service));
				foreach (EssbaseServer essbaseServer in enumerable)
				{
					yield return this.CreateRow(essbaseServer);
				}
				IEnumerator<EssbaseServer> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600567E RID: 22142 RVA: 0x0012BF1C File Offset: 0x0012A11C
			private RecordValue CreateRow(EssbaseServer server)
			{
				return RecordValue.New(EssbaseModule.EssbaseServersTableValue.recordType, new IValueReference[]
				{
					TextValue.New(server.Name),
					new DelayedValue(() => new EssbaseModule.ApplicationsTableValue(this.service, server))
				});
			}

			// Token: 0x040030AE RID: 12462
			private static readonly RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false)
			}));

			// Token: 0x040030AF RID: 12463
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(EssbaseModule.EssbaseServersTableValue.recordType, new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x040030B0 RID: 12464
			private readonly EssbaseService service;
		}

		// Token: 0x02000C78 RID: 3192
		private sealed class ApplicationsTableValue : TableValue
		{
			// Token: 0x17001A2D RID: 6701
			// (get) Token: 0x0600568A RID: 22154 RVA: 0x0012C194 File Offset: 0x0012A394
			public override TypeValue Type
			{
				get
				{
					return EssbaseModule.ApplicationsTableValue.type;
				}
			}

			// Token: 0x0600568B RID: 22155 RVA: 0x0012C19B File Offset: 0x0012A39B
			public ApplicationsTableValue(EssbaseService service, EssbaseServer server)
			{
				this.service = service;
				this.server = server;
			}

			// Token: 0x0600568C RID: 22156 RVA: 0x0012C1B1 File Offset: 0x0012A3B1
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (EssbaseApplication essbaseApplication in this.server.GetApplications())
				{
					yield return this.CreateRow(essbaseApplication);
				}
				IEnumerator<EssbaseApplication> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600568D RID: 22157 RVA: 0x0012C1C0 File Offset: 0x0012A3C0
			private RecordValue CreateRow(EssbaseApplication application)
			{
				return RecordValue.New(EssbaseModule.ApplicationsTableValue.recordType, new IValueReference[]
				{
					TextValue.New(application.Name),
					new DelayedValue(() => new EssbaseModule.CubesTableValue(this.service, this.server, application))
				});
			}

			// Token: 0x040030B7 RID: 12471
			private static readonly RecordTypeValue recordType = RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false)
			}));

			// Token: 0x040030B8 RID: 12472
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(EssbaseModule.ApplicationsTableValue.recordType, new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x040030B9 RID: 12473
			private readonly EssbaseService service;

			// Token: 0x040030BA RID: 12474
			private readonly EssbaseServer server;
		}

		// Token: 0x02000C7B RID: 3195
		private sealed class CubesTableValue : TableValue
		{
			// Token: 0x17001A30 RID: 6704
			// (get) Token: 0x06005698 RID: 22168 RVA: 0x0012C3D0 File Offset: 0x0012A5D0
			public override TypeValue Type
			{
				get
				{
					return EssbaseModule.CubesTableValue.type;
				}
			}

			// Token: 0x06005699 RID: 22169 RVA: 0x0012C3D7 File Offset: 0x0012A5D7
			public CubesTableValue(EssbaseService service, EssbaseServer server, EssbaseApplication application)
			{
				this.service = service;
				this.server = server;
				this.application = application;
			}

			// Token: 0x0600569A RID: 22170 RVA: 0x0012C3F4 File Offset: 0x0012A5F4
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (EssbaseCube essbaseCube in this.application.GetCubes(this.server))
				{
					yield return this.CreateRow(essbaseCube);
				}
				IEnumerator<EssbaseCube> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x0600569B RID: 22171 RVA: 0x0012C404 File Offset: 0x0012A604
			private RecordValue CreateRow(EssbaseCube cube)
			{
				return RecordValue.New(this.Type.AsTableType.ItemType, new IValueReference[]
				{
					TextValue.New(cube.Name),
					new DelayedValue(delegate
					{
						EssbaseCubeContextProvider essbaseCubeContextProvider = new EssbaseCubeContextProvider(this.server, this.application, cube, this.service);
						CubeContext cubeContext;
						essbaseCubeContextProvider.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(cube.MdxIdentifier)), EmptyArray<ParameterArguments>.Instance, out cubeContext);
						return CubeContextCubeValue.New(essbaseCubeContextProvider, cubeContext, Keys.Empty);
					}),
					TextValue.New("Cube")
				});
			}

			// Token: 0x0600569C RID: 22172 RVA: 0x0012C474 File Offset: 0x0012A674
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
				return new EssbaseModule.NativeQueryTableValue(this.service.Host, this.service, this.server, this.application.Name, query.AsString);
			}

			// Token: 0x040030C1 RID: 12481
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValuesWithKind, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Cube", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x040030C2 RID: 12482
			private readonly EssbaseService service;

			// Token: 0x040030C3 RID: 12483
			private readonly EssbaseServer server;

			// Token: 0x040030C4 RID: 12484
			private readonly EssbaseApplication application;
		}
	}
}
