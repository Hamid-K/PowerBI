using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000085 RID: 133
	public interface IEdmPrimitiveType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000394 RID: 916
		EdmPrimitiveTypeKind PrimitiveKind { get; }
	}
}
