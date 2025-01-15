using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000044 RID: 68
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	internal sealed class SourceCore<TOutput>
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000946F File Offset: 0x0000766F
		private object OutgoingLock
		{
			get
			{
				return this._completionTask;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00009477 File Offset: 0x00007677
		private object ValueLock
		{
			get
			{
				return this._targetRegistry;
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009480 File Offset: 0x00007680
		internal SourceCore(ISourceBlock<TOutput> owningSource, DataflowBlockOptions dataflowBlockOptions, Action<ISourceBlock<TOutput>> completeAction, Action<ISourceBlock<TOutput>, int> itemsRemovedAction = null, Func<ISourceBlock<TOutput>, TOutput, IList<TOutput>, int> itemCountingFunc = null)
		{
			this._owningSource = owningSource;
			this._dataflowBlockOptions = dataflowBlockOptions;
			this._itemsRemovedAction = itemsRemovedAction;
			this._itemCountingFunc = itemCountingFunc;
			this._completeAction = completeAction;
			this._targetRegistry = new TargetRegistry<TOutput>(this._owningSource);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009500 File Offset: 0x00007700
		internal IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (linkOptions == null)
			{
				throw new ArgumentNullException("linkOptions");
			}
			if (this._completionTask.Task.IsCompleted)
			{
				if (linkOptions.PropagateCompletion)
				{
					Common.PropagateCompletion(this._completionTask.Task, target, null);
				}
				return Disposables.Nop;
			}
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (!this._completionReserved)
				{
					this._targetRegistry.Add(ref target, linkOptions);
					this.OfferToTargets(target);
					return Common.CreateUnlinker<TOutput>(this.OutgoingLock, this._targetRegistry, target);
				}
			}
			if (linkOptions.PropagateCompletion)
			{
				Common.PropagateCompletionOnceCompleted(this._completionTask.Task, target);
			}
			return Disposables.Nop;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000095DC File Offset: 0x000077DC
		internal TOutput ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			TOutput toutput = default(TOutput);
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (this._nextMessageReservedFor != target && this._nextMessageReservedFor != null)
				{
					messageConsumed = false;
					return default(TOutput);
				}
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (messageHeader.Id != this._nextMessageId.Value || !this._messages.TryDequeue(out toutput))
					{
						messageConsumed = false;
						return default(TOutput);
					}
					this._nextMessageReservedFor = null;
					this._targetRegistry.Remove(target, true);
					this._enableOffering = true;
					this._nextMessageId.Value = this._nextMessageId.Value + 1L;
					this.CompleteBlockIfPossible();
					this.OfferAsyncIfNecessary(false, true);
				}
			}
			if (this._itemsRemovedAction != null)
			{
				int num = ((this._itemCountingFunc != null) ? this._itemCountingFunc(this._owningSource, toutput, null) : 1);
				this._itemsRemovedAction(this._owningSource, num);
			}
			messageConsumed = true;
			return toutput;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00009748 File Offset: 0x00007948
		internal bool ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (this._nextMessageReservedFor == null)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						if (messageHeader.Id == this._nextMessageId.Value && !this._messages.IsEmpty)
						{
							this._nextMessageReservedFor = target;
							this._enableOffering = false;
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009814 File Offset: 0x00007A14
		internal void ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (this._nextMessageReservedFor != target)
				{
					throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
				}
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (messageHeader.Id != this._nextMessageId.Value || this._messages.IsEmpty)
					{
						throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
					}
					this._nextMessageReservedFor = null;
					this._enableOffering = true;
					this.OfferAsyncIfNecessary(false, true);
					this.CompleteBlockIfPossible();
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000246 RID: 582 RVA: 0x000098F8 File Offset: 0x00007AF8
		internal Task Completion
		{
			get
			{
				return this._completionTask.Task;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009908 File Offset: 0x00007B08
		internal bool TryReceive(Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item)
		{
			item = default(TOutput);
			bool flag = false;
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (this._nextMessageReservedFor == null)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						if (this._messages.TryDequeueIf(filter, out item))
						{
							this._nextMessageId.Value = this._nextMessageId.Value + 1L;
							this._enableOffering = true;
							this.CompleteBlockIfPossible();
							this.OfferAsyncIfNecessary(false, true);
							flag = true;
						}
					}
				}
			}
			if (flag && this._itemsRemovedAction != null)
			{
				int num = ((this._itemCountingFunc != null) ? this._itemCountingFunc(this._owningSource, item, null) : 1);
				this._itemsRemovedAction(this._owningSource, num);
			}
			return flag;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000099FC File Offset: 0x00007BFC
		internal bool TryReceiveAll([NotNullWhen(true)] out IList<TOutput> items)
		{
			items = null;
			int num = 0;
			object outgoingLock = this.OutgoingLock;
			lock (outgoingLock)
			{
				if (this._nextMessageReservedFor == null)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						if (!this._messages.IsEmpty)
						{
							List<TOutput> list = new List<TOutput>();
							TOutput toutput;
							while (this._messages.TryDequeue(out toutput))
							{
								list.Add(toutput);
							}
							num = list.Count;
							items = list;
							this._nextMessageId.Value = this._nextMessageId.Value + 1L;
							this._enableOffering = true;
							this.CompleteBlockIfPossible();
						}
					}
				}
			}
			if (num > 0)
			{
				if (this._itemsRemovedAction != null)
				{
					int num2 = ((this._itemCountingFunc != null) ? this._itemCountingFunc(this._owningSource, default(TOutput), items) : num);
					this._itemsRemovedAction(this._owningSource, num2);
				}
				return true;
			}
			return false;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000249 RID: 585 RVA: 0x00009B14 File Offset: 0x00007D14
		internal int OutputCount
		{
			get
			{
				object outgoingLock = this.OutgoingLock;
				int count;
				lock (outgoingLock)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						count = this._messages.Count;
					}
				}
				return count;
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00009B84 File Offset: 0x00007D84
		internal void AddMessage(TOutput item)
		{
			if (this._decliningPermanently)
			{
				return;
			}
			this._messages.Enqueue(item);
			Interlocked.MemoryBarrier();
			if (this._taskForOutputProcessing == null)
			{
				this.OfferAsyncIfNecessaryWithValueLock();
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009BB0 File Offset: 0x00007DB0
		internal void AddMessages(IEnumerable<TOutput> items)
		{
			if (this._decliningPermanently)
			{
				return;
			}
			List<TOutput> list = items as List<TOutput>;
			if (list != null)
			{
				for (int i = 0; i < list.Count; i++)
				{
					this._messages.Enqueue(list[i]);
				}
			}
			else
			{
				TOutput[] array = items as TOutput[];
				if (array != null)
				{
					for (int j = 0; j < array.Length; j++)
					{
						this._messages.Enqueue(array[j]);
					}
				}
				else
				{
					foreach (TOutput toutput in items)
					{
						this._messages.Enqueue(toutput);
					}
				}
			}
			Interlocked.MemoryBarrier();
			if (this._taskForOutputProcessing == null)
			{
				this.OfferAsyncIfNecessaryWithValueLock();
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009C7C File Offset: 0x00007E7C
		internal void AddException(Exception exception)
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				Common.AddException(ref this._exceptions, exception, false);
			}
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009CC4 File Offset: 0x00007EC4
		internal void AddExceptions(List<Exception> exceptions)
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				foreach (Exception ex in exceptions)
				{
					Common.AddException(ref this._exceptions, ex, false);
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009D40 File Offset: 0x00007F40
		internal void AddAndUnwrapAggregateException(AggregateException aggregateException)
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				Common.AddException(ref this._exceptions, aggregateException, true);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600024F RID: 591 RVA: 0x00009D88 File Offset: 0x00007F88
		internal bool HasExceptions
		{
			get
			{
				return Volatile.Read<List<Exception>>(ref this._exceptions) != null;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009D98 File Offset: 0x00007F98
		internal void Complete()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				this._decliningPermanently = true;
				Task.Factory.StartNew(delegate(object state)
				{
					SourceCore<TOutput> sourceCore = (SourceCore<TOutput>)state;
					object outgoingLock = sourceCore.OutgoingLock;
					lock (outgoingLock)
					{
						object valueLock2 = sourceCore.ValueLock;
						lock (valueLock2)
						{
							sourceCore.CompleteBlockIfPossible();
						}
					}
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00009E14 File Offset: 0x00008014
		internal DataflowBlockOptions DataflowBlockOptions
		{
			get
			{
				return this._dataflowBlockOptions;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009E1C File Offset: 0x0000801C
		private bool OfferToTargets(ITargetBlock<TOutput> linkToTarget = null)
		{
			if (this._nextMessageReservedFor != null)
			{
				return false;
			}
			DataflowMessageHeader dataflowMessageHeader = default(DataflowMessageHeader);
			TOutput toutput = default(TOutput);
			bool flag = false;
			if (!Volatile.Read(ref this._enableOffering))
			{
				if (linkToTarget == null)
				{
					return false;
				}
				flag = true;
			}
			if (this._messages.TryPeek(out toutput))
			{
				dataflowMessageHeader = new DataflowMessageHeader(this._nextMessageId.Value);
			}
			bool flag2 = false;
			if (dataflowMessageHeader.IsValid)
			{
				if (flag)
				{
					this.OfferMessageToTarget(dataflowMessageHeader, toutput, linkToTarget, out flag2);
				}
				else
				{
					TargetRegistry<TOutput>.LinkedTargetInfo next;
					for (TargetRegistry<TOutput>.LinkedTargetInfo linkedTargetInfo = this._targetRegistry.FirstTargetNode; linkedTargetInfo != null; linkedTargetInfo = next)
					{
						next = linkedTargetInfo.Next;
						if (this.OfferMessageToTarget(dataflowMessageHeader, toutput, linkedTargetInfo.Target, out flag2))
						{
							break;
						}
					}
					if (!flag2)
					{
						object valueLock = this.ValueLock;
						lock (valueLock)
						{
							this._enableOffering = false;
						}
					}
				}
			}
			if (flag2)
			{
				object valueLock2 = this.ValueLock;
				lock (valueLock2)
				{
					if (this._nextMessageId.Value == dataflowMessageHeader.Id)
					{
						TOutput toutput2;
						this._messages.TryDequeue(out toutput2);
					}
					this._nextMessageId.Value = this._nextMessageId.Value + 1L;
					this._enableOffering = true;
					if (linkToTarget != null)
					{
						this.CompleteBlockIfPossible();
						this.OfferAsyncIfNecessary(false, true);
					}
				}
				if (this._itemsRemovedAction != null)
				{
					int num = ((this._itemCountingFunc != null) ? this._itemCountingFunc(this._owningSource, toutput, null) : 1);
					this._itemsRemovedAction(this._owningSource, num);
				}
			}
			return flag2;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009FC0 File Offset: 0x000081C0
		private bool OfferMessageToTarget(DataflowMessageHeader header, TOutput message, ITargetBlock<TOutput> target, out bool messageWasAccepted)
		{
			DataflowMessageStatus dataflowMessageStatus = target.OfferMessage(header, message, this._owningSource, false);
			messageWasAccepted = false;
			if (dataflowMessageStatus == DataflowMessageStatus.Accepted)
			{
				this._targetRegistry.Remove(target, true);
				messageWasAccepted = true;
				return true;
			}
			if (dataflowMessageStatus == DataflowMessageStatus.DecliningPermanently)
			{
				this._targetRegistry.Remove(target, false);
			}
			else if (this._nextMessageReservedFor != null)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000A018 File Offset: 0x00008218
		private void OfferAsyncIfNecessaryWithValueLock()
		{
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				this.OfferAsyncIfNecessary(false, false);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000A05C File Offset: 0x0000825C
		private void OfferAsyncIfNecessary(bool isReplacementReplica, bool outgoingLockKnownAcquired)
		{
			if (this._taskForOutputProcessing == null && this._enableOffering && !this._messages.IsEmpty)
			{
				this.OfferAsyncIfNecessary_Slow(isReplacementReplica, outgoingLockKnownAcquired);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000A084 File Offset: 0x00008284
		private void OfferAsyncIfNecessary_Slow(bool isReplacementReplica, bool outgoingLockKnownAcquired)
		{
			bool flag = true;
			if (outgoingLockKnownAcquired || Monitor.IsEntered(this.OutgoingLock))
			{
				flag = this._targetRegistry.FirstTargetNode != null;
			}
			if (flag && !this.CanceledOrFaulted)
			{
				this._taskForOutputProcessing = new Task(delegate(object thisSourceCore)
				{
					((SourceCore<TOutput>)thisSourceCore).OfferMessagesLoopCore();
				}, this, Common.GetCreationOptionsForTask(isReplacementReplica));
				DataflowEtwProvider log = DataflowEtwProvider.Log;
				if (log.IsEnabled())
				{
					log.TaskLaunchedForMessageHandling(this._owningSource, this._taskForOutputProcessing, DataflowEtwProvider.TaskLaunchedReason.OfferingOutputMessages, this._messages.Count);
				}
				Exception ex = Common.StartTaskSafe(this._taskForOutputProcessing, this._dataflowBlockOptions.TaskScheduler);
				if (ex != null)
				{
					this.AddException(ex);
					this._taskForOutputProcessing = null;
					this._decliningPermanently = true;
					Task.Factory.StartNew(delegate(object state)
					{
						SourceCore<TOutput> sourceCore = (SourceCore<TOutput>)state;
						object outgoingLock = sourceCore.OutgoingLock;
						lock (outgoingLock)
						{
							object valueLock = sourceCore.ValueLock;
							lock (valueLock)
							{
								sourceCore.CompleteBlockIfPossible();
							}
						}
					}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A18C File Offset: 0x0000838C
		private void OfferMessagesLoopCore()
		{
			try
			{
				int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
				int num = ((this._dataflowBlockOptions.MaxMessagesPerTask == -1) ? 10 : actualMaxMessagesPerTask);
				int num2 = 0;
				while (num2 < actualMaxMessagesPerTask && !this.CanceledOrFaulted)
				{
					object outgoingLock = this.OutgoingLock;
					lock (outgoingLock)
					{
						int num3 = 0;
						while (num2 < actualMaxMessagesPerTask && num3 < num && !this.CanceledOrFaulted)
						{
							if (!this.OfferToTargets(null))
							{
								return;
							}
							num2++;
							num3++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				this.AddException(ex);
				this._completeAction(this._owningSource);
			}
			finally
			{
				object outgoingLock2 = this.OutgoingLock;
				lock (outgoingLock2)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						this._taskForOutputProcessing = null;
						Interlocked.MemoryBarrier();
						this.OfferAsyncIfNecessary(true, true);
						this.CompleteBlockIfPossible();
					}
				}
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000A2D4 File Offset: 0x000084D4
		private bool CanceledOrFaulted
		{
			get
			{
				return this._dataflowBlockOptions.CancellationToken.IsCancellationRequested || (this.HasExceptions && this._decliningPermanently);
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A308 File Offset: 0x00008508
		private void CompleteBlockIfPossible()
		{
			if (!this._completionReserved && this._decliningPermanently && this._taskForOutputProcessing == null && this._nextMessageReservedFor == null)
			{
				this.CompleteBlockIfPossible_Slow();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A330 File Offset: 0x00008530
		private void CompleteBlockIfPossible_Slow()
		{
			if (this._messages.IsEmpty || this.CanceledOrFaulted)
			{
				this._completionReserved = true;
				Task.Factory.StartNew(delegate(object state)
				{
					((SourceCore<TOutput>)state).CompleteBlockOncePossible();
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A394 File Offset: 0x00008594
		private void CompleteBlockOncePossible()
		{
			object outgoingLock = this.OutgoingLock;
			TargetRegistry<TOutput>.LinkedTargetInfo linkedTargetInfo;
			List<Exception> exceptions;
			lock (outgoingLock)
			{
				linkedTargetInfo = this._targetRegistry.ClearEntryPoints();
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this._messages.Clear();
					exceptions = this._exceptions;
					this._exceptions = null;
				}
			}
			if (exceptions != null)
			{
				this._completionTask.TrySetException(exceptions);
			}
			else if (this._dataflowBlockOptions.CancellationToken.IsCancellationRequested)
			{
				this._completionTask.TrySetCanceled();
			}
			else
			{
				this._completionTask.TrySetResult(default(VoidResult));
			}
			this._targetRegistry.PropagateCompletion(linkedTargetInfo);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCompleted(this._owningSource);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A494 File Offset: 0x00008694
		private object DebuggerDisplayContent
		{
			get
			{
				IDebuggerDisplay debuggerDisplay = this._owningSource as IDebuggerDisplay;
				return string.Format("Block=\"{0}\"", (debuggerDisplay != null) ? debuggerDisplay.Content : this._owningSource);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000A4C8 File Offset: 0x000086C8
		internal SourceCore<TOutput>.DebuggingInformation GetDebuggingInformation()
		{
			return new SourceCore<TOutput>.DebuggingInformation(this);
		}

		// Token: 0x040000A2 RID: 162
		private readonly TaskCompletionSource<VoidResult> _completionTask = new TaskCompletionSource<VoidResult>();

		// Token: 0x040000A3 RID: 163
		private readonly TargetRegistry<TOutput> _targetRegistry;

		// Token: 0x040000A4 RID: 164
		private readonly SingleProducerSingleConsumerQueue<TOutput> _messages = new SingleProducerSingleConsumerQueue<TOutput>();

		// Token: 0x040000A5 RID: 165
		private readonly ISourceBlock<TOutput> _owningSource;

		// Token: 0x040000A6 RID: 166
		private readonly DataflowBlockOptions _dataflowBlockOptions;

		// Token: 0x040000A7 RID: 167
		private readonly Action<ISourceBlock<TOutput>> _completeAction;

		// Token: 0x040000A8 RID: 168
		private readonly Action<ISourceBlock<TOutput>, int> _itemsRemovedAction;

		// Token: 0x040000A9 RID: 169
		private readonly Func<ISourceBlock<TOutput>, TOutput, IList<TOutput>, int> _itemCountingFunc;

		// Token: 0x040000AA RID: 170
		private Task _taskForOutputProcessing;

		// Token: 0x040000AB RID: 171
		private PaddedInt64 _nextMessageId = new PaddedInt64
		{
			Value = 1L
		};

		// Token: 0x040000AC RID: 172
		private ITargetBlock<TOutput> _nextMessageReservedFor;

		// Token: 0x040000AD RID: 173
		private bool _decliningPermanently;

		// Token: 0x040000AE RID: 174
		private bool _enableOffering = true;

		// Token: 0x040000AF RID: 175
		private bool _completionReserved;

		// Token: 0x040000B0 RID: 176
		private List<Exception> _exceptions;

		// Token: 0x02000088 RID: 136
		internal sealed class DebuggingInformation
		{
			// Token: 0x0600043D RID: 1085 RVA: 0x0000FE5B File Offset: 0x0000E05B
			internal DebuggingInformation(SourceCore<TOutput> source)
			{
				this._source = source;
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000FE6A File Offset: 0x0000E06A
			internal int OutputCount
			{
				get
				{
					return this._source._messages.Count;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000FE7C File Offset: 0x0000E07C
			internal IEnumerable<TOutput> OutputQueue
			{
				get
				{
					return this._source._messages.ToList<TOutput>();
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000FE8E File Offset: 0x0000E08E
			internal Task TaskForOutputProcessing
			{
				get
				{
					return this._source._taskForOutputProcessing;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000FE9B File Offset: 0x0000E09B
			internal DataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._source._dataflowBlockOptions;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
			internal bool IsCompleted
			{
				get
				{
					return this._source.Completion.IsCompleted;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000FEBA File Offset: 0x0000E0BA
			internal TargetRegistry<TOutput> LinkedTargets
			{
				get
				{
					return this._source._targetRegistry;
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000FEC7 File Offset: 0x0000E0C7
			internal ITargetBlock<TOutput> NextMessageReservedFor
			{
				get
				{
					return this._source._nextMessageReservedFor;
				}
			}

			// Token: 0x040001BC RID: 444
			private readonly SourceCore<TOutput> _source;
		}
	}
}
