using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(JoinBlock<, >.DebugView))]
	public sealed class JoinBlock<[Nullable(2)] T1, [Nullable(2)] T2> : IReceivableSourceBlock<Tuple<T1, T2>>, ISourceBlock<Tuple<T1, T2>>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x0600013A RID: 314 RVA: 0x000056DB File Offset: 0x000038DB
		public JoinBlock()
			: this(GroupingDataflowBlockOptions.Default)
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000056E8 File Offset: 0x000038E8
		public JoinBlock(GroupingDataflowBlockOptions dataflowBlockOptions)
		{
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<ISourceBlock<Tuple<T1, T2>>, int> action = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = delegate(ISourceBlock<Tuple<T1, T2>> owningSource, int count)
				{
					((JoinBlock<T1, T2>)owningSource)._sharedResources.OnItemsRemoved(count);
				};
			}
			this._source = new SourceCore<Tuple<T1, T2>>(this, dataflowBlockOptions, delegate(ISourceBlock<Tuple<T1, T2>> owningSource)
			{
				((JoinBlock<T1, T2>)owningSource)._sharedResources.CompleteEachTarget();
			}, action, null);
			JoinBlockTargetBase[] array = new JoinBlockTargetBase[2];
			this._sharedResources = new JoinBlockTargetSharedResources(this, array, delegate
			{
				this._source.AddMessage(Tuple.Create<T1, T2>(this._target1.GetOneMessage(), this._target2.GetOneMessage()));
			}, delegate(Exception exception)
			{
				Volatile.Write(ref this._sharedResources._hasExceptions, true);
				this._source.AddException(exception);
			}, dataflowBlockOptions);
			array[0] = (this._target1 = new JoinBlockTarget<T1>(this._sharedResources));
			array[1] = (this._target2 = new JoinBlockTarget<T2>(this._sharedResources));
			Task.Factory.ContinueWhenAll(new Task[]
			{
				this._target1.CompletionTaskInternal,
				this._target2.CompletionTaskInternal
			}, delegate(Task[] _)
			{
				this._source.Complete();
			}, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (JoinBlock<T1, T2>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((JoinBlock<T1, T2>)state)._sharedResources.CompleteEachTarget();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000058A3 File Offset: 0x00003AA3
		public IDisposable LinkTo(ITargetBlock<Tuple<T1, T2>> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000058B2 File Offset: 0x00003AB2
		public bool TryReceive([Nullable(new byte[] { 2, 1, 1, 1 })] Predicate<Tuple<T1, T2>> filter, [Nullable(new byte[] { 2, 1, 1 })] [NotNullWhen(true)] out Tuple<T1, T2> item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000058C1 File Offset: 0x00003AC1
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1, 1, 1 })] [NotNullWhen(true)] out IList<Tuple<T1, T2>> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000058CF File Offset: 0x00003ACF
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000058DC File Offset: 0x00003ADC
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000058E9 File Offset: 0x00003AE9
		public void Complete()
		{
			this._target1.CompleteCore(null, false, false);
			this._target2.CompleteCore(null, false, false);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005908 File Offset: 0x00003B08
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000597C File Offset: 0x00003B7C
		public ITargetBlock<T1> Target1
		{
			get
			{
				return this._target1;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00005984 File Offset: 0x00003B84
		public ITargetBlock<T2> Target2
		{
			get
			{
				return this._target2;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000598C File Offset: 0x00003B8C
		Tuple<T1, T2> ISourceBlock<Tuple<T1, T2>>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2>> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000599C File Offset: 0x00003B9C
		bool ISourceBlock<Tuple<T1, T2>>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2>> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000059AB File Offset: 0x00003BAB
		void ISourceBlock<Tuple<T1, T2>>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<Tuple<T1, T2>> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000059BA File Offset: 0x00003BBA
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000059CC File Offset: 0x00003BCC
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000059DF File Offset: 0x00003BDF
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, OutputCount={1}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.OutputCountForDebugger);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005A07 File Offset: 0x00003C07
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly JoinBlockTargetSharedResources _sharedResources;

		// Token: 0x0400004D RID: 77
		private readonly SourceCore<Tuple<T1, T2>> _source;

		// Token: 0x0400004E RID: 78
		private readonly JoinBlockTarget<T1> _target1;

		// Token: 0x0400004F RID: 79
		private readonly JoinBlockTarget<T2> _target2;

		// Token: 0x0200006C RID: 108
		private sealed class DebugView
		{
			// Token: 0x060003AD RID: 941 RVA: 0x0000F273 File Offset: 0x0000D473
			public DebugView(JoinBlock<T1, T2> joinBlock)
			{
				this._joinBlock = joinBlock;
				this._sourceDebuggingInformation = joinBlock._source.GetDebuggingInformation();
			}

			// Token: 0x1700012B RID: 299
			// (get) Token: 0x060003AE RID: 942 RVA: 0x0000F293 File Offset: 0x0000D493
			public IEnumerable<Tuple<T1, T2>> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x1700012C RID: 300
			// (get) Token: 0x060003AF RID: 943 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
			public long JoinsCreated
			{
				get
				{
					return this._joinBlock._sharedResources._joinsCreated;
				}
			}

			// Token: 0x1700012D RID: 301
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000F2B2 File Offset: 0x0000D4B2
			public Task TaskForInputProcessing
			{
				get
				{
					return this._joinBlock._sharedResources._taskForInputProcessing;
				}
			}

			// Token: 0x1700012E RID: 302
			// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x1700012F RID: 303
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000F2D1 File Offset: 0x0000D4D1
			public GroupingDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return (GroupingDataflowBlockOptions)this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x17000130 RID: 304
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000F2E3 File Offset: 0x0000D4E3
			public bool IsDecliningPermanently
			{
				get
				{
					return this._joinBlock._sharedResources._decliningPermanently;
				}
			}

			// Token: 0x17000131 RID: 305
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000F2F5 File Offset: 0x0000D4F5
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x17000132 RID: 306
			// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000F302 File Offset: 0x0000D502
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._joinBlock);
				}
			}

			// Token: 0x17000133 RID: 307
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000F30F File Offset: 0x0000D50F
			public ITargetBlock<T1> Target1
			{
				get
				{
					return this._joinBlock._target1;
				}
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000F31C File Offset: 0x0000D51C
			public ITargetBlock<T2> Target2
			{
				get
				{
					return this._joinBlock._target2;
				}
			}

			// Token: 0x17000135 RID: 309
			// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000F329 File Offset: 0x0000D529
			public TargetRegistry<Tuple<T1, T2>> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x17000136 RID: 310
			// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000F336 File Offset: 0x0000D536
			public ITargetBlock<Tuple<T1, T2>> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000164 RID: 356
			private readonly JoinBlock<T1, T2> _joinBlock;

			// Token: 0x04000165 RID: 357
			private readonly SourceCore<Tuple<T1, T2>>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
