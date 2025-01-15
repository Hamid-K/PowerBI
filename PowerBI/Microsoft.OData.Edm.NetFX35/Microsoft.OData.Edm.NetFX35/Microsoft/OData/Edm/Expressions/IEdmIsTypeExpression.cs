using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200008C RID: 140
	public interface IEdmIsTypeExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000252 RID: 594
		IEdmExpression Operand { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000253 RID: 595
		IEdmTypeReference Type { get; }
	}
}
