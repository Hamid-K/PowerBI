using System;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x02000105 RID: 261
	public interface IEdmLabeledExpressionReferenceExpression : IEdmExpression, IEdmElement
	{
		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000769 RID: 1897
		IEdmLabeledExpression ReferencedLabeledExpression { get; }
	}
}
