using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200035A RID: 858
	internal sealed class InternalTablixCell : TablixCell
	{
		// Token: 0x060020D1 RID: 8401 RVA: 0x0007F648 File Offset: 0x0007D848
		internal InternalTablixCell(Tablix owner, int rowIndex, int colIndex, TablixCell cellDef)
			: base(cellDef, owner, rowIndex, colIndex)
		{
			this.m_cellDef = cellDef;
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0007F65D File Offset: 0x0007D85D
		public override string ID
		{
			get
			{
				return this.m_cellDef.RenderingModelID;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0007F66A File Offset: 0x0007D86A
		public override string DataElementName
		{
			get
			{
				return this.m_cellDef.DataElementName;
			}
		}

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0007F677 File Offset: 0x0007D877
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_cellDef.DataElementOutput;
			}
		}

		// Token: 0x17001288 RID: 4744
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0007F684 File Offset: 0x0007D884
		public override StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return this.m_cellDef.StructureTypeOverwrite;
			}
		}

		// Token: 0x17001289 RID: 4745
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0007F694 File Offset: 0x0007D894
		public override CellContents CellContents
		{
			get
			{
				if (this.m_cellContents == null)
				{
					this.m_cellContents = new CellContents(this, this, this.m_cellDef.CellContents, this.m_cellDef.RowSpan, this.m_cellDef.ColSpan, this.m_owner.RenderingContext);
				}
				return this.m_cellContents;
			}
		}

		// Token: 0x0400107D RID: 4221
		private TablixCell m_cellDef;
	}
}
