using System;
using System.Collections.Generic;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x0200008D RID: 141
	public abstract class ODataPathSegmentTemplate
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x000102A1 File Offset: 0x0000E4A1
		public virtual bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			return false;
		}
	}
}
