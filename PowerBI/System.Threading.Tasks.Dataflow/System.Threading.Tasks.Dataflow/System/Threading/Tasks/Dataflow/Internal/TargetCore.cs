using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000047 RID: 71
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	internal sealed class TargetCore<TInput>
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A96C File Offset: 0x00008B6C
		private object IncomingLock
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A974 File Offset: 0x00008B74
		internal TargetCore(ITargetBlock<TInput> owningTarget, Action<KeyValuePair<TInput, long>> callAction, IReorderingBuffer reorderingBuffer, ExecutionDataflowBlockOptions dataflowBlockOptions, TargetCoreOptions targetCoreOptions)
		{
			this._owningTarget = owningTarget;
			this._callAction = callAction;
			this._reorderingBuffer = reorderingBuffer;
			this._dataflowBlockOptions = dataflowBlockOptions;
			this._targetCoreOptions = targetCoreOptions;
			IProducerConsumerQueue<KeyValuePair<TInput, long>> producerConsumerQueue2;
			if (dataflowBlockOptions.MaxDegreeOfParallelism != 1)
			{
				IProducerConsumerQueue<KeyValuePair<TInput, long>> producerConsumerQueue = new MultiProducerMultiConsumerQueue<KeyValuePair<TInput, long>>();
				producerConsumerQueue2 = producerConsumerQueue;
			}
			else
			{
				IProducerConsumerQueue<KeyValuePair<TInput, long>> producerConsumerQueue = new SingleProducerSingleConsumerQueue<KeyValuePair<TInput, long>>();
				producerConsumerQueue2 = producerConsumerQueue;
			}
			this._messages = producerConsumerQueue2;
			if (this._dataflowBlockOptions.BoundedCapacity != -1)
			{
				this._boundingState = new BoundingStateWithPostponed<TInput>(this._dataflowBlockOptions.BoundedCapacity);
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A9FC File Offset: 0x00008BFC
		internal void Complete(Exception exception, bool dropPendingMessages, bool storeExceptionEvenIfAlreadyCompleting = false, bool unwrapInnerExceptions = false, bool revertProcessingState = false)
		{
			object incomingLock = this.IncomingLock;
			lock (incomingLock)
			{
				if (exception != null && (!this._decliningPermanently || storeExceptionEvenIfAlreadyCompleting))
				{
					Common.AddException(ref this._exceptions, exception, unwrapInnerExceptions);
				}
				if (dropPendingMessages)
				{
					KeyValuePair<TInput, long> keyValuePair;
					while (this._messages.TryDequeue(out keyValuePair))
					{
					}
				}
				if (revertProcessingState)
				{
					this._numberOfOutstandingOperations--;
					if (this.UsesAsyncCompletion)
					{
						this._numberOfOutstandingServiceTasks--;
					}
				}
				this._decliningPermanently = true;
				this.CompleteBlockIfPossible();
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000AA9C File Offset: 0x00008C9C
		internal DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
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
				else if (this._boundingState == null || (this._boundingState.OutstandingTransfers == 0 && this._boundingState.CountIsLessThanBound && this._boundingState.PostponedMessages.Count == 0))
				{
					if (consumeToAccept)
					{
						bool flag2;
						messageValue = source.ConsumeMessage(messageHeader, this._owningTarget, out flag2);
						if (!flag2)
						{
							return DataflowMessageStatus.NotAvailable;
						}
					}
					long value = this._nextAvailableInputMessageId.Value;
					this._nextAvailableInputMessageId.Value = value + 1L;
					long num = value;
					if (this._boundingState != null)
					{
						this._boundingState.CurrentCount++;
					}
					this._messages.Enqueue(new KeyValuePair<TInput, long>(messageValue, num));
					this.ProcessAsyncIfNecessary(false);
					dataflowMessageStatus = DataflowMessageStatus.Accepted;
				}
				else if (source != null)
				{
					this._boundingState.PostponedMessages.Push(source, messageHeader);
					this.ProcessAsyncIfNecessary(false);
					dataflowMessageStatus = DataflowMessageStatus.Postponed;
				}
				else
				{
					dataflowMessageStatus = DataflowMessageStatus.Declined;
				}
			}
			return dataflowMessageStatus;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		internal Task Completion
		{
			get
			{
				return this._completionSource.Task;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000ABF1 File Offset: 0x00008DF1
		internal int InputCount
		{
			get
			{
				return this._messages.GetCountSafe(this.IncomingLock);
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000AC04 File Offset: 0x00008E04
		internal void SignalOneAsyncMessageCompleted()
		{
			this.SignalOneAsyncMessageCompleted(0);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000AC10 File Offset: 0x00008E10
		internal void SignalOneAsyncMessageCompleted(int boundingCountChange)
		{
			object incomingLock = this.IncomingLock;
			lock (incomingLock)
			{
				if (this._numberOfOutstandingOperations > 0)
				{
					this._numberOfOutstandingOperations--;
				}
				if (this._boundingState != null && boundingCountChange != 0)
				{
					this._boundingState.CurrentCount += boundingCountChange;
				}
				this.ProcessAsyncIfNecessary(true);
				this.CompleteBlockIfPossible();
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000AC8C File Offset: 0x00008E8C
		private bool UsesAsyncCompletion
		{
			get
			{
				return (this._targetCoreOptions & TargetCoreOptions.UsesAsyncCompletion) > TargetCoreOptions.None;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000AC99 File Offset: 0x00008E99
		private bool HasRoomForMoreOperations
		{
			get
			{
				return this._numberOfOutstandingOperations - this._numberOfOutstandingServiceTasks < this._dataflowBlockOptions.ActualMaxDegreeOfParallelism;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000ACB5 File Offset: 0x00008EB5
		private bool HasRoomForMoreServiceTasks
		{
			get
			{
				if (!this.UsesAsyncCompletion)
				{
					return this.HasRoomForMoreOperations;
				}
				return this.HasRoomForMoreOperations && this._numberOfOutstandingServiceTasks < this._dataflowBlockOptions.ActualMaxDegreeOfParallelism;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000ACE3 File Offset: 0x00008EE3
		private void ProcessAsyncIfNecessary(bool repeat = false)
		{
			if (this.HasRoomForMoreServiceTasks)
			{
				this.ProcessAsyncIfNecessary_Slow(repeat);
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		private void ProcessAsyncIfNecessary_Slow(bool repeat)
		{
			bool flag = !this._messages.IsEmpty || (!this._decliningPermanently && this._boundingState != null && this._boundingState.CountIsLessThanBound && this._boundingState.PostponedMessages.Count > 0);
			if (flag && !this.CanceledOrFaulted)
			{
				this._numberOfOutstandingOperations++;
				if (this.UsesAsyncCompletion)
				{
					this._numberOfOutstandingServiceTasks++;
				}
				Task task = new Task(delegate(object thisTargetCore)
				{
					((TargetCore<TInput>)thisTargetCore).ProcessMessagesLoopCore();
				}, this, Common.GetCreationOptionsForTask(repeat));
				DataflowEtwProvider log = DataflowEtwProvider.Log;
				if (log.IsEnabled())
				{
					log.TaskLaunchedForMessageHandling(this._owningTarget, task, DataflowEtwProvider.TaskLaunchedReason.ProcessingInputMessages, this._messages.Count + ((this._boundingState != null) ? this._boundingState.PostponedMessages.Count : 0));
				}
				Exception ex = Common.StartTaskSafe(task, this._dataflowBlockOptions.TaskScheduler);
				if (ex != null)
				{
					Task.Factory.StartNew(delegate(object exc)
					{
						this.Complete((Exception)exc, true, true, false, true);
					}, ex, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000AE28 File Offset: 0x00009028
		private void ProcessMessagesLoopCore()
		{
			KeyValuePair<TInput, long> keyValuePair = default(KeyValuePair<TInput, long>);
			try
			{
				bool usesAsyncCompletion = this.UsesAsyncCompletion;
				bool flag = this._boundingState != null && this._boundingState.BoundedCapacity > 1;
				int num = 0;
				int num2 = 0;
				int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
				while (num < actualMaxMessagesPerTask && !this.CanceledOrFaulted)
				{
					KeyValuePair<TInput, long> keyValuePair2;
					if (flag && this.TryConsumePostponedMessage(true, out keyValuePair2))
					{
						object incomingLock = this.IncomingLock;
						lock (incomingLock)
						{
							this._boundingState.OutstandingTransfers--;
							this._messages.Enqueue(keyValuePair2);
							this.ProcessAsyncIfNecessary(false);
						}
					}
					if (usesAsyncCompletion)
					{
						if (!this.TryGetNextMessageForNewAsyncOperation(out keyValuePair))
						{
							break;
						}
					}
					else if (!this.TryGetNextAvailableOrPostponedMessage(out keyValuePair))
					{
						if (this._dataflowBlockOptions.MaxDegreeOfParallelism != 1 || num2 > 1)
						{
							break;
						}
						if (this._keepAliveBanCounter > 0)
						{
							this._keepAliveBanCounter--;
							break;
						}
						num2 = 0;
						if (!Common.TryKeepAliveUntil<TargetCore<TInput>, KeyValuePair<TInput, long>>(TargetCore<TInput>._keepAlivePredicate, this, out keyValuePair))
						{
							this._keepAliveBanCounter = 1000;
							break;
						}
					}
					num++;
					num2++;
					this._callAction(keyValuePair);
				}
			}
			catch (Exception ex)
			{
				Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, keyValuePair.Key, false);
				this.Complete(ex, true, true, false, false);
			}
			finally
			{
				object incomingLock2 = this.IncomingLock;
				lock (incomingLock2)
				{
					this._numberOfOutstandingOperations--;
					if (this.UsesAsyncCompletion)
					{
						this._numberOfOutstandingServiceTasks--;
					}
					this.ProcessAsyncIfNecessary(true);
					this.CompleteBlockIfPossible();
				}
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B030 File Offset: 0x00009230
		private bool TryGetNextMessageForNewAsyncOperation(out KeyValuePair<TInput, long> messageWithId)
		{
			object incomingLock = this.IncomingLock;
			bool hasRoomForMoreOperations;
			lock (incomingLock)
			{
				hasRoomForMoreOperations = this.HasRoomForMoreOperations;
				if (hasRoomForMoreOperations)
				{
					this._numberOfOutstandingOperations++;
				}
			}
			messageWithId = default(KeyValuePair<TInput, long>);
			if (hasRoomForMoreOperations)
			{
				bool flag2 = false;
				try
				{
					flag2 = this.TryGetNextAvailableOrPostponedMessage(out messageWithId);
				}
				catch
				{
					this.SignalOneAsyncMessageCompleted();
					throw;
				}
				if (!flag2)
				{
					this.SignalOneAsyncMessageCompleted();
				}
				return flag2;
			}
			return false;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B0BC File Offset: 0x000092BC
		private bool TryGetNextAvailableOrPostponedMessage(out KeyValuePair<TInput, long> messageWithId)
		{
			if (this._messages.TryDequeue(out messageWithId))
			{
				return true;
			}
			if (this._boundingState != null && this.TryConsumePostponedMessage(false, out messageWithId))
			{
				return true;
			}
			messageWithId = default(KeyValuePair<TInput, long>);
			return false;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B0EC File Offset: 0x000092EC
		private bool TryConsumePostponedMessage(bool forPostponementTransfer, out KeyValuePair<TInput, long> result)
		{
			bool flag = false;
			long num = -1L;
			TInput tinput;
			for (;;)
			{
				object incomingLock = this.IncomingLock;
				KeyValuePair<ISourceBlock<TInput>, DataflowMessageHeader> keyValuePair;
				lock (incomingLock)
				{
					if (this._decliningPermanently)
					{
						goto IL_011F;
					}
					if (!forPostponementTransfer && this._messages.TryDequeue(out result))
					{
						return true;
					}
					if (!this._boundingState.CountIsLessThanBound || !this._boundingState.PostponedMessages.TryPop(out keyValuePair))
					{
						if (flag)
						{
							flag = false;
							this._boundingState.CurrentCount--;
						}
						goto IL_011F;
					}
					if (!flag)
					{
						flag = true;
						long value = this._nextAvailableInputMessageId.Value;
						this._nextAvailableInputMessageId.Value = value + 1L;
						num = value;
						this._boundingState.CurrentCount++;
						if (forPostponementTransfer)
						{
							this._boundingState.OutstandingTransfers++;
						}
					}
				}
				bool flag3;
				tinput = keyValuePair.Key.ConsumeMessage(keyValuePair.Value, this._owningTarget, out flag3);
				if (flag3)
				{
					break;
				}
				if (forPostponementTransfer)
				{
					this._boundingState.OutstandingTransfers--;
				}
			}
			result = new KeyValuePair<TInput, long>(tinput, num);
			return true;
			IL_011F:
			if (this._reorderingBuffer != null && num != -1L)
			{
				this._reorderingBuffer.IgnoreItem(num);
			}
			if (flag)
			{
				this.ChangeBoundingCount(-1);
			}
			result = default(KeyValuePair<TInput, long>);
			return false;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000B258 File Offset: 0x00009458
		private bool CanceledOrFaulted
		{
			get
			{
				return this._dataflowBlockOptions.CancellationToken.IsCancellationRequested || Volatile.Read<List<Exception>>(ref this._exceptions) != null;
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B28C File Offset: 0x0000948C
		private void CompleteBlockIfPossible()
		{
			if ((this._decliningPermanently && this._messages.IsEmpty) || this.CanceledOrFaulted)
			{
				this.CompleteBlockIfPossible_Slow();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B2C4 File Offset: 0x000094C4
		private void CompleteBlockIfPossible_Slow()
		{
			bool flag = this._numberOfOutstandingOperations == 0;
			if (flag && !this._completionReserved)
			{
				this._completionReserved = true;
				this._decliningPermanently = true;
				Task.Factory.StartNew(delegate(object state)
				{
					((TargetCore<TInput>)state).CompleteBlockOncePossible();
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B330 File Offset: 0x00009530
		private void CompleteBlockOncePossible()
		{
			if (this._boundingState != null)
			{
				Common.ReleaseAllPostponedMessages<TInput>(this._owningTarget, this._boundingState.PostponedMessages, ref this._exceptions);
			}
			IProducerConsumerQueue<KeyValuePair<TInput, long>> messages = this._messages;
			KeyValuePair<TInput, long> keyValuePair;
			while (messages.TryDequeue(out keyValuePair))
			{
			}
			if (Volatile.Read<List<Exception>>(ref this._exceptions) != null)
			{
				this._completionSource.TrySetException(Volatile.Read<List<Exception>>(ref this._exceptions));
			}
			else if (this._dataflowBlockOptions.CancellationToken.IsCancellationRequested)
			{
				this._completionSource.TrySetCanceled();
			}
			else
			{
				this._completionSource.TrySetResult(default(VoidResult));
			}
			DataflowEtwProvider log;
			if ((this._targetCoreOptions & TargetCoreOptions.RepresentsBlockCompletion) != TargetCoreOptions.None && (log = DataflowEtwProvider.Log).IsEnabled())
			{
				log.DataflowBlockCompleted(this._owningTarget);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000B3F5 File Offset: 0x000095F5
		internal bool IsBounded
		{
			get
			{
				return this._boundingState != null;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B400 File Offset: 0x00009600
		internal void ChangeBoundingCount(int count)
		{
			if (this._boundingState != null)
			{
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					this._boundingState.CurrentCount += count;
					this.ProcessAsyncIfNecessary(false);
					this.CompleteBlockIfPossible();
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000B464 File Offset: 0x00009664
		private object DebuggerDisplayContent
		{
			get
			{
				IDebuggerDisplay debuggerDisplay = this._owningTarget as IDebuggerDisplay;
				return string.Format("Block=\"{0}\"", (debuggerDisplay != null) ? debuggerDisplay.Content : this._owningTarget);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000B498 File Offset: 0x00009698
		internal ExecutionDataflowBlockOptions DataflowBlockOptions
		{
			get
			{
				return this._dataflowBlockOptions;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B4A0 File Offset: 0x000096A0
		internal TargetCore<TInput>.DebuggingInformation GetDebuggingInformation()
		{
			return new TargetCore<TInput>.DebuggingInformation(this);
		}

		// Token: 0x040000BE RID: 190
		private static readonly Common.KeepAlivePredicate<TargetCore<TInput>, KeyValuePair<TInput, long>> _keepAlivePredicate = delegate(TargetCore<TInput> thisTargetCore, out KeyValuePair<TInput, long> messageWithId)
		{
			return thisTargetCore.TryGetNextAvailableOrPostponedMessage(out messageWithId);
		};

		// Token: 0x040000BF RID: 191
		private readonly TaskCompletionSource<VoidResult> _completionSource = new TaskCompletionSource<VoidResult>();

		// Token: 0x040000C0 RID: 192
		private readonly ITargetBlock<TInput> _owningTarget;

		// Token: 0x040000C1 RID: 193
		private readonly IProducerConsumerQueue<KeyValuePair<TInput, long>> _messages;

		// Token: 0x040000C2 RID: 194
		private readonly ExecutionDataflowBlockOptions _dataflowBlockOptions;

		// Token: 0x040000C3 RID: 195
		private readonly Action<KeyValuePair<TInput, long>> _callAction;

		// Token: 0x040000C4 RID: 196
		private readonly TargetCoreOptions _targetCoreOptions;

		// Token: 0x040000C5 RID: 197
		private readonly BoundingStateWithPostponed<TInput> _boundingState;

		// Token: 0x040000C6 RID: 198
		private readonly IReorderingBuffer _reorderingBuffer;

		// Token: 0x040000C7 RID: 199
		private List<Exception> _exceptions;

		// Token: 0x040000C8 RID: 200
		private bool _decliningPermanently;

		// Token: 0x040000C9 RID: 201
		private int _numberOfOutstandingOperations;

		// Token: 0x040000CA RID: 202
		private int _numberOfOutstandingServiceTasks;

		// Token: 0x040000CB RID: 203
		private PaddedInt64 _nextAvailableInputMessageId;

		// Token: 0x040000CC RID: 204
		private bool _completionReserved;

		// Token: 0x040000CD RID: 205
		private int _keepAliveBanCounter;

		// Token: 0x0200008C RID: 140
		internal sealed class DebuggingInformation
		{
			// Token: 0x06000456 RID: 1110 RVA: 0x00010089 File Offset: 0x0000E289
			internal DebuggingInformation(TargetCore<TInput> target)
			{
				this._target = target;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00010098 File Offset: 0x0000E298
			internal int InputCount
			{
				get
				{
					return this._target._messages.Count;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000458 RID: 1112 RVA: 0x000100AA File Offset: 0x0000E2AA
			internal IEnumerable<TInput> InputQueue
			{
				get
				{
					return this._target._messages.Select((KeyValuePair<TInput, long> kvp) => kvp.Key).ToList<TInput>();
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x000100E0 File Offset: 0x0000E2E0
			internal QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					if (this._target._boundingState == null)
					{
						return null;
					}
					return this._target._boundingState.PostponedMessages;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x0600045A RID: 1114 RVA: 0x00010101 File Offset: 0x0000E301
			internal int CurrentDegreeOfParallelism
			{
				get
				{
					return this._target._numberOfOutstandingOperations - this._target._numberOfOutstandingServiceTasks;
				}
			}

			// Token: 0x17000179 RID: 377
			// (get) Token: 0x0600045B RID: 1115 RVA: 0x0001011A File Offset: 0x0000E31A
			internal ExecutionDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._target._dataflowBlockOptions;
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00010127 File Offset: 0x0000E327
			internal bool IsDecliningPermanently
			{
				get
				{
					return this._target._decliningPermanently;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600045D RID: 1117 RVA: 0x00010134 File Offset: 0x0000E334
			internal bool IsCompleted
			{
				get
				{
					return this._target.Completion.IsCompleted;
				}
			}

			// Token: 0x040001C7 RID: 455
			private readonly TargetCore<TInput> _target;
		}
	}
}
