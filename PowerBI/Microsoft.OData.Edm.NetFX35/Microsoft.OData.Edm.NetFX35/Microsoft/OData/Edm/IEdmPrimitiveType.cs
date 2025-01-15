using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200013C RID: 316
	public interface IEdmPrimitiveType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000615 RID: 1557
		EdmPrimitiveTypeKind PrimitiveKind { get; }
	}
}
