using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000310 RID: 784
	public sealed class ReportSection : IDefinitionPath, IReportScope
	{
		// Token: 0x06001CEA RID: 7402 RVA: 0x00072E55 File Offset: 0x00071055
		internal ReportSection(Microsoft.ReportingServices.OnDemandReportRendering.Report reportDef, Microsoft.ReportingServices.ReportRendering.Report renderReport, int indexInCollection)
			: this(reportDef, indexInCollection)
		{
			this.m_renderReport = renderReport;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x00072E66 File Offset: 0x00071066
		internal ReportSection(Microsoft.ReportingServices.OnDemandReportRendering.Report reportDef, ReportSection sectionDef, int indexInCollection)
			: this(reportDef, indexInCollection)
		{
			this.m_sectionDef = sectionDef;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x00072E77 File Offset: 0x00071077
		private ReportSection(Microsoft.ReportingServices.OnDemandReportRendering.Report reportDef, int indexInCollection)
		{
			this.m_reportDef = reportDef;
			this.m_sectionIndex = indexInCollection;
			this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(reportDef, indexInCollection);
		}

		// Token: 0x17001028 RID: 4136
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x00072EA5 File Offset: 0x000710A5
		public string Name
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return "ReportSection0";
				}
				return this.m_sectionDef.Name;
			}
		}

		// Token: 0x17001029 RID: 4137
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x00072EC0 File Offset: 0x000710C0
		public Body Body
		{
			get
			{
				if (this.m_body == null)
				{
					if (this.IsOldSnapshot)
					{
						this.m_body = new Body(this, this.m_reportDef.SubreportInSubtotal, this.m_renderReport, this.m_reportDef.RenderingContext);
					}
					else
					{
						this.m_body = new Body(this, this, this.m_sectionDef, this.m_reportDef.RenderingContext);
					}
				}
				return this.m_body;
			}
		}

		// Token: 0x1700102A RID: 4138
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x00072F2C File Offset: 0x0007112C
		public ReportSize Width
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.Width);
				}
				if (this.m_sectionDef.WidthForRendering == null)
				{
					this.m_sectionDef.WidthForRendering = new ReportSize(this.m_sectionDef.Width, this.m_sectionDef.WidthValue);
				}
				return this.m_sectionDef.WidthForRendering;
			}
		}

		// Token: 0x1700102B RID: 4139
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x00072F90 File Offset: 0x00071190
		public Microsoft.ReportingServices.OnDemandReportRendering.Page Page
		{
			get
			{
				if (this.m_page == null)
				{
					if (this.m_reportDef.IsOldSnapshot)
					{
						this.m_page = new Microsoft.ReportingServices.OnDemandReportRendering.Page(this, this.m_reportDef.RenderReport, this.m_reportDef.RenderingContext, this);
					}
					else
					{
						this.m_page = new Microsoft.ReportingServices.OnDemandReportRendering.Page(this, this.m_reportDef.RenderingContext, this);
					}
				}
				return this.m_page;
			}
		}

		// Token: 0x1700102C RID: 4140
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00072FF5 File Offset: 0x000711F5
		public string DataElementName
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return string.Empty;
				}
				return this.m_sectionDef.DataElementName;
			}
		}

		// Token: 0x1700102D RID: 4141
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00073010 File Offset: 0x00071210
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return DataElementOutputTypes.ContentsOnly;
				}
				return this.m_sectionDef.DataElementOutput;
			}
		}

		// Token: 0x1700102E RID: 4142
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00073027 File Offset: 0x00071227
		public ReportSectionInstance Instance
		{
			get
			{
				if (this.m_reportDef.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new ReportSectionInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x1700102F RID: 4143
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00073057 File Offset: 0x00071257
		public string ID
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderReport.Body.ID + "xE";
				}
				return this.m_sectionDef.RenderingModelID;
			}
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00073087 File Offset: 0x00071287
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_reportDef.IsOldSnapshot;
			}
		}

		// Token: 0x17001031 RID: 4145
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x00073094 File Offset: 0x00071294
		internal ReportSection SectionDef
		{
			get
			{
				return this.m_sectionDef;
			}
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x0007309C File Offset: 0x0007129C
		// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x000730A4 File Offset: 0x000712A4
		internal ReportItemsImpl BodyItemsForHeadFoot
		{
			get
			{
				return this.m_bodyItemsForHeadFoot;
			}
			set
			{
				this.m_bodyItemsForHeadFoot = value;
			}
		}

		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x000730AD File Offset: 0x000712AD
		// (set) Token: 0x06001CFA RID: 7418 RVA: 0x000730B5 File Offset: 0x000712B5
		internal ReportItemsImpl PageSectionItemsForHeadFoot
		{
			get
			{
				return this.m_pageSectionItemsForHeadFoot;
			}
			set
			{
				this.m_pageSectionItemsForHeadFoot = value;
			}
		}

		// Token: 0x17001034 RID: 4148
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x000730BE File Offset: 0x000712BE
		// (set) Token: 0x06001CFC RID: 7420 RVA: 0x000730C6 File Offset: 0x000712C6
		internal Dictionary<string, AggregatesImpl> PageAggregatesOverReportItems
		{
			get
			{
				return this.m_pageAggregatesOverReportItems;
			}
			set
			{
				this.m_pageAggregatesOverReportItems = value;
			}
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x000730CF File Offset: 0x000712CF
		internal Microsoft.ReportingServices.OnDemandReportRendering.Report Report
		{
			get
			{
				return this.m_reportDef;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000730D7 File Offset: 0x000712D7
		internal int SectionIndex
		{
			get
			{
				return this.m_sectionIndex;
			}
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000730DF File Offset: 0x000712DF
		internal void UpdateSubReportContents(Microsoft.ReportingServices.ReportRendering.Report newRenderSubreport)
		{
			this.m_renderReport = newRenderSubreport;
			if (this.m_body != null)
			{
				this.m_body.UpdateSubReportContents(this.m_renderReport);
			}
			if (this.m_page != null)
			{
				this.m_page.UpdateSubReportContents(this.m_renderReport);
			}
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x0007311A File Offset: 0x0007131A
		internal void SetNewContext()
		{
			if (this.m_body != null)
			{
				this.m_body.SetNewContext();
			}
			if (this.m_page != null)
			{
				this.m_page.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00073158 File Offset: 0x00071358
		public void GetPageSections()
		{
			PageEvaluation pageEvaluation = this.m_reportDef.PageEvaluation;
			if (pageEvaluation == null)
			{
				return;
			}
			pageEvaluation.UpdatePageSections(this);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x0007317C File Offset: 0x0007137C
		public void SetPage(int pageNumber, int totalPages, int overallPageNumber, int overallTotalPages)
		{
			PageEvaluation pageEvaluation = this.m_reportDef.PageEvaluation;
			if (pageEvaluation == null)
			{
				return;
			}
			pageEvaluation.Reset(this, pageNumber, totalPages, overallPageNumber, overallTotalPages);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000731A5 File Offset: 0x000713A5
		public void SetPage(int pageNumber, int totalPages)
		{
			this.SetPage(pageNumber, totalPages, pageNumber, totalPages);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000731B4 File Offset: 0x000713B4
		public void SetPageName(string pageName)
		{
			PageEvaluation pageEvaluation = this.m_reportDef.PageEvaluation;
			if (pageEvaluation == null)
			{
				return;
			}
			pageEvaluation.SetPageName(pageName);
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000731D8 File Offset: 0x000713D8
		public bool NeedsTotalPages
		{
			get
			{
				return this.NeedsOverallTotalPages || this.NeedsPageBreakTotalPages;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000731EA File Offset: 0x000713EA
		public bool NeedsOverallTotalPages
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderReport.NeedsHeaderFooterEvaluation;
				}
				return this.m_sectionDef.NeedsOverallTotalPages;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0007320B File Offset: 0x0007140B
		public bool NeedsPageBreakTotalPages
		{
			get
			{
				return !this.IsOldSnapshot && this.m_sectionDef.NeedsPageBreakTotalPages;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x00073222 File Offset: 0x00071422
		public bool NeedsReportItemsOnPage
		{
			get
			{
				if (this.IsOldSnapshot)
				{
					return this.m_renderReport.NeedsHeaderFooterEvaluation;
				}
				return this.m_sectionDef.NeedsReportItemsOnPage;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00073243 File Offset: 0x00071443
		public string DefinitionPath
		{
			get
			{
				return this.m_definitionPath;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0007324B File Offset: 0x0007144B
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_reportDef;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x00073253 File Offset: 0x00071453
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0007325B File Offset: 0x0007145B
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_sectionDef;
			}
		}

		// Token: 0x04000F27 RID: 3879
		private Microsoft.ReportingServices.ReportRendering.Report m_renderReport;

		// Token: 0x04000F28 RID: 3880
		private Microsoft.ReportingServices.OnDemandReportRendering.Report m_reportDef;

		// Token: 0x04000F29 RID: 3881
		private int m_sectionIndex;

		// Token: 0x04000F2A RID: 3882
		private ReportSectionInstance m_instance;

		// Token: 0x04000F2B RID: 3883
		private Microsoft.ReportingServices.OnDemandReportRendering.Page m_page;

		// Token: 0x04000F2C RID: 3884
		private Body m_body;

		// Token: 0x04000F2D RID: 3885
		private string m_definitionPath;

		// Token: 0x04000F2E RID: 3886
		private ReportSection m_sectionDef;

		// Token: 0x04000F2F RID: 3887
		private ReportItemsImpl m_bodyItemsForHeadFoot;

		// Token: 0x04000F30 RID: 3888
		private ReportItemsImpl m_pageSectionItemsForHeadFoot;

		// Token: 0x04000F31 RID: 3889
		private Dictionary<string, AggregatesImpl> m_pageAggregatesOverReportItems = new Dictionary<string, AggregatesImpl>();
	}
}
