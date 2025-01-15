using System;
using Microsoft.AspNet.OData.Routing;

namespace Microsoft.ReportingServices.Portal.ODataWebApi.WebApi
{
	// Token: 0x0200000C RID: 12
	internal sealed class PathAndSlashEscapeODataPathHandler : DefaultODataPathHandler
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00003721 File Offset: 0x00001921
		public override ODataPath Parse(string serviceRoot, string odataPath, IServiceProvider requestContainer)
		{
			odataPath = ODataPathRewriteHelper.Rewrite(odataPath);
			return base.Parse(serviceRoot, odataPath, requestContainer);
		}
	}
}
