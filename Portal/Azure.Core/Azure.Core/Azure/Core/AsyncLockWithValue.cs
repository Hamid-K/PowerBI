using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000069 RID: 105
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AsyncLockWithValue<[Nullable(2)] T>
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000386 RID: 902 RVA: 0x0000A554 File Offset: 0x00008754
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

		// Token: 0x06000387 RID: 903 RVA: 0x0000A598 File Offset: 0x00008798
		public AsyncLockWithValue()
		{
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000A5AB File Offset: 0x000087AB
		public AsyncLockWithValue(T value)
		{
			this._hasValue = true;
			this._value = value;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000A5CC File Offset: 0x000087CC
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

		// Token: 0x0600038A RID: 906 RVA: 0x0000A628 File Offset: 0x00008828
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

		// Token: 0x0600038B RID: 907 RVA: 0x0000A67C File Offset: 0x0000887C
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

		// Token: 0x0600038C RID: 908 RVA: 0x0000A750 File Offset: 0x00008950
		private void Reset(in long lockIndex)
		{
			TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue> taskCompletionSource;
			this.UnlockOrGetNextWaiter(in lockIndex, out taskCompletionSource);
			while (taskCompletionSource != null && !taskCompletionSource.TrySetResult(new AsyncLockWithValue<T>.LockOrValue(this, lockIndex + 1L)))
			{
				this.UnlockOrGetNextWaiter(in lockIndex, out taskCompletionSource);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000A788 File Offset: 0x00008988
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

		// Token: 0x04000179 RID: 377
		private readonly object _syncObj = new object();

		// Token: 0x0400017A RID: 378
		[Nullable(new byte[] { 2, 1, 0, 0 })]
		private Queue<TaskCompletionSource<AsyncLockWithValue<T>.LockOrValue>> _waiters;

		// Token: 0x0400017B RID: 379
		private bool _isLocked;

		// Token: 0x0400017C RID: 380
		private bool _hasValue;

		// Token: 0x0400017D RID: 381
		private long _index;

		// Token: 0x0400017E RID: 382
		[Nullable(2)]
		private T _value;

		// Token: 0x020000F8 RID: 248
		[Nullable(0)]
		public readonly struct LockOrValue : IDisposable
		{
			// Token: 0x170001CB RID: 459
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001A162 File Offset: 0x00018362
			public bool HasValue
			{
				get
				{
					return this._owner == null;
				}
			}

			// Token: 0x170001CC RID: 460
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x0001A170 File Offset: 0x00018370
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

			// Token: 0x06000770 RID: 1904 RVA: 0x0001A196 File Offset: 0x00018396
			public LockOrValue(T value)
			{
				this._owner = null;
				this._value = value;
				this._index = 0L;
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x0001A1AE File Offset: 0x000183AE
			public LockOrValue(AsyncLockWithValue<T> owner, long index)
			{
				this._owner = owner;
				this._index = index;
				this._value = default(T);
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x0001A1CA File Offset: 0x000183CA
			public void SetValue(T value)
			{
				if (this._owner != null)
				{
					this._owner.SetValue(value, in this._index);
					return;
				}
				throw new InvalidOperationException("Value for the lock is set already");
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x0001A1F1 File Offset: 0x000183F1
			public void Dispose()
			{
				AsyncLockWithValue<T> owner = this._owner;
				if (owner == null)
				{
					return;
				}
				owner.Reset(in this._index);
			}

			// Token: 0x0400037A RID: 890
			[Nullable(new byte[] { 2, 1 })]
			private readonly AsyncLockWithValue<T> _owner;

			// Token: 0x0400037B RID: 891
			[Nullable(2)]
			private readonly T _value;

			// Token: 0x0400037C RID: 892
			private readonly long _index;
		}
	}
}
