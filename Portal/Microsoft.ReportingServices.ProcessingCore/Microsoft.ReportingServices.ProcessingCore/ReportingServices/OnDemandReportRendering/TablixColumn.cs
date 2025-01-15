using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034E RID: 846
	public sealed class TablixColumn
	{
		// Token: 0x06002096 RID: 8342 RVA: 0x0007EA6D File Offset: 0x0007CC6D
		internal TablixColumn(Tablix owner, int columnIndex)
		{
			this.m_owner = owner;
			this.m_columnIndex = columnIndex;
		}

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0007EA84 File Offset: 0x0007CC84
		public ReportSize Width
		{
			get
			{
				if (this.m_width == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						switch (this.m_owner.SnapshotTablixType)
						{
						case DataRegion.Type.List:
							this.m_width = new ReportSize(this.m_owner.RenderList.Width);
							break;
						case DataRegion.Type.Table:
							this.m_width = new ReportSize(this.m_owner.RenderTable.Columns[this.m_columnIndex].Width);
							break;
						case DataRegion.Type.Matrix:
						{
							int num = this.m_owner.MatrixColDefinitionMapping[this.m_columnIndex];
							this.m_width = new ReportSize(this.m_owner.RenderMatrix.CellWidths[num]);
							break;
						}
						}
					}
					else
					{
						TablixColumn tablixColumn = this.m_owner.TablixDef.TablixColumns[this.m_columnIndex];
						this.m_width = new ReportSize(tablixColumn.Width, tablixColumn.WidthValue);
					}
				}
				return this.m_width;
			}
		}

		// Token: 0x04001061 RID: 4193
		private Tablix m_owner;

		// Token: 0x04001062 RID: 4194
		private int m_columnIndex;

		// Token: 0x04001063 RID: 4195
		private ReportSize m_width;
	}
}
