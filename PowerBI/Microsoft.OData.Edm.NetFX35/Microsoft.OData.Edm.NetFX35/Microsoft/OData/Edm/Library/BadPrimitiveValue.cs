using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;
using Microsoft.OData.Edm.Values;

namespace Microsoft.OData.Edm.Library
{
	// Token: 0x0200013F RID: 319
	internal class BadPrimitiveValue : BadElement, IEdmPrimitiveValue, IEdmValue, IEdmElement
	{
		// Token: 0x0600061F RID: 1567 RVA: 0x0000E60E File Offset: 0x0000C80E
		public BadPrimitiveValue(IEdmPrimitiveTypeReference type, IEnumerable<EdmError> errors)
			: base(errors)
		{
			this.type = type;
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0000E61E File Offset: 0x0000C81E
		public IEdmTypeReference Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000E626 File Offset: 0x0000C826
		public EdmValueKind ValueKind
		{
			get
			{
				return EdmValueKind.None;
			}
		}

		// Token: 0x04000246 RID: 582
		private readonly IEdmPrimitiveTypeReference type;
	}
}
