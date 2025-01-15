using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E9 RID: 233
	public sealed class PositionCollection : ICollection, IEnumerable
	{
		// Token: 0x06000CBA RID: 3258 RVA: 0x0002F7C0 File Offset: 0x0002D9C0
		internal PositionCollection(AdomdConnection connection, Set set, string cubeName)
		{
			this.connection = connection;
			this.set = set;
			this.cubeName = cubeName;
			if (set.AxisDataset.Count > 0)
			{
				this.internalCollection = set.AxisDataset[0].Rows;
				return;
			}
			this.internalCollection = null;
		}

		// Token: 0x170004D8 RID: 1240
		public Position this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				Tuple tuple = new Tuple(this.connection, this.set, index, this.cubeName);
				return new Position(this.connection, tuple, this.cubeName);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002F868 File Offset: 0x0002DA68
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002F86B File Offset: 0x0002DA6B
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002F878 File Offset: 0x0002DA78
		public int Count
		{
			get
			{
				if (this.internalCollection != null)
				{
					return this.internalCollection.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002F88F File Offset: 0x0002DA8F
		public void CopyTo(Position[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x0002F89C File Offset: 0x0002DA9C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0002F8D7 File Offset: 0x0002DAD7
		public PositionCollection.Enumerator GetEnumerator()
		{
			return new PositionCollection.Enumerator(this);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0002F8DF File Offset: 0x0002DADF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400081A RID: 2074
		private DataRowCollection internalCollection;

		// Token: 0x0400081B RID: 2075
		private Set set;

		// Token: 0x0400081C RID: 2076
		private string cubeName;

		// Token: 0x0400081D RID: 2077
		private AdomdConnection connection;

		// Token: 0x020001C6 RID: 454
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060013A5 RID: 5029 RVA: 0x00044E40 File Offset: 0x00043040
			internal Enumerator(PositionCollection positions)
			{
				this.positions = positions;
				this.currentIndex = -1;
			}

			// Token: 0x170006DD RID: 1757
			// (get) Token: 0x060013A6 RID: 5030 RVA: 0x00044E50 File Offset: 0x00043050
			public Position Current
			{
				get
				{
					Position position;
					try
					{
						position = this.positions[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return position;
				}
			}

			// Token: 0x170006DE RID: 1758
			// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00044E8C File Offset: 0x0004308C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013A8 RID: 5032 RVA: 0x00044E94 File Offset: 0x00043094
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.positions.Count;
			}

			// Token: 0x060013A9 RID: 5033 RVA: 0x00044EBF File Offset: 0x000430BF
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CFA RID: 3322
			private int currentIndex;

			// Token: 0x04000CFB RID: 3323
			private PositionCollection positions;
		}
	}
}
