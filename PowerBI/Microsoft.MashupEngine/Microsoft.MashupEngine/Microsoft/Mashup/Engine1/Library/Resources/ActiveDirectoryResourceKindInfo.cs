using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Library.ActiveDirectory;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x0200051D RID: 1309
	internal class ActiveDirectoryResourceKindInfo : ResourceKindInfo
	{
		// Token: 0x06002A37 RID: 10807 RVA: 0x0007E6AC File Offset: 0x0007C8AC
		public ActiveDirectoryResourceKindInfo()
			: base("ActiveDirectory", null, false, false, false, false, false, false, new AuthenticationInfo[]
			{
				new WindowsAuthenticationInfo
				{
					SupportsAlternateCredentials = true
				},
				new UsernamePasswordAuthenticationInfo()
			}, null, null, null, new DataSourceLocationFactory[] { ActiveDirectoryDataSourceLocation.Factory })
		{
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x0007E6F9 File Offset: 0x0007C8F9
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			if (!ActiveDirectoryModule.IsValidDomainName(resourcePath))
			{
				errorMessage = Strings.Resource_DomainName_Invalid;
				resource = null;
				return false;
			}
			errorMessage = null;
			resource = new Resource(base.Kind, resourcePath.ToLowerInvariant(), resourcePath);
			return true;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x0007D355 File Offset: 0x0007B555
		public override bool TryGetHostName(string resourcePath, out string hostName)
		{
			hostName = null;
			return false;
		}
	}
}
