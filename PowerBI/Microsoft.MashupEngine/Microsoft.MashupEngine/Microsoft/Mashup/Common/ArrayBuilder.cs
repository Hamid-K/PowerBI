using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BD4 RID: 7124
	public struct ArrayBuilder<T>
	{
		// Token: 0x0600B1B8 RID: 45496 RVA: 0x00243FD8 File Offset: 0x002421D8
		public ArrayBuilder(int initialCapacity)
		{
			this.count = 0;
			this.t0 = default(T);
			this.t1 = default(T);
			this.t2 = default(T);
			this.t3 = default(T);
			if (initialCapacity >= 8)
			{
				this.ts = new List<T>(initialCapacity);
				return;
			}
			this.ts = null;
		}

		// Token: 0x17002CAE RID: 11438
		// (get) Token: 0x0600B1B9 RID: 45497 RVA: 0x00244034 File Offset: 0x00242234
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600B1BA RID: 45498 RVA: 0x0024403C File Offset: 0x0024223C
		public void Add(T t)
		{
			switch (this.count)
			{
			case 0:
				this.t0 = t;
				goto IL_005F;
			case 1:
				this.t1 = t;
				goto IL_005F;
			case 2:
				this.t2 = t;
				goto IL_005F;
			case 3:
				this.t3 = t;
				goto IL_005F;
			case 4:
				this.ts = new List<T>(8);
				break;
			}
			this.ts.Add(t);
			IL_005F:
			this.count++;
		}

		// Token: 0x0600B1BB RID: 45499 RVA: 0x002440B8 File Offset: 0x002422B8
		public T[] ToArray()
		{
			switch (this.count)
			{
			case 0:
				return EmptyArray<T>.Instance;
			case 1:
				return new T[] { this.t0 };
			case 2:
				return new T[] { this.t0, this.t1 };
			case 3:
				return new T[] { this.t0, this.t1, this.t2 };
			case 4:
				return new T[] { this.t0, this.t1, this.t2, this.t3 };
			default:
			{
				T[] array = new T[this.count];
				array[0] = this.t0;
				array[1] = this.t1;
				array[2] = this.t2;
				array[3] = this.t3;
				for (int i = 0; i < this.ts.Count; i++)
				{
					array[i + 4] = this.ts[i];
				}
				return array;
			}
			}
		}

		// Token: 0x04005B17 RID: 23319
		private const int stackCount = 4;

		// Token: 0x04005B18 RID: 23320
		private const int defaultInitialCapacity = 8;

		// Token: 0x04005B19 RID: 23321
		private int count;

		// Token: 0x04005B1A RID: 23322
		private T t0;

		// Token: 0x04005B1B RID: 23323
		private T t1;

		// Token: 0x04005B1C RID: 23324
		private T t2;

		// Token: 0x04005B1D RID: 23325
		private T t3;

		// Token: 0x04005B1E RID: 23326
		private List<T> ts;
	}
}
