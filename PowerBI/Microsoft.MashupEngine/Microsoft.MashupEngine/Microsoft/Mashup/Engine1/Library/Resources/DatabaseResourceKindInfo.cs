using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000506 RID: 1286
	internal class DatabaseResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x060029DA RID: 10714 RVA: 0x0007D35C File Offset: 0x0007B55C
		public DatabaseResourceKindInfo(string kind, string label, bool supportsEncryptedConnection, bool supportsConnectionString, bool supportsNativeQuery, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<CredentialProperty> applicationProperties = null, IEnumerable<string> connectionStringProperties = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null)
			: base(kind, label, false, true, false, supportsEncryptedConnection, supportsConnectionString, supportsNativeQuery, authenticationInfo, applicationProperties, null, connectionStringProperties, dslFactories)
		{
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0007D384 File Offset: 0x0007B584
		public override IEnumerable<string> EnumerateKnownSupersets(string resourcePath)
		{
			string text;
			string text2;
			if (!DatabaseResource.TryParsePath(resourcePath, out text, out text2))
			{
				return new string[0];
			}
			if (text2 == null)
			{
				return new string[] { resourcePath };
			}
			return new string[] { resourcePath, text };
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0007D3C0 File Offset: 0x0007B5C0
		public override bool IsSubset(string permittedResourcePath, string attemptedResourcePath)
		{
			return DatabaseResource.IsSubPath(permittedResourcePath, attemptedResourcePath);
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0007D3CC File Offset: 0x0007B5CC
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			string text2;
			if (!DatabaseResource.TryParsePath(resourcePath, out text, out text2))
			{
				errorMessage = Strings.Resource_DbPath_Invalid;
				resource = null;
				return false;
			}
			resource = new Resource(base.Kind, DatabaseResource.ToPath(text.ToLowerInvariant(), text2), resourcePath);
			errorMessage = null;
			return true;
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x0007D414 File Offset: 0x0007B614
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			string text;
			return DatabaseResource.TryParsePath(resourcePath, out hostName, out text);
		}
	}
}
