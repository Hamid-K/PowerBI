using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000E9 RID: 233
	public sealed class PositionCollection : ICollection, IEnumerable
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x0002F490 File Offset: 0x0002D690
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

		// Token: 0x170004D2 RID: 1234
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

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000CAF RID: 3247 RVA: 0x0002F538 File Offset: 0x0002D738
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0002F53B File Offset: 0x0002D73B
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000CB1 RID: 3249 RVA: 0x0002F548 File Offset: 0x0002D748
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

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002F55F File Offset: 0x0002D75F
		public void CopyTo(Position[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002F56C File Offset: 0x0002D76C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002F5A7 File Offset: 0x0002D7A7
		public PositionCollection.Enumerator GetEnumerator()
		{
			return new PositionCollection.Enumerator(this);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002F5AF File Offset: 0x0002D7AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400080D RID: 2061
		private DataRowCollection internalCollection;

		// Token: 0x0400080E RID: 2062
		private Set set;

		// Token: 0x0400080F RID: 2063
		private string cubeName;

		// Token: 0x04000810 RID: 2064
		private AdomdConnection connection;

		// Token: 0x020001C6 RID: 454
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001398 RID: 5016 RVA: 0x00044904 File Offset: 0x00042B04
			internal Enumerator(PositionCollection positions)
			{
				this.positions = positions;
				this.currentIndex = -1;
			}

			// Token: 0x170006D7 RID: 1751
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x00044914 File Offset: 0x00042B14
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

			// Token: 0x170006D8 RID: 1752
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x00044950 File Offset: 0x00042B50
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600139B RID: 5019 RVA: 0x00044958 File Offset: 0x00042B58
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.positions.Count;
			}

			// Token: 0x0600139C RID: 5020 RVA: 0x00044983 File Offset: 0x00042B83
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CE9 RID: 3305
			private int currentIndex;

			// Token: 0x04000CEA RID: 3306
			private PositionCollection positions;
		}
	}
}
