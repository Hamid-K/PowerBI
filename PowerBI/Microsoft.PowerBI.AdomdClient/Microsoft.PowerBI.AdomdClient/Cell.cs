using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000076 RID: 118
	public sealed class Cell : ISubordinateObject
	{
		// Token: 0x0600076D RID: 1901 RVA: 0x000248E1 File Offset: 0x00022AE1
		internal Cell(DataTable cellsTable, int cellOrdinal, DataRow cellRow, CellSet cellSet)
		{
			this.cellsTable = cellsTable;
			this.cellOrdinal = cellOrdinal;
			this.cellRow = cellRow;
			this.cellSet = cellSet;
			this.cellProps = null;
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0002490D File Offset: 0x00022B0D
		internal int Ordinal
		{
			get
			{
				return this.cellOrdinal;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x00024915 File Offset: 0x00022B15
		public object Value
		{
			get
			{
				return this.cellSet.Cells.GetCellValue(this.cellRow);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0002492D File Offset: 0x00022B2D
		public string FormattedValue
		{
			get
			{
				return this.cellSet.Cells.GetCellFmtValue(this.cellRow);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x00024945 File Offset: 0x00022B45
		public CellPropertyCollection CellProperties
		{
			get
			{
				if (this.cellProps == null)
				{
					this.cellProps = new CellPropertyCollection(this.cellsTable, this.cellRow, this);
				}
				return this.cellProps;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0002496D File Offset: 0x00022B6D
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cellSet;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00024975 File Offset: 0x00022B75
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.Ordinal;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0002497D File Offset: 0x00022B7D
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Cell);
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00024989 File Offset: 0x00022B89
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000249AC File Offset: 0x00022BAC
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x000249BA File Offset: 0x00022BBA
		public static bool operator ==(Cell o1, Cell o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000249C3 File Offset: 0x00022BC3
		public static bool operator !=(Cell o1, Cell o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000522 RID: 1314
		private DataTable cellsTable;

		// Token: 0x04000523 RID: 1315
		private int cellOrdinal;

		// Token: 0x04000524 RID: 1316
		private CellPropertyCollection cellProps;

		// Token: 0x04000525 RID: 1317
		private DataRow cellRow;

		// Token: 0x04000526 RID: 1318
		private CellSet cellSet;

		// Token: 0x04000527 RID: 1319
		private int hashCode;

		// Token: 0x04000528 RID: 1320
		private bool hashCodeCalculated;
	}
}
