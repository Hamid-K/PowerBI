using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B9 RID: 185
	internal sealed class MiningDistributionCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x0002B4F0 File Offset: 0x000296F0
		internal MiningDistributionCollectionInternal(AdomdConnection connection, MiningContentNode parentNode, DataRow[] rows)
		{
			this.parentNode = parentNode;
			this.internalCollection = rows;
			this.internalObjectCollection = new object[rows.Length];
		}

		// Token: 0x17000387 RID: 903
		public MiningDistribution this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				DataRow dataRow = this.internalCollection[index];
				MiningDistribution miningDistribution;
				if (this.internalObjectCollection[index] == null)
				{
					miningDistribution = MiningDistributionCollectionInternal.GetMiningDistributionByRow(this.connection, dataRow, this.parentNode);
					this.internalObjectCollection[index] = miningDistribution;
					miningDistribution.ordinal = index;
				}
				else
				{
					miningDistribution = (MiningDistribution)this.internalObjectCollection[index];
				}
				return miningDistribution;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x0002B582 File Offset: 0x00029782
		public IEnumerator GetEnumerator()
		{
			return new MiningDistributionCollection.Enumerator(this);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x0002B58F File Offset: 0x0002978F
		internal static MiningDistribution GetMiningDistributionByRow(AdomdConnection connection, DataRow row, MiningContentNode parentNode)
		{
			return new MiningDistribution(connection, row, parentNode);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002B599 File Offset: 0x00029799
		public void CopyTo(Array array, int index)
		{
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0002B5A8 File Offset: 0x000297A8
		public int Count
		{
			get
			{
				return this.internalCollection.Length;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x0002B5B2 File Offset: 0x000297B2
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0002B5B5 File Offset: 0x000297B5
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x040006F0 RID: 1776
		private object[] internalObjectCollection;

		// Token: 0x040006F1 RID: 1777
		private DataRow[] internalCollection;

		// Token: 0x040006F2 RID: 1778
		private MiningContentNode parentNode;

		// Token: 0x040006F3 RID: 1779
		private AdomdConnection connection;
	}
}
