using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002A RID: 42
	internal class BadComplexType : BadNamedStructuredType, IEdmComplexType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000908D File Offset: 0x0000728D
		public BadComplexType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00009097 File Offset: 0x00007297
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}
	}
}
