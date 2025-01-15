using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000032 RID: 50
	internal class BadEntityType : BadNamedStructuredType, IEdmEntityType, IEdmStructuredType, IEdmType, IEdmElement, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x0600025F RID: 607 RVA: 0x0000908D File Offset: 0x0000728D
		public BadEntityType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00008D69 File Offset: 0x00006F69
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00008F68 File Offset: 0x00007168
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00008EC3 File Offset: 0x000070C3
		public bool HasStream
		{
			get
			{
				return false;
			}
		}
	}
}
