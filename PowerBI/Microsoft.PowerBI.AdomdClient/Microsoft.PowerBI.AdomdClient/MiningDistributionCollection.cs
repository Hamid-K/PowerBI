using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B8 RID: 184
	public sealed class MiningDistributionCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A56 RID: 2646 RVA: 0x0002B11C File Offset: 0x0002931C
		internal MiningDistributionCollection(AdomdConnection connection, MiningContentNode parentNode, DataRow[] rows)
		{
			this.miningDistributionCollectionInternal = new MiningDistributionCollectionInternal(connection, parentNode, rows);
		}

		// Token: 0x1700037C RID: 892
		public MiningDistribution this[int index]
		{
			get
			{
				return this.miningDistributionCollectionInternal[index];
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x0002B140 File Offset: 0x00029340
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x0002B143 File Offset: 0x00029343
		public object SyncRoot
		{
			get
			{
				return this.miningDistributionCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x0002B150 File Offset: 0x00029350
		public int Count
		{
			get
			{
				return this.miningDistributionCollectionInternal.Count;
			}
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0002B15D File Offset: 0x0002935D
		public void CopyTo(MiningDistribution[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0002B168 File Offset: 0x00029368
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0002B1A3 File Offset: 0x000293A3
		public MiningDistributionCollection.Enumerator GetEnumerator()
		{
			return new MiningDistributionCollection.Enumerator(this);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0002B1AB File Offset: 0x000293AB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0002B1B8 File Offset: 0x000293B8
		internal MiningDistributionCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningDistributionCollectionInternal;
			}
		}

		// Token: 0x040006E2 RID: 1762
		private MiningDistributionCollectionInternal miningDistributionCollectionInternal;

		// Token: 0x020001B8 RID: 440
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001348 RID: 4936 RVA: 0x000440A4 File Offset: 0x000422A4
			internal Enumerator(MiningDistributionCollection miningDistributions)
			{
				this.miningDistributions = miningDistributions.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001349 RID: 4937 RVA: 0x000440B9 File Offset: 0x000422B9
			internal Enumerator(MiningDistributionCollectionInternal miningDistributions)
			{
				this.miningDistributions = miningDistributions;
				this.currentIndex = -1;
			}

			// Token: 0x170006BB RID: 1723
			// (get) Token: 0x0600134A RID: 4938 RVA: 0x000440CC File Offset: 0x000422CC
			public MiningDistribution Current
			{
				get
				{
					MiningDistribution miningDistribution;
					try
					{
						miningDistribution = this.miningDistributions[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningDistribution;
				}
			}

			// Token: 0x170006BC RID: 1724
			// (get) Token: 0x0600134B RID: 4939 RVA: 0x00044108 File Offset: 0x00042308
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600134C RID: 4940 RVA: 0x00044110 File Offset: 0x00042310
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningDistributions.Count;
			}

			// Token: 0x0600134D RID: 4941 RVA: 0x0004413B File Offset: 0x0004233B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCD RID: 3277
			private MiningDistributionCollectionInternal miningDistributions;

			// Token: 0x04000CCE RID: 3278
			private int currentIndex;
		}
	}
}
