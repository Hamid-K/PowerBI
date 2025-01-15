using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200009D RID: 157
	public sealed class LevelCollection : ICollection, IEnumerable
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x000283B7 File Offset: 0x000265B7
		internal LevelCollection(AdomdConnection connection, Hierarchy parentHierarchy)
		{
			this.levelCollectionInternal = new LevelCollectionInternal(connection, parentHierarchy);
		}

		// Token: 0x170002E0 RID: 736
		public Level this[int index]
		{
			get
			{
				return this.levelCollectionInternal[index];
			}
		}

		// Token: 0x170002E1 RID: 737
		public Level this[string index]
		{
			get
			{
				return this.levelCollectionInternal[index];
			}
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000283E8 File Offset: 0x000265E8
		public Level Find(string index)
		{
			return this.levelCollectionInternal.Find(index);
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000283F6 File Offset: 0x000265F6
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000283F9 File Offset: 0x000265F9
		public object SyncRoot
		{
			get
			{
				return this.levelCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x00028406 File Offset: 0x00026606
		public int Count
		{
			get
			{
				return this.levelCollectionInternal.Count;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00028413 File Offset: 0x00026613
		public void CopyTo(Level[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00028420 File Offset: 0x00026620
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002845B File Offset: 0x0002665B
		public LevelCollection.Enumerator GetEnumerator()
		{
			return new LevelCollection.Enumerator(this);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00028463 File Offset: 0x00026663
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x00028470 File Offset: 0x00026670
		internal LevelCollectionInternal CollectionInternal
		{
			get
			{
				return this.levelCollectionInternal;
			}
		}

		// Token: 0x040005FB RID: 1531
		private LevelCollectionInternal levelCollectionInternal;

		// Token: 0x020001B1 RID: 433
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600132E RID: 4910 RVA: 0x000441B0 File Offset: 0x000423B0
			internal Enumerator(LevelCollection levels)
			{
				this.levels = levels.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600132F RID: 4911 RVA: 0x000441C5 File Offset: 0x000423C5
			internal Enumerator(LevelCollectionInternal levels)
			{
				this.levels = levels;
				this.currentIndex = -1;
			}

			// Token: 0x170006B3 RID: 1715
			// (get) Token: 0x06001330 RID: 4912 RVA: 0x000441D8 File Offset: 0x000423D8
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

			// Token: 0x170006B4 RID: 1716
			// (get) Token: 0x06001331 RID: 4913 RVA: 0x00044214 File Offset: 0x00042414
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001332 RID: 4914 RVA: 0x0004421C File Offset: 0x0004241C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.levels.Count;
			}

			// Token: 0x06001333 RID: 4915 RVA: 0x00044247 File Offset: 0x00042447
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD0 RID: 3280
			private LevelCollectionInternal levels;

			// Token: 0x04000CD1 RID: 3281
			private int currentIndex;
		}
	}
}
