using System;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200004A RID: 74
	internal class UnresolvedAction : UnresolvedOperation, IEdmAction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000114 RID: 276 RVA: 0x00003AB3 File Offset: 0x00001CB3
		public UnresolvedAction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00003ABE File Offset: 0x00001CBE
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Action;
			}
		}
	}
}
