using System;
using System.Collections.Generic;
using System.Web.Http.Routing;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Interfaces;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Adapters
{
	// Token: 0x020001E0 RID: 480
	internal class WebApiUrlHelper : IWebApiUrlHelper
	{
		// Token: 0x06000FBD RID: 4029 RVA: 0x0003FAF6 File Offset: 0x0003DCF6
		public WebApiUrlHelper(UrlHelper helper)
		{
			if (helper == null)
			{
				throw Error.ArgumentNull("helper");
			}
			this.innerHelper = helper;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003FB13 File Offset: 0x0003DD13
		public string CreateODataLink(params ODataPathSegment[] segments)
		{
			return this.innerHelper.CreateODataLink(segments);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003FB21 File Offset: 0x0003DD21
		public string CreateODataLink(IList<ODataPathSegment> segments)
		{
			return this.innerHelper.CreateODataLink(segments);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003FB2F File Offset: 0x0003DD2F
		public string CreateODataLink(string routeName, IODataPathHandler pathHandler, IList<ODataPathSegment> segments)
		{
			return this.innerHelper.CreateODataLink(routeName, pathHandler, segments);
		}

		// Token: 0x04000457 RID: 1111
		private UrlHelper innerHelper;
	}
}
