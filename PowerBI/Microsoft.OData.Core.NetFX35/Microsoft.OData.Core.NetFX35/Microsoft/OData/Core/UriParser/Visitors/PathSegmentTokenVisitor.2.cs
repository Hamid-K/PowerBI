using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x02000297 RID: 663
	internal abstract class PathSegmentTokenVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x060016C3 RID: 5827 RVA: 0x0004E850 File Offset: 0x0004CA50
		public virtual void Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x0004E857 File Offset: 0x0004CA57
		public virtual void Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
