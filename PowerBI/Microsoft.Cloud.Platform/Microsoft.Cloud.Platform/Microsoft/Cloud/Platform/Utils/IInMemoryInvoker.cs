using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000231 RID: 561
	public interface IInMemoryInvoker
	{
		// Token: 0x06000EA2 RID: 3746
		IAsyncResult BeginInvoke<T>(Invoker<T> i, AsyncCallback callback, object asyncState);

		// Token: 0x06000EA3 RID: 3747
		T EndInvoke<T>(IAsyncResult ar);

		// Token: 0x06000EA4 RID: 3748
		IAsyncResult BeginInvoke(EmptyInvoker i, AsyncCallback callback, object asyncState);

		// Token: 0x06000EA5 RID: 3749
		void EndInvoke(IAsyncResult ar);
	}
}
