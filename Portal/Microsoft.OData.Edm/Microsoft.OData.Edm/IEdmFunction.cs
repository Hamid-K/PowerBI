using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000021 RID: 33
	public interface IEdmFunction : IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A5 RID: 165
		bool IsComposable { get; }
	}
}
