using System;

namespace Microsoft.PowerBI.ExploreHost
{
	// Token: 0x02000031 RID: 49
	public interface IHierarchy
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000173 RID: 371
		bool CanDelete { get; }

		// Token: 0x06000174 RID: 372
		IHierarchyLevel FindLevel(string name);
	}
}
