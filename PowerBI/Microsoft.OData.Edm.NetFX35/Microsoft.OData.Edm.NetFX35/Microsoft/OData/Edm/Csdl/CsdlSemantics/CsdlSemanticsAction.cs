using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000061 RID: 97
	internal class CsdlSemanticsAction : CsdlSemanticsOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00004126 File Offset: 0x00002326
		public CsdlSemanticsAction(CsdlSemanticsSchema context, CsdlAction action)
			: base(context, action)
		{
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00004130 File Offset: 0x00002330
		public override EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
