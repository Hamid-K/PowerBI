using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001B RID: 27
	public interface IEdmTypeDefinition : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600009F RID: 159
		IEdmPrimitiveType UnderlyingType { get; }
	}
}
