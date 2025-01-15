using System;

namespace Microsoft.OData.Edm.Expressions
{
	// Token: 0x02000090 RID: 144
	public interface IEdmLabeledExpressionReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000269 RID: 617
		IEdmLabeledExpression ReferencedLabeledExpression { get; }
	}
}
