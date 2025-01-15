using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000522 RID: 1314
	internal class UriRootResourceKindInfo : UriResourceKindInfo
	{
		// Token: 0x06002A4C RID: 10828 RVA: 0x0007EAB8 File Offset: 0x0007CCB8
		public UriRootResourceKindInfo(string kind, string label, IEnumerable<AuthenticationInfo> authenticationInfo = null, IEnumerable<IDataSourceLocationFactory> dslFactories = null, bool supportsNativeQuery = false)
			: base(kind, label, authenticationInfo, null, false, supportsNativeQuery, false, null, dslFactories)
		{
		}

		// Token: 0x06002A4D RID: 10829 RVA: 0x0007EAD8 File Offset: 0x0007CCD8
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			if (!base.Validate(resourcePath, out resource, out errorMessage))
			{
				return false;
			}
			string absoluteUri = new Uri(new Uri(resource.Path).GetLeftPart(UriPartial.Authority)).AbsoluteUri;
			resource = new Resource(base.Kind, absoluteUri, resource.NonNormalizedPath);
			return true;
		}
	}
}
