using System;
using System.Runtime.Remoting.Messaging;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000232 RID: 562
	public class InMemoryAsyncInvoker : IInMemoryInvoker
	{
		// Token: 0x06000EA6 RID: 3750 RVA: 0x000330EA File Offset: 0x000312EA
		public IAsyncResult BeginInvoke<T>(Invoker<T> i, AsyncCallback callback, object asyncState)
		{
			return i.BeginInvoke(callback, asyncState);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000330F4 File Offset: 0x000312F4
		public T EndInvoke<T>(IAsyncResult ar)
		{
			return ((Invoker<T>)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0003310C File Offset: 0x0003130C
		public IAsyncResult BeginInvoke(EmptyInvoker i, AsyncCallback callback, object asyncState)
		{
			return i.BeginInvoke(callback, asyncState);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00033116 File Offset: 0x00031316
		public void EndInvoke(IAsyncResult ar)
		{
			((EmptyInvoker)((AsyncResult)ar).AsyncDelegate).EndInvoke(ar);
		}
	}
}
