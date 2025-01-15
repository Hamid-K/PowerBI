using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x0200010C RID: 268
	public interface IEdmLabeledExpressionReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600073C RID: 1852
		IEdmLabeledExpression ReferencedLabeledExpression { get; }
	}
}
