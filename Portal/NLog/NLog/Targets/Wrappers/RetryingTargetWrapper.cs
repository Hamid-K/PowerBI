using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000073 RID: 115
	[Target("RetryingWrapper", IsWrapper = true)]
	public class RetryingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000918 RID: 2328 RVA: 0x0001771B File Offset: 0x0001591B
		public RetryingTargetWrapper()
			: this(null, 3, 100)
		{
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00017727 File Offset: 0x00015927
		public RetryingTargetWrapper(string name, Target wrappedTarget, int retryCount, int retryDelayMilliseconds)
			: this(wrappedTarget, retryCount, retryDelayMilliseconds)
		{
			base.Name = name;
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0001773C File Offset: 0x0001593C
		public RetryingTargetWrapper(Target wrappedTarget, int retryCount, int retryDelayMilliseconds)
		{
			base.WrappedTarget = wrappedTarget;
			this.RetryCount = retryCount;
			this.RetryDelayMilliseconds = retryDelayMilliseconds;
			base.OptimizeBufferReuse = base.GetType() == typeof(RetryingTargetWrapper);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x0001778A File Offset: 0x0001598A
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00017792 File Offset: 0x00015992
		[DefaultValue(3)]
		public int RetryCount { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x0001779B File Offset: 0x0001599B
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x000177A3 File Offset: 0x000159A3
		[DefaultValue(100)]
		public int RetryDelayMilliseconds { get; set; }

		// Token: 0x0600091F RID: 2335 RVA: 0x000177AC File Offset: 0x000159AC
		protected override void WriteAsyncThreadSafe(IList<AsyncLogEventInfo> logEvents)
		{
			object retrySyncObject = this._retrySyncObject;
			lock (retrySyncObject)
			{
				for (int i = 0; i < logEvents.Count; i++)
				{
					if (!base.IsInitialized)
					{
						logEvents[i].Continuation(null);
					}
					else
					{
						this.WriteAsyncThreadSafe(logEvents[i]);
					}
				}
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00017824 File Offset: 0x00015A24
		protected override void WriteAsyncThreadSafe(AsyncLogEventInfo logEvent)
		{
			object retrySyncObject = this._retrySyncObject;
			lock (retrySyncObject)
			{
				this.Write(logEvent);
			}
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00017868 File Offset: 0x00015A68
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			AsyncContinuation continuation = null;
			int counter = 0;
			continuation = delegate(Exception ex)
			{
				if (ex == null)
				{
					logEvent.Continuation(null);
					return;
				}
				int num = Interlocked.Increment(ref counter);
				InternalLogger.Warn(ex, "RetryingWrapper(Name={0}): Error while writing to '{1}'. Try {2}/{3}", new object[] { this.Name, this.WrappedTarget, num, this.RetryCount });
				if (num >= this.RetryCount)
				{
					InternalLogger.Warn("Too many retries. Aborting.");
					logEvent.Continuation(ex);
					return;
				}
				int i = 0;
				while (i < this.RetryDelayMilliseconds)
				{
					int num2 = Math.Min(100, this.RetryDelayMilliseconds - i);
					AsyncHelpers.WaitForDelay(TimeSpan.FromMilliseconds((double)num2));
					i += num2;
					if (!this.IsInitialized)
					{
						InternalLogger.Warn<string>("RetryingWrapper(Name={0}): Target closed. Aborting.", this.Name);
						logEvent.Continuation(ex);
						return;
					}
				}
				this.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
			};
			base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(continuation));
		}

		// Token: 0x0400020D RID: 525
		private readonly object _retrySyncObject = new object();
	}
}
