using System;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x020001FD RID: 509
	public class EdmEntityReferenceType : EdmType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x06000BF3 RID: 3059 RVA: 0x00021E14 File Offset: 0x00020014
		public EdmEntityReferenceType(IEdmEntityType entityType)
		{
			EdmUtil.CheckArgumentNull<IEdmEntityType>(entityType, "entityType");
			this.entityType = entityType;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00021E2F File Offset: 0x0002002F
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00021E32 File Offset: 0x00020032
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000579 RID: 1401
		private readonly IEdmEntityType entityType;
	}
}
