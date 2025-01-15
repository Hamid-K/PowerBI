using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000BF RID: 191
	public sealed class MiningModelColumnCollection : ICollection, IEnumerable
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002C227 File Offset: 0x0002A427
		internal MiningModelColumnCollection(AdomdConnection connection, MiningModel parentModel)
		{
			this.miningModelColumnCollectionInternal = new MiningModelColumnCollectionInternal(connection, parentModel);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002C23C File Offset: 0x0002A43C
		internal MiningModelColumnCollection(AdomdConnection connection, MiningModelColumn parentColumn)
		{
			this.miningModelColumnCollectionInternal = new MiningModelColumnCollectionInternal(connection, parentColumn);
		}

		// Token: 0x170003DB RID: 987
		public MiningModelColumn this[int index]
		{
			get
			{
				return this.miningModelColumnCollectionInternal[index];
			}
		}

		// Token: 0x170003DC RID: 988
		public MiningModelColumn this[string index]
		{
			get
			{
				return this.miningModelColumnCollectionInternal[index];
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002C26D File Offset: 0x0002A46D
		public MiningModelColumn Find(string index)
		{
			return this.miningModelColumnCollectionInternal.Find(index);
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0002C27B File Offset: 0x0002A47B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0002C27E File Offset: 0x0002A47E
		public object SyncRoot
		{
			get
			{
				return this.miningModelColumnCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0002C28B File Offset: 0x0002A48B
		public int Count
		{
			get
			{
				return this.miningModelColumnCollectionInternal.Count;
			}
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x0002C298 File Offset: 0x0002A498
		public void CopyTo(MiningModelColumn[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0002C2A4 File Offset: 0x0002A4A4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002C2DF File Offset: 0x0002A4DF
		public MiningModelColumnCollection.Enumerator GetEnumerator()
		{
			return new MiningModelColumnCollection.Enumerator(this);
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x0002C2E7 File Offset: 0x0002A4E7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
		internal MiningModelColumnCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningModelColumnCollectionInternal;
			}
		}

		// Token: 0x0400072B RID: 1835
		private MiningModelColumnCollectionInternal miningModelColumnCollectionInternal;

		// Token: 0x020001BA RID: 442
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001361 RID: 4961 RVA: 0x00044720 File Offset: 0x00042920
			internal Enumerator(MiningModelColumnCollection miningModelColumns)
			{
				this.miningModelColumns = miningModelColumns.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001362 RID: 4962 RVA: 0x00044735 File Offset: 0x00042935
			internal Enumerator(MiningModelColumnCollectionInternal miningModelColumns)
			{
				this.miningModelColumns = miningModelColumns;
				this.currentIndex = -1;
			}

			// Token: 0x170006C5 RID: 1733
			// (get) Token: 0x06001363 RID: 4963 RVA: 0x00044748 File Offset: 0x00042948
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

			// Token: 0x170006C6 RID: 1734
			// (get) Token: 0x06001364 RID: 4964 RVA: 0x00044784 File Offset: 0x00042984
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001365 RID: 4965 RVA: 0x0004478C File Offset: 0x0004298C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningModelColumns.Count;
			}

			// Token: 0x06001366 RID: 4966 RVA: 0x000447B7 File Offset: 0x000429B7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE2 RID: 3298
			private MiningModelColumnCollectionInternal miningModelColumns;

			// Token: 0x04000CE3 RID: 3299
			private int currentIndex;
		}
	}
}
