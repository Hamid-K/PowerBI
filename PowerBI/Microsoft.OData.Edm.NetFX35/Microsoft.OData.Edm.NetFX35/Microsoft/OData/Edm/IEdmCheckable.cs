using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200000B RID: 11
	public interface IEdmCheckable
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000025 RID: 37
		IEnumerable<EdmError> Errors { get; }
	}
}
