using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BatchedJoinBlock<, >.DebugView))]
	public sealed class BatchedJoinBlock<[Nullable(2)] T1, [Nullable(2)] T2> : IReceivableSourceBlock<Tuple<IList<T1>, IList<T2>>>, ISourceBlock<Tuple<IList<T1>, IList<T2>>>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x060000DD RID: 221 RVA: 0x00004088 File Offset: 0x00002288
		public BatchedJoinBlock(int batchSize)
			: this(batchSize, GroupingDataflowBlockOptions.Default)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004098 File Offset: 0x00002298
		public BatchedJoinBlock(int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions)
		{
			if (batchSize < 1)
			{
				throw new ArgumentOutOfRangeException("batchSize", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			if (!dataflowBlockOptions.Greedy)
			{
				throw new ArgumentException(SR.Argument_NonGreedyNotSupported, "dataflowBlockOptions");
			}
			if (dataflowBlockOptions.BoundedCapacity != -1)
			{
				throw new ArgumentException(SR.Argument_BoundedCapacityNotSupported, "dataflowBlockOptions");
			}
			this._batchSize = batchSize;
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			this._source = new SourceCore<Tuple<IList<T1>, IList<T2>>>(this, dataflowBlockOptions, delegate(ISourceBlock<Tuple<IList<T1>, IList<T2>>> owningSource)
			{
				((BatchedJoinBlock<T1, T2>)owningSource).CompleteEachTarget();
			}, null, null);
			Action createBatchAction = delegate
			{
				if (this._target1.Count > 0 || this._target2.Count > 0)
				{
					this._source.AddMessage(Tuple.Create<IList<T1>, IList<T2>>(this._target1.GetAndEmptyMessages(), this._target2.GetAndEmptyMessages()));
				}
			};
			this._sharedResources = new BatchedJoinBlockTargetSharedResources(batchSize, dataflowBlockOptions, createBatchAction, delegate
			{
				createBatchAction();
				this._source.Complete();
			}, new Action<Exception>(this._source.AddException), new Action(this.Complete));
			this._target1 = new BatchedJoinBlockTarget<T1>(this._sharedResources);
			this._target2 = new BatchedJoinBlockTarget<T2>(this._sharedResources);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (BatchedJoinBlock<T1, T2>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((BatchedJoinBlock<T1, T2>)state).CompleteEachTarget();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000424C File Offset: 0x0000244C
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004254 File Offset: 0x00002454
		public ITargetBlock<T1> Target1
		{
			get
			{
				return this._target1;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000425C File Offset: 0x0000245C
		public ITargetBlock<T2> Target2
		{
			get
			{
				return this._target2;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004264 File Offset: 0x00002464
		public IDisposable LinkTo(ITargetBlock<Tuple<IList<T1>, IList<T2>>> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004273 File Offset: 0x00002473
		public bool TryReceive([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] Predicate<Tuple<IList<T1>, IList<T2>>> filter, [Nullable(new byte[] { 2, 1, 1, 1, 1 })] [NotNullWhen(true)] out Tuple<IList<T1>, IList<T2>> item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004282 File Offset: 0x00002482
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1, 1, 1, 1, 1 })] [NotNullWhen(true)] out IList<Tuple<IList<T1>, IList<T2>>> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004290 File Offset: 0x00002490
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000429D File Offset: 0x0000249D
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000042AA File Offset: 0x000024AA
		public void Complete()
		{
			this._target1.Complete();
			this._target2.Complete();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000042C4 File Offset: 0x000024C4
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			object incomingLock = this._sharedResources._incomingLock;
			lock (incomingLock)
			{
				if (!this._sharedResources._decliningPermanently)
				{
					this._source.AddException(exception);
				}
			}
			this.Complete();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004330 File Offset: 0x00002530
		Tuple<IList<T1>, IList<T2>> ISourceBlock<Tuple<IList<T1>, IList<T2>>>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>>> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004340 File Offset: 0x00002540
		bool ISourceBlock<Tuple<IList<T1>, IList<T2>>>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>>> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000434F File Offset: 0x0000254F
		void ISourceBlock<Tuple<IList<T1>, IList<T2>>>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>>> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000435E File Offset: 0x0000255E
		private void CompleteEachTarget()
		{
			this._target1.Complete();
			this._target2.Complete();
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004376 File Offset: 0x00002576
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004388 File Offset: 0x00002588
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000439B File Offset: 0x0000259B
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, BatchSize={1}, OutputCount={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.BatchSize, this.OutputCountForDebugger);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x000043CE File Offset: 0x000025CE
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000039 RID: 57
		private readonly int _batchSize;

		// Token: 0x0400003A RID: 58
		private readonly BatchedJoinBlockTargetSharedResources _sharedResources;

		// Token: 0x0400003B RID: 59
		private readonly BatchedJoinBlockTarget<T1> _target1;

		// Token: 0x0400003C RID: 60
		private readonly BatchedJoinBlockTarget<T2> _target2;

		// Token: 0x0400003D RID: 61
		private readonly SourceCore<Tuple<IList<T1>, IList<T2>>> _source;

		// Token: 0x02000061 RID: 97
		private sealed class DebugView
		{
			// Token: 0x06000345 RID: 837 RVA: 0x0000E06D File Offset: 0x0000C26D
			public DebugView(BatchedJoinBlock<T1, T2> batchedJoinBlock)
			{
				this._batchedJoinBlock = batchedJoinBlock;
				this._sourceDebuggingInformation = batchedJoinBlock._source.GetDebuggingInformation();
			}

			// Token: 0x170000F8 RID: 248
			// (get) Token: 0x06000346 RID: 838 RVA: 0x0000E08D File Offset: 0x0000C28D
			public IEnumerable<Tuple<IList<T1>, IList<T2>>> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x170000F9 RID: 249
			// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E09A File Offset: 0x0000C29A
			public long BatchesCreated
			{
				get
				{
					return this._batchedJoinBlock._sharedResources._batchesCreated;
				}
			}

			// Token: 0x170000FA RID: 250
			// (get) Token: 0x06000348 RID: 840 RVA: 0x0000E0AC File Offset: 0x0000C2AC
			public int RemainingItemsForBatch
			{
				get
				{
					return this._batchedJoinBlock._sharedResources._remainingItemsInBatch;
				}
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E0BE File Offset: 0x0000C2BE
			public int BatchSize
			{
				get
				{
					return this._batchedJoinBlock._batchSize;
				}
			}

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600034A RID: 842 RVA: 0x0000E0CB File Offset: 0x0000C2CB
			public ITargetBlock<T1> Target1
			{
				get
				{
					return this._batchedJoinBlock._target1;
				}
			}

			// Token: 0x170000FD RID: 253
			// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
			public ITargetBlock<T2> Target2
			{
				get
				{
					return this._batchedJoinBlock._target2;
				}
			}

			// Token: 0x170000FE RID: 254
			// (get) Token: 0x0600034C RID: 844 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x170000FF RID: 255
			// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
			public GroupingDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return (GroupingDataflowBlockOptions)this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600034E RID: 846 RVA: 0x0000E104 File Offset: 0x0000C304
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x0600034F RID: 847 RVA: 0x0000E111 File Offset: 0x0000C311
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._batchedJoinBlock);
				}
			}

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x06000350 RID: 848 RVA: 0x0000E11E File Offset: 0x0000C31E
			public TargetRegistry<Tuple<IList<T1>, IList<T2>>> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x17000103 RID: 259
			// (get) Token: 0x06000351 RID: 849 RVA: 0x0000E12B File Offset: 0x0000C32B
			public ITargetBlock<Tuple<IList<T1>, IList<T2>>> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000134 RID: 308
			private readonly BatchedJoinBlock<T1, T2> _batchedJoinBlock;

			// Token: 0x04000135 RID: 309
			private readonly SourceCore<Tuple<IList<T1>, IList<T2>>>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
