using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200016A RID: 362
	internal class CsdlSemanticsAction : CsdlSemanticsOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060009C5 RID: 2501 RVA: 0x0001B6AF File Offset: 0x000198AF
		public CsdlSemanticsAction(CsdlSemanticsSchema context, CsdlAction action)
			: base(context, action)
		{
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
