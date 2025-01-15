using System;
using System.Globalization;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002FE RID: 766
	public sealed class Page : ReportElement, IReportScope
	{
		// Token: 0x06001BFE RID: 7166 RVA: 0x0006FD2C File Offset: 0x0006DF2C
		internal Page(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ReportSection reportSection)
			: base(null, parentDefinitionPath, reportSection.SectionDef, renderingContext)
		{
			this.m_isOldSnapshot = false;
			this.m_pageDef = reportSection.SectionDef.Page;
			this.m_reportSection = reportSection;
		}

		// Token: 0x06001BFF RID: 7167 RVA: 0x0006FD5C File Offset: 0x0006DF5C
		internal Page(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.ReportRendering.Report renderReport, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ReportSection reportSection)
			: base(parentDefinitionPath, renderingContext)
		{
			this.m_isOldSnapshot = true;
			this.m_renderReport = renderReport;
			this.m_reportSection = reportSection;
		}

		// Token: 0x06001C00 RID: 7168 RVA: 0x0006FD7C File Offset: 0x0006DF7C
		internal void UpdateWithCurrentPageSections(Microsoft.ReportingServices.ReportRendering.PageSection header, Microsoft.ReportingServices.ReportRendering.PageSection footer)
		{
			if (header != null)
			{
				this.PageHeader.UpdatePageSection(header);
			}
			if (footer != null)
			{
				this.PageFooter.UpdatePageSection(footer);
			}
		}

		// Token: 0x06001C01 RID: 7169 RVA: 0x0006FD9C File Offset: 0x0006DF9C
		internal void UpdateSubReportContents(Microsoft.ReportingServices.ReportRendering.Report newRenderSubreport)
		{
			this.m_renderReport = newRenderSubreport;
			this.UpdateWithCurrentPageSections(this.m_renderReport.PageHeader, this.m_renderReport.PageFooter);
		}

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x0006FDC4 File Offset: 0x0006DFC4
		public override string ID
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.ReportDef.ID.ToString(CultureInfo.InvariantCulture) + "xP";
				}
				return this.m_pageDef.RenderingModelID;
			}
		}

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x0006FE0C File Offset: 0x0006E00C
		public override string DefinitionPath
		{
			get
			{
				return ((base.ParentDefinitionPath.DefinitionPath != null) ? base.ParentDefinitionPath.DefinitionPath : "") + "xP";
			}
		}

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0006FE38 File Offset: 0x0006E038
		public Microsoft.ReportingServices.OnDemandReportRendering.PageSection PageHeader
		{
			get
			{
				if (this.m_pageHeader == null && !this.m_renderingContext.IsSubReportContext)
				{
					if (this.m_isOldSnapshot && this.m_renderReport.PageHeader != null)
					{
						this.m_pageHeader = new Microsoft.ReportingServices.OnDemandReportRendering.PageSection(this, true, this.m_renderReport.PageHeader, this.m_reportSection.Report.HeaderFooterRenderingContext);
					}
					else if (!this.m_isOldSnapshot && this.m_pageDef.PageHeader != null)
					{
						this.m_pageHeader = new Microsoft.ReportingServices.OnDemandReportRendering.PageSection(this, this, true, this.m_pageDef.PageHeader, this.m_reportSection.Report.HeaderFooterRenderingContext);
					}
				}
				return this.m_pageHeader;
			}
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0006FEE0 File Offset: 0x0006E0E0
		public Microsoft.ReportingServices.OnDemandReportRendering.PageSection PageFooter
		{
			get
			{
				if (this.m_pageFooter == null && !this.m_renderingContext.IsSubReportContext)
				{
					if (this.m_isOldSnapshot && this.m_renderReport.PageFooter != null)
					{
						this.m_pageFooter = new Microsoft.ReportingServices.OnDemandReportRendering.PageSection(this, false, this.m_renderReport.PageFooter, this.m_reportSection.Report.HeaderFooterRenderingContext);
					}
					else if (!this.m_isOldSnapshot && this.m_pageDef.PageFooter != null)
					{
						this.m_pageFooter = new Microsoft.ReportingServices.OnDemandReportRendering.PageSection(this, this, false, this.m_pageDef.PageFooter, this.m_reportSection.Report.HeaderFooterRenderingContext);
					}
				}
				return this.m_pageFooter;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0006FF88 File Offset: 0x0006E188
		public ReportSize PageHeight
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.PageHeight);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.PageHeight;
				}
				if (this.m_pageDef.PageHeightForRendering == null)
				{
					this.m_pageDef.PageHeightForRendering = new ReportSize(this.m_pageDef.PageHeight, this.m_pageDef.PageHeightValue);
				}
				return this.m_pageDef.PageHeightForRendering;
			}
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x00070000 File Offset: 0x0006E200
		public ReportSize PageWidth
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.PageWidth);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.PageWidth;
				}
				if (this.m_pageDef.PageWidthForRendering == null)
				{
					this.m_pageDef.PageWidthForRendering = new ReportSize(this.m_pageDef.PageWidth, this.m_pageDef.PageWidthValue);
				}
				return this.m_pageDef.PageWidthForRendering;
			}
		}

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00070078 File Offset: 0x0006E278
		public ReportSize InteractiveHeight
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.ReportDef.InteractiveHeight, this.m_renderReport.ReportDef.InteractiveHeightValue);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.InteractiveHeight;
				}
				if (this.m_pageDef.InteractiveHeightForRendering == null)
				{
					this.m_pageDef.InteractiveHeightForRendering = new ReportSize(this.m_pageDef.InteractiveHeight, this.m_pageDef.InteractiveHeightValue);
				}
				return this.m_pageDef.InteractiveHeightForRendering;
			}
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00070108 File Offset: 0x0006E308
		public ReportSize InteractiveWidth
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.ReportDef.InteractiveWidth, this.m_renderReport.ReportDef.InteractiveWidthValue);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.InteractiveWidth;
				}
				if (this.m_pageDef.InteractiveWidthForRendering == null)
				{
					this.m_pageDef.InteractiveWidthForRendering = new ReportSize(this.m_pageDef.InteractiveWidth, this.m_pageDef.InteractiveWidthValue);
				}
				return this.m_pageDef.InteractiveWidthForRendering;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00070198 File Offset: 0x0006E398
		public ReportSize LeftMargin
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.LeftMargin);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.LeftMargin;
				}
				if (this.m_pageDef.LeftMarginForRendering == null)
				{
					this.m_pageDef.LeftMarginForRendering = new ReportSize(this.m_pageDef.LeftMargin, this.m_pageDef.LeftMarginValue);
				}
				return this.m_pageDef.LeftMarginForRendering;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x00070210 File Offset: 0x0006E410
		public ReportSize RightMargin
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.RightMargin);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.RightMargin;
				}
				if (this.m_pageDef.RightMarginForRendering == null)
				{
					this.m_pageDef.RightMarginForRendering = new ReportSize(this.m_pageDef.RightMargin, this.m_pageDef.RightMarginValue);
				}
				return this.m_pageDef.RightMarginForRendering;
			}
		}

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00070288 File Offset: 0x0006E488
		public ReportSize TopMargin
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.TopMargin);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.TopMargin;
				}
				if (this.m_pageDef.TopMarginForRendering == null)
				{
					this.m_pageDef.TopMarginForRendering = new ReportSize(this.m_pageDef.TopMargin, this.m_pageDef.TopMarginValue);
				}
				return this.m_pageDef.TopMarginForRendering;
			}
		}

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x00070300 File Offset: 0x0006E500
		public ReportSize BottomMargin
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.BottomMargin);
				}
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.BottomMargin;
				}
				if (this.m_pageDef.BottomMarginForRendering == null)
				{
					this.m_pageDef.BottomMarginForRendering = new ReportSize(this.m_pageDef.BottomMargin, this.m_pageDef.BottomMarginValue);
				}
				return this.m_pageDef.BottomMarginForRendering;
			}
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x00070378 File Offset: 0x0006E578
		internal override bool UseRenderStyle
		{
			get
			{
				return !this.m_renderReport.BodyHasBorderStyles;
			}
		}

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00070388 File Offset: 0x0006E588
		internal override IStyleContainer StyleContainer
		{
			get
			{
				return this.m_pageDef;
			}
		}

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00070390 File Offset: 0x0006E590
		public int Columns
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.Columns;
				}
				return this.m_pageDef.Columns;
			}
		}

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x000703B4 File Offset: 0x0006E5B4
		public ReportSize ColumnSpacing
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.ColumnSpacing);
				}
				if (this.m_pageDef.ColumnSpacingForRendering == null)
				{
					this.m_pageDef.ColumnSpacingForRendering = new ReportSize(this.m_pageDef.ColumnSpacing, this.m_pageDef.ColumnSpacingValue);
				}
				return this.m_pageDef.ColumnSpacingForRendering;
			}
		}

		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00070418 File Offset: 0x0006E618
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.ShouldUseFirstSection)
				{
					return this.FirstSectionPage.Style;
				}
				return base.Style;
			}
		}

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x00070434 File Offset: 0x0006E634
		internal Microsoft.ReportingServices.ReportRendering.Report RenderReport
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReport;
			}
		}

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x0007044F File Offset: 0x0006E64F
		internal override Microsoft.ReportingServices.ReportRendering.ReportItem RenderReportItem
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReport.Body;
			}
		}

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0007046F File Offset: 0x0006E66F
		internal Microsoft.ReportingServices.OnDemandReportRendering.Page FirstSectionPage
		{
			get
			{
				return this.m_reportSection.Report.FirstSection.Page;
			}
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00070486 File Offset: 0x0006E686
		internal bool ShouldUseFirstSection
		{
			get
			{
				return this.m_reportSection.SectionIndex > 0;
			}
		}

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x00070496 File Offset: 0x0006E696
		internal override string InstanceUniqueName
		{
			get
			{
				if (this.Instance != null)
				{
					return this.Instance.UniqueName;
				}
				return null;
			}
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x000704AD File Offset: 0x0006E6AD
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x000704B5 File Offset: 0x0006E6B5
		public new PageInstance Instance
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new PageInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000704E0 File Offset: 0x0006E6E0
		internal override void SetNewContextChildren()
		{
			if (this.m_pageHeader != null)
			{
				this.m_pageHeader.SetNewContext();
			}
			if (this.m_pageFooter != null)
			{
				this.m_pageFooter.SetNewContext();
			}
		}

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x00070508 File Offset: 0x0006E708
		internal override IReportScope ReportScope
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x0007050B File Offset: 0x0006E70B
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000FBD RID: 4029
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x00070513 File Offset: 0x0006E713
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_pageDef;
			}
		}

		// Token: 0x04000ED5 RID: 3797
		private PageInstance m_instance;

		// Token: 0x04000ED6 RID: 3798
		private Microsoft.ReportingServices.ReportIntermediateFormat.Page m_pageDef;

		// Token: 0x04000ED7 RID: 3799
		private Microsoft.ReportingServices.ReportRendering.Report m_renderReport;

		// Token: 0x04000ED8 RID: 3800
		private ReportSection m_reportSection;

		// Token: 0x04000ED9 RID: 3801
		private Microsoft.ReportingServices.OnDemandReportRendering.PageSection m_pageHeader;

		// Token: 0x04000EDA RID: 3802
		private Microsoft.ReportingServices.OnDemandReportRendering.PageSection m_pageFooter;
	}
}
