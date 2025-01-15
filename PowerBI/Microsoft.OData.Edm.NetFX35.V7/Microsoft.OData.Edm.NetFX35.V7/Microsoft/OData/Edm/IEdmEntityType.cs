using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000096 RID: 150
	public interface IEdmEntityType : IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600046C RID: 1132
		IEnumerable<IEdmStructuralProperty> DeclaredKey { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600046D RID: 1133
		bool HasStream { get; }
	}
}
