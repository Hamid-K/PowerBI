using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003D4 RID: 980
	internal sealed class FunctionalList<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x06002789 RID: 10121 RVA: 0x000BABD9 File Offset: 0x000B8DD9
		private FunctionalList()
		{
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000BABE1 File Offset: 0x000B8DE1
		private FunctionalList(T aItem, FunctionalList<T> aCdr)
		{
			this.m_car = aItem;
			this.m_cdr = aCdr;
			this.m_size = aCdr.Count + 1;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000BAC05 File Offset: 0x000B8E05
		internal FunctionalList<T> Add(T aItem)
		{
			return new FunctionalList<T>(aItem, this);
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x0600278C RID: 10124 RVA: 0x000BAC0E File Offset: 0x000B8E0E
		internal T First
		{
			get
			{
				return this.m_car;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x0600278D RID: 10125 RVA: 0x000BAC16 File Offset: 0x000B8E16
		internal FunctionalList<T> Rest
		{
			get
			{
				return this.m_cdr;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x0600278E RID: 10126 RVA: 0x000BAC1E File Offset: 0x000B8E1E
		internal int Count
		{
			get
			{
				return this.m_size;
			}
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000BAC26 File Offset: 0x000B8E26
		internal bool IsEmpty()
		{
			return this.m_size == 0;
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000BAC31 File Offset: 0x000B8E31
		internal int IndexOf(T aItem)
		{
			if (this.Count == 0)
			{
				return -1;
			}
			if (object.Equals(this.First, aItem))
			{
				return this.m_size - 1;
			}
			return this.Rest.IndexOf(aItem);
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000BAC6A File Offset: 0x000B8E6A
		internal bool Contains(T aItem)
		{
			return this.IndexOf(aItem) != -1;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000BAC7C File Offset: 0x000B8E7C
		internal T Get(T aItem)
		{
			if (this.Count == 0)
			{
				return default(T);
			}
			if (object.Equals(this.First, aItem))
			{
				return this.First;
			}
			return this.Rest.Get(aItem);
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000BACC8 File Offset: 0x000B8EC8
		internal FunctionalList<T> Reverse()
		{
			FunctionalList<T> functionalList = FunctionalList<T>.Empty;
			foreach (T t in this)
			{
				functionalList = functionalList.Add(t);
			}
			return functionalList;
		}

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x000BAD18 File Offset: 0x000B8F18
		internal static FunctionalList<T> Empty
		{
			get
			{
				return FunctionalList<T>.m_emptyList;
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000BAD1F File Offset: 0x000B8F1F
		public IEnumerator<T> GetEnumerator()
		{
			return new FunctionalList<T>.FunctionalListEnumerator(this);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000BAD27 File Offset: 0x000B8F27
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400168A RID: 5770
		private T m_car;

		// Token: 0x0400168B RID: 5771
		private FunctionalList<T> m_cdr;

		// Token: 0x0400168C RID: 5772
		private int m_size;

		// Token: 0x0400168D RID: 5773
		private static FunctionalList<T> m_emptyList = new FunctionalList<T>();

		// Token: 0x02000966 RID: 2406
		private class FunctionalListEnumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x06008027 RID: 32807 RVA: 0x00210923 File Offset: 0x0020EB23
			public FunctionalListEnumerator(FunctionalList<T> aList)
			{
				this.m_list = aList;
			}

			// Token: 0x1700297F RID: 10623
			// (get) Token: 0x06008028 RID: 32808 RVA: 0x00210932 File Offset: 0x0020EB32
			public T Current
			{
				get
				{
					if (this.m_rest == null)
					{
						throw new InvalidOperationException("MoveNext must be called before calling Current");
					}
					return this.m_item;
				}
			}

			// Token: 0x06008029 RID: 32809 RVA: 0x0021094D File Offset: 0x0020EB4D
			public void Dispose()
			{
			}

			// Token: 0x17002980 RID: 10624
			// (get) Token: 0x0600802A RID: 32810 RVA: 0x0021094F File Offset: 0x0020EB4F
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600802B RID: 32811 RVA: 0x0021095C File Offset: 0x0020EB5C
			public bool MoveNext()
			{
				if (this.m_rest == null)
				{
					this.m_rest = this.m_list;
				}
				if (this.m_rest.Count > 0)
				{
					this.m_item = this.m_rest.First;
					this.m_rest = this.m_rest.Rest;
					return true;
				}
				return false;
			}

			// Token: 0x0600802C RID: 32812 RVA: 0x002109B0 File Offset: 0x0020EBB0
			public void Reset()
			{
				this.m_rest = null;
			}

			// Token: 0x04004097 RID: 16535
			private FunctionalList<T> m_list;

			// Token: 0x04004098 RID: 16536
			private FunctionalList<T> m_rest;

			// Token: 0x04004099 RID: 16537
			private T m_item;
		}
	}
}
