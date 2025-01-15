using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000029 RID: 41
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BatchedJoinBlock<, , >.DebugView))]
	public sealed class BatchedJoinBlock<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3> : IReceivableSourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>>, ISourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004427 File Offset: 0x00002627
		public BatchedJoinBlock(int batchSize)
			: this(batchSize, GroupingDataflowBlockOptions.Default)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004438 File Offset: 0x00002638
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
			if (!dataflowBlockOptions.Greedy || dataflowBlockOptions.BoundedCapacity != -1)
			{
				throw new ArgumentException(SR.Argument_NonGreedyNotSupported, "dataflowBlockOptions");
			}
			this._batchSize = batchSize;
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			this._source = new SourceCore<Tuple<IList<T1>, IList<T2>, IList<T3>>>(this, dataflowBlockOptions, delegate(ISourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> owningSource)
			{
				((BatchedJoinBlock<T1, T2, T3>)owningSource).CompleteEachTarget();
			}, null, null);
			Action createBatchAction = delegate
			{
				if (this._target1.Count > 0 || this._target2.Count > 0 || this._target3.Count > 0)
				{
					this._source.AddMessage(Tuple.Create<IList<T1>, IList<T2>, IList<T3>>(this._target1.GetAndEmptyMessages(), this._target2.GetAndEmptyMessages(), this._target3.GetAndEmptyMessages()));
				}
			};
			this._sharedResources = new BatchedJoinBlockTargetSharedResources(batchSize, dataflowBlockOptions, createBatchAction, delegate
			{
				createBatchAction();
				this._source.Complete();
			}, new Action<Exception>(this._source.AddException), new Action(this.Complete));
			this._target1 = new BatchedJoinBlockTarget<T1>(this._sharedResources);
			this._target2 = new BatchedJoinBlockTarget<T2>(this._sharedResources);
			this._target3 = new BatchedJoinBlockTarget<T3>(this._sharedResources);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (BatchedJoinBlock<T1, T2, T3>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((BatchedJoinBlock<T1, T2, T3>)state).CompleteEachTarget();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000045ED File Offset: 0x000027ED
		public int BatchSize
		{
			get
			{
				return this._batchSize;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000045F5 File Offset: 0x000027F5
		public ITargetBlock<T1> Target1
		{
			get
			{
				return this._target1;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000045FD File Offset: 0x000027FD
		public ITargetBlock<T2> Target2
		{
			get
			{
				return this._target2;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004605 File Offset: 0x00002805
		public ITargetBlock<T3> Target3
		{
			get
			{
				return this._target3;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000460D File Offset: 0x0000280D
		public IDisposable LinkTo(ITargetBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000461C File Offset: 0x0000281C
		public bool TryReceive([Nullable(new byte[] { 2, 1, 1, 1, 1, 1, 1, 1 })] Predicate<Tuple<IList<T1>, IList<T2>, IList<T3>>> filter, [Nullable(new byte[] { 2, 1, 1, 1, 1, 1, 1 })] [NotNullWhen(true)] out Tuple<IList<T1>, IList<T2>, IList<T3>> item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000462B File Offset: 0x0000282B
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1, 1, 1, 1, 1, 1, 1 })] [NotNullWhen(true)] out IList<Tuple<IList<T1>, IList<T2>, IList<T3>>> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004639 File Offset: 0x00002839
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004646 File Offset: 0x00002846
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004653 File Offset: 0x00002853
		public void Complete()
		{
			this._target1.Complete();
			this._target2.Complete();
			this._target3.Complete();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004678 File Offset: 0x00002878
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

		// Token: 0x060000FF RID: 255 RVA: 0x000046E4 File Offset: 0x000028E4
		Tuple<IList<T1>, IList<T2>, IList<T3>> ISourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000046F4 File Offset: 0x000028F4
		bool ISourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004703 File Offset: 0x00002903
		void ISourceBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004712 File Offset: 0x00002912
		private void CompleteEachTarget()
		{
			this._target1.Complete();
			this._target2.Complete();
			this._target3.Complete();
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00004735 File Offset: 0x00002935
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004747 File Offset: 0x00002947
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000475A File Offset: 0x0000295A
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, BatchSize={1}, OutputCount={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.BatchSize, this.OutputCountForDebugger);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000478D File Offset: 0x0000298D
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x0400003E RID: 62
		private readonly int _batchSize;

		// Token: 0x0400003F RID: 63
		private readonly BatchedJoinBlockTargetSharedResources _sharedResources;

		// Token: 0x04000040 RID: 64
		private readonly BatchedJoinBlockTarget<T1> _target1;

		// Token: 0x04000041 RID: 65
		private readonly BatchedJoinBlockTarget<T2> _target2;

		// Token: 0x04000042 RID: 66
		private readonly BatchedJoinBlockTarget<T3> _target3;

		// Token: 0x04000043 RID: 67
		private readonly SourceCore<Tuple<IList<T1>, IList<T2>, IList<T3>>> _source;

		// Token: 0x02000064 RID: 100
		private sealed class DebugView
		{
			// Token: 0x06000359 RID: 857 RVA: 0x0000E1AD File Offset: 0x0000C3AD
			public DebugView(BatchedJoinBlock<T1, T2, T3> batchedJoinBlock)
			{
				this._sourceDebuggingInformation = batchedJoinBlock._source.GetDebuggingInformation();
				this._batchedJoinBlock = batchedJoinBlock;
			}

			// Token: 0x17000104 RID: 260
			// (get) Token: 0x0600035A RID: 858 RVA: 0x0000E1CD File Offset: 0x0000C3CD
			public IEnumerable<Tuple<IList<T1>, IList<T2>, IList<T3>>> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x17000105 RID: 261
			// (get) Token: 0x0600035B RID: 859 RVA: 0x0000E1DA File Offset: 0x0000C3DA
			public long BatchesCreated
			{
				get
				{
					return this._batchedJoinBlock._sharedResources._batchesCreated;
				}
			}

			// Token: 0x17000106 RID: 262
			// (get) Token: 0x0600035C RID: 860 RVA: 0x0000E1EC File Offset: 0x0000C3EC
			public int RemainingItemsForBatch
			{
				get
				{
					return this._batchedJoinBlock._sharedResources._remainingItemsInBatch;
				}
			}

			// Token: 0x17000107 RID: 263
			// (get) Token: 0x0600035D RID: 861 RVA: 0x0000E1FE File Offset: 0x0000C3FE
			public int BatchSize
			{
				get
				{
					return this._batchedJoinBlock._batchSize;
				}
			}

			// Token: 0x17000108 RID: 264
			// (get) Token: 0x0600035E RID: 862 RVA: 0x0000E20B File Offset: 0x0000C40B
			public ITargetBlock<T1> Target1
			{
				get
				{
					return this._batchedJoinBlock._target1;
				}
			}

			// Token: 0x17000109 RID: 265
			// (get) Token: 0x0600035F RID: 863 RVA: 0x0000E218 File Offset: 0x0000C418
			public ITargetBlock<T2> Target2
			{
				get
				{
					return this._batchedJoinBlock._target2;
				}
			}

			// Token: 0x1700010A RID: 266
			// (get) Token: 0x06000360 RID: 864 RVA: 0x0000E225 File Offset: 0x0000C425
			public ITargetBlock<T3> Target3
			{
				get
				{
					return this._batchedJoinBlock._target3;
				}
			}

			// Token: 0x1700010B RID: 267
			// (get) Token: 0x06000361 RID: 865 RVA: 0x0000E232 File Offset: 0x0000C432
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x1700010C RID: 268
			// (get) Token: 0x06000362 RID: 866 RVA: 0x0000E23F File Offset: 0x0000C43F
			public GroupingDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return (GroupingDataflowBlockOptions)this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x1700010D RID: 269
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000E251 File Offset: 0x0000C451
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x1700010E RID: 270
			// (get) Token: 0x06000364 RID: 868 RVA: 0x0000E25E File Offset: 0x0000C45E
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._batchedJoinBlock);
				}
			}

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x06000365 RID: 869 RVA: 0x0000E26B File Offset: 0x0000C46B
			public TargetRegistry<Tuple<IList<T1>, IList<T2>, IList<T3>>> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x06000366 RID: 870 RVA: 0x0000E278 File Offset: 0x0000C478
			public ITargetBlock<Tuple<IList<T1>, IList<T2>, IList<T3>>> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x0400013C RID: 316
			private readonly BatchedJoinBlock<T1, T2, T3> _batchedJoinBlock;

			// Token: 0x0400013D RID: 317
			private readonly SourceCore<Tuple<IList<T1>, IList<T2>, IList<T3>>>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
