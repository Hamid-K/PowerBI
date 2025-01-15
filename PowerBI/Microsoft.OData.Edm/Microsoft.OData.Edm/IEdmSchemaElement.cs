using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A5 RID: 165
	public interface IEdmSchemaElement : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003DE RID: 990
		EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003DF RID: 991
		string Namespace { get; }
	}
}
