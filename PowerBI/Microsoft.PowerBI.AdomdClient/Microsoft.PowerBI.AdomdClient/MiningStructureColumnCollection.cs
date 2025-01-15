using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D5 RID: 213
	public sealed class MiningStructureColumnCollection : ICollection, IEnumerable
	{
		// Token: 0x06000BEC RID: 3052 RVA: 0x0002DAE1 File Offset: 0x0002BCE1
		internal MiningStructureColumnCollection(AdomdConnection connection, MiningStructure parentStructure)
		{
			this.miningStructureColumnCollectionInternal = new MiningStructureColumnCollectionInternal(connection, parentStructure);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x0002DAF6 File Offset: 0x0002BCF6
		internal MiningStructureColumnCollection(AdomdConnection connection, MiningStructureColumn parentColumn)
		{
			this.miningStructureColumnCollectionInternal = new MiningStructureColumnCollectionInternal(connection, parentColumn);
		}

		// Token: 0x1700047C RID: 1148
		public MiningStructureColumn this[int index]
		{
			get
			{
				return this.miningStructureColumnCollectionInternal[index];
			}
		}

		// Token: 0x1700047D RID: 1149
		public MiningStructureColumn this[string index]
		{
			get
			{
				return this.miningStructureColumnCollectionInternal[index];
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002DB27 File Offset: 0x0002BD27
		public MiningStructureColumn Find(string index)
		{
			return this.miningStructureColumnCollectionInternal.Find(index);
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x0002DB35 File Offset: 0x0002BD35
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0002DB38 File Offset: 0x0002BD38
		public object SyncRoot
		{
			get
			{
				return this.miningStructureColumnCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x0002DB45 File Offset: 0x0002BD45
		public int Count
		{
			get
			{
				return this.miningStructureColumnCollectionInternal.Count;
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002DB52 File Offset: 0x0002BD52
		public void CopyTo(MiningStructureColumn[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002DB5C File Offset: 0x0002BD5C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002DB97 File Offset: 0x0002BD97
		public MiningStructureColumnCollection.Enumerator GetEnumerator()
		{
			return new MiningStructureColumnCollection.Enumerator(this);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002DB9F File Offset: 0x0002BD9F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		internal MiningStructureColumnCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningStructureColumnCollectionInternal;
			}
		}

		// Token: 0x040007B5 RID: 1973
		private MiningStructureColumnCollectionInternal miningStructureColumnCollectionInternal;

		// Token: 0x020001BF RID: 447
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001372 RID: 4978 RVA: 0x00044504 File Offset: 0x00042704
			internal Enumerator(MiningStructureColumnCollection miningStructureColumns)
			{
				this.miningStructureColumns = miningStructureColumns.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001373 RID: 4979 RVA: 0x00044519 File Offset: 0x00042719
			internal Enumerator(MiningStructureColumnCollectionInternal miningStructureColumns)
			{
				this.miningStructureColumns = miningStructureColumns;
				this.currentIndex = -1;
			}

			// Token: 0x170006C9 RID: 1737
			// (get) Token: 0x06001374 RID: 4980 RVA: 0x0004452C File Offset: 0x0004272C
			public MiningStructureColumn Current
			{
				get
				{
					MiningStructureColumn miningStructureColumn;
					try
					{
						miningStructureColumn = this.miningStructureColumns[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningStructureColumn;
				}
			}

			// Token: 0x170006CA RID: 1738
			// (get) Token: 0x06001375 RID: 4981 RVA: 0x00044568 File Offset: 0x00042768
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001376 RID: 4982 RVA: 0x00044570 File Offset: 0x00042770
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningStructureColumns.Count;
			}

			// Token: 0x06001377 RID: 4983 RVA: 0x0004459B File Offset: 0x0004279B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDB RID: 3291
			private MiningStructureColumnCollectionInternal miningStructureColumns;

			// Token: 0x04000CDC RID: 3292
			private int currentIndex;
		}
	}
}
