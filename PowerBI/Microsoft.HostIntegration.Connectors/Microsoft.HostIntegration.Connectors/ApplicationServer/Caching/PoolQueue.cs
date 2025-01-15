using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000358 RID: 856
	internal class PoolQueue<T>
	{
		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0005A299 File Offset: 0x00058499
		private long Head
		{
			get
			{
				return this.head % this.size;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0005A2A8 File Offset: 0x000584A8
		private long Tail
		{
			get
			{
				return this.tail % this.size;
			}
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x0005A2B8 File Offset: 0x000584B8
		public PoolQueue(long size, T defaultValue)
		{
			this.defaultValue = defaultValue;
			this.size = size;
			this.slots = new T[this.size];
			int num = 0;
			while ((long)num < size)
			{
				this.slots[num] = this.defaultValue;
				num++;
			}
			this.head = 0L;
			this.tail = 0L;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0005A33C File Offset: 0x0005853C
		public bool Enqueue(T obj)
		{
			for (int i = 0; i < 3; i++)
			{
				if (this.EnqueueInternal(obj))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x0005A364 File Offset: 0x00058564
		private bool EnqueueInternal(T obj)
		{
			bool flag2;
			lock (this.insertLock)
			{
				long num = this.head - this.tail;
				if (num == this.size)
				{
					flag2 = false;
				}
				else
				{
					long num2 = this.Head;
					checked
					{
						this.valueComparer.Compare(this.slots[(int)((IntPtr)num2)], this.defaultValue);
						this.slots[(int)((IntPtr)num2)] = obj;
					}
					this.head += 1L;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0005A404 File Offset: 0x00058604
		public T Dequeue()
		{
			for (int i = 0; i < 3; i++)
			{
				T t = this.DequeueInternal();
				if (this.valueComparer.Compare(t, this.defaultValue) != 0)
				{
					return t;
				}
			}
			return this.defaultValue;
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0005A440 File Offset: 0x00058640
		private T DequeueInternal()
		{
			T t;
			lock (this.deleteLock)
			{
				long num = this.head - this.tail;
				if (num == 0L)
				{
					t = this.defaultValue;
				}
				else
				{
					long num2 = this.Tail;
					T t2;
					checked
					{
						t2 = this.slots[(int)((IntPtr)num2)];
						this.valueComparer.Compare(t2, this.defaultValue);
						this.slots[(int)((IntPtr)this.Tail)] = this.defaultValue;
					}
					this.tail += 1L;
					t = t2;
				}
			}
			return t;
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0005A4F0 File Offset: 0x000586F0
		public long SafeCount
		{
			get
			{
				long num;
				lock (this.insertLock)
				{
					lock (this.deleteLock)
					{
						num = this.head - this.tail;
					}
				}
				return num;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0005A564 File Offset: 0x00058764
		public long Count
		{
			get
			{
				return this.head - this.tail;
			}
		}

		// Token: 0x040010F5 RID: 4341
		private const int NumberOfRetries = 3;

		// Token: 0x040010F6 RID: 4342
		private long size;

		// Token: 0x040010F7 RID: 4343
		private object insertLock = new object();

		// Token: 0x040010F8 RID: 4344
		private object deleteLock = new object();

		// Token: 0x040010F9 RID: 4345
		private T[] slots;

		// Token: 0x040010FA RID: 4346
		private long head;

		// Token: 0x040010FB RID: 4347
		private long tail;

		// Token: 0x040010FC RID: 4348
		private T defaultValue;

		// Token: 0x040010FD RID: 4349
		private Comparer<T> valueComparer = Comparer<T>.Default;
	}
}
