using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000045 RID: 69
	public interface IEdmSchemaElement : IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000104 RID: 260
		EdmSchemaElementKind SchemaElementKind { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000105 RID: 261
		string Namespace { get; }
	}
}
