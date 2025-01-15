using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000055 RID: 85
	public class EdmEntityReferenceType : EdmType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x0600033E RID: 830 RVA: 0x0000AA3E File Offset: 0x00008C3E
		public EdmEntityReferenceType(IEdmEntityType entityType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.entityType = entityType;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000AA59 File Offset: 0x00008C59
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x040000B0 RID: 176
		private readonly IEdmEntityType entityType;
	}
}
