using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000157 RID: 343
	internal class UnresolvedFunction : UnresolvedOperation, IEdmFunction, IEdmOperation, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060008F3 RID: 2291 RVA: 0x00019425 File Offset: 0x00017625
		public UnresolvedFunction(string qualifiedName, string errorMessage, EdmLocation location)
			: base(qualifiedName, errorMessage, location)
		{
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool IsComposable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00009215 File Offset: 0x00007415
		public new EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.Function;
			}
		}
	}
}
