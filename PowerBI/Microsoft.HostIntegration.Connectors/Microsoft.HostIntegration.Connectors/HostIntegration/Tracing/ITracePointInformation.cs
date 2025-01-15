using System;
using System.Collections.Generic;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200065F RID: 1631
	public interface ITracePointInformation
	{
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003669 RID: 13929
		int Identifier { get; }

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600366A RID: 13930
		string Name { get; }

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600366B RID: 13931
		bool AllowPropertyUpdates { get; }

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600366C RID: 13932
		List<ITracePointPropertyInformation> Properties { get; }

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600366D RID: 13933
		List<ITracePointInformation> TracePoints { get; }
	}
}
