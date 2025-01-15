using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000078 RID: 120
	public interface IRdlSandboxConfig
	{
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000350 RID: 848
		IList<IRdlSandboxTypeInfo> AllowedTypes { get; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000351 RID: 849
		IList<string> DeniedMembers { get; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000352 RID: 850
		int MaxExpressionLength { get; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000353 RID: 851
		int MaxResourceSizeKB { get; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000354 RID: 852
		int MaxStringResultLength { get; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000355 RID: 853
		int MaxArrayResultLength { get; }
	}
}
