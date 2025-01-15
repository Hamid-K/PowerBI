using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000CE RID: 206
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class BlockingQueue<[global::System.Runtime.CompilerServices.Nullable(2)] T>
	{
		// Token: 0x060003AF RID: 943 RVA: 0x0000B05C File Offset: 0x0000925C
		internal BlockingQueue(int count)
		{
			this.items = new T[count];
			this.head = 0;
			this.count = 0;
			this.notEmpty = new ManualResetEvent(false);
			this.notFull = new ManualResetEvent(true);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000B0AC File Offset: 0x000092AC
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

		// Token: 0x060003B1 RID: 945 RVA: 0x0000B194 File Offset: 0x00009394
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

		// Token: 0x0400039A RID: 922
		private readonly T[] items;

		// Token: 0x0400039B RID: 923
		private readonly ManualResetEvent notFull;

		// Token: 0x0400039C RID: 924
		private readonly ManualResetEvent notEmpty;

		// Token: 0x0400039D RID: 925
		private readonly object syncRoot = new object();

		// Token: 0x0400039E RID: 926
		private int head;

		// Token: 0x0400039F RID: 927
		private int count;
	}
}
