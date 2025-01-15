using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200004F RID: 79
	internal class BadEnumType : BadType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x0600011D RID: 285 RVA: 0x00003B1A File Offset: 0x00001D1A
		public BadEnumType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00003B42 File Offset: 0x00001D42
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return Enumerable.Empty<IEdmEnumMember>();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00003B49 File Offset: 0x00001D49
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00003B4C File Offset: 0x00001D4C
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00003B5A File Offset: 0x00001D5A
		public bool IsFlags
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00003B5D File Offset: 0x00001D5D
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00003B60 File Offset: 0x00001D60
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00003B68 File Offset: 0x00001D68
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000065 RID: 101
		private readonly string namespaceName;

		// Token: 0x04000066 RID: 102
		private readonly string name;
	}
}
