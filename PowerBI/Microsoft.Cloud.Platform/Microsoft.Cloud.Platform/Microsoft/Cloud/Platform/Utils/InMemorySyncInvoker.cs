using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000233 RID: 563
	public class InMemorySyncInvoker : IInMemoryInvoker
	{
		// Token: 0x06000EAB RID: 3755 RVA: 0x00033130 File Offset: 0x00031330
		public IAsyncResult BeginInvoke<T>(Invoker<T> i, AsyncCallback callback, object asyncState)
		{
			T t = i();
			AsyncResult<T> asyncResult = new AsyncResult<T>(callback, asyncState);
			asyncResult.SignalCompletion(true, t);
			return asyncResult;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00033153 File Offset: 0x00031353
		public T EndInvoke<T>(IAsyncResult ar)
		{
			return ((AsyncResult<T>)ar).End();
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00033160 File Offset: 0x00031360
		public IAsyncResult BeginInvoke(EmptyInvoker i, AsyncCallback callback, object asyncState)
		{
			i();
			VoidAsyncResult voidAsyncResult = new VoidAsyncResult(callback, asyncState);
			voidAsyncResult.SignalCompletion(true);
			return voidAsyncResult;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00023BE0 File Offset: 0x00021DE0
		public void EndInvoke(IAsyncResult ar)
		{
			((VoidAsyncResult)ar).End();
		}
	}
}
