using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007A RID: 122
	public class EdmTypeDefinition : EdmType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0000C4F4 File Offset: 0x0000A6F4
		public EdmTypeDefinition(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType))
		{
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000C50C File Offset: 0x0000A70C
		public EdmTypeDefinition(string namespaceName, string name, IEdmPrimitiveType underlyingType)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(underlyingType, "underlyingType");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.underlyingType = underlyingType;
			this.name = name;
			this.namespaceName = namespaceName;
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000C558 File Offset: 0x0000A758
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00008D76 File Offset: 0x00006F76
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000C55B File Offset: 0x0000A75B
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000C563 File Offset: 0x0000A763
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000C56B File Offset: 0x0000A76B
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x0400010C RID: 268
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x0400010D RID: 269
		private readonly string namespaceName;

		// Token: 0x0400010E RID: 270
		private readonly string name;
	}
}
