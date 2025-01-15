using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000158 RID: 344
	public interface IScheduledTask : IIdentifiable
	{
		// Token: 0x060008E7 RID: 2279
		IAsyncResult BeginExecute(ScheduledTaskInformation info, AsyncCallback callback, object state);

		// Token: 0x060008E8 RID: 2280
		ScheduledTaskResult EndExecute(IAsyncResult asyncResult);

		// Token: 0x060008E9 RID: 2281
		void Abort();
	}
}
