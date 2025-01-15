using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000380 RID: 896
	internal sealed class ReportItemVisibilityInstance : VisibilityInstance
	{
		// Token: 0x06002253 RID: 8787 RVA: 0x00083E4F File Offset: 0x0008204F
		internal ReportItemVisibilityInstance(ReportItem reportitem)
			: base(reportitem.ReportScope)
		{
			this.m_reportItem = reportitem;
		}

		// Token: 0x17001370 RID: 4976
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x00083E64 File Offset: 0x00082064
		public override bool CurrentlyHidden
		{
			get
			{
				if (!this.m_cachedCurrentlyHidden)
				{
					this.m_cachedCurrentlyHidden = true;
					if (this.m_reportItem.IsOldSnapshot)
					{
						this.m_currentlyHiddenValue = this.m_reportItem.RenderReportItem.Hidden;
					}
					else
					{
						this.m_currentlyHiddenValue = this.m_reportItem.ReportItemDef.ComputeHidden(this.m_reportItem.RenderingContext, ToggleCascadeDirection.None);
					}
				}
				return this.m_currentlyHiddenValue;
			}
		}

		// Token: 0x17001371 RID: 4977
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00083ED0 File Offset: 0x000820D0
		public override bool StartHidden
		{
			get
			{
				if (!this.m_cachedStartHidden)
				{
					this.m_cachedStartHidden = true;
					if (this.m_reportItem.IsOldSnapshot)
					{
						this.m_startHiddenValue = this.m_reportItem.RenderReportItem.Hidden;
					}
					else
					{
						this.m_startHiddenValue = this.m_reportItem.ReportItemDef.ComputeStartHidden(this.m_reportItem.RenderingContext);
					}
				}
				return this.m_startHiddenValue;
			}
		}

		// Token: 0x040010F6 RID: 4342
		private ReportItem m_reportItem;
	}
}
