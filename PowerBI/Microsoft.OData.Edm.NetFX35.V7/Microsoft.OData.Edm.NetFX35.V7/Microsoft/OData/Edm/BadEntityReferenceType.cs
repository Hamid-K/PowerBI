using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000030 RID: 48
	internal class BadEntityReferenceType : BadType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x06000251 RID: 593 RVA: 0x000091F6 File Offset: 0x000073F6
		public BadEntityReferenceType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.entityType = new BadEntityType(string.Empty, base.Errors);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00009215 File Offset: 0x00007415
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009218 File Offset: 0x00007418
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x0400004D RID: 77
		private readonly IEdmEntityType entityType;
	}
}
