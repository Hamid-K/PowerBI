using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009D RID: 157
	public sealed class LevelCollection : ICollection, IEnumerable
	{
		// Token: 0x0600091B RID: 2331 RVA: 0x00028087 File Offset: 0x00026287
		internal LevelCollection(AdomdConnection connection, Hierarchy parentHierarchy)
		{
			this.levelCollectionInternal = new LevelCollectionInternal(connection, parentHierarchy);
		}

		// Token: 0x170002DA RID: 730
		public Level this[int index]
		{
			get
			{
				return this.levelCollectionInternal[index];
			}
		}

		// Token: 0x170002DB RID: 731
		public Level this[string index]
		{
			get
			{
				return this.levelCollectionInternal[index];
			}
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x000280B8 File Offset: 0x000262B8
		public Level Find(string index)
		{
			return this.levelCollectionInternal.Find(index);
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000280C6 File Offset: 0x000262C6
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x000280C9 File Offset: 0x000262C9
		public object SyncRoot
		{
			get
			{
				return this.levelCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x000280D6 File Offset: 0x000262D6
		public int Count
		{
			get
			{
				return this.levelCollectionInternal.Count;
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x000280E3 File Offset: 0x000262E3
		public void CopyTo(Level[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x000280F0 File Offset: 0x000262F0
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002812B File Offset: 0x0002632B
		public LevelCollection.Enumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00028133 File Offset: 0x00026333
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x00028140 File Offset: 0x00026340
		internal LevelCollectionInternal CollectionInternal
		{
			get
			{
				return this.levelCollectionInternal;
			}
		}

		// Token: 0x040005EE RID: 1518
		private LevelCollectionInternal levelCollectionInternal;

		// Token: 0x020001B1 RID: 433
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001321 RID: 4897 RVA: 0x00043C74 File Offset: 0x00041E74
			internal Enumerator(LevelCollection levels)
			{
				this.levels = levels.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001322 RID: 4898 RVA: 0x00043C89 File Offset: 0x00041E89
			internal Enumerator(LevelCollectionInternal levels)
			{
				this.levels = levels;
				this.currentIndex = -1;
			}

			// Token: 0x170006AD RID: 1709
			// (get) Token: 0x06001323 RID: 4899 RVA: 0x00043C9C File Offset: 0x00041E9C
			public Level Current
			{
				get
				{
					Level level;
					try
					{
						level = this.levels[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return level;
				}
			}

			// Token: 0x170006AE RID: 1710
			// (get) Token: 0x06001324 RID: 4900 RVA: 0x00043CD8 File Offset: 0x00041ED8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001325 RID: 4901 RVA: 0x00043CE0 File Offset: 0x00041EE0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.levels.Count;
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x00043D0B File Offset: 0x00041F0B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBF RID: 3263
			private LevelCollectionInternal levels;

			// Token: 0x04000CC0 RID: 3264
			private int currentIndex;
		}
	}
}
