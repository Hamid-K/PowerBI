using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x0200020C RID: 524
	internal interface IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06001323 RID: 4899
		T Visit(SystemToken tokenIn);

		// Token: 0x06001324 RID: 4900
		T Visit(NonSystemToken tokenIn);
	}
}
