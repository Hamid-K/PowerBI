using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B4 RID: 180
	public class EdmEntityReferenceType : EdmType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x0000AEE6 File Offset: 0x000090E6
		public EdmEntityReferenceType(IEdmEntityType entityType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.entityType = entityType;
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000AF01 File Offset: 0x00009101
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000148 RID: 328
		private readonly IEdmEntityType entityType;
	}
}
