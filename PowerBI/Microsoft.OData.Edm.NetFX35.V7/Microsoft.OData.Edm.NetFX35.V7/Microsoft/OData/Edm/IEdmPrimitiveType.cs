using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B1 RID: 177
	public interface IEdmPrimitiveType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004A5 RID: 1189
		EdmPrimitiveTypeKind PrimitiveKind { get; }
	}
}
