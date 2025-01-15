using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006F RID: 111
	public sealed class AxisCollection : ICollection, IEnumerable
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x000240D8 File Offset: 0x000222D8
		internal AxisCollection(AdomdConnection connection, CellSet cellset, string cubeName)
		{
			this.connection = connection;
			this.cellset = cellset;
			this.cubeName = cubeName;
			this.internalCollection = cellset.Formatter.AxesList;
		}

		// Token: 0x170001D2 RID: 466
		public Axis this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				IDSFDataSet idsfdataSet = this.internalCollection[index];
				return new Axis(this.connection, idsfdataSet, this.cubeName, this.cellset, index);
			}
		}

		// Token: 0x170001D3 RID: 467
		public Axis this[string index]
		{
			get
			{
				Axis axis = this.Find(index);
				if (null == axis)
				{
					throw new ArgumentException(SR.Indexer_ObjectNotFound(index), "index");
				}
				return axis;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00024184 File Offset: 0x00022384
		public Axis Find(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			int num = 0;
			foreach (object obj in this.internalCollection)
			{
				IDSFDataSet idsfdataSet = (IDSFDataSet)obj;
				if (idsfdataSet.DataSetName == name)
				{
					return new Axis(this.connection, idsfdataSet, this.cubeName, this.cellset, num);
				}
				num++;
			}
			return null;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000729 RID: 1833 RVA: 0x0002421C File Offset: 0x0002241C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0002421F File Offset: 0x0002241F
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x0002422C File Offset: 0x0002242C
		public int Count
		{
			get
			{
				return this.internalCollection.Count;
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00024239 File Offset: 0x00022439
		public void CopyTo(Axis[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00024244 File Offset: 0x00022444
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0002427F File Offset: 0x0002247F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0002428C File Offset: 0x0002248C
		public AxisCollection.Enumerator GetEnumerator()
		{
			return new AxisCollection.Enumerator(this);
		}

		// Token: 0x04000501 RID: 1281
		private IDSFAxisCollection internalCollection;

		// Token: 0x04000502 RID: 1282
		private AdomdConnection connection;

		// Token: 0x04000503 RID: 1283
		private CellSet cellset;

		// Token: 0x04000504 RID: 1284
		private string cubeName;

		// Token: 0x020001AA RID: 426
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012FA RID: 4858 RVA: 0x00043738 File Offset: 0x00041938
			internal Enumerator(AxisCollection axes)
			{
				this.axes = axes;
				this.currentIndex = -1;
			}

			// Token: 0x1700069F RID: 1695
			// (get) Token: 0x060012FB RID: 4859 RVA: 0x00043748 File Offset: 0x00041948
			public Axis Current
			{
				get
				{
					Axis axis;
					try
					{
						axis = this.axes[this.currentIndex];
					}
					catch (ArgumentException)
					{
						throw new InvalidOperationException();
					}
					return axis;
				}
			}

			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x060012FC RID: 4860 RVA: 0x00043784 File Offset: 0x00041984
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060012FD RID: 4861 RVA: 0x0004378C File Offset: 0x0004198C
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.axes.Count;
			}

			// Token: 0x060012FE RID: 4862 RVA: 0x000437B7 File Offset: 0x000419B7
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CAD RID: 3245
			private int currentIndex;

			// Token: 0x04000CAE RID: 3246
			private AxisCollection axes;
		}
	}
}
