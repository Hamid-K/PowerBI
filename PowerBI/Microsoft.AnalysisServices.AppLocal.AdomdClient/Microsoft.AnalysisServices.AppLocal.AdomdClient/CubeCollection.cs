using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007C RID: 124
	public sealed class CubeCollection : ICollection, IEnumerable
	{
		// Token: 0x060007BC RID: 1980 RVA: 0x000258EC File Offset: 0x00023AEC
		internal CubeCollection(AdomdConnection connection)
		{
			this.cubeCollectionInternal = new CubeCollectionInternal(connection);
		}

		// Token: 0x1700021F RID: 543
		public CubeDef this[int index]
		{
			get
			{
				return this.cubeCollectionInternal[index];
			}
		}

		// Token: 0x17000220 RID: 544
		public CubeDef this[string index]
		{
			get
			{
				return this.cubeCollectionInternal[index];
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002591C File Offset: 0x00023B1C
		public CubeDef Find(string index)
		{
			return this.cubeCollectionInternal.Find(index);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002592A File Offset: 0x00023B2A
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0002592D File Offset: 0x00023B2D
		public object SyncRoot
		{
			get
			{
				return this.cubeCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002593A File Offset: 0x00023B3A
		public int Count
		{
			get
			{
				return this.cubeCollectionInternal.Count;
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00025947 File Offset: 0x00023B47
		public void CopyTo(CubeDef[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00025954 File Offset: 0x00023B54
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002598F File Offset: 0x00023B8F
		public CubeCollection.Enumerator GetEnumerator()
		{
			return new CubeCollection.Enumerator(this);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00025997 File Offset: 0x00023B97
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x000259A4 File Offset: 0x00023BA4
		internal CubeCollectionInternal CollectionInternal
		{
			get
			{
				return this.cubeCollectionInternal;
			}
		}

		// Token: 0x04000557 RID: 1367
		private CubeCollectionInternal cubeCollectionInternal;

		// Token: 0x020001AD RID: 429
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001316 RID: 4886 RVA: 0x00043F30 File Offset: 0x00042130
			internal Enumerator(CubeCollection cubes)
			{
				this.cubes = cubes.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x06001317 RID: 4887 RVA: 0x00043F45 File Offset: 0x00042145
			internal Enumerator(CubeCollectionInternal cubes)
			{
				this.cubes = cubes;
				this.currentIndex = -1;
			}

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x06001318 RID: 4888 RVA: 0x00043F58 File Offset: 0x00042158
			public CubeDef Current
			{
				get
				{
					CubeDef cubeDef;
					try
					{
						cubeDef = this.cubes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return cubeDef;
				}
			}

			// Token: 0x170006AC RID: 1708
			// (get) Token: 0x06001319 RID: 4889 RVA: 0x00043F94 File Offset: 0x00042194
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600131A RID: 4890 RVA: 0x00043F9C File Offset: 0x0004219C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cubes.Count;
			}

			// Token: 0x0600131B RID: 4891 RVA: 0x00043FC7 File Offset: 0x000421C7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CC8 RID: 3272
			private readonly CubeCollectionInternal cubes;

			// Token: 0x04000CC9 RID: 3273
			private int currentIndex;
		}
	}
}
