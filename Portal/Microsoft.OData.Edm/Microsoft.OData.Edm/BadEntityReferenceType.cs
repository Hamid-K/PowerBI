using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000059 RID: 89
	internal class BadEntityReferenceType : BadType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x060001CF RID: 463 RVA: 0x00004FA7 File Offset: 0x000031A7
		public BadEntityReferenceType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.entityType = new BadEntityType(string.Empty, base.Errors);
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000480B File Offset: 0x00002A0B
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00004FC6 File Offset: 0x000031C6
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly IEdmEntityType entityType;
	}
}
