using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D8 RID: 216
	public sealed class MiningValueCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C0A RID: 3082 RVA: 0x0002DE6E File Offset: 0x0002C06E
		internal MiningValueCollection()
		{
			this.miningValueCollectionInternal = new MiningValueCollectionInternal();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0002DE81 File Offset: 0x0002C081
		internal MiningValueCollection(MiningModelColumn column)
		{
			this.miningValueCollectionInternal = new MiningValueCollectionInternal(column);
		}

		// Token: 0x17000487 RID: 1159
		public MiningValue this[int index]
		{
			get
			{
				return this.miningValueCollectionInternal[index];
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x0002DEA3 File Offset: 0x0002C0A3
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x0002DEA6 File Offset: 0x0002C0A6
		public object SyncRoot
		{
			get
			{
				return this.miningValueCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002DEB3 File Offset: 0x0002C0B3
		public int Count
		{
			get
			{
				return this.miningValueCollectionInternal.Count;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0002DEC0 File Offset: 0x0002C0C0
		public void CopyTo(MiningValue[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002DECC File Offset: 0x0002C0CC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002DF07 File Offset: 0x0002C107
		public MiningValueCollection.Enumerator GetEnumerator()
		{
			return new MiningValueCollection.Enumerator(this);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002DF0F File Offset: 0x0002C10F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002DF1C File Offset: 0x0002C11C
		internal MiningValueCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningValueCollectionInternal;
			}
		}

		// Token: 0x040007BD RID: 1981
		private MiningValueCollectionInternal miningValueCollectionInternal;

		// Token: 0x020001C0 RID: 448
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001378 RID: 4984 RVA: 0x000445A4 File Offset: 0x000427A4
			internal Enumerator(MiningValueCollection MiningValues)
			{
				this.miningValues = MiningValues.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001379 RID: 4985 RVA: 0x000445B9 File Offset: 0x000427B9
			internal Enumerator(MiningValueCollectionInternal miningValues)
			{
				this.miningValues = miningValues;
				this.currentIndex = -1;
			}

			// Token: 0x170006CB RID: 1739
			// (get) Token: 0x0600137A RID: 4986 RVA: 0x000445CC File Offset: 0x000427CC
			public MiningValue Current
			{
				get
				{
					MiningValue miningValue;
					try
					{
						miningValue = this.miningValues[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return miningValue;
				}
			}

			// Token: 0x170006CC RID: 1740
			// (get) Token: 0x0600137B RID: 4987 RVA: 0x00044608 File Offset: 0x00042808
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600137C RID: 4988 RVA: 0x00044610 File Offset: 0x00042810
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningValues.Count;
			}

			// Token: 0x0600137D RID: 4989 RVA: 0x0004463B File Offset: 0x0004283B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDD RID: 3293
			private MiningValueCollectionInternal miningValues;

			// Token: 0x04000CDE RID: 3294
			private int currentIndex;
		}
	}
}
