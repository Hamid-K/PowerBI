using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005C RID: 92
	public interface IEdmTypeDefinition : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000150 RID: 336
		IEdmPrimitiveType UnderlyingType { get; }
	}
}
