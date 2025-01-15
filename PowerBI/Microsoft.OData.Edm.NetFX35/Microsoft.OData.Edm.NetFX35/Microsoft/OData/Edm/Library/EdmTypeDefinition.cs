using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020000FD RID: 253
	public class EdmTypeDefinition : EdmType, IEdmTypeDefinition, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmVocabularyAnnotatable, IEdmType, IEdmElement
	{
		// Token: 0x060004FB RID: 1275 RVA: 0x0000D1E1 File Offset: 0x0000B3E1
		public EdmTypeDefinition(string namespaceName, string name, EdmPrimitiveTypeKind underlyingType)
			: this(namespaceName, name, EdmCoreModel.Instance.GetPrimitiveType(underlyingType))
		{
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000D1F8 File Offset: 0x0000B3F8
		public EdmTypeDefinition(string namespaceName, string name, IEdmPrimitiveType underlyingType)
		{
			EdmUtil.CheckArgumentNull<IEdmPrimitiveType>(underlyingType, "underlyingType");
			EdmUtil.CheckArgumentNull<string>(namespaceName, "namespaceName");
			EdmUtil.CheckArgumentNull<string>(name, "name");
			this.underlyingType = underlyingType;
			this.name = name;
			this.namespaceName = namespaceName;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000D244 File Offset: 0x0000B444
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.TypeDefinition;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x0000D247 File Offset: 0x0000B447
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000D24A File Offset: 0x0000B44A
		public string Namespace
		{
			get
			{
				return this.namespaceName;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000D252 File Offset: 0x0000B452
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000D25A File Offset: 0x0000B45A
		public IEdmPrimitiveType UnderlyingType
		{
			get
			{
				return this.underlyingType;
			}
		}

		// Token: 0x040001DE RID: 478
		private readonly IEdmPrimitiveType underlyingType;

		// Token: 0x040001DF RID: 479
		private readonly string namespaceName;

		// Token: 0x040001E0 RID: 480
		private readonly string name;
	}
}
