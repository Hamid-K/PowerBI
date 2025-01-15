using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000076 RID: 118
	public sealed class Cell : ISubordinateObject
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x00024C11 File Offset: 0x00022E11
		internal Cell(DataTable cellsTable, int cellOrdinal, DataRow cellRow, CellSet cellSet)
		{
			this.cellsTable = cellsTable;
			this.cellOrdinal = cellOrdinal;
			this.cellRow = cellRow;
			this.cellSet = cellSet;
			this.cellProps = null;
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00024C3D File Offset: 0x00022E3D
		internal int Ordinal
		{
			get
			{
				return this.cellOrdinal;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00024C45 File Offset: 0x00022E45
		public object Value
		{
			get
			{
				return this.cellSet.Cells.GetCellValue(this.cellRow);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600077D RID: 1917 RVA: 0x00024C5D File Offset: 0x00022E5D
		public string FormattedValue
		{
			get
			{
				return this.cellSet.Cells.GetCellFmtValue(this.cellRow);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x00024C75 File Offset: 0x00022E75
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x00024C9D File Offset: 0x00022E9D
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cellSet;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00024CA5 File Offset: 0x00022EA5
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.Ordinal;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000781 RID: 1921 RVA: 0x00024CAD File Offset: 0x00022EAD
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(Cell);
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00024CB9 File Offset: 0x00022EB9
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00024CDC File Offset: 0x00022EDC
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00024CEA File Offset: 0x00022EEA
		public static bool operator ==(Cell o1, Cell o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024CF3 File Offset: 0x00022EF3
		public static bool operator !=(Cell o1, Cell o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x0400052F RID: 1327
		private DataTable cellsTable;

		// Token: 0x04000530 RID: 1328
		private int cellOrdinal;

		// Token: 0x04000531 RID: 1329
		private CellPropertyCollection cellProps;

		// Token: 0x04000532 RID: 1330
		private DataRow cellRow;

		// Token: 0x04000533 RID: 1331
		private CellSet cellSet;

		// Token: 0x04000534 RID: 1332
		private int hashCode;

		// Token: 0x04000535 RID: 1333
		private bool hashCodeCalculated;
	}
}
