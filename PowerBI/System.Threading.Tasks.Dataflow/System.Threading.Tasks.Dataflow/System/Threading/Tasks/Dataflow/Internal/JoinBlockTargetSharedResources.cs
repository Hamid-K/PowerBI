using System;
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000035 RID: 53
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	internal sealed class JoinBlockTargetSharedResources
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000081E0 File Offset: 0x000063E0
		internal JoinBlockTargetSharedResources(IDataflowBlock ownerJoin, JoinBlockTargetBase[] targets, Action joinFilledAction, Action<Exception> exceptionAction, GroupingDataflowBlockOptions dataflowBlockOptions)
		{
			this._ownerJoin = ownerJoin;
			this._targets = targets;
			this._joinFilledAction = joinFilledAction;
			this._exceptionAction = exceptionAction;
			this._dataflowBlockOptions = dataflowBlockOptions;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				this._boundingState = new BoundingState(dataflowBlockOptions.BoundedCapacity);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00008234 File Offset: 0x00006434
		internal object IncomingLock
		{
			get
			{
				return this._targets;
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000823C File Offset: 0x0000643C
		internal void CompleteEachTarget()
		{
			foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
			{
				joinBlockTargetBase.CompleteCore(null, true, false);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000826C File Offset: 0x0000646C
		internal bool AllTargetsHaveAtLeastOneMessage
		{
			get
			{
				foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
				{
					if (!joinBlockTargetBase.HasAtLeastOneMessageAvailable)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x000082A0 File Offset: 0x000064A0
		private bool TargetsHaveAtLeastOneMessageQueuedOrPostponed
		{
			get
			{
				if (this._boundingState == null)
				{
					foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
					{
						if (!joinBlockTargetBase.HasAtLeastOneMessageAvailable && (this._decliningPermanently || joinBlockTargetBase.IsDecliningPermanently || !joinBlockTargetBase.HasAtLeastOnePostponedMessage))
						{
							return false;
						}
					}
					return true;
				}
				bool countIsLessThanBound = this._boundingState.CountIsLessThanBound;
				bool flag = true;
				bool flag2 = false;
				foreach (JoinBlockTargetBase joinBlockTargetBase2 in this._targets)
				{
					bool flag3 = !this._decliningPermanently && !joinBlockTargetBase2.IsDecliningPermanently && joinBlockTargetBase2.HasAtLeastOnePostponedMessage;
					if (this._dataflowBlockOptions.Greedy && flag3 && (countIsLessThanBound || !joinBlockTargetBase2.HasTheHighestNumberOfMessagesAvailable))
					{
						return true;
					}
					bool hasAtLeastOneMessageAvailable = joinBlockTargetBase2.HasAtLeastOneMessageAvailable;
					flag &= hasAtLeastOneMessageAvailable || flag3;
					if (hasAtLeastOneMessageAvailable)
					{
						flag2 = true;
					}
				}
				return flag && (flag2 || countIsLessThanBound);
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00008388 File Offset: 0x00006588
		private bool RetrievePostponedItemsNonGreedy()
		{
			object incomingLock = this.IncomingLock;
			lock (incomingLock)
			{
				if (!this.TargetsHaveAtLeastOneMessageQueuedOrPostponed)
				{
					return false;
				}
			}
			bool flag2 = true;
			foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
			{
				if (!joinBlockTargetBase.ReserveOneMessage())
				{
					flag2 = false;
					break;
				}
			}
			if (flag2)
			{
				foreach (JoinBlockTargetBase joinBlockTargetBase2 in this._targets)
				{
					if (!joinBlockTargetBase2.ConsumeReservedMessage())
					{
						flag2 = false;
						break;
					}
				}
			}
			if (!flag2)
			{
				foreach (JoinBlockTargetBase joinBlockTargetBase3 in this._targets)
				{
					joinBlockTargetBase3.ReleaseReservedMessage();
				}
			}
			return flag2;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00008464 File Offset: 0x00006664
		private bool RetrievePostponedItemsGreedyBounded()
		{
			bool flag = false;
			foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
			{
				flag |= joinBlockTargetBase.ConsumeOnePostponedMessage();
			}
			return flag;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008498 File Offset: 0x00006698
		private bool CanceledOrFaulted
		{
			get
			{
				return this._dataflowBlockOptions.CancellationToken.IsCancellationRequested || this._hasExceptions;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001ED RID: 493 RVA: 0x000084C2 File Offset: 0x000066C2
		internal bool JoinNeedsProcessing
		{
			get
			{
				return this._taskForInputProcessing == null && !this.CanceledOrFaulted && this.TargetsHaveAtLeastOneMessageQueuedOrPostponed;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000084DC File Offset: 0x000066DC
		internal void ProcessAsyncIfNecessary(bool isReplacementReplica = false)
		{
			if (this.JoinNeedsProcessing)
			{
				this.ProcessAsyncIfNecessary_Slow(isReplacementReplica);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000084F0 File Offset: 0x000066F0
		private void ProcessAsyncIfNecessary_Slow(bool isReplacementReplica)
		{
			this._taskForInputProcessing = new Task(delegate(object thisSharedResources)
			{
				((JoinBlockTargetSharedResources)thisSharedResources).ProcessMessagesLoopCore();
			}, this, Common.GetCreationOptionsForTask(isReplacementReplica));
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.TaskLaunchedForMessageHandling(this._ownerJoin, this._taskForInputProcessing, DataflowEtwProvider.TaskLaunchedReason.ProcessingInputMessages, this._targets.Max((JoinBlockTargetBase t) => t.NumberOfMessagesAvailableOrPostponed));
			}
			Exception ex = Common.StartTaskSafe(this._taskForInputProcessing, this._dataflowBlockOptions.TaskScheduler);
			if (ex != null)
			{
				this._exceptionAction(ex);
				this._taskForInputProcessing = null;
				this.CompleteBlockIfPossible();
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000085AC File Offset: 0x000067AC
		internal void CompleteBlockIfPossible()
		{
			if (!this._completionReserved)
			{
				bool flag = this._decliningPermanently && !this.AllTargetsHaveAtLeastOneMessage;
				if (!flag)
				{
					foreach (JoinBlockTargetBase joinBlockTargetBase in this._targets)
					{
						if (joinBlockTargetBase.IsDecliningPermanently && !joinBlockTargetBase.HasAtLeastOneMessageAvailable)
						{
							flag = true;
							break;
						}
					}
				}
				bool flag2 = this._taskForInputProcessing == null && (flag || this.CanceledOrFaulted);
				if (flag2)
				{
					this._completionReserved = true;
					this._decliningPermanently = true;
					Task.Factory.StartNew(delegate(object state)
					{
						JoinBlockTargetSharedResources joinBlockTargetSharedResources = (JoinBlockTargetSharedResources)state;
						foreach (JoinBlockTargetBase joinBlockTargetBase2 in joinBlockTargetSharedResources._targets)
						{
							joinBlockTargetBase2.CompleteOncePossible();
						}
					}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000867C File Offset: 0x0000687C
		private void ProcessMessagesLoopCore()
		{
			try
			{
				int num = 0;
				int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
				bool flag;
				do
				{
					flag = ((!this._dataflowBlockOptions.Greedy) ? this.RetrievePostponedItemsNonGreedy() : this.RetrievePostponedItemsGreedyBounded());
					if (flag)
					{
						object incomingLock = this.IncomingLock;
						lock (incomingLock)
						{
							if (this.AllTargetsHaveAtLeastOneMessage)
							{
								this._joinFilledAction();
								this._joinsCreated += 1L;
								if (!this._dataflowBlockOptions.Greedy && this._boundingState != null)
								{
									this._boundingState.CurrentCount++;
								}
							}
						}
					}
					num++;
				}
				while (flag && num < actualMaxMessagesPerTask);
			}
			catch (Exception ex)
			{
				this._targets[0].CompleteCore(ex, true, true);
			}
			finally
			{
				object incomingLock2 = this.IncomingLock;
				lock (incomingLock2)
				{
					this._taskForInputProcessing = null;
					this.ProcessAsyncIfNecessary(true);
					this.CompleteBlockIfPossible();
				}
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000087B0 File Offset: 0x000069B0
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

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x00008814 File Offset: 0x00006A14
		private object DebuggerDisplayContent
		{
			get
			{
				IDebuggerDisplay debuggerDisplay = this._ownerJoin as IDebuggerDisplay;
				return string.Format("Block=\"{0}\"", (debuggerDisplay != null) ? debuggerDisplay.Content : this._ownerJoin);
			}
		}

		// Token: 0x04000075 RID: 117
		internal readonly IDataflowBlock _ownerJoin;

		// Token: 0x04000076 RID: 118
		internal readonly JoinBlockTargetBase[] _targets;

		// Token: 0x04000077 RID: 119
		internal readonly Action<Exception> _exceptionAction;

		// Token: 0x04000078 RID: 120
		internal readonly Action _joinFilledAction;

		// Token: 0x04000079 RID: 121
		internal readonly GroupingDataflowBlockOptions _dataflowBlockOptions;

		// Token: 0x0400007A RID: 122
		internal readonly BoundingState _boundingState;

		// Token: 0x0400007B RID: 123
		internal bool _decliningPermanently;

		// Token: 0x0400007C RID: 124
		internal Task _taskForInputProcessing;

		// Token: 0x0400007D RID: 125
		internal bool _hasExceptions;

		// Token: 0x0400007E RID: 126
		internal long _joinsCreated;

		// Token: 0x0400007F RID: 127
		private bool _completionReserved;
	}
}
