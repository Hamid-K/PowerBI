using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm
{
	// Token: 0x02000027 RID: 39
	public interface IEdmReference : IEdmElement
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B4 RID: 180
		Uri Uri { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B5 RID: 181
		IEnumerable<IEdmInclude> Includes { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B6 RID: 182
		IEnumerable<IEdmIncludeAnnotations> IncludeAnnotations { get; }
	}
}
