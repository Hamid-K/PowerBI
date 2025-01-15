using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using NLog.Common;
using NLog.Internal;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000061 RID: 97
	[Target("AsyncWrapper", IsWrapper = true)]
	public class AsyncTargetWrapper : WrapperTargetBase
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000867 RID: 2151 RVA: 0x000158F0 File Offset: 0x00013AF0
		// (remove) Token: 0x06000868 RID: 2152 RVA: 0x00015928 File Offset: 0x00013B28
		private event EventHandler<LogEventDroppedEventArgs> _logEventDroppedEvent;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000869 RID: 2153 RVA: 0x00015960 File Offset: 0x00013B60
		// (remove) Token: 0x0600086A RID: 2154 RVA: 0x00015998 File Offset: 0x00013B98
		private event EventHandler<LogEventQueueGrowEventArgs> _eventQueueGrowEvent;

		// Token: 0x0600086B RID: 2155 RVA: 0x000159CD File Offset: 0x00013BCD
		public AsyncTargetWrapper()
			: this(null)
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000159D6 File Offset: 0x00013BD6
		public AsyncTargetWrapper(string name, Target wrappedTarget)
			: this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x000159E6 File Offset: 0x00013BE6
		public AsyncTargetWrapper(Target wrappedTarget)
			: this(wrappedTarget, 10000, AsyncTargetWrapperOverflowAction.Discard)
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000159F8 File Offset: 0x00013BF8
		public AsyncTargetWrapper(Target wrappedTarget, int queueLimit, AsyncTargetWrapperOverflowAction overflowAction)
		{
			this._requestQueue = new AsyncRequestQueue(10000, AsyncTargetWrapperOverflowAction.Discard);
			this.TimeToSleepBetweenBatches = 1;
			this.BatchSize = 200;
			this.FullBatchSizeWriteLimit = 5;
			base.WrappedTarget = wrappedTarget;
			this.QueueLimit = queueLimit;
			this.OverflowAction = overflowAction;
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00015A70 File Offset: 0x00013C70
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x00015A78 File Offset: 0x00013C78
		[DefaultValue(200)]
		public int BatchSize { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00015A81 File Offset: 0x00013C81
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00015A89 File Offset: 0x00013C89
		[DefaultValue(1)]
		public int TimeToSleepBetweenBatches { get; set; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000873 RID: 2163 RVA: 0x00015A92 File Offset: 0x00013C92
		// (remove) Token: 0x06000874 RID: 2164 RVA: 0x00015AC2 File Offset: 0x00013CC2
		public event EventHandler<LogEventDroppedEventArgs> LogEventDropped
		{
			add
			{
				if (this._logEventDroppedEvent == null && this._requestQueue != null)
				{
					this._requestQueue.LogEventDropped += this.OnRequestQueueDropItem;
				}
				this._logEventDroppedEvent += value;
			}
			remove
			{
				this._logEventDroppedEvent -= value;
				if (this._logEventDroppedEvent == null && this._requestQueue != null)
				{
					this._requestQueue.LogEventDropped -= this.OnRequestQueueDropItem;
				}
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000875 RID: 2165 RVA: 0x00015AF2 File Offset: 0x00013CF2
		// (remove) Token: 0x06000876 RID: 2166 RVA: 0x00015B22 File Offset: 0x00013D22
		public event EventHandler<LogEventQueueGrowEventArgs> EventQueueGrow
		{
			add
			{
				if (this._eventQueueGrowEvent == null && this._requestQueue != null)
				{
					this._requestQueue.LogEventQueueGrow += this.OnRequestQueueGrow;
				}
				this._eventQueueGrowEvent += value;
			}
			remove
			{
				this._eventQueueGrowEvent -= value;
				if (this._eventQueueGrowEvent == null && this._requestQueue != null)
				{
					this._requestQueue.LogEventQueueGrow -= this.OnRequestQueueGrow;
				}
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00015B52 File Offset: 0x00013D52
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x00015B5F File Offset: 0x00013D5F
		[DefaultValue("Discard")]
		public AsyncTargetWrapperOverflowAction OverflowAction
		{
			get
			{
				return this._requestQueue.OnOverflow;
			}
			set
			{
				this._requestQueue.OnOverflow = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00015B6D File Offset: 0x00013D6D
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x00015B7A File Offset: 0x00013D7A
		[DefaultValue(10000)]
		public int QueueLimit
		{
			get
			{
				return this._requestQueue.RequestLimit;
			}
			set
			{
				this._requestQueue.RequestLimit = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00015B88 File Offset: 0x00013D88
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00015B90 File Offset: 0x00013D90
		[DefaultValue(5)]
		public int FullBatchSizeWriteLimit { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00015B9C File Offset: 0x00013D9C
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x00015BC2 File Offset: 0x00013DC2
		[DefaultValue(false)]
		public bool ForceLockingQueue
		{
			get
			{
				return this._forceLockingQueue ?? false;
			}
			set
			{
				this._forceLockingQueue = new bool?(value);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00015BD0 File Offset: 0x00013DD0
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			if (this._flushEventsInQueueDelegate == null)
			{
				this._flushEventsInQueueDelegate = new AsyncHelpersTask?(new AsyncHelpersTask(new WaitCallback(this.FlushEventsInQueue)));
			}
			AsyncHelpers.StartAsyncTask(this._flushEventsInQueueDelegate.Value, asyncContinuation);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00015C0C File Offset: 0x00013E0C
		protected override void InitializeTarget()
		{
			base.InitializeTarget();
			if (!base.OptimizeBufferReuse && base.WrappedTarget != null && base.WrappedTarget.OptimizeBufferReuse)
			{
				base.OptimizeBufferReuse = base.GetType() == typeof(AsyncTargetWrapper);
				if (!base.OptimizeBufferReuse && !this.ForceLockingQueue)
				{
					this.ForceLockingQueue = true;
				}
			}
			if (!this.ForceLockingQueue && this.OverflowAction == AsyncTargetWrapperOverflowAction.Block && this.BatchSize * 1.5m > this.QueueLimit)
			{
				this.ForceLockingQueue = true;
			}
			if (this._forceLockingQueue != null && this._forceLockingQueue.Value != this._requestQueue is AsyncRequestQueue)
			{
				this._requestQueue = (this.ForceLockingQueue ? new AsyncRequestQueue(this.QueueLimit, this.OverflowAction) : new ConcurrentRequestQueue(this.QueueLimit, this.OverflowAction));
			}
			if (this.BatchSize > this.QueueLimit && this.TimeToSleepBetweenBatches <= 1)
			{
				this.BatchSize = this.QueueLimit;
			}
			this._requestQueue.Clear();
			InternalLogger.Trace<string>("AsyncWrapper(Name={0}): Start Timer", base.Name);
			this._lazyWriterTimer = new Timer(new TimerCallback(this.ProcessPendingEvents), null, -1, -1);
			this.StartLazyWriterTimer();
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00015D6C File Offset: 0x00013F6C
		protected override void CloseTarget()
		{
			this.StopLazyWriterThread();
			if (Monitor.TryEnter(this._writeLockObject, 500))
			{
				try
				{
					this.WriteEventsInQueue(int.MaxValue, "Closing Target");
				}
				finally
				{
					Monitor.Exit(this._writeLockObject);
				}
			}
			base.CloseTarget();
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00015DC8 File Offset: 0x00013FC8
		protected virtual void StartLazyWriterTimer()
		{
			object timerLockObject = this._timerLockObject;
			lock (timerLockObject)
			{
				if (this._lazyWriterTimer != null)
				{
					if (this.TimeToSleepBetweenBatches <= 1)
					{
						InternalLogger.Trace<string>("AsyncWrapper(Name={0}): Throttled timer scheduled", base.Name);
						this._lazyWriterTimer.Change(1, -1);
					}
					else
					{
						this._lazyWriterTimer.Change(this.TimeToSleepBetweenBatches, -1);
					}
				}
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00015E48 File Offset: 0x00014048
		protected virtual bool StartInstantWriterTimer()
		{
			bool flag = false;
			bool flag3;
			try
			{
				flag = Monitor.TryEnter(this._writeLockObject);
				if (flag)
				{
					object timerLockObject = this._timerLockObject;
					lock (timerLockObject)
					{
						if (this._lazyWriterTimer != null)
						{
							this._lazyWriterTimer.Change(0, -1);
							return true;
						}
					}
				}
				flag3 = false;
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(this._writeLockObject);
				}
			}
			return flag3;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00015ED0 File Offset: 0x000140D0
		protected virtual void StopLazyWriterThread()
		{
			object timerLockObject = this._timerLockObject;
			lock (timerLockObject)
			{
				Timer lazyWriterTimer = this._lazyWriterTimer;
				if (lazyWriterTimer != null)
				{
					this._lazyWriterTimer = null;
					lazyWriterTimer.WaitForDispose(TimeSpan.FromSeconds(1.0));
				}
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00015F30 File Offset: 0x00014130
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			base.PrecalculateVolatileLayouts(logEvent.LogEvent);
			if (this._requestQueue.Enqueue(logEvent))
			{
				if (this.TimeToSleepBetweenBatches == 0)
				{
					this.StartInstantWriterTimer();
					return;
				}
				if (this.TimeToSleepBetweenBatches <= 1)
				{
					this.StartLazyWriterTimer();
				}
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00015F6C File Offset: 0x0001416C
		protected override void WriteAsyncThreadSafe(AsyncLogEventInfo logEvent)
		{
			try
			{
				this.Write(logEvent);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				logEvent.Continuation(ex);
			}
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00015FAC File Offset: 0x000141AC
		private void ProcessPendingEvents(object state)
		{
			if (this._lazyWriterTimer == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				object obj = this._writeLockObject;
				lock (obj)
				{
					if (this.WriteEventsInQueue(this.BatchSize, "Timer") == this.BatchSize)
					{
						flag = true;
					}
					if (flag && this.TimeToSleepBetweenBatches <= 1)
					{
						this.StartInstantWriterTimer();
					}
				}
			}
			catch (Exception ex)
			{
				flag = false;
				InternalLogger.Error(ex, "AsyncWrapper(Name={0}): Error in lazy writer timer procedure.", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
			finally
			{
				if (this.TimeToSleepBetweenBatches <= 1)
				{
					if (flag || this._requestQueue.IsEmpty)
					{
						goto IL_00C3;
					}
					object obj = this._writeLockObject;
					lock (obj)
					{
						this.StartLazyWriterTimer();
						goto IL_00C3;
					}
				}
				this.StartLazyWriterTimer();
				IL_00C3:;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000160B4 File Offset: 0x000142B4
		private void FlushEventsInQueue(object state)
		{
			try
			{
				AsyncContinuation asyncContinuation = state as AsyncContinuation;
				object writeLockObject = this._writeLockObject;
				lock (writeLockObject)
				{
					this.WriteEventsInQueue(int.MaxValue, "Flush Async");
					if (asyncContinuation != null)
					{
						base.FlushAsync(asyncContinuation);
					}
				}
				if (this.TimeToSleepBetweenBatches <= 1 && !this._requestQueue.IsEmpty)
				{
					this.StartLazyWriterTimer();
				}
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "AsyncWrapper(Name={0}): Error in flush procedure.", new object[] { base.Name });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00016164 File Offset: 0x00014364
		private int WriteEventsInQueue(int batchSize, string reason)
		{
			if (base.WrappedTarget == null)
			{
				InternalLogger.Error<string>("AsyncWrapper(Name={0}): WrappedTarget is NULL", base.Name);
				return 0;
			}
			int num = 0;
			for (int i = 0; i < this.FullBatchSizeWriteLimit; i++)
			{
				if (!base.OptimizeBufferReuse || batchSize == 2147483647)
				{
					AsyncLogEventInfo[] array = this._requestQueue.DequeueBatch(batchSize);
					if (array.Length != 0)
					{
						if (reason != null)
						{
							InternalLogger.Trace<string, int, string>("AsyncWrapper(Name={0}): Writing {1} events ({2})", base.Name, array.Length, reason);
						}
						base.WrappedTarget.WriteAsyncLogEvents(array);
					}
					num = array.Length;
				}
				else
				{
					using (ReusableObjectCreator<IList<AsyncLogEventInfo>>.LockOject lockOject = this._reusableAsyncLogEventList.Allocate())
					{
						IList<AsyncLogEventInfo> result = lockOject.Result;
						this._requestQueue.DequeueBatch(batchSize, result);
						if (result.Count > 0)
						{
							if (reason != null)
							{
								InternalLogger.Trace<string, int, string>("AsyncWrapper(Name={0}): Writing {1} events ({2})", base.Name, result.Count, reason);
							}
							base.WrappedTarget.WriteAsyncLogEvents(result);
						}
						num = result.Count;
					}
				}
				if (num < batchSize)
				{
					break;
				}
			}
			return num;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00016270 File Offset: 0x00014470
		private void OnRequestQueueDropItem(object sender, LogEventDroppedEventArgs logEventDroppedEventArgs)
		{
			EventHandler<LogEventDroppedEventArgs> logEventDroppedEvent = this._logEventDroppedEvent;
			if (logEventDroppedEvent == null)
			{
				return;
			}
			logEventDroppedEvent(this, logEventDroppedEventArgs);
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00016284 File Offset: 0x00014484
		private void OnRequestQueueGrow(object sender, LogEventQueueGrowEventArgs logEventQueueGrowEventArgs)
		{
			EventHandler<LogEventQueueGrowEventArgs> eventQueueGrowEvent = this._eventQueueGrowEvent;
			if (eventQueueGrowEvent == null)
			{
				return;
			}
			eventQueueGrowEvent(this, logEventQueueGrowEventArgs);
		}

		// Token: 0x040001CF RID: 463
		private readonly object _writeLockObject = new object();

		// Token: 0x040001D0 RID: 464
		private readonly object _timerLockObject = new object();

		// Token: 0x040001D1 RID: 465
		private Timer _lazyWriterTimer;

		// Token: 0x040001D2 RID: 466
		private readonly ReusableAsyncLogEventList _reusableAsyncLogEventList = new ReusableAsyncLogEventList(200);

		// Token: 0x040001D8 RID: 472
		private bool? _forceLockingQueue;

		// Token: 0x040001D9 RID: 473
		private AsyncRequestQueueBase _requestQueue;

		// Token: 0x040001DA RID: 474
		private AsyncHelpersTask? _flushEventsInQueueDelegate;
	}
}
