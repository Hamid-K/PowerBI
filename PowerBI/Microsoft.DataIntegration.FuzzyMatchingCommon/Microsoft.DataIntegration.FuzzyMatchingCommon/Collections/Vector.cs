using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Collections
{
	// Token: 0x020000A3 RID: 163
	[Serializable]
	public class Vector<T> : IMemoryUsage
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600073C RID: 1852 RVA: 0x00027C45 File Offset: 0x00025E45
		// (set) Token: 0x0600073D RID: 1853 RVA: 0x00027C4D File Offset: 0x00025E4D
		public T[] Items { get; protected set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600073E RID: 1854 RVA: 0x00027C56 File Offset: 0x00025E56
		// (set) Token: 0x0600073F RID: 1855 RVA: 0x00027C5E File Offset: 0x00025E5E
		public int Count { get; set; }

		// Token: 0x06000740 RID: 1856 RVA: 0x00027C67 File Offset: 0x00025E67
		public Vector()
			: this(1)
		{
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00027C70 File Offset: 0x00025E70
		public Vector(int capacity)
		{
			this.Items = new T[capacity];
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00027C84 File Offset: 0x00025E84
		public Vector(T[] items)
		{
			this.Items = items;
			this.Count = items.Length;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00027C9C File Offset: 0x00025E9C
		public Vector(T[] items, int count)
		{
			this.Items = items;
			this.Count = count;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00027CB2 File Offset: 0x00025EB2
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x00027CBC File Offset: 0x00025EBC
		public virtual int Capacity
		{
			get
			{
				return this.Items.Length;
			}
			set
			{
				if (value > this.Items.Length)
				{
					T[] items = this.Items;
					Array.Resize<T>(ref items, value);
					this.Items = items;
				}
			}
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x00027CEC File Offset: 0x00025EEC
		public void Add(T item)
		{
			if (this.Count == this.Capacity)
			{
				this.Capacity = Math.Max(1, this.Capacity * 2);
			}
			T[] items = this.Items;
			int count = this.Count;
			this.Count = count + 1;
			items[count] = item;
		}

		// Token: 0x17000121 RID: 289
		public T this[int index]
		{
			get
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				return this.Items[index];
			}
			set
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.Items[index] = value;
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00027D73 File Offset: 0x00025F73
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00027D7C File Offset: 0x00025F7C
		public void Truncate(int newCount)
		{
			if (newCount > this.Count)
			{
				throw new ArgumentException("New count must be smaller than current count");
			}
			this.Count = newCount;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00027D99 File Offset: 0x00025F99
		public IEnumerator<T> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Count; i = num + 1)
			{
				yield return this.Items[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00027DA8 File Offset: 0x00025FA8
		public long MemoryUsage
		{
			get
			{
				return (long)(20 + Math.Max(8, this.Items.Length * 4));
			}
		}

		// Token: 0x04000177 RID: 375
		private const int DefaultCapacity = 1;
	}
}
