using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200001A RID: 26
	[NullableContext(1)]
	[Nullable(0)]
	public static class DataflowBlock
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public static IDisposable LinkTo<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, ITargetBlock<TOutput> target)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return source.LinkTo(target, DataflowLinkOptions.Default);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C22 File Offset: 0x00000E22
		public static IDisposable LinkTo<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, ITargetBlock<TOutput> target, Predicate<TOutput> predicate)
		{
			return source.LinkTo(target, DataflowLinkOptions.Default, predicate);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C34 File Offset: 0x00000E34
		public static IDisposable LinkTo<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions, Predicate<TOutput> predicate)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (linkOptions == null)
			{
				throw new ArgumentNullException("linkOptions");
			}
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			DataflowBlock.FilteredLinkPropagator<TOutput> filteredLinkPropagator = new DataflowBlock.FilteredLinkPropagator<TOutput>(source, target, predicate);
			return source.LinkTo(filteredLinkPropagator, linkOptions);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C8A File Offset: 0x00000E8A
		public static bool Post<[Nullable(2)] TInput>(this ITargetBlock<TInput> target, TInput item)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.OfferMessage(Common.SingleMessageHeader, item, null, false) == DataflowMessageStatus.Accepted;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002CAB File Offset: 0x00000EAB
		public static Task<bool> SendAsync<[Nullable(2)] TInput>(this ITargetBlock<TInput> target, TInput item)
		{
			return target.SendAsync(item, CancellationToken.None);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002CBC File Offset: 0x00000EBC
		public static Task<bool> SendAsync<[Nullable(2)] TInput>(this ITargetBlock<TInput> target, TInput item, CancellationToken cancellationToken)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Common.CreateTaskFromCancellation<bool>(cancellationToken);
			}
			DataflowBlock.SendAsyncSource<TInput> sendAsyncSource;
			try
			{
				DataflowMessageStatus dataflowMessageStatus = target.OfferMessage(Common.SingleMessageHeader, item, null, false);
				if (dataflowMessageStatus == DataflowMessageStatus.Accepted)
				{
					return Common.CompletedTaskWithTrueResult;
				}
				if (dataflowMessageStatus == DataflowMessageStatus.DecliningPermanently)
				{
					return Common.CompletedTaskWithFalseResult;
				}
				sendAsyncSource = new DataflowBlock.SendAsyncSource<TInput>(target, item, cancellationToken);
			}
			catch (Exception ex)
			{
				Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, item, false);
				return Common.CreateTaskFromException<bool>(ex);
			}
			sendAsyncSource.OfferToTarget();
			return sendAsyncSource.Task;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D4C File Offset: 0x00000F4C
		public static bool TryReceive<[Nullable(2)] TOutput>(this IReceivableSourceBlock<TOutput> source, [MaybeNullWhen(false)] out TOutput item)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.TryReceive(null, out item);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002D64 File Offset: 0x00000F64
		public static Task<TOutput> ReceiveAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source)
		{
			return source.ReceiveAsync(Common.InfiniteTimeSpan, CancellationToken.None);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002D76 File Offset: 0x00000F76
		public static Task<TOutput> ReceiveAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, CancellationToken cancellationToken)
		{
			return source.ReceiveAsync(Common.InfiniteTimeSpan, cancellationToken);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002D84 File Offset: 0x00000F84
		public static Task<TOutput> ReceiveAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, TimeSpan timeout)
		{
			return source.ReceiveAsync(timeout, CancellationToken.None);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D92 File Offset: 0x00000F92
		public static Task<TOutput> ReceiveAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, TimeSpan timeout, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!Common.IsValidTimeout(timeout))
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return source.ReceiveCore(true, timeout, cancellationToken);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public static TOutput Receive<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source)
		{
			return source.Receive(Common.InfiniteTimeSpan, CancellationToken.None);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DD5 File Offset: 0x00000FD5
		public static TOutput Receive<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, CancellationToken cancellationToken)
		{
			return source.Receive(Common.InfiniteTimeSpan, cancellationToken);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002DE3 File Offset: 0x00000FE3
		public static TOutput Receive<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, TimeSpan timeout)
		{
			return source.Receive(timeout, CancellationToken.None);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public static TOutput Receive<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, TimeSpan timeout, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (!Common.IsValidTimeout(timeout))
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			cancellationToken.ThrowIfCancellationRequested();
			IReceivableSourceBlock<TOutput> receivableSourceBlock = source as IReceivableSourceBlock<TOutput>;
			TOutput toutput;
			if (receivableSourceBlock != null && receivableSourceBlock.TryReceive(null, out toutput))
			{
				return toutput;
			}
			Task<TOutput> task = source.ReceiveCore(false, timeout, cancellationToken);
			TOutput result;
			try
			{
				result = task.GetAwaiter().GetResult();
			}
			catch
			{
				if (task.IsCanceled)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002E88 File Offset: 0x00001088
		private static Task<TOutput> ReceiveCore<TOutput>(this ISourceBlock<TOutput> source, bool attemptTryReceive, TimeSpan timeout, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return Common.CreateTaskFromCancellation<TOutput>(cancellationToken);
			}
			if (attemptTryReceive)
			{
				IReceivableSourceBlock<TOutput> receivableSourceBlock = source as IReceivableSourceBlock<TOutput>;
				if (receivableSourceBlock != null)
				{
					try
					{
						TOutput toutput;
						if (receivableSourceBlock.TryReceive(null, out toutput))
						{
							return Task.FromResult<TOutput>(toutput);
						}
					}
					catch (Exception ex)
					{
						return Common.CreateTaskFromException<TOutput>(ex);
					}
				}
			}
			int num = (int)timeout.TotalMilliseconds;
			if (num == 0)
			{
				return Common.CreateTaskFromException<TOutput>(DataflowBlock.ReceiveTarget<TOutput>.CreateExceptionForTimeout());
			}
			return DataflowBlock.ReceiveCoreByLinking<TOutput>(source, num, cancellationToken);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002F08 File Offset: 0x00001108
		private static Task<TOutput> ReceiveCoreByLinking<TOutput>(ISourceBlock<TOutput> source, int millisecondsTimeout, CancellationToken cancellationToken)
		{
			DataflowBlock.ReceiveTarget<TOutput> receiveTarget = new DataflowBlock.ReceiveTarget<TOutput>();
			try
			{
				if (cancellationToken.CanBeCanceled)
				{
					receiveTarget._externalCancellationToken = cancellationToken;
					receiveTarget._regFromExternalCancellationToken = cancellationToken.Register(DataflowBlock._cancelCts, receiveTarget._cts);
				}
				if (millisecondsTimeout > 0)
				{
					receiveTarget._timer = new Timer(DataflowBlock.ReceiveTarget<TOutput>.CachedLinkingTimerCallback, receiveTarget, millisecondsTimeout, -1);
				}
				if (receiveTarget._cts.Token.CanBeCanceled)
				{
					receiveTarget._cts.Token.Register(DataflowBlock.ReceiveTarget<TOutput>.CachedLinkingCancellationCallback, receiveTarget);
				}
				IDisposable disposable = source.LinkTo(receiveTarget, DataflowLinkOptions.UnlinkAfterOneAndPropagateCompletion);
				receiveTarget._unlink = disposable;
				if (Volatile.Read(ref receiveTarget._cleanupReserved))
				{
					IDisposable disposable2 = Interlocked.CompareExchange<IDisposable>(ref receiveTarget._unlink, null, disposable);
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				receiveTarget._receivedException = ex;
				receiveTarget.TryCleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceProtocolError);
			}
			return receiveTarget.Task;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static Task<bool> OutputAvailableAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source)
		{
			return source.OutputAvailableAsync(CancellationToken.None);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003000 File Offset: 0x00001200
		public static Task<bool> OutputAvailableAsync<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source, CancellationToken cancellationToken)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return Common.CreateTaskFromCancellation<bool>(cancellationToken);
			}
			DataflowBlock.OutputAvailableAsyncTarget<TOutput> outputAvailableAsyncTarget = new DataflowBlock.OutputAvailableAsyncTarget<TOutput>();
			Task<bool> task;
			try
			{
				outputAvailableAsyncTarget._unlinker = source.LinkTo(outputAvailableAsyncTarget, DataflowLinkOptions.UnlinkAfterOneAndPropagateCompletion);
				if (outputAvailableAsyncTarget.Task.IsCompleted)
				{
					task = outputAvailableAsyncTarget.Task;
				}
				else
				{
					if (cancellationToken.CanBeCanceled)
					{
						outputAvailableAsyncTarget._ctr = cancellationToken.Register(DataflowBlock.OutputAvailableAsyncTarget<TOutput>.s_cancelAndUnlink, outputAvailableAsyncTarget);
					}
					task = outputAvailableAsyncTarget.Task.ContinueWith<bool>(DataflowBlock.OutputAvailableAsyncTarget<TOutput>.s_handleCompletion, outputAvailableAsyncTarget, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.NotOnCanceled, TaskScheduler.Default);
				}
			}
			catch (Exception ex)
			{
				outputAvailableAsyncTarget.TrySetException(ex);
				outputAvailableAsyncTarget.AttemptThreadSafeUnlink();
				task = outputAvailableAsyncTarget.Task;
			}
			return task;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000030C8 File Offset: 0x000012C8
		public static IPropagatorBlock<TInput, TOutput> Encapsulate<[Nullable(2)] TInput, [Nullable(2)] TOutput>(ITargetBlock<TInput> target, ISourceBlock<TOutput> source)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new DataflowBlock.EncapsulatingPropagator<TInput, TOutput>(target, source);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000030ED File Offset: 0x000012ED
		public static Task<int> Choose<[Nullable(2)] T1, [Nullable(2)] T2>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2)
		{
			return DataflowBlock.Choose<T1, T2>(source1, action1, source2, action2, DataflowBlockOptions.Default);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003100 File Offset: 0x00001300
		public static Task<int> Choose<[Nullable(2)] T1, [Nullable(2)] T2>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2, DataflowBlockOptions dataflowBlockOptions)
		{
			if (source1 == null)
			{
				throw new ArgumentNullException("source1");
			}
			if (action1 == null)
			{
				throw new ArgumentNullException("action1");
			}
			if (source2 == null)
			{
				throw new ArgumentNullException("source2");
			}
			if (action2 == null)
			{
				throw new ArgumentNullException("action2");
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			return DataflowBlock.ChooseCore<T1, T2, VoidResult>(source1, action1, source2, action2, null, null, dataflowBlockOptions);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003161 File Offset: 0x00001361
		public static Task<int> Choose<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2, ISourceBlock<T3> source3, Action<T3> action3)
		{
			return DataflowBlock.Choose<T1, T2, T3>(source1, action1, source2, action2, source3, action3, DataflowBlockOptions.Default);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003178 File Offset: 0x00001378
		public static Task<int> Choose<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2, ISourceBlock<T3> source3, Action<T3> action3, DataflowBlockOptions dataflowBlockOptions)
		{
			if (source1 == null)
			{
				throw new ArgumentNullException("source1");
			}
			if (action1 == null)
			{
				throw new ArgumentNullException("action1");
			}
			if (source2 == null)
			{
				throw new ArgumentNullException("source2");
			}
			if (action2 == null)
			{
				throw new ArgumentNullException("action2");
			}
			if (source3 == null)
			{
				throw new ArgumentNullException("source3");
			}
			if (action3 == null)
			{
				throw new ArgumentNullException("action3");
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			return DataflowBlock.ChooseCore<T1, T2, T3>(source1, action1, source2, action2, source3, action3, dataflowBlockOptions);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000031FC File Offset: 0x000013FC
		private static Task<int> ChooseCore<T1, T2, T3>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2, ISourceBlock<T3> source3, Action<T3> action3, DataflowBlockOptions dataflowBlockOptions)
		{
			bool flag = source3 != null;
			if (dataflowBlockOptions.CancellationToken.IsCancellationRequested)
			{
				return Common.CreateTaskFromCancellation<int>(dataflowBlockOptions.CancellationToken);
			}
			try
			{
				TaskScheduler taskScheduler = dataflowBlockOptions.TaskScheduler;
				Task<int> task;
				if (DataflowBlock.TryChooseFromSource<T1>(source1, action1, 0, taskScheduler, out task) || DataflowBlock.TryChooseFromSource<T2>(source2, action2, 1, taskScheduler, out task) || (flag && DataflowBlock.TryChooseFromSource<T3>(source3, action3, 2, taskScheduler, out task)))
				{
					return task;
				}
			}
			catch (Exception ex)
			{
				return Common.CreateTaskFromException<int>(ex);
			}
			return DataflowBlock.ChooseCoreByLinking<T1, T2, T3>(source1, action1, source2, action2, source3, action3, dataflowBlockOptions);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003298 File Offset: 0x00001498
		private static bool TryChooseFromSource<T>(ISourceBlock<T> source, Action<T> action, int branchId, TaskScheduler scheduler, [NotNullWhen(true)] out Task<int> task)
		{
			IReceivableSourceBlock<T> receivableSourceBlock = source as IReceivableSourceBlock<T>;
			T t;
			if (receivableSourceBlock == null || !receivableSourceBlock.TryReceive(out t))
			{
				task = null;
				return false;
			}
			task = Task.Factory.StartNew<int>(DataflowBlock.ChooseTarget<T>.s_processBranchFunction, Tuple.Create<Action<T>, T, int>(action, t, branchId), CancellationToken.None, Common.GetCreationOptionsForTask(false), scheduler);
			return true;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000032E8 File Offset: 0x000014E8
		private static Task<int> ChooseCoreByLinking<T1, T2, T3>(ISourceBlock<T1> source1, Action<T1> action1, ISourceBlock<T2> source2, Action<T2> action2, ISourceBlock<T3> source3, Action<T3> action3, DataflowBlockOptions dataflowBlockOptions)
		{
			bool flag = source3 != null;
			StrongBox<Task> strongBox = new StrongBox<Task>();
			CancellationTokenSource cts = CancellationTokenSource.CreateLinkedTokenSource(dataflowBlockOptions.CancellationToken, CancellationToken.None);
			TaskScheduler taskScheduler = dataflowBlockOptions.TaskScheduler;
			Task<int>[] array = new Task<int>[flag ? 3 : 2];
			array[0] = DataflowBlock.CreateChooseBranch<T1>(strongBox, cts, taskScheduler, 0, source1, action1);
			array[1] = DataflowBlock.CreateChooseBranch<T2>(strongBox, cts, taskScheduler, 1, source2, action2);
			if (flag)
			{
				array[2] = DataflowBlock.CreateChooseBranch<T3>(strongBox, cts, taskScheduler, 2, source3, action3);
			}
			TaskCompletionSource<int> result = new TaskCompletionSource<int>();
			Task.Factory.ContinueWhenAll<int>(array, delegate(Task<int>[] tasks)
			{
				List<Exception> list = null;
				int num = -1;
				foreach (Task<int> task in tasks)
				{
					TaskStatus status = task.Status;
					if (status != TaskStatus.RanToCompletion)
					{
						if (status == TaskStatus.Faulted)
						{
							Common.AddException(ref list, task.Exception, true);
						}
					}
					else
					{
						int result2 = task.Result;
						if (result2 >= 0)
						{
							num = result2;
						}
					}
				}
				if (list != null)
				{
					result.TrySetException(list);
				}
				else if (num >= 0)
				{
					result.TrySetResult(num);
				}
				else
				{
					result.TrySetCanceled();
				}
				cts.Dispose();
			}, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			return result.Task;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000033B4 File Offset: 0x000015B4
		private static Task<int> CreateChooseBranch<T>(StrongBox<Task> boxedCompleted, CancellationTokenSource cts, TaskScheduler scheduler, int branchId, ISourceBlock<T> source, Action<T> action)
		{
			if (cts.IsCancellationRequested)
			{
				return Common.CreateTaskFromCancellation<int>(cts.Token);
			}
			DataflowBlock.ChooseTarget<T> chooseTarget = new DataflowBlock.ChooseTarget<T>(boxedCompleted, cts.Token);
			IDisposable unlink;
			try
			{
				unlink = source.LinkTo(chooseTarget, DataflowLinkOptions.UnlinkAfterOneAndPropagateCompletion);
			}
			catch (Exception ex)
			{
				cts.Cancel();
				return Common.CreateTaskFromException<int>(ex);
			}
			return chooseTarget.Task.ContinueWith<int>(delegate(Task<T> completed)
			{
				int num;
				try
				{
					if (completed.Status == TaskStatus.RanToCompletion)
					{
						cts.Cancel();
						action(completed.Result);
						num = branchId;
					}
					else
					{
						num = -1;
					}
				}
				finally
				{
					unlink.Dispose();
				}
				return num;
			}, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), scheduler);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000346C File Offset: 0x0000166C
		public static IObservable<TOutput> AsObservable<[Nullable(2)] TOutput>(this ISourceBlock<TOutput> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return DataflowBlock.SourceObservable<TOutput>.From(source);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003482 File Offset: 0x00001682
		public static IObserver<TInput> AsObserver<[Nullable(2)] TInput>(this ITargetBlock<TInput> target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return new DataflowBlock.TargetObserver<TInput>(target);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003498 File Offset: 0x00001698
		public static ITargetBlock<TInput> NullTarget<[Nullable(2)] TInput>()
		{
			return new DataflowBlock.NullTargetBlock<TInput>();
		}

		// Token: 0x04000018 RID: 24
		private static readonly Action<object> _cancelCts = delegate(object state)
		{
			((CancellationTokenSource)state).Cancel();
		};

		// Token: 0x04000019 RID: 25
		private static readonly ExecutionDataflowBlockOptions _nonGreedyExecutionOptions = new ExecutionDataflowBlockOptions
		{
			BoundedCapacity = 1
		};

		// Token: 0x0200004D RID: 77
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		[DebuggerTypeProxy(typeof(DataflowBlock.FilteredLinkPropagator<>.DebugView))]
		private sealed class FilteredLinkPropagator<T> : IPropagatorBlock<T, T>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T>, IDebuggerDisplay
		{
			// Token: 0x0600029E RID: 670 RVA: 0x0000B958 File Offset: 0x00009B58
			internal FilteredLinkPropagator(ISourceBlock<T> source, ITargetBlock<T> target, Predicate<T> predicate)
			{
				this._source = source;
				this._target = target;
				this._userProvidedPredicate = predicate;
			}

			// Token: 0x0600029F RID: 671 RVA: 0x0000B975 File Offset: 0x00009B75
			private bool RunPredicate(T item)
			{
				return this._userProvidedPredicate(item);
			}

			// Token: 0x060002A0 RID: 672 RVA: 0x0000B984 File Offset: 0x00009B84
			DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				bool flag = this.RunPredicate(messageValue);
				if (flag)
				{
					return this._target.OfferMessage(messageHeader, messageValue, this, consumeToAccept);
				}
				return DataflowMessageStatus.Declined;
			}

			// Token: 0x060002A1 RID: 673 RVA: 0x0000B9D5 File Offset: 0x00009BD5
			T ISourceBlock<T>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
			{
				return this._source.ConsumeMessage(messageHeader, this, out messageConsumed);
			}

			// Token: 0x060002A2 RID: 674 RVA: 0x0000B9E5 File Offset: 0x00009BE5
			bool ISourceBlock<T>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
			{
				return this._source.ReserveMessage(messageHeader, this);
			}

			// Token: 0x060002A3 RID: 675 RVA: 0x0000B9F4 File Offset: 0x00009BF4
			void ISourceBlock<T>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
			{
				this._source.ReleaseReservation(messageHeader, this);
			}

			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000BA03 File Offset: 0x00009C03
			Task IDataflowBlock.Completion
			{
				get
				{
					return this._source.Completion;
				}
			}

			// Token: 0x060002A5 RID: 677 RVA: 0x0000BA10 File Offset: 0x00009C10
			void IDataflowBlock.Complete()
			{
				this._target.Complete();
			}

			// Token: 0x060002A6 RID: 678 RVA: 0x0000BA1D File Offset: 0x00009C1D
			void IDataflowBlock.Fault(Exception exception)
			{
				this._target.Fault(exception);
			}

			// Token: 0x060002A7 RID: 679 RVA: 0x0000BA2B File Offset: 0x00009C2B
			IDisposable ISourceBlock<T>.LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}

			// Token: 0x170000C5 RID: 197
			// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000BA38 File Offset: 0x00009C38
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._source as IDebuggerDisplay;
					IDebuggerDisplay debuggerDisplay2 = this._target as IDebuggerDisplay;
					return string.Format("{0} Source=\"{1}\", Target=\"{2}\"", Common.GetNameForDebugger(this, null), (debuggerDisplay != null) ? debuggerDisplay.Content : this._source, (debuggerDisplay2 != null) ? debuggerDisplay2.Content : this._target);
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000BA90 File Offset: 0x00009C90
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x040000E3 RID: 227
			private readonly ISourceBlock<T> _source;

			// Token: 0x040000E4 RID: 228
			private readonly ITargetBlock<T> _target;

			// Token: 0x040000E5 RID: 229
			private readonly Predicate<T> _userProvidedPredicate;

			// Token: 0x02000091 RID: 145
			private sealed class DebugView
			{
				// Token: 0x06000471 RID: 1137 RVA: 0x000102AC File Offset: 0x0000E4AC
				public DebugView(DataflowBlock.FilteredLinkPropagator<T> filter)
				{
					this._filter = filter;
				}

				// Token: 0x17000180 RID: 384
				// (get) Token: 0x06000472 RID: 1138 RVA: 0x000102BB File Offset: 0x0000E4BB
				public ITargetBlock<T> LinkedTarget
				{
					get
					{
						return this._filter._target;
					}
				}

				// Token: 0x040001D3 RID: 467
				private readonly DataflowBlock.FilteredLinkPropagator<T> _filter;
			}
		}

		// Token: 0x0200004E RID: 78
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		[DebuggerTypeProxy(typeof(DataflowBlock.SendAsyncSource<>.DebugView))]
		private sealed class SendAsyncSource<TOutput> : TaskCompletionSource<bool>, ISourceBlock<TOutput>, IDataflowBlock, IDebuggerDisplay
		{
			// Token: 0x060002AA RID: 682 RVA: 0x0000BA98 File Offset: 0x00009C98
			internal SendAsyncSource(ITargetBlock<TOutput> target, TOutput messageValue, CancellationToken cancellationToken)
			{
				this._target = target;
				this._messageValue = messageValue;
				if (cancellationToken.CanBeCanceled)
				{
					this._cancellationToken = cancellationToken;
					this._cancellationState = 1;
					try
					{
						this._cancellationRegistration = cancellationToken.Register(DataflowBlock.SendAsyncSource<TOutput>._cancellationCallback, new WeakReference<DataflowBlock.SendAsyncSource<TOutput>>(this));
					}
					catch
					{
						GC.SuppressFinalize(this);
						throw;
					}
				}
			}

			// Token: 0x060002AB RID: 683 RVA: 0x0000BB04 File Offset: 0x00009D04
			~SendAsyncSource()
			{
				if (!Environment.HasShutdownStarted)
				{
					this.CompleteAsDeclined(true);
				}
			}

			// Token: 0x060002AC RID: 684 RVA: 0x0000BB38 File Offset: 0x00009D38
			private void CompleteAsAccepted(bool runAsync)
			{
				this.RunCompletionAction(delegate(object state)
				{
					try
					{
						((DataflowBlock.SendAsyncSource<TOutput>)state).TrySetResult(true);
					}
					catch (ObjectDisposedException)
					{
					}
				}, this, runAsync);
			}

			// Token: 0x060002AD RID: 685 RVA: 0x0000BB61 File Offset: 0x00009D61
			private void CompleteAsDeclined(bool runAsync)
			{
				this.RunCompletionAction(delegate(object state)
				{
					try
					{
						((DataflowBlock.SendAsyncSource<TOutput>)state).TrySetResult(false);
					}
					catch (ObjectDisposedException)
					{
					}
				}, this, runAsync);
			}

			// Token: 0x060002AE RID: 686 RVA: 0x0000BB8A File Offset: 0x00009D8A
			private void CompleteAsFaulted(Exception exception, bool runAsync)
			{
				this.RunCompletionAction(delegate(object state)
				{
					Tuple<DataflowBlock.SendAsyncSource<TOutput>, Exception> tuple = (Tuple<DataflowBlock.SendAsyncSource<TOutput>, Exception>)state;
					try
					{
						tuple.Item1.TrySetException(tuple.Item2);
					}
					catch (ObjectDisposedException)
					{
					}
				}, Tuple.Create<DataflowBlock.SendAsyncSource<TOutput>, Exception>(this, exception), runAsync);
			}

			// Token: 0x060002AF RID: 687 RVA: 0x0000BBB9 File Offset: 0x00009DB9
			private void CompleteAsCanceled(bool runAsync)
			{
				this.RunCompletionAction(delegate(object state)
				{
					try
					{
						((DataflowBlock.SendAsyncSource<TOutput>)state).TrySetCanceled();
					}
					catch (ObjectDisposedException)
					{
					}
				}, this, runAsync);
			}

			// Token: 0x060002B0 RID: 688 RVA: 0x0000BBE4 File Offset: 0x00009DE4
			private void RunCompletionAction(Action<object> completionAction, object completionActionState, bool runAsync)
			{
				GC.SuppressFinalize(this);
				if (this._cancellationState != 0)
				{
					this._cancellationRegistration.Dispose();
				}
				if (runAsync)
				{
					global::System.Threading.Tasks.Task.Factory.StartNew(completionAction, completionActionState, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
					return;
				}
				completionAction(completionActionState);
			}

			// Token: 0x060002B1 RID: 689 RVA: 0x0000BC32 File Offset: 0x00009E32
			private void OfferToTargetAsync()
			{
				global::System.Threading.Tasks.Task.Factory.StartNew(delegate(object state)
				{
					((DataflowBlock.SendAsyncSource<TOutput>)state).OfferToTarget();
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}

			// Token: 0x060002B2 RID: 690 RVA: 0x0000BC70 File Offset: 0x00009E70
			private static void CancellationHandler(object state)
			{
				DataflowBlock.SendAsyncSource<TOutput> sendAsyncSource = Common.UnwrapWeakReference<DataflowBlock.SendAsyncSource<TOutput>>(state);
				if (sendAsyncSource != null && sendAsyncSource._cancellationState == 1 && Interlocked.CompareExchange(ref sendAsyncSource._cancellationState, 3, 1) == 1)
				{
					sendAsyncSource.CompleteAsCanceled(true);
				}
			}

			// Token: 0x060002B3 RID: 691 RVA: 0x0000BCA8 File Offset: 0x00009EA8
			internal void OfferToTarget()
			{
				try
				{
					bool flag = this._cancellationState != 0;
					switch (this._target.OfferMessage(Common.SingleMessageHeader, this._messageValue, this, flag))
					{
					case DataflowMessageStatus.Accepted:
						if (!flag)
						{
							this.CompleteAsAccepted(false);
						}
						break;
					case DataflowMessageStatus.Declined:
					case DataflowMessageStatus.DecliningPermanently:
						this.CompleteAsDeclined(false);
						break;
					}
				}
				catch (Exception ex)
				{
					Common.StoreDataflowMessageValueIntoExceptionData<TOutput>(ex, this._messageValue, false);
					this.CompleteAsFaulted(ex, false);
				}
			}

			// Token: 0x060002B4 RID: 692 RVA: 0x0000BD34 File Offset: 0x00009F34
			TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (target == null)
				{
					throw new ArgumentNullException("target");
				}
				if (base.Task.IsCompleted)
				{
					messageConsumed = false;
					return default(TOutput);
				}
				bool flag = messageHeader.Id == 1L;
				if (flag)
				{
					int cancellationState = this._cancellationState;
					if (cancellationState == 0 || (cancellationState != 3 && Interlocked.CompareExchange(ref this._cancellationState, 3, cancellationState) == cancellationState))
					{
						this.CompleteAsAccepted(true);
						messageConsumed = true;
						return this._messageValue;
					}
				}
				messageConsumed = false;
				return default(TOutput);
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x0000BDCC File Offset: 0x00009FCC
			bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (target == null)
				{
					throw new ArgumentNullException("target");
				}
				if (base.Task.IsCompleted)
				{
					return false;
				}
				bool flag = messageHeader.Id == 1L;
				return flag && (this._cancellationState == 0 || Interlocked.CompareExchange(ref this._cancellationState, 2, 1) == 1);
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x0000BE3C File Offset: 0x0000A03C
			void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (target == null)
				{
					throw new ArgumentNullException("target");
				}
				if (messageHeader.Id != 1L)
				{
					throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
				}
				if (base.Task.IsCompleted)
				{
					return;
				}
				if (this._cancellationState != 0)
				{
					if (Interlocked.CompareExchange(ref this._cancellationState, 1, 2) != 2)
					{
						throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
					}
					if (this._cancellationToken.IsCancellationRequested)
					{
						DataflowBlock.SendAsyncSource<TOutput>.CancellationHandler(new WeakReference<DataflowBlock.SendAsyncSource<TOutput>>(this));
					}
				}
				this.OfferToTargetAsync();
			}

			// Token: 0x170000C7 RID: 199
			// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000BED5 File Offset: 0x0000A0D5
			Task IDataflowBlock.Completion
			{
				get
				{
					return base.Task;
				}
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x0000BEDD File Offset: 0x0000A0DD
			IDisposable ISourceBlock<TOutput>.LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x0000BEE9 File Offset: 0x0000A0E9
			void IDataflowBlock.Complete()
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}

			// Token: 0x060002BA RID: 698 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
			void IDataflowBlock.Fault(Exception exception)
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}

			// Token: 0x170000C8 RID: 200
			// (get) Token: 0x060002BB RID: 699 RVA: 0x0000BF04 File Offset: 0x0000A104
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._target as IDebuggerDisplay;
					return string.Format("{0} Message={1}, Target=\"{2}\"", Common.GetNameForDebugger(this, null), this._messageValue, (debuggerDisplay != null) ? debuggerDisplay.Content : this._target);
				}
			}

			// Token: 0x170000C9 RID: 201
			// (get) Token: 0x060002BC RID: 700 RVA: 0x0000BF4A File Offset: 0x0000A14A
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x040000E6 RID: 230
			private readonly ITargetBlock<TOutput> _target;

			// Token: 0x040000E7 RID: 231
			private readonly TOutput _messageValue;

			// Token: 0x040000E8 RID: 232
			private CancellationToken _cancellationToken;

			// Token: 0x040000E9 RID: 233
			private CancellationTokenRegistration _cancellationRegistration;

			// Token: 0x040000EA RID: 234
			private int _cancellationState;

			// Token: 0x040000EB RID: 235
			private const int CANCELLATION_STATE_NONE = 0;

			// Token: 0x040000EC RID: 236
			private const int CANCELLATION_STATE_REGISTERED = 1;

			// Token: 0x040000ED RID: 237
			private const int CANCELLATION_STATE_RESERVED = 2;

			// Token: 0x040000EE RID: 238
			private const int CANCELLATION_STATE_COMPLETING = 3;

			// Token: 0x040000EF RID: 239
			private static readonly Action<object> _cancellationCallback = new Action<object>(DataflowBlock.SendAsyncSource<TOutput>.CancellationHandler);

			// Token: 0x02000092 RID: 146
			private sealed class DebugView
			{
				// Token: 0x06000473 RID: 1139 RVA: 0x000102C8 File Offset: 0x0000E4C8
				public DebugView(DataflowBlock.SendAsyncSource<TOutput> source)
				{
					this._source = source;
				}

				// Token: 0x17000181 RID: 385
				// (get) Token: 0x06000474 RID: 1140 RVA: 0x000102D7 File Offset: 0x0000E4D7
				public ITargetBlock<TOutput> Target
				{
					get
					{
						return this._source._target;
					}
				}

				// Token: 0x17000182 RID: 386
				// (get) Token: 0x06000475 RID: 1141 RVA: 0x000102E4 File Offset: 0x0000E4E4
				public TOutput Message
				{
					get
					{
						return this._source._messageValue;
					}
				}

				// Token: 0x17000183 RID: 387
				// (get) Token: 0x06000476 RID: 1142 RVA: 0x000102F1 File Offset: 0x0000E4F1
				public Task<bool> Completion
				{
					get
					{
						return this._source.Task;
					}
				}

				// Token: 0x040001D4 RID: 468
				private readonly DataflowBlock.SendAsyncSource<TOutput> _source;
			}
		}

		// Token: 0x0200004F RID: 79
		private enum ReceiveCoreByLinkingCleanupReason
		{
			// Token: 0x040000F1 RID: 241
			Success,
			// Token: 0x040000F2 RID: 242
			Timer,
			// Token: 0x040000F3 RID: 243
			Cancellation,
			// Token: 0x040000F4 RID: 244
			SourceCompletion,
			// Token: 0x040000F5 RID: 245
			SourceProtocolError,
			// Token: 0x040000F6 RID: 246
			ErrorDuringCleanup
		}

		// Token: 0x02000050 RID: 80
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class ReceiveTarget<T> : TaskCompletionSource<T>, ITargetBlock<T>, IDataflowBlock, IDebuggerDisplay
		{
			// Token: 0x170000CA RID: 202
			// (get) Token: 0x060002BE RID: 702 RVA: 0x0000BF65 File Offset: 0x0000A165
			internal object IncomingLock
			{
				get
				{
					return this._cts;
				}
			}

			// Token: 0x060002BF RID: 703 RVA: 0x0000BF6D File Offset: 0x0000A16D
			internal ReceiveTarget()
			{
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x0000BF80 File Offset: 0x0000A180
			DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (source == null && consumeToAccept)
				{
					throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
				}
				DataflowMessageStatus dataflowMessageStatus = DataflowMessageStatus.NotAvailable;
				if (Volatile.Read(ref this._cleanupReserved))
				{
					return DataflowMessageStatus.DecliningPermanently;
				}
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (this._cleanupReserved)
					{
						return DataflowMessageStatus.DecliningPermanently;
					}
					try
					{
						bool flag2 = true;
						T t = (consumeToAccept ? source.ConsumeMessage(messageHeader, this, out flag2) : messageValue);
						if (flag2)
						{
							dataflowMessageStatus = DataflowMessageStatus.Accepted;
							this._receivedValue = t;
							this._cleanupReserved = true;
						}
					}
					catch (Exception ex)
					{
						dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
						Common.StoreDataflowMessageValueIntoExceptionData<T>(ex, messageValue, false);
						this._receivedException = ex;
						this._cleanupReserved = true;
					}
				}
				if (dataflowMessageStatus == DataflowMessageStatus.Accepted)
				{
					this.CleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.Success);
				}
				else if (dataflowMessageStatus == DataflowMessageStatus.DecliningPermanently)
				{
					this.CleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceProtocolError);
				}
				return dataflowMessageStatus;
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x0000C07C File Offset: 0x0000A27C
			internal bool TryCleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason reason)
			{
				if (Volatile.Read(ref this._cleanupReserved))
				{
					return false;
				}
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (this._cleanupReserved)
					{
						return false;
					}
					this._cleanupReserved = true;
				}
				this.CleanupAndComplete(reason);
				return true;
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
			private void CleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason reason)
			{
				IDisposable unlink = this._unlink;
				if (reason != DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceCompletion && unlink != null)
				{
					IDisposable disposable = Interlocked.CompareExchange<IDisposable>(ref this._unlink, null, unlink);
					if (disposable != null)
					{
						try
						{
							disposable.Dispose();
						}
						catch (Exception ex)
						{
							this._receivedException = ex;
							reason = DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceProtocolError;
						}
					}
				}
				if (this._timer != null)
				{
					this._timer.Dispose();
				}
				if (reason != DataflowBlock.ReceiveCoreByLinkingCleanupReason.Cancellation)
				{
					if (reason == DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceCompletion && (this._externalCancellationToken.IsCancellationRequested || this._cts.IsCancellationRequested))
					{
						reason = DataflowBlock.ReceiveCoreByLinkingCleanupReason.Cancellation;
					}
					this._cts.Cancel();
				}
				this._regFromExternalCancellationToken.Dispose();
				switch (reason)
				{
				case DataflowBlock.ReceiveCoreByLinkingCleanupReason.Success:
					global::System.Threading.Tasks.Task.Factory.StartNew(delegate(object state)
					{
						DataflowBlock.ReceiveTarget<T> receiveTarget = (DataflowBlock.ReceiveTarget<T>)state;
						try
						{
							receiveTarget.TrySetResult(receiveTarget._receivedValue);
						}
						catch (ObjectDisposedException)
						{
						}
					}, this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
					return;
				case DataflowBlock.ReceiveCoreByLinkingCleanupReason.Timer:
					if (this._receivedException == null)
					{
						this._receivedException = DataflowBlock.ReceiveTarget<T>.CreateExceptionForTimeout();
						goto IL_0138;
					}
					goto IL_0138;
				case DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceCompletion:
					if (this._receivedException == null)
					{
						this._receivedException = DataflowBlock.ReceiveTarget<T>.CreateExceptionForSourceCompletion();
						goto IL_0138;
					}
					goto IL_0138;
				case DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceProtocolError:
				case DataflowBlock.ReceiveCoreByLinkingCleanupReason.ErrorDuringCleanup:
					goto IL_0138;
				}
				global::System.Threading.Tasks.Task.Factory.StartNew(delegate(object state)
				{
					DataflowBlock.ReceiveTarget<T> receiveTarget2 = (DataflowBlock.ReceiveTarget<T>)state;
					try
					{
						receiveTarget2.TrySetCanceled();
					}
					catch (ObjectDisposedException)
					{
					}
				}, this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
				return;
				IL_0138:
				global::System.Threading.Tasks.Task.Factory.StartNew(delegate(object state)
				{
					DataflowBlock.ReceiveTarget<T> receiveTarget3 = (DataflowBlock.ReceiveTarget<T>)state;
					try
					{
						receiveTarget3.TrySetException(receiveTarget3._receivedException ?? new InvalidOperationException(SR.InvalidOperation_ErrorDuringCleanup));
					}
					catch (ObjectDisposedException)
					{
					}
				}, this, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0000C270 File Offset: 0x0000A470
			internal static Exception CreateExceptionForSourceCompletion()
			{
				return Common.InitializeStackTrace(new InvalidOperationException(SR.InvalidOperation_DataNotAvailableForReceive));
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000C281 File Offset: 0x0000A481
			internal static Exception CreateExceptionForTimeout()
			{
				return Common.InitializeStackTrace(new TimeoutException());
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x0000C28D File Offset: 0x0000A48D
			void IDataflowBlock.Complete()
			{
				this.TryCleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.SourceCompletion);
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0000C297 File Offset: 0x0000A497
			void IDataflowBlock.Fault(Exception exception)
			{
				((IDataflowBlock)this).Complete();
			}

			// Token: 0x170000CB RID: 203
			// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000C29F File Offset: 0x0000A49F
			Task IDataflowBlock.Completion
			{
				get
				{
					throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
				}
			}

			// Token: 0x170000CC RID: 204
			// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000C2AB File Offset: 0x0000A4AB
			private object DebuggerDisplayContent
			{
				get
				{
					return string.Format("{0} IsCompleted={1}", Common.GetNameForDebugger(this, null), base.Task.IsCompleted);
				}
			}

			// Token: 0x170000CD RID: 205
			// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000C2CE File Offset: 0x0000A4CE
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x040000F7 RID: 247
			internal static readonly TimerCallback CachedLinkingTimerCallback = delegate(object state)
			{
				DataflowBlock.ReceiveTarget<T> receiveTarget = (DataflowBlock.ReceiveTarget<T>)state;
				receiveTarget.TryCleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.Timer);
			};

			// Token: 0x040000F8 RID: 248
			internal static readonly Action<object> CachedLinkingCancellationCallback = delegate(object state)
			{
				DataflowBlock.ReceiveTarget<T> receiveTarget2 = (DataflowBlock.ReceiveTarget<T>)state;
				receiveTarget2.TryCleanupAndComplete(DataflowBlock.ReceiveCoreByLinkingCleanupReason.Cancellation);
			};

			// Token: 0x040000F9 RID: 249
			private T _receivedValue;

			// Token: 0x040000FA RID: 250
			internal readonly CancellationTokenSource _cts = new CancellationTokenSource();

			// Token: 0x040000FB RID: 251
			internal bool _cleanupReserved;

			// Token: 0x040000FC RID: 252
			internal CancellationToken _externalCancellationToken;

			// Token: 0x040000FD RID: 253
			internal CancellationTokenRegistration _regFromExternalCancellationToken;

			// Token: 0x040000FE RID: 254
			internal Timer _timer;

			// Token: 0x040000FF RID: 255
			internal IDisposable _unlink;

			// Token: 0x04000100 RID: 256
			internal Exception _receivedException;
		}

		// Token: 0x02000051 RID: 81
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class OutputAvailableAsyncTarget<T> : TaskCompletionSource<bool>, ITargetBlock<T>, IDataflowBlock, IDebuggerDisplay
		{
			// Token: 0x060002CB RID: 715 RVA: 0x0000C304 File Offset: 0x0000A504
			private static void CancelAndUnlink(object state)
			{
				DataflowBlock.OutputAvailableAsyncTarget<T> outputAvailableAsyncTarget = state as DataflowBlock.OutputAvailableAsyncTarget<T>;
				global::System.Threading.Tasks.Task.Factory.StartNew(delegate(object tgt)
				{
					DataflowBlock.OutputAvailableAsyncTarget<T> outputAvailableAsyncTarget2 = (DataflowBlock.OutputAvailableAsyncTarget<T>)tgt;
					outputAvailableAsyncTarget2.TrySetCanceled();
					outputAvailableAsyncTarget2.AttemptThreadSafeUnlink();
				}, outputAvailableAsyncTarget, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}

			// Token: 0x060002CC RID: 716 RVA: 0x0000C354 File Offset: 0x0000A554
			internal void AttemptThreadSafeUnlink()
			{
				IDisposable unlinker = this._unlinker;
				if (unlinker != null && Interlocked.CompareExchange<IDisposable>(ref this._unlinker, null, unlinker) == unlinker)
				{
					unlinker.Dispose();
				}
			}

			// Token: 0x060002CD RID: 717 RVA: 0x0000C381 File Offset: 0x0000A581
			DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (source == null)
				{
					throw new ArgumentNullException("source");
				}
				base.TrySetResult(true);
				return DataflowMessageStatus.DecliningPermanently;
			}

			// Token: 0x060002CE RID: 718 RVA: 0x0000C3B3 File Offset: 0x0000A5B3
			void IDataflowBlock.Complete()
			{
				base.TrySetResult(false);
			}

			// Token: 0x060002CF RID: 719 RVA: 0x0000C3BD File Offset: 0x0000A5BD
			void IDataflowBlock.Fault(Exception exception)
			{
				if (exception == null)
				{
					throw new ArgumentNullException("exception");
				}
				base.TrySetResult(false);
			}

			// Token: 0x170000CE RID: 206
			// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000C3D5 File Offset: 0x0000A5D5
			Task IDataflowBlock.Completion
			{
				get
				{
					throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
				}
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000C3E1 File Offset: 0x0000A5E1
			private object DebuggerDisplayContent
			{
				get
				{
					return string.Format("{0} IsCompleted={1}", Common.GetNameForDebugger(this, null), base.Task.IsCompleted);
				}
			}

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000C404 File Offset: 0x0000A604
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x04000101 RID: 257
			internal static readonly Func<Task<bool>, object, bool> s_handleCompletion = delegate(Task<bool> antecedent, object state)
			{
				DataflowBlock.OutputAvailableAsyncTarget<T> outputAvailableAsyncTarget = state as DataflowBlock.OutputAvailableAsyncTarget<T>;
				outputAvailableAsyncTarget._ctr.Dispose();
				return antecedent.GetAwaiter().GetResult();
			};

			// Token: 0x04000102 RID: 258
			internal static readonly Action<object> s_cancelAndUnlink = new Action<object>(DataflowBlock.OutputAvailableAsyncTarget<T>.CancelAndUnlink);

			// Token: 0x04000103 RID: 259
			internal IDisposable _unlinker;

			// Token: 0x04000104 RID: 260
			internal CancellationTokenRegistration _ctr;
		}

		// Token: 0x02000052 RID: 82
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		[DebuggerTypeProxy(typeof(DataflowBlock.EncapsulatingPropagator<, >.DebugView))]
		private sealed class EncapsulatingPropagator<TInput, TOutput> : IPropagatorBlock<TInput, TOutput>, ITargetBlock<TInput>, IDataflowBlock, ISourceBlock<TOutput>, IReceivableSourceBlock<TOutput>, IDebuggerDisplay
		{
			// Token: 0x060002D5 RID: 725 RVA: 0x0000C43C File Offset: 0x0000A63C
			public EncapsulatingPropagator(ITargetBlock<TInput> target, ISourceBlock<TOutput> source)
			{
				this._target = target;
				this._source = source;
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x0000C452 File Offset: 0x0000A652
			public void Complete()
			{
				this._target.Complete();
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x0000C45F File Offset: 0x0000A65F
			void IDataflowBlock.Fault(Exception exception)
			{
				if (exception == null)
				{
					throw new ArgumentNullException("exception");
				}
				this._target.Fault(exception);
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000C47B File Offset: 0x0000A67B
			public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
			{
				return this._target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
			}

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000C48D File Offset: 0x0000A68D
			public Task Completion
			{
				get
				{
					return this._source.Completion;
				}
			}

			// Token: 0x060002DA RID: 730 RVA: 0x0000C49A File Offset: 0x0000A69A
			public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
			{
				return this._source.LinkTo(target, linkOptions);
			}

			// Token: 0x060002DB RID: 731 RVA: 0x0000C4AC File Offset: 0x0000A6AC
			public bool TryReceive(Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item)
			{
				IReceivableSourceBlock<TOutput> receivableSourceBlock = this._source as IReceivableSourceBlock<TOutput>;
				if (receivableSourceBlock != null)
				{
					return receivableSourceBlock.TryReceive(filter, out item);
				}
				item = default(TOutput);
				return false;
			}

			// Token: 0x060002DC RID: 732 RVA: 0x0000C4DC File Offset: 0x0000A6DC
			public bool TryReceiveAll([NotNullWhen(true)] out IList<TOutput> items)
			{
				IReceivableSourceBlock<TOutput> receivableSourceBlock = this._source as IReceivableSourceBlock<TOutput>;
				if (receivableSourceBlock != null)
				{
					return receivableSourceBlock.TryReceiveAll(out items);
				}
				items = null;
				return false;
			}

			// Token: 0x060002DD RID: 733 RVA: 0x0000C504 File Offset: 0x0000A704
			public TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
			{
				return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
			}

			// Token: 0x060002DE RID: 734 RVA: 0x0000C514 File Offset: 0x0000A714
			public bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
			{
				return this._source.ReserveMessage(messageHeader, target);
			}

			// Token: 0x060002DF RID: 735 RVA: 0x0000C523 File Offset: 0x0000A723
			public void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
			{
				this._source.ReleaseReservation(messageHeader, target);
			}

			// Token: 0x170000D2 RID: 210
			// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000C534 File Offset: 0x0000A734
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._target as IDebuggerDisplay;
					IDebuggerDisplay debuggerDisplay2 = this._source as IDebuggerDisplay;
					return string.Format("{0} Target=\"{1}\", Source=\"{2}\"", Common.GetNameForDebugger(this, null), (debuggerDisplay != null) ? debuggerDisplay.Content : this._target, (debuggerDisplay2 != null) ? debuggerDisplay2.Content : this._source);
				}
			}

			// Token: 0x170000D3 RID: 211
			// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000C58C File Offset: 0x0000A78C
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x04000105 RID: 261
			private readonly ITargetBlock<TInput> _target;

			// Token: 0x04000106 RID: 262
			private readonly ISourceBlock<TOutput> _source;

			// Token: 0x02000096 RID: 150
			private sealed class DebugView
			{
				// Token: 0x06000489 RID: 1161 RVA: 0x0001054D File Offset: 0x0000E74D
				public DebugView(DataflowBlock.EncapsulatingPropagator<TInput, TOutput> propagator)
				{
					this._propagator = propagator;
				}

				// Token: 0x17000184 RID: 388
				// (get) Token: 0x0600048A RID: 1162 RVA: 0x0001055C File Offset: 0x0000E75C
				public ITargetBlock<TInput> Target
				{
					get
					{
						return this._propagator._target;
					}
				}

				// Token: 0x17000185 RID: 389
				// (get) Token: 0x0600048B RID: 1163 RVA: 0x00010569 File Offset: 0x0000E769
				public ISourceBlock<TOutput> Source
				{
					get
					{
						return this._propagator._source;
					}
				}

				// Token: 0x040001E1 RID: 481
				private readonly DataflowBlock.EncapsulatingPropagator<TInput, TOutput> _propagator;
			}
		}

		// Token: 0x02000053 RID: 83
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class ChooseTarget<T> : TaskCompletionSource<T>, ITargetBlock<T>, IDataflowBlock, IDebuggerDisplay
		{
			// Token: 0x060002E2 RID: 738 RVA: 0x0000C594 File Offset: 0x0000A794
			internal ChooseTarget(StrongBox<Task> completed, CancellationToken cancellationToken)
			{
				this._completed = completed;
				Common.WireCancellationToComplete(cancellationToken, base.Task, delegate(object state)
				{
					DataflowBlock.ChooseTarget<T> chooseTarget = (DataflowBlock.ChooseTarget<T>)state;
					StrongBox<Task> completed2 = chooseTarget._completed;
					lock (completed2)
					{
						chooseTarget.TrySetCanceled();
					}
				}, this);
			}

			// Token: 0x060002E3 RID: 739 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
			public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (source == null && consumeToAccept)
				{
					throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
				}
				StrongBox<Task> completed = this._completed;
				DataflowMessageStatus dataflowMessageStatus;
				lock (completed)
				{
					if (this._completed.Value != null || base.Task.IsCompleted)
					{
						dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
					}
					else
					{
						if (consumeToAccept)
						{
							bool flag2;
							messageValue = source.ConsumeMessage(messageHeader, this, out flag2);
							if (!flag2)
							{
								return DataflowMessageStatus.NotAvailable;
							}
						}
						base.TrySetResult(messageValue);
						this._completed.Value = base.Task;
						dataflowMessageStatus = DataflowMessageStatus.Accepted;
					}
				}
				return dataflowMessageStatus;
			}

			// Token: 0x060002E4 RID: 740 RVA: 0x0000C690 File Offset: 0x0000A890
			void IDataflowBlock.Complete()
			{
				StrongBox<Task> completed = this._completed;
				lock (completed)
				{
					base.TrySetCanceled();
				}
			}

			// Token: 0x060002E5 RID: 741 RVA: 0x0000C6D4 File Offset: 0x0000A8D4
			void IDataflowBlock.Fault(Exception exception)
			{
				((IDataflowBlock)this).Complete();
			}

			// Token: 0x170000D4 RID: 212
			// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000C6DC File Offset: 0x0000A8DC
			Task IDataflowBlock.Completion
			{
				get
				{
					throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
				}
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
			private object DebuggerDisplayContent
			{
				get
				{
					return string.Format("{0} IsCompleted={1}", Common.GetNameForDebugger(this, null), base.Task.IsCompleted);
				}
			}

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000C70B File Offset: 0x0000A90B
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x04000107 RID: 263
			internal static readonly Func<object, int> s_processBranchFunction = delegate(object state)
			{
				Tuple<Action<T>, T, int> tuple = (Tuple<Action<T>, T, int>)state;
				tuple.Item1(tuple.Item2);
				return tuple.Item3;
			};

			// Token: 0x04000108 RID: 264
			private readonly StrongBox<Task> _completed;
		}

		// Token: 0x02000054 RID: 84
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		[DebuggerTypeProxy(typeof(DataflowBlock.SourceObservable<>.DebugView))]
		private sealed class SourceObservable<TOutput> : IObservable<TOutput>, IDebuggerDisplay
		{
			// Token: 0x060002EA RID: 746 RVA: 0x0000C72A File Offset: 0x0000A92A
			internal static IObservable<TOutput> From(ISourceBlock<TOutput> source)
			{
				return DataflowBlock.SourceObservable<TOutput>._table.GetValue(source, (ISourceBlock<TOutput> s) => new DataflowBlock.SourceObservable<TOutput>(s));
			}

			// Token: 0x060002EB RID: 747 RVA: 0x0000C756 File Offset: 0x0000A956
			internal SourceObservable(ISourceBlock<TOutput> source)
			{
				this._source = source;
				this._observersState = new DataflowBlock.SourceObservable<TOutput>.ObserversState(this);
			}

			// Token: 0x060002EC RID: 748 RVA: 0x0000C77C File Offset: 0x0000A97C
			private AggregateException GetCompletionError()
			{
				Task potentiallyNotSupportedCompletionTask = Common.GetPotentiallyNotSupportedCompletionTask(this._source);
				if (potentiallyNotSupportedCompletionTask == null || !potentiallyNotSupportedCompletionTask.IsFaulted)
				{
					return null;
				}
				return potentiallyNotSupportedCompletionTask.Exception;
			}

			// Token: 0x060002ED RID: 749 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
			IDisposable IObservable<TOutput>.Subscribe(IObserver<TOutput> observer)
			{
				if (observer == null)
				{
					throw new ArgumentNullException("observer");
				}
				Task potentiallyNotSupportedCompletionTask = Common.GetPotentiallyNotSupportedCompletionTask(this._source);
				Exception ex = null;
				object subscriptionLock = this._SubscriptionLock;
				lock (subscriptionLock)
				{
					if (potentiallyNotSupportedCompletionTask == null || !potentiallyNotSupportedCompletionTask.IsCompleted || !this._observersState.Target.Completion.IsCompleted)
					{
						this._observersState.Observers = this._observersState.Observers.Add(observer);
						if (this._observersState.Observers.Count == 1)
						{
							this._observersState.Unlinker = this._source.LinkTo(this._observersState.Target);
							if (this._observersState.Unlinker == null)
							{
								this._observersState.Observers = ImmutableArray<IObserver<TOutput>>.Empty;
								return Disposables.Nop;
							}
						}
						return Disposables.Create<DataflowBlock.SourceObservable<TOutput>, IObserver<TOutput>>(delegate(DataflowBlock.SourceObservable<TOutput> s, IObserver<TOutput> o)
						{
							s.Unsubscribe(o);
						}, this, observer);
					}
					ex = this.GetCompletionError();
				}
				if (ex != null)
				{
					observer.OnError(ex);
				}
				else
				{
					observer.OnCompleted();
				}
				return Disposables.Nop;
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
			private void Unsubscribe(IObserver<TOutput> observer)
			{
				object subscriptionLock = this._SubscriptionLock;
				lock (subscriptionLock)
				{
					DataflowBlock.SourceObservable<TOutput>.ObserversState observersState = this._observersState;
					if (observersState.Observers.Contains(observer))
					{
						if (observersState.Observers.Count == 1)
						{
							this.ResetObserverState();
						}
						else
						{
							observersState.Observers = observersState.Observers.Remove(observer);
						}
					}
				}
			}

			// Token: 0x060002EF RID: 751 RVA: 0x0000C960 File Offset: 0x0000AB60
			private ImmutableArray<IObserver<TOutput>> ResetObserverState()
			{
				DataflowBlock.SourceObservable<TOutput>.ObserversState observersState = this._observersState;
				ImmutableArray<IObserver<TOutput>> observers = observersState.Observers;
				this._observersState = new DataflowBlock.SourceObservable<TOutput>.ObserversState(this);
				observersState.Unlinker.Dispose();
				observersState.Canceler.Cancel();
				return observers;
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._source as IDebuggerDisplay;
					return string.Format("Observers={0}, Block=\"{1}\"", this._observersState.Observers.Count, (debuggerDisplay != null) ? debuggerDisplay.Content : this._source);
				}
			}

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000C9E9 File Offset: 0x0000ABE9
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x04000109 RID: 265
			private static readonly ConditionalWeakTable<ISourceBlock<TOutput>, DataflowBlock.SourceObservable<TOutput>> _table = new ConditionalWeakTable<ISourceBlock<TOutput>, DataflowBlock.SourceObservable<TOutput>>();

			// Token: 0x0400010A RID: 266
			private readonly object _SubscriptionLock = new object();

			// Token: 0x0400010B RID: 267
			private readonly ISourceBlock<TOutput> _source;

			// Token: 0x0400010C RID: 268
			private DataflowBlock.SourceObservable<TOutput>.ObserversState _observersState;

			// Token: 0x02000098 RID: 152
			private sealed class DebugView
			{
				// Token: 0x06000490 RID: 1168 RVA: 0x000105FF File Offset: 0x0000E7FF
				public DebugView(DataflowBlock.SourceObservable<TOutput> observable)
				{
					this._observable = observable;
				}

				// Token: 0x17000186 RID: 390
				// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001060E File Offset: 0x0000E80E
				[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
				public IObserver<TOutput>[] Observers
				{
					get
					{
						return this._observable._observersState.Observers.ToArray();
					}
				}

				// Token: 0x040001E4 RID: 484
				private readonly DataflowBlock.SourceObservable<TOutput> _observable;
			}

			// Token: 0x02000099 RID: 153
			private sealed class ObserversState
			{
				// Token: 0x06000492 RID: 1170 RVA: 0x00010628 File Offset: 0x0000E828
				internal ObserversState(DataflowBlock.SourceObservable<TOutput> observable)
				{
					this.Observable = observable;
					this.Target = new ActionBlock<TOutput>(new Func<TOutput, Task>(this.ProcessItemAsync), DataflowBlock._nonGreedyExecutionOptions);
					this.Target.Completion.ContinueWith(delegate(Task t, object state)
					{
						((DataflowBlock.SourceObservable<TOutput>.ObserversState)state).NotifyObserversOfCompletion(t.Exception);
					}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.NotOnRanToCompletion | TaskContinuationOptions.NotOnCanceled | TaskContinuationOptions.ExecuteSynchronously), TaskScheduler.Default);
					Task potentiallyNotSupportedCompletionTask = Common.GetPotentiallyNotSupportedCompletionTask(this.Observable._source);
					if (potentiallyNotSupportedCompletionTask != null)
					{
						potentiallyNotSupportedCompletionTask.ContinueWith(delegate(Task _1, object state1)
						{
							DataflowBlock.SourceObservable<TOutput>.ObserversState observersState = (DataflowBlock.SourceObservable<TOutput>.ObserversState)state1;
							observersState.Target.Complete();
							observersState.Target.Completion.ContinueWith(delegate(Task _2, object state2)
							{
								((DataflowBlock.SourceObservable<TOutput>.ObserversState)state2).NotifyObserversOfCompletion(null);
							}, state1, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.ExecuteSynchronously), TaskScheduler.Default);
						}, this, this.Canceler.Token, Common.GetContinuationOptions(TaskContinuationOptions.ExecuteSynchronously), TaskScheduler.Default);
					}
				}

				// Token: 0x06000493 RID: 1171 RVA: 0x00010710 File Offset: 0x0000E910
				private Task ProcessItemAsync(TOutput item)
				{
					object subscriptionLock = this.Observable._SubscriptionLock;
					ImmutableArray<IObserver<TOutput>> observers;
					lock (subscriptionLock)
					{
						observers = this.Observers;
					}
					try
					{
						foreach (IObserver<TOutput> observer in observers)
						{
							DataflowBlock.TargetObserver<TOutput> targetObserver = observer as DataflowBlock.TargetObserver<TOutput>;
							if (targetObserver != null)
							{
								Task<bool> task = targetObserver.SendAsyncToTarget(item);
								if (task.Status != TaskStatus.RanToCompletion)
								{
									if (this._tempSendAsyncTaskList == null)
									{
										this._tempSendAsyncTaskList = new List<Task<bool>>();
									}
									this._tempSendAsyncTaskList.Add(task);
								}
							}
							else
							{
								observer.OnNext(item);
							}
						}
						if (this._tempSendAsyncTaskList != null && this._tempSendAsyncTaskList.Count > 0)
						{
							Task<bool[]> task2 = Task.WhenAll<bool>(this._tempSendAsyncTaskList);
							this._tempSendAsyncTaskList.Clear();
							return task2;
						}
					}
					catch (Exception ex)
					{
						return Common.CreateTaskFromException<VoidResult>(ex);
					}
					return Common.CompletedTaskWithTrueResult;
				}

				// Token: 0x06000494 RID: 1172 RVA: 0x0001082C File Offset: 0x0000EA2C
				private void NotifyObserversOfCompletion(Exception targetException = null)
				{
					object subscriptionLock = this.Observable._SubscriptionLock;
					ImmutableArray<IObserver<TOutput>> observers;
					lock (subscriptionLock)
					{
						observers = this.Observers;
						if (targetException != null)
						{
							this.Observable.ResetObserverState();
						}
						this.Observers = ImmutableArray<IObserver<TOutput>>.Empty;
					}
					if (observers.Count > 0)
					{
						Exception ex = targetException ?? this.Observable.GetCompletionError();
						try
						{
							if (ex != null)
							{
								using (IEnumerator<IObserver<TOutput>> enumerator = observers.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										IObserver<TOutput> observer = enumerator.Current;
										observer.OnError(ex);
									}
									goto IL_00C9;
								}
							}
							foreach (IObserver<TOutput> observer2 in observers)
							{
								observer2.OnCompleted();
							}
							IL_00C9:;
						}
						catch (Exception ex2)
						{
							Common.ThrowAsync(ex2);
						}
					}
				}

				// Token: 0x040001E5 RID: 485
				internal readonly DataflowBlock.SourceObservable<TOutput> Observable;

				// Token: 0x040001E6 RID: 486
				internal readonly ActionBlock<TOutput> Target;

				// Token: 0x040001E7 RID: 487
				internal readonly CancellationTokenSource Canceler = new CancellationTokenSource();

				// Token: 0x040001E8 RID: 488
				internal ImmutableArray<IObserver<TOutput>> Observers = ImmutableArray<IObserver<TOutput>>.Empty;

				// Token: 0x040001E9 RID: 489
				internal IDisposable Unlinker;

				// Token: 0x040001EA RID: 490
				private List<Task<bool>> _tempSendAsyncTaskList;
			}
		}

		// Token: 0x02000055 RID: 85
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class TargetObserver<TInput> : IObserver<TInput>, IDebuggerDisplay
		{
			// Token: 0x060002F3 RID: 755 RVA: 0x0000C9FD File Offset: 0x0000ABFD
			internal TargetObserver(ITargetBlock<TInput> target)
			{
				this._target = target;
			}

			// Token: 0x060002F4 RID: 756 RVA: 0x0000CA0C File Offset: 0x0000AC0C
			void IObserver<TInput>.OnNext(TInput value)
			{
				Task<bool> task = this.SendAsyncToTarget(value);
				task.GetAwaiter().GetResult();
			}

			// Token: 0x060002F5 RID: 757 RVA: 0x0000CA30 File Offset: 0x0000AC30
			void IObserver<TInput>.OnCompleted()
			{
				this._target.Complete();
			}

			// Token: 0x060002F6 RID: 758 RVA: 0x0000CA3D File Offset: 0x0000AC3D
			void IObserver<TInput>.OnError(Exception error)
			{
				this._target.Fault(error);
			}

			// Token: 0x060002F7 RID: 759 RVA: 0x0000CA4B File Offset: 0x0000AC4B
			internal Task<bool> SendAsyncToTarget(TInput value)
			{
				return this._target.SendAsync(value);
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000CA5C File Offset: 0x0000AC5C
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay debuggerDisplay = this._target as IDebuggerDisplay;
					return string.Format("Block=\"{0}\"", (debuggerDisplay != null) ? debuggerDisplay.Content : this._target);
				}
			}

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000CA90 File Offset: 0x0000AC90
			object IDebuggerDisplay.Content
			{
				get
				{
					return this.DebuggerDisplayContent;
				}
			}

			// Token: 0x0400010D RID: 269
			private readonly ITargetBlock<TInput> _target;
		}

		// Token: 0x02000056 RID: 86
		private sealed class NullTargetBlock<TInput> : ITargetBlock<TInput>, IDataflowBlock
		{
			// Token: 0x060002FA RID: 762 RVA: 0x0000CA98 File Offset: 0x0000AC98
			DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (consumeToAccept)
				{
					if (source == null)
					{
						throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
					}
					bool flag;
					source.ConsumeMessage(messageHeader, this, out flag);
					if (!flag)
					{
						return DataflowMessageStatus.NotAvailable;
					}
				}
				return DataflowMessageStatus.Accepted;
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000CAE6 File Offset: 0x0000ACE6
			void IDataflowBlock.Complete()
			{
			}

			// Token: 0x060002FC RID: 764 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
			void IDataflowBlock.Fault(Exception exception)
			{
			}

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x060002FD RID: 765 RVA: 0x0000CAEA File Offset: 0x0000ACEA
			Task IDataflowBlock.Completion
			{
				get
				{
					return LazyInitializer.EnsureInitialized<Task>(ref this._completion, () => new TaskCompletionSource<VoidResult>().Task);
				}
			}

			// Token: 0x0400010E RID: 270
			private Task _completion;
		}
	}
}
