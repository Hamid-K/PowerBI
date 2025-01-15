using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000038 RID: 56
	internal class BadPrimitiveType : BadType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x06000284 RID: 644 RVA: 0x00009437 File Offset: 0x00007637
		public BadPrimitiveType(string qualifiedName, EdmPrimitiveTypeKind primitiveKind, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.primitiveKind = primitiveKind;
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00009466 File Offset: 0x00007666
		public EdmPrimitiveTypeKind PrimitiveKind
		{
			get
			{
				return this.primitiveKind;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000946E File Offset: 0x0000766E
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00009476 File Offset: 0x00007676
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000288 RID: 648 RVA: 0x00008D76 File Offset: 0x00006F76
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Primitive;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly EdmPrimitiveTypeKind primitiveKind;

		// Token: 0x0400005D RID: 93
		private readonly string name;

		// Token: 0x0400005E RID: 94
		private readonly string namespaceName;
	}
}
