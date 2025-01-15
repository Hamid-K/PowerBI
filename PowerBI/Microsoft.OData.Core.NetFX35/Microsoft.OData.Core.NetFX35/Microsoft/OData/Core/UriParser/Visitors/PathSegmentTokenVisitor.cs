using System;
using Microsoft.OData.Core.UriParser.Syntactic;

namespace Microsoft.OData.Core.UriParser.Visitors
{
	// Token: 0x0200020D RID: 525
	internal abstract class PathSegmentTokenVisitor<T> : IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x00045D04 File Offset: 0x00043F04
		public virtual T Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00045D0B File Offset: 0x00043F0B
		public virtual T Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
