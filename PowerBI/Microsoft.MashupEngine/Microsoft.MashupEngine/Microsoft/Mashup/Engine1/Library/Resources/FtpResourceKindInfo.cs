using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.DataSourceReference;

namespace Microsoft.Mashup.Engine1.Library.Resources
{
	// Token: 0x02000523 RID: 1315
	internal class FtpResourceKindInfo : UriResourceKindInfo
	{
		// Token: 0x06002A4E RID: 10830 RVA: 0x0007EB28 File Offset: 0x0007CD28
		public FtpResourceKindInfo()
			: base("Ftp", null, new AuthenticationInfo[]
			{
				new ImplicitAuthenticationInfo(),
				new UsernamePasswordAuthenticationInfo()
			}, null, false, false, false, null, new DataSourceLocationFactory[] { FtpDataSourceLocation.Factory })
		{
		}

		// Token: 0x06002A4F RID: 10831 RVA: 0x0007EB6C File Offset: 0x0007CD6C
		public override bool Validate(string resourcePath, out IResource resource, out string errorMessage)
		{
			string text;
			if (!Resource.TryNormalizeFtpUri(resourcePath, out text))
			{
				errorMessage = Strings.Resource_FtpUrl_Invalid;
				resource = null;
				return false;
			}
			errorMessage = null;
			resource = new Resource(base.Kind, text, resourcePath);
			return true;
		}
	}
}
