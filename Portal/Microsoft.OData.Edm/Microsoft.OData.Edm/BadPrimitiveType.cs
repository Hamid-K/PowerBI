using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000060 RID: 96
	internal class BadPrimitiveType : BadType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00005158 File Offset: 0x00003358
		public BadPrimitiveType(string qualifiedName, EdmPrimitiveTypeKind primitiveKind, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.primitiveKind = primitiveKind;
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000518D File Offset: 0x0000338D
		public EdmPrimitiveTypeKind PrimitiveKind
		{
			get
			{
				return this.primitiveKind;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005195 File Offset: 0x00003395
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000519D File Offset: 0x0000339D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000051A5 File Offset: 0x000033A5
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Primitive;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x040000B5 RID: 181
		private readonly EdmPrimitiveTypeKind primitiveKind;

		// Token: 0x040000B6 RID: 182
		private readonly string name;

		// Token: 0x040000B7 RID: 183
		private readonly string namespaceName;

		// Token: 0x040000B8 RID: 184
		private readonly string fullName;
	}
}
