using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000058 RID: 88
	public sealed class PageCollection
	{
		// Token: 0x0600066A RID: 1642 RVA: 0x00018EE5 File Offset: 0x000170E5
		internal PageCollection(PaginationInfo paginationDef, Report report)
		{
			this.m_paginationDef = paginationDef;
			this.m_report = report;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00018EFB File Offset: 0x000170FB
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00018F08 File Offset: 0x00017108
		public int TotalCount
		{
			get
			{
				return this.m_paginationDef.TotalPageNumber;
			}
			set
			{
				this.m_paginationDef.TotalPageNumber = value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		public Page this[int pageNumber]
		{
			get
			{
				if (0 > pageNumber || pageNumber >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { pageNumber, 0, this.Count });
				}
				Page page = this.m_paginationDef[pageNumber];
				if (page != null && this.m_report != null)
				{
					if (page.PageSectionHeader == null)
					{
						page.PageSectionHeader = this.GetHeader(page.HeaderInstance);
					}
					if (page.PageSectionFooter == null)
					{
						page.PageSectionFooter = this.GetFooter(page.FooterInstance);
					}
				}
				return page;
			}
			set
			{
				if (0 > pageNumber || pageNumber >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { pageNumber, 0, this.Count });
				}
				this.m_paginationDef[pageNumber] = value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00019008 File Offset: 0x00017208
		public int Count
		{
			get
			{
				return this.m_paginationDef.CurrentPageCount;
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00019015 File Offset: 0x00017215
		public void Add(Page page)
		{
			this.m_paginationDef.AddPage(page);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00019023 File Offset: 0x00017223
		public void Clear()
		{
			this.m_paginationDef.Clear();
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00019030 File Offset: 0x00017230
		public void Insert(int index, Page page)
		{
			this.m_paginationDef.InsertPage(index, page);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001903F File Offset: 0x0001723F
		public void RemoveAt(int index)
		{
			this.m_paginationDef.RemovePage(index);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00019050 File Offset: 0x00017250
		internal PageSection GetHeader(PageSectionInstance headerInstance)
		{
			PageSection pageSection = null;
			Report reportDef = this.m_report.ReportDef;
			if (reportDef != null)
			{
				if (!reportDef.PageHeaderEvaluation)
				{
					pageSection = this.m_report.PageHeader;
				}
				else if (reportDef.PageHeader != null && headerInstance != null)
				{
					string text = headerInstance.PageNumber.ToString() + "ph";
					RenderingContext renderingContext = new RenderingContext(this.m_report.RenderingContext, text);
					pageSection = new PageSection(text, reportDef.PageHeader, headerInstance, this.m_report, renderingContext, false);
				}
			}
			return pageSection;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000190D4 File Offset: 0x000172D4
		internal PageSection GetFooter(PageSectionInstance footerInstance)
		{
			PageSection pageSection = null;
			Report reportDef = this.m_report.ReportDef;
			if (reportDef != null)
			{
				if (!reportDef.PageFooterEvaluation)
				{
					pageSection = this.m_report.PageFooter;
				}
				else if (reportDef.PageFooter != null && footerInstance != null)
				{
					string text = footerInstance.PageNumber.ToString() + "pf";
					RenderingContext renderingContext = new RenderingContext(this.m_report.RenderingContext, text);
					pageSection = new PageSection(text, reportDef.PageFooter, footerInstance, this.m_report, renderingContext, false);
				}
			}
			return pageSection;
		}

		// Token: 0x040001A9 RID: 425
		private PaginationInfo m_paginationDef;

		// Token: 0x040001AA RID: 426
		private Report m_report;
	}
}
