using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000013 RID: 19
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AsyncLockWithValue<[Nullable(2)] T>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000027C0 File Offset: 0x000009C0
		public bool HasValue
		{
			get
			{
				object syncObj = this._syncObj;
				bool hasValue;
				lock (syncObj)
				{
					hasValue = this._hasValue;
				}
				return hasValue;
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002804 File Offset: 0x00000A04
		public AsyncLockWithValue()
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002817 File Offset: 0x00000A17
		public AsyncLockWithValue(T value)
		{
			this._hasValue = true;
			this._value = value;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002838 File Offset: 0x00000A38
		[NullableContext(2)]
		public bool TryGetValue(out T value)
		{
			object syncObj = this._syncObj;
			lock (syncObj)
			{
				if (this._hasValue)
				{
					value = this._value;
					return true;
				}
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002894 File Offset: 0x00000A94
		[NullableContext(0)]
		public async ValueTask<AsyncLockWithValue<T>.LockOrValue> GetLockOrValueAsync(bool async, CancellationToken cancellationToken = default(CancellationToken))
		{
			object syncObj = this._syncObj;
			TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue> valueTcs;
			lock (syncObj)
			{
				if (this._hasValue)
				{
					return new AsyncLockWithValue<T>.LockOrValue(this._value);
				}
				if (!this._isLocked)
				{
					this._isLocked = true;
					this._index += 1L;
					return new AsyncLockWithValue<T>.LockOrValue(this, this._index);
				}
				cancellationToken.ThrowIfCancellationRequested();
				if (this._waiters == null)
				{
					this._waiters = new Queue<TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue>>();
				}
				valueTcs = new TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue>(async ? TaskCreationOptions.RunContinuationsAsynchronously : TaskCreationOptions.None);
				this._waiters.Enqueue(valueTcs);
			}
			AsyncLockWithValue<T>.LockOrValue lockOrValue;
			try
			{
				if (async)
				{
					lockOrValue = await valueTcs.Task.AwaitWithCancellation(cancellationToken);
				}
				else
				{
					valueTcs.Task.Wait(cancellationToken);
					lockOrValue = valueTcs.Task.EnsureCompleted<AsyncLockWithValue<T>.LockOrValue>();
				}
			}
			catch (OperationCanceledException)
			{
				if (valueTcs.TrySetCanceled(cancellationToken))
				{
					throw;
				}
				lockOrValue = valueTcs.Task.Result;
			}
			return lockOrValue;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028E8 File Offset: 0x00000AE8
		private void SetValue(T value, in long lockIndex)
		{
			object syncObj = this._syncObj;
			Queue<TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue>> waiters;
			lock (syncObj)
			{
				if (lockIndex != this._index)
				{
					throw new InvalidOperationException(string.Format("Disposed {0} tries to set value. Current index: {1}, {2} index: {3}", new object[] { "LockOrValue", this._index, "LockOrValue", lockIndex }));
				}
				this._value = value;
				this._hasValue = true;
				this._index = 0L;
				this._isLocked = false;
				if (this._waiters == null)
				{
					return;
				}
				waiters = this._waiters;
				this._waiters = null;
				goto IL_00AC;
			}
			IL_009A:
			waiters.Dequeue().TrySetResult(new AsyncLockWithValue<T>.LockOrValue(value));
			IL_00AC:
			if (waiters.Count > 0)
			{
				goto IL_009A;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000029BC File Offset: 0x00000BBC
		private void Reset(in long lockIndex)
		{
			TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue> taskCompletionSource;
			this.UnlockOrGetNextWaiter(in lockIndex, out taskCompletionSource);
			while (taskCompletionSource != null && !taskCompletionSource.TrySetResult(new AsyncLockWithValue<T>.LockOrValue(this, lockIndex + 1L)))
			{
				this.UnlockOrGetNextWaiter(in lockIndex, out taskCompletionSource);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000029F4 File Offset: 0x00000BF4
		private void UnlockOrGetNextWaiter(in long lockIndex, [Nullable(new byte[] { 2, 0, 0 })] out TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue> nextWaiter)
		{
			object syncObj = this._syncObj;
			lock (syncObj)
			{
				nextWaiter = null;
				if (this._isLocked && lockIndex == this._index)
				{
					this._index = lockIndex + 1L;
					if (this._waiters == null)
					{
						this._isLocked = false;
					}
					else
					{
						while (this._waiters.Count > 0)
						{
							nextWaiter = this._waiters.Dequeue();
							if (!nextWaiter.Task.IsCompleted)
							{
								return;
							}
						}
						this._isLocked = false;
					}
				}
			}
		}

		// Token: 0x0400002B RID: 43
		private readonly object _syncObj = new object();

		// Token: 0x0400002C RID: 44
		[Nullable(new byte[] { 2, 1, 0, 0 })]
		private Queue<TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue>> _waiters;

		// Token: 0x0400002D RID: 45
		private bool _isLocked;

		// Token: 0x0400002E RID: 46
		private bool _hasValue;

		// Token: 0x0400002F RID: 47
		private long _index;

		// Token: 0x04000030 RID: 48
		[Nullable(2)]
		private T _value;

		// Token: 0x02000094 RID: 148
		[Nullable(0)]
		public readonly struct LockOrValue : IDisposable
		{
			// Token: 0x17000145 RID: 325
			// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000E956 File Offset: 0x0000CB56
			public bool HasValue
			{
				get
				{
					return this._owner == null;
				}
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000E964 File Offset: 0x0000CB64
			public T Value
			{
				get
				{
					if (!this.HasValue)
					{
						throw new InvalidOperationException("Value isn't set");
					}
					return this._value;
				}
			}

			// Token: 0x060004A9 RID: 1193 RVA: 0x0000E98A File Offset: 0x0000CB8A
			public LockOrValue(T value)
			{
				this._owner = null;
				this._value = value;
				this._index = 0L;
			}

			// Token: 0x060004AA RID: 1194 RVA: 0x0000E9A2 File Offset: 0x0000CBA2
			public LockOrValue(AsyncLockWithValue<T> owner, long index)
			{
				this._owner = owner;
				this._index = index;
				this._value = default(T);
			}

			// Token: 0x060004AB RID: 1195 RVA: 0x0000E9BE File Offset: 0x0000CBBE
			public void SetValue(T value)
			{
				if (this._owner != null)
				{
					this._owner.SetValue(value, in this._index);
					return;
				}
				throw new InvalidOperationException("Value for the lock is set already");
			}

			// Token: 0x060004AC RID: 1196 RVA: 0x0000E9E5 File Offset: 0x0000CBE5
			public void Dispose()
			{
				AsyncLockWithValue<T> owner = this._owner;
				if (owner == null)
				{
					return;
				}
				owner.Reset(in this._index);
			}

			// Token: 0x040002AD RID: 685
			[Nullable(new byte[] { 2, 1 })]
			private readonly AsyncLockWithValue<T> _owner;

			// Token: 0x040002AE RID: 686
			[Nullable(2)]
			private readonly T _value;

			// Token: 0x040002AF RID: 687
			private readonly long _index;
		}
	}
}
