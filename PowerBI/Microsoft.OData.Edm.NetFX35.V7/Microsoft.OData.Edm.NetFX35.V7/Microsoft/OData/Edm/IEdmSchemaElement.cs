using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B9 RID: 185
	public interface IEdmSchemaElement : IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060004B2 RID: 1202
		EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060004B3 RID: 1203
		string Namespace { get; }
	}
}
