using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x0200069E RID: 1694
	internal class RefCountList<T> : IRefCountList<T>
	{
		// Token: 0x06002469 RID: 9321 RVA: 0x00066325 File Offset: 0x00064525
		public RefCountList(int readerCount)
		{
			this._readerCount = readerCount;
			this._list = new Dictionary<int, RefCountList<T>.RefCount>();
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x0006633F File Offset: 0x0006453F
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x00066347 File Offset: 0x00064547
		public int ReaderCount
		{
			get
			{
				return this._readerCount;
			}
			set
			{
				this._readerCount = value;
			}
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x00066350 File Offset: 0x00064550
		public void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x0006635D File Offset: 0x0006455D
		public int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x1700062D RID: 1581
		public T this[int i]
		{
			get
			{
				RefCountList<T>.RefCount refCount;
				if (!this._list.TryGetValue(i, out refCount))
				{
					throw new InvalidOperationException("Element no longer available in the buffer.");
				}
				T value = refCount.Value;
				RefCountList<T>.RefCount refCount2 = refCount;
				int num = refCount2.Count - 1;
				refCount2.Count = num;
				if (num == 0)
				{
					this._list.Remove(i);
				}
				return value;
			}
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x000663B6 File Offset: 0x000645B6
		public void Add(T item)
		{
			this._list[this._count] = new RefCountList<T>.RefCount
			{
				Value = item,
				Count = this._readerCount
			};
			this._count++;
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x000663F0 File Offset: 0x000645F0
		public void Done(int index)
		{
			for (int i = index; i < this._count; i++)
			{
				T t = this[i];
			}
			this._readerCount--;
		}

		// Token: 0x04001166 RID: 4454
		private int _readerCount;

		// Token: 0x04001167 RID: 4455
		private readonly IDictionary<int, RefCountList<T>.RefCount> _list;

		// Token: 0x04001168 RID: 4456
		private int _count;

		// Token: 0x0200069F RID: 1695
		private class RefCount
		{
			// Token: 0x04001169 RID: 4457
			public int Count;

			// Token: 0x0400116A RID: 4458
			public T Value;
		}
	}
}
