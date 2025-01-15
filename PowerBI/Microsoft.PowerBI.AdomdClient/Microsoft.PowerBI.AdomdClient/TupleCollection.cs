using System;
using System.Collections;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F2 RID: 242
	public sealed class TupleCollection : ICollection, IEnumerable
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x0002FE34 File Offset: 0x0002E034
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

		// Token: 0x1700050C RID: 1292
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

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x0002FEBB File Offset: 0x0002E0BB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0002FEBE File Offset: 0x0002E0BE
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x0002FECB File Offset: 0x0002E0CB
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

		// Token: 0x06000D2B RID: 3371 RVA: 0x0002FEE2 File Offset: 0x0002E0E2
		public void CopyTo(Tuple[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0002FEEC File Offset: 0x0002E0EC
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0002FF27 File Offset: 0x0002E127
		public TupleCollection.Enumerator GetEnumerator()
		{
			return new TupleCollection.Enumerator(this);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002FF2F File Offset: 0x0002E12F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000845 RID: 2117
		private DataRowCollection internalCollection;

		// Token: 0x04000846 RID: 2118
		private Set axis;

		// Token: 0x04000847 RID: 2119
		private string cubeName;

		// Token: 0x04000848 RID: 2120
		private AdomdConnection connection;

		// Token: 0x020001C8 RID: 456
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060013A2 RID: 5026 RVA: 0x00044A14 File Offset: 0x00042C14
			internal Enumerator(TupleCollection tuples)
			{
				this.tuples = tuples;
				this.currentIndex = -1;
			}

			// Token: 0x170006DB RID: 1755
			// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00044A24 File Offset: 0x00042C24
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

			// Token: 0x170006DC RID: 1756
			// (get) Token: 0x060013A4 RID: 5028 RVA: 0x00044A60 File Offset: 0x00042C60
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060013A5 RID: 5029 RVA: 0x00044A68 File Offset: 0x00042C68
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.tuples.Count;
			}

			// Token: 0x060013A6 RID: 5030 RVA: 0x00044A93 File Offset: 0x00042C93
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CED RID: 3309
			private int currentIndex;

			// Token: 0x04000CEE RID: 3310
			private TupleCollection tuples;
		}
	}
}
