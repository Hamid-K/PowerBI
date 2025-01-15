using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(JoinBlock<, , >.DebugView))]
	public sealed class JoinBlock<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3> : IReceivableSourceBlock<Tuple<T1, T2, T3>>, ISourceBlock<Tuple<T1, T2, T3>>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x0600014F RID: 335 RVA: 0x00005A63 File Offset: 0x00003C63
		public JoinBlock()
			: this(GroupingDataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005A70 File Offset: 0x00003C70
		public JoinBlock(GroupingDataflowBlockOptions dataflowBlockOptions)
		{
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<ISourceBlock<Tuple<T1, T2, T3>>, int> action = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = delegate(ISourceBlock<Tuple<T1, T2, T3>> owningSource, int count)
				{
					((JoinBlock<T1, T2, T3>)owningSource)._sharedResources.OnItemsRemoved(count);
				};
			}
			this._source = new SourceCore<Tuple<T1, T2, T3>>(this, dataflowBlockOptions, delegate(ISourceBlock<Tuple<T1, T2, T3>> owningSource)
			{
				((JoinBlock<T1, T2, T3>)owningSource)._sharedResources.CompleteEachTarget();
			}, action, null);
			JoinBlockTargetBase[] array = new JoinBlockTargetBase[3];
			this._sharedResources = new JoinBlockTargetSharedResources(this, array, delegate
			{
				this._source.AddMessage(Tuple.Create<T1, T2, T3>(this._target1.GetOneMessage(), this._target2.GetOneMessage(), this._target3.GetOneMessage()));
			}, delegate(Exception exception)
			{
				Volatile.Write(ref this._sharedResources._hasExceptions, true);
				this._source.AddException(exception);
			}, dataflowBlockOptions);
			array[0] = (this._target1 = new JoinBlockTarget<T1>(this._sharedResources));
			array[1] = (this._target2 = new JoinBlockTarget<T2>(this._sharedResources));
			array[2] = (this._target3 = new JoinBlockTarget<T3>(this._sharedResources));
			Task.Factory.ContinueWhenAll(new Task[]
			{
				this._target1.CompletionTaskInternal,
				this._target2.CompletionTaskInternal,
				this._target3.CompletionTaskInternal
			}, delegate(Task[] _)
			{
				this._source.Complete();
			}, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (JoinBlock<T1, T2, T3>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((JoinBlock<T1, T2, T3>)state)._sharedResources.CompleteEachTarget();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005C52 File Offset: 0x00003E52
		public IDisposable LinkTo(ITargetBlock<Tuple<T1, T2, T3>> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005C61 File Offset: 0x00003E61
		public bool TryReceive([Nullable(new byte[] { 2, 1, 1, 1, 1 })] Predicate<Tuple<T1, T2, T3>> filter, [Nullable(new byte[] { 2, 1, 1, 1 })] [NotNullWhen(true)] out Tuple<T1, T2, T3> item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005C70 File Offset: 0x00003E70
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1, 1, 1, 1 })] [NotNullWhen(true)] out IList<Tuple<T1, T2, T3>> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005C7E File Offset: 0x00003E7E
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00005C8B File Offset: 0x00003E8B
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005C98 File Offset: 0x00003E98
		public void Complete()
		{
			this._target1.CompleteCore(null, false, false);
			this._target2.CompleteCore(null, false, false);
			this._target3.CompleteCore(null, false, false);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005CC4 File Offset: 0x00003EC4
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			object incomingLock = this._sharedResources.IncomingLock;
			lock (incomingLock)
			{
				if (!this._sharedResources._decliningPermanently)
				{
					this._sharedResources._exceptionAction(exception);
				}
			}
			this.Complete();
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005D38 File Offset: 0x00003F38
		public ITargetBlock<T1> Target1
		{
			get
			{
				return this._target1;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00005D40 File Offset: 0x00003F40
		public ITargetBlock<T2> Target2
		{
			get
			{
				return this._target2;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005D48 File Offset: 0x00003F48
		public ITargetBlock<T3> Target3
		{
			get
			{
				return this._target3;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005D50 File Offset: 0x00003F50
		Tuple<T1, T2, T3> ISourceBlock<Tuple<T1, T2, T3>>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2, T3>> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005D60 File Offset: 0x00003F60
		bool ISourceBlock<Tuple<T1, T2, T3>>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2, T3>> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005D6F File Offset: 0x00003F6F
		void ISourceBlock<Tuple<T1, T2, T3>>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2, T3>> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00005D7E File Offset: 0x00003F7E
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005D90 File Offset: 0x00003F90
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005DA3 File Offset: 0x00003FA3
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0} OutputCount={1}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.OutputCountForDebugger);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00005DCB File Offset: 0x00003FCB
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000050 RID: 80
		private readonly JoinBlockTargetSharedResources _sharedResources;

		// Token: 0x04000051 RID: 81
		private readonly SourceCore<Tuple<T1, T2, T3>> _source;

		// Token: 0x04000052 RID: 82
		private readonly JoinBlockTarget<T1> _target1;

		// Token: 0x04000053 RID: 83
		private readonly JoinBlockTarget<T2> _target2;

		// Token: 0x04000054 RID: 84
		private readonly JoinBlockTarget<T3> _target3;

		// Token: 0x0200006E RID: 110
		private sealed class DebugView
		{
			// Token: 0x060003C0 RID: 960 RVA: 0x0000F3AE File Offset: 0x0000D5AE
			public DebugView(JoinBlock<T1, T2, T3> joinBlock)
			{
				this._joinBlock = joinBlock;
				this._sourceDebuggingInformation = joinBlock._source.GetDebuggingInformation();
			}

			// Token: 0x17000137 RID: 311
			// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000F3CE File Offset: 0x0000D5CE
			public IEnumerable<Tuple<T1, T2, T3>> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x17000138 RID: 312
			// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000F3DB File Offset: 0x0000D5DB
			public long JoinsCreated
			{
				get
				{
					return this._joinBlock._sharedResources._joinsCreated;
				}
			}

			// Token: 0x17000139 RID: 313
			// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000F3ED File Offset: 0x0000D5ED
			public Task TaskForInputProcessing
			{
				get
				{
					return this._joinBlock._sharedResources._taskForInputProcessing;
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000F3FF File Offset: 0x0000D5FF
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000F40C File Offset: 0x0000D60C
			public GroupingDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return (GroupingDataflowBlockOptions)this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000F41E File Offset: 0x0000D61E
			public bool IsDecliningPermanently
			{
				get
				{
					return this._joinBlock._sharedResources._decliningPermanently;
				}
			}

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000F430 File Offset: 0x0000D630
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060003C8 RID: 968 RVA: 0x0000F43D File Offset: 0x0000D63D
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._joinBlock);
				}
			}

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000F44A File Offset: 0x0000D64A
			public ITargetBlock<T1> Target1
			{
				get
				{
					return this._joinBlock._target1;
				}
			}

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060003CA RID: 970 RVA: 0x0000F457 File Offset: 0x0000D657
			public ITargetBlock<T2> Target2
			{
				get
				{
					return this._joinBlock._target2;
				}
			}

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060003CB RID: 971 RVA: 0x0000F464 File Offset: 0x0000D664
			public ITargetBlock<T3> Target3
			{
				get
				{
					return this._joinBlock._target3;
				}
			}

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060003CC RID: 972 RVA: 0x0000F471 File Offset: 0x0000D671
			public TargetRegistry<Tuple<T1, T2, T3>> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x17000143 RID: 323
			// (get) Token: 0x060003CD RID: 973 RVA: 0x0000F47E File Offset: 0x0000D67E
			public ITargetBlock<Tuple<T1, T2, T3>> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x0400016B RID: 363
			private readonly JoinBlock<T1, T2, T3> _joinBlock;

			// Token: 0x0400016C RID: 364
			private readonly SourceCore<Tuple<T1, T2, T3>>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
