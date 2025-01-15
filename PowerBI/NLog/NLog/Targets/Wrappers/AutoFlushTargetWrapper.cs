using System;
using NLog.Common;
using NLog.Conditions;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000063 RID: 99
	[Target("AutoFlushWrapper", IsWrapper = true)]
	public class AutoFlushTargetWrapper : WrapperTargetBase
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00016298 File Offset: 0x00014498
		// (set) Token: 0x0600088D RID: 2189 RVA: 0x000162A0 File Offset: 0x000144A0
		public ConditionExpression Condition { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x000162AC File Offset: 0x000144AC
		// (set) Token: 0x0600088F RID: 2191 RVA: 0x000162D2 File Offset: 0x000144D2
		public bool AsyncFlush
		{
			get
			{
				return this._asyncFlush ?? true;
			}
			set
			{
				this._asyncFlush = new bool?(value);
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000162E0 File Offset: 0x000144E0
		public AutoFlushTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x000162E9 File Offset: 0x000144E9
		public AutoFlushTargetWrapper(string name, Target wrappedTarget)
			: this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x000162F9 File Offset: 0x000144F9
		public AutoFlushTargetWrapper(Target wrappedTarget)
		{
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00016313 File Offset: 0x00014513
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (this._asyncFlush == null && base.WrappedTarget is BufferingTargetWrapper)
			{
				this.AsyncFlush = false;
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001633C File Offset: 0x0001453C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (this.Condition != null && !this.Condition.Evaluate(logEvent.LogEvent).Equals(true))
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
				return;
			}
			if (this.AsyncFlush)
			{
				AsyncContinuation currentContinuation = logEvent.Continuation;
				AsyncContinuation asyncContinuation = delegate(Exception ex)
				{
					if (ex == null)
					{
						this.WrappedTarget.Flush(delegate(Exception e)
						{
						});
					}
					this._pendingManualFlushList.CompleteOperation(ex);
					currentContinuation(ex);
				};
				this._pendingManualFlushList.BeginOperation();
				base.WrappedTarget.WriteAsyncLogEvent(logEvent.LogEvent.WithContinuation(asyncContinuation));
				return;
			}
			base.WrappedTarget.WriteAsyncLogEvent(logEvent);
			this.FlushAsync(delegate(Exception e)
			{
			});
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00016404 File Offset: 0x00014604
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncContinuation asyncContinuation2 = this._pendingManualFlushList.RegisterCompletionNotification(asyncContinuation);
			base.WrappedTarget.Flush(asyncContinuation2);
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001642A File Offset: 0x0001462A
		protected override void CloseTarget()
		{
			this._pendingManualFlushList.Clear();
			base.CloseTarget();
		}

		// Token: 0x040001E0 RID: 480
		private bool? _asyncFlush;

		// Token: 0x040001E1 RID: 481
		private readonly AsyncOperationCounter _pendingManualFlushList = new AsyncOperationCounter();
	}
}
