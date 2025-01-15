using System;
using System.Threading;

namespace Microsoft.OleDb.Serialization
{
	// Token: 0x02001FAB RID: 8107
	public class BlockingQueue<T>
	{
		// Token: 0x0600C5B8 RID: 50616 RVA: 0x002766E0 File Offset: 0x002748E0
		public BlockingQueue(int count)
		{
			this.items = new T[count];
			this.head = 0;
			this.count = 0;
			this.notEmpty = new ManualResetEvent(false);
			this.notFull = new ManualResetEvent(true);
		}

		// Token: 0x17002FFB RID: 12283
		// (get) Token: 0x0600C5B9 RID: 50617 RVA: 0x00276730 File Offset: 0x00274930
		public bool IsFull
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.count == this.items.Length;
				}
				return flag2;
			}
		}

		// Token: 0x17002FFC RID: 12284
		// (get) Token: 0x0600C5BA RID: 50618 RVA: 0x0027677C File Offset: 0x0027497C
		public bool IsEmpty
		{
			get
			{
				object obj = this.syncRoot;
				bool flag2;
				lock (obj)
				{
					flag2 = this.count == 0;
				}
				return flag2;
			}
		}

		// Token: 0x0600C5BB RID: 50619 RVA: 0x002767C4 File Offset: 0x002749C4
		public T Dequeue()
		{
			T t2;
			for (;;)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.count > 0)
					{
						if (this.count == this.items.Length)
						{
							this.notFull.Set();
						}
						int num = this.head;
						this.head = num + 1;
						int num2 = num;
						if (this.head >= this.items.Length)
						{
							this.head = 0;
						}
						this.count--;
						if (this.count == 0)
						{
							this.notEmpty.Reset();
						}
						T t = this.items[num2];
						T[] array = this.items;
						int num3 = num2;
						t2 = default(T);
						array[num3] = t2;
						t2 = t;
						break;
					}
				}
				this.notEmpty.WaitOne();
			}
			return t2;
		}

		// Token: 0x0600C5BC RID: 50620 RVA: 0x002768AC File Offset: 0x00274AAC
		public void Enqueue(T item)
		{
			for (;;)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.count < this.items.Length)
					{
						if (this.count == 0)
						{
							this.notEmpty.Set();
						}
						int num = this.head + this.count;
						if (num >= this.items.Length)
						{
							num -= this.items.Length;
						}
						this.items[num] = item;
						this.count++;
						if (this.count == this.items.Length)
						{
							this.notFull.Reset();
						}
						break;
					}
				}
				this.notFull.WaitOne();
			}
		}

		// Token: 0x04006507 RID: 25863
		private readonly T[] items;

		// Token: 0x04006508 RID: 25864
		private readonly ManualResetEvent notFull;

		// Token: 0x04006509 RID: 25865
		private readonly ManualResetEvent notEmpty;

		// Token: 0x0400650A RID: 25866
		private int head;

		// Token: 0x0400650B RID: 25867
		private int count;

		// Token: 0x0400650C RID: 25868
		private readonly object syncRoot = new object();
	}
}
