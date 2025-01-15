using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(TransformManyBlock<, >.DebugView))]
	public sealed class TransformManyBlock<[Nullable(2)] TInput, [Nullable(2)] TOutput> : IPropagatorBlock<TInput, TOutput>, ITargetBlock<TInput>, IDataflowBlock, ISourceBlock<TOutput>, IReceivableSourceBlock<TOutput>, IDebuggerDisplay
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000646B File Offset: 0x0000466B
		private object ParallelSourceLock
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00006473 File Offset: 0x00004673
		public TransformManyBlock(Func<TInput, IEnumerable<TOutput>> transform)
			: this(transform, null, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006482 File Offset: 0x00004682
		public TransformManyBlock(Func<TInput, IEnumerable<TOutput>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(transform, null, dataflowBlockOptions)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000648D File Offset: 0x0000468D
		public TransformManyBlock(Func<TInput, Task<IEnumerable<TOutput>>> transform)
			: this(null, transform, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000649C File Offset: 0x0000469C
		public TransformManyBlock(Func<TInput, Task<IEnumerable<TOutput>>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(null, transform, dataflowBlockOptions)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000064A8 File Offset: 0x000046A8
		private TransformManyBlock(Func<TInput, IEnumerable<TOutput>> transformSync, Func<TInput, Task<IEnumerable<TOutput>>> transformAsync, ExecutionDataflowBlockOptions dataflowBlockOptions)
		{
			TransformManyBlock<TInput, TOutput> <>4__this = this;
			if (transformSync == null && transformAsync == null)
			{
				throw new ArgumentNullException("transform");
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<ISourceBlock<TOutput>, int> action = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = delegate(ISourceBlock<TOutput> owningSource, int count)
				{
					((TransformManyBlock<TInput, TOutput>)owningSource)._target.ChangeBoundingCount(-count);
				};
			}
			this._source = new SourceCore<TOutput>(this, dataflowBlockOptions, delegate(ISourceBlock<TOutput> owningSource)
			{
				((TransformManyBlock<TInput, TOutput>)owningSource)._target.Complete(null, true, false, false, false);
			}, action, null);
			if (dataflowBlockOptions.SupportsParallelExecution && dataflowBlockOptions.EnsureOrdered)
			{
				this._reorderingBuffer = new ReorderingBuffer<IEnumerable<TOutput>>(this, delegate(object source, IEnumerable<TOutput> messages)
				{
					((TransformManyBlock<TInput, TOutput>)source)._source.AddMessages(messages);
				});
			}
			if (transformSync != null)
			{
				this._target = new TargetCore<TInput>(this, delegate(KeyValuePair<TInput, long> messageWithId)
				{
					<>4__this.ProcessMessage(transformSync, messageWithId);
				}, this._reorderingBuffer, dataflowBlockOptions, TargetCoreOptions.None);
			}
			else
			{
				this._target = new TargetCore<TInput>(this, delegate(KeyValuePair<TInput, long> messageWithId)
				{
					<>4__this.ProcessMessageWithTask(transformAsync, messageWithId);
				}, this._reorderingBuffer, dataflowBlockOptions, TargetCoreOptions.UsesAsyncCompletion);
			}
			this._target.Completion.ContinueWith(delegate(Task completed, object state)
			{
				SourceCore<TOutput> sourceCore = (SourceCore<TOutput>)state;
				if (completed.IsFaulted)
				{
					sourceCore.AddAndUnwrapAggregateException(completed.Exception);
				}
				sourceCore.Complete();
			}, this._source, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (TransformManyBlock<TInput, TOutput>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this.Completion, delegate(object state)
			{
				((TargetCore<TInput>)state).Complete(null, true, false, false, false);
			}, this._target);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000066C0 File Offset: 0x000048C0
		private void ProcessMessage(Func<TInput, IEnumerable<TOutput>> transformFunction, KeyValuePair<TInput, long> messageWithId)
		{
			bool flag = false;
			try
			{
				IEnumerable<TOutput> enumerable = transformFunction(messageWithId.Key);
				flag = true;
				this.StoreOutputItems(messageWithId, enumerable);
			}
			catch (Exception ex)
			{
				if (!Common.IsCooperativeCancellation(ex))
				{
					throw;
				}
			}
			finally
			{
				if (!flag)
				{
					this.StoreOutputItems(messageWithId, null);
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006720 File Offset: 0x00004920
		private void ProcessMessageWithTask(Func<TInput, Task<IEnumerable<TOutput>>> function, KeyValuePair<TInput, long> messageWithId)
		{
			Task<IEnumerable<TOutput>> task = null;
			Exception ex = null;
			try
			{
				task = function(messageWithId.Key);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (task != null)
			{
				task.ContinueWith(delegate(Task<IEnumerable<TOutput>> completed, object state)
				{
					Tuple<TransformManyBlock<TInput, TOutput>, KeyValuePair<TInput, long>> tuple = (Tuple<TransformManyBlock<TInput, TOutput>, KeyValuePair<TInput, long>>)state;
					tuple.Item1.AsyncCompleteProcessMessageWithTask(completed, tuple.Item2);
				}, Tuple.Create<TransformManyBlock<TInput, TOutput>, KeyValuePair<TInput, long>>(this, messageWithId), CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.ExecuteSynchronously), this._source.DataflowBlockOptions.TaskScheduler);
				return;
			}
			if (ex != null && !Common.IsCooperativeCancellation(ex))
			{
				Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, messageWithId.Key, false);
				this._target.Complete(ex, true, true, false, false);
			}
			if (this._reorderingBuffer != null)
			{
				this.StoreOutputItems(messageWithId, null);
				this._target.SignalOneAsyncMessageCompleted();
				return;
			}
			this._target.SignalOneAsyncMessageCompleted(-1);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000067F8 File Offset: 0x000049F8
		private void AsyncCompleteProcessMessageWithTask(Task<IEnumerable<TOutput>> completed, KeyValuePair<TInput, long> messageWithId)
		{
			switch (completed.Status)
			{
			case TaskStatus.RanToCompletion:
			{
				IEnumerable<TOutput> result = completed.Result;
				try
				{
					this.StoreOutputItems(messageWithId, result);
					goto IL_0084;
				}
				catch (Exception ex)
				{
					if (!Common.IsCooperativeCancellation(ex))
					{
						Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, messageWithId.Key, false);
						this._target.Complete(ex, true, true, false, false);
					}
					goto IL_0084;
				}
				break;
			}
			case TaskStatus.Canceled:
				goto IL_007C;
			case TaskStatus.Faulted:
				break;
			default:
				goto IL_0084;
			}
			AggregateException exception = completed.Exception;
			Common.StoreDataflowMessageValueIntoExceptionData<TInput>(exception, messageWithId.Key, true);
			this._target.Complete(exception, true, true, true, false);
			IL_007C:
			this.StoreOutputItems(messageWithId, null);
			IL_0084:
			this._target.SignalOneAsyncMessageCompleted();
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000068A4 File Offset: 0x00004AA4
		private void StoreOutputItems(KeyValuePair<TInput, long> messageWithId, IEnumerable<TOutput> outputItems)
		{
			if (this._reorderingBuffer != null)
			{
				this.StoreOutputItemsReordered(messageWithId.Value, outputItems);
				return;
			}
			if (outputItems == null)
			{
				if (this._target.IsBounded)
				{
					this._target.ChangeBoundingCount(-1);
				}
				return;
			}
			if (outputItems is TOutput[] || outputItems is List<TOutput>)
			{
				this.StoreOutputItemsNonReorderedAtomic(outputItems);
				return;
			}
			this.StoreOutputItemsNonReorderedWithIteration(outputItems);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006904 File Offset: 0x00004B04
		private void StoreOutputItemsReordered(long id, IEnumerable<TOutput> item)
		{
			TargetCore<TInput> target = this._target;
			bool isBounded = target.IsBounded;
			if (item == null)
			{
				this._reorderingBuffer.AddItem(id, null, false);
				if (isBounded)
				{
					target.ChangeBoundingCount(-1);
				}
				return;
			}
			IList<TOutput> list = item as TOutput[];
			if (list == null)
			{
				list = item as List<TOutput>;
			}
			if (list != null && isBounded)
			{
				this.UpdateBoundingCountWithOutputCount(list.Count);
			}
			bool? flag = this._reorderingBuffer.AddItemIfNextAndTrusted(id, list, list != null);
			if (flag == null)
			{
				return;
			}
			bool value = flag.Value;
			List<TOutput> list2 = null;
			try
			{
				if (value)
				{
					this.StoreOutputItemsNonReorderedWithIteration(item);
				}
				else if (list != null)
				{
					list2 = list.ToList<TOutput>();
				}
				else
				{
					int num = 0;
					try
					{
						list2 = item.ToList<TOutput>();
						num = list2.Count;
					}
					finally
					{
						if (isBounded)
						{
							this.UpdateBoundingCountWithOutputCount(num);
						}
					}
				}
			}
			finally
			{
				this._reorderingBuffer.AddItem(id, list2, list2 != null);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000069F4 File Offset: 0x00004BF4
		private void StoreOutputItemsNonReorderedAtomic(IEnumerable<TOutput> outputItems)
		{
			if (this._target.IsBounded)
			{
				this.UpdateBoundingCountWithOutputCount(((ICollection<TOutput>)outputItems).Count);
			}
			if (this._target.DataflowBlockOptions.MaxDegreeOfParallelism == 1)
			{
				this._source.AddMessages(outputItems);
				return;
			}
			object parallelSourceLock = this.ParallelSourceLock;
			lock (parallelSourceLock)
			{
				this._source.AddMessages(outputItems);
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006A78 File Offset: 0x00004C78
		private void StoreOutputItemsNonReorderedWithIteration(IEnumerable<TOutput> outputItems)
		{
			bool flag = this._target.DataflowBlockOptions.MaxDegreeOfParallelism == 1 || this._reorderingBuffer != null;
			if (this._target.IsBounded)
			{
				bool flag2 = false;
				try
				{
					using (IEnumerator<TOutput> enumerator = outputItems.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TOutput toutput = enumerator.Current;
							if (flag2)
							{
								this._target.ChangeBoundingCount(1);
							}
							else
							{
								flag2 = true;
							}
							if (flag)
							{
								this._source.AddMessage(toutput);
							}
							else
							{
								object parallelSourceLock = this.ParallelSourceLock;
								lock (parallelSourceLock)
								{
									this._source.AddMessage(toutput);
								}
							}
						}
						return;
					}
				}
				finally
				{
					if (!flag2)
					{
						this._target.ChangeBoundingCount(-1);
					}
				}
			}
			if (flag)
			{
				using (IEnumerator<TOutput> enumerator2 = outputItems.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TOutput toutput2 = enumerator2.Current;
						this._source.AddMessage(toutput2);
					}
					return;
				}
			}
			foreach (TOutput toutput3 in outputItems)
			{
				object parallelSourceLock2 = this.ParallelSourceLock;
				lock (parallelSourceLock2)
				{
					this._source.AddMessage(toutput3);
				}
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00006C20 File Offset: 0x00004E20
		private void UpdateBoundingCountWithOutputCount(int count)
		{
			if (count > 1)
			{
				this._target.ChangeBoundingCount(count - 1);
				return;
			}
			if (count == 0)
			{
				this._target.ChangeBoundingCount(-1);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006C44 File Offset: 0x00004E44
		public void Complete()
		{
			this._target.Complete(null, false, false, false, false);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006C56 File Offset: 0x00004E56
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._target.Complete(exception, true, false, false, false);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006C76 File Offset: 0x00004E76
		public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006C85 File Offset: 0x00004E85
		public bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006C94 File Offset: 0x00004E94
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IList<TOutput> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00006CA2 File Offset: 0x00004EA2
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006CAF File Offset: 0x00004EAF
		public int InputCount
		{
			get
			{
				return this._target.InputCount;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00006CBC File Offset: 0x00004EBC
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006CC9 File Offset: 0x00004EC9
		DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			return this._target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006CDB File Offset: 0x00004EDB
		TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006CEB File Offset: 0x00004EEB
		bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006CFA File Offset: 0x00004EFA
		void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00006D09 File Offset: 0x00004F09
		private int InputCountForDebugger
		{
			get
			{
				return this._target.GetDebuggingInformation().InputCount;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00006D1B File Offset: 0x00004F1B
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006D2D File Offset: 0x00004F2D
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00006D40 File Offset: 0x00004F40
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, InputCount={1}, OutputCount={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.InputCountForDebugger, this.OutputCountForDebugger);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00006D73 File Offset: 0x00004F73
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000058 RID: 88
		private readonly TargetCore<TInput> _target;

		// Token: 0x04000059 RID: 89
		private readonly ReorderingBuffer<IEnumerable<TOutput>> _reorderingBuffer;

		// Token: 0x0400005A RID: 90
		private readonly SourceCore<TOutput> _source;

		// Token: 0x02000073 RID: 115
		private sealed class DebugView
		{
			// Token: 0x060003EC RID: 1004 RVA: 0x0000F6C2 File Offset: 0x0000D8C2
			public DebugView(TransformManyBlock<TInput, TOutput> transformManyBlock)
			{
				this._transformManyBlock = transformManyBlock;
				this._targetDebuggingInformation = transformManyBlock._target.GetDebuggingInformation();
				this._sourceDebuggingInformation = transformManyBlock._source.GetDebuggingInformation();
			}

			// Token: 0x1700014F RID: 335
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F6F3 File Offset: 0x0000D8F3
			public IEnumerable<TInput> InputQueue
			{
				get
				{
					return this._targetDebuggingInformation.InputQueue;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000F700 File Offset: 0x0000D900
			public QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					return this._targetDebuggingInformation.PostponedMessages;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F70D File Offset: 0x0000D90D
			public IEnumerable<TOutput> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x17000152 RID: 338
			// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000F71A File Offset: 0x0000D91A
			public int CurrentDegreeOfParallelism
			{
				get
				{
					return this._targetDebuggingInformation.CurrentDegreeOfParallelism;
				}
			}

			// Token: 0x17000153 RID: 339
			// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000F727 File Offset: 0x0000D927
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000F734 File Offset: 0x0000D934
			public ExecutionDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._targetDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000F741 File Offset: 0x0000D941
			public bool IsDecliningPermanently
			{
				get
				{
					return this._targetDebuggingInformation.IsDecliningPermanently;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000F74E File Offset: 0x0000D94E
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x17000157 RID: 343
			// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000F75B File Offset: 0x0000D95B
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._transformManyBlock);
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000F768 File Offset: 0x0000D968
			public TargetRegistry<TOutput> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000F775 File Offset: 0x0000D975
			public ITargetBlock<TOutput> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000180 RID: 384
			private readonly TransformManyBlock<TInput, TOutput> _transformManyBlock;

			// Token: 0x04000181 RID: 385
			private readonly TargetCore<TInput>.DebuggingInformation _targetDebuggingInformation;

			// Token: 0x04000182 RID: 386
			private readonly SourceCore<TOutput>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
