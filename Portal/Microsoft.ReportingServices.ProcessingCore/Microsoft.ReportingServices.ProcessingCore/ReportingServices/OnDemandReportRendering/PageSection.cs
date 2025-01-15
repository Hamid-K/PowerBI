using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002FF RID: 767
	public sealed class PageSection : ReportElement
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x0007051B File Offset: 0x0006E71B
		internal PageSection(IReportScope reportScope, IDefinitionPath parentDefinitionPath, bool isHeader, Microsoft.ReportingServices.ReportIntermediateFormat.PageSection pageSectionDef, RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, pageSectionDef, renderingContext)
		{
			this.m_isHeader = isHeader;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00070530 File Offset: 0x0006E730
		internal PageSection(IDefinitionPath parentDefinitionPath, bool isHeader, Microsoft.ReportingServices.ReportRendering.PageSection renderPageSection, RenderingContext renderingContext)
			: base(parentDefinitionPath, renderPageSection, renderingContext)
		{
			this.m_isHeader = isHeader;
		}

		// Token: 0x17000FBE RID: 4030
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00070543 File Offset: 0x0006E743
		public override string ID
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReportItem.ID;
				}
				return this.m_reportItemDef.RenderingModelID;
			}
		}

		// Token: 0x17000FBF RID: 4031
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00070564 File Offset: 0x0006E764
		public override string DefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath.DefinitionPath + (this.m_isHeader ? "xH" : "xF");
			}
		}

		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0007058A File Offset: 0x0006E78A
		internal Microsoft.ReportingServices.OnDemandReportRendering.Page PageDefinition
		{
			get
			{
				return (Microsoft.ReportingServices.OnDemandReportRendering.Page)this.m_parentDefinitionPath;
			}
		}

		// Token: 0x17000FC1 RID: 4033
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00070597 File Offset: 0x0006E797
		internal Microsoft.ReportingServices.ReportIntermediateFormat.PageSection RifPageSection
		{
			get
			{
				return (Microsoft.ReportingServices.ReportIntermediateFormat.PageSection)this.m_reportItemDef;
			}
		}

		// Token: 0x17000FC2 RID: 4034
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x000705A4 File Offset: 0x0006E7A4
		internal Microsoft.ReportingServices.ReportRendering.PageSection RenderPageSection
		{
			get
			{
				return (Microsoft.ReportingServices.ReportRendering.PageSection)this.m_renderReportItem;
			}
		}

		// Token: 0x17000FC3 RID: 4035
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x000705B1 File Offset: 0x0006E7B1
		internal bool IsHeader
		{
			get
			{
				return this.m_isHeader;
			}
		}

		// Token: 0x17000FC4 RID: 4036
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x000705BC File Offset: 0x0006E7BC
		public ReportSize Height
		{
			get
			{
				if (this.m_height == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_height = new ReportSize(this.RenderPageSection.Height);
					}
					else
					{
						this.m_height = new ReportSize(this.RifPageSection.Height);
					}
				}
				return this.m_height;
			}
		}

		// Token: 0x17000FC5 RID: 4037
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x0007060D File Offset: 0x0006E80D
		public bool PrintOnFirstPage
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderPageSection.PrintOnFirstPage;
				}
				return this.RifPageSection.PrintOnFirstPage;
			}
		}

		// Token: 0x17000FC6 RID: 4038
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x0007062E File Offset: 0x0006E82E
		public bool PrintOnLastPage
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderPageSection.PrintOnLastPage;
				}
				return this.RifPageSection.PrintOnLastPage;
			}
		}

		// Token: 0x17000FC7 RID: 4039
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00070650 File Offset: 0x0006E850
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection ReportItemCollection
		{
			get
			{
				if (this.m_reportItems == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this, false, this.RenderPageSection.ReportItemCollection, this.m_renderingContext);
					}
					else
					{
						this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this.ReportScope, this, this.RifPageSection.ReportItems, this.m_renderingContext);
					}
				}
				return this.m_reportItems;
			}
		}

		// Token: 0x17000FC8 RID: 4040
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x000706B6 File Offset: 0x0006E8B6
		public bool PrintBetweenSections
		{
			get
			{
				return !this.m_isOldSnapshot && this.RifPageSection.PrintBetweenSections;
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x000706CD File Offset: 0x0006E8CD
		internal void UpdatePageSection(Microsoft.ReportingServices.ReportRendering.PageSection renderPageSection)
		{
			this.m_renderReportItem = renderPageSection;
			if (this.m_reportItems != null)
			{
				this.m_reportItems.UpdateRenderReportItem(renderPageSection.ReportItemCollection);
			}
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x000706EF File Offset: 0x0006E8EF
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			base.SetNewContext();
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x0007070A File Offset: 0x0006E90A
		internal override void SetNewContextChildren()
		{
			if (this.m_reportItems != null)
			{
				this.m_reportItems.SetNewContext();
			}
		}

		// Token: 0x17000FC9 RID: 4041
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x0007071F File Offset: 0x0006E91F
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

		// Token: 0x17000FCA RID: 4042
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00070736 File Offset: 0x0006E936
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000FCB RID: 4043
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x0007073E File Offset: 0x0006E93E
		public new PageSectionInstance Instance
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new PageSectionInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x04000EDB RID: 3803
		private bool m_isHeader;

		// Token: 0x04000EDC RID: 3804
		private ReportSize m_height;

		// Token: 0x04000EDD RID: 3805
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection m_reportItems;

		// Token: 0x04000EDE RID: 3806
		private PageSectionInstance m_instance;

		// Token: 0x04000EDF RID: 3807
		internal const string PageHeaderUniqueNamePrefix = "ph";

		// Token: 0x04000EE0 RID: 3808
		internal const string PageFooterUniqueNamePrefix = "pf";
	}
}
