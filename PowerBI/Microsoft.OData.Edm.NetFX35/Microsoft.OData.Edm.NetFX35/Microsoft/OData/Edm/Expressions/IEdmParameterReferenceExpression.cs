using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x020000AF RID: 175
	public interface IEdmParameterReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060002F6 RID: 758
		IEdmOperationParameter ReferencedParameter { get; }
	}
}
