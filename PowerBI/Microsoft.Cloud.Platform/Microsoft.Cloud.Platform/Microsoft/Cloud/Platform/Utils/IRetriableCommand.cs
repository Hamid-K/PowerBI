using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000237 RID: 567
	public interface IRetriableCommand
	{
		// Token: 0x06000EC5 RID: 3781
		IAsyncResult BeginExecute(RetrierContext retrierContext, AsyncCallback callback, object userState);

		// Token: 0x06000EC6 RID: 3782
		void EndExecute(IAsyncResult ar);
	}
}
