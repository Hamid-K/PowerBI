using System;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000127 RID: 295
	public interface IPathSegmentTokenVisitor
	{
		// Token: 0x06000C28 RID: 3112
		void Visit(SystemToken tokenIn);

		// Token: 0x06000C29 RID: 3113
		void Visit(NonSystemToken tokenIn);
	}
}
