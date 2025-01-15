using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DA RID: 474
	public interface IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06001547 RID: 5447
		T Visit(SystemToken tokenIn);

		// Token: 0x06001548 RID: 5448
		T Visit(NonSystemToken tokenIn);
	}
}
