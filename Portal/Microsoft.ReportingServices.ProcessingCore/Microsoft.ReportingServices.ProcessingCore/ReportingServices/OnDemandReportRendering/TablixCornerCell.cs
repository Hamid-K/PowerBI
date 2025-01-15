using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000362 RID: 866
	public sealed class TablixCornerCell : IDefinitionPath
	{
		// Token: 0x060020F5 RID: 8437 RVA: 0x0007FDB0 File Offset: 0x0007DFB0
		internal TablixCornerCell(Tablix owner, int rowIndex, int colIndex, TablixCornerCell cellDef)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			this.m_cellDef = cellDef;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0007FDD5 File Offset: 0x0007DFD5
		internal TablixCornerCell(Tablix owner, int rowIndex, int colIndex, Microsoft.ReportingServices.ReportRendering.ReportItem cornerReportItem)
		{
			this.m_owner = owner;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			this.m_cornerReportItem = cornerReportItem;
		}

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0007FDFA File Offset: 0x0007DFFA
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetTablixCellDefinitionPath(this.m_owner, this.m_rowIndex, this.m_columnIndex, false);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x0007FE28 File Offset: 0x0007E028
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0007FE30 File Offset: 0x0007E030
		public CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						if (this.m_cornerReportItem != null)
						{
							int columns = this.m_owner.Columns;
							int rows = this.m_owner.Rows;
							this.m_cellContents = new CellContents(this, this.m_owner.InSubtotal, this.m_cornerReportItem, columns, rows, this.m_owner.RenderingContext);
						}
					}
					else
					{
						this.m_cellContents = new CellContents(this.m_owner.ReportScope, this, this.m_cellDef.CellContents, this.m_cellDef.RowSpan, this.m_cellDef.ColSpan, this.m_owner.RenderingContext);
					}
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x04001091 RID: 4241
		private Tablix m_owner;

		// Token: 0x04001092 RID: 4242
		private int m_rowIndex;

		// Token: 0x04001093 RID: 4243
		private int m_columnIndex;

		// Token: 0x04001094 RID: 4244
		private string m_definitionPath;

		// Token: 0x04001095 RID: 4245
		private TablixCornerCell m_cellDef;

		// Token: 0x04001096 RID: 4246
		private Microsoft.ReportingServices.ReportRendering.ReportItem m_cornerReportItem;

		// Token: 0x04001097 RID: 4247
		private CellContents m_cellContents;
	}
}
