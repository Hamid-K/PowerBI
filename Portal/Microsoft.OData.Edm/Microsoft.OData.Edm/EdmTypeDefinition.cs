using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000035 RID: 53
	public class EdmTypeDefinition : EdmType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmFullNamedElement
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00003A82 File Offset: 0x00001C82
		public EdmTypeDefinition(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType))
		{
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003A98 File Offset: 0x00001C98
		public EdmTypeDefinition(string namespaceName, string name, IEdmPrimitiveType underlyingType)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(underlyingType, "underlyingType");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.underlyingType = underlyingType;
			this.name = name;
			this.namespaceName = namespaceName;
			this.fullName = EdmUtil.GetFullNameForSchemaElement(this.namespaceName, this.name);
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003AFB File Offset: 0x00001CFB
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00003AFE File Offset: 0x00001CFE
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00003B06 File Offset: 0x00001D06
		public string FullName
		{
			get
			{
				return this.fullName;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003B0E File Offset: 0x00001D0E
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003B16 File Offset: 0x00001D16
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x04000054 RID: 84
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x04000055 RID: 85
		private readonly string namespaceName;

		// Token: 0x04000056 RID: 86
		private readonly string name;

		// Token: 0x04000057 RID: 87
		private readonly string fullName;
	}
}
