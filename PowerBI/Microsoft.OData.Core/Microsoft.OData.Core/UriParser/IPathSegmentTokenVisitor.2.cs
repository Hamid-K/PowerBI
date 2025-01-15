using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001DB RID: 475
	public interface IPathSegmentTokenVisitor
	{
		// Token: 0x06001549 RID: 5449
		void Visit(SystemToken tokenIn);

		// Token: 0x0600154A RID: 5450
		void Visit(NonSystemToken tokenIn);
	}
}
