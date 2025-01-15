using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000F2 RID: 242
	public interface IEdmReference : IEdmElement
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060004CB RID: 1227
		string Uri { get; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060004CC RID: 1228
		IEnumerable<IEdmInclude> Includes { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060004CD RID: 1229
		IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations { get; }
	}
}
