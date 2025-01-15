using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000034 RID: 52
	internal class BadTypeDefinition : BadType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00003A2B File Offset: 0x00001C2B
		public BadTypeDefinition(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name, out this.fullName);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003A59 File Offset: 0x00001C59
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003A5C File Offset: 0x00001C5C
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003A6A File Offset: 0x00001C6A
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00003A72 File Offset: 0x00001C72
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003A7A File Offset: 0x00001C7A
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x04000051 RID: 81
		private readonly string namespaceName;

		// Token: 0x04000052 RID: 82
		private readonly string name;

		// Token: 0x04000053 RID: 83
		private readonly string fullName;
	}
}
