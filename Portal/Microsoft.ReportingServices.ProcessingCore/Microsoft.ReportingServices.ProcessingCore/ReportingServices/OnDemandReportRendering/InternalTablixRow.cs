using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000355 RID: 853
	internal sealed class InternalTablixRow : TablixRow
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x0007F078 File Offset: 0x0007D278
		internal InternalTablixRow(Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, int rowIndex, TablixRow rowDef)
			: base(owner, rowIndex)
		{
			this.m_rowDef = rowDef;
			this.m_cellROMDefs = new TablixCell[rowDef.Cells.Count];
		}

		// Token: 0x1700126E RID: 4718
		public override TablixCell this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				TablixCell tablixCell = this.m_rowDef.TablixCells[index];
				if (tablixCell.ColSpan > 0 && this.m_cellROMDefs[index] == null)
				{
					this.m_cellROMDefs[index] = new InternalTablixCell(this.m_owner, this.m_rowIndex, index, tablixCell);
				}
				return this.m_cellROMDefs[index];
			}
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0007F133 File Offset: 0x0007D333
		internal override IDataRegionCell GetIfExists(int index)
		{
			if (this.m_cellROMDefs != null && index >= 0 && index < this.Count)
			{
				return this.m_cellROMDefs[index];
			}
			return null;
		}

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007F154 File Offset: 0x0007D354
		public override int Count
		{
			get
			{
				return this.m_rowDef.Cells.Count;
			}
		}

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0007F166 File Offset: 0x0007D366
		public override ReportSize Height
		{
			get
			{
				if (this.m_height == null)
				{
					this.m_height = new ReportSize(this.m_rowDef.Height, this.m_rowDef.HeightValue);
				}
				return this.m_height;
			}
		}

		// Token: 0x0400106C RID: 4204
		private TablixRow m_rowDef;

		// Token: 0x0400106D RID: 4205
		private ReportSize m_height;

		// Token: 0x0400106E RID: 4206
		private TablixCell[] m_cellROMDefs;
	}
}
