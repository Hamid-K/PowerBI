using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Oracle
{
	// Token: 0x02000568 RID: 1384
	internal sealed class OracleModule : Module
	{
		// Token: 0x06002C31 RID: 11313 RVA: 0x00086B91 File Offset: 0x00084D91
		private static OAuthResource AadForOracle(OAuthServices services, string url)
		{
			return AadOAuthProvider.CreateResourceForId(services, "https://analysis.windows.net/powerbi/connector/Oracle", null);
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x00086B9F File Offset: 0x00084D9F
		public override string Name
		{
			get
			{
				return "Oracle";
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06002C33 RID: 11315 RVA: 0x00086BA6 File Offset: 0x00084DA6
		public override Keys ExportKeys
		{
			get
			{
				if (OracleModule.exportKeys == null)
				{
					OracleModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Oracle.Database";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return OracleModule.exportKeys;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06002C34 RID: 11316 RVA: 0x00086BDE File Offset: 0x00084DDE
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { OracleModule.resourceKindInfo };
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00086BF0 File Offset: 0x00084DF0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new OracleModule.DatabaseFunctionValue(hostEnvironment);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x0400132E RID: 4910
		private static Keys exportKeys;

		// Token: 0x0400132F RID: 4911
		public const string OracleDatabase = "Oracle.Database";

		// Token: 0x04001330 RID: 4912
		public const string OracleResourceId = "https://analysis.windows.net/powerbi/connector/Oracle";

		// Token: 0x04001331 RID: 4913
		public static readonly OptionRecordDefinition OptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("CreateNavigationProperties", NullableTypeValue.Logical, LogicalValue.True, OptionItemOption.None, null, null),
			Options.NavigationPropertyNameGeneratorOption,
			new OptionItem("Query", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, "SQL"),
			new OptionItem("CommandTimeout", NullableTypeValue.Duration),
			new OptionItem("ConnectionTimeout", NullableTypeValue.Duration),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.None, null, null)
		});

		// Token: 0x04001332 RID: 4914
		private static readonly ResourceKindInfo resourceKindInfo = new OracleResourceKindInfo(new AuthenticationInfo[]
		{
			ResourceHelpers.WindowsAuthAlternateCredentials,
			ResourceHelpers.SqlAuth,
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Required,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => OracleModule.AadForOracle(services, url), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, OracleModule.AadForOracle(services, url)))
			}
		});

		// Token: 0x02000569 RID: 1385
		private enum Exports
		{
			// Token: 0x04001334 RID: 4916
			Database,
			// Token: 0x04001335 RID: 4917
			Count
		}

		// Token: 0x0200056A RID: 1386
		private sealed class DatabaseFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x06002C38 RID: 11320 RVA: 0x00086D19 File Offset: 0x00084F19
			public DatabaseFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "server", TypeValue.Text, "options", OracleModule.DatabaseFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x06002C39 RID: 11321 RVA: 0x00086D42 File Offset: 0x00084F42
			public override TableValue TypedInvoke(TextValue server, Value options)
			{
				if (server.Length <= 0)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.ValueException_ServerCannotBeEmpty, server, null);
				}
				return OracleEnvironment.Create(this.host, server.String, options).CreateTable();
			}

			// Token: 0x17001060 RID: 4192
			// (get) Token: 0x06002C3A RID: 11322 RVA: 0x00086B9F File Offset: 0x00084D9F
			public override string PrimaryResourceKind
			{
				get
				{
					return "Oracle";
				}
			}

			// Token: 0x06002C3B RID: 11323 RVA: 0x00086D71 File Offset: 0x00084F71
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				return StaticAnalysisResolver.TryGetServerLocation<OracleDataSourceLocation>(expression, out location, out foundOptions, out unknownOptions);
			}

			// Token: 0x04001336 RID: 4918
			private static readonly TypeValue optionsType = OracleModule.OptionRecord.CreateRecordType().Nullable;

			// Token: 0x04001337 RID: 4919
			private readonly IEngineHost host;
		}
	}
}
