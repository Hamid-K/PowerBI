using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000365 RID: 869
	public sealed class CellContents
	{
		// Token: 0x06002131 RID: 8497 RVA: 0x00080904 File Offset: 0x0007EB04
		internal CellContents(IReportScope reportScope, IDefinitionPath ownerPath, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem cellReportItem, int rowSpan, int colSpan, RenderingContext renderingContext)
		{
			this.m_reportScope = reportScope;
			this.m_rowSpan = rowSpan;
			this.m_colSpan = colSpan;
			this.m_ownerPath = ownerPath;
			this.m_isOldSnapshot = false;
			this.m_cellReportItem = cellReportItem;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0008095C File Offset: 0x0007EB5C
		internal CellContents(IDefinitionPath ownerPath, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, int rowSpan, int colSpan, RenderingContext renderingContext)
		{
			this.m_rowSpan = rowSpan;
			this.m_colSpan = colSpan;
			this.m_ownerPath = ownerPath;
			this.m_isOldSnapshot = true;
			this.m_inSubtotal = inSubtotal;
			this.m_renderReportItem = renderReportItem;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x000809B4 File Offset: 0x0007EBB4
		internal CellContents(IDefinitionPath ownerPath, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, int rowSpan, int colSpan, RenderingContext renderingContext, double sizeDelta, bool isColumn)
		{
			this.m_rowSpan = rowSpan;
			this.m_colSpan = colSpan;
			this.m_ownerPath = ownerPath;
			this.m_isOldSnapshot = true;
			this.m_inSubtotal = inSubtotal;
			this.m_renderReportItem = renderReportItem;
			this.m_renderingContext = renderingContext;
			this.m_sizeDelta = sizeDelta;
			this.m_isColumn = isColumn;
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00080A1C File Offset: 0x0007EC1C
		internal CellContents(Microsoft.ReportingServices.OnDemandReportRendering.Rectangle rectangle, int rowSpan, int colSpan, RenderingContext renderingContext)
		{
			this.m_rowSpan = rowSpan;
			this.m_colSpan = colSpan;
			this.m_ownerPath = rectangle;
			this.m_reportItem = rectangle;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = true;
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00080A68 File Offset: 0x0007EC68
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItem ReportItem
		{
			get
			{
				if (this.m_reportItem == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_reportItem = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateShim(this.m_ownerPath, 0, this.m_inSubtotal, this.m_renderReportItem, this.m_renderingContext);
						if (this.m_sizeDelta > 0.0)
						{
							if (this.m_isColumn)
							{
								this.m_reportItem.SetCachedWidth(this.m_sizeDelta);
							}
							else
							{
								this.m_reportItem.SetCachedHeight(this.m_sizeDelta);
							}
						}
					}
					else if (this.m_cellReportItem != null)
					{
						this.m_reportItem = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateItem(this.m_reportScope, this.m_ownerPath, 0, this.m_cellReportItem, this.m_renderingContext);
					}
				}
				if (this.m_reportItem != null)
				{
					return this.m_reportItem.ExposeAs(this.m_renderingContext);
				}
				return null;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00080B32 File Offset: 0x0007ED32
		public int ColSpan
		{
			get
			{
				return this.m_colSpan;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00080B3A File Offset: 0x0007ED3A
		public int RowSpan
		{
			get
			{
				return this.m_rowSpan;
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00080B42 File Offset: 0x0007ED42
		internal void SetNewContext()
		{
			if (this.m_reportItem != null)
			{
				this.m_reportItem.SetNewContext();
			}
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00080B57 File Offset: 0x0007ED57
		internal void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			if (renderReportItem != null)
			{
				this.m_renderReportItem = renderReportItem;
			}
			if (this.m_reportItem != null)
			{
				this.m_reportItem.UpdateRenderReportItem(this.m_renderReportItem);
			}
		}

		// Token: 0x040010A9 RID: 4265
		private IDefinitionPath m_ownerPath;

		// Token: 0x040010AA RID: 4266
		private bool m_isOldSnapshot;

		// Token: 0x040010AB RID: 4267
		private bool m_inSubtotal;

		// Token: 0x040010AC RID: 4268
		private RenderingContext m_renderingContext;

		// Token: 0x040010AD RID: 4269
		private Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_cellReportItem;

		// Token: 0x040010AE RID: 4270
		private Microsoft.ReportingServices.ReportRendering.ReportItem m_renderReportItem;

		// Token: 0x040010AF RID: 4271
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem m_reportItem;

		// Token: 0x040010B0 RID: 4272
		private int m_colSpan = 1;

		// Token: 0x040010B1 RID: 4273
		private int m_rowSpan = 1;

		// Token: 0x040010B2 RID: 4274
		private IReportScope m_reportScope;

		// Token: 0x040010B3 RID: 4275
		private double m_sizeDelta;

		// Token: 0x040010B4 RID: 4276
		private bool m_isColumn;
	}
}
