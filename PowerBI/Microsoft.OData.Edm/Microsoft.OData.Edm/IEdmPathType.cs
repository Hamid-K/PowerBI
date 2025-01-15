using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000017 RID: 23
	public interface IEdmPathType : IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000098 RID: 152
		EdmPathTypeKind PathKind { get; }
	}
}
