using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000007 RID: 7
	internal class BadPathType : BadType, IEdmPathType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002613 File Offset: 0x00000813
		public BadPathType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000261C File Offset: 0x0000081C
		public string Name
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000261C File Offset: 0x0000081C
		public string Namespace
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000261C File Offset: 0x0000081C
		public string FullName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000261C File Offset: 0x0000081C
		public EdmPathTypeKind PathKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000261C File Offset: 0x0000081C
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002623 File Offset: 0x00000823
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Path;
			}
		}
	}
}
