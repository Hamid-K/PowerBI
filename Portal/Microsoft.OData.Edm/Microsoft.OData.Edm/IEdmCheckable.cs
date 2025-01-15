using System;
using System.Collections.Generic;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000076 RID: 118
	public interface IEdmCheckable
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600025E RID: 606
		IEnumerable<EdmError> Errors { get; }
	}
}
