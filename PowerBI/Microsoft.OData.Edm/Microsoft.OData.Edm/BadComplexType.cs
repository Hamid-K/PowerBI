using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000054 RID: 84
	internal class BadComplexType : BadNamedStructuredType, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x060001BC RID: 444 RVA: 0x00004E41 File Offset: 0x00003041
		public BadComplexType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000268B File Offset: 0x0000088B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}
	}
}
