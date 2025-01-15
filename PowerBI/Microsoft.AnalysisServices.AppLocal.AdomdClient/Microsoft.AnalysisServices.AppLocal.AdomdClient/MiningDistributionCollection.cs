using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B8 RID: 184
	public sealed class MiningDistributionCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x0002B44C File Offset: 0x0002964C
		internal MiningDistributionCollection(AdomdConnection connection, MiningContentNode parentNode, DataRow[] rows)
		{
			this.miningDistributionCollectionInternal = new MiningDistributionCollectionInternal(connection, parentNode, rows);
		}

		// Token: 0x17000382 RID: 898
		public MiningDistribution this[int index]
		{
			get
			{
				return this.miningDistributionCollectionInternal[index];
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002B470 File Offset: 0x00029670
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002B473 File Offset: 0x00029673
		public object SyncRoot
		{
			get
			{
				return this.miningDistributionCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002B480 File Offset: 0x00029680
		public int Count
		{
			get
			{
				return this.miningDistributionCollectionInternal.Count;
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x0002B48D File Offset: 0x0002968D
		public void CopyTo(MiningDistribution[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x0002B498 File Offset: 0x00029698
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x0002B4D3 File Offset: 0x000296D3
		public MiningDistributionCollection.Enumerator GetEnumerator()
		{
			return new MiningDistributionCollection.Enumerator(this);
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x0002B4DB File Offset: 0x000296DB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x0002B4E8 File Offset: 0x000296E8
		internal MiningDistributionCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningDistributionCollectionInternal;
			}
		}

		// Token: 0x040006EF RID: 1775
		private MiningDistributionCollectionInternal miningDistributionCollectionInternal;

		// Token: 0x020001B8 RID: 440
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001355 RID: 4949 RVA: 0x000445E0 File Offset: 0x000427E0
			internal Enumerator(MiningDistributionCollection miningDistributions)
			{
				this.miningDistributions = miningDistributions.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001356 RID: 4950 RVA: 0x000445F5 File Offset: 0x000427F5
			internal Enumerator(MiningDistributionCollectionInternal miningDistributions)
			{
				this.miningDistributions = miningDistributions;
				this.currentIndex = -1;
			}

			// Token: 0x170006C1 RID: 1729
			// (get) Token: 0x06001357 RID: 4951 RVA: 0x00044608 File Offset: 0x00042808
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

			// Token: 0x170006C2 RID: 1730
			// (get) Token: 0x06001358 RID: 4952 RVA: 0x00044644 File Offset: 0x00042844
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x0004464C File Offset: 0x0004284C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningDistributions.Count;
			}

			// Token: 0x0600135A RID: 4954 RVA: 0x00044677 File Offset: 0x00042877
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDE RID: 3294
			private MiningDistributionCollectionInternal miningDistributions;

			// Token: 0x04000CDF RID: 3295
			private int currentIndex;
		}
	}
}
