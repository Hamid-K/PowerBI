using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D2 RID: 210
	public sealed class MiningStructureCollection : ICollection, IEnumerable
	{
		// Token: 0x06000BBB RID: 3003 RVA: 0x0002D82D File Offset: 0x0002BA2D
		internal MiningStructureCollection(AdomdConnection connection)
		{
			this.miningStructureCollectionInternal = new MiningStructureCollectionInternal(connection);
		}

		// Token: 0x1700045A RID: 1114
		public MiningStructure this[int index]
		{
			get
			{
				return this.miningStructureCollectionInternal[index];
			}
		}

		// Token: 0x1700045B RID: 1115
		public MiningStructure this[string index]
		{
			get
			{
				return this.miningStructureCollectionInternal[index];
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0002D85D File Offset: 0x0002BA5D
		public MiningStructure Find(string index)
		{
			return this.miningStructureCollectionInternal.Find(index);
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0002D86B File Offset: 0x0002BA6B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0002D86E File Offset: 0x0002BA6E
		public object SyncRoot
		{
			get
			{
				return this.miningStructureCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0002D87B File Offset: 0x0002BA7B
		public int Count
		{
			get
			{
				return this.miningStructureCollectionInternal.Count;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0002D888 File Offset: 0x0002BA88
		public void CopyTo(MiningStructure[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0002D894 File Offset: 0x0002BA94
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0002D8CF File Offset: 0x0002BACF
		public MiningStructureCollection.Enumerator GetEnumerator()
		{
			return new MiningStructureCollection.Enumerator(this);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0002D8D7 File Offset: 0x0002BAD7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		internal MiningStructureCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningStructureCollectionInternal;
			}
		}

		// Token: 0x040007B0 RID: 1968
		private MiningStructureCollectionInternal miningStructureCollectionInternal;

		// Token: 0x020001BE RID: 446
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001379 RID: 4985 RVA: 0x000449A0 File Offset: 0x00042BA0
			internal Enumerator(MiningStructureCollection miningStructures)
			{
				this.miningStructures = miningStructures.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600137A RID: 4986 RVA: 0x000449B5 File Offset: 0x00042BB5
			internal Enumerator(MiningStructureCollectionInternal miningStructures)
			{
				this.miningStructures = miningStructures;
				this.currentIndex = -1;
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x0600137B RID: 4987 RVA: 0x000449C8 File Offset: 0x00042BC8
			public MiningStructure Current
			{
				get
				{
					MiningStructure miningStructure;
					try
					{
						miningStructure = this.miningStructures[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningStructure;
				}
			}

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x0600137C RID: 4988 RVA: 0x00044A04 File Offset: 0x00042C04
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600137D RID: 4989 RVA: 0x00044A0C File Offset: 0x00042C0C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningStructures.Count;
			}

			// Token: 0x0600137E RID: 4990 RVA: 0x00044A37 File Offset: 0x00042C37
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CEA RID: 3306
			private MiningStructureCollectionInternal miningStructures;

			// Token: 0x04000CEB RID: 3307
			private int currentIndex;
		}
	}
}
