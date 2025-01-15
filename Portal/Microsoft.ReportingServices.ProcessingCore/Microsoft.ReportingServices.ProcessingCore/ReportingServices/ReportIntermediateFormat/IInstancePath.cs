using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003B4 RID: 948
	public interface IInstancePath
	{
		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x0600268A RID: 9866
		List<InstancePathItem> InstancePath { get; }

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x0600268B RID: 9867
		IInstancePath ParentInstancePath { get; }

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x0600268C RID: 9868
		InstancePathItem InstancePathItem { get; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x0600268D RID: 9869
		string UniqueName { get; }
	}
}
