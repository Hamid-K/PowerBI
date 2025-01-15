using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x02000016 RID: 22
	public interface ISortInformation
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000035 RID: 53
		int SortIndex { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000036 RID: 54
		SortDirection SortDirection { get; }
	}
}
