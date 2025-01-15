using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FCD RID: 4045
	internal class ActiveDirectoryModule : Module
	{
		// Token: 0x17001E7C RID: 7804
		// (get) Token: 0x06006A2D RID: 27181 RVA: 0x0016D970 File Offset: 0x0016BB70
		public override string Name
		{
			get
			{
				return "ActiveDirectory";
			}
		}

		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06006A2E RID: 27182 RVA: 0x0016D977 File Offset: 0x0016BB77
		public override Keys ExportKeys
		{
			get
			{
				if (ActiveDirectoryModule.exportKeys == null)
				{
					ActiveDirectoryModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "ActiveDirectory.Domains";
						}
						throw new InvalidOperationException();
					});
				}
				return ActiveDirectoryModule.exportKeys;
			}
		}

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06006A2F RID: 27183 RVA: 0x0016D9AF File Offset: 0x0016BBAF
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { ActiveDirectoryModule.resourceKindInfo };
			}
		}

		// Token: 0x06006A30 RID: 27184 RVA: 0x0016D9C0 File Offset: 0x0016BBC0
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ActiveDirectoryModule.DomainsFunctionValue(host);
				}
				throw new InvalidOperationException();
			});
		}

		// Token: 0x06006A31 RID: 27185 RVA: 0x0016D9F1 File Offset: 0x0016BBF1
		public static bool IsValidDomainName(string domainName)
		{
			return Uri.CheckHostName(domainName) == UriHostNameType.Dns;
		}

		// Token: 0x04003AEB RID: 15083
		public const string DataSourceNameString = "Active Directory";

		// Token: 0x04003AEC RID: 15084
		private static readonly ResourceKindInfo resourceKindInfo = new ActiveDirectoryResourceKindInfo();

		// Token: 0x04003AED RID: 15085
		private static Keys exportKeys;

		// Token: 0x02000FCE RID: 4046
		private enum Exports
		{
			// Token: 0x04003AEF RID: 15087
			Domains,
			// Token: 0x04003AF0 RID: 15088
			Count
		}

		// Token: 0x02000FCF RID: 4047
		private sealed class DomainsFunctionValue : NativeFunctionValue1<TableValue, Value>
		{
			// Token: 0x06006A34 RID: 27188 RVA: 0x0016DA08 File Offset: 0x0016BC08
			public DomainsFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 0, "forestRootDomainName", NullableTypeValue.Text)
			{
				this.host = host;
				this.activeDirectoryService = host.Hook(() => new ActiveDirectoryService());
			}

			// Token: 0x06006A35 RID: 27189 RVA: 0x0016DA60 File Offset: 0x0016BC60
			public override TableValue TypedInvoke(Value forestName)
			{
				ActiveDirectoryServiceAccessor activeDirectoryServiceAccessor = ActiveDirectoryServiceAccessor.New(this.host, this.activeDirectoryService, forestName.IsNull ? null : forestName.AsText.String);
				return new ActiveDirectoryModuleCore(this.host, activeDirectoryServiceAccessor).GetDomainsTableValue();
			}

			// Token: 0x17001E7F RID: 7807
			// (get) Token: 0x06006A36 RID: 27190 RVA: 0x0016D970 File Offset: 0x0016BB70
			public override string PrimaryResourceKind
			{
				get
				{
					return "ActiveDirectory";
				}
			}

			// Token: 0x06006A37 RID: 27191 RVA: 0x0016DAA8 File Offset: 0x0016BCA8
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
				if (argumentValues != null && argumentValues.Length == 1 && argumentValues[0].IsText)
				{
					location = new ActiveDirectoryDataSourceLocation();
					location.Address["domain"] = argumentValues[0].AsString;
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
					return true;
				}
				location = null;
				foundOptions = null;
				unknownOptions = null;
				return false;
			}

			// Token: 0x04003AF1 RID: 15089
			private readonly IEngineHost host;

			// Token: 0x04003AF2 RID: 15090
			private readonly IActiveDirectoryService activeDirectoryService;
		}
	}
}
