using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000A0 RID: 160
	public sealed class LevelPropertyCollection : ICollection, IEnumerable
	{
		// Token: 0x0600094A RID: 2378 RVA: 0x00028714 File Offset: 0x00026914
		internal LevelPropertyCollection(AdomdConnection connection, Level parentLevel)
		{
			this.levelPropertyCollectionInternal = new LevelPropertyCollectionInternal(connection, parentLevel);
		}

		// Token: 0x170002F0 RID: 752
		public LevelProperty this[int index]
		{
			get
			{
				return this.levelPropertyCollectionInternal[index];
			}
		}

		// Token: 0x170002F1 RID: 753
		public LevelProperty this[string index]
		{
			get
			{
				return this.levelPropertyCollectionInternal[index];
			}
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00028745 File Offset: 0x00026945
		public LevelProperty Find(string index)
		{
			return this.levelPropertyCollectionInternal.Find(index);
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x00028753 File Offset: 0x00026953
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x00028756 File Offset: 0x00026956
		public object SyncRoot
		{
			get
			{
				return this.levelPropertyCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x00028763 File Offset: 0x00026963
		public int Count
		{
			get
			{
				return this.levelPropertyCollectionInternal.Count;
			}
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00028770 File Offset: 0x00026970
		public void CopyTo(LevelProperty[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0002877C File Offset: 0x0002697C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x000287B7 File Offset: 0x000269B7
		public LevelPropertyCollection.Enumerator GetEnumerator()
		{
			return new LevelPropertyCollection.Enumerator(this);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000287BF File Offset: 0x000269BF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000608 RID: 1544
		private LevelPropertyCollectionInternal levelPropertyCollectionInternal;

		// Token: 0x020001B2 RID: 434
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001334 RID: 4916 RVA: 0x00044250 File Offset: 0x00042450
			internal Enumerator(LevelPropertyCollection props)
			{
				this.levelProperties = props.levelPropertyCollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001335 RID: 4917 RVA: 0x00044265 File Offset: 0x00042465
			internal Enumerator(LevelPropertyCollectionInternal levelProperties)
			{
				this.levelProperties = levelProperties;
				this.currentIndex = -1;
			}

			// Token: 0x170006B5 RID: 1717
			// (get) Token: 0x06001336 RID: 4918 RVA: 0x00044278 File Offset: 0x00042478
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

			// Token: 0x170006B6 RID: 1718
			// (get) Token: 0x06001337 RID: 4919 RVA: 0x000442B4 File Offset: 0x000424B4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001338 RID: 4920 RVA: 0x000442BC File Offset: 0x000424BC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.levelProperties.Count;
			}

			// Token: 0x06001339 RID: 4921 RVA: 0x000442E7 File Offset: 0x000424E7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CD2 RID: 3282
			private LevelPropertyCollectionInternal levelProperties;

			// Token: 0x04000CD3 RID: 3283
			private int currentIndex;
		}
	}
}
