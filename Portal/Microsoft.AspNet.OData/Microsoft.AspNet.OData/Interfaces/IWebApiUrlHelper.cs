using System;
using System.Collections.Generic;
using Microsoft.AspNet.OData.Routing;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Interfaces
{
	// Token: 0x02000061 RID: 97
	internal interface IWebApiUrlHelper
	{
		// Token: 0x060002A9 RID: 681
		string CreateODataLink(IList<ODataPathSegment> segments);

		// Token: 0x060002AA RID: 682
		string CreateODataLink(params ODataPathSegment[] segments);

		// Token: 0x060002AB RID: 683
		string CreateODataLink(string routeName, IODataPathHandler pathHandler, IList<ODataPathSegment> segments);
	}
}
