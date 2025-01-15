using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010A RID: 266
	public interface IEdmIsTypeExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000739 RID: 1849
		IEdmExpression Operand { get; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600073A RID: 1850
		IEdmTypeReference Type { get; }
	}
}
