using System;

namespace Microsoft.Data.OData.Query.SyntacticAst
{
	// Token: 0x02000054 RID: 84
	internal abstract class PathSegmentTokenVisitor<T> : IPathSegmentTokenVisitor<T>
	{
		// Token: 0x0600022C RID: 556 RVA: 0x000084B2 File Offset: 0x000066B2
		public virtual T Visit(SystemToken tokenIn)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000084B9 File Offset: 0x000066B9
		public virtual T Visit(NonSystemToken tokenIn)
		{
			throw new NotImplementedException();
		}
	}
}
