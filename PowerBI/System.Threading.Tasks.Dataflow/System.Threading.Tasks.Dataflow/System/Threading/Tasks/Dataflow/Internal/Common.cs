using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000037 RID: 55
	internal static class Common
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00008871 File Offset: 0x00006A71
		[Conditional("DEBUG")]
		internal static void ContractAssertMonitorStatus(object syncObj, bool held)
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008874 File Offset: 0x00006A74
		internal static bool TryKeepAliveUntil<TStateIn, TStateOut>(Common.KeepAlivePredicate<TStateIn, TStateOut> predicate, TStateIn stateIn, [MaybeNullWhen(false)] out TStateOut stateOut)
		{
			for (int i = 16; i > 0; i--)
			{
				if (!Thread.Yield() && predicate(stateIn, out stateOut))
				{
					return true;
				}
			}
			stateOut = default(TStateOut);
			return false;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000088AC File Offset: 0x00006AAC
		internal static T UnwrapWeakReference<T>(object state) where T : class
		{
			WeakReference<T> weakReference = state as WeakReference<T>;
			T t;
			if (!weakReference.TryGetTarget(out t))
			{
				return default(T);
			}
			return t;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000088D8 File Offset: 0x00006AD8
		internal static int GetBlockId(IDataflowBlock block)
		{
			Task potentiallyNotSupportedCompletionTask = Common.GetPotentiallyNotSupportedCompletionTask(block);
			if (potentiallyNotSupportedCompletionTask == null)
			{
				return 0;
			}
			return potentiallyNotSupportedCompletionTask.Id;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000088F8 File Offset: 0x00006AF8
		internal static string GetNameForDebugger(IDataflowBlock block, DataflowBlockOptions options = null)
		{
			if (block == null)
			{
				return string.Empty;
			}
			string name = block.GetType().Name;
			if (options == null)
			{
				return name;
			}
			int blockId = Common.GetBlockId(block);
			string text;
			try
			{
				text = string.Format(options.NameFormat, name, blockId);
			}
			catch (Exception ex)
			{
				text = ex.Message;
			}
			return text;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008958 File Offset: 0x00006B58
		internal static bool IsCooperativeCancellation(Exception exception)
		{
			return exception is OperationCanceledException;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008964 File Offset: 0x00006B64
		internal static void WireCancellationToComplete(CancellationToken cancellationToken, Task completionTask, Action<object> completeAction, object completeState)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				completeAction(completeState);
				return;
			}
			if (cancellationToken.CanBeCanceled)
			{
				CancellationTokenRegistration cancellationTokenRegistration = cancellationToken.Register(completeAction, completeState);
				completionTask.ContinueWith(delegate(Task completed, object state)
				{
					((CancellationTokenRegistration)state).Dispose();
				}, cancellationTokenRegistration, cancellationToken, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000089D0 File Offset: 0x00006BD0
		internal static Exception InitializeStackTrace(Exception exception)
		{
			try
			{
				throw exception;
			}
			catch
			{
			}
			return exception;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000089F8 File Offset: 0x00006BF8
		internal static void StoreDataflowMessageValueIntoExceptionData<T>(Exception exc, T messageValue, bool targetInnerExceptions = false)
		{
			string text = messageValue as string;
			if (text == null && messageValue != null)
			{
				try
				{
					text = messageValue.ToString();
				}
				catch
				{
				}
			}
			if (text == null)
			{
				return;
			}
			Common.StoreStringIntoExceptionData(exc, "DataflowMessageValue", text);
			if (targetInnerExceptions)
			{
				AggregateException ex = exc as AggregateException;
				if (ex != null)
				{
					using (IEnumerator<Exception> enumerator = ex.InnerExceptions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Exception ex2 = enumerator.Current;
							Common.StoreStringIntoExceptionData(ex2, "DataflowMessageValue", text);
						}
						return;
					}
				}
				if (exc.InnerException != null)
				{
					Common.StoreStringIntoExceptionData(exc.InnerException, "DataflowMessageValue", text);
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008AB8 File Offset: 0x00006CB8
		private static void StoreStringIntoExceptionData(Exception exception, string key, string value)
		{
			try
			{
				IDictionary data = exception.Data;
				if (data != null && !data.IsFixedSize && !data.IsReadOnly && data[key] == null)
				{
					data[key] = value;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008B08 File Offset: 0x00006D08
		internal static void ThrowAsync(Exception error)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(error);
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				((ExceptionDispatchInfo)state).Throw();
			}, exceptionDispatchInfo);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008B44 File Offset: 0x00006D44
		internal static void AddException([NotNull] ref List<Exception> list, Exception exception, bool unwrapInnerExceptions = false)
		{
			if (list == null)
			{
				list = new List<Exception>();
			}
			if (!unwrapInnerExceptions)
			{
				list.Add(exception);
				return;
			}
			AggregateException ex = exception as AggregateException;
			if (ex != null)
			{
				list.AddRange(ex.InnerExceptions);
				return;
			}
			list.Add(exception.InnerException);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008B90 File Offset: 0x00006D90
		private static Task<bool> CreateCachedBooleanTask(bool value)
		{
			AsyncTaskMethodBuilder<bool> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<bool>.Create();
			asyncTaskMethodBuilder.SetResult(value);
			return asyncTaskMethodBuilder.Task;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00008BB4 File Offset: 0x00006DB4
		private static TaskCompletionSource<T> CreateCachedTaskCompletionSource<T>()
		{
			TaskCompletionSource<T> taskCompletionSource = new TaskCompletionSource<T>();
			taskCompletionSource.SetResult(default(T));
			return taskCompletionSource;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008BD8 File Offset: 0x00006DD8
		internal static Task<TResult> CreateTaskFromException<TResult>(Exception exception)
		{
			AsyncTaskMethodBuilder<TResult> asyncTaskMethodBuilder = AsyncTaskMethodBuilder<TResult>.Create();
			asyncTaskMethodBuilder.SetException(exception);
			return asyncTaskMethodBuilder.Task;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00008BFC File Offset: 0x00006DFC
		internal static Task<TResult> CreateTaskFromCancellation<TResult>(CancellationToken cancellationToken)
		{
			return new Task<TResult>(Common.CachedGenericDelegates<TResult>.DefaultTResultFunc, cancellationToken);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00008C18 File Offset: 0x00006E18
		internal static Task GetPotentiallyNotSupportedCompletionTask(IDataflowBlock block)
		{
			try
			{
				return block.Completion;
			}
			catch (NotImplementedException)
			{
			}
			catch (NotSupportedException)
			{
			}
			return null;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00008C54 File Offset: 0x00006E54
		internal static IDisposable CreateUnlinker<TOutput>(object outgoingLock, TargetRegistry<TOutput> targetRegistry, ITargetBlock<TOutput> targetBlock)
		{
			return Disposables.Create<object, TargetRegistry<TOutput>, ITargetBlock<TOutput>>(Common.CachedGenericDelegates<TOutput>.CreateUnlinkerShimAction, outgoingLock, targetRegistry, targetBlock);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00008C64 File Offset: 0x00006E64
		internal static bool IsValidTimeout(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			return num >= -1L && num <= 2147483647L;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00008C8D File Offset: 0x00006E8D
		internal static TaskContinuationOptions GetContinuationOptions(TaskContinuationOptions toInclude = TaskContinuationOptions.None)
		{
			return toInclude | TaskContinuationOptions.DenyChildAttach;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00008C94 File Offset: 0x00006E94
		internal static TaskCreationOptions GetCreationOptionsForTask(bool isReplacementReplica = false)
		{
			TaskCreationOptions taskCreationOptions = TaskCreationOptions.DenyChildAttach;
			if (isReplacementReplica)
			{
				taskCreationOptions |= TaskCreationOptions.PreferFairness;
			}
			return taskCreationOptions;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00008CAB File Offset: 0x00006EAB
		internal static Exception StartTaskSafe(Task task, TaskScheduler scheduler)
		{
			if (scheduler == TaskScheduler.Default)
			{
				task.Start(scheduler);
				return null;
			}
			return Common.StartTaskSafeCore(task, scheduler);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00008CC8 File Offset: 0x00006EC8
		private static Exception StartTaskSafeCore(Task task, TaskScheduler scheduler)
		{
			Exception ex = null;
			try
			{
				task.Start(scheduler);
			}
			catch (Exception ex2)
			{
				AggregateException exception = task.Exception;
				ex = ex2;
			}
			return ex;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00008D00 File Offset: 0x00006F00
		internal static void ReleaseAllPostponedMessages<T>(ITargetBlock<T> target, QueuedMap<ISourceBlock<T>, DataflowMessageHeader> postponedMessages, ref List<Exception> exceptions)
		{
			int count = postponedMessages.Count;
			int num = 0;
			KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair;
			while (postponedMessages.TryPop(out keyValuePair))
			{
				try
				{
					if (keyValuePair.Key.ReserveMessage(keyValuePair.Value, target))
					{
						keyValuePair.Key.ReleaseReservation(keyValuePair.Value, target);
					}
				}
				catch (Exception ex)
				{
					Common.AddException(ref exceptions, ex, false);
				}
				num++;
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00008D70 File Offset: 0x00006F70
		internal static void PropagateCompletion(Task sourceCompletionTask, IDataflowBlock target, Action<Exception> exceptionHandler)
		{
			AggregateException ex = (sourceCompletionTask.IsFaulted ? sourceCompletionTask.Exception : null);
			try
			{
				if (ex != null)
				{
					target.Fault(ex);
				}
				else
				{
					target.Complete();
				}
			}
			catch (Exception ex2)
			{
				if (exceptionHandler == null)
				{
					throw;
				}
				exceptionHandler(ex2);
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00008DC4 File Offset: 0x00006FC4
		private static void PropagateCompletionAsContinuation(Task sourceCompletionTask, IDataflowBlock target)
		{
			sourceCompletionTask.ContinueWith(delegate(Task task, object state)
			{
				Common.PropagateCompletion(task, (IDataflowBlock)state, Common.AsyncExceptionHandler);
			}, target, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00008DFD File Offset: 0x00006FFD
		internal static void PropagateCompletionOnceCompleted(Task sourceCompletionTask, IDataflowBlock target)
		{
			if (sourceCompletionTask.IsCompleted)
			{
				Common.PropagateCompletion(sourceCompletionTask, target, null);
				return;
			}
			Common.PropagateCompletionAsContinuation(sourceCompletionTask, target);
		}

		// Token: 0x04000081 RID: 129
		internal const long INVALID_REORDERING_ID = -1L;

		// Token: 0x04000082 RID: 130
		internal const int SINGLE_MESSAGE_ID = 1;

		// Token: 0x04000083 RID: 131
		internal static readonly DataflowMessageHeader SingleMessageHeader = new DataflowMessageHeader(1L);

		// Token: 0x04000084 RID: 132
		internal static readonly Task<bool> CompletedTaskWithTrueResult = Common.CreateCachedBooleanTask(true);

		// Token: 0x04000085 RID: 133
		internal static readonly Task<bool> CompletedTaskWithFalseResult = Common.CreateCachedBooleanTask(false);

		// Token: 0x04000086 RID: 134
		internal static readonly TaskCompletionSource<VoidResult> CompletedVoidResultTaskCompletionSource = Common.CreateCachedTaskCompletionSource<VoidResult>();

		// Token: 0x04000087 RID: 135
		internal const int KEEP_ALIVE_NUMBER_OF_MESSAGES_THRESHOLD = 1;

		// Token: 0x04000088 RID: 136
		internal const int KEEP_ALIVE_BAN_COUNT = 1000;

		// Token: 0x04000089 RID: 137
		internal const string EXCEPTIONDATAKEY_DATAFLOWMESSAGEVALUE = "DataflowMessageValue";

		// Token: 0x0400008A RID: 138
		internal static readonly TimeSpan InfiniteTimeSpan = Timeout.InfiniteTimeSpan;

		// Token: 0x0400008B RID: 139
		internal static readonly Action<Exception> AsyncExceptionHandler = new Action<Exception>(Common.ThrowAsync);

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x06000428 RID: 1064
		internal delegate bool KeepAlivePredicate<TStateIn, TStateOut>(TStateIn stateIn, out TStateOut stateOut);

		// Token: 0x02000081 RID: 129
		private static class CachedGenericDelegates<T>
		{
			// Token: 0x040001A7 RID: 423
			internal static readonly Func<T> DefaultTResultFunc = () => default(T);

			// Token: 0x040001A8 RID: 424
			internal static readonly Action<object, TargetRegistry<T>, ITargetBlock<T>> CreateUnlinkerShimAction = delegate(object syncObj, TargetRegistry<T> registry, ITargetBlock<T> target)
			{
				lock (syncObj)
				{
					registry.Remove(target, false);
				}
			};
		}
	}
}
