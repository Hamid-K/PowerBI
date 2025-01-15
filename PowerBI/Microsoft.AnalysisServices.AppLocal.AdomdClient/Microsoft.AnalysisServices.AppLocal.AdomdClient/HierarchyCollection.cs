using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000089 RID: 137
	public sealed class HierarchyCollection : ICollection, IEnumerable
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00027847 File Offset: 0x00025A47
		internal HierarchyCollection(AdomdConnection connection, Set axis, string cubeName)
		{
			this.hierarchyCollectionInternal = new HierarchyCollectionInternal(connection, axis, cubeName);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0002785D File Offset: 0x00025A5D
		internal HierarchyCollection(AdomdConnection connection, Dimension parentDimension, bool isAttribute)
		{
			this.hierarchyCollectionInternal = new HierarchyCollectionInternal(connection, parentDimension, isAttribute);
		}

		// Token: 0x17000287 RID: 647
		public Hierarchy this[int index]
		{
			get
			{
				return this.hierarchyCollectionInternal[index];
			}
		}

		// Token: 0x17000288 RID: 648
		public Hierarchy this[string index]
		{
			get
			{
				return this.hierarchyCollectionInternal[index];
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002788F File Offset: 0x00025A8F
		public Hierarchy Find(string index)
		{
			return this.hierarchyCollectionInternal.Find(index);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0002789D File Offset: 0x00025A9D
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x000278A0 File Offset: 0x00025AA0
		public object SyncRoot
		{
			get
			{
				return this.hierarchyCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x000278AD File Offset: 0x00025AAD
		public int Count
		{
			get
			{
				return this.hierarchyCollectionInternal.Count;
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x000278BA File Offset: 0x00025ABA
		public void CopyTo(Hierarchy[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000278C4 File Offset: 0x00025AC4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000278FF File Offset: 0x00025AFF
		public HierarchyCollection.Enumerator GetEnumerator()
		{
			return new HierarchyCollection.Enumerator(this);
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00027907 File Offset: 0x00025B07
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00027914 File Offset: 0x00025B14
		internal HierarchyCollectionInternal CollectionInternal
		{
			get
			{
				return this.hierarchyCollectionInternal;
			}
		}

		// Token: 0x040005BA RID: 1466
		private HierarchyCollectionInternal hierarchyCollectionInternal;

		// Token: 0x020001AF RID: 431
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001322 RID: 4898 RVA: 0x00044070 File Offset: 0x00042270
			internal Enumerator(HierarchyCollection hierarchies)
			{
				this.hierarchies = hierarchies.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001323 RID: 4899 RVA: 0x00044085 File Offset: 0x00042285
			internal Enumerator(HierarchyCollectionInternal hierarchies)
			{
				this.hierarchies = hierarchies;
				this.currentIndex = -1;
			}

			// Token: 0x170006AF RID: 1711
			// (get) Token: 0x06001324 RID: 4900 RVA: 0x00044098 File Offset: 0x00042298
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

			// Token: 0x170006B0 RID: 1712
			// (get) Token: 0x06001325 RID: 4901 RVA: 0x000440D4 File Offset: 0x000422D4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001326 RID: 4902 RVA: 0x000440DC File Offset: 0x000422DC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.hierarchies.Count;
			}

			// Token: 0x06001327 RID: 4903 RVA: 0x00044107 File Offset: 0x00042307
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CCC RID: 3276
			private HierarchyCollectionInternal hierarchies;

			// Token: 0x04000CCD RID: 3277
			private int currentIndex;
		}
	}
}
