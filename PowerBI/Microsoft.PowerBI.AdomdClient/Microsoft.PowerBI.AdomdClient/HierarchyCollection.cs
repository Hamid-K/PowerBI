using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000089 RID: 137
	public sealed class HierarchyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000874 RID: 2164 RVA: 0x00027517 File Offset: 0x00025717
		internal HierarchyCollection(AdomdConnection connection, Set axis, string cubeName)
		{
			this.hierarchyCollectionInternal = new HierarchyCollectionInternal(connection, axis, cubeName);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002752D File Offset: 0x0002572D
		internal HierarchyCollection(AdomdConnection connection, Dimension parentDimension, bool isAttribute)
		{
			this.hierarchyCollectionInternal = new HierarchyCollectionInternal(connection, parentDimension, isAttribute);
		}

		// Token: 0x17000281 RID: 641
		public Hierarchy this[int index]
		{
			get
			{
				return this.hierarchyCollectionInternal[index];
			}
		}

		// Token: 0x17000282 RID: 642
		public Hierarchy this[string index]
		{
			get
			{
				return this.hierarchyCollectionInternal[index];
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002755F File Offset: 0x0002575F
		public Hierarchy Find(string index)
		{
			return this.hierarchyCollectionInternal.Find(index);
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x0002756D File Offset: 0x0002576D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00027570 File Offset: 0x00025770
		public object SyncRoot
		{
			get
			{
				return this.hierarchyCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x0002757D File Offset: 0x0002577D
		public int Count
		{
			get
			{
				return this.hierarchyCollectionInternal.Count;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002758A File Offset: 0x0002578A
		public void CopyTo(Hierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00027594 File Offset: 0x00025794
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000275CF File Offset: 0x000257CF
		public HierarchyCollection.Enumerator GetEnumerator()
		{
			return new HierarchyCollection.Enumerator(this);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000275D7 File Offset: 0x000257D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x000275E4 File Offset: 0x000257E4
		internal HierarchyCollectionInternal CollectionInternal
		{
			get
			{
				return this.hierarchyCollectionInternal;
			}
		}

		// Token: 0x040005AD RID: 1453
		private HierarchyCollectionInternal hierarchyCollectionInternal;

		// Token: 0x020001AF RID: 431
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001315 RID: 4885 RVA: 0x00043B34 File Offset: 0x00041D34
			internal Enumerator(HierarchyCollection hierarchies)
			{
				this.hierarchies = hierarchies.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001316 RID: 4886 RVA: 0x00043B49 File Offset: 0x00041D49
			internal Enumerator(HierarchyCollectionInternal hierarchies)
			{
				this.hierarchies = hierarchies;
				this.currentIndex = -1;
			}

			// Token: 0x170006A9 RID: 1705
			// (get) Token: 0x06001317 RID: 4887 RVA: 0x00043B5C File Offset: 0x00041D5C
			public Hierarchy Current
			{
				get
				{
					Hierarchy hierarchy;
					try
					{
						hierarchy = this.hierarchies[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return hierarchy;
				}
			}

			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x06001318 RID: 4888 RVA: 0x00043B98 File Offset: 0x00041D98
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001319 RID: 4889 RVA: 0x00043BA0 File Offset: 0x00041DA0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.hierarchies.Count;
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x00043BCB File Offset: 0x00041DCB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBB RID: 3259
			private HierarchyCollectionInternal hierarchies;

			// Token: 0x04000CBC RID: 3260
			private int currentIndex;
		}
	}
}
