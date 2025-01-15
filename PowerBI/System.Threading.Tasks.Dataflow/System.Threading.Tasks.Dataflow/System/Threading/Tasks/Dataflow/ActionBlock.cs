using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000026 RID: 38
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(ActionBlock<>.DebugView))]
	public sealed class ActionBlock<[Nullable(2)] TInput> : ITargetBlock<TInput>, IDataflowBlock, IDebuggerDisplay
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00003931 File Offset: 0x00001B31
		public ActionBlock(Action<TInput> action)
			: this(action, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000393F File Offset: 0x00001B3F
		public ActionBlock(Action<TInput> action, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(action, dataflowBlockOptions)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003949 File Offset: 0x00001B49
		public ActionBlock(Func<TInput, Task> action)
			: this(action, ExecutionDataflowBlockOptions.Default)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003957 File Offset: 0x00001B57
		public ActionBlock(Func<TInput, Task> action, ExecutionDataflowBlockOptions dataflowBlockOptions)
			: this(action, dataflowBlockOptions)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003964 File Offset: 0x00001B64
		private ActionBlock(Delegate action, ExecutionDataflowBlockOptions dataflowBlockOptions)
		{
			ActionBlock<TInput> <>4__this = this;
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			Action<TInput> syncAction = action as Action<TInput>;
			if (syncAction != null && dataflowBlockOptions.SingleProducerConstrained && dataflowBlockOptions.MaxDegreeOfParallelism == 1 && !dataflowBlockOptions.CancellationToken.CanBeCanceled && dataflowBlockOptions.BoundedCapacity == -1)
			{
				this._spscTarget = new SpscTargetCore<TInput>(this, syncAction, dataflowBlockOptions);
			}
			else
			{
				if (syncAction != null)
				{
					this._defaultTarget = new TargetCore<TInput>(this, delegate(KeyValuePair<TInput, long> messageWithId)
					{
						<>4__this.ProcessMessage(syncAction, messageWithId);
					}, null, dataflowBlockOptions, TargetCoreOptions.RepresentsBlockCompletion);
				}
				else
				{
					Func<TInput, Task> asyncAction = action as Func<TInput, Task>;
					this._defaultTarget = new TargetCore<TInput>(this, delegate(KeyValuePair<TInput, long> messageWithId)
					{
						<>4__this.ProcessMessageWithTask(asyncAction, messageWithId);
					}, null, dataflowBlockOptions, TargetCoreOptions.UsesAsyncCompletion | TargetCoreOptions.RepresentsBlockCompletion);
				}
				Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this.Completion, delegate(object state)
				{
					((TargetCore<TInput>)state).Complete(null, true, false, false, false);
				}, this._defaultTarget);
			}
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003AA4 File Offset: 0x00001CA4
		private void ProcessMessage(Action<TInput> action, KeyValuePair<TInput, long> messageWithId)
		{
			try
			{
				action(messageWithId.Key);
			}
			catch (Exception ex)
			{
				if (!Common.IsCooperativeCancellation(ex))
				{
					throw;
				}
			}
			finally
			{
				if (this._defaultTarget.IsBounded)
				{
					this._defaultTarget.ChangeBoundingCount(-1);
				}
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003B04 File Offset: 0x00001D04
		private void ProcessMessageWithTask(Func<TInput, Task> action, KeyValuePair<TInput, long> messageWithId)
		{
			Task task = null;
			Exception ex = null;
			try
			{
				task = action(messageWithId.Key);
			}
			catch (Exception ex2)
			{
				ex = ex2;
			}
			if (task == null)
			{
				if (ex != null && !Common.IsCooperativeCancellation(ex))
				{
					Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, messageWithId.Key, false);
					this._defaultTarget.Complete(ex, true, true, false, false);
				}
				this._defaultTarget.SignalOneAsyncMessageCompleted(-1);
				return;
			}
			if (task.IsCompleted)
			{
				this.AsyncCompleteProcessMessageWithTask(task);
				return;
			}
			task.ContinueWith(delegate(Task completed, object state)
			{
				((ActionBlock<TInput>)state).AsyncCompleteProcessMessageWithTask(completed);
			}, this, CancellationToken.None, Common.GetContinuationOptions(TaskContinuationOptions.ExecuteSynchronously), TaskScheduler.Default);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private void AsyncCompleteProcessMessageWithTask(Task completed)
		{
			if (completed.IsFaulted)
			{
				this._defaultTarget.Complete(completed.Exception, true, true, true, false);
			}
			this._defaultTarget.SignalOneAsyncMessageCompleted(-1);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003BEB File Offset: 0x00001DEB
		public void Complete()
		{
			if (this._defaultTarget != null)
			{
				this._defaultTarget.Complete(null, false, false, false, false);
				return;
			}
			this._spscTarget.Complete(null);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003C12 File Offset: 0x00001E12
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			if (this._defaultTarget != null)
			{
				this._defaultTarget.Complete(exception, true, false, false, false);
				return;
			}
			this._spscTarget.Complete(exception);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003C47 File Offset: 0x00001E47
		public Task Completion
		{
			get
			{
				if (this._defaultTarget == null)
				{
					return this._spscTarget.Completion;
				}
				return this._defaultTarget.Completion;
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003C68 File Offset: 0x00001E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Post(TInput item)
		{
			if (this._defaultTarget == null)
			{
				return this._spscTarget.Post(item);
			}
			return this._defaultTarget.OfferMessage(Common.SingleMessageHeader, item, null, false) == DataflowMessageStatus.Accepted;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003C95 File Offset: 0x00001E95
		DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			if (this._defaultTarget == null)
			{
				return this._spscTarget.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
			}
			return this._defaultTarget.OfferMessage(messageHeader, messageValue, source, consumeToAccept);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public int InputCount
		{
			get
			{
				if (this._defaultTarget == null)
				{
					return this._spscTarget.InputCount;
				}
				return this._defaultTarget.InputCount;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003CE1 File Offset: 0x00001EE1
		private int InputCountForDebugger
		{
			get
			{
				if (this._defaultTarget == null)
				{
					return this._spscTarget.InputCount;
				}
				return this._defaultTarget.GetDebuggingInformation().InputCount;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003D07 File Offset: 0x00001F07
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, (this._defaultTarget != null) ? this._defaultTarget.DataflowBlockOptions : this._spscTarget.DataflowBlockOptions);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003D2F File Offset: 0x00001F2F
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, InputCount={1}", Common.GetNameForDebugger(this, (this._defaultTarget != null) ? this._defaultTarget.DataflowBlockOptions : this._spscTarget.DataflowBlockOptions), this.InputCountForDebugger);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003D6C File Offset: 0x00001F6C
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x04000035 RID: 53
		private readonly TargetCore<TInput> _defaultTarget;

		// Token: 0x04000036 RID: 54
		private readonly SpscTargetCore<TInput> _spscTarget;

		// Token: 0x0200005A RID: 90
		private sealed class DebugView
		{
			// Token: 0x06000306 RID: 774 RVA: 0x0000CC45 File Offset: 0x0000AE45
			public DebugView(ActionBlock<TInput> actionBlock)
			{
				this._actionBlock = actionBlock;
				if (this._actionBlock._defaultTarget != null)
				{
					this._defaultDebugInfo = actionBlock._defaultTarget.GetDebuggingInformation();
					return;
				}
				this._spscDebugInfo = actionBlock._spscTarget.GetDebuggingInformation();
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000307 RID: 775 RVA: 0x0000CC84 File Offset: 0x0000AE84
			public IEnumerable<TInput> InputQueue
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return this._spscDebugInfo.InputQueue;
					}
					return this._defaultDebugInfo.InputQueue;
				}
			}

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x06000308 RID: 776 RVA: 0x0000CCA5 File Offset: 0x0000AEA5
			public QueuedMap<ISourceBlock<TInput>, DataflowMessageHeader> PostponedMessages
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return null;
					}
					return this._defaultDebugInfo.PostponedMessages;
				}
			}

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x06000309 RID: 777 RVA: 0x0000CCBC File Offset: 0x0000AEBC
			public int CurrentDegreeOfParallelism
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return this._spscDebugInfo.CurrentDegreeOfParallelism;
					}
					return this._defaultDebugInfo.CurrentDegreeOfParallelism;
				}
			}

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x0600030A RID: 778 RVA: 0x0000CCDD File Offset: 0x0000AEDD
			public ExecutionDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return this._spscDebugInfo.DataflowBlockOptions;
					}
					return this._defaultDebugInfo.DataflowBlockOptions;
				}
			}

			// Token: 0x170000E0 RID: 224
			// (get) Token: 0x0600030B RID: 779 RVA: 0x0000CCFE File Offset: 0x0000AEFE
			public bool IsDecliningPermanently
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return this._spscDebugInfo.IsDecliningPermanently;
					}
					return this._defaultDebugInfo.IsDecliningPermanently;
				}
			}

			// Token: 0x170000E1 RID: 225
			// (get) Token: 0x0600030C RID: 780 RVA: 0x0000CD1F File Offset: 0x0000AF1F
			public bool IsCompleted
			{
				get
				{
					if (this._defaultDebugInfo == null)
					{
						return this._spscDebugInfo.IsCompleted;
					}
					return this._defaultDebugInfo.IsCompleted;
				}
			}

			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x0600030D RID: 781 RVA: 0x0000CD40 File Offset: 0x0000AF40
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._actionBlock);
				}
			}

			// Token: 0x04000116 RID: 278
			private readonly ActionBlock<TInput> _actionBlock;

			// Token: 0x04000117 RID: 279
			private readonly TargetCore<TInput>.DebuggingInformation _defaultDebugInfo;

			// Token: 0x04000118 RID: 280
			private readonly SpscTargetCore<TInput>.DebuggingInformation _spscDebugInfo;
		}
	}
}
