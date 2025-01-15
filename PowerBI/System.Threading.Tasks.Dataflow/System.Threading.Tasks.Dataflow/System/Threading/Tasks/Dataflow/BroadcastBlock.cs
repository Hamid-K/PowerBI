using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BroadcastBlock<>.DebugView))]
	public sealed class BroadcastBlock<[Nullable(2)] T> : IPropagatorBlock<T, T>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T>, IReceivableSourceBlock<T>, IDebuggerDisplay
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004800 File Offset: 0x00002A00
		private object IncomingLock
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004808 File Offset: 0x00002A08
		public BroadcastBlock([Nullable(new byte[] { 2, 1, 1 })] Func<T, T> cloningFunction)
			: this(cloningFunction, DataflowBlockOptions.Default)
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004818 File Offset: 0x00002A18
		public BroadcastBlock([Nullable(new byte[] { 2, 1, 1 })] Func<T, T> cloningFunction, DataflowBlockOptions dataflowBlockOptions)
		{
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<int> action = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = new Action<int>(this.OnItemsRemoved);
				this._boundingState = new BoundingStateWithPostponedAndTask<T>(dataflowBlockOptions.BoundedCapacity);
			}
			this._source = new BroadcastBlock<T>.BroadcastingSourceCore<T>(this, cloningFunction, dataflowBlockOptions, action);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (BroadcastBlock<T>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object state)
			{
				((BroadcastBlock<T>)state).Complete();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000490C File Offset: 0x00002B0C
		public void Complete()
		{
			this.CompleteCore(null, false, false);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004917 File Offset: 0x00002B17
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.CompleteCore(exception, false, false);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004930 File Offset: 0x00002B30
		internal void CompleteCore(Exception exception, bool storeExceptionEvenIfAlreadyCompleting, bool revertProcessingState = false)
		{
			object incomingLock = this.IncomingLock;
			lock (incomingLock)
			{
				if (exception != null && (!this._decliningPermanently || storeExceptionEvenIfAlreadyCompleting))
				{
					this._source.AddException(exception);
				}
				if (revertProcessingState)
				{
					this._boundingState.TaskForInputProcessing = null;
				}
				this._decliningPermanently = true;
				this.CompleteTargetIfPossible();
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000049A4 File Offset: 0x00002BA4
		public IDisposable LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000049B3 File Offset: 0x00002BB3
		public bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<T> filter, [MaybeNullWhen(false)] out T item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000049C2 File Offset: 0x00002BC2
		bool IReceivableSourceBlock<T>.TryReceiveAll([NotNullWhen(true)] out IList<T> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000049D0 File Offset: 0x00002BD0
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000049E0 File Offset: 0x00002BE0
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
			object incomingLock = this.IncomingLock;
			DataflowMessageStatus dataflowMessageStatus;
			lock (incomingLock)
			{
				if (this._decliningPermanently)
				{
					this.CompleteTargetIfPossible();
					dataflowMessageStatus = DataflowMessageStatus.DecliningPermanently;
				}
				else if (this._boundingState == null || (this._boundingState.CountIsLessThanBound && this._boundingState.PostponedMessages.Count == 0 && this._boundingState.TaskForInputProcessing == null))
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
					this._source.AddMessage(messageValue);
					if (this._boundingState != null)
					{
						this._boundingState.CurrentCount++;
					}
					dataflowMessageStatus = DataflowMessageStatus.Accepted;
				}
				else if (source != null)
				{
					this._boundingState.PostponedMessages.Push(source, messageHeader);
					dataflowMessageStatus = DataflowMessageStatus.Postponed;
				}
				else
				{
					dataflowMessageStatus = DataflowMessageStatus.Declined;
				}
			}
			return dataflowMessageStatus;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004AF4 File Offset: 0x00002CF4
		private void OnItemsRemoved(int numItemsRemoved)
		{
			if (this._boundingState != null)
			{
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					this._boundingState.CurrentCount -= numItemsRemoved;
					this.ConsumeAsyncIfNecessary(false);
					this.CompleteTargetIfPossible();
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004B58 File Offset: 0x00002D58
		internal void ConsumeAsyncIfNecessary(bool isReplacementReplica = false)
		{
			if (!this._decliningPermanently && this._boundingState.TaskForInputProcessing == null && this._boundingState.PostponedMessages.Count > 0 && this._boundingState.CountIsLessThanBound)
			{
				this._boundingState.TaskForInputProcessing = new Task(delegate(object state)
				{
					((BroadcastBlock<T>)state).ConsumeMessagesLoopCore();
				}, this, Common.GetCreationOptionsForTask(isReplacementReplica));
				DataflowEtwProvider log = DataflowEtwProvider.Log;
				if (log.IsEnabled())
				{
					log.TaskLaunchedForMessageHandling(this, this._boundingState.TaskForInputProcessing, DataflowEtwProvider.TaskLaunchedReason.ProcessingInputMessages, this._boundingState.PostponedMessages.Count);
				}
				Exception ex = Common.StartTaskSafe(this._boundingState.TaskForInputProcessing, this._source.DataflowBlockOptions.TaskScheduler);
				if (ex != null)
				{
					Task.Factory.StartNew(delegate(object exc)
					{
						this.CompleteCore((Exception)exc, true, true);
					}, ex, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004C5C File Offset: 0x00002E5C
		private void ConsumeMessagesLoopCore()
		{
			try
			{
				int actualMaxMessagesPerTask = this._source.DataflowBlockOptions.ActualMaxMessagesPerTask;
				int num = 0;
				while (num < actualMaxMessagesPerTask && this.ConsumeAndStoreOneMessageIfAvailable())
				{
					num++;
				}
			}
			catch (Exception ex)
			{
				this.CompleteCore(ex, true, false);
			}
			finally
			{
				object incomingLock = this.IncomingLock;
				lock (incomingLock)
				{
					this._boundingState.TaskForInputProcessing = null;
					this.ConsumeAsyncIfNecessary(true);
					this.CompleteTargetIfPossible();
				}
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004D00 File Offset: 0x00002F00
		private bool ConsumeAndStoreOneMessageIfAvailable()
		{
			bool flag2;
			for (;;)
			{
				object incomingLock = this.IncomingLock;
				KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair;
				lock (incomingLock)
				{
					if (!this._boundingState.CountIsLessThanBound)
					{
						flag2 = false;
						break;
					}
					if (!this._boundingState.PostponedMessages.TryPop(out keyValuePair))
					{
						flag2 = false;
						break;
					}
					this._boundingState.CurrentCount++;
				}
				bool flag3 = false;
				try
				{
					T t = keyValuePair.Key.ConsumeMessage(keyValuePair.Value, this, out flag3);
					if (!flag3)
					{
						continue;
					}
					this._source.AddMessage(t);
					flag2 = true;
				}
				finally
				{
					if (!flag3)
					{
						object incomingLock2 = this.IncomingLock;
						lock (incomingLock2)
						{
							this._boundingState.CurrentCount--;
						}
					}
				}
				break;
			}
			return flag2;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004E08 File Offset: 0x00003008
		private void CompleteTargetIfPossible()
		{
			if (this._decliningPermanently && !this._completionReserved && (this._boundingState == null || this._boundingState.TaskForInputProcessing == null))
			{
				this._completionReserved = true;
				if (this._boundingState != null && this._boundingState.PostponedMessages.Count > 0)
				{
					Task.Factory.StartNew(delegate(object state)
					{
						BroadcastBlock<T> broadcastBlock = (BroadcastBlock<T>)state;
						List<Exception> list = null;
						if (broadcastBlock._boundingState != null)
						{
							Common.ReleaseAllPostponedMessages<T>(broadcastBlock, broadcastBlock._boundingState.PostponedMessages, ref list);
						}
						if (list != null)
						{
							broadcastBlock._source.AddExceptions(list);
						}
						broadcastBlock._source.Complete();
					}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
					return;
				}
				this._source.Complete();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004EA6 File Offset: 0x000030A6
		T ISourceBlock<T>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004EB6 File Offset: 0x000030B6
		bool ISourceBlock<T>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004EC5 File Offset: 0x000030C5
		void ISourceBlock<T>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004ED4 File Offset: 0x000030D4
		private bool HasValueForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().HasValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004EE6 File Offset: 0x000030E6
		private T ValueForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().Value;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004EF8 File Offset: 0x000030F8
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00004F0B File Offset: 0x0000310B
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, HasValue={1}, Value={2}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.HasValueForDebugger, this.ValueForDebugger);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00004F3E File Offset: 0x0000313E
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000044 RID: 68
		private readonly BroadcastBlock<T>.BroadcastingSourceCore<T> _source;

		// Token: 0x04000045 RID: 69
		private readonly BoundingStateWithPostponedAndTask<T> _boundingState;

		// Token: 0x04000046 RID: 70
		private bool _decliningPermanently;

		// Token: 0x04000047 RID: 71
		private bool _completionReserved;

		// Token: 0x02000067 RID: 103
		private sealed class DebugView
		{
			// Token: 0x0600036E RID: 878 RVA: 0x0000E2F9 File Offset: 0x0000C4F9
			public DebugView(BroadcastBlock<T> broadcastBlock)
			{
				this._broadcastBlock = broadcastBlock;
				this._sourceDebuggingInformation = broadcastBlock._source.GetDebuggingInformation();
			}

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0000E319 File Offset: 0x0000C519
			public IEnumerable<T> InputQueue
			{
				get
				{
					return this._sourceDebuggingInformation.InputQueue;
				}
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0000E326 File Offset: 0x0000C526
			public bool HasValue
			{
				get
				{
					return this._broadcastBlock.HasValueForDebugger;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000371 RID: 881 RVA: 0x0000E333 File Offset: 0x0000C533
			public T Value
			{
				get
				{
					return this._broadcastBlock.ValueForDebugger;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000372 RID: 882 RVA: 0x0000E340 File Offset: 0x0000C540
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x17000115 RID: 277
			// (get) Token: 0x06000373 RID: 883 RVA: 0x0000E34D File Offset: 0x0000C54D
			public DataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x17000116 RID: 278
			// (get) Token: 0x06000374 RID: 884 RVA: 0x0000E35A File Offset: 0x0000C55A
			public bool IsDecliningPermanently
			{
				get
				{
					return this._broadcastBlock._decliningPermanently;
				}
			}

			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000375 RID: 885 RVA: 0x0000E367 File Offset: 0x0000C567
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000376 RID: 886 RVA: 0x0000E374 File Offset: 0x0000C574
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._broadcastBlock);
				}
			}

			// Token: 0x17000119 RID: 281
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000E381 File Offset: 0x0000C581
			public TargetRegistry<T> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x1700011A RID: 282
			// (get) Token: 0x06000378 RID: 888 RVA: 0x0000E38E File Offset: 0x0000C58E
			public ITargetBlock<T> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x04000144 RID: 324
			private readonly BroadcastBlock<T> _broadcastBlock;

			// Token: 0x04000145 RID: 325
			private readonly BroadcastBlock<T>.BroadcastingSourceCore<T>.DebuggingInformation _sourceDebuggingInformation;
		}

		// Token: 0x02000068 RID: 104
		[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
		private sealed class BroadcastingSourceCore<TOutput>
		{
			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000379 RID: 889 RVA: 0x0000E39B File Offset: 0x0000C59B
			private object OutgoingLock
			{
				get
				{
					return this._completionTask;
				}
			}

			// Token: 0x1700011C RID: 284
			// (get) Token: 0x0600037A RID: 890 RVA: 0x0000E3A3 File Offset: 0x0000C5A3
			private object ValueLock
			{
				get
				{
					return this._targetRegistry;
				}
			}

			// Token: 0x0600037B RID: 891 RVA: 0x0000E3AC File Offset: 0x0000C5AC
			internal BroadcastingSourceCore(BroadcastBlock<TOutput> owningSource, Func<TOutput, TOutput> cloningFunction, DataflowBlockOptions dataflowBlockOptions, Action<int> itemsRemovedAction)
			{
				this._owningSource = owningSource;
				this._cloningFunction = cloningFunction;
				this._dataflowBlockOptions = dataflowBlockOptions;
				this._itemsRemovedAction = itemsRemovedAction;
				this._targetRegistry = new TargetRegistry<TOutput>(this._owningSource);
			}

			// Token: 0x0600037C RID: 892 RVA: 0x0000E40C File Offset: 0x0000C60C
			internal bool TryReceive(Predicate<TOutput> filter, [MaybeNullWhen(false)] out TOutput item)
			{
				object outgoingLock = this.OutgoingLock;
				TOutput currentMessage;
				bool currentMessageIsValid;
				lock (outgoingLock)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						currentMessage = this._currentMessage;
						currentMessageIsValid = this._currentMessageIsValid;
					}
				}
				if (currentMessageIsValid && (filter == null || filter(currentMessage)))
				{
					item = this.CloneItem(currentMessage);
					return true;
				}
				item = default(TOutput);
				return false;
			}

			// Token: 0x0600037D RID: 893 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
			internal bool TryReceiveAll([NotNullWhen(true)] out IList<TOutput> items)
			{
				TOutput toutput;
				if (this.TryReceive(null, out toutput))
				{
					items = new TOutput[] { toutput };
					return true;
				}
				items = null;
				return false;
			}

			// Token: 0x0600037E RID: 894 RVA: 0x0000E4D8 File Offset: 0x0000C6D8
			internal void AddMessage(TOutput item)
			{
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (!this._decliningPermanently)
					{
						this._messages.Enqueue(item);
						if (this._messages.Count == 1)
						{
							this._enableOffering = true;
						}
						this.OfferAsyncIfNecessary(false);
					}
				}
			}

			// Token: 0x0600037F RID: 895 RVA: 0x0000E544 File Offset: 0x0000C744
			internal void Complete()
			{
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					this._decliningPermanently = true;
					Task.Factory.StartNew(delegate(object state)
					{
						BroadcastBlock<T>.BroadcastingSourceCore<TOutput> broadcastingSourceCore = (BroadcastBlock<T>.BroadcastingSourceCore<TOutput>)state;
						object outgoingLock = broadcastingSourceCore.OutgoingLock;
						lock (outgoingLock)
						{
							object valueLock2 = broadcastingSourceCore.ValueLock;
							lock (valueLock2)
							{
								broadcastingSourceCore.CompleteBlockIfPossible();
							}
						}
					}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
				}
			}

			// Token: 0x06000380 RID: 896 RVA: 0x0000E5C0 File Offset: 0x0000C7C0
			private TOutput CloneItem(TOutput item)
			{
				if (this._cloningFunction == null)
				{
					return item;
				}
				return this._cloningFunction(item);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
			private void OfferCurrentMessageToNewTarget(ITargetBlock<TOutput> target)
			{
				object valueLock = this.ValueLock;
				TOutput currentMessage;
				bool currentMessageIsValid;
				lock (valueLock)
				{
					currentMessage = this._currentMessage;
					currentMessageIsValid = this._currentMessageIsValid;
				}
				if (!currentMessageIsValid)
				{
					return;
				}
				bool flag2 = this._cloningFunction != null;
				DataflowMessageStatus dataflowMessageStatus = target.OfferMessage(new DataflowMessageHeader(this._nextMessageId), currentMessage, this._owningSource, flag2);
				if (dataflowMessageStatus == DataflowMessageStatus.Accepted)
				{
					if (!flag2)
					{
						this._targetRegistry.Remove(target, true);
						return;
					}
				}
				else if (dataflowMessageStatus == DataflowMessageStatus.DecliningPermanently)
				{
					this._targetRegistry.Remove(target, false);
				}
			}

			// Token: 0x06000382 RID: 898 RVA: 0x0000E674 File Offset: 0x0000C874
			private bool OfferToTargets()
			{
				DataflowMessageHeader dataflowMessageHeader = default(DataflowMessageHeader);
				TOutput toutput = default(TOutput);
				int num = 0;
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					if (this._nextMessageReservedFor != null || this._messages.Count <= 0)
					{
						this._enableOffering = false;
						return false;
					}
					if (this._targetRegistry.FirstTargetNode == null)
					{
						while (this._messages.Count > 1)
						{
							this._messages.Dequeue();
							num++;
						}
					}
					toutput = (this._currentMessage = this._messages.Dequeue());
					num++;
					this._currentMessageIsValid = true;
					long num2 = this._nextMessageId + 1L;
					this._nextMessageId = num2;
					dataflowMessageHeader = new DataflowMessageHeader(num2);
					if (this._messages.Count == 0)
					{
						this._enableOffering = false;
					}
				}
				if (dataflowMessageHeader.IsValid)
				{
					if (this._itemsRemovedAction != null)
					{
						this._itemsRemovedAction(num);
					}
					TargetRegistry<TOutput>.LinkedTargetInfo next;
					for (TargetRegistry<TOutput>.LinkedTargetInfo linkedTargetInfo = this._targetRegistry.FirstTargetNode; linkedTargetInfo != null; linkedTargetInfo = next)
					{
						next = linkedTargetInfo.Next;
						ITargetBlock<TOutput> target = linkedTargetInfo.Target;
						this.OfferMessageToTarget(dataflowMessageHeader, toutput, target);
					}
				}
				return true;
			}

			// Token: 0x06000383 RID: 899 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
			private void OfferMessageToTarget(DataflowMessageHeader header, TOutput message, ITargetBlock<TOutput> target)
			{
				bool flag = this._cloningFunction != null;
				switch (target.OfferMessage(header, message, this._owningSource, flag))
				{
				case DataflowMessageStatus.Accepted:
					if (!flag)
					{
						this._targetRegistry.Remove(target, true);
						return;
					}
					break;
				case DataflowMessageStatus.Declined:
				case DataflowMessageStatus.Postponed:
				case DataflowMessageStatus.NotAvailable:
					break;
				case DataflowMessageStatus.DecliningPermanently:
					this._targetRegistry.Remove(target, false);
					break;
				default:
					return;
				}
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000E818 File Offset: 0x0000CA18
			private void OfferAsyncIfNecessary(bool isReplacementReplica = false)
			{
				bool flag = this._taskForOutputProcessing != null;
				bool flag2 = this._enableOffering && this._messages.Count > 0;
				if (!flag && flag2 && !this.CanceledOrFaulted)
				{
					this._taskForOutputProcessing = new Task(delegate(object thisSourceCore)
					{
						((BroadcastBlock<T>.BroadcastingSourceCore<TOutput>)thisSourceCore).OfferMessagesLoopCore();
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
						this._decliningPermanently = true;
						this._taskForOutputProcessing = null;
						Task.Factory.StartNew(delegate(object state)
						{
							BroadcastBlock<T>.BroadcastingSourceCore<TOutput> broadcastingSourceCore = (BroadcastBlock<T>.BroadcastingSourceCore<TOutput>)state;
							object outgoingLock = broadcastingSourceCore.OutgoingLock;
							lock (outgoingLock)
							{
								object valueLock = broadcastingSourceCore.ValueLock;
								lock (valueLock)
								{
									broadcastingSourceCore.CompleteBlockIfPossible();
								}
							}
						}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
					}
				}
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000E928 File Offset: 0x0000CB28
			private void OfferMessagesLoopCore()
			{
				try
				{
					int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
					object outgoingLock = this.OutgoingLock;
					lock (outgoingLock)
					{
						int num = 0;
						while (num < actualMaxMessagesPerTask && !this.CanceledOrFaulted && this.OfferToTargets())
						{
							num++;
						}
					}
				}
				catch (Exception ex)
				{
					this._owningSource.CompleteCore(ex, true, false);
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
							this.OfferAsyncIfNecessary(true);
							this.CompleteBlockIfPossible();
						}
					}
				}
			}

			// Token: 0x06000386 RID: 902 RVA: 0x0000EA28 File Offset: 0x0000CC28
			private void CompleteBlockIfPossible()
			{
				if (!this._completionReserved)
				{
					bool flag = this._taskForOutputProcessing != null;
					bool flag2 = this._decliningPermanently && this._messages.Count == 0;
					bool flag3 = !flag && (flag2 || this.CanceledOrFaulted);
					if (flag3)
					{
						this.CompleteBlockIfPossible_Slow();
					}
				}
			}

			// Token: 0x06000387 RID: 903 RVA: 0x0000EA80 File Offset: 0x0000CC80
			private void CompleteBlockIfPossible_Slow()
			{
				this._completionReserved = true;
				Task.Factory.StartNew(delegate(object thisSourceCore)
				{
					((BroadcastBlock<T>.BroadcastingSourceCore<TOutput>)thisSourceCore).CompleteBlockOncePossible();
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}

			// Token: 0x06000388 RID: 904 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
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

			// Token: 0x06000389 RID: 905 RVA: 0x0000EBD0 File Offset: 0x0000CDD0
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
				object outgoingLock = this.OutgoingLock;
				IDisposable disposable;
				lock (outgoingLock)
				{
					if (this._completionReserved)
					{
						this.OfferCurrentMessageToNewTarget(target);
						if (linkOptions.PropagateCompletion)
						{
							Common.PropagateCompletionOnceCompleted(this._completionTask.Task, target);
						}
						disposable = Disposables.Nop;
					}
					else
					{
						this._targetRegistry.Add(ref target, linkOptions);
						this.OfferCurrentMessageToNewTarget(target);
						disposable = Common.CreateUnlinker<TOutput>(this.OutgoingLock, this._targetRegistry, target);
					}
				}
				return disposable;
			}

			// Token: 0x0600038A RID: 906 RVA: 0x0000EC80 File Offset: 0x0000CE80
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
				object outgoingLock = this.OutgoingLock;
				TOutput currentMessage;
				lock (outgoingLock)
				{
					object valueLock = this.ValueLock;
					lock (valueLock)
					{
						if (messageHeader.Id != this._nextMessageId)
						{
							messageConsumed = false;
							return default(TOutput);
						}
						if (this._nextMessageReservedFor == target)
						{
							this._nextMessageReservedFor = null;
							this._enableOffering = true;
						}
						this._targetRegistry.Remove(target, true);
						this.OfferAsyncIfNecessary(false);
						this.CompleteBlockIfPossible();
						currentMessage = this._currentMessage;
					}
				}
				messageConsumed = true;
				return this.CloneItem(currentMessage);
			}

			// Token: 0x0600038B RID: 907 RVA: 0x0000ED70 File Offset: 0x0000CF70
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
							if (messageHeader.Id == this._nextMessageId)
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

			// Token: 0x0600038C RID: 908 RVA: 0x0000EE28 File Offset: 0x0000D028
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
					TOutput currentMessage;
					lock (valueLock)
					{
						if (messageHeader.Id != this._nextMessageId)
						{
							throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
						}
						this._nextMessageReservedFor = null;
						this._enableOffering = true;
						currentMessage = this._currentMessage;
						this.OfferAsyncIfNecessary(false);
					}
					this.OfferMessageToTarget(messageHeader, currentMessage, target);
				}
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x0600038D RID: 909 RVA: 0x0000EF08 File Offset: 0x0000D108
			private bool CanceledOrFaulted
			{
				get
				{
					return this._dataflowBlockOptions.CancellationToken.IsCancellationRequested || (Volatile.Read<List<Exception>>(ref this._exceptions) != null && this._decliningPermanently);
				}
			}

			// Token: 0x0600038E RID: 910 RVA: 0x0000EF44 File Offset: 0x0000D144
			internal void AddException(Exception exception)
			{
				object valueLock = this.ValueLock;
				lock (valueLock)
				{
					Common.AddException(ref this._exceptions, exception, false);
				}
			}

			// Token: 0x0600038F RID: 911 RVA: 0x0000EF8C File Offset: 0x0000D18C
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

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000390 RID: 912 RVA: 0x0000F008 File Offset: 0x0000D208
			internal Task Completion
			{
				get
				{
					return this._completionTask.Task;
				}
			}

			// Token: 0x1700011F RID: 287
			// (get) Token: 0x06000391 RID: 913 RVA: 0x0000F015 File Offset: 0x0000D215
			internal DataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._dataflowBlockOptions;
				}
			}

			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000392 RID: 914 RVA: 0x0000F020 File Offset: 0x0000D220
			private object DebuggerDisplayContent
			{
				get
				{
					IDebuggerDisplay owningSource = this._owningSource;
					return string.Format("Block=\"{0}\"", (owningSource != null) ? owningSource.Content : this._owningSource);
				}
			}

			// Token: 0x06000393 RID: 915 RVA: 0x0000F04F File Offset: 0x0000D24F
			internal BroadcastBlock<T>.BroadcastingSourceCore<TOutput>.DebuggingInformation GetDebuggingInformation()
			{
				return new BroadcastBlock<T>.BroadcastingSourceCore<TOutput>.DebuggingInformation(this);
			}

			// Token: 0x04000146 RID: 326
			private readonly TargetRegistry<TOutput> _targetRegistry;

			// Token: 0x04000147 RID: 327
			private readonly Queue<TOutput> _messages = new Queue<TOutput>();

			// Token: 0x04000148 RID: 328
			private readonly TaskCompletionSource<VoidResult> _completionTask = new TaskCompletionSource<VoidResult>();

			// Token: 0x04000149 RID: 329
			private readonly Action<int> _itemsRemovedAction;

			// Token: 0x0400014A RID: 330
			private readonly BroadcastBlock<TOutput> _owningSource;

			// Token: 0x0400014B RID: 331
			private readonly DataflowBlockOptions _dataflowBlockOptions;

			// Token: 0x0400014C RID: 332
			private readonly Func<TOutput, TOutput> _cloningFunction;

			// Token: 0x0400014D RID: 333
			private bool _currentMessageIsValid;

			// Token: 0x0400014E RID: 334
			private TOutput _currentMessage;

			// Token: 0x0400014F RID: 335
			private ITargetBlock<TOutput> _nextMessageReservedFor;

			// Token: 0x04000150 RID: 336
			private bool _enableOffering;

			// Token: 0x04000151 RID: 337
			private bool _decliningPermanently;

			// Token: 0x04000152 RID: 338
			private Task _taskForOutputProcessing;

			// Token: 0x04000153 RID: 339
			private List<Exception> _exceptions;

			// Token: 0x04000154 RID: 340
			private long _nextMessageId = 1L;

			// Token: 0x04000155 RID: 341
			private bool _completionReserved;

			// Token: 0x0200009F RID: 159
			internal sealed class DebuggingInformation
			{
				// Token: 0x060004A8 RID: 1192 RVA: 0x00010AC0 File Offset: 0x0000ECC0
				public DebuggingInformation(BroadcastBlock<T>.BroadcastingSourceCore<TOutput> source)
				{
					this._source = source;
				}

				// Token: 0x1700018D RID: 397
				// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00010ACF File Offset: 0x0000ECCF
				public bool HasValue
				{
					get
					{
						return this._source._currentMessageIsValid;
					}
				}

				// Token: 0x1700018E RID: 398
				// (get) Token: 0x060004AA RID: 1194 RVA: 0x00010ADC File Offset: 0x0000ECDC
				public TOutput Value
				{
					get
					{
						return this._source._currentMessage;
					}
				}

				// Token: 0x1700018F RID: 399
				// (get) Token: 0x060004AB RID: 1195 RVA: 0x00010AE9 File Offset: 0x0000ECE9
				public IEnumerable<TOutput> InputQueue
				{
					get
					{
						return this._source._messages.ToList<TOutput>();
					}
				}

				// Token: 0x17000190 RID: 400
				// (get) Token: 0x060004AC RID: 1196 RVA: 0x00010AFB File Offset: 0x0000ECFB
				public Task TaskForOutputProcessing
				{
					get
					{
						return this._source._taskForOutputProcessing;
					}
				}

				// Token: 0x17000191 RID: 401
				// (get) Token: 0x060004AD RID: 1197 RVA: 0x00010B08 File Offset: 0x0000ED08
				public DataflowBlockOptions DataflowBlockOptions
				{
					get
					{
						return this._source._dataflowBlockOptions;
					}
				}

				// Token: 0x17000192 RID: 402
				// (get) Token: 0x060004AE RID: 1198 RVA: 0x00010B15 File Offset: 0x0000ED15
				public bool IsCompleted
				{
					get
					{
						return this._source.Completion.IsCompleted;
					}
				}

				// Token: 0x17000193 RID: 403
				// (get) Token: 0x060004AF RID: 1199 RVA: 0x00010B27 File Offset: 0x0000ED27
				public TargetRegistry<TOutput> LinkedTargets
				{
					get
					{
						return this._source._targetRegistry;
					}
				}

				// Token: 0x17000194 RID: 404
				// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00010B34 File Offset: 0x0000ED34
				public ITargetBlock<TOutput> NextMessageReservedFor
				{
					get
					{
						return this._source._nextMessageReservedFor;
					}
				}

				// Token: 0x040001F9 RID: 505
				private readonly BroadcastBlock<T>.BroadcastingSourceCore<TOutput> _source;
			}
		}
	}
}
