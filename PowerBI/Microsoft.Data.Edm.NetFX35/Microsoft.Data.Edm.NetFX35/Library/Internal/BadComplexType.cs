using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000EE RID: 238
	internal class BadComplexType : BadNamedStructuredType, IEdmComplexType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x060004BA RID: 1210 RVA: 0x0000C437 File Offset: 0x0000A637
		public BadComplexType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000C441 File Offset: 0x0000A641
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Complex;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000C444 File Offset: 0x0000A644
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}
	}
}
