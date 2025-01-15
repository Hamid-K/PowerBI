using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200015B RID: 347
	internal class CsdlSemanticsAction : CsdlSemanticsOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600090A RID: 2314 RVA: 0x000195AF File Offset: 0x000177AF
		public CsdlSemanticsAction(CsdlSemanticsSchema context, CsdlAction action)
			: base(context, action)
		{
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
