using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200001E RID: 30
	[NullableContext(1)]
	[Nullable(0)]
	[StructLayout(LayoutKind.Auto)]
	public struct ManualResetValueTaskSourceCore<[Nullable(2)] TResult>
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000235F File Offset: 0x0000055F
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00002367 File Offset: 0x00000567
		public bool RunContinuationsAsynchronously { readonly get; set; }

		// Token: 0x0600003D RID: 61 RVA: 0x00002370 File Offset: 0x00000570
		public void Reset()
		{
			this._version += 1;
			this._completed = false;
			this._result = default(TResult);
			this._error = null;
			this._executionContext = null;
			this._capturedContext = null;
			this._continuation = null;
			this._continuationState = null;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000023C2 File Offset: 0x000005C2
		public void SetResult(TResult result)
		{
			this._result = result;
			this.SignalCompletion();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000023D1 File Offset: 0x000005D1
		public void SetException(Exception error)
		{
			this._error = ExceptionDispatchInfo.Capture(error);
			this.SignalCompletion();
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000023E5 File Offset: 0x000005E5
		public short Version
		{
			get
			{
				return this._version;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000023ED File Offset: 0x000005ED
		public ValueTaskSourceStatus GetStatus(short token)
		{
			this.ValidateToken(token);
			if (this._continuation == null || !this._completed)
			{
				return ValueTaskSourceStatus.Pending;
			}
			if (this._error == null)
			{
				return ValueTaskSourceStatus.Succeeded;
			}
			if (!(this._error.SourceException is OperationCanceledException))
			{
				return ValueTaskSourceStatus.Faulted;
			}
			return ValueTaskSourceStatus.Canceled;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002427 File Offset: 0x00000627
		public TResult GetResult(short token)
		{
			this.ValidateToken(token);
			if (!this._completed)
			{
				throw new InvalidOperationException();
			}
			ExceptionDispatchInfo error = this._error;
			if (error != null)
			{
				error.Throw();
			}
			return this._result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002458 File Offset: 0x00000658
		[NullableContext(2)]
		public void OnCompleted([Nullable(new byte[] { 1, 2 })] Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
		{
			if (continuation == null)
			{
				throw new ArgumentNullException("continuation");
			}
			this.ValidateToken(token);
			if ((flags & ValueTaskSourceOnCompletedFlags.FlowExecutionContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				this._executionContext = ExecutionContext.Capture();
			}
			if ((flags & ValueTaskSourceOnCompletedFlags.UseSchedulingContext) != ValueTaskSourceOnCompletedFlags.None)
			{
				SynchronizationContext synchronizationContext = SynchronizationContext.Current;
				if (synchronizationContext != null && synchronizationContext.GetType() != typeof(SynchronizationContext))
				{
					this._capturedContext = synchronizationContext;
				}
				else
				{
					TaskScheduler taskScheduler = TaskScheduler.Current;
					if (taskScheduler != TaskScheduler.Default)
					{
						this._capturedContext = taskScheduler;
					}
				}
			}
			object obj = this._continuation;
			if (obj == null)
			{
				this._continuationState = state;
				obj = Interlocked.CompareExchange<Action<object>>(ref this._continuation, continuation, null);
			}
			if (obj != null)
			{
				if (obj != ManualResetValueTaskSourceCoreShared.s_sentinel)
				{
					throw new InvalidOperationException();
				}
				object capturedContext = this._capturedContext;
				if (capturedContext == null)
				{
					Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
					return;
				}
				SynchronizationContext synchronizationContext2 = capturedContext as SynchronizationContext;
				if (synchronizationContext2 != null)
				{
					synchronizationContext2.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create<Action<object>, object>(continuation, state));
					return;
				}
				TaskScheduler taskScheduler2 = capturedContext as TaskScheduler;
				if (taskScheduler2 == null)
				{
					return;
				}
				Task.Factory.StartNew(continuation, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler2);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002582 File Offset: 0x00000782
		private void ValidateToken(short token)
		{
			if (token != this._version)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002594 File Offset: 0x00000794
		private void SignalCompletion()
		{
			if (this._completed)
			{
				throw new InvalidOperationException();
			}
			this._completed = true;
			if (this._continuation != null || Interlocked.CompareExchange<Action<object>>(ref this._continuation, ManualResetValueTaskSourceCoreShared.s_sentinel, null) != null)
			{
				if (this._executionContext != null)
				{
					ExecutionContext.Run(this._executionContext, delegate(object s)
					{
						((ManualResetValueTaskSourceCore<TResult>)s).InvokeContinuation();
					}, this);
					return;
				}
				this.InvokeContinuation();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002618 File Offset: 0x00000818
		private void InvokeContinuation()
		{
			object capturedContext = this._capturedContext;
			if (capturedContext != null)
			{
				SynchronizationContext synchronizationContext = capturedContext as SynchronizationContext;
				if (synchronizationContext != null)
				{
					synchronizationContext.Post(delegate(object s)
					{
						Tuple<Action<object>, object> tuple = (Tuple<Action<object>, object>)s;
						tuple.Item1(tuple.Item2);
					}, Tuple.Create<Action<object>, object>(this._continuation, this._continuationState));
					return;
				}
				TaskScheduler taskScheduler = capturedContext as TaskScheduler;
				if (taskScheduler == null)
				{
					return;
				}
				Task.Factory.StartNew(this._continuation, this._continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, taskScheduler);
				return;
			}
			else
			{
				if (this.RunContinuationsAsynchronously)
				{
					Task.Factory.StartNew(this._continuation, this._continuationState, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
					return;
				}
				this._continuation(this._continuationState);
				return;
			}
		}

		// Token: 0x0400001C RID: 28
		private Action<object> _continuation;

		// Token: 0x0400001D RID: 29
		private object _continuationState;

		// Token: 0x0400001E RID: 30
		private ExecutionContext _executionContext;

		// Token: 0x0400001F RID: 31
		private object _capturedContext;

		// Token: 0x04000020 RID: 32
		private bool _completed;

		// Token: 0x04000021 RID: 33
		private TResult _result;

		// Token: 0x04000022 RID: 34
		private ExceptionDispatchInfo _error;

		// Token: 0x04000023 RID: 35
		private short _version;
	}
}
