using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(BufferBlock<>.DebugView))]
	public sealed class BufferBlock<[Nullable(2)] T> : IPropagatorBlock<T, T>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T>, IReceivableSourceBlock<T>, IDebuggerDisplay
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00004F56 File Offset: 0x00003156
		private object IncomingLock
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004F5E File Offset: 0x0000315E
		public BufferBlock()
			: this(DataflowBlockOptions.Default)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004F6C File Offset: 0x0000316C
		public BufferBlock(DataflowBlockOptions dataflowBlockOptions)
		{
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<ISourceBlock<T>, int> action = null;
			if (dataflowBlockOptions.BoundedCapacity > 0)
			{
				action = delegate(ISourceBlock<T> owningSource, int count)
				{
					((BufferBlock<T>)owningSource).OnItemsRemoved(count);
				};
				this._boundingState = new BoundingStateWithPostponedAndTask<T>(dataflowBlockOptions.BoundedCapacity);
			}
			this._source = new SourceCore<T>(this, dataflowBlockOptions, delegate(ISourceBlock<T> owningSource)
			{
				((BufferBlock<T>)owningSource).Complete();
			}, action, null);
			this._source.Completion.ContinueWith(delegate(Task completed, object state)
			{
				IDataflowBlock dataflowBlock = (BufferBlock<T>)state;
				dataflowBlock.Fault(completed.Exception);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.None) | TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);
			Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._source.Completion, delegate(object owningSource)
			{
				((BufferBlock<T>)owningSource).Complete();
			}, this);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00005094 File Offset: 0x00003294
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
				if (this._targetDecliningPermanently)
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

		// Token: 0x06000125 RID: 293 RVA: 0x000051A8 File Offset: 0x000033A8
		public void Complete()
		{
			this.CompleteCore(null, false, false);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000051B3 File Offset: 0x000033B3
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.CompleteCore(exception, false, false);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000051CC File Offset: 0x000033CC
		private void CompleteCore(Exception exception, bool storeExceptionEvenIfAlreadyCompleting, bool revertProcessingState = false)
		{
			object incomingLock = this.IncomingLock;
			lock (incomingLock)
			{
				if (exception != null && (!this._targetDecliningPermanently || storeExceptionEvenIfAlreadyCompleting))
				{
					this._source.AddException(exception);
				}
				if (revertProcessingState)
				{
					this._boundingState.TaskForInputProcessing = null;
				}
				this._targetDecliningPermanently = true;
				this.CompleteTargetIfPossible();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005240 File Offset: 0x00003440
		public IDisposable LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
		{
			return this._source.LinkTo(target, linkOptions);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000524F File Offset: 0x0000344F
		public bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<T> filter, [MaybeNullWhen(false)] out T item)
		{
			return this._source.TryReceive(filter, out item);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000525E File Offset: 0x0000345E
		public bool TryReceiveAll([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IList<T> items)
		{
			return this._source.TryReceiveAll(out items);
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000526C File Offset: 0x0000346C
		public int Count
		{
			get
			{
				return this._source.OutputCount;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005279 File Offset: 0x00003479
		public Task Completion
		{
			get
			{
				return this._source.Completion;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005286 File Offset: 0x00003486
		T ISourceBlock<T>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
		{
			return this._source.ConsumeMessage(messageHeader, target, out messageConsumed);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005296 File Offset: 0x00003496
		bool ISourceBlock<T>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			return this._source.ReserveMessage(messageHeader, target);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000052A5 File Offset: 0x000034A5
		void ISourceBlock<T>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			this._source.ReleaseReservation(messageHeader, target);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000052B4 File Offset: 0x000034B4
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

		// Token: 0x06000131 RID: 305 RVA: 0x00005318 File Offset: 0x00003518
		internal void ConsumeAsyncIfNecessary(bool isReplacementReplica = false)
		{
			if (!this._targetDecliningPermanently && this._boundingState.TaskForInputProcessing == null && this._boundingState.PostponedMessages.Count > 0 && this._boundingState.CountIsLessThanBound)
			{
				this._boundingState.TaskForInputProcessing = new Task(delegate(object state)
				{
					((BufferBlock<T>)state).ConsumeMessagesLoopCore();
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

		// Token: 0x06000132 RID: 306 RVA: 0x0000541C File Offset: 0x0000361C
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

		// Token: 0x06000133 RID: 307 RVA: 0x000054C0 File Offset: 0x000036C0
		private bool ConsumeAndStoreOneMessageIfAvailable()
		{
			bool flag2;
			for (;;)
			{
				object incomingLock = this.IncomingLock;
				KeyValuePair<ISourceBlock<T>, DataflowMessageHeader> keyValuePair;
				lock (incomingLock)
				{
					if (this._targetDecliningPermanently)
					{
						flag2 = false;
						break;
					}
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

		// Token: 0x06000134 RID: 308 RVA: 0x000055D8 File Offset: 0x000037D8
		private void CompleteTargetIfPossible()
		{
			if (this._targetDecliningPermanently && !this._targetCompletionReserved && (this._boundingState == null || this._boundingState.TaskForInputProcessing == null))
			{
				this._targetCompletionReserved = true;
				if (this._boundingState != null && this._boundingState.PostponedMessages.Count > 0)
				{
					Task.Factory.StartNew(delegate(object state)
					{
						BufferBlock<T> bufferBlock = (BufferBlock<T>)state;
						List<Exception> list = null;
						if (bufferBlock._boundingState != null)
						{
							Common.ReleaseAllPostponedMessages<T>(bufferBlock, bufferBlock._boundingState.PostponedMessages, ref list);
						}
						if (list != null)
						{
							bufferBlock._source.AddExceptions(list);
						}
						bufferBlock._source.Complete();
					}, this, CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
					return;
				}
				this._source.Complete();
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00005676 File Offset: 0x00003876
		private int CountForDebugger
		{
			get
			{
				return this._source.GetDebuggingInformation().OutputCount;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00005688 File Offset: 0x00003888
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._source.DataflowBlockOptions);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000569B File Offset: 0x0000389B
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, Count={1}", Common.GetNameForDebugger(this, this._source.DataflowBlockOptions), this.CountForDebugger);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000056C3 File Offset: 0x000038C3
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000048 RID: 72
		private readonly SourceCore<T> _source;

		// Token: 0x04000049 RID: 73
		private readonly BoundingStateWithPostponedAndTask<T> _boundingState;

		// Token: 0x0400004A RID: 74
		private bool _targetDecliningPermanently;

		// Token: 0x0400004B RID: 75
		private bool _targetCompletionReserved;

		// Token: 0x0200006A RID: 106
		private sealed class DebugView
		{
			// Token: 0x0600039A RID: 922 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
			public DebugView(BufferBlock<T> bufferBlock)
			{
				this._bufferBlock = bufferBlock;
				this._sourceDebuggingInformation = bufferBlock._source.GetDebuggingInformation();
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x0600039B RID: 923 RVA: 0x0000F113 File Offset: 0x0000D313
			public QueuedMap<ISourceBlock<T>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					if (this._bufferBlock._boundingState == null)
					{
						return null;
					}
					return this._bufferBlock._boundingState.PostponedMessages;
				}
			}

			// Token: 0x17000122 RID: 290
			// (get) Token: 0x0600039C RID: 924 RVA: 0x0000F134 File Offset: 0x0000D334
			public IEnumerable<T> Queue
			{
				get
				{
					return this._sourceDebuggingInformation.OutputQueue;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x0600039D RID: 925 RVA: 0x0000F141 File Offset: 0x0000D341
			public Task TaskForInputProcessing
			{
				get
				{
					if (this._bufferBlock._boundingState == null)
					{
						return null;
					}
					return this._bufferBlock._boundingState.TaskForInputProcessing;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x0600039E RID: 926 RVA: 0x0000F162 File Offset: 0x0000D362
			public Task TaskForOutputProcessing
			{
				get
				{
					return this._sourceDebuggingInformation.TaskForOutputProcessing;
				}
			}

			// Token: 0x17000125 RID: 293
			// (get) Token: 0x0600039F RID: 927 RVA: 0x0000F16F File Offset: 0x0000D36F
			public DataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._sourceDebuggingInformation.DataflowBlockOptions;
				}
			}

			// Token: 0x17000126 RID: 294
			// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000F17C File Offset: 0x0000D37C
			public bool IsDecliningPermanently
			{
				get
				{
					return this._bufferBlock._targetDecliningPermanently;
				}
			}

			// Token: 0x17000127 RID: 295
			// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000F189 File Offset: 0x0000D389
			public bool IsCompleted
			{
				get
				{
					return this._sourceDebuggingInformation.IsCompleted;
				}
			}

			// Token: 0x17000128 RID: 296
			// (get) Token: 0x060003A2 RID: 930 RVA: 0x0000F196 File Offset: 0x0000D396
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._bufferBlock);
				}
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000F1A3 File Offset: 0x0000D3A3
			public TargetRegistry<T> LinkedTargets
			{
				get
				{
					return this._sourceDebuggingInformation.LinkedTargets;
				}
			}

			// Token: 0x1700012A RID: 298
			// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
			public ITargetBlock<T> NextMessageReservedFor
			{
				get
				{
					return this._sourceDebuggingInformation.NextMessageReservedFor;
				}
			}

			// Token: 0x0400015B RID: 347
			private readonly BufferBlock<T> _bufferBlock;

			// Token: 0x0400015C RID: 348
			private readonly SourceCore<T>.DebuggingInformation _sourceDebuggingInformation;
		}
	}
}
