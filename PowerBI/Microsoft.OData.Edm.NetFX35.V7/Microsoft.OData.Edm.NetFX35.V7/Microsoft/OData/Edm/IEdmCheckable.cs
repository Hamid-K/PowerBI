using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000086 RID: 134
	public interface IEdmCheckable
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600045C RID: 1116
		IEnumerable<EdmError> Errors { get; }
	}
}
