using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x02000031 RID: 49
	internal class BadEnumType : BadType, IEdmEnumType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00002EB6 File Offset: 0x000010B6
		public BadEnumType(string qualifiedName, IEnumerable<EdmError> errors)
			: base(errors)
		{
			qualifiedName = qualifiedName ?? string.Empty;
			EdmUtil.TryGetNamespaceNameFromQualifiedName(qualifiedName, out this.namespaceName, out this.name);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00002EDE File Offset: 0x000010DE
		public IEnumerable<IEdmEnumMember> Members
		{
			get
			{
				return Enumerable.Empty<IEdmEnumMember>();
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002EE5 File Offset: 0x000010E5
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002EE8 File Offset: 0x000010E8
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return EdmCoreModel.Instance.GetPrimitiveType(EdmPrimitiveTypeKind.Int32);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002EF6 File Offset: 0x000010F6
		public bool IsFlags
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00002EF9 File Offset: 0x000010F9
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002EFC File Offset: 0x000010FC
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002F04 File Offset: 0x00001104
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000039 RID: 57
		private readonly string namespaceName;

		// Token: 0x0400003A RID: 58
		private readonly string name;
	}
}
