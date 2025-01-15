using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200012A RID: 298
	internal class BadComplexType : BadNamedStructuredType, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x0000E1D7 File Offset: 0x0000C3D7
		public BadComplexType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000E1E1 File Offset: 0x0000C3E1
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000E1E4 File Offset: 0x0000C3E4
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}
	}
}
