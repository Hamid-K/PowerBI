using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005B RID: 91
	internal class BadEntityType : BadNamedStructuredType, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001DD RID: 477 RVA: 0x00004E41 File Offset: 0x00003041
		public BadEntityType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000026B0 File Offset: 0x000008B0
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00002732 File Offset: 0x00000932
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool HasStream
		{
			get
			{
				return false;
			}
		}
	}
}
