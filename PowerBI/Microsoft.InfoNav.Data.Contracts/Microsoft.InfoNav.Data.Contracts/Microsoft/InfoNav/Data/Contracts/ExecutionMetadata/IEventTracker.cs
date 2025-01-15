using System;

namespace Microsoft.InfoNav.Data.Contracts.ExecutionMetadata
{
	// Token: 0x020000F6 RID: 246
	public interface IEventTracker
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000683 RID: 1667
		string Id { get; }

		// Token: 0x06000684 RID: 1668
		void SetMetric(string name, object value);
	}
}
