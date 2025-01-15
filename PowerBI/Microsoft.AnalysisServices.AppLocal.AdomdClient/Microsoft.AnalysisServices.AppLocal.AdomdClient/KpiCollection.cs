using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009A RID: 154
	public sealed class KpiCollection : ICollection, IEnumerable
	{
		// Token: 0x060008ED RID: 2285 RVA: 0x00027E37 File Offset: 0x00026037
		internal KpiCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.kpiCollectionInternal = new KpiCollectionInternal(connection, parentCube);
		}

		// Token: 0x170002BF RID: 703
		public Kpi this[int index]
		{
			get
			{
				return this.kpiCollectionInternal[index];
			}
		}

		// Token: 0x170002C0 RID: 704
		public Kpi this[string index]
		{
			get
			{
				return this.kpiCollectionInternal[index];
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00027E68 File Offset: 0x00026068
		public Kpi Find(string index)
		{
			return this.kpiCollectionInternal.Find(index);
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00027E76 File Offset: 0x00026076
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x00027E79 File Offset: 0x00026079
		public object SyncRoot
		{
			get
			{
				return this.kpiCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00027E86 File Offset: 0x00026086
		public int Count
		{
			get
			{
				return this.kpiCollectionInternal.Count;
			}
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00027E93 File Offset: 0x00026093
		public void CopyTo(Kpi[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00027EA0 File Offset: 0x000260A0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00027EDB File Offset: 0x000260DB
		public KpiCollection.Enumerator GetEnumerator()
		{
			return new KpiCollection.Enumerator(this);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00027EE3 File Offset: 0x000260E3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x00027EF0 File Offset: 0x000260F0
		internal KpiCollectionInternal CollectionInternal
		{
			get
			{
				return this.kpiCollectionInternal;
			}
		}

		// Token: 0x040005EB RID: 1515
		private KpiCollectionInternal kpiCollectionInternal;

		// Token: 0x020001B0 RID: 432
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001328 RID: 4904 RVA: 0x00044110 File Offset: 0x00042310
			internal Enumerator(KpiCollection kpis)
			{
				this.kpis = kpis.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001329 RID: 4905 RVA: 0x00044125 File Offset: 0x00042325
			internal Enumerator(KpiCollectionInternal kpis)
			{
				this.kpis = kpis;
				this.currentIndex = -1;
			}

			// Token: 0x170006B1 RID: 1713
			// (get) Token: 0x0600132A RID: 4906 RVA: 0x00044138 File Offset: 0x00042338
			public Kpi Current
			{
				get
				{
					Kpi kpi;
					try
					{
						kpi = this.kpis[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return kpi;
				}
			}

			// Token: 0x170006B2 RID: 1714
			// (get) Token: 0x0600132B RID: 4907 RVA: 0x00044174 File Offset: 0x00042374
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x0004417C File Offset: 0x0004237C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.kpis.Count;
			}

			// Token: 0x0600132D RID: 4909 RVA: 0x000441A7 File Offset: 0x000423A7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCE RID: 3278
			private KpiCollectionInternal kpis;

			// Token: 0x04000CCF RID: 3279
			private int currentIndex;
		}
	}
}
