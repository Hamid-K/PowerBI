using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000086 RID: 134
	public interface IEdmEntityType : IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000395 RID: 917
		IEnumerable<IEdmStructuralProperty> DeclaredKey { get; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000396 RID: 918
		bool HasStream { get; }
	}
}
