using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200026E RID: 622
	public interface IPollingCommand : IIdentifiable
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001071 RID: 4209
		bool IsPollingAttemptRequired { get; }

		// Token: 0x06001072 RID: 4210
		IAsyncResult BeginExecute(AsyncCallback callback, object state);

		// Token: 0x06001073 RID: 4211
		void EndExecute(IAsyncResult ar);

		// Token: 0x06001074 RID: 4212
		bool IsTransientError(Exception ex);

		// Token: 0x06001075 RID: 4213
		void OnPollingTimeoutDepletion(TimeSpan timeout);
	}
}
