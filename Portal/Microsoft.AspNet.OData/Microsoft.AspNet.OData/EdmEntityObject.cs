using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200003D RID: 61
	[NonValidatingParameterBinding]
	public class EdmEntityObject : EdmStructuredObject, IEdmEntityObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000075A2 File Offset: 0x000057A2
		public EdmEntityObject(IEdmEntityType edmType)
			: this(edmType, false)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000075AC File Offset: 0x000057AC
		public EdmEntityObject(IEdmEntityTypeReference edmType)
			: this(edmType.EntityDefinition(), edmType.IsNullable)
		{
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007500 File Offset: 0x00005700
		public EdmEntityObject(IEdmEntityType edmType, bool isNullable)
			: base(edmType, isNullable)
		{
		}
	}
}
