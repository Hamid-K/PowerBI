using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B6 RID: 438
	internal class DiagEventManager
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0002FFE6 File Offset: 0x0002E1E6
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0002FFED File Offset: 0x0002E1ED
		public static bool IsEventManagementEnabled { get; set; }

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002FFF8 File Offset: 0x0002E1F8
		internal static void Init(ProcessOpStateCallback _opCallback)
		{
			lock (DiagEventManager._initLock)
			{
				if (!DiagEventManager.IsEventManagementEnabled)
				{
					DiagEventManager._requestStates = new DiagOperationPool();
					DiagEventManager._eventProcessingTimer = new global::System.Threading.Timer(new TimerCallback(DiagEventManager.TimerBasedEventAnalysis), null, TimeSpan.FromMilliseconds((double)DiagEventManager.AsyncProcessingInterval), TimeSpan.FromMilliseconds((double)DiagEventManager.AsyncProcessingInterval));
					DiagEventManager.IsEventManagementEnabled = true;
					DiagEventManager._opStateCompletedCallback = _opCallback;
					DiagEventManager.ResetErrorCounts();
				}
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00030080 File Offset: 0x0002E280
		private static void ResetErrorCounts()
		{
			for (int i = 0; i < DiagEventManager._errorCounts.Length; i++)
			{
				DiagEventManager._errorCounts[i] = 0;
			}
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000300A7 File Offset: 0x0002E2A7
		internal static void AddRequestStates(DiagOperationState states)
		{
			if (states != null)
			{
				DiagEventManager.AddRequestStates(states.UniqueIdentifier, states, true);
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000300B9 File Offset: 0x0002E2B9
		internal static void AddRequestStates(string key, DiagOperationState states, bool isAsync)
		{
			if (!isAsync)
			{
				DiagEventManager._opStateCompletedCallback(states, false);
				states = null;
				return;
			}
			if (DiagEventManager._requestStates.TotalPublished < DiagEventManager.BufferThrottlingFactor * DiagEventManager.MaxBufferedOperations)
			{
				DiagEventManager._requestStates.AddRequestStates(key, states);
			}
			DiagEventManager.CheckToDump();
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000300F8 File Offset: 0x0002E2F8
		internal static void Complete(DiagOperationState states, bool isAsync)
		{
			if (!isAsync)
			{
				DiagEventManager._requestStates.RemoveRequestState(states);
				DiagEventManager._opStateCompletedCallback(states, false);
				states = null;
				return;
			}
			DiagEventManager.AddRequestStates(states);
			DiagEventManager.CheckToDump();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00030128 File Offset: 0x0002E328
		internal static void AddRequestStates(List<string> keys, DiagOperationState states, bool isAsync)
		{
			if (keys != null)
			{
				foreach (string text in keys)
				{
					DiagEventManager.AddRequestStates(text, states, isAsync);
				}
				DiagEventManager.CheckToDump();
			}
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00030180 File Offset: 0x0002E380
		internal static void AddRequestStates(DiagOperationState states, bool isAsync)
		{
			if (isAsync)
			{
				DiagEventManager.AddRequestStates(states.UniqueIdentifier, states, true);
				return;
			}
			DiagEventManager._opStateCompletedCallback(states, false);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000301A0 File Offset: 0x0002E3A0
		internal static DiagOperationState GetOperationState(string key)
		{
			return DiagEventManager._requestStates.GetOperationState(key);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000301B0 File Offset: 0x0002E3B0
		internal static DiagOperationState GetOrAddOperationState(string key)
		{
			DiagOperationState diagOperationState;
			if (DiagEventManager._requestStates.TotalPublished < DiagEventManager.BufferThrottlingFactor * DiagEventManager.MaxBufferedOperations)
			{
				diagOperationState = DiagEventManager._requestStates.GetOrAddOperationState(key);
				DiagEventManager.CheckToDump();
			}
			else
			{
				diagOperationState = DiagEventManager._requestStates.GetOperationState(key);
			}
			return diagOperationState;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000301F4 File Offset: 0x0002E3F4
		internal static void AddRequestStates(string key, DiagEvent ev)
		{
			if (!string.IsNullOrEmpty(key) && ev != null)
			{
				DiagEventManager._requestStates.AddRequestStates(key, ev);
				DiagEventManager.CheckToDump();
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00030212 File Offset: 0x0002E412
		private static void CheckToDump()
		{
			DiagEventManager.CheckToDump(false);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0003021A File Offset: 0x0002E41A
		private static void CheckToDump(bool isTimerBased)
		{
			if ((DiagEventManager._requestStates.TotalPublished > DiagEventManager.MaxBufferedOperations || isTimerBased) && DiagEventManager.TryEnterAnalysisLock())
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(DiagEventManager.StartEventAnalysis), null);
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0003024C File Offset: 0x0002E44C
		public static void DrainRequestsOnException(ProcessOpStateCallback dumpExceptionCallback)
		{
			if (!DiagEventManager.IsEventManagementEnabled)
			{
				return;
			}
			ProcessOpStateCallback opStateCompletedCallback = DiagEventManager._opStateCompletedCallback;
			EventLogWriter.WriteError("DiagnosticsError", "Unhandled exception. Dumping all the last few requests", new object[0]);
			DiagEventManager._opStateCompletedCallback = dumpExceptionCallback;
			if (DiagEventManager._requestStates.TotalPublished > 0)
			{
				DiagOperationPool requestStates = DiagEventManager._requestStates;
				DiagEventManager._requestStates = new DiagOperationPool();
				DiagEventManager.ProcessStates(requestStates.GetStateEnumerator());
			}
			EventLogWriter.WriteError("DiagnosticsError", "Unhandled exception. Dumping all the last few requests finished.", new object[0]);
			DiagEventManager._opStateCompletedCallback = opStateCompletedCallback;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000302C7 File Offset: 0x0002E4C7
		private static bool TryEnterAnalysisLock()
		{
			return Interlocked.CompareExchange(ref DiagEventManager._lock, 1, 0) == 0;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000302D8 File Offset: 0x0002E4D8
		private static void ExitAnalysisLock()
		{
			ReleaseAssert.IsTrue(Interlocked.Exchange(ref DiagEventManager._lock, 0) != 0, "Exit of Analysis process is done by multiple threads");
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000302F5 File Offset: 0x0002E4F5
		private static void TimerBasedEventAnalysis(object state)
		{
			DiagEventManager.CheckToDump(true);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x00030300 File Offset: 0x0002E500
		private static void StartEventAnalysis(object state)
		{
			if (Provider.IsEnabled(TraceLevel.Info))
			{
				EventLogWriter.WriteInfo("DiagnosticStats", "Dump process started", new object[0]);
			}
			try
			{
				DiagEventManager.ResetErrorCounts();
				Stopwatch stopwatch = new Stopwatch();
				int num = 0;
				stopwatch.Start();
				if (DiagEventManager._requestStates.TotalPublished > 0)
				{
					DiagOperationPool requestStates = DiagEventManager._requestStates;
					num = requestStates.TotalPublished;
					DiagEventManager._requestStates = new DiagOperationPool();
					DiagEventManager.ProcessStates(requestStates.GetStateEnumerator());
				}
				stopwatch.Stop();
				if (Provider.IsEnabled(TraceLevel.Info))
				{
					EventLogWriter.WriteInfo("DiagnosticStats", "Dump process completed and took {0} msec: {1} ticks for {2} requests ", new object[] { stopwatch.ElapsedMilliseconds, stopwatch.ElapsedTicks, num });
				}
			}
			finally
			{
				DiagEventManager.ExitAnalysisLock();
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000303D0 File Offset: 0x0002E5D0
		private static void ProcessStates(IEnumerator<KeyValuePair<string, DiagOperationState>> allStates)
		{
			if (allStates == null)
			{
				return;
			}
			while (allStates.MoveNext())
			{
				KeyValuePair<string, DiagOperationState> keyValuePair = allStates.Current;
				if (keyValuePair.Value != null)
				{
					DiagOperationState value = keyValuePair.Value;
					if (value.ErrorCode < 0 || value.ErrorCode >= DiagEventManager._errorCounts.Length)
					{
						EventLogWriter.WriteInfo("DiagnosticStats", "Unknown error code found for Opstate:{0}, Code:{1}", new object[]
						{
							value.ToString(),
							value.ErrorCode
						});
						return;
					}
					bool flag = false;
					if (DiagEventManager._errorCounts[value.ErrorCode] > DiagEventManager.MaxErrorsPerType)
					{
						flag = true;
					}
					bool flag2 = DiagEventManager._opStateCompletedCallback(keyValuePair.Value, flag);
					if (flag2)
					{
						DiagEventManager._errorCounts[value.ErrorCode] = DiagEventManager._errorCounts[value.ErrorCode] + 1;
					}
				}
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0003049E File Offset: 0x0002E69E
		public static void Clear()
		{
			if (DiagEventManager._eventProcessingTimer != null)
			{
				DiagEventManager._eventProcessingTimer.Dispose();
				DiagEventManager.CheckToDump(true);
			}
		}

		// Token: 0x040009DF RID: 2527
		private static DiagOperationPool _requestStates;

		// Token: 0x040009E0 RID: 2528
		private static int[] _errorCounts = new int[73];

		// Token: 0x040009E1 RID: 2529
		private static global::System.Threading.Timer _eventProcessingTimer;

		// Token: 0x040009E2 RID: 2530
		private static int _lock = 0;

		// Token: 0x040009E3 RID: 2531
		private static object _initLock = new object();

		// Token: 0x040009E4 RID: 2532
		private static ProcessOpStateCallback _opStateCompletedCallback;

		// Token: 0x040009E5 RID: 2533
		public static int AsyncProcessingInterval = 30000;

		// Token: 0x040009E6 RID: 2534
		public static int MaxBufferedOperations = 300;

		// Token: 0x040009E7 RID: 2535
		public static int MaxErrorsPerType = 5;

		// Token: 0x040009E8 RID: 2536
		public static int BufferThrottlingFactor = 3;
	}
}
