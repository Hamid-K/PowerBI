using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System.Threading.Tasks.Dataflow.Internal
{
	// Token: 0x02000045 RID: 69
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	internal sealed class SpscTargetCore<TInput>
	{
		// Token: 0x0600025E RID: 606 RVA: 0x0000A4D0 File Offset: 0x000086D0
		internal SpscTargetCore(ITargetBlock<TInput> owningTarget, Action<TInput> action, ExecutionDataflowBlockOptions dataflowBlockOptions)
		{
			this._owningTarget = owningTarget;
			this._action = action;
			this._dataflowBlockOptions = dataflowBlockOptions;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000A4F8 File Offset: 0x000086F8
		internal bool Post(TInput messageValue)
		{
			if (this._decliningPermanently)
			{
				return false;
			}
			this._messages.Enqueue(messageValue);
			Interlocked.MemoryBarrier();
			if (this._activeConsumer == null)
			{
				this.ScheduleConsumerIfNecessary(false);
			}
			return true;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000A529 File Offset: 0x00008729
		internal DataflowMessageStatus OfferMessage(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			if (consumeToAccept || !this.Post(messageValue))
			{
				return this.OfferMessage_Slow(messageHeader, messageValue, source, consumeToAccept);
			}
			return DataflowMessageStatus.Accepted;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000A548 File Offset: 0x00008748
		private DataflowMessageStatus OfferMessage_Slow(DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept)
		{
			if (this._decliningPermanently)
			{
				return DataflowMessageStatus.DecliningPermanently;
			}
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (consumeToAccept)
			{
				if (source == null)
				{
					throw new ArgumentException(SR.Argument_CantConsumeFromANullSource, "consumeToAccept");
				}
				bool flag;
				messageValue = source.ConsumeMessage(messageHeader, this._owningTarget, out flag);
				if (!flag)
				{
					return DataflowMessageStatus.NotAvailable;
				}
			}
			this._messages.Enqueue(messageValue);
			Interlocked.MemoryBarrier();
			if (this._activeConsumer == null)
			{
				this.ScheduleConsumerIfNecessary(false);
			}
			return DataflowMessageStatus.Accepted;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A5CC File Offset: 0x000087CC
		private void ScheduleConsumerIfNecessary(bool isReplica)
		{
			if (this._activeConsumer == null)
			{
				Task task = new Task(delegate(object state)
				{
					((SpscTargetCore<TInput>)state).ProcessMessagesLoopCore();
				}, this, CancellationToken.None, Common.GetCreationOptionsForTask(isReplica));
				if (Interlocked.CompareExchange<Task>(ref this._activeConsumer, task, null) == null)
				{
					DataflowEtwProvider log = DataflowEtwProvider.Log;
					if (log.IsEnabled())
					{
						log.TaskLaunchedForMessageHandling(this._owningTarget, task, DataflowEtwProvider.TaskLaunchedReason.ProcessingInputMessages, this._messages.Count);
					}
					task.Start(this._dataflowBlockOptions.TaskScheduler);
				}
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A65C File Offset: 0x0000885C
		private void ProcessMessagesLoopCore()
		{
			int num = 0;
			int actualMaxMessagesPerTask = this._dataflowBlockOptions.ActualMaxMessagesPerTask;
			bool flag = true;
			while (flag)
			{
				flag = false;
				TInput tinput = default(TInput);
				try
				{
					while (this._exceptions == null && num < actualMaxMessagesPerTask && this._messages.TryDequeue(out tinput))
					{
						num++;
						this._action(tinput);
					}
				}
				catch (Exception ex)
				{
					if (!Common.IsCooperativeCancellation(ex))
					{
						this._decliningPermanently = true;
						Common.StoreDataflowMessageValueIntoExceptionData<TInput>(ex, tinput, false);
						this.StoreException(ex);
					}
				}
				finally
				{
					if (!this._messages.IsEmpty && this._exceptions == null && num < actualMaxMessagesPerTask)
					{
						flag = true;
					}
					else
					{
						bool decliningPermanently = this._decliningPermanently;
						if ((decliningPermanently && this._messages.IsEmpty) || this._exceptions != null)
						{
							if (!this._completionReserved)
							{
								this._completionReserved = true;
								this.CompleteBlockOncePossible();
							}
						}
						else
						{
							Task task = Interlocked.Exchange<Task>(ref this._activeConsumer, null);
							if (!this._messages.IsEmpty || (!decliningPermanently && this._decliningPermanently) || this._exceptions != null)
							{
								this.ScheduleConsumerIfNecessary(true);
							}
						}
					}
				}
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A7A4 File Offset: 0x000089A4
		internal int InputCount
		{
			get
			{
				return this._messages.Count;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A7B1 File Offset: 0x000089B1
		internal void Complete(Exception exception)
		{
			if (!this._decliningPermanently)
			{
				if (exception != null)
				{
					this.StoreException(exception);
				}
				this._decliningPermanently = true;
				this.ScheduleConsumerIfNecessary(false);
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A7D8 File Offset: 0x000089D8
		private void StoreException(Exception exception)
		{
			List<Exception> list = LazyInitializer.EnsureInitialized<List<Exception>>(ref this._exceptions, () => new List<Exception>());
			lock (list)
			{
				this._exceptions.Add(exception);
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A844 File Offset: 0x00008A44
		private void CompleteBlockOncePossible()
		{
			TInput tinput;
			while (this._messages.TryDequeue(out tinput))
			{
			}
			if (this._exceptions != null)
			{
				List<Exception> exceptions = this._exceptions;
				Exception[] array;
				lock (exceptions)
				{
					array = this._exceptions.ToArray();
				}
				bool flag2 = this.CompletionSource.TrySetException(array);
			}
			else
			{
				bool flag2 = this.CompletionSource.TrySetResult(default(VoidResult));
			}
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCompleted(this._owningTarget);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000A8EC File Offset: 0x00008AEC
		internal Task Completion
		{
			get
			{
				return this.CompletionSource.Task;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A8F9 File Offset: 0x00008AF9
		private TaskCompletionSource<VoidResult> CompletionSource
		{
			get
			{
				return LazyInitializer.EnsureInitialized<TaskCompletionSource<VoidResult>>(ref this._completionTask, () => new TaskCompletionSource<VoidResult>());
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000A925 File Offset: 0x00008B25
		internal ExecutionDataflowBlockOptions DataflowBlockOptions
		{
			get
			{
				return this._dataflowBlockOptions;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A92D File Offset: 0x00008B2D
		internal SpscTargetCore<TInput>.DebuggingInformation GetDebuggingInformation()
		{
			return new SpscTargetCore<TInput>.DebuggingInformation(this);
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000A938 File Offset: 0x00008B38
		private object DebuggerDisplayContent
		{
			get
			{
				IDebuggerDisplay debuggerDisplay = this._owningTarget as IDebuggerDisplay;
				return string.Format("Block=\"{0}\"", (debuggerDisplay != null) ? debuggerDisplay.Content : this._owningTarget);
			}
		}

		// Token: 0x040000B1 RID: 177
		private readonly ITargetBlock<TInput> _owningTarget;

		// Token: 0x040000B2 RID: 178
		private readonly SingleProducerSingleConsumerQueue<TInput> _messages = new SingleProducerSingleConsumerQueue<TInput>();

		// Token: 0x040000B3 RID: 179
		private readonly ExecutionDataflowBlockOptions _dataflowBlockOptions;

		// Token: 0x040000B4 RID: 180
		private readonly Action<TInput> _action;

		// Token: 0x040000B5 RID: 181
		private volatile List<Exception> _exceptions;

		// Token: 0x040000B6 RID: 182
		private volatile bool _decliningPermanently;

		// Token: 0x040000B7 RID: 183
		private volatile bool _completionReserved;

		// Token: 0x040000B8 RID: 184
		private volatile Task _activeConsumer;

		// Token: 0x040000B9 RID: 185
		private TaskCompletionSource<VoidResult> _completionTask;

		// Token: 0x0200008A RID: 138
		internal sealed class DebuggingInformation
		{
			// Token: 0x0600044B RID: 1099 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
			internal DebuggingInformation(SpscTargetCore<TInput> target)
			{
				this._target = target;
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
			internal IEnumerable<TInput> InputQueue
			{
				get
				{
					return this._target._messages.ToList<TInput>();
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x00010006 File Offset: 0x0000E206
			internal int CurrentDegreeOfParallelism
			{
				get
				{
					if (this._target._activeConsumer == null || this._target.Completion.IsCompleted)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001002C File Offset: 0x0000E22C
			internal ExecutionDataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._target._dataflowBlockOptions;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x00010039 File Offset: 0x0000E239
			internal bool IsDecliningPermanently
			{
				get
				{
					return this._target._decliningPermanently;
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x00010048 File Offset: 0x0000E248
			internal bool IsCompleted
			{
				get
				{
					return this._target.Completion.IsCompleted;
				}
			}

			// Token: 0x040001C2 RID: 450
			private readonly SpscTargetCore<TInput> _target;
		}
	}
}
