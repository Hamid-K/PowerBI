using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000524 RID: 1316
	internal class SasResourceKindInfo : UriResourceKindInfo
	{
		// Token: 0x06002A50 RID: 10832 RVA: 0x0007EBA8 File Offset: 0x0007CDA8
		public SasResourceKindInfo(string kind, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, bool supportsEncryptedConnection = false, bool supportsNativeQuery = false, bool isSingleton = false, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: base(kind, label, authenticationInfo, applicationProperties, supportsEncryptedConnection, supportsNativeQuery, isSingleton, connectionStringProperties, dslFactories)
		{
		}

		// Token: 0x06002A51 RID: 10833 RVA: 0x0007EBCC File Offset: 0x0007CDCC
		public override bool CanRefresh(IResourceCredential credential)
		{
			ParameterizedCredential parameterizedCredential = credential as ParameterizedCredential;
			return (parameterizedCredential != null && parameterizedCredential.Name == "SAS") || base.CanRefresh(credential);
		}
	}
}
