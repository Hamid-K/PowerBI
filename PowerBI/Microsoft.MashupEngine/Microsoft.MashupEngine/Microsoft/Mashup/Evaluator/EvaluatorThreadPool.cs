using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CA4 RID: 7332
	public static class EvaluatorThreadPool
	{
		// Token: 0x0600B64A RID: 46666 RVA: 0x00250250 File Offset: 0x0024E450
		public static void Initialize(int maxThreadsToPool)
		{
			object obj = EvaluatorThreadPool.syncRoot;
			lock (obj)
			{
				EvaluatorThreadPool.maxThreadsToPool = maxThreadsToPool;
			}
		}

		// Token: 0x0600B64B RID: 46667 RVA: 0x00250290 File Offset: 0x0024E490
		public static void Start(ThreadStart threadStart)
		{
			if (threadStart == null)
			{
				throw new ArgumentNullException("threadStart");
			}
			EvaluatorThreadPool.Start(delegate(object state)
			{
				threadStart();
			}, null);
		}

		// Token: 0x0600B64C RID: 46668 RVA: 0x002502C4 File Offset: 0x0024E4C4
		public static void Start(ParameterizedThreadStart threadStart, object state)
		{
			if (threadStart == null)
			{
				throw new ArgumentNullException("threadStart");
			}
			EvaluatorThreadPool.EvaluatorThreadState evaluatorThreadState = null;
			object obj = EvaluatorThreadPool.syncRoot;
			lock (obj)
			{
				if (EvaluatorThreadPool.threads.Count > 0)
				{
					evaluatorThreadState = EvaluatorThreadPool.threads.Pop();
				}
			}
			if (evaluatorThreadState == null)
			{
				evaluatorThreadState = new EvaluatorThreadPool.EvaluatorThreadState
				{
					workReady = new ManualResetEvent(false)
				};
				Thread thread = new Thread(SafeThread2.CreateThreadStart(new ParameterizedThreadStart(EvaluatorThreadPool.EvaluatorThread)));
				thread.IsBackground = true;
				thread.SetApartmentState(ApartmentState.MTA);
				thread.Start(evaluatorThreadState);
			}
			obj = EvaluatorThreadPool.syncRoot;
			lock (obj)
			{
				evaluatorThreadState.threadStart = threadStart;
				evaluatorThreadState.state = state;
				evaluatorThreadState.workReady.Set();
			}
		}

		// Token: 0x0600B64D RID: 46669 RVA: 0x002503A8 File Offset: 0x0024E5A8
		private static void EvaluatorThread(object state)
		{
			EvaluatorThreadPool.EvaluatorThreadState evaluatorThreadState = (EvaluatorThreadPool.EvaluatorThreadState)state;
			for (;;)
			{
				evaluatorThreadState.workReady.WaitOne();
				object obj = EvaluatorThreadPool.syncRoot;
				ParameterizedThreadStart threadStart;
				object state2;
				lock (obj)
				{
					threadStart = evaluatorThreadState.threadStart;
					state2 = evaluatorThreadState.state;
				}
				threadStart(state2);
				obj = EvaluatorThreadPool.syncRoot;
				lock (obj)
				{
					evaluatorThreadState.threadStart = null;
					evaluatorThreadState.state = null;
					evaluatorThreadState.workReady.Reset();
					if (EvaluatorThreadPool.threads.Count < EvaluatorThreadPool.maxThreadsToPool)
					{
						EvaluatorThreadPool.threads.Push(evaluatorThreadState);
						continue;
					}
					evaluatorThreadState.workReady.Close();
				}
				break;
			}
		}

		// Token: 0x04005D22 RID: 23842
		private static readonly object syncRoot = new object();

		// Token: 0x04005D23 RID: 23843
		private static readonly Stack<EvaluatorThreadPool.EvaluatorThreadState> threads = new Stack<EvaluatorThreadPool.EvaluatorThreadState>();

		// Token: 0x04005D24 RID: 23844
		private static int maxThreadsToPool;

		// Token: 0x02001CA5 RID: 7333
		private class EvaluatorThreadState
		{
			// Token: 0x04005D25 RID: 23845
			public ManualResetEvent workReady;

			// Token: 0x04005D26 RID: 23846
			public ParameterizedThreadStart threadStart;

			// Token: 0x04005D27 RID: 23847
			public object state;
		}
	}
}
