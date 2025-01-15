using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000193 RID: 403
	internal abstract class PathSegmentTokenVisitor<T> : IPathSegmentTokenVisitor<T>
	{
		// Token: 0x0600104E RID: 4174 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public virtual T Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
