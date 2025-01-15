using System;
using System.ComponentModel;
using NLog.Common;
using NLog.Time;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200006C RID: 108
	[Target("LimitingWrapper", IsWrapper = true)]
	public class LimitingTargetWrapper : WrapperTargetBase
	{
		// Token: 0x060008EC RID: 2284 RVA: 0x00017146 File Offset: 0x00015346
		public LimitingTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001714F File Offset: 0x0001534F
		public LimitingTargetWrapper(string name, Target wrappedTarget)
			: this(wrappedTarget, 1000, TimeSpan.FromHours(1.0))
		{
			base.Name = name;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00017172 File Offset: 0x00015372
		public LimitingTargetWrapper(Target wrappedTarget)
			: this(wrappedTarget, 1000, TimeSpan.FromHours(1.0))
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001718E File Offset: 0x0001538E
		public LimitingTargetWrapper(Target wrappedTarget, int messageLimit, TimeSpan interval)
		{
			this.MessageLimit = messageLimit;
			this.Interval = interval;
			base.WrappedTarget = wrappedTarget;
			base.OptimizeBufferReuse = base.GetType() == typeof(LimitingTargetWrapper);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060008F0 RID: 2288 RVA: 0x000171C6 File Offset: 0x000153C6
		// (set) Token: 0x060008F1 RID: 2289 RVA: 0x000171CE File Offset: 0x000153CE
		[DefaultValue(1000)]
		public int MessageLimit { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x000171D7 File Offset: 0x000153D7
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x000171DF File Offset: 0x000153DF
		[DefaultValue(typeof(TimeSpan), "01:00")]
		public TimeSpan Interval { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000171E8 File Offset: 0x000153E8
		public DateTime IntervalResetsAt
		{
			get
			{
				return this._firstWriteInInterval + this.Interval;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000171FB File Offset: 0x000153FB
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00017203 File Offset: 0x00015403
		public int MessagesWrittenCount { get; private set; }

		// Token: 0x060008F7 RID: 2295 RVA: 0x0001720C File Offset: 0x0001540C
		protected override void InitializeTarget()
		{
			if (this.MessageLimit <= 0)
			{
				throw new NLogConfigurationException("The LimitingTargetWrapper's MessageLimit property must be > 0.");
			}
			if (this.Interval <= TimeSpan.Zero)
			{
				throw new NLogConfigurationException("The LimitingTargetWrapper's property Interval must be > 0.");
			}
			base.InitializeTarget();
			this.ResetInterval();
			InternalLogger.Trace<string, int, TimeSpan>("LimitingWrapper(Name={0}): Initialized with MessageLimit={1} and Interval={2}.", base.Name, this.MessageLimit, this.Interval);
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00017274 File Offset: 0x00015474
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (this.IsIntervalExpired())
			{
				this.ResetInterval();
				InternalLogger.Debug<string, TimeSpan>("LimitingWrapper(Name={0}): New interval of '{1}' started.", base.Name, this.Interval);
			}
			if (this.MessagesWrittenCount < this.MessageLimit)
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
				int messagesWrittenCount = this.MessagesWrittenCount;
				this.MessagesWrittenCount = messagesWrittenCount + 1;
				return;
			}
			logEvent.Continuation(null);
			InternalLogger.Trace<string, int>("LimitingWrapper(Name={0}): Discarded event, because MessageLimit of '{1}' was reached.", base.Name, this.MessageLimit);
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x000172F3 File Offset: 0x000154F3
		private void ResetInterval()
		{
			this._firstWriteInInterval = TimeSource.Current.Time;
			this.MessagesWrittenCount = 0;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x0001730C File Offset: 0x0001550C
		private bool IsIntervalExpired()
		{
			return TimeSource.Current.Time - this._firstWriteInInterval > this.Interval;
		}

		// Token: 0x040001FD RID: 509
		private DateTime _firstWriteInInterval;
	}
}
