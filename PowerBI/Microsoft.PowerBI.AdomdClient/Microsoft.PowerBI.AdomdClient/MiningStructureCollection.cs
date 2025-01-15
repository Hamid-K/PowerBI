using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D2 RID: 210
	public sealed class MiningStructureCollection : ICollection, IEnumerable
	{
		// Token: 0x06000BAE RID: 2990 RVA: 0x0002D4FD File Offset: 0x0002B6FD
		internal MiningStructureCollection(AdomdConnection connection)
		{
			this.miningStructureCollectionInternal = new MiningStructureCollectionInternal(connection);
		}

		// Token: 0x17000454 RID: 1108
		public MiningStructure this[int index]
		{
			get
			{
				return this.miningStructureCollectionInternal[index];
			}
		}

		// Token: 0x17000455 RID: 1109
		public MiningStructure this[string index]
		{
			get
			{
				return this.miningStructureCollectionInternal[index];
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002D52D File Offset: 0x0002B72D
		public MiningStructure Find(string index)
		{
			return this.miningStructureCollectionInternal.Find(index);
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0002D53B File Offset: 0x0002B73B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0002D53E File Offset: 0x0002B73E
		public object SyncRoot
		{
			get
			{
				return this.miningStructureCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0002D54B File Offset: 0x0002B74B
		public int Count
		{
			get
			{
				return this.miningStructureCollectionInternal.Count;
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0002D558 File Offset: 0x0002B758
		public void CopyTo(MiningStructure[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002D564 File Offset: 0x0002B764
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002D59F File Offset: 0x0002B79F
		public MiningStructureCollection.Enumerator GetEnumerator()
		{
			return new MiningStructureCollection.Enumerator(this);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002D5A7 File Offset: 0x0002B7A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		internal MiningStructureCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningStructureCollectionInternal;
			}
		}

		// Token: 0x040007A3 RID: 1955
		private MiningStructureCollectionInternal miningStructureCollectionInternal;

		// Token: 0x020001BE RID: 446
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600136C RID: 4972 RVA: 0x00044464 File Offset: 0x00042664
			internal Enumerator(MiningStructureCollection miningStructures)
			{
				this.miningStructures = miningStructures.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600136D RID: 4973 RVA: 0x00044479 File Offset: 0x00042679
			internal Enumerator(MiningStructureCollectionInternal miningStructures)
			{
				this.miningStructures = miningStructures;
				this.currentIndex = -1;
			}

			// Token: 0x170006C7 RID: 1735
			// (get) Token: 0x0600136E RID: 4974 RVA: 0x0004448C File Offset: 0x0004268C
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

			// Token: 0x170006C8 RID: 1736
			// (get) Token: 0x0600136F RID: 4975 RVA: 0x000444C8 File Offset: 0x000426C8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001370 RID: 4976 RVA: 0x000444D0 File Offset: 0x000426D0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningStructures.Count;
			}

			// Token: 0x06001371 RID: 4977 RVA: 0x000444FB File Offset: 0x000426FB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD9 RID: 3289
			private MiningStructureCollectionInternal miningStructures;

			// Token: 0x04000CDA RID: 3290
			private int currentIndex;
		}
	}
}
