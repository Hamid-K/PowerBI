using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001D8 RID: 472
	public class EdmCollectionType : EdmType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060009DF RID: 2527 RVA: 0x00019B98 File Offset: 0x00017D98
		public EdmCollectionType(IEdmTypeReference elementType)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(elementType, "elementType");
			this.elementType = elementType;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00019BB3 File Offset: 0x00017DB3
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00019BB6 File Offset: 0x00017DB6
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x040004C9 RID: 1225
		private readonly IEdmTypeReference elementType;
	}
}
