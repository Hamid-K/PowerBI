using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F2 RID: 242
	public sealed class TupleCollection : ICollection, IEnumerable
	{
		// Token: 0x06000D33 RID: 3379 RVA: 0x00030164 File Offset: 0x0002E364
		internal TupleCollection(AdomdConnection connection, Set axis, string cubeName)
		{
			this.connection = connection;
			this.axis = axis;
			this.cubeName = cubeName;
			if (axis.AxisDataset.Count > 0)
			{
				this.internalCollection = axis.AxisDataset[0].Rows;
				return;
			}
			this.internalCollection = null;
		}

		// Token: 0x17000512 RID: 1298
		public Tuple this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return new Tuple(this.connection, this.axis, index, this.cubeName);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x000301EB File Offset: 0x0002E3EB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06000D36 RID: 3382 RVA: 0x000301EE File Offset: 0x0002E3EE
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x000301FB File Offset: 0x0002E3FB
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

		// Token: 0x06000D38 RID: 3384 RVA: 0x00030212 File Offset: 0x0002E412
		public void CopyTo(Tuple[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003021C File Offset: 0x0002E41C
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00030257 File Offset: 0x0002E457
		public TupleCollection.Enumerator GetEnumerator()
		{
			return new TupleCollection.Enumerator(this);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003025F File Offset: 0x0002E45F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000852 RID: 2130
		private DataRowCollection internalCollection;

		// Token: 0x04000853 RID: 2131
		private Set axis;

		// Token: 0x04000854 RID: 2132
		private string cubeName;

		// Token: 0x04000855 RID: 2133
		private AdomdConnection connection;

		// Token: 0x020001C8 RID: 456
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060013AF RID: 5039 RVA: 0x00044F50 File Offset: 0x00043150
			internal Enumerator(TupleCollection tuples)
			{
				this.tuples = tuples;
				this.currentIndex = -1;
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x00044F60 File Offset: 0x00043160
			public Tuple Current
			{
				get
				{
					Tuple tuple;
					try
					{
						tuple = this.tuples[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return tuple;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00044F9C File Offset: 0x0004319C
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x00044FA4 File Offset: 0x000431A4
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.tuples.Count;
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x00044FCF File Offset: 0x000431CF
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CFE RID: 3326
			private int currentIndex;

			// Token: 0x04000CFF RID: 3327
			private TupleCollection tuples;
		}
	}
}
