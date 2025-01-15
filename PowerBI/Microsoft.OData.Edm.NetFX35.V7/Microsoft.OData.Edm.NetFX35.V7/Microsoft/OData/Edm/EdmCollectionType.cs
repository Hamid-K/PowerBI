using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000049 RID: 73
	public class EdmCollectionType : EdmType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00009C5F File Offset: 0x00007E5F
		public EdmCollectionType(IEdmTypeReference elementType)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(elementType, "elementType");
			this.elementType = elementType;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00009C7A File Offset: 0x00007E7A
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x04000071 RID: 113
		private readonly IEdmTypeReference elementType;
	}
}
