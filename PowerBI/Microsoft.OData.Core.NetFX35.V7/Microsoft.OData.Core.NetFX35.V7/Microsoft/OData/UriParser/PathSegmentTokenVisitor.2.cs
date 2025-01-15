using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000194 RID: 404
	internal abstract class PathSegmentTokenVisitor : IPathSegmentTokenVisitor
	{
		// Token: 0x06001051 RID: 4177 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual void Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
