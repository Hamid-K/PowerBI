using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A0 RID: 160
	public sealed class LevelPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x0600093D RID: 2365 RVA: 0x000283E4 File Offset: 0x000265E4
		internal LevelPropertyCollection(AdomdConnection connection, Level parentLevel)
		{
			this.levelPropertyCollectionInternal = new LevelPropertyCollectionInternal(connection, parentLevel);
		}

		// Token: 0x170002EA RID: 746
		public LevelProperty this[int index]
		{
			get
			{
				return this.levelPropertyCollectionInternal[index];
			}
		}

		// Token: 0x170002EB RID: 747
		public LevelProperty this[string index]
		{
			get
			{
				return this.levelPropertyCollectionInternal[index];
			}
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00028415 File Offset: 0x00026615
		public LevelProperty Find(string index)
		{
			return this.levelPropertyCollectionInternal.Find(index);
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x00028423 File Offset: 0x00026623
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x00028426 File Offset: 0x00026626
		public object SyncRoot
		{
			get
			{
				return this.levelPropertyCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00028433 File Offset: 0x00026633
		public int Count
		{
			get
			{
				return this.levelPropertyCollectionInternal.Count;
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00028440 File Offset: 0x00026640
		public void CopyTo(LevelProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x0002844C File Offset: 0x0002664C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00028487 File Offset: 0x00026687
		public LevelPropertyCollection.Enumerator GetEnumerator()
		{
			return new LevelPropertyCollection.Enumerator(this);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002848F File Offset: 0x0002668F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040005FB RID: 1531
		private LevelPropertyCollectionInternal levelPropertyCollectionInternal;

		// Token: 0x020001B2 RID: 434
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001327 RID: 4903 RVA: 0x00043D14 File Offset: 0x00041F14
			internal Enumerator(LevelPropertyCollection props)
			{
				this.levelProperties = props.levelPropertyCollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001328 RID: 4904 RVA: 0x00043D29 File Offset: 0x00041F29
			internal Enumerator(LevelPropertyCollectionInternal levelProperties)
			{
				this.levelProperties = levelProperties;
				this.currentIndex = -1;
			}

			// Token: 0x170006AF RID: 1711
			// (get) Token: 0x06001329 RID: 4905 RVA: 0x00043D3C File Offset: 0x00041F3C
			public LevelProperty Current
			{
				get
				{
					LevelProperty levelProperty;
					try
					{
						levelProperty = this.levelProperties[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return levelProperty;
				}
			}

			// Token: 0x170006B0 RID: 1712
			// (get) Token: 0x0600132A RID: 4906 RVA: 0x00043D78 File Offset: 0x00041F78
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600132B RID: 4907 RVA: 0x00043D80 File Offset: 0x00041F80
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.levelProperties.Count;
			}

			// Token: 0x0600132C RID: 4908 RVA: 0x00043DAB File Offset: 0x00041FAB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC1 RID: 3265
			private LevelPropertyCollectionInternal levelProperties;

			// Token: 0x04000CC2 RID: 3266
			private int currentIndex;
		}
	}
}
