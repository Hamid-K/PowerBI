using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000033 RID: 51
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(JoinBlockTarget<>.DebugView))]
	internal sealed class JoinBlockTarget<T> : JoinBlockTargetBase, ITargetBlock<T>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000077F8 File Offset: 0x000059F8
		internal JoinBlockTarget(JoinBlockTargetSharedResources sharedResources)
		{
			GroupingDataflowBlockOptions dataflowBlockOptions = sharedResources._dataflowBlockOptions;
			this._sharedResources = sharedResources;
			if (!dataflowBlockOptions.Greedy || dataflowBlockOptions.BoundedCapacity > 0)
			{
				this._nonGreedy = new JoinBlockTarget<T>.NonGreedyState();
			}
			if (dataflowBlockOptions.Greedy)
			{
				this._messages = new Queue<T>();
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007854 File Offset: 0x00005A54
		internal T GetOneMessage()
		{
			if (this._sharedResources._dataflowBlockOptions.Greedy)
			{
				return this._messages.Dequeue();
			}
			T value = this._nonGreedy.ConsumedMessage.Value;
			this._nonGreedy.ConsumedMessage = new KeyValuePair<bool, T>(false, default(T));
			return value;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x000078AB File Offset: 0x00005AAB
		internal override bool IsDecliningPermanently
		{
			get
			{
				return this._decliningPermanently;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000078B3 File Offset: 0x00005AB3
		internal override bool HasAtLeastOneMessageAvailable
		{
			get
			{
				if (this._sharedResources._dataflowBlockOptions.Greedy)
				{
					return this._messages.Count > 0;
				}
				return this._nonGreedy.ConsumedMessage.Key;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x000078E6 File Offset: 0x00005AE6
		internal override bool HasAtLeastOnePostponedMessage
		{
			get
			{
				return this._nonGreedy != null && this._nonGreedy.PostponedMessages.Count > 0;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00007905 File Offset: 0x00005B05
		internal override int NumberOfMessagesAvailableOrPostponed
		{
			get
			{
				if (this._sharedResources._dataflowBlockOptions.Greedy)
				{
					return this._messages.Count;
				}
				return this._nonGreedy.PostponedMessages.Count;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00007938 File Offset: 0x00005B38
		internal override bool HasTheHighestNumberOfMessagesAvailable
		{
			get
			{
				int count = this._messages.Count;
				foreach (JoinBlockTargetBase joinBlockTargetBase in this._sharedResources._targets)
				{
					if (joinBlockTargetBase != this && joinBlockTargetBase.NumberOfMessagesAvailableOrPostponed > count)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007980 File Offset: 0x00005B80
		internal override bool ReserveOneMessage()
		{
			object incomingLock = this._sharedResources.IncomingLock;
			KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair;
			lock (incomingLock)
			{
				if (!this._nonGreedy.PostponedMessages.TryPop(out keyValuePair))
				{
					return false;
				}
			}
			while (!keyValuePair.Key.ReserveMessage(keyValuePair.Value, this))
			{
				object incomingLock2 = this._sharedResources.IncomingLock;
				bool flag3;
				lock (incomingLock2)
				{
					if (this._nonGreedy.PostponedMessages.TryPop(out keyValuePair))
					{
						continue;
					}
					flag3 = false;
				}
				return flag3;
			}
			this._nonGreedy.ReservedMessage = keyValuePair;
			return true;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007A48 File Offset: 0x00005C48
		internal override bool ConsumeReservedMessage()
		{
			bool flag;
			T t = this._nonGreedy.ReservedMessage.Key.ConsumeMessage(this._nonGreedy.ReservedMessage.Value, this, out flag);
			this._nonGreedy.ReservedMessage = default(KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>);
			if (!flag)
			{
				this._sharedResources._exceptionAction(new InvalidOperationException(SR.InvalidOperation_FailedToConsumeReservedMessage));
				this.CompleteOncePossible();
				return false;
			}
			object incomingLock = this._sharedResources.IncomingLock;
			lock (incomingLock)
			{
				this._nonGreedy.ConsumedMessage = new KeyValuePair<bool, T>(true, t);
				this.CompleteIfLastJoinIsFeasible();
			}
			return true;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00007B00 File Offset: 0x00005D00
		internal override bool ConsumeOnePostponedMessage()
		{
			bool hasTheHighestNumberOfMessagesAvailable;
			bool flag3;
			T t;
			do
			{
				object incomingLock = this._sharedResources.IncomingLock;
				KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair;
				lock (incomingLock)
				{
					hasTheHighestNumberOfMessagesAvailable = this.HasTheHighestNumberOfMessagesAvailable;
					bool flag2 = this._sharedResources._boundingState.CountIsLessThanBound || !hasTheHighestNumberOfMessagesAvailable;
					if (this._decliningPermanently || this._sharedResources._decliningPermanently || !flag2 || !this._nonGreedy.PostponedMessages.TryPop(out keyValuePair))
					{
						return false;
					}
				}
				t = keyValuePair.Key.ConsumeMessage(keyValuePair.Value, this, out flag3);
			}
			while (!flag3);
			object incomingLock2 = this._sharedResources.IncomingLock;
			bool flag5;
			lock (incomingLock2)
			{
				if (hasTheHighestNumberOfMessagesAvailable)
				{
					this._sharedResources._boundingState.CurrentCount++;
				}
				this._messages.Enqueue(t);
				this.CompleteIfLastJoinIsFeasible();
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007C20 File Offset: 0x00005E20
		private void CompleteIfLastJoinIsFeasible()
		{
			int num = (this._sharedResources._dataflowBlockOptions.Greedy ? this._messages.Count : (this._nonGreedy.ConsumedMessage.Key ? 1 : 0));
			if (this._sharedResources._joinsCreated + (long)num >= this._sharedResources._dataflowBlockOptions.ActualMaxNumberOfGroups)
			{
				this._decliningPermanently = true;
				bool flag = true;
				foreach (JoinBlockTargetBase joinBlockTargetBase in this._sharedResources._targets)
				{
					if (!joinBlockTargetBase.IsDecliningPermanently)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					this._sharedResources._decliningPermanently = true;
				}
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00007CC8 File Offset: 0x00005EC8
		internal override void ReleaseReservedMessage()
		{
			if (this._nonGreedy != null && this._nonGreedy.ReservedMessage.Key != null)
			{
				try
				{
					this._nonGreedy.ReservedMessage.Key.ReleaseReservation(this._nonGreedy.ReservedMessage.Value, this);
				}
				finally
				{
					this.ClearReservation();
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007D30 File Offset: 0x00005F30
		internal override void ClearReservation()
		{
			this._nonGreedy.ReservedMessage = default(KeyValuePair<ISourceBlock<T>, DataflowMessageHeader>);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00007D44 File Offset: 0x00005F44
		internal override void CompleteOncePossible()
		{
			object incomingLock = this._sharedResources.IncomingLock;
			lock (incomingLock)
			{
				this._decliningPermanently = true;
				if (this._messages != null)
				{
					this._messages.Clear();
				}
			}
			List<Exception> list = null;
			if (this._nonGreedy != null)
			{
				Common.ReleaseAllPostponedMessages<T>(this, this._nonGreedy.PostponedMessages, ref list);
			}
			if (list != null)
			{
				foreach (Exception ex in list)
				{
					this._sharedResources._exceptionAction(ex);
				}
			}
			this._completionTask.TrySetResult(default(VoidResult));
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00007E1C File Offset: 0x0000601C
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
			object incomingLock = this._sharedResources.IncomingLock;
			DataflowMessageStatus dataflowMessageStatus;
			lock (incomingLock)
			{
				if (this._decliningPermanently || this._sharedResources._decliningPermanently)
				{
					this._sharedResources.CompleteBlockIfPossible();
					dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
				}
				else if (this._sharedResources._dataflowBlockOptions.Greedy && (this._sharedResources._boundingState == null || ((this._sharedResources._boundingState.CountIsLessThanBound || !this.HasTheHighestNumberOfMessagesAvailable) && this._nonGreedy.PostponedMessages.Count == 0 && this._sharedResources._taskForInputProcessing == null)))
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
					if (this._sharedResources._boundingState != null && this.HasTheHighestNumberOfMessagesAvailable)
					{
						this._sharedResources._boundingState.CurrentCount++;
					}
					this._messages.Enqueue(messageValue);
					this.CompleteIfLastJoinIsFeasible();
					if (this._sharedResources.AllTargetsHaveAtLeastOneMessage)
					{
						this._sharedResources._joinFilledAction();
						this._sharedResources._joinsCreated += 1L;
					}
					this._sharedResources.CompleteBlockIfPossible();
					dataflowMessageStatus = DataflowMessageStatus.Accepted;
				}
				else if (source != null)
				{
					this._nonGreedy.PostponedMessages.Push(source, messageHeader);
					this._sharedResources.ProcessAsyncIfNecessary(false);
					dataflowMessageStatus = DataflowMessageStatus.Postponed;
				}
				else
				{
					dataflowMessageStatus = DataflowMessageStatus.Declined;
				}
			}
			return dataflowMessageStatus;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00007FE8 File Offset: 0x000061E8
		internal override void CompleteCore(Exception exception, bool dropPendingMessages, bool releaseReservedMessages)
		{
			bool greedy = this._sharedResources._dataflowBlockOptions.Greedy;
			object incomingLock = this._sharedResources.IncomingLock;
			lock (incomingLock)
			{
				if (exception != null && ((!this._decliningPermanently && !this._sharedResources._decliningPermanently) || releaseReservedMessages))
				{
					this._sharedResources._exceptionAction(exception);
				}
				if (dropPendingMessages && greedy)
				{
					this._messages.Clear();
				}
			}
			if (releaseReservedMessages && !greedy)
			{
				foreach (JoinBlockTargetBase joinBlockTargetBase in this._sharedResources._targets)
				{
					try
					{
						joinBlockTargetBase.ReleaseReservedMessage();
					}
					catch (Exception ex)
					{
						this._sharedResources._exceptionAction(ex);
					}
				}
			}
			object incomingLock2 = this._sharedResources.IncomingLock;
			lock (incomingLock2)
			{
				this._decliningPermanently = true;
				this._sharedResources.CompleteBlockIfPossible();
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008114 File Offset: 0x00006314
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.CompleteCore(exception, true, false);
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000812D File Offset: 0x0000632D
		public Task Completion
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008139 File Offset: 0x00006339
		internal Task CompletionTaskInternal
		{
			get
			{
				return this._completionTask.Task;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00008146 File Offset: 0x00006346
		private int InputCountForDebugger
		{
			get
			{
				if (this._messages != null)
				{
					return this._messages.Count;
				}
				if (!this._nonGreedy.ConsumedMessage.Key)
				{
					return 0;
				}
				return 1;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008174 File Offset: 0x00006374
		private object DebuggerDisplayContent
		{
			get
			{
				IDebuggerDisplay debuggerDisplay = this._sharedResources._ownerJoin as IDebuggerDisplay;
				return string.Format("{0} InputCount={1}, Join=\"{2}\"", Common.GetNameForDebugger(this, null), this.InputCountForDebugger, (debuggerDisplay != null) ? debuggerDisplay.Content : this._sharedResources._ownerJoin);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000081C4 File Offset: 0x000063C4
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000070 RID: 112
		private readonly JoinBlockTargetSharedResources _sharedResources;

		// Token: 0x04000071 RID: 113
		private readonly TaskCompletionSource<VoidResult> _completionTask = new TaskCompletionSource<VoidResult>();

		// Token: 0x04000072 RID: 114
		private readonly Queue<T> _messages;

		// Token: 0x04000073 RID: 115
		private readonly JoinBlockTarget<T>.NonGreedyState _nonGreedy;

		// Token: 0x04000074 RID: 116
		private bool _decliningPermanently;

		// Token: 0x0200007A RID: 122
		private sealed class NonGreedyState
		{
			// Token: 0x04000198 RID: 408
			internal readonly QueuedMap<ISourceBlock<T>, DataflowMessageHeader> PostponedMessages = new QueuedMap<ISourceBlock<T>, DataflowMessageHeader>();

			// Token: 0x04000199 RID: 409
			internal KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> ReservedMessage;

			// Token: 0x0400019A RID: 410
			internal KeyValuePair<bool, T> ConsumedMessage;
		}

		// Token: 0x0200007B RID: 123
		private sealed class DebugView
		{
			// Token: 0x06000417 RID: 1047 RVA: 0x0000FA27 File Offset: 0x0000DC27
			public DebugView(JoinBlockTarget<T> joinBlockTarget)
			{
				this._joinBlockTarget = joinBlockTarget;
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000FA36 File Offset: 0x0000DC36
			public IEnumerable<T> InputQueue
			{
				get
				{
					return this._joinBlockTarget._messages;
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000FA43 File Offset: 0x0000DC43
			public bool IsDecliningPermanently
			{
				get
				{
					return this._joinBlockTarget._decliningPermanently || this._joinBlockTarget._sharedResources._decliningPermanently;
				}
			}

			// Token: 0x0400019B RID: 411
			private readonly JoinBlockTarget<T> _joinBlockTarget;
		}
	}
}
