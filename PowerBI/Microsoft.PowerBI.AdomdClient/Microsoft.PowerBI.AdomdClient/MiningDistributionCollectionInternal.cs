using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B9 RID: 185
	internal sealed class MiningDistributionCollectionInternal : ICollection, IEnumerable
	{
		// Token: 0x06000A60 RID: 2656 RVA: 0x0002B1C0 File Offset: 0x000293C0
		internal MiningDistributionCollectionInternal(AdomdConnection connection, MiningContentNode parentNode, DataRow[] rows)
		{
			this.parentNode = parentNode;
			this.internalCollection = rows;
			this.internalObjectCollection = new object[rows.Length];
		}

		// Token: 0x17000381 RID: 897
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

		// Token: 0x06000A62 RID: 2658 RVA: 0x0002B252 File Offset: 0x00029452
		public IEnumerator GetEnumerator()
		{
			return new MiningDistributionCollection.Enumerator(this);
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x0002B25F File Offset: 0x0002945F
		internal static MiningDistribution GetMiningDistributionByRow(AdomdConnection connection, DataRow row, MiningContentNode parentNode)
		{
			return new MiningDistribution(connection, row, parentNode);
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x0002B269 File Offset: 0x00029469
		public void CopyTo(Array array, int index)
		{
			this.internalCollection.CopyTo(array, index);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0002B278 File Offset: 0x00029478
		public int Count
		{
			get
			{
				return this.internalCollection.Length;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0002B282 File Offset: 0x00029482
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0002B285 File Offset: 0x00029485
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x040006E3 RID: 1763
		private object[] internalObjectCollection;

		// Token: 0x040006E4 RID: 1764
		private DataRow[] internalCollection;

		// Token: 0x040006E5 RID: 1765
		private MiningContentNode parentNode;

		// Token: 0x040006E6 RID: 1766
		private AdomdConnection connection;
	}
}
