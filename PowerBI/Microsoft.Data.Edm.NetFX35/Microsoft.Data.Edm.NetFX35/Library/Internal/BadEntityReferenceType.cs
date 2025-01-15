using System;
using System.Collections.Generic;
using Microsoft.Data.Edm.Validation;

namespace Microsoft.Data.Edm.Library.Internal
{
	// Token: 0x020000F7 RID: 247
	internal class BadEntityReferenceType : BadType, IEdmEntityReferenceType, IEdmType, IEdmElement
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000C5DE File Offset: 0x0000A7DE
		public BadEntityReferenceType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.entityType = new BadEntityType(string.Empty, base.Errors);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000C5FD File Offset: 0x0000A7FD
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.EntityReference;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0000C600 File Offset: 0x0000A800
		public IEdmEntityType EntityType
		{
			get
			{
				return this.entityType;
			}
		}

		// Token: 0x040001C9 RID: 457
		private readonly IEdmEntityType entityType;
	}
}
