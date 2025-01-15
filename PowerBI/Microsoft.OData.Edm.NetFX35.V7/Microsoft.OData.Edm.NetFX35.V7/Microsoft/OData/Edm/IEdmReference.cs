using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000B5 RID: 181
	public interface IEdmReference : IEdmElement
	{
		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004A9 RID: 1193
		Uri Uri { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060004AA RID: 1194
		IEnumerable<IEdmInclude> Includes { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060004AB RID: 1195
		IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations { get; }
	}
}
