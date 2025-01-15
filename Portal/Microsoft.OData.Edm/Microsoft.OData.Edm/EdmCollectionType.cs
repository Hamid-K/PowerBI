using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200007D RID: 125
	public class EdmCollectionType : EdmType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x06000273 RID: 627 RVA: 0x0000609E File Offset: 0x0000429E
		public EdmCollectionType(IEdmTypeReference elementType)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(elementType, "elementType");
			this.elementType = elementType;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000275 RID: 629 RVA: 0x000060B9 File Offset: 0x000042B9
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x040000DA RID: 218
		private readonly IEdmTypeReference elementType;
	}
}
