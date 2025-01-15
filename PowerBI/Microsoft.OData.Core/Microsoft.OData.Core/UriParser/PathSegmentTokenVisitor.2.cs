using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001E4 RID: 484
	internal abstract class PathSegmentTokenVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000032BD File Offset: 0x000014BD
		public virtual void Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
