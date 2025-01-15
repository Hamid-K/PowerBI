using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000053 RID: 83
	public interface IEdmTerm : IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600012A RID: 298
		EdmTermKind TermKind { get; }
	}
}
