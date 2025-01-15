using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000170 RID: 368
	public interface IEdmValueTermReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000703 RID: 1795
		IEdmExpression Base { get; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000704 RID: 1796
		IEdmValueTerm Term { get; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000705 RID: 1797
		string Qualifier { get; }
	}
}
