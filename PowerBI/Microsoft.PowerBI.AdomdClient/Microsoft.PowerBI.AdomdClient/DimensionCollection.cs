using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000083 RID: 131
	public sealed class DimensionCollection : ICollection, IEnumerable
	{
		// Token: 0x06000831 RID: 2097 RVA: 0x00026CC6 File Offset: 0x00024EC6
		internal DimensionCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.dimensionCollectionInternal = new DimensionCollectionInternal(connection, parentCube);
		}

		// Token: 0x17000256 RID: 598
		public Dimension this[int index]
		{
			get
			{
				return this.dimensionCollectionInternal[index];
			}
		}

		// Token: 0x17000257 RID: 599
		public Dimension this[string index]
		{
			get
			{
				return this.dimensionCollectionInternal[index];
			}
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00026CF7 File Offset: 0x00024EF7
		public Dimension Find(string index)
		{
			return this.dimensionCollectionInternal.Find(index);
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00026D05 File Offset: 0x00024F05
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00026D08 File Offset: 0x00024F08
		public object SyncRoot
		{
			get
			{
				return this.dimensionCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00026D15 File Offset: 0x00024F15
		public int Count
		{
			get
			{
				return this.dimensionCollectionInternal.Count;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00026D22 File Offset: 0x00024F22
		public void CopyTo(Dimension[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00026D2C File Offset: 0x00024F2C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00026D67 File Offset: 0x00024F67
		public DimensionCollection.Enumerator GetEnumerator()
		{
			return new DimensionCollection.Enumerator(this);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00026D6F File Offset: 0x00024F6F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00026D7C File Offset: 0x00024F7C
		internal DimensionCollectionInternal CollectionInternal
		{
			get
			{
				return this.dimensionCollectionInternal;
			}
		}

		// Token: 0x0400057F RID: 1407
		private DimensionCollectionInternal dimensionCollectionInternal;

		// Token: 0x020001AE RID: 430
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600130F RID: 4879 RVA: 0x00043A94 File Offset: 0x00041C94
			internal Enumerator(DimensionCollection dimensions)
			{
				this.dimensions = dimensions.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001310 RID: 4880 RVA: 0x00043AA9 File Offset: 0x00041CA9
			internal Enumerator(DimensionCollectionInternal dimensions)
			{
				this.dimensions = dimensions;
				this.currentIndex = -1;
			}

			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x06001311 RID: 4881 RVA: 0x00043ABC File Offset: 0x00041CBC
			public Dimension Current
			{
				get
				{
					Dimension dimension;
					try
					{
						dimension = this.dimensions[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return dimension;
				}
			}

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x06001312 RID: 4882 RVA: 0x00043AF8 File Offset: 0x00041CF8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001313 RID: 4883 RVA: 0x00043B00 File Offset: 0x00041D00
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.dimensions.Count;
			}

			// Token: 0x06001314 RID: 4884 RVA: 0x00043B2B File Offset: 0x00041D2B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CB9 RID: 3257
			private DimensionCollectionInternal dimensions;

			// Token: 0x04000CBA RID: 3258
			private int currentIndex;
		}
	}
}
