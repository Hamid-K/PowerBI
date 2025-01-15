using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000290 RID: 656
	internal interface IPathSegmentTokenVisitor
	{
		// Token: 0x06001670 RID: 5744
		void Visit(SystemToken tokenIn);

		// Token: 0x06001671 RID: 5745
		void Visit(NonSystemToken tokenIn);
	}
}
