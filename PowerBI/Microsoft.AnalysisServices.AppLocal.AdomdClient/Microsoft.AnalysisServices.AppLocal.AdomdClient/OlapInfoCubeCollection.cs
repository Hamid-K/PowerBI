using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E3 RID: 227
	public sealed class OlapInfoCubeCollection : ICollection, IEnumerable
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x0002F0E4 File Offset: 0x0002D2E4
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

		// Token: 0x170004BF RID: 1215
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

		// Token: 0x170004C0 RID: 1216
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

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002F1BC File Offset: 0x0002D3BC
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

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0002F24C File Offset: 0x0002D44C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002F24F File Offset: 0x0002D44F
		public object SyncRoot
		{
			get
			{
				return this.cubesCach.SyncRoot;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002F25C File Offset: 0x0002D45C
		public int Count
		{
			get
			{
				return this.cubesCach.Length;
			}
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002F266 File Offset: 0x0002D466
		public void CopyTo(OlapInfoCube[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002F270 File Offset: 0x0002D470
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002F2AB File Offset: 0x0002D4AB
		public OlapInfoCubeCollection.Enumerator GetEnumerator()
		{
			return new OlapInfoCubeCollection.Enumerator(this);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002F2B3 File Offset: 0x0002D4B3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400080A RID: 2058
		private DataTable cubesTable;

		// Token: 0x0400080B RID: 2059
		private OlapInfoCube[] cubesCach;

		// Token: 0x020001C3 RID: 451
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001396 RID: 5014 RVA: 0x00044CA8 File Offset: 0x00042EA8
			internal Enumerator(OlapInfoCubeCollection cubes)
			{
				this.cubes = cubes;
				this.currentIndex = -1;
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x06001397 RID: 5015 RVA: 0x00044CB8 File Offset: 0x00042EB8
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

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x00044CF4 File Offset: 0x00042EF4
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001399 RID: 5017 RVA: 0x00044CFC File Offset: 0x00042EFC
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.cubes.Count;
			}

			// Token: 0x0600139A RID: 5018 RVA: 0x00044D27 File Offset: 0x00042F27
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CF4 RID: 3316
			private int currentIndex;

			// Token: 0x04000CF5 RID: 3317
			private OlapInfoCubeCollection cubes;
		}
	}
}
