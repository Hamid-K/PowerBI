using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000136 RID: 310
	internal class BadEntityType : BadNamedStructuredType, IEdmEntityType, IEdmStructuredType, IEdmSchemaType, IEdmType, IEdmTerm, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmElement
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x0000E408 File Offset: 0x0000C608
		public BadEntityType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(qualifiedName, errors)
		{
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x0000E412 File Offset: 0x0000C612
		public IEnumerable<IEdmStructuralProperty> DeclaredKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000E415 File Offset: 0x0000C615
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000E418 File Offset: 0x0000C618
		public EdmTermKind TermKind
		{
			get
			{
				return EdmTermKind.Type;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000E41B File Offset: 0x0000C61B
		public bool HasStream
		{
			get
			{
				return false;
			}
		}
	}
}
