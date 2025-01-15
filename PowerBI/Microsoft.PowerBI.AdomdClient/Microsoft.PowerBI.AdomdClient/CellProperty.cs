using System;
using System.Data;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000079 RID: 121
	public sealed class CellProperty : ISubordinateObject
	{
		// Token: 0x0600078B RID: 1931 RVA: 0x00025099 File Offset: 0x00023299
		internal CellProperty(DataTable cellsTable, DataRow cellRow, int propOrdinal, Cell cell)
		{
			this.cellsTable = cellsTable;
			this.cellRow = cellRow;
			this.propOrdinal = propOrdinal;
			this.cell = cell;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000250BE File Offset: 0x000232BE
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600078D RID: 1933 RVA: 0x000250C6 File Offset: 0x000232C6
		public string Name
		{
			get
			{
				return this.cellsTable.Columns[this.propOrdinal].Caption;
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x000250E3 File Offset: 0x000232E3
		public string Namespace
		{
			get
			{
				return this.cellsTable.Columns[this.propOrdinal].Namespace;
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00025100 File Offset: 0x00023300
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x0002516D File Offset: 0x0002336D
		object ISubordinateObject.Parent
		{
			get
			{
				return this.cell;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x00025175 File Offset: 0x00023375
		int ISubordinateObject.Ordinal
		{
			get
			{
				return this.propOrdinal;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0002517D File Offset: 0x0002337D
		Type ISubordinateObject.Type
		{
			get
			{
				return typeof(CellProperty);
			}
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00025189 File Offset: 0x00023389
		public override int GetHashCode()
		{
			if (!this.hashCodeCalculated)
			{
				this.hashCode = AdomdUtils.GetHashCode(this);
				this.hashCodeCalculated = true;
			}
			return this.hashCode;
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000251AC File Offset: 0x000233AC
		public override bool Equals(object obj)
		{
			return AdomdUtils.Equals(this, obj as ISubordinateObject);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000251BA File Offset: 0x000233BA
		public static bool operator ==(CellProperty o1, CellProperty o2)
		{
			return object.Equals(o1, o2);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000251C3 File Offset: 0x000233C3
		public static bool operator !=(CellProperty o1, CellProperty o2)
		{
			return !(o1 == o2);
		}

		// Token: 0x04000535 RID: 1333
		private DataTable cellsTable;

		// Token: 0x04000536 RID: 1334
		private DataRow cellRow;

		// Token: 0x04000537 RID: 1335
		private int propOrdinal;

		// Token: 0x04000538 RID: 1336
		private Cell cell;

		// Token: 0x04000539 RID: 1337
		private int hashCode;

		// Token: 0x0400053A RID: 1338
		private bool hashCodeCalculated;
	}
}
