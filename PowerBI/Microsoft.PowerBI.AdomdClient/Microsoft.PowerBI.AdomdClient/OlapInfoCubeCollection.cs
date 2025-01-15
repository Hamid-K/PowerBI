using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E3 RID: 227
	public sealed class OlapInfoCubeCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C7A RID: 3194 RVA: 0x0002EDB4 File Offset: 0x0002CFB4
		internal OlapInfoCubeCollection(MDDatasetFormatter formatter)
		{
			this.cubesTable = formatter.CubesInfos;
			int num = 0;
			if (this.cubesTable != null)
			{
				num = this.cubesTable.Rows.Count;
			}
			this.cubesCach = new OlapInfoCube[num];
			for (int i = 0; i < this.cubesCach.Length; i++)
			{
				this.cubesCach[i] = null;
			}
		}

		// Token: 0x170004B9 RID: 1209
		public OlapInfoCube this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				if (this.cubesCach[index] == null)
				{
					this.cubesCach[index] = new OlapInfoCube(this.cubesTable.Rows[index]);
				}
				return this.cubesCach[index];
			}
		}

		// Token: 0x170004BA RID: 1210
		public OlapInfoCube this[string name]
		{
			get
			{
				OlapInfoCube olapInfoCube = this.Find(name);
				if (olapInfoCube == null)
				{
					throw new ArgumentException(SR.ICollection_ItemWithThisNameDoesNotExistInTheCollection, "name");
				}
				return olapInfoCube;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002EE8C File Offset: 0x0002D08C
		public OlapInfoCube Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = -1;
			for (int i = 0; i < this.Count; i++)
			{
				if ((string)this.cubesTable.Rows[i]["CubeName"] == name)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				return null;
			}
			if (this.cubesCach[num] == null)
			{
				this.cubesCach[num] = new OlapInfoCube(this.cubesTable.Rows[num]);
			}
			return this.cubesCach[num];
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0002EF1C File Offset: 0x0002D11C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0002EF1F File Offset: 0x0002D11F
		public object SyncRoot
		{
			get
			{
				return this.cubesCach.SyncRoot;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0002EF2C File Offset: 0x0002D12C
		public int Count
		{
			get
			{
				return this.cubesCach.Length;
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002EF36 File Offset: 0x0002D136
		public void CopyTo(OlapInfoCube[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002EF40 File Offset: 0x0002D140
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002EF7B File Offset: 0x0002D17B
		public OlapInfoCubeCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoCubeCollection.Enumerator(this);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002EF83 File Offset: 0x0002D183
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040007FD RID: 2045
		private DataTable cubesTable;

		// Token: 0x040007FE RID: 2046
		private OlapInfoCube[] cubesCach;

		// Token: 0x020001C3 RID: 451
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001389 RID: 5001 RVA: 0x0004476C File Offset: 0x0004296C
			internal Enumerator(OlapInfoCubeCollection cubes)
			{
				this.cubes = cubes;
				this.currentIndex = -1;
			}

			// Token: 0x170006D1 RID: 1745
			// (get) Token: 0x0600138A RID: 5002 RVA: 0x0004477C File Offset: 0x0004297C
			public OlapInfoCube Current
			{
				get
				{
					OlapInfoCube olapInfoCube;
					try
					{
						olapInfoCube = this.cubes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return olapInfoCube;
				}
			}

			// Token: 0x170006D2 RID: 1746
			// (get) Token: 0x0600138B RID: 5003 RVA: 0x000447B8 File Offset: 0x000429B8
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600138C RID: 5004 RVA: 0x000447C0 File Offset: 0x000429C0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cubes.Count;
			}

			// Token: 0x0600138D RID: 5005 RVA: 0x000447EB File Offset: 0x000429EB
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE3 RID: 3299
			private int currentIndex;

			// Token: 0x04000CE4 RID: 3300
			private OlapInfoCubeCollection cubes;
		}
	}
}
