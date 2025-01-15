using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D8 RID: 216
	public sealed class MiningValueCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x0002E19E File Offset: 0x0002C39E
		internal MiningValueCollection()
		{
			this.miningValueCollectionInternal = new MiningValueCollectionInternal();
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0002E1B1 File Offset: 0x0002C3B1
		internal MiningValueCollection(MiningModelColumn column)
		{
			this.miningValueCollectionInternal = new MiningValueCollectionInternal(column);
		}

		// Token: 0x1700048D RID: 1165
		public MiningValue this[int index]
		{
			get
			{
				return this.miningValueCollectionInternal[index];
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0002E1D3 File Offset: 0x0002C3D3
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0002E1D6 File Offset: 0x0002C3D6
		public object SyncRoot
		{
			get
			{
				return this.miningValueCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0002E1E3 File Offset: 0x0002C3E3
		public int Count
		{
			get
			{
				return this.miningValueCollectionInternal.Count;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0002E1F0 File Offset: 0x0002C3F0
		public void CopyTo(MiningValue[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0002E237 File Offset: 0x0002C437
		public MiningValueCollection.Enumerator GetEnumerator()
		{
			return new MiningValueCollection.Enumerator(this);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0002E23F File Offset: 0x0002C43F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0002E24C File Offset: 0x0002C44C
		internal MiningValueCollectionInternal CollectionInternal
		{
			get
			{
				return this.miningValueCollectionInternal;
			}
		}

		// Token: 0x040007CA RID: 1994
		private MiningValueCollectionInternal miningValueCollectionInternal;

		// Token: 0x020001C0 RID: 448
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001385 RID: 4997 RVA: 0x00044AE0 File Offset: 0x00042CE0
			internal Enumerator(MiningValueCollection MiningValues)
			{
				this.miningValues = MiningValues.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001386 RID: 4998 RVA: 0x00044AF5 File Offset: 0x00042CF5
			internal Enumerator(MiningValueCollectionInternal miningValues)
			{
				this.miningValues = miningValues;
				this.currentIndex = -1;
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x06001387 RID: 4999 RVA: 0x00044B08 File Offset: 0x00042D08
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

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x06001388 RID: 5000 RVA: 0x00044B44 File Offset: 0x00042D44
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001389 RID: 5001 RVA: 0x00044B4C File Offset: 0x00042D4C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.miningValues.Count;
			}

			// Token: 0x0600138A RID: 5002 RVA: 0x00044B77 File Offset: 0x00042D77
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CEE RID: 3310
			private MiningValueCollectionInternal miningValues;

			// Token: 0x04000CEF RID: 3311
			private int currentIndex;
		}
	}
}
