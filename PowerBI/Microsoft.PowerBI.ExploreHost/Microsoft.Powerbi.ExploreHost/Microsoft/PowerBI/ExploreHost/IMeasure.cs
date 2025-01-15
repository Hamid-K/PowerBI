using System;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000030 RID: 48
	public interface IMeasure
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600016F RID: 367
		bool HasErrors { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000170 RID: 368
		string ErrorMessage { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000171 RID: 369
		bool CanDelete { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000172 RID: 370
		bool CanEdit { get; }
	}
}
