using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200018B RID: 395
	public interface IPathSegmentTokenVisitor<T>
	{
		// Token: 0x06000FFA RID: 4090
		T Visit(SystemToken tokenIn);

		// Token: 0x06000FFB RID: 4091
		T Visit(NonSystemToken tokenIn);
	}
}
