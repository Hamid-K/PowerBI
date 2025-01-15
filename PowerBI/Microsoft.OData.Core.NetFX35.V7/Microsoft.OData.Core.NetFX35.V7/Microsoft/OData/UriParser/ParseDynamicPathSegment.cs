using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001A0 RID: 416
	// (Invoke) Token: 0x060010E1 RID: 4321
	public delegate ICollection<ODataPathSegment> ParseDynamicPathSegment(ODataPathSegment previous, string identifier, string parenthesisExpression);
}
