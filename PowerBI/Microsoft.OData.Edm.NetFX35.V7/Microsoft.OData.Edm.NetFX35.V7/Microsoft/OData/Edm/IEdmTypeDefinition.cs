using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C4 RID: 196
	public interface IEdmTypeDefinition : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004C0 RID: 1216
		IEdmPrimitiveType UnderlyingType { get; }
	}
}
