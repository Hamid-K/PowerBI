using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x02000126 RID: 294
	internal class BadCollectionType : BadType, IEdmCollectionType, IEdmType, IEdmElement
	{
		// Token: 0x060005D0 RID: 1488 RVA: 0x0000E15A File Offset: 0x0000C35A
		public BadCollectionType(IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.elementType = new BadTypeReference(new BadType(errors), true);
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000E175 File Offset: 0x0000C375
		public override EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Collection;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000E178 File Offset: 0x0000C378
		public IEdmTypeReference ElementType
		{
			get
			{
				return this.elementType;
			}
		}

		// Token: 0x0400022F RID: 559
		private readonly IEdmTypeReference elementType;
	}
}
