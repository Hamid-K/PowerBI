using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200170B RID: 5899
	internal class ExtensionSingletonResource : SingletonResourceKindInfo, IExtensionResourceKind
	{
		// Token: 0x060095F4 RID: 38388 RVA: 0x001F1250 File Offset: 0x001EF450
		public ExtensionSingletonResource(string moduleName, string kind, string label, bool supportsEncryptedConnection, bool? supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo, IEnumerable<CredentialProperty> applicationProperties, ExtensionResourceKindInfo extensionInfo)
			: base(kind, kind, label, authenticationInfo, applicationProperties, null, supportsEncryptedConnection, supportsNativeQuery, extensionInfo.DsrHandlers.Cast<IDataSourceLocationFactory>())
		{
			this.moduleName = moduleName;
			this.extensionInfo = extensionInfo;
		}

		// Token: 0x17002734 RID: 10036
		// (get) Token: 0x060095F5 RID: 38389 RVA: 0x001F128E File Offset: 0x001EF48E
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
		}

		// Token: 0x17002735 RID: 10037
		// (get) Token: 0x060095F6 RID: 38390 RVA: 0x001F1296 File Offset: 0x001EF496
		public ExtensionResourceKindInfo Info
		{
			get
			{
				return this.extensionInfo;
			}
		}

		// Token: 0x060095F7 RID: 38391 RVA: 0x001F129E File Offset: 0x001EF49E
		public override string CreateTestFormula(string resourcePath)
		{
			return this.extensionInfo.CreateTestFormula(resourcePath);
		}

		// Token: 0x060095F8 RID: 38392 RVA: 0x001F12AC File Offset: 0x001EF4AC
		public override IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
		{
			return new KeyValuePair<string, string>[0];
		}

		// Token: 0x060095F9 RID: 38393 RVA: 0x001F12B4 File Offset: 0x001EF4B4
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			return this.extensionInfo.TryGetHostName(resourcePath, out hostName) || base.TryGetHostName(resourcePath, out hostName);
		}

		// Token: 0x17002736 RID: 10038
		// (get) Token: 0x060095FA RID: 38394 RVA: 0x001F12CF File Offset: 0x001EF4CF
		public override IRecordValue ResourceRecord
		{
			get
			{
				return this.extensionInfo.ResourceRecord;
			}
		}

		// Token: 0x04004FC1 RID: 20417
		private readonly string moduleName;

		// Token: 0x04004FC2 RID: 20418
		private readonly ExtensionResourceKindInfo extensionInfo;
	}
}
