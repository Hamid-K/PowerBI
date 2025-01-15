using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x0200006D RID: 109
	public interface IEdmCastExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001AA RID: 426
		IEdmExpression Operand { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001AB RID: 427
		IEdmTypeReference Type { get; }
	}
}
