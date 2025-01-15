using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000163 RID: 355
	internal class UnresolvedAction : UnresolvedOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060009A9 RID: 2473 RVA: 0x0001B4E1 File Offset: 0x000196E1
		public UnresolvedAction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0000268B File Offset: 0x0000088B
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
