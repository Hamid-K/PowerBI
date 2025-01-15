using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000006 RID: 6
	internal sealed class DefaultArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023CD File Offset: 0x000005CD
		internal DefaultArrayPool()
			: this(1048576, 50)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023DC File Offset: 0x000005DC
		internal DefaultArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			int num = Utilities.SelectBucketIndex(maxArrayLength);
			DefaultArrayPool<T>.Bucket[] array = new DefaultArrayPool<T>.Bucket[num + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DefaultArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002463 File Offset: 0x00000663
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000246C File Offset: 0x0000066C
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				T[] array;
				if ((array = DefaultArrayPool<T>.s_emptyArray) == null)
				{
					array = (DefaultArrayPool<T>.s_emptyArray = new T[0]);
				}
				return array;
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array2;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array2 = this._buckets[num2].Rent();
					if (array2 != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_0096;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, this._buckets[num2].Id);
				}
				return array2;
				IL_0096:
				array2 = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array2 = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array2.GetHashCode();
				int num3 = -1;
				log.BufferRented(hashCode, array2.Length, this.Id, num3);
				log.BufferAllocated(hashCode, array2.Length, this.Id, num3, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array2;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002574 File Offset: 0x00000774
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04000005 RID: 5
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x04000006 RID: 6
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x04000007 RID: 7
		private static T[] s_emptyArray;

		// Token: 0x04000008 RID: 8
		private readonly DefaultArrayPool<T>.Bucket[] _buckets;

		// Token: 0x02000009 RID: 9
		private sealed class Bucket
		{
			// Token: 0x0600001F RID: 31 RVA: 0x0000265E File Offset: 0x0000085E
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002463 File Offset: 0x00000663
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x06000021 RID: 33 RVA: 0x00002690 File Offset: 0x00000890
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = array == null;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x06000022 RID: 34 RVA: 0x0000274C File Offset: 0x0000094C
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException(SR.ArgumentException_BufferNotFromPool, "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x0400000D RID: 13
			internal readonly int _bufferLength;

			// Token: 0x0400000E RID: 14
			private readonly T[][] _buffers;

			// Token: 0x0400000F RID: 15
			private readonly int _poolId;

			// Token: 0x04000010 RID: 16
			private SpinLock _lock;

			// Token: 0x04000011 RID: 17
			private int _index;
		}
	}
}
