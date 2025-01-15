using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000DC RID: 220
	public sealed class NamedSetCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C31 RID: 3121 RVA: 0x0002E26B File Offset: 0x0002C46B
		internal NamedSetCollection(AdomdConnection connection, CubeDef parentCube)
		{
			this.namedSetCollectionInternal = new NamedSetCollectionInternal(connection, parentCube);
		}

		// Token: 0x1700049D RID: 1181
		public NamedSet this[int index]
		{
			get
			{
				return this.namedSetCollectionInternal[index];
			}
		}

		// Token: 0x1700049E RID: 1182
		public NamedSet this[string index]
		{
			get
			{
				return this.namedSetCollectionInternal[index];
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0002E29C File Offset: 0x0002C49C
		public NamedSet Find(string index)
		{
			return this.namedSetCollectionInternal.Find(index);
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0002E2AA File Offset: 0x0002C4AA
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0002E2AD File Offset: 0x0002C4AD
		public object SyncRoot
		{
			get
			{
				return this.namedSetCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0002E2BA File Offset: 0x0002C4BA
		public int Count
		{
			get
			{
				return this.namedSetCollectionInternal.Count;
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x0002E2C7 File Offset: 0x0002C4C7
		public void CopyTo(NamedSet[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0002E2D4 File Offset: 0x0002C4D4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x0002E30F File Offset: 0x0002C50F
		public NamedSetCollection.Enumerator GetEnumerator()
		{
			return new NamedSetCollection.Enumerator(this);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0002E317 File Offset: 0x0002C517
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000C3C RID: 3132 RVA: 0x0002E324 File Offset: 0x0002C524
		internal NamedSetCollectionInternal CollectionInternal
		{
			get
			{
				return this.namedSetCollectionInternal;
			}
		}

		// Token: 0x040007DE RID: 2014
		private NamedSetCollectionInternal namedSetCollectionInternal;

		// Token: 0x020001C1 RID: 449
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600137E RID: 4990 RVA: 0x00044644 File Offset: 0x00042844
			internal Enumerator(NamedSetCollection namedSets)
			{
				this.namedSets = namedSets.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600137F RID: 4991 RVA: 0x00044659 File Offset: 0x00042859
			internal Enumerator(NamedSetCollectionInternal namedSets)
			{
				this.namedSets = namedSets;
				this.currentIndex = -1;
			}

			// Token: 0x170006CD RID: 1741
			// (get) Token: 0x06001380 RID: 4992 RVA: 0x0004466C File Offset: 0x0004286C
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

			// Token: 0x170006CE RID: 1742
			// (get) Token: 0x06001381 RID: 4993 RVA: 0x000446A8 File Offset: 0x000428A8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001382 RID: 4994 RVA: 0x000446B0 File Offset: 0x000428B0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.namedSets.Count;
			}

			// Token: 0x06001383 RID: 4995 RVA: 0x000446DB File Offset: 0x000428DB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CDF RID: 3295
			private NamedSetCollectionInternal namedSets;

			// Token: 0x04000CE0 RID: 3296
			private int currentIndex;
		}
	}
}
