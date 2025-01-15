using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200051C RID: 1308
	internal class SingletonResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x06002A33 RID: 10803 RVA: 0x0007E614 File Offset: 0x0007C814
		public SingletonResourceKindInfo(string kind, string path, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, string hostName = null, bool supportsEncryptedConnection = false, bool supportsNativeQuery = false, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: this(kind, path, label, authenticationInfo, applicationProperties, hostName, supportsEncryptedConnection, new bool?(supportsNativeQuery), dslFactories)
		{
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0007E63C File Offset: 0x0007C83C
		protected SingletonResourceKindInfo(string kind, string path, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, string hostName = null, bool supportsEncryptedConnection = false, bool? supportsNativeQuery = false, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: base(kind, label, false, false, true, supportsEncryptedConnection, false, supportsNativeQuery, authenticationInfo, applicationProperties, null, null, dslFactories)
		{
			this.path = path ?? kind;
			this.hostName = hostName;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0007E675 File Offset: 0x0007C875
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			resource = new Resource(base.Kind, this.path, this.path);
			errorMessage = null;
			return true;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0007E694 File Offset: 0x0007C894
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			if (this.hostName != null)
			{
				hostName = this.hostName;
				return true;
			}
			hostName = null;
			return false;
		}

		// Token: 0x04001259 RID: 4697
		private readonly string path;

		// Token: 0x0400125A RID: 4698
		private readonly string hostName;
	}
}
