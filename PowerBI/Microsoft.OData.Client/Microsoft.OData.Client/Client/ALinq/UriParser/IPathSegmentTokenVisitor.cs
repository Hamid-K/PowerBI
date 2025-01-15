using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000126 RID: 294
	public interface IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06000C26 RID: 3110
		T Visit(SystemToken tokenIn);

		// Token: 0x06000C27 RID: 3111
		T Visit(NonSystemToken tokenIn);
	}
}
