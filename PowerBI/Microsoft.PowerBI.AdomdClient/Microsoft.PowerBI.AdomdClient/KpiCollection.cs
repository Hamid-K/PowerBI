using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009A RID: 154
	public sealed class KpiCollection : ICollection, IEnumerable
	{
		// Token: 0x060008E0 RID: 2272 RVA: 0x00027B07 File Offset: 0x00025D07
		internal KpiCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.kpiCollectionInternal = new KpiCollectionInternal(connection, parentCube);
		}

		// Token: 0x170002B9 RID: 697
		public Kpi this[int index]
		{
			get
			{
				return this.kpiCollectionInternal[index];
			}
		}

		// Token: 0x170002BA RID: 698
		public Kpi this[string index]
		{
			get
			{
				return this.kpiCollectionInternal[index];
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00027B38 File Offset: 0x00025D38
		public Kpi Find(string index)
		{
			return this.kpiCollectionInternal.Find(index);
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00027B46 File Offset: 0x00025D46
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00027B49 File Offset: 0x00025D49
		public object SyncRoot
		{
			get
			{
				return this.kpiCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00027B56 File Offset: 0x00025D56
		public int Count
		{
			get
			{
				return this.kpiCollectionInternal.Count;
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00027B63 File Offset: 0x00025D63
		public void CopyTo(Kpi[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00027B70 File Offset: 0x00025D70
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00027BAB File Offset: 0x00025DAB
		public KpiCollection.Enumerator GetEnumerator()
		{
			return new KpiCollection.Enumerator(this);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00027BB3 File Offset: 0x00025DB3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00027BC0 File Offset: 0x00025DC0
		internal KpiCollectionInternal CollectionInternal
		{
			get
			{
				return this.kpiCollectionInternal;
			}
		}

		// Token: 0x040005DE RID: 1502
		private KpiCollectionInternal kpiCollectionInternal;

		// Token: 0x020001B0 RID: 432
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600131B RID: 4891 RVA: 0x00043BD4 File Offset: 0x00041DD4
			internal Enumerator(KpiCollection kpis)
			{
				this.kpis = kpis.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600131C RID: 4892 RVA: 0x00043BE9 File Offset: 0x00041DE9
			internal Enumerator(KpiCollectionInternal kpis)
			{
				this.kpis = kpis;
				this.currentIndex = -1;
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x0600131D RID: 4893 RVA: 0x00043BFC File Offset: 0x00041DFC
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

			// Token: 0x170006AC RID: 1708
			// (get) Token: 0x0600131E RID: 4894 RVA: 0x00043C38 File Offset: 0x00041E38
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600131F RID: 4895 RVA: 0x00043C40 File Offset: 0x00041E40
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.kpis.Count;
			}

			// Token: 0x06001320 RID: 4896 RVA: 0x00043C6B File Offset: 0x00041E6B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBD RID: 3261
			private KpiCollectionInternal kpis;

			// Token: 0x04000CBE RID: 3262
			private int currentIndex;
		}
	}
}
