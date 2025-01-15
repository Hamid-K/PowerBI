using System;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000A RID: 10
	internal sealed class EdmCoreModelPrimitiveType : EdmType, IEdmPrimitiveType, IEdmSchemaType, IEdmSchemaElement, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable, IEdmType, IEdmCoreModelElement, IEdmFullNamedElement
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000026BF File Offset: 0x000008BF
		public EdmCoreModelPrimitiveType(EdmPrimitiveTypeKind primitiveKind)
		{
			this.Name = primitiveKind.ToString();
			this.PrimitiveKind = primitiveKind;
			this.FullName = this.Namespace + "." + this.Name;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026FD File Offset: 0x000008FD
		public string Name { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002698 File Offset: 0x00000898
		public string Namespace
		{
			get
			{
				return "Edm";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000268E File Offset: 0x0000088E
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Primitive;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002705 File Offset: 0x00000905
		public EdmPrimitiveTypeKind PrimitiveKind { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000268E File Offset: 0x0000088E
		public EdmSchemaElementKind SchemaElementKind
		{
			get
			{
				return EdmSchemaElementKind.TypeDefinition;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000270D File Offset: 0x0000090D
		public string FullName { get; }
	}
}
