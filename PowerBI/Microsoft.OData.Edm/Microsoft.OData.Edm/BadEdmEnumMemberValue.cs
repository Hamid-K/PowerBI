using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000062 RID: 98
	internal class BadEdmEnumMemberValue : BadElement, IEdmEnumMemberValue, IEdmElement
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000521E File Offset: 0x0000341E
		public BadEdmEnumMemberValue(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00005227 File Offset: 0x00003427
		public long Value
		{
			get
			{
				return 0L;
			}
		}
	}
}
