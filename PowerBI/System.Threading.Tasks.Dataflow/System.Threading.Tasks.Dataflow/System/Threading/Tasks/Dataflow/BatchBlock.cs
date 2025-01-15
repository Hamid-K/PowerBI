using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000027 RID: 39
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BatchBlock<>.DebugView))]
	public sealed class BatchBlock<[Nullable(2)] T> : IPropagatorBlock<T, T[]>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T[]>, IReceivableSourceBlock<T[]>, IDebuggerDisplay
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00003D74 File Offset: 0x00001F74
		public BatchBlock(int batchSize)
			: this(batchSize, GroupingDataflowBlockOptions.Default)
		{
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003D84 File Offset: 0x00001F84
		public BatchBlock(int batchSize, GroupingDataflowBlockOptions dataflowBlockOptions)
		{
			if (batchSize < 1)
			{
				throw new ArgumentOutOfRangeException("batchSize", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			if (dataflowBlockOptions.BoundedCapacity > 0 && dataflowBlockOptions.BoundedCapacity < batchSize)
			{
				throw new ArgumentOutOfRangeException("batchSize", SR.ArgumentOutOfRange_BatchSizeMustBeNoGreaterThanBoundedCapacity);
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<ISourceBlock<T[]>, int> action = null;
			Func<ISourceBlock<T[]>, T[], IList<T[]>, int> func = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = delegate(ISourceBlock<T[]> owningSource, int count)
				{
					((BatchBlock<T>)owningSource)._target.OnItemsRemoved(count);
				};
				func = (ISourceBlock<T[]> owningSource, T[] singleOutputItem, IList<T[]> multipleOutputItems) => BatchBlock<T>.BatchBlockTargetCore.CountItems(singleOutputItem, multipleOutputItems);
			}
			this._source = new SourceCore<T[]>(this, dataflowBlockOptions, delegate(ISourceBlock<T[]> owningSource)
			{
				((BatchBlock<T>)owningSource)._target.Complete(null, true, false, false);
			}, action, func);
			this._target = new BatchBlock<T>.BatchBlockTargetCore(this, batchSize, delegate(T[] batch)
			{
				this._source.AddMessage(batch);
			}, dataflowBlockOptions);
			this._target.Completion.ContinueWith(delegate
			{
				this._source.Complete();
			}, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None), TaskScheduler.Default);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (BatchBlock<T>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((BatchBlock<T>.BatchBlockTargetCore)state).Complete(null, true, false, false);
			}, this._target);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003F3D File Offset: 0x0000213D
		public void Complete()
		{
			this._target.Complete(null, false, false, false);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003F4E File Offset: 0x0000214E
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this._target.Complete(exception, true, false, false);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003F6D File Offset: 0x0000216D
		public void TriggerBatch()
		{
			this._target.TriggerBatch();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003F7A File Offset: 0x0000217A
		public IDisposable LinkTo(ITargetBlock<T[]> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003F89 File Offset: 0x00002189
		public bool TryReceive([Nullable(new byte[] { 2, 1, 1 })] Predicate<T[]> filter, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out T[] item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003F98 File Offset: 0x00002198
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1, 1 })] [NotNullWhen(true)] out IList<T[]> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003FA6 File Offset: 0x000021A6
		public int OutputCount
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003FB3 File Offset: 0x000021B3
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003FC0 File Offset: 0x000021C0
		public int BatchSize
		{
			get
			{
				return this._target.BatchSize;
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003FCD File Offset: 0x000021CD
		DataflowMessageStatus ITargetBlock<T>.OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
		{
			return this._target.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003FDF File Offset: 0x000021DF
		T[] ISourceBlock<T[]>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T[]> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003FEF File Offset: 0x000021EF
		bool ISourceBlock<T[]>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T[]> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003FFE File Offset: 0x000021FE
		void ISourceBlock<T[]>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T[]> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000400D File Offset: 0x0000220D
		private int OutputCountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000401F File Offset: 0x0000221F
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004032 File Offset: 0x00002232
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, BatchSize={1}, OutputCount={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.BatchSize, this.OutputCountForDebugger);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004065 File Offset: 0x00002265
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000037 RID: 55
		private readonly BatchBlock<T>.BatchBlockTargetCore _target;

		// Token: 0x04000038 RID: 56
		private readonly SourceCore<T[]> _source;

		// Token: 0x0200005E RID: 94
		private sealed class DebugView
		{
			// Token: 0x06000316 RID: 790 RVA: 0x0000CDBE File Offset: 0x0000AFBE
			public DebugView(BatchBlock<T> batchBlock)
			{
				this._batchBlock = batchBlock;
				this._targetDebuggingInformation = batchBlock._target.GetDebuggingInformation();
				this._sourceDebuggingInformation = batchBlock._source.GetDebuggingInformation();
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x06000317 RID: 791 RVA: 0x0000CDEF File Offset: 0x0000AFEF
			public IEnumerable<T> InputQueue
			{
				get
				{
					return this._targetDebuggingInformation.InputQueue;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000318 RID: 792 RVA: 0x0000CDFC File Offset: 0x0000AFFC
			public IEnumerable<T[]> OutputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000319 RID: 793 RVA: 0x0000CE09 File Offset: 0x0000B009
			public long BatchesCompleted
			{
				get
				{
					return this._targetDebuggingInformation.NumberOfBatchesCompleted;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (get) Token: 0x0600031A RID: 794 RVA: 0x0000CE16 File Offset: 0x0000B016
			public Task TaskForInputProcessing
			{
				get
				{
					return this._targetDebuggingInformation.TaskForInputProcessing;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x0600031B RID: 795 RVA: 0x0000CE23 File Offset: 0x0000B023
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x0600031C RID: 796 RVA: 0x0000CE30 File Offset: 0x0000B030
			public GroupingDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._targetDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (get) Token: 0x0600031D RID: 797 RVA: 0x0000CE3D File Offset: 0x0000B03D
			public int BatchSize
			{
				get
				{
					return this._batchBlock.BatchSize;
				}
			}

			// Token: 0x170000EA RID: 234
			// (get) Token: 0x0600031E RID: 798 RVA: 0x0000CE4A File Offset: 0x0000B04A
			public bool IsDecliningPermanently
			{
				get
				{
					return this._targetDebuggingInformation.IsDecliningPermanently;
				}
			}

			// Token: 0x170000EB RID: 235
			// (get) Token: 0x0600031F RID: 799 RVA: 0x0000CE57 File Offset: 0x0000B057
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000320 RID: 800 RVA: 0x0000CE64 File Offset: 0x0000B064
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._batchBlock);
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000321 RID: 801 RVA: 0x0000CE71 File Offset: 0x0000B071
			public QueuedMap<ISourceBlock<T>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					return this._targetDebuggingInformation.PostponedMessages;
				}
			}

			// Token: 0x170000EE RID: 238
			// (get) Token: 0x06000322 RID: 802 RVA: 0x0000CE7E File Offset: 0x0000B07E
			public TargetRegistry<T[]> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x170000EF RID: 239
			// (get) Token: 0x06000323 RID: 803 RVA: 0x0000CE8B File Offset: 0x0000B08B
			public ITargetBlock<T[]> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000120 RID: 288
			private readonly BatchBlock<T> _batchBlock;

			// Token: 0x04000121 RID: 289
			private readonly BatchBlock<T>.BatchBlockTargetCore.DebuggingInformation _targetDebuggingInformation;

			// Token: 0x04000122 RID: 290
			private readonly SourceCore<T[]>.DebuggingInformation _sourceDebuggingInformation;
		}

		// Token: 0x0200005F RID: 95
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class BatchBlockTargetCore
		{
			// Token: 0x170000F0 RID: 240
			// (get) Token: 0x06000324 RID: 804 RVA: 0x0000CE98 File Offset: 0x0000B098
			private object IncomingLock
			{
				get
				{
					return this._completionTask;
				}
			}

			// Token: 0x06000325 RID: 805 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
			internal BatchBlockTargetCore(BatchBlock<T> owningBatch, int batchSize, Action<T[]> batchCompletedAction, GroupingDataflowBlockOptions dataflowBlockOptions)
			{
				this._owningBatch = owningBatch;
				this._batchSize = batchSize;
				this._batchCompletedAction = batchCompletedAction;
				this._dataflowBlockOptions = dataflowBlockOptions;
				bool flag = dataflowBlockOptions.BoundedCapacity > 0;
				if (!this._dataflowBlockOptions.Greedy || flag)
				{
					this._nonGreedyState = new BatchBlock<T>.BatchBlockTargetCore.NonGreedyState(batchSize);
				}
				if (flag)
				{
					this._boundingState = new BoundingState(dataflowBlockOptions.BoundedCapacity);
				}
			}

			// Token: 0x06000326 RID: 806 RVA: 0x0000CF24 File Offset: 0x0000B124
			internal void TriggerBatch()
			{
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (!this._decliningPermanently && !this._dataflowBlockOptions.CancellationToken.IsCancellationRequested)
					{
						if (this._nonGreedyState == null)
						{
							this.MakeBatchIfPossible(true);
						}
						else
						{
							this._nonGreedyState.AcceptFewerThanBatchSize = true;
							this.ProcessAsyncIfNecessary(false);
						}
					}
					this.CompleteBlockIfPossible();
				}
			}

			// Token: 0x06000327 RID: 807 RVA: 0x0000CFA8 File Offset: 0x0000B1A8
			internal DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
			{
				if (!messageHeader.IsValid)
				{
					throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
				}
				if (source == null && consumeToAccept)
				{
					throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
				}
				object incomingLock = this.IncomingLock;
				DataflowMessageStatus dataflowMessageStatus;
				lock (incomingLock)
				{
					if (this._decliningPermanently)
					{
						this.CompleteBlockIfPossible();
						dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
					}
					else if (this._dataflowBlockOptions.Greedy && (this._boundingState == null || (this._boundingState.CountIsLessThanBound && this._nonGreedyState.PostponedMessages.Count == 0 && this._nonGreedyState.TaskForInputProcessing == null)))
					{
						if (consumeToAccept)
						{
							bool flag2;
							messageValue = source.ConsumeMessage(messageHeader, this._owningBatch, out flag2);
							if (!flag2)
							{
								return DataflowMessageStatus.NotAvailable;
							}
						}
						this._messages.Enqueue(messageValue);
						if (this._boundingState != null)
						{
							this._boundingState.CurrentCount++;
						}
						if (!this._decliningPermanently && this._batchesCompleted + (long)(this._messages.Count / this._batchSize) >= this._dataflowBlockOptions.ActualMaxNumberOfGroups)
						{
							this._decliningPermanently = true;
						}
						this.MakeBatchIfPossible(false);
						this.CompleteBlockIfPossible();
						dataflowMessageStatus = DataflowMessageStatus.Accepted;
					}
					else if (source != null)
					{
						this._nonGreedyState.PostponedMessages.Push(source, messageHeader);
						if (!this._dataflowBlockOptions.Greedy)
						{
							this.ProcessAsyncIfNecessary(false);
						}
						dataflowMessageStatus = DataflowMessageStatus.Postponed;
					}
					else
					{
						dataflowMessageStatus = DataflowMessageStatus.Declined;
					}
				}
				return dataflowMessageStatus;
			}

			// Token: 0x06000328 RID: 808 RVA: 0x0000D140 File Offset: 0x0000B340
			internal void Complete(Exception exception, bool dropPendingMessages, bool releaseReservedMessages, bool revertProcessingState = false)
			{
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (exception != null && (!this._decliningPermanently || releaseReservedMessages))
					{
						this._owningBatch._source.AddException(exception);
					}
					if (dropPendingMessages)
					{
						this._messages.Clear();
					}
				}
				if (releaseReservedMessages)
				{
					try
					{
						this.ReleaseReservedMessages(false);
					}
					catch (Exception ex)
					{
						this._owningBatch._source.AddException(ex);
					}
				}
				object incomingLock2 = this.IncomingLock;
				lock (incomingLock2)
				{
					if (revertProcessingState)
					{
						this._nonGreedyState.TaskForInputProcessing = null;
					}
					this._decliningPermanently = true;
					this.CompleteBlockIfPossible();
				}
			}

			// Token: 0x170000F1 RID: 241
			// (get) Token: 0x06000329 RID: 809 RVA: 0x0000D220 File Offset: 0x0000B420
			internal Task Completion
			{
				get
				{
					return this._completionTask.Task;
				}
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x0600032A RID: 810 RVA: 0x0000D22D File Offset: 0x0000B42D
			internal int BatchSize
			{
				get
				{
					return this._batchSize;
				}
			}

			// Token: 0x170000F3 RID: 243
			// (get) Token: 0x0600032B RID: 811 RVA: 0x0000D238 File Offset: 0x0000B438
			private bool CanceledOrFaulted
			{
				get
				{
					return this._dataflowBlockOptions.CancellationToken.IsCancellationRequested || this._owningBatch._source.HasExceptions;
				}
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x0600032C RID: 812 RVA: 0x0000D26C File Offset: 0x0000B46C
			private int BoundedCapacityAvailable
			{
				get
				{
					if (this._boundingState == null)
					{
						return this._batchSize;
					}
					return this._dataflowBlockOptions.BoundedCapacity - this._boundingState.CurrentCount;
				}
			}

			// Token: 0x0600032D RID: 813 RVA: 0x0000D294 File Offset: 0x0000B494
			private void CompleteBlockIfPossible()
			{
				if (!this._completionReserved)
				{
					bool flag = this._nonGreedyState != null && this._nonGreedyState.TaskForInputProcessing != null;
					bool flag2 = this._batchesCompleted >= this._dataflowBlockOptions.ActualMaxNumberOfGroups;
					bool flag3 = this._decliningPermanently && this._messages.Count < this._batchSize;
					bool flag4 = !flag && (flag2 || flag3 || this.CanceledOrFaulted);
					if (flag4)
					{
						this._completionReserved = true;
						this._decliningPermanently = true;
						if (this._messages.Count > 0)
						{
							this.MakeBatchIfPossible(true);
						}
						Task.Factory.StartNew(delegate(object thisTargetCore)
						{
							BatchBlock<T>.BatchBlockTargetCore batchBlockTargetCore = (BatchBlock<T>.BatchBlockTargetCore)thisTargetCore;
							List<Exception> list = null;
							if (batchBlockTargetCore._nonGreedyState != null)
							{
								Common.ReleaseAllPostponedMessages<T>(batchBlockTargetCore._owningBatch, batchBlockTargetCore._nonGreedyState.PostponedMessages, ref list);
							}
							if (list != null)
							{
								batchBlockTargetCore._owningBatch._source.AddExceptions(list);
							}
							batchBlockTargetCore._completionTask.TrySetResult(default(VoidResult));
						}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
					}
				}
			}

			// Token: 0x170000F5 RID: 245
			// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D374 File Offset: 0x0000B574
			private bool BatchesNeedProcessing
			{
				get
				{
					bool flag = this._batchesCompleted >= this._dataflowBlockOptions.ActualMaxNumberOfGroups;
					bool flag2 = this._nonGreedyState != null && this._nonGreedyState.TaskForInputProcessing != null;
					if (flag || flag2 || this.CanceledOrFaulted)
					{
						return false;
					}
					int num = this._batchSize - this._messages.Count;
					int boundedCapacityAvailable = this.BoundedCapacityAvailable;
					if (num <= 0)
					{
						return true;
					}
					if (this._nonGreedyState != null)
					{
						if (this._nonGreedyState.AcceptFewerThanBatchSize && this._messages.Count > 0)
						{
							return true;
						}
						if (this._decliningPermanently)
						{
							return false;
						}
						if (this._nonGreedyState.AcceptFewerThanBatchSize && this._nonGreedyState.PostponedMessages.Count > 0 && boundedCapacityAvailable > 0)
						{
							return true;
						}
						if (this._dataflowBlockOptions.Greedy)
						{
							if (this._nonGreedyState.PostponedMessages.Count > 0 && boundedCapacityAvailable > 0)
							{
								return true;
							}
						}
						else if (this._nonGreedyState.PostponedMessages.Count >= num && boundedCapacityAvailable >= num)
						{
							return true;
						}
					}
					return false;
				}
			}

			// Token: 0x0600032F RID: 815 RVA: 0x0000D479 File Offset: 0x0000B679
			private void ProcessAsyncIfNecessary(bool isReplacementReplica = false)
			{
				if (this.BatchesNeedProcessing)
				{
					this.ProcessAsyncIfNecessary_Slow(isReplacementReplica);
				}
			}

			// Token: 0x06000330 RID: 816 RVA: 0x0000D48C File Offset: 0x0000B68C
			private void ProcessAsyncIfNecessary_Slow(bool isReplacementReplica)
			{
				this._nonGreedyState.TaskForInputProcessing = new Task(delegate(object thisBatchTarget)
				{
					((BatchBlock<T>.BatchBlockTargetCore)thisBatchTarget).ProcessMessagesLoopCore();
				}, this, Common.GetCreationOptionsForTask(isReplacementReplica));
				DataflowEtwProvider log = DataflowEtwProvider.Log;
				if (log.IsEnabled())
				{
					log.TaskLaunchedForMessageHandling(this._owningBatch, this._nonGreedyState.TaskForInputProcessing, DataflowEtwProvider.TaskLaunchedReason.ProcessingInputMessages, this._messages.Count + this._nonGreedyState.PostponedMessages.Count);
				}
				Exception ex = Common.StartTaskSafe(this._nonGreedyState.TaskForInputProcessing, this._dataflowBlockOptions.TaskScheduler);
				if (ex != null)
				{
					Task.Factory.StartNew(delegate(object exc)
					{
						this.Complete((Exception)exc, true, true, true);
					}, ex, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}

			// Token: 0x06000331 RID: 817 RVA: 0x0000D558 File Offset: 0x0000B758
			private void ProcessMessagesLoopCore()
			{
				try
				{
					int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
					int num = 0;
					bool flag3;
					do
					{
						bool flag = Volatile.Read(ref this._nonGreedyState.AcceptFewerThanBatchSize);
						if (!this._dataflowBlockOptions.Greedy)
						{
							this.RetrievePostponedItemsNonGreedy(flag);
						}
						else
						{
							this.RetrievePostponedItemsGreedyBounded(flag);
						}
						object incomingLock = this.IncomingLock;
						lock (incomingLock)
						{
							flag3 = this.MakeBatchIfPossible(flag);
							if (flag3 || flag)
							{
								this._nonGreedyState.AcceptFewerThanBatchSize = false;
							}
						}
						num++;
					}
					while (flag3 && num < actualMaxMessagesPerTask);
				}
				catch (Exception ex)
				{
					this.Complete(ex, false, true, false);
				}
				finally
				{
					object incomingLock2 = this.IncomingLock;
					lock (incomingLock2)
					{
						this._nonGreedyState.TaskForInputProcessing = null;
						this.ProcessAsyncIfNecessary(true);
						this.CompleteBlockIfPossible();
					}
				}
			}

			// Token: 0x06000332 RID: 818 RVA: 0x0000D668 File Offset: 0x0000B868
			private bool MakeBatchIfPossible(bool evenIfFewerThanBatchSize)
			{
				bool flag = this._messages.Count >= this._batchSize;
				if (flag || (evenIfFewerThanBatchSize && this._messages.Count > 0))
				{
					T[] array = new T[flag ? this._batchSize : this._messages.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._messages.Dequeue();
					}
					this._batchCompletedAction(array);
					this._batchesCompleted += 1L;
					if (this._batchesCompleted >= this._dataflowBlockOptions.ActualMaxNumberOfGroups)
					{
						this._decliningPermanently = true;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06000333 RID: 819 RVA: 0x0000D718 File Offset: 0x0000B918
			private void RetrievePostponedItemsNonGreedy(bool allowFewerThanBatchSize)
			{
				QueuedMap<ISourceBlock<T>, DataflowMessageHeader> postponedMessages = this._nonGreedyState.PostponedMessages;
				KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>[] postponedMessagesTemp = this._nonGreedyState.PostponedMessagesTemp;
				List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> reservedSourcesTemp = this._nonGreedyState.ReservedSourcesTemp;
				reservedSourcesTemp.Clear();
				object incomingLock = this.IncomingLock;
				int num;
				lock (incomingLock)
				{
					int boundedCapacityAvailable = this.BoundedCapacityAvailable;
					if (this._decliningPermanently || postponedMessages.Count == 0 || boundedCapacityAvailable <= 0 || (!allowFewerThanBatchSize && (postponedMessages.Count < this._batchSize || boundedCapacityAvailable < this._batchSize)))
					{
						return;
					}
					num = postponedMessages.PopRange(postponedMessagesTemp, 0, this._batchSize);
				}
				for (int i = 0; i < num; i++)
				{
					KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair = postponedMessagesTemp[i];
					if (keyValuePair.Key.ReserveMessage(keyValuePair.Value, this._owningBatch))
					{
						KeyValuePair<DataflowMessageHeader, T> keyValuePair2 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair.Value, default(T));
						KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair3 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair.Key, keyValuePair2);
						reservedSourcesTemp.Add(keyValuePair3);
					}
				}
				Array.Clear(postponedMessagesTemp, 0, postponedMessagesTemp.Length);
				while (reservedSourcesTemp.Count < this._batchSize)
				{
					object incomingLock2 = this.IncomingLock;
					KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair4;
					lock (incomingLock2)
					{
						if (!postponedMessages.TryPop(out keyValuePair4))
						{
							break;
						}
					}
					if (keyValuePair4.Key.ReserveMessage(keyValuePair4.Value, this._owningBatch))
					{
						KeyValuePair<DataflowMessageHeader, T> keyValuePair5 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair4.Value, default(T));
						KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair6 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair4.Key, keyValuePair5);
						reservedSourcesTemp.Add(keyValuePair6);
					}
				}
				if (reservedSourcesTemp.Count > 0)
				{
					bool flag3 = true;
					if (allowFewerThanBatchSize)
					{
						object incomingLock3 = this.IncomingLock;
						lock (incomingLock3)
						{
							if (!this._decliningPermanently && this._batchesCompleted + 1L >= this._dataflowBlockOptions.ActualMaxNumberOfGroups)
							{
								flag3 = !this._decliningPermanently;
								this._decliningPermanently = true;
							}
						}
					}
					if (flag3 && (allowFewerThanBatchSize || reservedSourcesTemp.Count == this._batchSize))
					{
						this.ConsumeReservedMessagesNonGreedy();
					}
					else
					{
						this.ReleaseReservedMessages(true);
					}
				}
				reservedSourcesTemp.Clear();
			}

			// Token: 0x06000334 RID: 820 RVA: 0x0000D96C File Offset: 0x0000BB6C
			private void RetrievePostponedItemsGreedyBounded(bool allowFewerThanBatchSize)
			{
				QueuedMap<ISourceBlock<T>, DataflowMessageHeader> postponedMessages = this._nonGreedyState.PostponedMessages;
				KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>[] postponedMessagesTemp = this._nonGreedyState.PostponedMessagesTemp;
				List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> reservedSourcesTemp = this._nonGreedyState.ReservedSourcesTemp;
				reservedSourcesTemp.Clear();
				object incomingLock = this.IncomingLock;
				int num;
				int num2;
				lock (incomingLock)
				{
					int boundedCapacityAvailable = this.BoundedCapacityAvailable;
					num = this._batchSize - this._messages.Count;
					if (this._decliningPermanently || postponedMessages.Count == 0 || boundedCapacityAvailable <= 0)
					{
						return;
					}
					if (boundedCapacityAvailable < num)
					{
						num = boundedCapacityAvailable;
					}
					num2 = postponedMessages.PopRange(postponedMessagesTemp, 0, num);
				}
				for (int i = 0; i < num2; i++)
				{
					KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair = postponedMessagesTemp[i];
					KeyValuePair<DataflowMessageHeader, T> keyValuePair2 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair.Value, default(T));
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair3 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair.Key, keyValuePair2);
					reservedSourcesTemp.Add(keyValuePair3);
				}
				Array.Clear(postponedMessagesTemp, 0, postponedMessagesTemp.Length);
				while (reservedSourcesTemp.Count < num)
				{
					object incomingLock2 = this.IncomingLock;
					KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair4;
					lock (incomingLock2)
					{
						if (!postponedMessages.TryPop(out keyValuePair4))
						{
							break;
						}
					}
					KeyValuePair<DataflowMessageHeader, T> keyValuePair5 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair4.Value, default(T));
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair6 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair4.Key, keyValuePair5);
					reservedSourcesTemp.Add(keyValuePair6);
				}
				if (reservedSourcesTemp.Count > 0)
				{
					bool flag3 = true;
					if (allowFewerThanBatchSize)
					{
						object incomingLock3 = this.IncomingLock;
						lock (incomingLock3)
						{
							if (!this._decliningPermanently && this._batchesCompleted + 1L >= this._dataflowBlockOptions.ActualMaxNumberOfGroups)
							{
								flag3 = !this._decliningPermanently;
								this._decliningPermanently = true;
							}
						}
					}
					if (flag3)
					{
						this.ConsumeReservedMessagesGreedyBounded();
					}
				}
				reservedSourcesTemp.Clear();
			}

			// Token: 0x06000335 RID: 821 RVA: 0x0000DB68 File Offset: 0x0000BD68
			private void ConsumeReservedMessagesNonGreedy()
			{
				List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> reservedSourcesTemp = this._nonGreedyState.ReservedSourcesTemp;
				for (int i = 0; i < reservedSourcesTemp.Count; i++)
				{
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair = reservedSourcesTemp[i];
					reservedSourcesTemp[i] = default(KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>);
					bool flag;
					T t = keyValuePair.Key.ConsumeMessage(keyValuePair.Value.Key, this._owningBatch, out flag);
					if (!flag)
					{
						for (int j = 0; j < i; j++)
						{
							reservedSourcesTemp[j] = default(KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>);
						}
						throw new InvalidOperationException(SR.InvalidOperation_FailedToConsumeReservedMessage);
					}
					KeyValuePair<DataflowMessageHeader, T> keyValuePair2 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair.Value.Key, t);
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair3 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair.Key, keyValuePair2);
					reservedSourcesTemp[i] = keyValuePair3;
				}
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (this._boundingState != null)
					{
						this._boundingState.CurrentCount += reservedSourcesTemp.Count;
					}
					foreach (KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair4 in reservedSourcesTemp)
					{
						this._messages.Enqueue(keyValuePair4.Value.Value);
					}
				}
			}

			// Token: 0x06000336 RID: 822 RVA: 0x0000DCDC File Offset: 0x0000BEDC
			private void ConsumeReservedMessagesGreedyBounded()
			{
				int num = 0;
				List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> reservedSourcesTemp = this._nonGreedyState.ReservedSourcesTemp;
				for (int i = 0; i < reservedSourcesTemp.Count; i++)
				{
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair = reservedSourcesTemp[i];
					reservedSourcesTemp[i] = default(KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>);
					bool flag;
					T t = keyValuePair.Key.ConsumeMessage(keyValuePair.Value.Key, this._owningBatch, out flag);
					if (flag)
					{
						KeyValuePair<DataflowMessageHeader, T> keyValuePair2 = new KeyValuePair<DataflowMessageHeader, T>(keyValuePair.Value.Key, t);
						KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair3 = new KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>(keyValuePair.Key, keyValuePair2);
						reservedSourcesTemp[i] = keyValuePair3;
						num++;
					}
				}
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					if (this._boundingState != null)
					{
						this._boundingState.CurrentCount += num;
					}
					foreach (KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair4 in reservedSourcesTemp)
					{
						if (keyValuePair4.Key != null)
						{
							this._messages.Enqueue(keyValuePair4.Value.Value);
						}
					}
				}
			}

			// Token: 0x06000337 RID: 823 RVA: 0x0000DE2C File Offset: 0x0000C02C
			internal void ReleaseReservedMessages(bool throwOnFirstException)
			{
				List<Exception> list = null;
				List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> reservedSourcesTemp = this._nonGreedyState.ReservedSourcesTemp;
				for (int i = 0; i < reservedSourcesTemp.Count; i++)
				{
					KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>> keyValuePair = reservedSourcesTemp[i];
					reservedSourcesTemp[i] = default(KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>);
					ISourceBlock<T> key = keyValuePair.Key;
					KeyValuePair<DataflowMessageHeader, T> value = keyValuePair.Value;
					if (key != null && value.Key.IsValid)
					{
						try
						{
							key.ReleaseReservation(value.Key, this._owningBatch);
						}
						catch (Exception ex)
						{
							if (throwOnFirstException)
							{
								throw;
							}
							if (list == null)
							{
								list = new List<Exception>(1);
							}
							list.Add(ex);
						}
					}
				}
				if (list != null)
				{
					throw new AggregateException(list);
				}
			}

			// Token: 0x06000338 RID: 824 RVA: 0x0000DEE4 File Offset: 0x0000C0E4
			internal void OnItemsRemoved(int numItemsRemoved)
			{
				if (this._boundingState != null)
				{
					object incomingLock = this.IncomingLock;
					lock (incomingLock)
					{
						this._boundingState.CurrentCount -= numItemsRemoved;
						this.ProcessAsyncIfNecessary(false);
						this.CompleteBlockIfPossible();
					}
				}
			}

			// Token: 0x06000339 RID: 825 RVA: 0x0000DF48 File Offset: 0x0000C148
			internal static int CountItems(T[] singleOutputItem, IList<T[]> multipleOutputItems)
			{
				if (multipleOutputItems == null)
				{
					return singleOutputItem.Length;
				}
				int num = 0;
				foreach (T[] array in multipleOutputItems)
				{
					num += array.Length;
				}
				return num;
			}

			// Token: 0x170000F6 RID: 246
			// (get) Token: 0x0600033A RID: 826 RVA: 0x0000DF9C File Offset: 0x0000C19C
			private int InputCountForDebugger
			{
				get
				{
					return this._messages.Count;
				}
			}

			// Token: 0x0600033B RID: 827 RVA: 0x0000DFA9 File Offset: 0x0000C1A9
			internal BatchBlock<T>.BatchBlockTargetCore.DebuggingInformation GetDebuggingInformation()
			{
				return new BatchBlock<T>.BatchBlockTargetCore.DebuggingInformation(this);
			}

			// Token: 0x170000F7 RID: 247
			// (get) Token: 0x0600033C RID: 828 RVA: 0x0000DFB4 File Offset: 0x0000C1B4
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay owningBatch = this._owningBatch;
					return string.Format("Block=\"{0}\"", (owningBatch != null) ? owningBatch.Content : this._owningBatch);
				}
			}

			// Token: 0x04000123 RID: 291
			private readonly Queue<T> _messages = new Queue<T>();

			// Token: 0x04000124 RID: 292
			private readonly TaskCompletionSource<VoidResult> _completionTask = new TaskCompletionSource<VoidResult>();

			// Token: 0x04000125 RID: 293
			private readonly BatchBlock<T> _owningBatch;

			// Token: 0x04000126 RID: 294
			private readonly int _batchSize;

			// Token: 0x04000127 RID: 295
			private readonly BatchBlock<T>.BatchBlockTargetCore.NonGreedyState _nonGreedyState;

			// Token: 0x04000128 RID: 296
			private readonly BoundingState _boundingState;

			// Token: 0x04000129 RID: 297
			private readonly GroupingDataflowBlockOptions _dataflowBlockOptions;

			// Token: 0x0400012A RID: 298
			private readonly Action<T[]> _batchCompletedAction;

			// Token: 0x0400012B RID: 299
			private bool _decliningPermanently;

			// Token: 0x0400012C RID: 300
			private long _batchesCompleted;

			// Token: 0x0400012D RID: 301
			private bool _completionReserved;

			// Token: 0x0200009C RID: 156
			private sealed class NonGreedyState
			{
				// Token: 0x0600049C RID: 1180 RVA: 0x00010989 File Offset: 0x0000EB89
				internal NonGreedyState(int batchSize)
				{
					this.PostponedMessages = new QueuedMap<ISourceBlock<T>, DataflowMessageHeader>(batchSize);
					this.PostponedMessagesTemp = new KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>[batchSize];
					this.ReservedSourcesTemp = new List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>>(batchSize);
				}

				// Token: 0x040001F0 RID: 496
				internal readonly QueuedMap<ISourceBlock<T>, DataflowMessageHeader> PostponedMessages;

				// Token: 0x040001F1 RID: 497
				internal readonly KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>[] PostponedMessagesTemp;

				// Token: 0x040001F2 RID: 498
				internal readonly List<KeyValuePair<ISourceBlock<T>, KeyValuePair<DataflowMessageHeader, T>>> ReservedSourcesTemp;

				// Token: 0x040001F3 RID: 499
				internal bool AcceptFewerThanBatchSize;

				// Token: 0x040001F4 RID: 500
				internal Task TaskForInputProcessing;
			}

			// Token: 0x0200009D RID: 157
			internal sealed class DebuggingInformation
			{
				// Token: 0x0600049D RID: 1181 RVA: 0x000109B5 File Offset: 0x0000EBB5
				public DebuggingInformation(BatchBlock<T>.BatchBlockTargetCore target)
				{
					this._target = target;
				}

				// Token: 0x17000187 RID: 391
				// (get) Token: 0x0600049E RID: 1182 RVA: 0x000109C4 File Offset: 0x0000EBC4
				public IEnumerable<T> InputQueue
				{
					get
					{
						return this._target._messages.ToList<T>();
					}
				}

				// Token: 0x17000188 RID: 392
				// (get) Token: 0x0600049F RID: 1183 RVA: 0x000109D6 File Offset: 0x0000EBD6
				public Task TaskForInputProcessing
				{
					get
					{
						if (this._target._nonGreedyState == null)
						{
							return null;
						}
						return this._target._nonGreedyState.TaskForInputProcessing;
					}
				}

				// Token: 0x17000189 RID: 393
				// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000109F7 File Offset: 0x0000EBF7
				public QueuedMap<ISourceBlock<T>, DataflowMessageHeader> PostponedMessages
				{
					get
					{
						if (this._target._nonGreedyState == null)
						{
							return null;
						}
						return this._target._nonGreedyState.PostponedMessages;
					}
				}

				// Token: 0x1700018A RID: 394
				// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00010A18 File Offset: 0x0000EC18
				public bool IsDecliningPermanently
				{
					get
					{
						return this._target._decliningPermanently;
					}
				}

				// Token: 0x1700018B RID: 395
				// (get) Token: 0x060004A2 RID: 1186 RVA: 0x00010A25 File Offset: 0x0000EC25
				public GroupingDataflowBlockOptions DataflowBlockOptions
				{
					get
					{
						return this._target._dataflowBlockOptions;
					}
				}

				// Token: 0x1700018C RID: 396
				// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00010A32 File Offset: 0x0000EC32
				public long NumberOfBatchesCompleted
				{
					get
					{
						return this._target._batchesCompleted;
					}
				}

				// Token: 0x040001F5 RID: 501
				private readonly BatchBlock<T>.BatchBlockTargetCore _target;
			}
		}
	}
}
