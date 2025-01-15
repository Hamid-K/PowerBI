using System;
using System.Collections.Generic;
using Microsoft.OData.UriParser;

namespace Microsoft.AspNet.OData.Routing.Template
{
	// Token: 0x0200008A RID: 138
	public class ODataPathSegmentTemplate<T> : ODataPathSegmentTemplate where T : ODataPathSegment
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x000101C8 File Offset: 0x0000E3C8
		public override bool TryMatch(ODataPathSegment pathSegment, IDictionary<string, object> values)
		{
			return pathSegment is T;
		}
	}
}
