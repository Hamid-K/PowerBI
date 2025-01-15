using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(TransformBlock<, >.DebugView))]
	public sealed class TransformBlock<[Nullable(2)] TInput, [Nullable(2)] TOutput> : IPropagatorBlock<TInput, TOutput>, ITargetBlock<TInput>, IDataflowBlock, ISourceBlock<TOutput>, IReceivableSourceBlock<TOutput>, IDebuggerDisplay
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005E32 File Offset: 0x00004032
		private object ParallelSourceLock
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00005E3A File Offset: 0x0000403A
		public TransformBlock(Func<TInput, TOutput> transform)
			: this(transform, null, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005E49 File Offset: 0x00004049
		public TransformBlock(Func<TInput, TOutput> transform, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(transform, null, dataflowBlockOptions)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005E54 File Offset: 0x00004054
		public TransformBlock(Func<TInput, Task<TOutput>> transform)
			: this(null, transform, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005E63 File Offset: 0x00004063
		public TransformBlock(Func<TInput, Task<TOutput>> transform, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(null, transform, dataflowBlockOptions)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005E70 File Offset: 0x00004070
		private TransformBlock(Func<TInput, TOutput> transformSync, Func<TInput, Task<TOutput>> transformAsync, ExecutionDataflowBlockOptions dataflowBlockOptions)
		{
			TransformBlock<TInput, TOutput> <>4__this = this;
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
					((TransformBlock<TInput, TOutput>)owningSource)._target.ChangeBoundingCount(-count);
				};
			}
			this._source = new SourceCore<TOutput>(this, dataflowBlockOptions, delegate(ISourceBlock<TOutput> owningSource)
			{
				((TransformBlock<TInput, TOutput>)owningSource)._target.Complete(null, true, false, false, false);
			}, action, null);
			if (dataflowBlockOptions.SupportsParallelExecution && dataflowBlockOptions.EnsureOrdered)
			{
				this._reorderingBuffer = new ReorderingBuffer<TOutput>(this, delegate(object owningSource, TOutput message)
				{
					((TransformBlock<TInput, TOutput>)owningSource)._source.AddMessage(message);
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
				IDataflowBlock dataflowBlock = (TransformBlock<TInput, TOutput>)state;
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

		// Token: 0x0600016B RID: 363 RVA: 0x00006088 File Offset: 0x00004288
		private void ProcessMessage(Func<TInput, TOutput> transform, KeyValuePair<TInput, long> messageWithId)
		{
			TOutput toutput = default(TOutput);
			bool flag = false;
			try
			{
				toutput = transform(messageWithId.Key);
				flag = true;
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
					this._target.ChangeBoundingCount(-1);
				}
				if (this._reorderingBuffer == null)
				{
					if (!flag)
					{
						goto IL_00A6;
					}
					if (this._target.DataflowBlockOptions.MaxDegreeOfParallelism == 1)
					{
						this._source.AddMessage(toutput);
						goto IL_00A6;
					}
					object parallelSourceLock = this.ParallelSourceLock;
					lock (parallelSourceLock)
					{
						this._source.AddMessage(toutput);
						goto IL_00A6;
					}
				}
				this._reorderingBuffer.AddItem(messageWithId.Value, toutput, flag);
				IL_00A6:;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006164 File Offset: 0x00004364
		private void ProcessMessageWithTask(Func<TInput, Task<TOutput>> transform, KeyValuePair<TInput, long> messageWithId)
		{
			Task<TOutput> task = null;
			Exception ex = null;
			try
			{
				task = transform(messageWithId.Key);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (task == null)
			{
				if (ex != null && !Common.IsCooperativeCancellation(ex))
				{
					Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, messageWithId.Key, false);
					this._target.Complete(ex, true, true, false, false);
				}
				if (this._reorderingBuffer != null)
				{
					this._reorderingBuffer.IgnoreItem(messageWithId.Value);
				}
				this._target.SignalOneAsyncMessageCompleted(-1);
				return;
			}
			task.ContinueWith(delegate(Task<TOutput> completed, object state)
			{
				Tuple<TransformBlock<TInput, TOutput>, KeyValuePair<TInput, long>> tuple = (Tuple<TransformBlock<TInput, TOutput>, KeyValuePair<TInput, long>>)state;
				tuple.Item1.AsyncCompleteProcessMessageWithTask(completed, tuple.Item2);
			}, Tuple.Create<TransformBlock<TInput, TOutput>, KeyValuePair<TInput, long>>(this, messageWithId), CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.ExecuteSynchronously), TaskScheduler.Default);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006230 File Offset: 0x00004430
		private void AsyncCompleteProcessMessageWithTask(Task<TOutput> completed, KeyValuePair<TInput, long> messageWithId)
		{
			bool isBounded = this._target.IsBounded;
			bool flag = false;
			TOutput toutput = default(TOutput);
			TaskStatus status = completed.Status;
			if (status != TaskStatus.RanToCompletion)
			{
				if (status == TaskStatus.Faulted)
				{
					AggregateException exception = completed.Exception;
					Common.StoreDataflowMessageValueIntoExceptionData<TInput>(exception, messageWithId.Key, true);
					this._target.Complete(exception, true, true, true, false);
				}
			}
			else
			{
				toutput = completed.Result;
				flag = true;
			}
			if (!flag && isBounded)
			{
				this._target.ChangeBoundingCount(-1);
			}
			if (this._reorderingBuffer == null)
			{
				if (!flag)
				{
					goto IL_00DC;
				}
				if (this._target.DataflowBlockOptions.MaxDegreeOfParallelism == 1)
				{
					this._source.AddMessage(toutput);
					goto IL_00DC;
				}
				object parallelSourceLock = this.ParallelSourceLock;
				lock (parallelSourceLock)
				{
					this._source.AddMessage(toutput);
					goto IL_00DC;
				}
			}
			this._reorderingBuffer.AddItem(messageWithId.Value, toutput, flag);
			IL_00DC:
			this._target.SignalOneAsyncMessageCompleted();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006334 File Offset: 0x00004534
		public void Complete()
		{
			this._target.Complete(null, false, false, false, false);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006346 File Offset: 0x00004546
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._target.Complete(exception, true, false, false, false);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006366 File Offset: 0x00004566
		public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006375 File Offset: 0x00004575
		public bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006384 File Offset: 0x00004584
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IList<TOutput> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00006392 File Offset: 0x00004592
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000639F File Offset: 0x0000459F
		public int InputCount
		{
			get
			{
				return this._target.InputCount;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000063AC File Offset: 0x000045AC
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000063B9 File Offset: 0x000045B9
		DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			return this._target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000063CB File Offset: 0x000045CB
		TOutput ISourceBlock<TOutput>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000063DB File Offset: 0x000045DB
		bool ISourceBlock<TOutput>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000063EA File Offset: 0x000045EA
		void ISourceBlock<TOutput>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000063F9 File Offset: 0x000045F9
		private int InputCountForDebugger
		{
			get
			{
				return this._target.GetDebuggingInformation().InputCount;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000640B File Offset: 0x0000460B
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000641D File Offset: 0x0000461D
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00006430 File Offset: 0x00004630
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, InputCount={1}, OutputCount={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.InputCountForDebugger, this.OutputCountForDebugger);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00006463 File Offset: 0x00004663
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000055 RID: 85
		private readonly TargetCore<TInput> _target;

		// Token: 0x04000056 RID: 86
		private readonly ReorderingBuffer<TOutput> _reorderingBuffer;

		// Token: 0x04000057 RID: 87
		private readonly SourceCore<TOutput> _source;

		// Token: 0x02000070 RID: 112
		private sealed class DebugView
		{
			// Token: 0x060003D4 RID: 980 RVA: 0x0000F4F6 File Offset: 0x0000D6F6
			public DebugView(TransformBlock<TInput, TOutput> transformBlock)
			{
				this._transformBlock = transformBlock;
				this._targetDebuggingInformation = transformBlock._target.GetDebuggingInformation();
				this._sourceDebuggingInformation = transformBlock._source.GetDebuggingInformation();
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000F527 File Offset: 0x0000D727
			public IEnumerable<TInput> InputQueue
			{
				get
				{
					return this._targetDebuggingInformation.InputQueue;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000F534 File Offset: 0x0000D734
			public QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					return this._targetDebuggingInformation.PostponedMessages;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000F541 File Offset: 0x0000D741
			public IEnumerable<TOutput> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000F54E File Offset: 0x0000D74E
			public int CurrentDegreeOfParallelism
			{
				get
				{
					return this._targetDebuggingInformation.CurrentDegreeOfParallelism;
				}
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000F55B File Offset: 0x0000D75B
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0000F568 File Offset: 0x0000D768
			public ExecutionDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._targetDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x1700014A RID: 330
			// (get) Token: 0x060003DB RID: 987 RVA: 0x0000F575 File Offset: 0x0000D775
			public bool IsDecliningPermanently
			{
				get
				{
					return this._targetDebuggingInformation.IsDecliningPermanently;
				}
			}

			// Token: 0x1700014B RID: 331
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0000F582 File Offset: 0x0000D782
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x1700014C RID: 332
			// (get) Token: 0x060003DD RID: 989 RVA: 0x0000F58F File Offset: 0x0000D78F
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._transformBlock);
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000F59C File Offset: 0x0000D79C
			public TargetRegistry<TOutput> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x060003DF RID: 991 RVA: 0x0000F5A9 File Offset: 0x0000D7A9
			public ITargetBlock<TOutput> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000172 RID: 370
			private readonly TransformBlock<TInput, TOutput> _transformBlock;

			// Token: 0x04000173 RID: 371
			private readonly TargetCore<TInput>.DebuggingInformation _targetDebuggingInformation;

			// Token: 0x04000174 RID: 372
			private readonly SourceCore<TOutput>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
