using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000079 RID: 121
	public sealed class CellProperty : ISubordinateObject
	{
		// Token: 0x06000798 RID: 1944 RVA: 0x000253C9 File Offset: 0x000235C9
		internal CellProperty(DataTable cellsTable, DataRow cellRow, int propOrdinal, Cell cell)
		{
			this.cellsTable = cellsTable;
			this.cellRow = cellRow;
			this.propOrdinal = propOrdinal;
			this.cell = cell;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000253EE File Offset: 0x000235EE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x000253F6 File Offset: 0x000235F6
		public string Name
		{
			get
			{
				return this.cellsTable.Columns[this.propOrdinal].Caption;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x00025413 File Offset: 0x00023613
		public string Namespace
		{
			get
			{
				return this.cellsTable.Columns[this.propOrdinal].Namespace;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00025430 File Offset: 0x00023630
		public object Value
		{
			get
			{
				object obj = null;
				if (this.cellRow != null)
				{
					obj = this.cellRow[this.propOrdinal];
				}
				else if (this.Name == "CellOrdinal")
				{
					obj = this.cell.Ordinal;
				}
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
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x0002549D File Offset: 0x0002369D
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x000254A5 File Offset: 0x000236A5
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propOrdinal;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x000254AD File Offset: 0x000236AD
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(CellProperty);
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000254B9 File Offset: 0x000236B9
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000254DC File Offset: 0x000236DC
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000254EA File Offset: 0x000236EA
		public static bool operator ==(CellProperty o1, CellProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000254F3 File Offset: 0x000236F3
		public static bool operator !=(CellProperty o1, CellProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000542 RID: 1346
		private DataTable cellsTable;

		// Token: 0x04000543 RID: 1347
		private DataRow cellRow;

		// Token: 0x04000544 RID: 1348
		private int propOrdinal;

		// Token: 0x04000545 RID: 1349
		private Cell cell;

		// Token: 0x04000546 RID: 1350
		private int hashCode;

		// Token: 0x04000547 RID: 1351
		private bool hashCodeCalculated;
	}
}
