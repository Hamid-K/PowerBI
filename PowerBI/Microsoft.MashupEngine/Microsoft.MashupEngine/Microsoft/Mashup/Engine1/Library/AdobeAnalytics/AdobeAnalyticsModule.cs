using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V1;
using Microsoft.Mashup.Engine1.Library.AdobeAnalytics.V2;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.AdobeAnalytics
{
	// Token: 0x02000F64 RID: 3940
	internal sealed class AdobeAnalyticsModule : Module
	{
		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06006802 RID: 26626 RVA: 0x00165C32 File Offset: 0x00163E32
		public override string Name
		{
			get
			{
				return "AdobeAnalyticsCube";
			}
		}

		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06006803 RID: 26627 RVA: 0x000020FA File Offset: 0x000002FA
		public override string Location
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06006804 RID: 26628 RVA: 0x00165C39 File Offset: 0x00163E39
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
							return "AdobeAnalytics.Cubes";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06006805 RID: 26629 RVA: 0x00165C74 File Offset: 0x00163E74
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AdobeAnalyticsModule.resourceKindInfo };
			}
		}

		// Token: 0x06006806 RID: 26630 RVA: 0x00165C84 File Offset: 0x00163E84
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AdobeAnalyticsModule.CubesFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x04003938 RID: 14648
		public const string DataSourceName = "Adobe Analytics";

		// Token: 0x04003939 RID: 14649
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("MaxRetryCount", NullableTypeValue.Int32, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("RetryInterval", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "AdobeAnalytics")
		});

		// Token: 0x0400393A RID: 14650
		private static readonly ResourceKindInfo resourceKindInfo = new SingletonResourceKindInfo("AdobeAnalytics", "AdobeAnalytics", Strings.AdobeAnalyticsChallengeTitle, new AuthenticationInfo[]
		{
			new OAuth2AuthenticationInfo
			{
				Description = Strings.AdobeAnalyticsChallengeTitle,
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory((OAuthServices services, OAuthClientApplication app, string url) => new AdobeOAuthProvider(services, app))
			}
		}, null, null, false, true, new DataSourceLocationFactory[] { AdobeAnalyticsDataSourceLocation.Factory });

		// Token: 0x0400393B RID: 14651
		private static readonly Keys NavigationTableWithIdKindKeys = Keys.New("Id", "Name", "Data", "Kind");

		// Token: 0x0400393C RID: 14652
		private Keys exportKeys;

		// Token: 0x02000F65 RID: 3941
		private enum Exports
		{
			// Token: 0x0400393E RID: 14654
			AdobeAnalytics_Cubes,
			// Token: 0x0400393F RID: 14655
			Count
		}

		// Token: 0x02000F66 RID: 3942
		private sealed class CubesFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x06006809 RID: 26633 RVA: 0x00165DCF File Offset: 0x00163FCF
			public CubesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "options", AdobeAnalyticsModule.CubesFunctionValue.OptionType)
			{
				this.host = host;
			}

			// Token: 0x17001E1B RID: 7707
			// (get) Token: 0x0600680A RID: 26634 RVA: 0x00165DEE File Offset: 0x00163FEE
			public override string PrimaryResourceKind
			{
				get
				{
					return "AdobeAnalytics";
				}
			}

			// Token: 0x0600680B RID: 26635 RVA: 0x00165DF8 File Offset: 0x00163FF8
			public override TableValue TypedInvoke(Value options)
			{
				AdobeAnalyticsOptions adobeAnalyticsOptions = new AdobeAnalyticsOptions(AdobeAnalyticsModule.OptionRecord.CreateOptions("Adobe Analytics", options));
				if (adobeAnalyticsOptions.Implementation == "2.0")
				{
					AdobeAnalyticsServiceV2 adobeAnalyticsServiceV = new AdobeAnalyticsServiceV2(this.host, adobeAnalyticsOptions);
					if (adobeAnalyticsOptions.HierarchicalNavigation)
					{
						return new AdobeAnalyticsModule.CubesFunctionValue.CompaniesTableValue2(adobeAnalyticsServiceV);
					}
					return new AdobeAnalyticsModule.CubesFunctionValue.CubesTableValue2(adobeAnalyticsServiceV, null);
				}
				else
				{
					if (adobeAnalyticsOptions.Implementation != null)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue(adobeAnalyticsOptions.Implementation), null, null);
					}
					AdobeAnalyticsServiceV1 adobeAnalyticsServiceV2 = new AdobeAnalyticsServiceV1(this.host, adobeAnalyticsOptions);
					if (adobeAnalyticsOptions.HierarchicalNavigation)
					{
						return new AdobeAnalyticsModule.CubesFunctionValue.CompaniesTableValue1(adobeAnalyticsServiceV2);
					}
					return new AdobeAnalyticsModule.CubesFunctionValue.CubesTableValue1(adobeAnalyticsServiceV2, null);
				}
			}

			// Token: 0x0600680C RID: 26636 RVA: 0x00165E90 File Offset: 0x00164090
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Dictionary<string, IExpression> dictionary;
				if (AdobeAnalyticsModule.CubesFunctionValue.pattern.TryMatch(expression, out dictionary))
				{
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					Value value = Value.Null;
					IExpression expression2;
					if (dictionary.TryGetValue("options", out expression2))
					{
						value = ExpressionAnalysis.GetValue(expression2);
						if (value.IsRecord)
						{
							foundOptions = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownOptions);
						}
					}
					location = new AdobeAnalyticsDataSourceLocation();
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04003940 RID: 14656
			private static readonly TypeValue OptionType = AdobeAnalyticsModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04003941 RID: 14657
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(_o_options)" });

			// Token: 0x04003942 RID: 14658
			private readonly IEngineHost host;

			// Token: 0x02000F67 RID: 3943
			private abstract class AdobeTableValue<TRow> : TableValue
			{
				// Token: 0x0600680E RID: 26638 RVA: 0x00165F30 File Offset: 0x00164130
				public AdobeTableValue()
				{
				}

				// Token: 0x0600680F RID: 26639 RVA: 0x00165F38 File Offset: 0x00164138
				public override IEnumerator<IValueReference> GetEnumerator()
				{
					foreach (TRow trow in this.GetRowObjects())
					{
						yield return this.CreateRow(trow);
					}
					IEnumerator<TRow> enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x06006810 RID: 26640
				protected abstract IEnumerable<TRow> GetRowObjects();

				// Token: 0x06006811 RID: 26641
				protected abstract RecordValue CreateRow(TRow rowObject);

				// Token: 0x04003943 RID: 14659
				protected static readonly TableTypeValue CubeType = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(AdobeAnalyticsModule.NavigationTableWithIdKindKeys, new Value[]
				{
					RecordTypeAlgebra.NewField(TypeValue.Text, false),
					RecordTypeAlgebra.NewField(TypeValue.Text, false),
					RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Cube", false), false),
					RecordTypeAlgebra.NewField(TypeValue.Text, false)
				})), new TableKey[]
				{
					new TableKey(new int[1], true)
				}));

				// Token: 0x04003944 RID: 14660
				protected static readonly RecordTypeValue RecordType = RecordTypeValue.New(RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
				{
					RecordTypeAlgebra.NewField(TypeValue.Text, false),
					RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false)
				}));

				// Token: 0x04003945 RID: 14661
				protected static readonly TableTypeValue CompanyType = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<TRow>.RecordType, new TableKey[]
				{
					new TableKey(new int[1], true)
				}));
			}

			// Token: 0x02000F69 RID: 3945
			private abstract class AdobeTableValue1<TRow> : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<TRow>
			{
				// Token: 0x0600681A RID: 26650 RVA: 0x00166154 File Offset: 0x00164354
				public AdobeTableValue1(AdobeAnalyticsServiceV1 service)
				{
					this.service = service;
				}

				// Token: 0x0600681B RID: 26651 RVA: 0x00166163 File Offset: 0x00164363
				public override Value NativeQuery(TextValue query, Value parameters, Value options)
				{
					return new AdobeAnalyticsNativeQueryTableValueV1(query, this.service);
				}

				// Token: 0x0400394A RID: 14666
				protected readonly AdobeAnalyticsServiceV1 service;
			}

			// Token: 0x02000F6A RID: 3946
			private sealed class CompaniesTableValue1 : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue1<AdobeAnalyticsCompany>
			{
				// Token: 0x0600681C RID: 26652 RVA: 0x00166171 File Offset: 0x00164371
				public CompaniesTableValue1(AdobeAnalyticsServiceV1 service)
					: base(service)
				{
				}

				// Token: 0x17001E1E RID: 7710
				// (get) Token: 0x0600681D RID: 26653 RVA: 0x0016617A File Offset: 0x0016437A
				public override TypeValue Type
				{
					get
					{
						return AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCompany>.CompanyType;
					}
				}

				// Token: 0x0600681E RID: 26654 RVA: 0x00166181 File Offset: 0x00164381
				protected override IEnumerable<AdobeAnalyticsCompany> GetRowObjects()
				{
					return this.service.GetCompanies();
				}

				// Token: 0x0600681F RID: 26655 RVA: 0x0016618E File Offset: 0x0016438E
				protected override RecordValue CreateRow(AdobeAnalyticsCompany company)
				{
					return RecordValue.New(AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCompany>.RecordType, new Value[]
					{
						TextValue.New(company.Name),
						new AdobeAnalyticsModule.CubesFunctionValue.CubesTableValue1(this.service, company)
					});
				}
			}

			// Token: 0x02000F6B RID: 3947
			private sealed class CubesTableValue1 : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue1<AdobeAnalyticsCube>
			{
				// Token: 0x06006820 RID: 26656 RVA: 0x001661BD File Offset: 0x001643BD
				public CubesTableValue1(AdobeAnalyticsServiceV1 service, AdobeAnalyticsCompany company = null)
					: base(service)
				{
					this.company = company ?? AdobeAnalyticsCompany.NewDefaultCompany(service);
				}

				// Token: 0x17001E1F RID: 7711
				// (get) Token: 0x06006821 RID: 26657 RVA: 0x001661D7 File Offset: 0x001643D7
				public override TypeValue Type
				{
					get
					{
						return AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCube>.CubeType;
					}
				}

				// Token: 0x06006822 RID: 26658 RVA: 0x001661DE File Offset: 0x001643DE
				protected override IEnumerable<AdobeAnalyticsCube> GetRowObjects()
				{
					return this.company.Cubes;
				}

				// Token: 0x06006823 RID: 26659 RVA: 0x001661EC File Offset: 0x001643EC
				protected override RecordValue CreateRow(AdobeAnalyticsCube cube)
				{
					return RecordValue.New(this.Type.AsTableType.ItemType, new IValueReference[]
					{
						TextValue.New(cube.Id),
						TextValue.New(cube.Name),
						new DelayedValue(delegate
						{
							AdobeAnalyticsCubeContextProviderV1 adobeAnalyticsCubeContextProviderV = new AdobeAnalyticsCubeContextProviderV1(cube, this.service);
							CubeContext cubeContext;
							adobeAnalyticsCubeContextProviderV.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(cube.Id)), EmptyArray<ParameterArguments>.Instance, out cubeContext);
							return CubeContextCubeValue.New(adobeAnalyticsCubeContextProviderV, cubeContext, Keys.Empty);
						}),
						TextValue.New("Cube")
					});
				}

				// Token: 0x0400394B RID: 14667
				private readonly AdobeAnalyticsCompany company;
			}

			// Token: 0x02000F6D RID: 3949
			private abstract class AdobeTableValue2<TRow> : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<TRow>
			{
				// Token: 0x06006826 RID: 26662 RVA: 0x001662C1 File Offset: 0x001644C1
				public AdobeTableValue2(AdobeAnalyticsServiceV2 service)
				{
					this.service = service;
				}

				// Token: 0x17001E20 RID: 7712
				// (get) Token: 0x06006827 RID: 26663 RVA: 0x001662D0 File Offset: 0x001644D0
				protected virtual AdobeAnalyticsCompany Company
				{
					get
					{
						return AdobeAnalyticsCompany.NewDefaultCompany(this.service);
					}
				}

				// Token: 0x06006828 RID: 26664 RVA: 0x001662DD File Offset: 0x001644DD
				public override Value NativeQuery(TextValue query, Value parameters, Value options)
				{
					return new AdobeAnalyticsNativeQueryTableValueV2(query, this.service, this.Company.Id);
				}

				// Token: 0x0400394E RID: 14670
				protected readonly AdobeAnalyticsServiceV2 service;
			}

			// Token: 0x02000F6E RID: 3950
			private sealed class CompaniesTableValue2 : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue2<AdobeAnalyticsCompany>
			{
				// Token: 0x06006829 RID: 26665 RVA: 0x001662F6 File Offset: 0x001644F6
				public CompaniesTableValue2(AdobeAnalyticsServiceV2 service)
					: base(service)
				{
				}

				// Token: 0x17001E21 RID: 7713
				// (get) Token: 0x0600682A RID: 26666 RVA: 0x0016617A File Offset: 0x0016437A
				public override TypeValue Type
				{
					get
					{
						return AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCompany>.CompanyType;
					}
				}

				// Token: 0x0600682B RID: 26667 RVA: 0x001662FF File Offset: 0x001644FF
				protected override IEnumerable<AdobeAnalyticsCompany> GetRowObjects()
				{
					return this.service.GetCompanies();
				}

				// Token: 0x0600682C RID: 26668 RVA: 0x0016630C File Offset: 0x0016450C
				protected override RecordValue CreateRow(AdobeAnalyticsCompany company)
				{
					return RecordValue.New(AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCompany>.RecordType, new Value[]
					{
						TextValue.New(company.Name),
						new AdobeAnalyticsModule.CubesFunctionValue.CubesTableValue2(this.service, company)
					});
				}
			}

			// Token: 0x02000F6F RID: 3951
			private sealed class CubesTableValue2 : AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue2<AdobeAnalyticsCube>
			{
				// Token: 0x0600682D RID: 26669 RVA: 0x0016633B File Offset: 0x0016453B
				public CubesTableValue2(AdobeAnalyticsServiceV2 service, AdobeAnalyticsCompany company = null)
					: base(service)
				{
					this.company = company;
				}

				// Token: 0x17001E22 RID: 7714
				// (get) Token: 0x0600682E RID: 26670 RVA: 0x001661D7 File Offset: 0x001643D7
				public override TypeValue Type
				{
					get
					{
						return AdobeAnalyticsModule.CubesFunctionValue.AdobeTableValue<AdobeAnalyticsCube>.CubeType;
					}
				}

				// Token: 0x17001E23 RID: 7715
				// (get) Token: 0x0600682F RID: 26671 RVA: 0x0016634B File Offset: 0x0016454B
				protected override AdobeAnalyticsCompany Company
				{
					get
					{
						return this.company ?? base.Company;
					}
				}

				// Token: 0x06006830 RID: 26672 RVA: 0x0016635D File Offset: 0x0016455D
				protected override IEnumerable<AdobeAnalyticsCube> GetRowObjects()
				{
					return this.Company.Cubes;
				}

				// Token: 0x06006831 RID: 26673 RVA: 0x0016636C File Offset: 0x0016456C
				protected override RecordValue CreateRow(AdobeAnalyticsCube cube)
				{
					return RecordValue.New(this.Type.AsTableType.ItemType, new IValueReference[]
					{
						TextValue.New(cube.Id),
						TextValue.New(cube.Name),
						new DelayedValue(delegate
						{
							AdobeAnalyticsCubeContextProviderV2 adobeAnalyticsCubeContextProviderV = new AdobeAnalyticsCubeContextProviderV2(cube, this.service);
							CubeContext cubeContext;
							adobeAnalyticsCubeContextProviderV.TryCreateContext(new QueryCubeExpression(new IdentifierCubeExpression(cube.Id)), EmptyArray<ParameterArguments>.Instance, out cubeContext);
							return CubeContextCubeValue.New(adobeAnalyticsCubeContextProviderV, cubeContext, Keys.Empty);
						}),
						TextValue.New("Cube")
					});
				}

				// Token: 0x0400394F RID: 14671
				private readonly AdobeAnalyticsCompany company;
			}
		}
	}
}
