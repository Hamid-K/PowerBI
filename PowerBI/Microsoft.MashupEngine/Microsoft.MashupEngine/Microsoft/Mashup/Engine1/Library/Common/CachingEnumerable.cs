using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001032 RID: 4146
	internal abstract class CachingEnumerable<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06006C3A RID: 27706 RVA: 0x00175107 File Offset: 0x00173307
		public IEnumerator<T> GetEnumerator()
		{
			return new CachingEnumerable<T>.CachingEnumerator(this);
		}

		// Token: 0x06006C3B RID: 27707 RVA: 0x0017510F File Offset: 0x0017330F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006C3C RID: 27708
		protected abstract IEnumerable<T> GetNextPage(int offset, out bool hasMore);

		// Token: 0x06006C3D RID: 27709 RVA: 0x00175118 File Offset: 0x00173318
		private bool TryGetItem(int i, out T item)
		{
			if (i == this.items.Count)
			{
				if (this.isComplete)
				{
					item = default(T);
					return false;
				}
				IEnumerable<T> nextPage = this.GetNextPage(this.items.Count, out this.isComplete);
				this.items.AddRange(nextPage);
				if (this.items.Count == i)
				{
					item = default(T);
					return false;
				}
			}
			item = this.items[i];
			return true;
		}

		// Token: 0x04003C3B RID: 15419
		private readonly List<T> items = new List<T>();

		// Token: 0x04003C3C RID: 15420
		private bool isComplete;

		// Token: 0x02001033 RID: 4147
		private class CachingEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06006C3F RID: 27711 RVA: 0x001751A5 File Offset: 0x001733A5
			public CachingEnumerator(CachingEnumerable<T> enumerable)
			{
				this.enumerable = enumerable;
			}

			// Token: 0x17001ECF RID: 7887
			// (get) Token: 0x06006C40 RID: 27712 RVA: 0x001751B4 File Offset: 0x001733B4
			public T Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x06006C41 RID: 27713 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x17001ED0 RID: 7888
			// (get) Token: 0x06006C42 RID: 27714 RVA: 0x001751BC File Offset: 0x001733BC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06006C43 RID: 27715 RVA: 0x001751CC File Offset: 0x001733CC
			public bool MoveNext()
			{
				CachingEnumerable<T> cachingEnumerable = this.enumerable;
				int num = this.count;
				this.count = num + 1;
				return cachingEnumerable.TryGetItem(num, out this.current);
			}

			// Token: 0x06006C44 RID: 27716 RVA: 0x001751FB File Offset: 0x001733FB
			public void Reset()
			{
				this.count = 0;
			}

			// Token: 0x04003C3D RID: 15421
			private readonly CachingEnumerable<T> enumerable;

			// Token: 0x04003C3E RID: 15422
			private int count;

			// Token: 0x04003C3F RID: 15423
			private T current;
		}
	}
}
