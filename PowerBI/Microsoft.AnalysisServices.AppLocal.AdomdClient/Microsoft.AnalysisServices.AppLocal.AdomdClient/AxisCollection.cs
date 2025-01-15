using System;
using System.Collections;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x0200006F RID: 111
	public sealed class AxisCollection : ICollection, IEnumerable
	{
		// Token: 0x06000732 RID: 1842 RVA: 0x00024408 File Offset: 0x00022608
		internal AxisCollection(AdomdConnection connection, CellSet cellset, string cubeName)
		{
			this.connection = connection;
			this.cellset = cellset;
			this.cubeName = cubeName;
			this.internalCollection = cellset.Formatter.AxesList;
		}

		// Token: 0x170001D8 RID: 472
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

		// Token: 0x170001D9 RID: 473
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

		// Token: 0x06000735 RID: 1845 RVA: 0x000244B4 File Offset: 0x000226B4
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

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0002454C File Offset: 0x0002274C
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0002454F File Offset: 0x0002274F
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0002455C File Offset: 0x0002275C
		public int Count
		{
			get
			{
				return this.internalCollection.Count;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00024569 File Offset: 0x00022769
		public void CopyTo(Axis[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00024574 File Offset: 0x00022774
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x000245AF File Offset: 0x000227AF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000245BC File Offset: 0x000227BC
		public AxisCollection.Enumerator GetEnumerator()
		{
			return new AxisCollection.Enumerator(this);
		}

		// Token: 0x0400050E RID: 1294
		private IDSFAxisCollection internalCollection;

		// Token: 0x0400050F RID: 1295
		private AdomdConnection connection;

		// Token: 0x04000510 RID: 1296
		private CellSet cellset;

		// Token: 0x04000511 RID: 1297
		private string cubeName;

		// Token: 0x020001AA RID: 426
		public struct Enumerator : IEnumerator
		{
			// Token: 0x06001307 RID: 4871 RVA: 0x00043C74 File Offset: 0x00041E74
			internal Enumerator(AxisCollection axes)
			{
				this.axes = axes;
				this.currentIndex = -1;
			}

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x06001308 RID: 4872 RVA: 0x00043C84 File Offset: 0x00041E84
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

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06001309 RID: 4873 RVA: 0x00043CC0 File Offset: 0x00041EC0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600130A RID: 4874 RVA: 0x00043CC8 File Offset: 0x00041EC8
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.axes.Count;
			}

			// Token: 0x0600130B RID: 4875 RVA: 0x00043CF3 File Offset: 0x00041EF3
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000CBE RID: 3262
			private int currentIndex;

			// Token: 0x04000CBF RID: 3263
			private AxisCollection axes;
		}
	}
}
