using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000521 RID: 1313
	internal class UriResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x0007E9EC File Offset: 0x0007CBEC
		public UriResourceKindInfo(string kind, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, bool supportsEncryptedConnection = false, bool supportsNativeQuery = false, bool isSingleton = false, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: this(kind, label, authenticationInfo, applicationProperties, supportsEncryptedConnection, new bool?(supportsNativeQuery), isSingleton, connectionStringProperties, dslFactories)
		{
		}

		// Token: 0x06002A47 RID: 10823 RVA: 0x0007EA14 File Offset: 0x0007CC14
		protected UriResourceKindInfo(string kind, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, bool supportsEncryptedConnection = false, bool? supportsNativeQuery = false, bool isSingleton = false, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: base(kind, label, true, false, isSingleton, supportsEncryptedConnection, false, supportsNativeQuery, authenticationInfo, applicationProperties, null, connectionStringProperties, dslFactories)
		{
		}

		// Token: 0x06002A48 RID: 10824 RVA: 0x0007EA3A File Offset: 0x0007CC3A
		public override IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
		{
			return Resource.UriSubPaths(this, resourcePath, '/');
		}

		// Token: 0x06002A49 RID: 10825 RVA: 0x0007EA45 File Offset: 0x0007CC45
		public override bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
		{
			return Resource.IsSubPath(permittedResourcePath, attemptedResourcePath, '/');
		}

		// Token: 0x06002A4A RID: 10826 RVA: 0x0007EA50 File Offset: 0x0007CC50
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			string text2;
			if (!Resource.TryNormalizeWebUri(resourcePath, out text, out text2))
			{
				resource = null;
				errorMessage = Strings.Resource_WebUrl_Invalid;
				return false;
			}
			resource = new Resource(base.Kind, text, text2);
			errorMessage = null;
			return true;
		}

		// Token: 0x06002A4B RID: 10827 RVA: 0x0007EA90 File Offset: 0x0007CC90
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			Uri uri;
			if (Uri.TryCreate(resourcePath, UriKind.Absolute, out uri))
			{
				hostName = uri.Host;
				return true;
			}
			hostName = null;
			return false;
		}

		// Token: 0x0400125E RID: 4702
		private const char UriSeparatorChar = '/';
	}
}
