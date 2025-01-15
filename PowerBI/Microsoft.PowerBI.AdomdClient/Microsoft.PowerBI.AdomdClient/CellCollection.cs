using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000077 RID: 119
	public sealed class CellCollection : ICollection, IEnumerable
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x000249D0 File Offset: 0x00022BD0
		internal CellCollection(CellSet cellset)
		{
			this.cellset = cellset;
			this.internalCollection = cellset.Formatter.CellTable.Rows;
			foreach (Axis axis in cellset.Axes)
			{
				this.count *= axis.Set.Tuples.Count;
			}
			this.valueColumnIndex = cellset.Formatter.CellTable.Columns.IndexOf("Value");
			this.fmtValueColumnIndex = cellset.Formatter.CellTable.Columns.IndexOf("FmtValue");
			this.ordinalColumnIndex = cellset.Formatter.CellTable.Columns.IndexOf("CellOrdinal");
			this.firstCellOrdinal = -1;
			this.lastCellOrdinal = -1;
			if (cellset.Formatter.CellTable.Rows.Count > 0)
			{
				this.firstCellOrdinal = (int)cellset.Formatter.CellTable.Rows[0][this.ordinalColumnIndex];
				this.lastCellOrdinal = (int)cellset.Formatter.CellTable.Rows[cellset.Formatter.CellTable.Rows.Count - 1][this.ordinalColumnIndex];
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00024B68 File Offset: 0x00022D68
		private DataRow GetRowByOrdinal(int index)
		{
			if (index < this.firstCellOrdinal || index > this.lastCellOrdinal)
			{
				return null;
			}
			int num = this.cellset.Formatter.CellTable.Rows.Count;
			int num2 = index - this.firstCellOrdinal;
			num2 = ((num - 1 < index) ? (num - 1) : index);
			int num3 = 0;
			if (this.previouslyIndexedCellOrdinal != -1 && this.previouslyIndexedCellRowIndex != -1)
			{
				if (index == this.previouslyIndexedCellOrdinal)
				{
					return this.cellset.Formatter.CellTable.Rows[this.previouslyIndexedCellRowIndex];
				}
				if (index > this.previouslyIndexedCellOrdinal)
				{
					num3 = this.previouslyIndexedCellRowIndex + 1;
				}
				else
				{
					num2 = Math.Min(num2, this.previouslyIndexedCellOrdinal - 1);
				}
			}
			DataRow dataRow = this.cellset.Formatter.CellTable.Rows[num3];
			int num4 = (int)dataRow[this.ordinalColumnIndex];
			if (num4 == index)
			{
				this.previouslyIndexedCellOrdinal = index;
				this.previouslyIndexedCellRowIndex = num3;
				return dataRow;
			}
			dataRow = this.cellset.Formatter.CellTable.Rows[num2];
			num4 = (int)dataRow[this.ordinalColumnIndex];
			if (num4 == index)
			{
				this.previouslyIndexedCellOrdinal = index;
				this.previouslyIndexedCellRowIndex = num2;
				return dataRow;
			}
			int i = num3 + 1;
			int num5 = num2 - 1;
			while (i <= num5)
			{
				int num6 = i + (num5 - i) / 2;
				dataRow = this.cellset.Formatter.CellTable.Rows[num6];
				num4 = (int)dataRow[this.ordinalColumnIndex];
				if (num4 == index)
				{
					this.previouslyIndexedCellOrdinal = index;
					this.previouslyIndexedCellRowIndex = num6;
					return dataRow;
				}
				if (num4 < index)
				{
					i = num6 + 1;
				}
				else
				{
					num5 = num6 - 1;
				}
			}
			return null;
		}

		// Token: 0x170001FD RID: 509
		public Cell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return new Cell(this.cellset.Formatter.CellTable, index, this.GetRowByOrdinal(index), this.cellset);
			}
		}

		// Token: 0x170001FE RID: 510
		public Cell this[int index1, int index2]
		{
			get
			{
				if (2 != this.cellset.Axes.Count)
				{
					throw new ArgumentException(SR.CellIndexer_InvalidNumberOfAxesIndexers(this.cellset.Axes.Count, 2));
				}
				int num = this.cellset.Formatter.AxesList[0][0].Rows.Count;
				int num2 = this.cellset.Formatter.AxesList[1][0].Rows.Count;
				if (index1 < 0 || index1 >= num)
				{
					throw new ArgumentOutOfRangeException("index1", index1, SR.CellIndexer_IndexOutOfRange(0, num));
				}
				if (index2 < 0 || index2 >= num2)
				{
					throw new ArgumentOutOfRangeException("index2", index2, SR.CellIndexer_IndexOutOfRange(1, num2));
				}
				int num3 = index1 + num * index2;
				return this[num3];
			}
		}

		// Token: 0x170001FF RID: 511
		public Cell this[params int[] indexes]
		{
			get
			{
				return this[indexes];
			}
		}

		// Token: 0x17000200 RID: 512
		public Cell this[ICollection indexes]
		{
			get
			{
				if (indexes.Count != this.cellset.Axes.Count)
				{
					throw new ArgumentException(SR.CellIndexer_InvalidNumberOfAxesIndexers(this.cellset.Axes.Count, indexes.Count), "indexes");
				}
				int num = 0;
				int num2 = 1;
				IEnumerator enumerator = indexes.GetEnumerator();
				for (int i = 0; i < indexes.Count; i++)
				{
					enumerator.MoveNext();
					int num3 = this.cellset.Formatter.AxesList[i][0].Rows.Count;
					if (!(enumerator.Current is int))
					{
						throw new ArgumentException(SR.CellIndexer_InvalidIndexType(i), "indexes");
					}
					int num4 = (int)enumerator.Current;
					if (num4 < 0 || num4 >= num3)
					{
						throw new ArgumentOutOfRangeException("index #" + i.ToString(CultureInfo.CurrentCulture), num4, SR.CellIndexer_IndexOutOfRange(i, num3));
					}
					num += num2 * num4;
					num2 *= num3;
				}
				return new Cell(this.cellset.Formatter.CellTable, num, this.GetRowByOrdinal(num), this.cellset);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00024F72 File Offset: 0x00023172
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00024F75 File Offset: 0x00023175
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00024F82 File Offset: 0x00023182
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00024F8A File Offset: 0x0002318A
		public void CopyTo(Cell[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00024F94 File Offset: 0x00023194
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00024FCF File Offset: 0x000231CF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024FDC File Offset: 0x000231DC
		public CellCollection.Enumerator GetEnumerator()
		{
			return new CellCollection.Enumerator(this);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00024FE4 File Offset: 0x000231E4
		internal object GetCellValue(DataRow row)
		{
			return this.GetProperty(row, this.valueColumnIndex);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x00024FF4 File Offset: 0x000231F4
		internal string GetCellFmtValue(DataRow row)
		{
			object property = this.GetProperty(row, this.fmtValueColumnIndex);
			if (property == null)
			{
				return string.Empty;
			}
			return property.ToString();
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00025020 File Offset: 0x00023220
		private object GetProperty(DataRow row, int index)
		{
			if (index < 0 || row == null)
			{
				return null;
			}
			object obj = row[index];
			if (obj is XmlaError)
			{
				throw new AdomdErrorResponseException((XmlaError)obj);
			}
			if (obj is DBNull)
			{
				return null;
			}
			return obj;
		}

		// Token: 0x04000529 RID: 1321
		private DataRowCollection internalCollection;

		// Token: 0x0400052A RID: 1322
		private CellSet cellset;

		// Token: 0x0400052B RID: 1323
		private int count = 1;

		// Token: 0x0400052C RID: 1324
		private int valueColumnIndex = -1;

		// Token: 0x0400052D RID: 1325
		private int fmtValueColumnIndex = -1;

		// Token: 0x0400052E RID: 1326
		private int ordinalColumnIndex = -1;

		// Token: 0x0400052F RID: 1327
		private int firstCellOrdinal = -1;

		// Token: 0x04000530 RID: 1328
		private int lastCellOrdinal = -1;

		// Token: 0x04000531 RID: 1329
		private int previouslyIndexedCellOrdinal = -1;

		// Token: 0x04000532 RID: 1330
		private int previouslyIndexedCellRowIndex = -1;

		// Token: 0x020001AB RID: 427
		public struct Enumerator : IEnumerator
		{
			// Token: 0x060012FF RID: 4863 RVA: 0x000437C0 File Offset: 0x000419C0
			internal Enumerator(CellCollection cells)
			{
				this.cells = cells;
				this.currentCellOrdinal = -1;
				this.currentRowIndex = -1;
				this.currentRowOrdinal = -1;
				this.currentRow = null;
				this.currentCell = null;
			}

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x06001300 RID: 4864 RVA: 0x000437EC File Offset: 0x000419EC
			public Cell Current
			{
				get
				{
					if (this.currentCell == null)
					{
						throw new InvalidOperationException();
					}
					return this.currentCell;
				}
			}

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x06001301 RID: 4865 RVA: 0x00043808 File Offset: 0x00041A08
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06001302 RID: 4866 RVA: 0x00043810 File Offset: 0x00041A10
			public bool MoveNext()
			{
				if (this.currentCellOrdinal >= this.cells.Count - 1)
				{
					this.currentCell = null;
					return false;
				}
				this.currentCellOrdinal++;
				DataRow dataRow = null;
				if (this.currentCellOrdinal >= this.cells.firstCellOrdinal && this.currentCellOrdinal <= this.cells.lastCellOrdinal)
				{
					if (this.currentRowOrdinal < this.currentCellOrdinal)
					{
						this.currentRowIndex++;
						this.currentRow = null;
						DataRowCollection rows = this.cells.cellset.Formatter.CellTable.Rows;
						if (this.currentRowIndex < rows.Count)
						{
							this.currentRow = rows[this.currentRowIndex];
						}
						this.currentRowOrdinal = (int)this.currentRow[this.cells.ordinalColumnIndex];
					}
					if (this.currentRowOrdinal == this.currentCellOrdinal)
					{
						dataRow = this.currentRow;
					}
				}
				this.currentCell = new Cell(this.cells.cellset.Formatter.CellTable, this.currentCellOrdinal, dataRow, this.cells.cellset);
				return true;
			}

			// Token: 0x06001303 RID: 4867 RVA: 0x0004393E File Offset: 0x00041B3E
			public void Reset()
			{
				this.currentCellOrdinal = -1;
				this.currentRowIndex = -1;
				this.currentRow = null;
				this.currentRowOrdinal = -1;
				this.currentRow = null;
				this.currentCell = null;
			}

			// Token: 0x04000CAF RID: 3247
			private CellCollection cells;

			// Token: 0x04000CB0 RID: 3248
			private int currentCellOrdinal;

			// Token: 0x04000CB1 RID: 3249
			private int currentRowOrdinal;

			// Token: 0x04000CB2 RID: 3250
			private int currentRowIndex;

			// Token: 0x04000CB3 RID: 3251
			private DataRow currentRow;

			// Token: 0x04000CB4 RID: 3252
			private Cell currentCell;
		}
	}
}
