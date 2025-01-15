using System;
using System.Collections.Generic;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000116 RID: 278
	// (Invoke) Token: 0x06000F80 RID: 3968
	public delegate ICollection<ODataPathSegment> ParseDynamicPathSegment(ODataPathSegment previous, string identifier, string parenthesisExpression);
}
