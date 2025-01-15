using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000081 RID: 129
	public sealed class ReportServerThreadPool
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public static void TryQueueWorkItem(ThreadWorkItem wkItem)
		{
			if (ReportServerThreadPool.IsThreadPoolPressure())
			{
				ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "Thread pool pressure. Using current thread for a work item.");
				wkItem.OriginalCallback(wkItem.State);
				wkItem.Close();
				return;
			}
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "Spawning new thread for a work item.");
			if (wkItem.PreQueue != null)
			{
				wkItem.PreQueue(wkItem.PreQueueState);
			}
			RunningJobContext jobContext = ProcessingContext.JobContext;
			ThreadPool.QueueUserWorkItem(wkItem.FullCallback, new ThreadWorkItem.WorkItemParameters(jobContext, RequestCache.GetCurrentAndAddReference()));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000B93F File Offset: 0x00009B3F
		public static void QueueUserWorkItem(WaitCallback callBack, object state)
		{
			ThreadPool.QueueUserWorkItem(new ThreadWorkItem(callBack, state).MonitoredCallback);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000B953 File Offset: 0x00009B53
		public static int IncrementCurrentReportThreads()
		{
			return Interlocked.Increment(ref ReportServerThreadPool.m_currentReportThreads);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000B95F File Offset: 0x00009B5F
		public static int DecrementCurrentReportThreads()
		{
			return Interlocked.Decrement(ref ReportServerThreadPool.m_currentReportThreads);
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000B96B File Offset: 0x00009B6B
		public static bool HasRunningReportThreads
		{
			get
			{
				return ReportServerThreadPool.m_currentReportThreads > 0;
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000B978 File Offset: 0x00009B78
		private static bool IsThreadPoolPressure()
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			ThreadPool.GetAvailableThreads(out num, out num2);
			ThreadPool.GetMaxThreads(out num3, out num4);
			ProcessingContext.JobsTracer.Trace(TraceLevel.Verbose, "Thread pool settings: Available worker: {0}, Max worker: {1}, Available IO: {2}, Max IO: {3}", new object[] { num, num3, num2, num4 });
			return num3 - num > 7;
		}

		// Token: 0x040001F1 RID: 497
		private const int ThreadsInUseThreshold = 7;

		// Token: 0x040001F2 RID: 498
		private static int m_currentReportThreads;
	}
}
