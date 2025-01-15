using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000083 RID: 131
	public sealed class DimensionCollection : ICollection, IEnumerable
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00026FF6 File Offset: 0x000251F6
		internal DimensionCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.dimensionCollectionInternal = new DimensionCollectionInternal(connection, parentCube);
		}

		// Token: 0x1700025C RID: 604
		public Dimension this[int index]
		{
			get
			{
				return this.dimensionCollectionInternal[index];
			}
		}

		// Token: 0x1700025D RID: 605
		public Dimension this[string index]
		{
			get
			{
				return this.dimensionCollectionInternal[index];
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027027 File Offset: 0x00025227
		public Dimension Find(string index)
		{
			return this.dimensionCollectionInternal.Find(index);
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00027035 File Offset: 0x00025235
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00027038 File Offset: 0x00025238
		public object SyncRoot
		{
			get
			{
				return this.dimensionCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00027045 File Offset: 0x00025245
		public int Count
		{
			get
			{
				return this.dimensionCollectionInternal.Count;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00027052 File Offset: 0x00025252
		public void CopyTo(Dimension[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002705C File Offset: 0x0002525C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00027097 File Offset: 0x00025297
		public DimensionCollection.Enumerator GetEnumerator()
		{
			return new DimensionCollection.Enumerator(this);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002709F File Offset: 0x0002529F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x000270AC File Offset: 0x000252AC
		internal DimensionCollectionInternal CollectionInternal
		{
			get
			{
				return this.dimensionCollectionInternal;
			}
		}

		// Token: 0x0400058C RID: 1420
		private DimensionCollectionInternal dimensionCollectionInternal;

		// Token: 0x020001AE RID: 430
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600131C RID: 4892 RVA: 0x00043FD0 File Offset: 0x000421D0
			internal Enumerator(DimensionCollection dimensions)
			{
				this.dimensions = dimensions.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600131D RID: 4893 RVA: 0x00043FE5 File Offset: 0x000421E5
			internal Enumerator(DimensionCollectionInternal dimensions)
			{
				this.dimensions = dimensions;
				this.currentIndex = -1;
			}

			// Token: 0x170006AD RID: 1709
			// (get) Token: 0x0600131E RID: 4894 RVA: 0x00043FF8 File Offset: 0x000421F8
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

			// Token: 0x170006AE RID: 1710
			// (get) Token: 0x0600131F RID: 4895 RVA: 0x00044034 File Offset: 0x00042234
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001320 RID: 4896 RVA: 0x0004403C File Offset: 0x0004223C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.dimensions.Count;
			}

			// Token: 0x06001321 RID: 4897 RVA: 0x00044067 File Offset: 0x00042267
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCA RID: 3274
			private DimensionCollectionInternal dimensions;

			// Token: 0x04000CCB RID: 3275
			private int currentIndex;
		}
	}
}
