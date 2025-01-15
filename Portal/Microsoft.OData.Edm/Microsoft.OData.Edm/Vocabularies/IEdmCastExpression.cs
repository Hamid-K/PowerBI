using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000F8 RID: 248
	public interface IEdmCastExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600075E RID: 1886
		IEdmExpression Operand { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600075F RID: 1887
		IEdmTypeReference Type { get; }
	}
}
