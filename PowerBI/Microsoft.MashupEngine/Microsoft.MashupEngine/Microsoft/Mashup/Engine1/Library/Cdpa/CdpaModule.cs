using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E10 RID: 3600
	public sealed class CdpaModule : Module
	{
		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x060060F1 RID: 24817 RVA: 0x0014A6BC File Offset: 0x001488BC
		public override string Name
		{
			get
			{
				return "Cdpa";
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x060060F2 RID: 24818 RVA: 0x0014A6C3 File Offset: 0x001488C3
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
							return "Cdpa.Database";
						}
						throw new InvalidOperationException();
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x060060F3 RID: 24819 RVA: 0x0014A6FE File Offset: 0x001488FE
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { CdpaModule.resourceKindInfo };
			}
		}

		// Token: 0x060060F4 RID: 24820 RVA: 0x0014A710 File Offset: 0x00148910
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost engineHost)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new CdpaModule.DatabaseFunctionValue(engineHost);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x040034DF RID: 13535
		public const string DatabaseFunctionName = "Cdpa.Database";

		// Token: 0x040034E0 RID: 13536
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("CDPA", Strings.Cdpa_ChallengeTitle, new AuthenticationInfo[]
		{
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Optional,
				ProviderFactory = new OAuthFactory(new Func<OAuthServices, string, OAuthResource>(CdpaOAuthHelper.GetOAuthResource), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, CdpaOAuthHelper.GetOAuthResource(services, url)))
			}
		}, null, false, false, false, null, new IDataSourceLocationFactory[] { CdpaDataSourceLocation.Factory });

		// Token: 0x040034E1 RID: 13537
		private Keys exportKeys;

		// Token: 0x02000E11 RID: 3601
		private enum Exports
		{
			// Token: 0x040034E3 RID: 13539
			Cdpa_Database,
			// Token: 0x040034E4 RID: 13540
			Count
		}

		// Token: 0x02000E12 RID: 3602
		private class DatabaseFunctionValue : NativeFunctionValue1<Value, TextValue>
		{
			// Token: 0x060060F7 RID: 24823 RVA: 0x0014A7B9 File Offset: 0x001489B9
			public DatabaseFunctionValue(IEngineHost engineHost)
				: base(TypeValue.Any, 1, "tenantUri", TypeValue.Text)
			{
				this.engineHost = engineHost;
			}

			// Token: 0x060060F8 RID: 24824 RVA: 0x0014A7D8 File Offset: 0x001489D8
			public override Value TypedInvoke(TextValue tenantUri)
			{
				CdpaService cdpaService = new CdpaService(this.engineHost, tenantUri.AsString);
				CdpaCube cdpaCube = new CdpaCube(cdpaService);
				SetContextProvider setContextProvider = new CdpaSetContextProvider(cdpaService);
				Projection projection = new Projection(Keys.Empty, EmptyArray<IdentifierCubeExpression>.Instance);
				return SetContextTableValue.New(new SetContextQuery(setContextProvider, EmptyArray<ParameterArguments>.Instance, cdpaCube, EverythingSet.Instance, projection, EmptyArray<TableKey>.Instance));
			}

			// Token: 0x17001CA1 RID: 7329
			// (get) Token: 0x060060F9 RID: 24825 RVA: 0x0014A82D File Offset: 0x00148A2D
			public override string PrimaryResourceKind
			{
				get
				{
					return "CDPA";
				}
			}

			// Token: 0x060060FA RID: 24826 RVA: 0x0014A834 File Offset: 0x00148A34
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				location = null;
				foundOptions = null;
				unknownOptions = null;
				Dictionary<string, IExpression> dictionary;
				Value value;
				if (CdpaModule.DatabaseFunctionValue.pattern.TryMatch(expression, out dictionary) && dictionary.TryGetConstant("tenantUri", out value) && value.IsText)
				{
					location = new CdpaDataSourceLocation
					{
						TenantUri = value.AsString
					};
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
				}
				return location != null;
			}

			// Token: 0x040034E5 RID: 13541
			private readonly IEngineHost engineHost;

			// Token: 0x040034E6 RID: 13542
			private static readonly ExpressionPattern pattern = new ExpressionPattern(new string[] { "__func(__tenantUri)" });
		}
	}
}
