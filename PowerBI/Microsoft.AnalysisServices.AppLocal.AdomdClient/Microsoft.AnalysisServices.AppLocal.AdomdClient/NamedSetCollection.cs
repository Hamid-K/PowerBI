using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DC RID: 220
	public sealed class NamedSetCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x0002E59B File Offset: 0x0002C79B
		internal NamedSetCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.namedSetCollectionInternal = new NamedSetCollectionInternal(connection, parentCube);
		}

		// Token: 0x170004A3 RID: 1187
		public NamedSet this[int index]
		{
			get
			{
				return this.namedSetCollectionInternal[index];
			}
		}

		// Token: 0x170004A4 RID: 1188
		public NamedSet this[string index]
		{
			get
			{
				return this.namedSetCollectionInternal[index];
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0002E5CC File Offset: 0x0002C7CC
		public NamedSet Find(string index)
		{
			return this.namedSetCollectionInternal.Find(index);
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000C42 RID: 3138 RVA: 0x0002E5DA File Offset: 0x0002C7DA
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0002E5DD File Offset: 0x0002C7DD
		public object SyncRoot
		{
			get
			{
				return this.namedSetCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0002E5EA File Offset: 0x0002C7EA
		public int Count
		{
			get
			{
				return this.namedSetCollectionInternal.Count;
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0002E5F7 File Offset: 0x0002C7F7
		public void CopyTo(NamedSet[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002E604 File Offset: 0x0002C804
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002E63F File Offset: 0x0002C83F
		public NamedSetCollection.Enumerator GetEnumerator()
		{
			return new NamedSetCollection.Enumerator(this);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002E647 File Offset: 0x0002C847
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002E654 File Offset: 0x0002C854
		internal NamedSetCollectionInternal CollectionInternal
		{
			get
			{
				return this.namedSetCollectionInternal;
			}
		}

		// Token: 0x040007EB RID: 2027
		private NamedSetCollectionInternal namedSetCollectionInternal;

		// Token: 0x020001C1 RID: 449
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600138B RID: 5003 RVA: 0x00044B80 File Offset: 0x00042D80
			internal Enumerator(NamedSetCollection namedSets)
			{
				this.namedSets = namedSets.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600138C RID: 5004 RVA: 0x00044B95 File Offset: 0x00042D95
			internal Enumerator(NamedSetCollectionInternal namedSets)
			{
				this.namedSets = namedSets;
				this.currentIndex = -1;
			}

			// Token: 0x170006D3 RID: 1747
			// (get) Token: 0x0600138D RID: 5005 RVA: 0x00044BA8 File Offset: 0x00042DA8
			public NamedSet Current
			{
				get
				{
					NamedSet namedSet;
					try
					{
						namedSet = this.namedSets[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return namedSet;
				}
			}

			// Token: 0x170006D4 RID: 1748
			// (get) Token: 0x0600138E RID: 5006 RVA: 0x00044BE4 File Offset: 0x00042DE4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600138F RID: 5007 RVA: 0x00044BEC File Offset: 0x00042DEC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.namedSets.Count;
			}

			// Token: 0x06001390 RID: 5008 RVA: 0x00044C17 File Offset: 0x00042E17
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CF0 RID: 3312
			private NamedSetCollectionInternal namedSets;

			// Token: 0x04000CF1 RID: 3313
			private int currentIndex;
		}
	}
}
