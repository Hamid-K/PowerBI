using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000156 RID: 342
	public interface IRescheduleTaskExecution
	{
		// Token: 0x060008E5 RID: 2277
		void RescheduleNextExecutionTime(string taskName, DateTime now);
	}
}
