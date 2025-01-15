using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000056 RID: 86
	public interface IEdmEntityType : IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000135 RID: 309
		IEnumerable<IEdmStructuralProperty> DeclaredKey { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000136 RID: 310
		bool HasStream { get; }
	}
}
