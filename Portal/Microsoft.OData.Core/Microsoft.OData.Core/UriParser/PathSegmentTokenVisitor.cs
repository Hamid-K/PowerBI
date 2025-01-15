using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E3 RID: 483
	internal abstract class PathSegmentTokenVisitor<T> : IPathSegmentTokenVisitor<T>
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual T Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
