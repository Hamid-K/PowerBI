using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000101 RID: 257
	public interface IEdmIfExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000763 RID: 1891
		IEdmExpression TestExpression { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000764 RID: 1892
		IEdmExpression TrueExpression { get; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000765 RID: 1893
		IEdmExpression FalseExpression { get; }
	}
}
