using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000031 RID: 49
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BatchedJoinBlockTarget<>.DebugView))]
	internal sealed class BatchedJoinBlockTarget<T> : ITargetBlock<T>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x00007506 File Offset: 0x00005706
		internal BatchedJoinBlockTarget(BatchedJoinBlockTargetSharedResources sharedResources)
		{
			this._sharedResources = sharedResources;
			sharedResources._remainingAliveTargets++;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x0000752E File Offset: 0x0000572E
		internal int Count
		{
			get
			{
				return this._messages.Count;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000753C File Offset: 0x0000573C
		internal IList<T> GetAndEmptyMessages()
		{
			IList<T> messages = this._messages;
			this._messages = new List<T>();
			return messages;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000755C File Offset: 0x0000575C
		public DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (source == null && consumeToAccept)
			{
				throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
			}
			object incomingLock = this._sharedResources._incomingLock;
			DataflowMessageStatus dataflowMessageStatus;
			lock (incomingLock)
			{
				if (this._decliningPermanently || this._sharedResources._decliningPermanently)
				{
					dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
				}
				else
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
					this._messages.Add(messageValue);
					BatchedJoinBlockTargetSharedResources sharedResources = this._sharedResources;
					int num = sharedResources._remainingItemsInBatch - 1;
					sharedResources._remainingItemsInBatch = num;
					if (num == 0)
					{
						this._sharedResources._batchSizeReachedAction();
					}
					dataflowMessageStatus = DataflowMessageStatus.Accepted;
				}
			}
			return dataflowMessageStatus;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007638 File Offset: 0x00005838
		public void Complete()
		{
			object incomingLock = this._sharedResources._incomingLock;
			lock (incomingLock)
			{
				if (!this._decliningPermanently)
				{
					this._decliningPermanently = true;
					BatchedJoinBlockTargetSharedResources sharedResources = this._sharedResources;
					int num = sharedResources._remainingAliveTargets - 1;
					sharedResources._remainingAliveTargets = num;
					if (num == 0)
					{
						this._sharedResources._allTargetsDecliningPermanentlyAction();
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000076B0 File Offset: 0x000058B0
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			object incomingLock = this._sharedResources._incomingLock;
			lock (incomingLock)
			{
				if (!this._decliningPermanently && !this._sharedResources._decliningPermanently)
				{
					this._sharedResources._exceptionAction(exception);
				}
			}
			this._sharedResources._completionAction();
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00007734 File Offset: 0x00005934
		Task IDataflowBlock.Completion
		{
			get
			{
				throw new NotSupportedException(SR.NotSupported_MemberNotNeeded);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00007740 File Offset: 0x00005940
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0} InputCount={1}", Common.GetNameForDebugger(this, null), this._messages.Count);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00007763 File Offset: 0x00005963
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000063 RID: 99
		private readonly BatchedJoinBlockTargetSharedResources _sharedResources;

		// Token: 0x04000064 RID: 100
		private bool _decliningPermanently;

		// Token: 0x04000065 RID: 101
		private IList<T> _messages = new List<T>();

		// Token: 0x02000078 RID: 120
		private sealed class DebugView
		{
			// Token: 0x06000410 RID: 1040 RVA: 0x0000F945 File Offset: 0x0000DB45
			public DebugView(BatchedJoinBlockTarget<T> batchedJoinBlockTarget)
			{
				this._batchedJoinBlockTarget = batchedJoinBlockTarget;
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000F954 File Offset: 0x0000DB54
			public IEnumerable<T> InputQueue
			{
				get
				{
					return this._batchedJoinBlockTarget._messages;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000F961 File Offset: 0x0000DB61
			public bool IsDecliningPermanently
			{
				get
				{
					return this._batchedJoinBlockTarget._decliningPermanently || this._batchedJoinBlockTarget._sharedResources._decliningPermanently;
				}
			}

			// Token: 0x04000193 RID: 403
			private readonly BatchedJoinBlockTarget<T> _batchedJoinBlockTarget;
		}
	}
}
