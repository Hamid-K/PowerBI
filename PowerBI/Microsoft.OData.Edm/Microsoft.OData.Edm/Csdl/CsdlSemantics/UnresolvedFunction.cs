using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000166 RID: 358
	internal class UnresolvedFunction : UnresolvedOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x0001B4E1 File Offset: 0x000196E1
		public UnresolvedFunction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsComposable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0000480B File Offset: 0x00002A0B
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}
	}
}
