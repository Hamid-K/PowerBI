using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000029 RID: 41
	internal class BadCollectionType : BadType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x0600023A RID: 570 RVA: 0x0000906A File Offset: 0x0000726A
		public BadCollectionType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.elementType = new BadTypeReference(new BadType(errors), true);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00008D57 File Offset: 0x00006F57
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00009085 File Offset: 0x00007285
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x04000047 RID: 71
		private readonly IEdmTypeReference elementType;
	}
}
