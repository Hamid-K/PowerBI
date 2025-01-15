using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000053 RID: 83
	internal class BadCollectionType : BadType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060001B9 RID: 441 RVA: 0x00004E1E File Offset: 0x0000301E
		public BadCollectionType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.elementType = new BadTypeReference(new BadType(errors), true);
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060001BA RID: 442 RVA: 0x000039FB File Offset: 0x00001BFB
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00004E39 File Offset: 0x00003039
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x040000A1 RID: 161
		private readonly IEdmTypeReference elementType;
	}
}
