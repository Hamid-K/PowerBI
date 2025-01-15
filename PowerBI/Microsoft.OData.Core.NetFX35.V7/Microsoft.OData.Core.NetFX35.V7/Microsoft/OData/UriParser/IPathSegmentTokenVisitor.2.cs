using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018C RID: 396
	public interface IPathSegmentTokenVisitor
	{
		// Token: 0x06000FFC RID: 4092
		void Visit(SystemToken tokenIn);

		// Token: 0x06000FFD RID: 4093
		void Visit(NonSystemToken tokenIn);
	}
}
