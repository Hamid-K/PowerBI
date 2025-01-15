using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200007C RID: 124
	public sealed class CubeCollection : ICollection, IEnumerable
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x000255BC File Offset: 0x000237BC
		internal CubeCollection(AdomdConnection connection)
		{
			this.cubeCollectionInternal = new CubeCollectionInternal(connection);
		}

		// Token: 0x17000219 RID: 537
		public CubeDef this[int index]
		{
			get
			{
				return this.cubeCollectionInternal[index];
			}
		}

		// Token: 0x1700021A RID: 538
		public CubeDef this[string index]
		{
			get
			{
				return this.cubeCollectionInternal[index];
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000255EC File Offset: 0x000237EC
		public CubeDef Find(string index)
		{
			return this.cubeCollectionInternal.Find(index);
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x000255FA File Offset: 0x000237FA
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000255FD File Offset: 0x000237FD
		public object SyncRoot
		{
			get
			{
				return this.cubeCollectionInternal.SyncRoot;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0002560A File Offset: 0x0002380A
		public int Count
		{
			get
			{
				return this.cubeCollectionInternal.Count;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00025617 File Offset: 0x00023817
		public void CopyTo(CubeDef[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00025624 File Offset: 0x00023824
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002565F File Offset: 0x0002385F
		public CubeCollection.Enumerator GetEnumerator()
		{
			return new CubeCollection.Enumerator(this);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00025667 File Offset: 0x00023867
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00025674 File Offset: 0x00023874
		internal CubeCollectionInternal CollectionInternal
		{
			get
			{
				return this.cubeCollectionInternal;
			}
		}

		// Token: 0x0400054A RID: 1354
		private CubeCollectionInternal cubeCollectionInternal;

		// Token: 0x020001AD RID: 429
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001309 RID: 4873 RVA: 0x000439F4 File Offset: 0x00041BF4
			internal Enumerator(CubeCollection cubes)
			{
				this.cubes = cubes.CollectionInternal;
				this.currentIndex = -1;
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x00043A09 File Offset: 0x00041C09
			internal Enumerator(CubeCollectionInternal cubes)
			{
				this.cubes = cubes;
				this.currentIndex = -1;
			}

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x0600130B RID: 4875 RVA: 0x00043A1C File Offset: 0x00041C1C
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

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x0600130C RID: 4876 RVA: 0x00043A58 File Offset: 0x00041C58
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600130D RID: 4877 RVA: 0x00043A60 File Offset: 0x00041C60
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cubes.Count;
			}

			// Token: 0x0600130E RID: 4878 RVA: 0x00043A8B File Offset: 0x00041C8B
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CB7 RID: 3255
			private readonly CubeCollectionInternal cubes;

			// Token: 0x04000CB8 RID: 3256
			private int currentIndex;
		}
	}
}
