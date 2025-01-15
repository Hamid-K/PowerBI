using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000154 RID: 340
	internal class UnresolvedAction : UnresolvedOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x00019425 File Offset: 0x00017625
		public UnresolvedAction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00009097 File Offset: 0x00007297
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
