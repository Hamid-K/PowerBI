using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BF RID: 191
	public sealed class MiningModelColumnCollection : ICollection, IEnumerable
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x0002BEF7 File Offset: 0x0002A0F7
		internal MiningModelColumnCollection(AdomdConnection connection, MiningModel parentModel)
		{
			this.miningModelColumnCollectionInternal = new MiningModelColumnCollectionInternal(connection, parentModel);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002BF0C File Offset: 0x0002A10C
		internal MiningModelColumnCollection(AdomdConnection connection, MiningModelColumn parentColumn)
		{
			this.miningModelColumnCollectionInternal = new MiningModelColumnCollectionInternal(connection, parentColumn);
		}

		// Token: 0x170003D5 RID: 981
		public MiningModelColumn this[int index]
		{
			get
			{
				return this.miningModelColumnCollectionInternal[index];
			}
		}

		// Token: 0x170003D6 RID: 982
		public MiningModelColumn this[string index]
		{
			get
			{
				return this.miningModelColumnCollectionInternal[index];
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0002BF3D File Offset: 0x0002A13D
		public MiningModelColumn Find(string index)
		{
			return this.miningModelColumnCollectionInternal.Find(index);
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002BF4B File Offset: 0x0002A14B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x0002BF4E File Offset: 0x0002A14E
		public object SyncRoot
		{
			get
			{
				return this.miningModelColumnCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0002BF5B File Offset: 0x0002A15B
		public int Count
		{
			get
			{
				return this.miningModelColumnCollectionInternal.Count;
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002BF68 File Offset: 0x0002A168
		public void CopyTo(MiningModelColumn[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002BF74 File Offset: 0x0002A174
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002BFAF File Offset: 0x0002A1AF
		public MiningModelColumnCollection.Enumerator GetEnumerator()
		{
			return new MiningModelColumnCollection.Enumerator(this);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002BFB7 File Offset: 0x0002A1B7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0002BFC4 File Offset: 0x0002A1C4
		internal MiningModelColumnCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningModelColumnCollectionInternal;
			}
		}

		// Token: 0x0400071E RID: 1822
		private MiningModelColumnCollectionInternal miningModelColumnCollectionInternal;

		// Token: 0x020001BA RID: 442
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001354 RID: 4948 RVA: 0x000441E4 File Offset: 0x000423E4
			internal Enumerator(MiningModelColumnCollection miningModelColumns)
			{
				this.miningModelColumns = miningModelColumns.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x000441F9 File Offset: 0x000423F9
			internal Enumerator(MiningModelColumnCollectionInternal miningModelColumns)
			{
				this.miningModelColumns = miningModelColumns;
				this.currentIndex = -1;
			}

			// Token: 0x170006BF RID: 1727
			// (get) Token: 0x06001356 RID: 4950 RVA: 0x0004420C File Offset: 0x0004240C
			public MiningModelColumn Current
			{
				get
				{
					MiningModelColumn miningModelColumn;
					try
					{
						miningModelColumn = this.miningModelColumns[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningModelColumn;
				}
			}

			// Token: 0x170006C0 RID: 1728
			// (get) Token: 0x06001357 RID: 4951 RVA: 0x00044248 File Offset: 0x00042448
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001358 RID: 4952 RVA: 0x00044250 File Offset: 0x00042450
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningModelColumns.Count;
			}

			// Token: 0x06001359 RID: 4953 RVA: 0x0004427B File Offset: 0x0004247B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD1 RID: 3281
			private MiningModelColumnCollectionInternal miningModelColumns;

			// Token: 0x04000CD2 RID: 3282
			private int currentIndex;
		}
	}
}
