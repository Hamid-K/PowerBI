using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000082 RID: 130
	public interface IEdmOperationReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600021E RID: 542
		IEdmOperation ReferencedOperation { get; }
	}
}
