using System;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000052 RID: 82
	internal class UnresolvedFunction : UnresolvedOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public UnresolvedFunction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00003BAB File Offset: 0x00001DAB
		public bool IsComposable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00003BAE File Offset: 0x00001DAE
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}
	}
}
