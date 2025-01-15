using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000051 RID: 81
	public interface IEdmFunction : IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000126 RID: 294
		bool IsComposable { get; }
	}
}
