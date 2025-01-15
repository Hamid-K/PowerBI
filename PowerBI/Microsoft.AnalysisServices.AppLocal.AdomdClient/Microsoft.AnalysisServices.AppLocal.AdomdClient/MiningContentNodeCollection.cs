using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B5 RID: 181
	public sealed class MiningContentNodeCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x0002AE1B File Offset: 0x0002901B
		internal MiningContentNodeCollection(AdomdConnection connection, MiningModel parentMiningModel)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentMiningModel);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002AE30 File Offset: 0x00029030
		internal MiningContentNodeCollection(AdomdConnection connection, MiningModel parentMiningModel, string nodeUniqueName)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentMiningModel, nodeUniqueName);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002AE46 File Offset: 0x00029046
		internal MiningContentNodeCollection(AdomdConnection connection, MiningContentNode parentNode, MiningNodeTreeOpType operation)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentNode, operation);
		}

		// Token: 0x1700036D RID: 877
		public MiningContentNode this[int index]
		{
			get
			{
				return this.miningContentNodeCollectionInternal[index];
			}
		}

		// Token: 0x1700036E RID: 878
		public MiningContentNode this[string index]
		{
			get
			{
				return this.miningContentNodeCollectionInternal[index];
			}
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0002AE78 File Offset: 0x00029078
		public MiningContentNode Find(string index)
		{
			return this.miningContentNodeCollectionInternal.Find(index);
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0002AE86 File Offset: 0x00029086
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x0002AE89 File Offset: 0x00029089
		public object SyncRoot
		{
			get
			{
				return this.miningContentNodeCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0002AE96 File Offset: 0x00029096
		public int Count
		{
			get
			{
				return this.miningContentNodeCollectionInternal.Count;
			}
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002AEA3 File Offset: 0x000290A3
		public void CopyTo(MiningContentNode[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002AEB0 File Offset: 0x000290B0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0002AEEB File Offset: 0x000290EB
		public MiningContentNodeCollection.Enumerator GetEnumerator()
		{
			return new MiningContentNodeCollection.Enumerator(this);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0002AEF3 File Offset: 0x000290F3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x0002AF00 File Offset: 0x00029100
		internal MiningContentNodeCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningContentNodeCollectionInternal;
			}
		}

		// Token: 0x040006D7 RID: 1751
		private MiningContentNodeCollectionInternal miningContentNodeCollectionInternal;

		// Token: 0x020001B7 RID: 439
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600134F RID: 4943 RVA: 0x00044540 File Offset: 0x00042740
			internal Enumerator(MiningContentNodeCollection miningContentNodes)
			{
				this.miningContentNodes = miningContentNodes.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001350 RID: 4944 RVA: 0x00044555 File Offset: 0x00042755
			internal Enumerator(MiningContentNodeCollectionInternal miningContentNodes)
			{
				this.miningContentNodes = miningContentNodes;
				this.currentIndex = -1;
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06001351 RID: 4945 RVA: 0x00044568 File Offset: 0x00042768
			public MiningContentNode Current
			{
				get
				{
					MiningContentNode miningContentNode;
					try
					{
						miningContentNode = this.miningContentNodes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningContentNode;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06001352 RID: 4946 RVA: 0x000445A4 File Offset: 0x000427A4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001353 RID: 4947 RVA: 0x000445AC File Offset: 0x000427AC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningContentNodes.Count;
			}

			// Token: 0x06001354 RID: 4948 RVA: 0x000445D7 File Offset: 0x000427D7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDC RID: 3292
			private MiningContentNodeCollectionInternal miningContentNodes;

			// Token: 0x04000CDD RID: 3293
			private int currentIndex;
		}
	}
}
