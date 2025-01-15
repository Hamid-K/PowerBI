using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000077 RID: 119
	public sealed class CellCollection : ICollection, IEnumerable
	{
		// Token: 0x06000786 RID: 1926 RVA: 0x00024D00 File Offset: 0x00022F00
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

		// Token: 0x06000787 RID: 1927 RVA: 0x00024E98 File Offset: 0x00023098
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

		// Token: 0x17000203 RID: 515
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

		// Token: 0x17000204 RID: 516
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

		// Token: 0x17000205 RID: 517
		public Cell this[params int[] indexes]
		{
			get
			{
				return this[indexes];
			}
		}

		// Token: 0x17000206 RID: 518
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000252A2 File Offset: 0x000234A2
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000252A5 File Offset: 0x000234A5
		public object SyncRoot
		{
			get
			{
				return this.internalCollection.SyncRoot;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000252B2 File Offset: 0x000234B2
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x000252BA File Offset: 0x000234BA
		public void CopyTo(Cell[] array, int index)
		{
			((ICollection)this).CopyTo(array, index);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x000252C4 File Offset: 0x000234C4
		void ICollection.CopyTo(Array array, int index)
		{
			AdomdUtils.CheckCopyToParameters(array, index, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				array.SetValue(this[i], index + i);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000252FF File Offset: 0x000234FF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002530C File Offset: 0x0002350C
		public CellCollection.Enumerator GetEnumerator()
		{
			return new CellCollection.Enumerator(this);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00025314 File Offset: 0x00023514
		internal object GetCellValue(DataRow row)
		{
			return this.GetProperty(row, this.valueColumnIndex);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00025324 File Offset: 0x00023524
		internal string GetCellFmtValue(DataRow row)
		{
			object property = this.GetProperty(row, this.fmtValueColumnIndex);
			if (property == null)
			{
				return string.Empty;
			}
			return property.ToString();
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00025350 File Offset: 0x00023550
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

		// Token: 0x04000536 RID: 1334
		private DataRowCollection internalCollection;

		// Token: 0x04000537 RID: 1335
		private CellSet cellset;

		// Token: 0x04000538 RID: 1336
		private int count = 1;

		// Token: 0x04000539 RID: 1337
		private int valueColumnIndex = -1;

		// Token: 0x0400053A RID: 1338
		private int fmtValueColumnIndex = -1;

		// Token: 0x0400053B RID: 1339
		private int ordinalColumnIndex = -1;

		// Token: 0x0400053C RID: 1340
		private int firstCellOrdinal = -1;

		// Token: 0x0400053D RID: 1341
		private int lastCellOrdinal = -1;

		// Token: 0x0400053E RID: 1342
		private int previouslyIndexedCellOrdinal = -1;

		// Token: 0x0400053F RID: 1343
		private int previouslyIndexedCellRowIndex = -1;

		// Token: 0x020001AB RID: 427
		public struct Enumerator : IEnumerator
		{
			// Token: 0x0600130C RID: 4876 RVA: 0x00043CFC File Offset: 0x00041EFC
			internal Enumerator(CellCollection cells)
			{
				this.cells = cells;
				this.currentCellOrdinal = -1;
				this.currentRowIndex = -1;
				this.currentRowOrdinal = -1;
				this.currentRow = null;
				this.currentCell = null;
			}

			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x0600130D RID: 4877 RVA: 0x00043D28 File Offset: 0x00041F28
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

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x0600130E RID: 4878 RVA: 0x00043D44 File Offset: 0x00041F44
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600130F RID: 4879 RVA: 0x00043D4C File Offset: 0x00041F4C
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

			// Token: 0x06001310 RID: 4880 RVA: 0x00043E7A File Offset: 0x0004207A
			public void Reset()
			{
				this.currentCellOrdinal = -1;
				this.currentRowIndex = -1;
				this.currentRow = null;
				this.currentRowOrdinal = -1;
				this.currentRow = null;
				this.currentCell = null;
			}

			// Token: 0x04000CC0 RID: 3264
			private CellCollection cells;

			// Token: 0x04000CC1 RID: 3265
			private int currentCellOrdinal;

			// Token: 0x04000CC2 RID: 3266
			private int currentRowOrdinal;

			// Token: 0x04000CC3 RID: 3267
			private int currentRowIndex;

			// Token: 0x04000CC4 RID: 3268
			private DataRow currentRow;

			// Token: 0x04000CC5 RID: 3269
			private Cell currentCell;
		}
	}
}
