using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200009E RID: 158
	public interface IEdmFunction : IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000475 RID: 1141
		bool IsComposable { get; }
	}
}
