using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A4 RID: 164
	public sealed class MeasureCollection : ICollection, IEnumerable
	{
		// Token: 0x06000966 RID: 2406 RVA: 0x0002883F File Offset: 0x00026A3F
		internal MeasureCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.measureCollectionInternal = new MeasureCollectionInternal(connection, parentCube);
		}

		// Token: 0x17000302 RID: 770
		public Measure this[int index]
		{
			get
			{
				return this.measureCollectionInternal[index];
			}
		}

		// Token: 0x17000303 RID: 771
		public Measure this[string index]
		{
			get
			{
				return this.measureCollectionInternal[index];
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028870 File Offset: 0x00026A70
		public Measure Find(string index)
		{
			return this.measureCollectionInternal.Find(index);
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x0002887E File Offset: 0x00026A7E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600096B RID: 2411 RVA: 0x00028881 File Offset: 0x00026A81
		public object SyncRoot
		{
			get
			{
				return this.measureCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0002888E File Offset: 0x00026A8E
		public int Count
		{
			get
			{
				return this.measureCollectionInternal.Count;
			}
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002889B File Offset: 0x00026A9B
		public void CopyTo(Measure[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x000288A8 File Offset: 0x00026AA8
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x000288E3 File Offset: 0x00026AE3
		public MeasureCollection.Enumerator GetEnumerator()
		{
			return new MeasureCollection.Enumerator(this);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x000288EB File Offset: 0x00026AEB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000288F8 File Offset: 0x00026AF8
		internal MeasureCollectionInternal CollectionInternal
		{
			get
			{
				return this.measureCollectionInternal;
			}
		}

		// Token: 0x0400063B RID: 1595
		private MeasureCollectionInternal measureCollectionInternal;

		// Token: 0x020001B3 RID: 435
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600132D RID: 4909 RVA: 0x00043DB4 File Offset: 0x00041FB4
			internal Enumerator(MeasureCollection measures)
			{
				this.measures = measures.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600132E RID: 4910 RVA: 0x00043DC9 File Offset: 0x00041FC9
			internal Enumerator(MeasureCollectionInternal measures)
			{
				this.measures = measures;
				this.currentIndex = -1;
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x0600132F RID: 4911 RVA: 0x00043DDC File Offset: 0x00041FDC
			public Measure Current
			{
				get
				{
					Measure measure;
					try
					{
						measure = this.measures[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return measure;
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x06001330 RID: 4912 RVA: 0x00043E18 File Offset: 0x00042018
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001331 RID: 4913 RVA: 0x00043E20 File Offset: 0x00042020
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.measures.Count;
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x00043E4B File Offset: 0x0004204B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC3 RID: 3267
			private MeasureCollectionInternal measures;

			// Token: 0x04000CC4 RID: 3268
			private int currentIndex;
		}
	}
}
