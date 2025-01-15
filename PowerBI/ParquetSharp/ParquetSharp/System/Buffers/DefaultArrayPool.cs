using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x020000F4 RID: 244
	internal sealed class DefaultArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002B34C File Offset: 0x0002954C
		internal DefaultArrayPool()
			: this(1048576, 50)
		{
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0002B35C File Offset: 0x0002955C
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

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x0002B400 File Offset: 0x00029600
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x0002B408 File Offset: 0x00029608
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
						goto IL_00B3;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, this._buckets[num2].Id);
				}
				return array2;
				IL_00B3:
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

		// Token: 0x060008FC RID: 2300 RVA: 0x0002B544 File Offset: 0x00029744
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

		// Token: 0x040002B2 RID: 690
		private const int DefaultMaxArrayLength = 1048576;

		// Token: 0x040002B3 RID: 691
		private const int DefaultMaxNumberOfArraysPerBucket = 50;

		// Token: 0x040002B4 RID: 692
		private static T[] s_emptyArray;

		// Token: 0x040002B5 RID: 693
		private readonly DefaultArrayPool<T>.Bucket[] _buckets;

		// Token: 0x0200015F RID: 351
		private sealed class Bucket
		{
			// Token: 0x06000A2D RID: 2605 RVA: 0x0002C99C File Offset: 0x0002AB9C
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x1700011D RID: 285
			// (get) Token: 0x06000A2E RID: 2606 RVA: 0x0002C9D0 File Offset: 0x0002ABD0
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x06000A2F RID: 2607 RVA: 0x0002C9D8 File Offset: 0x0002ABD8
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

			// Token: 0x06000A30 RID: 2608 RVA: 0x0002CAA4 File Offset: 0x0002ACA4
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException(System.Buffers241621.SR.ArgumentException_BufferNotFromPool, "array");
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

			// Token: 0x04000361 RID: 865
			internal readonly int _bufferLength;

			// Token: 0x04000362 RID: 866
			private readonly T[][] _buffers;

			// Token: 0x04000363 RID: 867
			private readonly int _poolId;

			// Token: 0x04000364 RID: 868
			private SpinLock _lock;

			// Token: 0x04000365 RID: 869
			private int _index;
		}
	}
}
