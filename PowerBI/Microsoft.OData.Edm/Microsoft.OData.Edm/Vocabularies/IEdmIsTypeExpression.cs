using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000103 RID: 259
	public interface IEdmIsTypeExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000766 RID: 1894
		IEdmExpression Operand { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000767 RID: 1895
		IEdmTypeReference Type { get; }
	}
}
