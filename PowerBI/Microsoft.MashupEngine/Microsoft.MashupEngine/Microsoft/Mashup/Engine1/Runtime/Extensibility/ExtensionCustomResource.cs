using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x020016F6 RID: 5878
	internal class ExtensionCustomResource : ResourceKindInfo, IExtensionResourceKind
	{
		// Token: 0x06009553 RID: 38227 RVA: 0x001EDE5C File Offset: 0x001EC05C
		public ExtensionCustomResource(string moduleName, string kind, string label, bool supportsEncryptedConnection, bool? supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo, IEnumerable<CredentialProperty> applicationProperties, ExtensionResourceKindInfo extensionInfo)
			: base(kind, label, false, false, false, supportsEncryptedConnection, false, supportsNativeQuery, authenticationInfo, applicationProperties, null, null, extensionInfo.DsrHandlers.Cast<IDataSourceLocationFactory>())
		{
			this.moduleName = moduleName;
			this.extensionInfo = extensionInfo;
		}

		// Token: 0x17002710 RID: 10000
		// (get) Token: 0x06009554 RID: 38228 RVA: 0x001EDE9A File Offset: 0x001EC09A
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
		}

		// Token: 0x17002711 RID: 10001
		// (get) Token: 0x06009555 RID: 38229 RVA: 0x001EDEA2 File Offset: 0x001EC0A2
		public ExtensionResourceKindInfo Info
		{
			get
			{
				return this.extensionInfo;
			}
		}

		// Token: 0x06009556 RID: 38230 RVA: 0x001EDEAA File Offset: 0x001EC0AA
		public override string CreateTestFormula(string resourcePath)
		{
			return this.extensionInfo.CreateTestFormula(resourcePath);
		}

		// Token: 0x06009557 RID: 38231 RVA: 0x001EDEB8 File Offset: 0x001EC0B8
		public override IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
		{
			return this.extensionInfo.GetPartLabels(resourcePath);
		}

		// Token: 0x06009558 RID: 38232 RVA: 0x001EDEC6 File Offset: 0x001EC0C6
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			if (this.extensionInfo.TryGetHostName(resourcePath, out hostName))
			{
				return true;
			}
			hostName = null;
			return false;
		}

		// Token: 0x06009559 RID: 38233 RVA: 0x001EDEDD File Offset: 0x001EC0DD
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			return this.extensionInfo.TryParseResourcePath(resourcePath, out resource, out errorMessage);
		}

		// Token: 0x17002712 RID: 10002
		// (get) Token: 0x0600955A RID: 38234 RVA: 0x001EDEED File Offset: 0x001EC0ED
		public override IRecordValue ResourceRecord
		{
			get
			{
				return this.extensionInfo.ResourceRecord;
			}
		}

		// Token: 0x04004F63 RID: 20323
		private readonly string moduleName;

		// Token: 0x04004F64 RID: 20324
		private readonly ExtensionResourceKindInfo extensionInfo;
	}
}
