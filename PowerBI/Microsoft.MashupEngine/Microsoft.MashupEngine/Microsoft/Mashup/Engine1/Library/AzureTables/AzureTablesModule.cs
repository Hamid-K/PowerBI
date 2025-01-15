using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureTables
{
	// Token: 0x02000EB7 RID: 3767
	public sealed class AzureTablesModule : Module
	{
		// Token: 0x17001D18 RID: 7448
		// (get) Token: 0x06006422 RID: 25634 RVA: 0x0015691D File Offset: 0x00154B1D
		public override string Name
		{
			get
			{
				return "AzureTable";
			}
		}

		// Token: 0x17001D19 RID: 7449
		// (get) Token: 0x06006423 RID: 25635 RVA: 0x00156924 File Offset: 0x00154B24
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
							return "AzureStorage.Tables";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x06006424 RID: 25636 RVA: 0x0015695F File Offset: 0x00154B5F
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { AzureTablesModule.resourceKindInfo };
			}
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x00156970 File Offset: 0x00154B70
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new AzureTablesModule.AzureStorageTableFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x040036A7 RID: 13991
		private const string Tables = "AzureStorage.Tables";

		// Token: 0x040036A8 RID: 13992
		public const string DataSourceNameString = "Azure Table Storage";

		// Token: 0x040036A9 RID: 13993
		private static readonly ResourceKindInfo resourceKindInfo = new UriRootResourceKindInfo("AzureTables", null, new AuthenticationInfo[]
		{
			new KeyAuthenticationInfo()
		}, new DataSourceLocationFactory[] { AzureTablesDataSourceLocation.Factory }, false);

		// Token: 0x040036AA RID: 13994
		private Keys exportKeys;

		// Token: 0x040036AB RID: 13995
		internal static readonly OptionRecordDefinition AzureTablesOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Timeout", NullableTypeValue.Duration)
		});

		// Token: 0x02000EB8 RID: 3768
		private enum Exports
		{
			// Token: 0x040036AD RID: 13997
			Tables,
			// Token: 0x040036AE RID: 13998
			Count
		}

		// Token: 0x02000EB9 RID: 3769
		private sealed class AzureStorageTableFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06006428 RID: 25640 RVA: 0x00156A00 File Offset: 0x00154C00
			public AzureStorageTableFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "account", TypeValue.Text, "options", AzureTablesModule.AzureStorageTableFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06006429 RID: 25641 RVA: 0x00156A2C File Offset: 0x00154C2C
			public override TableValue TypedInvoke(TextValue account, Value options)
			{
				OptionsRecord optionsRecord = AzureTablesModule.AzureTablesOptionRecord.CreateOptions("AzureStorage.Tables", options);
				AzureUtilities.ValidateTableParameters(account);
				TextValue httpUri = AzureTablesService.GetHttpUri(account, null);
				TimeSpan? timeSpan = null;
				int num;
				if (optionsRecord.TryGetDurationAsSeconds("Timeout", out num))
				{
					timeSpan = new TimeSpan?(TimeSpan.FromSeconds((double)num));
				}
				HttpResource httpResource = HttpResource.New("AzureTables", httpUri.String, true);
				return new AzureTablesValue.AzureTablesCatalogTableValue(this.host, httpUri, httpResource, timeSpan);
			}

			// Token: 0x17001D1B RID: 7451
			// (get) Token: 0x0600642A RID: 25642 RVA: 0x00156A9B File Offset: 0x00154C9B
			public override string PrimaryResourceKind
			{
				get
				{
					return "AzureTables";
				}
			}

			// Token: 0x0600642B RID: 25643 RVA: 0x00156AA4 File Offset: 0x00154CA4
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				foundOptions = RecordValue.Empty;
				unknownOptions = Keys.Empty;
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (!AzureTablesModule.AzureStorageTableFunctionValue.pattern.TryMatch(expression, out dictionary) || !dictionary.TryGetConstant("account", out value))
				{
					location = null;
					return false;
				}
				AzureUtilities.ValidateParameters(value.AsText, null);
				IResource resource = Resource.New("AzureTables", AzureTablesService.GetHttpUri(value.AsText, null).String);
				if (!AzureTablesDataSourceLocation.Factory.TryCreateFromResource(resource, false, out location))
				{
					return false;
				}
				Value value2;
				if (dictionary.TryGetConstant("options", out value2) && value2.IsRecord)
				{
					foundOptions = ExpressionAnalysis.RemovePlaceholders(value2.AsRecord, out unknownOptions);
				}
				location.TrySetAddressString("name", dictionary, "table");
				return true;
			}

			// Token: 0x040036AF RID: 13999
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__account, _o_options)", "__func(__account, _o_options){[Name=__table]}[Data]" });

			// Token: 0x040036B0 RID: 14000
			private readonly IEngineHost host;

			// Token: 0x040036B1 RID: 14001
			private static readonly TypeValue optionsType = AzureTablesModule.AzureTablesOptionRecord.CreateRecordType().Nullable;
		}
	}
}
