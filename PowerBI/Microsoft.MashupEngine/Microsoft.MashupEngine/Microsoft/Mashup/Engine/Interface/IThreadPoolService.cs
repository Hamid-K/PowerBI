using System;
using System.Threading;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E3 RID: 227
	public interface IThreadPoolService
	{
		// Token: 0x06000355 RID: 853
		void QueueUserWorkItem(WaitCallback callback, object state);

		// Token: 0x06000356 RID: 854
		void StartThread(ParameterizedThreadStart threadStart, object state);
	}
}
