using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Resources;

namespace Microsoft.Mashup.Engine1.Runtime.Extensibility
{
	// Token: 0x0200170C RID: 5900
	internal class ExtensionUriResource : UriResourceKindInfo, IExtensionResourceKind
	{
		// Token: 0x060095FB RID: 38395 RVA: 0x001F12DC File Offset: 0x001EF4DC
		public ExtensionUriResource(string moduleName, string kind, string label, bool supportsEncryptedConnection, bool? supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo, IEnumerable<CredentialProperty> applicationProperties, ExtensionResourceKindInfo extensionInfo)
			: base(kind, label, authenticationInfo, applicationProperties, supportsEncryptedConnection, supportsNativeQuery, false, null, extensionInfo.DsrHandlers.Cast<IDataSourceLocationFactory>())
		{
			this.moduleName = moduleName;
			this.extensionInfo = extensionInfo;
		}

		// Token: 0x17002737 RID: 10039
		// (get) Token: 0x060095FC RID: 38396 RVA: 0x001F131A File Offset: 0x001EF51A
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
		}

		// Token: 0x17002738 RID: 10040
		// (get) Token: 0x060095FD RID: 38397 RVA: 0x001F1322 File Offset: 0x001EF522
		public ExtensionResourceKindInfo Info
		{
			get
			{
				return this.extensionInfo;
			}
		}

		// Token: 0x060095FE RID: 38398 RVA: 0x001F132A File Offset: 0x001EF52A
		public override string CreateTestFormula(string resourcePath)
		{
			return this.extensionInfo.CreateTestFormula(resourcePath);
		}

		// Token: 0x060095FF RID: 38399 RVA: 0x001F1338 File Offset: 0x001EF538
		public override IEnumerable<KeyValuePair<string, string>> GetPartLabels(string resourcePath)
		{
			return this.extensionInfo.GetPartLabels(resourcePath);
		}

		// Token: 0x06009600 RID: 38400 RVA: 0x001F1346 File Offset: 0x001EF546
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			return this.extensionInfo.TryGetHostName(resourcePath, out hostName) || base.TryGetHostName(resourcePath, out hostName);
		}

		// Token: 0x17002739 RID: 10041
		// (get) Token: 0x06009601 RID: 38401 RVA: 0x001F1361 File Offset: 0x001EF561
		public override IRecordValue ResourceRecord
		{
			get
			{
				return this.extensionInfo.ResourceRecord;
			}
		}

		// Token: 0x04004FC3 RID: 20419
		private readonly string moduleName;

		// Token: 0x04004FC4 RID: 20420
		private readonly ExtensionResourceKindInfo extensionInfo;
	}
}
