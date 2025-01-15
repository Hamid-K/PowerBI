using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D5 RID: 213
	public sealed class MiningStructureColumnCollection : ICollection, IEnumerable
	{
		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002DE11 File Offset: 0x0002C011
		internal MiningStructureColumnCollection(AdomdConnection connection, MiningStructure parentStructure)
		{
			this.miningStructureColumnCollectionInternal = new MiningStructureColumnCollectionInternal(connection, parentStructure);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002DE26 File Offset: 0x0002C026
		internal MiningStructureColumnCollection(AdomdConnection connection, MiningStructureColumn parentColumn)
		{
			this.miningStructureColumnCollectionInternal = new MiningStructureColumnCollectionInternal(connection, parentColumn);
		}

		// Token: 0x17000482 RID: 1154
		public MiningStructureColumn this[int index]
		{
			get
			{
				return this.miningStructureColumnCollectionInternal[index];
			}
		}

		// Token: 0x17000483 RID: 1155
		public MiningStructureColumn this[string index]
		{
			get
			{
				return this.miningStructureColumnCollectionInternal[index];
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002DE57 File Offset: 0x0002C057
		public MiningStructureColumn Find(string index)
		{
			return this.miningStructureColumnCollectionInternal.Find(index);
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0002DE65 File Offset: 0x0002C065
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0002DE68 File Offset: 0x0002C068
		public object SyncRoot
		{
			get
			{
				return this.miningStructureColumnCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0002DE75 File Offset: 0x0002C075
		public int Count
		{
			get
			{
				return this.miningStructureColumnCollectionInternal.Count;
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002DE82 File Offset: 0x0002C082
		public void CopyTo(MiningStructureColumn[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0002DE8C File Offset: 0x0002C08C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0002DEC7 File Offset: 0x0002C0C7
		public MiningStructureColumnCollection.Enumerator GetEnumerator()
		{
			return new MiningStructureColumnCollection.Enumerator(this);
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0002DECF File Offset: 0x0002C0CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0002DEDC File Offset: 0x0002C0DC
		internal MiningStructureColumnCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningStructureColumnCollectionInternal;
			}
		}

		// Token: 0x040007C2 RID: 1986
		private MiningStructureColumnCollectionInternal miningStructureColumnCollectionInternal;

		// Token: 0x020001BF RID: 447
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600137F RID: 4991 RVA: 0x00044A40 File Offset: 0x00042C40
			internal Enumerator(MiningStructureColumnCollection miningStructureColumns)
			{
				this.miningStructureColumns = miningStructureColumns.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001380 RID: 4992 RVA: 0x00044A55 File Offset: 0x00042C55
			internal Enumerator(MiningStructureColumnCollectionInternal miningStructureColumns)
			{
				this.miningStructureColumns = miningStructureColumns;
				this.currentIndex = -1;
			}

			// Token: 0x170006CF RID: 1743
			// (get) Token: 0x06001381 RID: 4993 RVA: 0x00044A68 File Offset: 0x00042C68
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

			// Token: 0x170006D0 RID: 1744
			// (get) Token: 0x06001382 RID: 4994 RVA: 0x00044AA4 File Offset: 0x00042CA4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x00044AAC File Offset: 0x00042CAC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningStructureColumns.Count;
			}

			// Token: 0x06001384 RID: 4996 RVA: 0x00044AD7 File Offset: 0x00042CD7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CEC RID: 3308
			private MiningStructureColumnCollectionInternal miningStructureColumns;

			// Token: 0x04000CED RID: 3309
			private int currentIndex;
		}
	}
}
