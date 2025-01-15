using System;
using System.Threading;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B45 RID: 6981
	public sealed class ThreadPoolService : IThreadPoolService
	{
		// Token: 0x0600AEB0 RID: 44720 RVA: 0x0023C67F File Offset: 0x0023A87F
		public void QueueUserWorkItem(WaitCallback callback, object state)
		{
			ThreadPool.QueueUserWorkItem(SafeThread2.CreateWaitCallback(callback), state);
		}

		// Token: 0x0600AEB1 RID: 44721 RVA: 0x0023C68E File Offset: 0x0023A88E
		public void StartThread(ParameterizedThreadStart threadStart, object state)
		{
			EvaluatorThreadPool.Start(threadStart, state);
		}
	}
}
