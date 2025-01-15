using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200005D RID: 93
	internal class BadEnumType : BadType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x0000506E File Offset: 0x0000326E
		public BadEnumType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000509C File Offset: 0x0000329C
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return Enumerable.Empty<IEdmEnumMember>();
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00003A5C File Offset: 0x00001C5C
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000026A6 File Offset: 0x000008A6
		public bool IsFlags
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000050A3 File Offset: 0x000032A3
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060001EB RID: 491 RVA: 0x000050AB File Offset: 0x000032AB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060001EC RID: 492 RVA: 0x000050B3 File Offset: 0x000032B3
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x040000AC RID: 172
		private readonly string namespaceName;

		// Token: 0x040000AD RID: 173
		private readonly string name;

		// Token: 0x040000AE RID: 174
		private readonly string fullName;
	}
}
