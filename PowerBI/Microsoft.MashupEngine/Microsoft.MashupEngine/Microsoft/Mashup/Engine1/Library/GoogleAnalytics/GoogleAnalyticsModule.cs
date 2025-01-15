using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.GoogleAnalytics
{
	// Token: 0x02000B18 RID: 2840
	internal sealed class GoogleAnalyticsModule : Module
	{
		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06004EBC RID: 20156 RVA: 0x00105113 File Offset: 0x00103313
		public override string Name
		{
			get
			{
				return "GoogleAnalytics";
			}
		}

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x000020FA File Offset: 0x000002FA
		public override string Location
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x0010511A File Offset: 0x0010331A
		public override Keys ExportKeys
		{
			get
			{
				if (GoogleAnalyticsModule.exportKeys == null)
				{
					GoogleAnalyticsModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "GoogleAnalytics.Accounts";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return GoogleAnalyticsModule.exportKeys;
			}
		}

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x00105152 File Offset: 0x00103352
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { GoogleAnalyticsModule.resourceKindInfo };
			}
		}

		// Token: 0x06004EC0 RID: 20160 RVA: 0x00105164 File Offset: 0x00103364
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new GoogleAnalyticsModule.AccountsFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04002A5C RID: 10844
		private static readonly Dictionary<string, Predicate<Value>> OptionValidation = new Dictionary<string, Predicate<Value>> { 
		{
			"Implementation",
			(Value value) => value.IsText && value.AsText.AsString == "2.0"
		} };

		// Token: 0x04002A5D RID: 10845
		public const string DataSourceNameString = "GoogleAnalytics";

		// Token: 0x04002A5E RID: 10846
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "GoogleAnalytics")
		});

		// Token: 0x04002A5F RID: 10847
		public static readonly string[] GoogleScopes = new string[] { "https://www.googleapis.com/auth/analytics.readonly" };

		// Token: 0x04002A60 RID: 10848
		private static readonly ResourceKindInfo resourceKindInfo = new SingletonResourceKindInfo("GoogleAnalytics", "GoogleAnalytics", Strings.GoogleAnalyticsChallengeTitle, new AuthenticationInfo[]
		{
			new OAuth2AuthenticationInfo
			{
				Label = Strings.GoogleAnalyticsChallengeTitle,
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory((OAuthServices services, OAuthClientApplication app, string url) => new GoogleOAuthProvider(services, app, GoogleAnalyticsModule.GoogleScopes))
			}
		}, null, new GoogleAnalyticsServerUrl().V1ServerBaseUrl.Host, false, false, new DataSourceLocationFactory[] { GoogleAnalyticsDataSourceLocation.Factory });

		// Token: 0x04002A61 RID: 10849
		private static readonly Keys NavigationTableWithIdKindKeys = Keys.New("Id", "Name", "Data", "Kind");

		// Token: 0x04002A62 RID: 10850
		private static Keys exportKeys;

		// Token: 0x02000B19 RID: 2841
		private enum Exports
		{
			// Token: 0x04002A64 RID: 10852
			GoogleAnalytics_Accounts,
			// Token: 0x04002A65 RID: 10853
			Count
		}

		// Token: 0x02000B1A RID: 2842
		private sealed class AccountsFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x06004EC3 RID: 20163 RVA: 0x001052A7 File Offset: 0x001034A7
			public AccountsFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "options", GoogleAnalyticsModule.AccountsFunctionValue.OptionType)
			{
				this.host = host;
			}

			// Token: 0x06004EC4 RID: 20164 RVA: 0x001052C8 File Offset: 0x001034C8
			public override TableValue TypedInvoke(Value options)
			{
				if (!options.IsNull)
				{
					this.ValidateOptions(options);
					Value value;
					if (options.AsRecord.TryGetValue("Implementation", out value) && value.IsText && value.AsString == "2.0")
					{
						return new GoogleAnalyticsModule.AccountsTableValue(new GoogleAnalyticsServiceV2(this.host));
					}
				}
				return new GoogleAnalyticsModule.AccountsTableValue(new GoogleAnalyticsServiceV1(this.host));
			}

			// Token: 0x06004EC5 RID: 20165 RVA: 0x00105334 File Offset: 0x00103534
			private void ValidateOptions(Value options)
			{
				RecordValue asRecord = options.AsRecord;
				foreach (string text in asRecord.Keys)
				{
					Predicate<Value> predicate;
					if (!GoogleAnalyticsModule.OptionValidation.TryGetValue(text, out predicate))
					{
						throw ValueException.NewExpressionError<Message1>(Strings.OptionsRecord_UnsupportedKey(text), null, null);
					}
					Value value = asRecord[text];
					if (!predicate(value))
					{
						throw ValueException.NewExpressionError<Message2>(Strings.OptionsRecord_Unsupported(text, value), null, null);
					}
				}
			}

			// Token: 0x170018B3 RID: 6323
			// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x00105113 File Offset: 0x00103313
			public override string PrimaryResourceKind
			{
				get
				{
					return "GoogleAnalytics";
				}
			}

			// Token: 0x06004EC7 RID: 20167 RVA: 0x001053C8 File Offset: 0x001035C8
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				if (GoogleAnalyticsModule.AccountsFunctionValue.pattern.TryMatch(expression, out dictionary))
				{
					Value value = Value.Null;
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2))
					{
						value = ExpressionAnalysis.GetValue(expression2);
						if (value.IsRecord)
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownOptions);
						}
					}
					location = new GoogleAnalyticsDataSourceLocation();
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04002A66 RID: 10854
			private static readonly TypeValue OptionType = GoogleAnalyticsModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04002A67 RID: 10855
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(_o_options)" });

			// Token: 0x04002A68 RID: 10856
			private readonly IEngineHost host;
		}

		// Token: 0x02000B1B RID: 2843
		private sealed class AccountsTableValue : TableValue
		{
			// Token: 0x06004EC9 RID: 20169 RVA: 0x00105468 File Offset: 0x00103668
			public AccountsTableValue(IGoogleAnalyticsService service)
			{
				this.service = service;
			}

			// Token: 0x170018B4 RID: 6324
			// (get) Token: 0x06004ECA RID: 20170 RVA: 0x00105477 File Offset: 0x00103677
			public override TypeValue Type
			{
				get
				{
					return GoogleAnalyticsModule.AccountsTableValue.type;
				}
			}

			// Token: 0x06004ECB RID: 20171 RVA: 0x0010547E File Offset: 0x0010367E
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (GoogleAnalyticsAccount googleAnalyticsAccount in this.service.GetAccounts())
				{
					yield return this.CreateRow(googleAnalyticsAccount);
				}
				IEnumerator<GoogleAnalyticsAccount> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06004ECC RID: 20172 RVA: 0x00105490 File Offset: 0x00103690
			private RecordValue CreateRow(GoogleAnalyticsAccount account)
			{
				return RecordValue.New(this.Columns, new Value[]
				{
					TextValue.New(account.ID),
					TextValue.New(account.Name),
					new GoogleAnalyticsModule.PropertiesTableValue(account),
					TextValue.New("Database")
				});
			}

			// Token: 0x04002A69 RID: 10857
			private readonly IGoogleAnalyticsService service;

			// Token: 0x04002A6A RID: 10858
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(GoogleAnalyticsModule.NavigationTableWithIdKindKeys, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Database", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));
		}

		// Token: 0x02000B1D RID: 2845
		private sealed class PropertiesTableValue : TableValue
		{
			// Token: 0x06004ED5 RID: 20181 RVA: 0x00105684 File Offset: 0x00103884
			public PropertiesTableValue(GoogleAnalyticsAccount account)
			{
				this.account = account;
			}

			// Token: 0x170018B7 RID: 6327
			// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x00105693 File Offset: 0x00103893
			public override TypeValue Type
			{
				get
				{
					return GoogleAnalyticsModule.PropertiesTableValue.type;
				}
			}

			// Token: 0x06004ED7 RID: 20183 RVA: 0x0010569A File Offset: 0x0010389A
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (GoogleAnalyticsProperty googleAnalyticsProperty in this.account.Properties)
				{
					yield return this.CreateRow(googleAnalyticsProperty);
				}
				IEnumerator<GoogleAnalyticsProperty> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06004ED8 RID: 20184 RVA: 0x001056AC File Offset: 0x001038AC
			private RecordValue CreateRow(GoogleAnalyticsProperty property)
			{
				return RecordValue.New(this.Columns, new Value[]
				{
					TextValue.New(property.ID),
					TextValue.New(property.Name),
					new GoogleAnalyticsModule.CubesTableValue(property),
					TextValue.New("Database")
				});
			}

			// Token: 0x04002A6F RID: 10863
			private readonly GoogleAnalyticsAccount account;

			// Token: 0x04002A70 RID: 10864
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(GoogleAnalyticsModule.NavigationTableWithIdKindKeys, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Database", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));
		}

		// Token: 0x02000B1F RID: 2847
		private sealed class CubesTableValue : TableValue
		{
			// Token: 0x06004EE1 RID: 20193 RVA: 0x001058A0 File Offset: 0x00103AA0
			public CubesTableValue(GoogleAnalyticsProperty property)
			{
				this.property = property;
			}

			// Token: 0x170018BA RID: 6330
			// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x001058AF File Offset: 0x00103AAF
			public override TypeValue Type
			{
				get
				{
					return GoogleAnalyticsModule.CubesTableValue.type;
				}
			}

			// Token: 0x06004EE3 RID: 20195 RVA: 0x001058B6 File Offset: 0x00103AB6
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (GoogleAnalyticsCube googleAnalyticsCube in this.property.Views)
				{
					yield return this.CreateRow(googleAnalyticsCube);
				}
				IEnumerator<GoogleAnalyticsCube> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06004EE4 RID: 20196 RVA: 0x001058C8 File Offset: 0x00103AC8
			private RecordValue CreateRow(GoogleAnalyticsCube cube)
			{
				return RecordValue.New(this.Type.AsTableType.ItemType, new IValueReference[]
				{
					TextValue.New(cube.ViewId),
					TextValue.New(cube.Name),
					new DelayedValue(delegate
					{
						GoogleAnalyticsCubeContextProvider googleAnalyticsCubeContextProvider = new GoogleAnalyticsCubeContextProvider(cube, this.property.CreateCompiler(cube));
						return CubeContextCubeValue.New(googleAnalyticsCubeContextProvider, googleAnalyticsCubeContextProvider.DefaultContext, Keys.Empty);
					}),
					TextValue.New("Cube")
				});
			}

			// Token: 0x04002A75 RID: 10869
			private readonly GoogleAnalyticsProperty property;

			// Token: 0x04002A76 RID: 10870
			private static readonly TableTypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(GoogleAnalyticsModule.NavigationTableWithIdKindKeys, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Cube", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));
		}
	}
}
