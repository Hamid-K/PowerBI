using System;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000064 RID: 100
	public sealed class CustomData
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x00019D64 File Offset: 0x00017F64
		internal CustomData(CustomReportItem owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00019D74 File Offset: 0x00017F74
		public bool NoRows
		{
			get
			{
				return this.m_owner.CriInstance == null || this.m_owner.CriInstance.Cells == null || this.m_owner.CriInstance.Cells.Count == 0 || this.m_owner.CriInstance.Cells[0].Count == 0;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00019DD8 File Offset: 0x00017FD8
		public DataCellCollection DataCells
		{
			get
			{
				DataCellCollection dataCellCollection = this.m_datacells;
				if (this.m_datacells == null)
				{
					if (!this.NoRows)
					{
						dataCellCollection = new DataCellCollection(this.m_owner, this.m_owner.CriInstance.CellRowCount, this.m_owner.CriInstance.CellColumnCount);
					}
					if (this.m_owner.UseCache)
					{
						this.m_datacells = dataCellCollection;
					}
				}
				return dataCellCollection;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x00019E40 File Offset: 0x00018040
		public DataGroupingCollection DataColumnGroupings
		{
			get
			{
				DataGroupingCollection dataGroupingCollection = this.m_columns;
				if (this.m_columns == null && this.m_owner.CriDefinition.Columns != null)
				{
					dataGroupingCollection = new DataGroupingCollection(this.m_owner, null, this.m_owner.CriDefinition.Columns, (this.m_owner.CriInstance == null) ? null : this.m_owner.CriInstance.ColumnInstances);
					if (this.m_owner.UseCache)
					{
						this.m_columns = dataGroupingCollection;
					}
				}
				return dataGroupingCollection;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00019EC0 File Offset: 0x000180C0
		public DataGroupingCollection DataRowGroupings
		{
			get
			{
				DataGroupingCollection dataGroupingCollection = this.m_rows;
				if (this.m_rows == null && this.m_owner.CriDefinition.Rows != null)
				{
					dataGroupingCollection = new DataGroupingCollection(this.m_owner, null, this.m_owner.CriDefinition.Rows, (this.m_owner.CriInstance == null) ? null : this.m_owner.CriInstance.RowInstances);
					if (this.m_owner.UseCache)
					{
						this.m_rows = dataGroupingCollection;
					}
				}
				return dataGroupingCollection;
			}
		}

		// Token: 0x040001CA RID: 458
		private CustomReportItem m_owner;

		// Token: 0x040001CB RID: 459
		private DataCellCollection m_datacells;

		// Token: 0x040001CC RID: 460
		private DataGroupingCollection m_columns;

		// Token: 0x040001CD RID: 461
		private DataGroupingCollection m_rows;
	}
}
