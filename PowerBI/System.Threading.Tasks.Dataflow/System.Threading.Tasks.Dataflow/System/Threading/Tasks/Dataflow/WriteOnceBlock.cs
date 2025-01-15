using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Dataflow.Internal;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000030 RID: 48
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{DebuggerDisplayContent,nq}")]
	[DebuggerTypeProxy(typeof(WriteOnceBlock<>.DebugView))]
	public sealed class WriteOnceBlock<[Nullable(2)] T> : IPropagatorBlock<T, T>, ITargetBlock<T>, IDataflowBlock, ISourceBlock<T>, IReceivableSourceBlock<T>, IDebuggerDisplay
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00006D7B File Offset: 0x00004F7B
		private object ValueLock
		{
			get
			{
				return this._targetRegistry;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006D83 File Offset: 0x00004F83
		public WriteOnceBlock([Nullable(new byte[] { 2, 1, 1 })] Func<T, T> cloningFunction)
			: this(cloningFunction, DataflowBlockOptions.Default)
		{
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006D94 File Offset: 0x00004F94
		public WriteOnceBlock([Nullable(new byte[] { 2, 1, 1 })] Func<T, T> cloningFunction, DataflowBlockOptions dataflowBlockOptions)
		{
			if (dataflowBlockOptions == null)
			{
				throw new ArgumentNullException("dataflowBlockOptions");
			}
			this._cloningFunction = cloningFunction;
			this._dataflowBlockOptions = dataflowBlockOptions.DefaultOrClone();
			this._targetRegistry = new TargetRegistry<T>(this);
			if (dataflowBlockOptions.CancellationToken.CanBeCanceled)
			{
				this._lazyCompletionTaskSource = new TaskCompletionSource<VoidResult>();
				if (dataflowBlockOptions.CancellationToken.IsCancellationRequested)
				{
					this._completionReserved = (this._decliningPermanently = true);
					this._lazyCompletionTaskSource.SetCanceled();
				}
				else
				{
					Common.WireCancellationToComplete(dataflowBlockOptions.CancellationToken, this._lazyCompletionTaskSource.Task, delegate(object state)
					{
						((WriteOnceBlock<T>)state).Complete();
					}, this);
				}
			}
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCreated(this, dataflowBlockOptions);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006E68 File Offset: 0x00005068
		private void CompleteBlockAsync(IList<Exception> exceptions)
		{
			if (exceptions == null)
			{
				Task task = new Task(delegate(object state)
				{
					((WriteOnceBlock<T>)state).OfferToTargetsAndCompleteBlock();
				}, this, Common.GetCreationOptionsForTask(false));
				DataflowEtwProvider log = DataflowEtwProvider.Log;
				if (log.IsEnabled())
				{
					log.TaskLaunchedForMessageHandling(this, task, DataflowEtwProvider.TaskLaunchedReason.OfferingOutputMessages, this._header.IsValid ? 1 : 0);
				}
				Exception ex = Common.StartTaskSafe(task, this._dataflowBlockOptions.TaskScheduler);
				if (ex != null)
				{
					this.CompleteCore(ex, true);
					return;
				}
			}
			else
			{
				Task.Factory.StartNew(delegate(object state)
				{
					Tuple<WriteOnceBlock<T>, IList<Exception>> tuple = (Tuple<WriteOnceBlock<T>, IList<Exception>>)state;
					tuple.Item1.CompleteBlock(tuple.Item2);
				}, Tuple.Create<WriteOnceBlock<T>, IList<Exception>>(this, exceptions), CancellationToken.None, Common.GetCreationOptionsForTask(false), TaskScheduler.Default);
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006F2C File Offset: 0x0000512C
		private void OfferToTargetsAndCompleteBlock()
		{
			List<Exception> list = this.OfferToTargets();
			this.CompleteBlock(list);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006F48 File Offset: 0x00005148
		private void CompleteBlock(IList<Exception> exceptions)
		{
			TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo = this._targetRegistry.ClearEntryPoints();
			if (exceptions != null && exceptions.Count > 0)
			{
				this.CompletionTaskSource.TrySetException(exceptions);
			}
			else if (this._dataflowBlockOptions.CancellationToken.IsCancellationRequested)
			{
				this.CompletionTaskSource.TrySetCanceled();
			}
			else if (Interlocked.CompareExchange<TaskCompletionSource<VoidResult>>(ref this._lazyCompletionTaskSource, Common.CompletedVoidResultTaskCompletionSource, null) != null)
			{
				this._lazyCompletionTaskSource.TrySetResult(default(VoidResult));
			}
			this._targetRegistry.PropagateCompletion(linkedTargetInfo);
			DataflowEtwProvider log = DataflowEtwProvider.Log;
			if (log.IsEnabled())
			{
				log.DataflowBlockCompleted(this);
			}
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006FE8 File Offset: 0x000051E8
		void IDataflowBlock.Fault(Exception exception)
		{
			if (exception == null)
			{
				throw new ArgumentNullException("exception");
			}
			this.CompleteCore(exception, false);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00007000 File Offset: 0x00005200
		public void Complete()
		{
			this.CompleteCore(null, false);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000700C File Offset: 0x0000520C
		private void CompleteCore(Exception exception, bool storeExceptionEvenIfAlreadyCompleting)
		{
			bool flag = false;
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (this._decliningPermanently && !storeExceptionEvenIfAlreadyCompleting)
				{
					return;
				}
				this._decliningPermanently = true;
				if (!this._completionReserved || storeExceptionEvenIfAlreadyCompleting)
				{
					flag = (this._completionReserved = true);
				}
			}
			if (flag)
			{
				List<Exception> list = null;
				if (exception != null)
				{
					list = new List<Exception>();
					list.Add(exception);
				}
				this.CompleteBlockAsync(list);
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007094 File Offset: 0x00005294
		public bool TryReceive([Nullable(new byte[] { 2, 1 })] Predicate<T> filter, [MaybeNullWhen(false)] out T item)
		{
			if (this._header.IsValid && (filter == null || filter(this._value)))
			{
				item = this.CloneItem(this._value);
				return true;
			}
			item = default(T);
			return false;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000070D0 File Offset: 0x000052D0
		bool IReceivableSourceBlock<T>.TryReceiveAll([NotNullWhen(true)] out IList<T> items)
		{
			T t;
			if (this.TryReceive(null, out t))
			{
				items = new T[] { t };
				return true;
			}
			items = null;
			return false;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00007100 File Offset: 0x00005300
		public IDisposable LinkTo(ITargetBlock<T> target, DataflowLinkOptions linkOptions)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (linkOptions == null)
			{
				throw new ArgumentNullException("linkOptions");
			}
			object valueLock = this.ValueLock;
			bool hasValue;
			lock (valueLock)
			{
				hasValue = this.HasValue;
				bool completionReserved = this._completionReserved;
				if (!hasValue && !completionReserved)
				{
					this._targetRegistry.Add(ref target, linkOptions);
					return Common.CreateUnlinker<T>(this.ValueLock, this._targetRegistry, target);
				}
			}
			if (hasValue)
			{
				bool flag2 = this._cloningFunction != null;
				target.OfferMessage(this._header, this._value, this, flag2);
			}
			if (linkOptions.PropagateCompletion)
			{
				Common.PropagateCompletionOnceCompleted(this.Completion, target);
			}
			return Disposables.Nop;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000071D0 File Offset: 0x000053D0
		public Task Completion
		{
			get
			{
				return this.CompletionTaskSource.Task;
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000071E0 File Offset: 0x000053E0
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
			bool flag = false;
			object valueLock = this.ValueLock;
			lock (valueLock)
			{
				if (this._decliningPermanently)
				{
					return DataflowMessageStatus.DecliningPermanently;
				}
				if (consumeToAccept)
				{
					bool flag3;
					messageValue = source.ConsumeMessage(messageHeader, this, out flag3);
					if (!flag3)
					{
						return DataflowMessageStatus.NotAvailable;
					}
				}
				this._header = Common.SingleMessageHeader;
				this._value = messageValue;
				this._decliningPermanently = true;
				if (!this._completionReserved)
				{
					flag = (this._completionReserved = true);
				}
			}
			if (flag)
			{
				this.CompleteBlockAsync(null);
			}
			return DataflowMessageStatus.Accepted;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000072B0 File Offset: 0x000054B0
		T ISourceBlock<T>.ConsumeMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target, out bool messageConsumed)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (this._header.Id == messageHeader.Id)
			{
				messageConsumed = true;
				return this.CloneItem(this._value);
			}
			messageConsumed = false;
			return default(T);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00007314 File Offset: 0x00005514
		bool ISourceBlock<T>.ReserveMessage(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return this._header.Id == messageHeader.Id;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007354 File Offset: 0x00005554
		void ISourceBlock<T>.ReleaseReservation(DataflowMessageHeader messageHeader, ITargetBlock<T> target)
		{
			if (!messageHeader.IsValid)
			{
				throw new ArgumentException(SR.Argument_InvalidMessageHeader, "messageHeader");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (this._header.Id != messageHeader.Id)
			{
				throw new InvalidOperationException(SR.InvalidOperation_MessageNotReservedByTarget);
			}
			bool flag = this._cloningFunction != null;
			target.OfferMessage(this._header, this._value, this, flag);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000073C6 File Offset: 0x000055C6
		private T CloneItem(T item)
		{
			if (this._cloningFunction == null)
			{
				return item;
			}
			return this._cloningFunction(item);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000073E0 File Offset: 0x000055E0
		private List<Exception> OfferToTargets()
		{
			List<Exception> list = null;
			if (this.HasValue)
			{
				TargetRegistry<T>.LinkedTargetInfo next;
				for (TargetRegistry<T>.LinkedTargetInfo linkedTargetInfo = this._targetRegistry.FirstTargetNode; linkedTargetInfo != null; linkedTargetInfo = next)
				{
					next = linkedTargetInfo.Next;
					ITargetBlock<T> target = linkedTargetInfo.Target;
					try
					{
						bool flag = this._cloningFunction != null;
						target.OfferMessage(this._header, this._value, this, flag);
					}
					catch (Exception ex)
					{
						Common.StoreDataflowMessageValueIntoExceptionData<T>(ex, this._value, false);
						Common.AddException(ref list, ex, false);
					}
				}
			}
			return list;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00007468 File Offset: 0x00005668
		private TaskCompletionSource<VoidResult> CompletionTaskSource
		{
			get
			{
				if (this._lazyCompletionTaskSource == null)
				{
					Interlocked.CompareExchange<TaskCompletionSource<VoidResult>>(ref this._lazyCompletionTaskSource, new TaskCompletionSource<VoidResult>(), null);
				}
				return this._lazyCompletionTaskSource;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000748A File Offset: 0x0000568A
		private bool HasValue
		{
			get
			{
				return this._header.IsValid;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00007498 File Offset: 0x00005698
		[Nullable(2)]
		private T Value
		{
			get
			{
				if (!this._header.IsValid)
				{
					return default(T);
				}
				return this._value;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000074C2 File Offset: 0x000056C2
		public override string ToString()
		{
			return Common.GetNameForDebugger(this, this._dataflowBlockOptions);
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x000074D0 File Offset: 0x000056D0
		private object DebuggerDisplayContent
		{
			get
			{
				return string.Format("{0}, HasValue={1}, Value={2}", Common.GetNameForDebugger(this, this._dataflowBlockOptions), this.HasValue, this.Value);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000074FE File Offset: 0x000056FE
		object IDebuggerDisplay.Content
		{
			get
			{
				return this.DebuggerDisplayContent;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly TargetRegistry<T> _targetRegistry;

		// Token: 0x0400005C RID: 92
		private readonly Func<T, T> _cloningFunction;

		// Token: 0x0400005D RID: 93
		private readonly DataflowBlockOptions _dataflowBlockOptions;

		// Token: 0x0400005E RID: 94
		private TaskCompletionSource<VoidResult> _lazyCompletionTaskSource;

		// Token: 0x0400005F RID: 95
		private bool _decliningPermanently;

		// Token: 0x04000060 RID: 96
		private bool _completionReserved;

		// Token: 0x04000061 RID: 97
		private DataflowMessageHeader _header;

		// Token: 0x04000062 RID: 98
		private T _value;

		// Token: 0x02000076 RID: 118
		private sealed class DebugView
		{
			// Token: 0x06000404 RID: 1028 RVA: 0x0000F88E File Offset: 0x0000DA8E
			public DebugView(WriteOnceBlock<T> writeOnceBlock)
			{
				this._writeOnceBlock = writeOnceBlock;
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000F89D File Offset: 0x0000DA9D
			public bool IsCompleted
			{
				get
				{
					return this._writeOnceBlock.Completion.IsCompleted;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000F8AF File Offset: 0x0000DAAF
			public int Id
			{
				get
				{
					return Common.GetBlockId(this._writeOnceBlock);
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000F8BC File Offset: 0x0000DABC
			public bool HasValue
			{
				get
				{
					return this._writeOnceBlock.HasValue;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
			public T Value
			{
				get
				{
					return this._writeOnceBlock.Value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000F8D6 File Offset: 0x0000DAD6
			public DataflowBlockOptions DataflowBlockOptions
			{
				get
				{
					return this._writeOnceBlock._dataflowBlockOptions;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000F8E3 File Offset: 0x0000DAE3
			public TargetRegistry<T> LinkedTargets
			{
				get
				{
					return this._writeOnceBlock._targetRegistry;
				}
			}

			// Token: 0x0400018E RID: 398
			private readonly WriteOnceBlock<T> _writeOnceBlock;
		}
	}
}
