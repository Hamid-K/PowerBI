using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using NLog.Common;
using NLog.Internal;
using NLog.Targets.Wrappers;

namespace NLog.Targets
{
	// Token: 0x02000027 RID: 39
	public abstract class AsyncTaskTarget : TargetWithContext
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000091E9 File Offset: 0x000073E9
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x000091F1 File Offset: 0x000073F1
		[DefaultValue(1)]
		public int TaskDelayMilliseconds { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000091FA File Offset: 0x000073FA
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x00009202 File Offset: 0x00007402
		[DefaultValue(150)]
		public int TaskTimeoutSeconds { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000920B File Offset: 0x0000740B
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x00009213 File Offset: 0x00007413
		[DefaultValue(0)]
		public int RetryCount { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000921C File Offset: 0x0000741C
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x00009224 File Offset: 0x00007424
		[DefaultValue(500)]
		public int RetryDelayMilliseconds { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00009230 File Offset: 0x00007430
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x00009256 File Offset: 0x00007456
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

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00009264 File Offset: 0x00007464
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x00009271 File Offset: 0x00007471
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

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0000927F File Offset: 0x0000747F
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0000928C File Offset: 0x0000748C
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

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0000929A File Offset: 0x0000749A
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x000092A2 File Offset: 0x000074A2
		[DefaultValue(1)]
		public int BatchSize { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x000092AB File Offset: 0x000074AB
		protected virtual TaskScheduler TaskScheduler
		{
			get
			{
				return TaskScheduler.Default;
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000092B4 File Offset: 0x000074B4
		protected AsyncTaskTarget()
		{
			base.OptimizeBufferReuse = true;
			this.TaskTimeoutSeconds = 150;
			this.TaskDelayMilliseconds = 1;
			this.BatchSize = 1;
			this.RetryDelayMilliseconds = 500;
			this._taskCompletion = new Action<Task, object>(this.TaskCompletion);
			this._taskCancelledToken = new Action(this.TaskCancelledToken);
			this._taskTimeoutTimer = new Timer(new TimerCallback(this.TaskTimeout), null, -1, -1);
			this._requestQueue = new AsyncRequestQueue(10000, AsyncTargetWrapperOverflowAction.Discard);
			this._lazyWriterTimer = new Timer(delegate(object s)
			{
				this.TaskStartNext(null, false);
			}, null, -1, -1);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000936C File Offset: 0x0000756C
		protected override void InitializeTarget()
		{
			this._cancelTokenSource = new CancellationTokenSource();
			this._cancelTokenSource.Token.Register(this._taskCancelledToken);
			base.InitializeTarget();
			if (this.BatchSize <= 0)
			{
				this.BatchSize = 1;
			}
			if (!this.ForceLockingQueue && this.OverflowAction == AsyncTargetWrapperOverflowAction.Block && this.BatchSize * 1.5m > this.QueueLimit)
			{
				this.ForceLockingQueue = true;
			}
			if (this._forceLockingQueue != null && this._forceLockingQueue.Value != this._requestQueue is AsyncRequestQueue)
			{
				this._requestQueue = (this.ForceLockingQueue ? new AsyncRequestQueue(this.QueueLimit, this.OverflowAction) : new ConcurrentRequestQueue(this.QueueLimit, this.OverflowAction));
			}
			if (this.BatchSize > this.QueueLimit)
			{
				this.BatchSize = this.QueueLimit;
			}
		}

		// Token: 0x060004C7 RID: 1223
		protected abstract Task WriteAsyncTask(LogEventInfo logEvent, CancellationToken cancellationToken);

		// Token: 0x060004C8 RID: 1224 RVA: 0x00009470 File Offset: 0x00007670
		protected virtual Task WriteAsyncTask(IList<LogEventInfo> logEvents, CancellationToken cancellationToken)
		{
			if (logEvents.Count == 1)
			{
				return this.WriteAsyncTask(logEvents[0], cancellationToken);
			}
			Task task = null;
			for (int i = 0; i < logEvents.Count; i++)
			{
				LogEventInfo logEvent = logEvents[i];
				if (task == null)
				{
					task = this.WriteAsyncTask(logEvent, cancellationToken);
				}
				else
				{
					task = task.ContinueWith<Task>((Task t) => this.WriteAsyncTask(logEvent, cancellationToken), cancellationToken, TaskContinuationOptions.ExecuteSynchronously, this.TaskScheduler).Unwrap();
				}
			}
			return task;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00009528 File Offset: 0x00007728
		protected virtual bool RetryFailedAsyncTask(Exception exception, CancellationToken cancellationToken, int retryCountRemaining, out TimeSpan retryDelay)
		{
			if (cancellationToken.IsCancellationRequested || retryCountRemaining < 0)
			{
				retryDelay = TimeSpan.Zero;
				return false;
			}
			retryDelay = TimeSpan.FromMilliseconds((double)(this.RetryDelayMilliseconds * (this.RetryCount - retryCountRemaining) * 2 + this.RetryDelayMilliseconds));
			return true;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00009578 File Offset: 0x00007778
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			if (this._cancelTokenSource.IsCancellationRequested)
			{
				logEvent.Continuation(null);
				return;
			}
			base.PrecalculateVolatileLayouts(logEvent.LogEvent);
			if (this._requestQueue.Enqueue(logEvent))
			{
				bool flag = false;
				try
				{
					if (this._previousTask == null)
					{
						Monitor.Enter(base.SyncRoot, ref flag);
					}
					else
					{
						Monitor.TryEnter(base.SyncRoot, 50, ref flag);
					}
					if (this._previousTask == null)
					{
						this._lazyWriterTimer.Change(this.TaskDelayMilliseconds, -1);
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(base.SyncRoot);
					}
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00009620 File Offset: 0x00007820
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

		// Token: 0x060004CC RID: 1228 RVA: 0x00009660 File Offset: 0x00007860
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			Task previousTask = this._previousTask;
			if ((previousTask != null && !previousTask.IsCompleted) || !this._requestQueue.IsEmpty)
			{
				InternalLogger.Debug<string, string>("{0} Flushing {1}", base.Name, this._requestQueue.IsEmpty ? "empty queue" : "pending queue items");
				this._requestQueue.Enqueue(new AsyncLogEventInfo(null, asyncContinuation));
				this._lazyWriterTimer.Change(0, -1);
				return;
			}
			InternalLogger.Debug<string>("{0} Flushing Nothing", base.Name);
			asyncContinuation(null);
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x000096F3 File Offset: 0x000078F3
		protected override void CloseTarget()
		{
			this._taskTimeoutTimer.Change(-1, -1);
			this._cancelTokenSource.Cancel();
			this._requestQueue.Clear();
			this._previousTask = null;
			base.CloseTarget();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00009726 File Offset: 0x00007926
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this._cancelTokenSource.Dispose();
				this._taskTimeoutTimer.WaitForDispose(TimeSpan.Zero);
				this._lazyWriterTimer.WaitForDispose(TimeSpan.Zero);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00009760 File Offset: 0x00007960
		private void TaskStartNext(object previousTask, bool fullBatchCompleted)
		{
			do
			{
				object syncRoot = base.SyncRoot;
				lock (syncRoot)
				{
					if (this.CheckOtherTask(previousTask))
					{
						break;
					}
					if (!base.IsInitialized)
					{
						this._previousTask = null;
						break;
					}
					if (previousTask != null && !fullBatchCompleted && this.TaskDelayMilliseconds >= 50 && !this._requestQueue.IsEmpty)
					{
						this._previousTask = null;
						this._lazyWriterTimer.Change(this.TaskDelayMilliseconds, -1);
						break;
					}
					using (ReusableObjectCreator<IList<AsyncLogEventInfo>>.LockOject lockOject = this._reusableAsyncLogEventList.Allocate())
					{
						IList<AsyncLogEventInfo> result = lockOject.Result;
						this._requestQueue.DequeueBatch(this.BatchSize, result);
						if (result.Count <= 0)
						{
							this._previousTask = null;
							break;
						}
						if (this.TaskCreation(result))
						{
							break;
						}
					}
				}
			}
			while (!this._requestQueue.IsEmpty || previousTask != null);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00009868 File Offset: 0x00007A68
		private bool CheckOtherTask(object previousTask)
		{
			if (previousTask != null)
			{
				if (this._previousTask != null && previousTask != this._previousTask)
				{
					return true;
				}
			}
			else
			{
				Task previousTask2 = this._previousTask;
				if (previousTask2 != null && !previousTask2.IsCompleted)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000989C File Offset: 0x00007A9C
		internal Task WriteAsyncTaskWithRetry(Task firstTask, IList<LogEventInfo> logEvents, CancellationToken cancellationToken, int retryCount)
		{
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
			try
			{
				return firstTask.ContinueWith<Task>(delegate(Task t)
				{
					if (t.IsFaulted || t.IsCanceled)
					{
						if (t.Exception != null)
						{
							tcs.TrySetException(t.Exception);
						}
						Exception ex2 = AsyncTaskTarget.ExtractActualException(t.Exception);
						TimeSpan timeSpan;
						if (this.RetryFailedAsyncTask(ex2, cancellationToken, retryCount - 1, out timeSpan))
						{
							InternalLogger.Warn(ex2, "{0}: Write operation failed. {1} attempts left. Sleep {2} ms", new object[] { this.Name, retryCount, timeSpan.TotalMilliseconds });
							AsyncHelpers.WaitForDelay(timeSpan);
							if (!cancellationToken.IsCancellationRequested)
							{
								object syncRoot = this.SyncRoot;
								Task task;
								lock (syncRoot)
								{
									task = this.StartWriteAsyncTask(logEvents, cancellationToken);
								}
								if (task != null)
								{
									return this.WriteAsyncTaskWithRetry(task, logEvents, cancellationToken, retryCount - 1);
								}
							}
						}
						InternalLogger.Warn(ex2, "{0}: Write operation failed after {1} retries", new object[]
						{
							this.Name,
							this.RetryCount - retryCount
						});
					}
					else
					{
						tcs.SetResult(null);
					}
					return tcs.Task;
				}, cancellationToken, TaskContinuationOptions.ExecuteSynchronously, this.TaskScheduler).Unwrap();
			}
			catch (Exception ex)
			{
				tcs.SetException(ex);
			}
			return tcs.Task;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00009930 File Offset: 0x00007B30
		private bool TaskCreation(IList<AsyncLogEventInfo> logEvents)
		{
			Tuple<List<LogEventInfo>, List<AsyncContinuation>> tuple = null;
			try
			{
				if (this._cancelTokenSource.IsCancellationRequested)
				{
					for (int i = 0; i < logEvents.Count; i++)
					{
						logEvents[i].Continuation(null);
					}
					return false;
				}
				tuple = Interlocked.CompareExchange<Tuple<List<LogEventInfo>, List<AsyncContinuation>>>(ref this._reusableLogEvents, null, this._reusableLogEvents) ?? Tuple.Create<List<LogEventInfo>, List<AsyncContinuation>>(new List<LogEventInfo>(), new List<AsyncContinuation>());
				for (int j = 0; j < logEvents.Count; j++)
				{
					if (logEvents[j].LogEvent == null)
					{
						tuple.Item2.Add(logEvents[j].Continuation);
					}
					else
					{
						tuple.Item1.Add(logEvents[j].LogEvent);
						tuple.Item2.Add(logEvents[j].Continuation);
					}
				}
				if (tuple.Item1.Count == 0)
				{
					this.NotifyTaskCompletion(tuple.Item2, null);
					tuple.Item2.Clear();
					Interlocked.CompareExchange<Tuple<List<LogEventInfo>, List<AsyncContinuation>>>(ref this._reusableLogEvents, tuple, null);
					InternalLogger.Debug<string>("{0} Flush Completed", base.Name);
					return false;
				}
				Task task = this.StartWriteAsyncTask(tuple.Item1, this._cancelTokenSource.Token);
				if (task == null)
				{
					InternalLogger.Debug<string>("{0} WriteAsyncTask returned null", base.Name);
					this.NotifyTaskCompletion(tuple.Item2, null);
					return false;
				}
				if (this.RetryCount > 0)
				{
					task = this.WriteAsyncTaskWithRetry(task, tuple.Item1, this._cancelTokenSource.Token, this.RetryCount);
				}
				this._previousTask = task;
				if (this.TaskTimeoutSeconds > 0)
				{
					this._taskTimeoutTimer.Change(this.TaskTimeoutSeconds * 1000, -1);
				}
				task.ContinueWith(this._taskCompletion, tuple);
				return true;
			}
			catch (Exception ex)
			{
				this._previousTask = null;
				InternalLogger.Error(ex, "{0} WriteAsyncTask failed on creation", new object[] { base.Name });
				this.NotifyTaskCompletion((tuple != null) ? tuple.Item2 : null, ex);
			}
			return false;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00009B64 File Offset: 0x00007D64
		private Task StartWriteAsyncTask(IList<LogEventInfo> logEvents, CancellationToken cancellationToken)
		{
			Task task2;
			try
			{
				Task task = this.WriteAsyncTask(logEvents, cancellationToken);
				if (task != null && task.Status == TaskStatus.Created)
				{
					task.Start(this.TaskScheduler);
				}
				task2 = task;
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				InternalLogger.Error(ex, "{0} WriteAsyncTask failed on creation", new object[] { base.Name });
				task2 = Task.Factory.StartNew(delegate(object e)
				{
					throw (Exception)e;
				}, new AggregateException(new Exception[] { ex }), this._cancelTokenSource.Token, TaskCreationOptions.None, this.TaskScheduler);
			}
			return task2;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00009C18 File Offset: 0x00007E18
		private void NotifyTaskCompletion(IList<AsyncContinuation> reusableContinuations, Exception ex)
		{
			try
			{
				int num = 0;
				for (;;)
				{
					int num2 = num;
					int? num3 = ((reusableContinuations != null) ? new int?(reusableContinuations.Count) : null);
					if (!((num2 < num3.GetValueOrDefault()) & (num3 != null)))
					{
						break;
					}
					reusableContinuations[num](ex);
					num++;
				}
			}
			catch
			{
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00009C80 File Offset: 0x00007E80
		private void TaskCompletion(Task completedTask, object continuation)
		{
			bool flag = true;
			bool flag2 = true;
			try
			{
				if (completedTask == this._previousTask)
				{
					if (this.TaskTimeoutSeconds > 0)
					{
						this._taskTimeoutTimer.Change(-1, -1);
					}
				}
				else
				{
					flag = false;
					if (!base.IsInitialized)
					{
						return;
					}
				}
				Tuple<List<LogEventInfo>, List<AsyncContinuation>> tuple = continuation as Tuple<List<LogEventInfo>, List<AsyncContinuation>>;
				if (tuple != null)
				{
					this.NotifyTaskCompletion(tuple.Item2, null);
				}
				else
				{
					flag = false;
				}
				if (completedTask.IsCanceled)
				{
					flag = false;
					if (completedTask.Exception != null)
					{
						InternalLogger.Warn(completedTask.Exception, "{0} WriteAsyncTask was cancelled", new object[] { base.Name });
					}
					else
					{
						InternalLogger.Info<string>("{0} WriteAsyncTask was cancelled", base.Name);
					}
				}
				else if (completedTask.Exception != null)
				{
					Exception ex = AsyncTaskTarget.ExtractActualException(completedTask.Exception);
					flag = false;
					if (this.RetryCount <= 0)
					{
						TimeSpan timeSpan;
						if (this.RetryFailedAsyncTask(ex, CancellationToken.None, 0, out timeSpan))
						{
							InternalLogger.Warn(ex, "{0}: WriteAsyncTask failed on completion. Sleep {1} ms", new object[] { base.Name, timeSpan.TotalMilliseconds });
							AsyncHelpers.WaitForDelay(timeSpan);
						}
					}
					else
					{
						InternalLogger.Warn(ex, "{0} WriteAsyncTask failed on completion", new object[] { base.Name });
					}
				}
				if (flag && base.OptimizeBufferReuse)
				{
					flag2 = tuple.Item2.Count * 2 > this.BatchSize;
					tuple.Item1.Clear();
					tuple.Item2.Clear();
					Interlocked.CompareExchange<Tuple<List<LogEventInfo>, List<AsyncContinuation>>>(ref this._reusableLogEvents, tuple, null);
				}
			}
			finally
			{
				this.TaskStartNext(completedTask, flag2);
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00009E10 File Offset: 0x00008010
		private void TaskTimeout(object state)
		{
			try
			{
				if (base.IsInitialized)
				{
					InternalLogger.Warn<string>("{0} WriteAsyncTask had timeout. Task will be cancelled.", base.Name);
					Task task = this._previousTask;
					try
					{
						object syncRoot = base.SyncRoot;
						lock (syncRoot)
						{
							if (task != null && task == this._previousTask)
							{
								this._previousTask = null;
								this._cancelTokenSource.Cancel();
							}
							else
							{
								task = null;
							}
						}
						if (task != null)
						{
							if (task.Status != TaskStatus.Canceled && task.Status != TaskStatus.Faulted && task.Status != TaskStatus.RanToCompletion && !task.Wait(100))
							{
								InternalLogger.Debug<string, TaskStatus>("{0} WriteAsyncTask had timeout. Task did not cancel properly: {1}.", base.Name, task.Status);
							}
							Exception ex = AsyncTaskTarget.ExtractActualException(task.Exception);
							TimeSpan timeSpan;
							this.RetryFailedAsyncTask(ex ?? new TimeoutException("WriteAsyncTask had timeout"), CancellationToken.None, 0, out timeSpan);
						}
					}
					catch (Exception ex2)
					{
						InternalLogger.Debug(ex2, "{0} WriteAsyncTask had timeout. Task failed to cancel properly.", new object[] { base.Name });
					}
					this.TaskStartNext(null, false);
				}
			}
			catch (Exception ex3)
			{
				InternalLogger.Error(ex3, "{0} WriteAsyncTask failed on timeout", new object[] { base.Name });
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00009F54 File Offset: 0x00008154
		private static Exception ExtractActualException(AggregateException taskException)
		{
			ReadOnlyCollection<Exception> readOnlyCollection;
			if (taskException == null)
			{
				readOnlyCollection = null;
			}
			else
			{
				AggregateException ex = taskException.Flatten();
				readOnlyCollection = ((ex != null) ? ex.InnerExceptions : null);
			}
			ReadOnlyCollection<Exception> readOnlyCollection2 = readOnlyCollection;
			if (readOnlyCollection2 == null || readOnlyCollection2.Count != 1)
			{
				return taskException;
			}
			return readOnlyCollection2[0];
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00009F90 File Offset: 0x00008190
		private void TaskCancelledToken()
		{
			this._cancelTokenSource = new CancellationTokenSource();
			this._cancelTokenSource.Token.Register(this._taskCancelledToken);
		}

		// Token: 0x04000057 RID: 87
		private readonly Timer _taskTimeoutTimer;

		// Token: 0x04000058 RID: 88
		private CancellationTokenSource _cancelTokenSource;

		// Token: 0x04000059 RID: 89
		private AsyncRequestQueueBase _requestQueue;

		// Token: 0x0400005A RID: 90
		private readonly Action _taskCancelledToken;

		// Token: 0x0400005B RID: 91
		private readonly Action<Task, object> _taskCompletion;

		// Token: 0x0400005C RID: 92
		private Task _previousTask;

		// Token: 0x0400005D RID: 93
		private readonly Timer _lazyWriterTimer;

		// Token: 0x0400005E RID: 94
		private readonly ReusableAsyncLogEventList _reusableAsyncLogEventList = new ReusableAsyncLogEventList(200);

		// Token: 0x0400005F RID: 95
		private Tuple<List<LogEventInfo>, List<AsyncContinuation>> _reusableLogEvents;

		// Token: 0x04000064 RID: 100
		private bool? _forceLockingQueue;
	}
}
