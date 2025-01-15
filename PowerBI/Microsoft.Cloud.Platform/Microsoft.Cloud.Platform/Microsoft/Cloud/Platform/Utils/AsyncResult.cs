using System;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Threading;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200018D RID: 397
	public class AsyncResult : IAsyncResult
	{
		// Token: 0x06000A38 RID: 2616 RVA: 0x000236A0 File Offset: 0x000218A0
		protected AsyncResult(AsyncCallback callback, object context)
		{
			this.m_callback = callback;
			this.m_context = context;
			this.m_state = 0;
			this.m_externalWaitHandle = new VolatileRef<ManualResetEvent>(null);
			this.m_internalWaitHandle = null;
			this.m_internalWaitHandleLock = new object();
			this.m_exception = null;
			this.m_creationContextStack = UtilsContext.Current.CaptureStack();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000236FD File Offset: 0x000218FD
		protected void SignalCompletionInternal(bool completedSynchronously)
		{
			this.SignalCompletionInternal(completedSynchronously, null);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00023708 File Offset: 0x00021908
		protected void SignalCompletionInternal(bool completedSynchronously, Exception ex)
		{
			if (ex != null)
			{
				if (!(ex is CrashException))
				{
					ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, ex);
				}
				this.m_exception = ex;
			}
			if (Interlocked.Exchange(ref this.m_state, completedSynchronously ? 1 : 2) != 0)
			{
				throw new InvalidOperationException("AsyncResult.SignalCompletion was called a second time");
			}
			ManualResetEvent manualResetEvent = null;
			object internalWaitHandleLock = this.m_internalWaitHandleLock;
			lock (internalWaitHandleLock)
			{
				manualResetEvent = this.m_internalWaitHandle;
			}
			if (manualResetEvent != null)
			{
				manualResetEvent.Set();
			}
			ManualResetEvent manualResetEvent2 = this.m_externalWaitHandle.VolatileRead();
			if (manualResetEvent2 != null)
			{
				manualResetEvent2.Set();
			}
			if (this.m_callback != null)
			{
				using (this.RestoreCapturedStackIfNeeded())
				{
					TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNothing, delegate
					{
						this.m_callback(this);
					});
				}
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000237E4 File Offset: 0x000219E4
		protected void EndInternal()
		{
			if (!this.IsCompleted)
			{
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				object internalWaitHandleLock = this.m_internalWaitHandleLock;
				lock (internalWaitHandleLock)
				{
					this.m_internalWaitHandle = manualResetEvent;
				}
				if (!this.IsCompleted)
				{
					manualResetEvent.WaitOne();
					manualResetEvent.Close();
				}
				else
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "AsyncResult will 'leak' a ManualResetEvent (m_internalWaitHandle) until finalization due to losing a race");
				}
			}
			if (!UtilsContext.Current.IsCurrentActivityEqualToCaptured(this.m_creationContextStack))
			{
				this.m_creationContextStack.Restore();
			}
			if (this.m_exception != null)
			{
				ExceptionDispatchInfo.Capture(this.m_exception).Throw();
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00023894 File Offset: 0x00021A94
		protected internal virtual void Dump(TraceDump dumper)
		{
			dumper.Add(base.GetType().ToString() + " (AsyncResult):");
			dumper.AddNameValue("  m_callback   ", TraceDump.Dump(this.m_callback));
			dumper.AddNameValue("  m_context    ", this.m_context);
			dumper.AddNameValue("  m_state      ", this.GetStateAsString());
			dumper.AddNameValue("  m_exception  ", this.m_exception);
			dumper.AddNameValue("  m_creationContextStack ", this.m_creationContextStack);
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00023916 File Offset: 0x00021B16
		public object AsyncState
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00023920 File Offset: 0x00021B20
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				ManualResetEvent manualResetEvent = this.m_externalWaitHandle.VolatileRead();
				if (manualResetEvent == null)
				{
					bool isCompleted = this.IsCompleted;
					ManualResetEvent manualResetEvent2 = new ManualResetEvent(isCompleted);
					manualResetEvent = this.m_externalWaitHandle.InterlockedCompareExchange(manualResetEvent2, null);
					if (manualResetEvent != null)
					{
						manualResetEvent2.Close();
					}
					else
					{
						manualResetEvent = manualResetEvent2;
						if (!isCompleted && this.IsCompleted)
						{
							manualResetEvent.Set();
						}
					}
				}
				return manualResetEvent;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x00023978 File Offset: 0x00021B78
		public bool CompletedSynchronously
		{
			get
			{
				return Thread.VolatileRead(ref this.m_state) == 1;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00023988 File Offset: 0x00021B88
		public bool IsCompleted
		{
			get
			{
				return Thread.VolatileRead(ref this.m_state) != 0;
			}
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00023998 File Offset: 0x00021B98
		private string GetStateAsString()
		{
			int num = Thread.VolatileRead(ref this.m_state);
			switch (num)
			{
			case 0:
				return "Pending";
			case 1:
				return "CompletedSynchronously";
			case 2:
				return "CompletedAsynchronously";
			default:
				return num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x000239E3 File Offset: 0x00021BE3
		[CanBeNull]
		private IDisposable RestoreCapturedStackIfNeeded()
		{
			if (!UtilsContext.Current.IsCurrentActivityEqualToCaptured(this.m_creationContextStack))
			{
				return this.m_creationContextStack.Restore();
			}
			return null;
		}

		// Token: 0x040003FD RID: 1021
		private readonly AsyncCallback m_callback;

		// Token: 0x040003FE RID: 1022
		private readonly object m_context;

		// Token: 0x040003FF RID: 1023
		private int m_state;

		// Token: 0x04000400 RID: 1024
		private VolatileRef<ManualResetEvent> m_externalWaitHandle;

		// Token: 0x04000401 RID: 1025
		private ManualResetEvent m_internalWaitHandle;

		// Token: 0x04000402 RID: 1026
		private object m_internalWaitHandleLock;

		// Token: 0x04000403 RID: 1027
		private Exception m_exception;

		// Token: 0x04000404 RID: 1028
		private IContextStack m_creationContextStack;

		// Token: 0x04000405 RID: 1029
		internal const string c_captureCreationCallStackTweakName = "Microsoft.Cloud.Platform.Utils.AsyncResult.CaptureCreationCallStack";

		// Token: 0x02000661 RID: 1633
		private static class State
		{
			// Token: 0x040011FB RID: 4603
			public const int Pending = 0;

			// Token: 0x040011FC RID: 4604
			public const string c_pending = "Pending";

			// Token: 0x040011FD RID: 4605
			public const int CompletedSynchronously = 1;

			// Token: 0x040011FE RID: 4606
			public const string c_completedSynchronously = "CompletedSynchronously";

			// Token: 0x040011FF RID: 4607
			public const int CompletedAsynchronously = 2;

			// Token: 0x04001200 RID: 4608
			public const string c_completedAsynchronously = "CompletedAsynchronously";
		}
	}
}
