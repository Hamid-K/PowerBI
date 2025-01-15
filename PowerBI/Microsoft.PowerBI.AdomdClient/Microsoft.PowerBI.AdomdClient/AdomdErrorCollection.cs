using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public sealed class AdomdErrorCollection : ICollection, IEnumerable
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x00021CCB File Offset: 0x0001FECB
		internal AdomdErrorCollection()
		{
		}

		// Token: 0x17000180 RID: 384
		public AdomdError this[int index]
		{
			get
			{
				return (AdomdError)this.errors[index];
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x00021CF1 File Offset: 0x0001FEF1
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x00021CF4 File Offset: 0x0001FEF4
		public object SyncRoot
		{
			get
			{
				return this.errors.SyncRoot;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00021D01 File Offset: 0x0001FF01
		public int Count
		{
			get
			{
				return this.errors.Count;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00021D0E File Offset: 0x0001FF0E
		public void CopyTo(AdomdError[] array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00021D1D File Offset: 0x0001FF1D
		void ICollection.CopyTo(Array array, int index)
		{
			this.errors.CopyTo(array, index);
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00021D2C File Offset: 0x0001FF2C
		public AdomdErrorCollection.Enumerator GetEnumerator()
		{
			return new AdomdErrorCollection.Enumerator(this);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00021D34 File Offset: 0x0001FF34
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00021D41 File Offset: 0x0001FF41
		internal void Add(AdomdError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x0400043B RID: 1083
		private ArrayList errors = new ArrayList();

		// Token: 0x020001A6 RID: 422
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012E7 RID: 4839 RVA: 0x000435DE File Offset: 0x000417DE
			internal Enumerator(AdomdErrorCollection errors)
			{
				this.enumer = errors.errors.GetEnumerator();
			}

			// Token: 0x17000699 RID: 1689
			// (get) Token: 0x060012E8 RID: 4840 RVA: 0x000435F1 File Offset: 0x000417F1
			public AdomdError Current
			{
				get
				{
					return (AdomdError)this.enumer.Current;
				}
			}

			// Token: 0x1700069A RID: 1690
			// (get) Token: 0x060012E9 RID: 4841 RVA: 0x00043603 File Offset: 0x00041803
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012EA RID: 4842 RVA: 0x0004360B File Offset: 0x0004180B
			public bool MoveNext()
			{
				return this.enumer.MoveNext();
			}

			// Token: 0x060012EB RID: 4843 RVA: 0x00043618 File Offset: 0x00041818
			public void Reset()
			{
				this.enumer.Reset();
			}

			// Token: 0x04000CA8 RID: 3240
			private IEnumerator enumer;
		}
	}
}
