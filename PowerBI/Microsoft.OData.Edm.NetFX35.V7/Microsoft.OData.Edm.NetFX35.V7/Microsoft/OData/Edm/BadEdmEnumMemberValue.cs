using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200002D RID: 45
	internal class BadEdmEnumMemberValue : BadElement, IEdmEnumMemberValue, IEdmElement
	{
		// Token: 0x06000245 RID: 581 RVA: 0x0000919A File Offset: 0x0000739A
		public BadEdmEnumMemberValue(IEnumerable<EdmError> errors)
			: base(errors)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000091A3 File Offset: 0x000073A3
		public long Value
		{
			get
			{
				return 0L;
			}
		}
	}
}
