using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000134 RID: 308
	internal class BadEntityReferenceType : BadType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0000E381 File Offset: 0x0000C581
		public BadEntityReferenceType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.entityType = new BadEntityType(string.Empty, base.Errors);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000E3A0 File Offset: 0x0000C5A0
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000E3A3 File Offset: 0x0000C5A3
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x04000238 RID: 568
		private readonly IEdmEntityType entityType;
	}
}
