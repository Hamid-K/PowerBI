using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000B5 RID: 181
	public sealed class MiningContentNodeCollection : ICollection, IEnumerable
	{
		// Token: 0x06000A2A RID: 2602 RVA: 0x0002AAEB File Offset: 0x00028CEB
		internal MiningContentNodeCollection(AdomdConnection connection, MiningModel parentMiningModel)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentMiningModel);
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002AB00 File Offset: 0x00028D00
		internal MiningContentNodeCollection(AdomdConnection connection, MiningModel parentMiningModel, string nodeUniqueName)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentMiningModel, nodeUniqueName);
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002AB16 File Offset: 0x00028D16
		internal MiningContentNodeCollection(AdomdConnection connection, MiningContentNode parentNode, MiningNodeTreeOpType operation)
		{
			this.miningContentNodeCollectionInternal = new MiningContentNodeCollectionInternal(connection, parentNode, operation);
		}

		// Token: 0x17000367 RID: 871
		public MiningContentNode this[int index]
		{
			get
			{
				return this.miningContentNodeCollectionInternal[index];
			}
		}

		// Token: 0x17000368 RID: 872
		public MiningContentNode this[string index]
		{
			get
			{
				return this.miningContentNodeCollectionInternal[index];
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002AB48 File Offset: 0x00028D48
		public MiningContentNode Find(string index)
		{
			return this.miningContentNodeCollectionInternal.Find(index);
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x0002AB56 File Offset: 0x00028D56
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x0002AB59 File Offset: 0x00028D59
		public object SyncRoot
		{
			get
			{
				return this.miningContentNodeCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002AB66 File Offset: 0x00028D66
		public int Count
		{
			get
			{
				return this.miningContentNodeCollectionInternal.Count;
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002AB73 File Offset: 0x00028D73
		public void CopyTo(MiningContentNode[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002AB80 File Offset: 0x00028D80
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0002ABBB File Offset: 0x00028DBB
		public MiningContentNodeCollection.Enumerator GetEnumerator()
		{
			return new MiningContentNodeCollection.Enumerator(this);
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0002ABC3 File Offset: 0x00028DC3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x0002ABD0 File Offset: 0x00028DD0
		internal MiningContentNodeCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningContentNodeCollectionInternal;
			}
		}

		// Token: 0x040006CA RID: 1738
		private MiningContentNodeCollectionInternal miningContentNodeCollectionInternal;

		// Token: 0x020001B7 RID: 439
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001342 RID: 4930 RVA: 0x00044004 File Offset: 0x00042204
			internal Enumerator(MiningContentNodeCollection miningContentNodes)
			{
				this.miningContentNodes = miningContentNodes.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001343 RID: 4931 RVA: 0x00044019 File Offset: 0x00042219
			internal Enumerator(MiningContentNodeCollectionInternal miningContentNodes)
			{
				this.miningContentNodes = miningContentNodes;
				this.currentIndex = -1;
			}

			// Token: 0x170006B9 RID: 1721
			// (get) Token: 0x06001344 RID: 4932 RVA: 0x0004402C File Offset: 0x0004222C
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

			// Token: 0x170006BA RID: 1722
			// (get) Token: 0x06001345 RID: 4933 RVA: 0x00044068 File Offset: 0x00042268
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001346 RID: 4934 RVA: 0x00044070 File Offset: 0x00042270
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningContentNodes.Count;
			}

			// Token: 0x06001347 RID: 4935 RVA: 0x0004409B File Offset: 0x0004229B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCB RID: 3275
			private MiningContentNodeCollectionInternal miningContentNodes;

			// Token: 0x04000CCC RID: 3276
			private int currentIndex;
		}
	}
}
