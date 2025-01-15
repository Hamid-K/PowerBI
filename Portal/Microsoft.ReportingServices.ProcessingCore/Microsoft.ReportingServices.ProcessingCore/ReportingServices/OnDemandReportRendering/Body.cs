using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002FD RID: 765
	public sealed class Body : ReportElement
	{
		// Token: 0x06001BEE RID: 7150 RVA: 0x0006FA82 File Offset: 0x0006DC82
		internal Body(IReportScope reportScope, IDefinitionPath parentDefinitionPath, ReportSection sectionDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, sectionDef, renderingContext)
		{
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0006FA8F File Offset: 0x0006DC8F
		internal Body(IDefinitionPath parentDefinitionPath, bool subreportInSubtotal, Microsoft.ReportingServices.ReportRendering.Report renderReport, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, renderingContext)
		{
			this.m_isOldSnapshot = true;
			this.m_subreportInSubtotal = subreportInSubtotal;
			this.m_renderReport = renderReport;
			this.m_renderingContext = renderingContext;
		}

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0006FAB7 File Offset: 0x0006DCB7
		internal override bool UseRenderStyle
		{
			get
			{
				return this.m_renderReport.BodyHasBorderStyles;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0006FAC4 File Offset: 0x0006DCC4
		public override string ID
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderReport.Body.ID;
				}
				if (this.m_renderingContext.IsSubReportContext)
				{
					return this.SectionDef.RenderingModelID + "xS";
				}
				return this.SectionDef.RenderingModelID + "xB";
			}
		}

		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0006FB24 File Offset: 0x0006DD24
		public override string DefinitionPath
		{
			get
			{
				string text;
				if (this.m_renderingContext.IsSubReportContext)
				{
					text = "xS";
				}
				else
				{
					text = "xB";
				}
				return this.m_parentDefinitionPath.DefinitionPath + text;
			}
		}

		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0006FB60 File Offset: 0x0006DD60
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection ReportItemCollection
		{
			get
			{
				if (this.m_reportItems == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this.m_parentDefinitionPath, this.m_subreportInSubtotal, this.m_renderReport.Body.ReportItemCollection, this.m_renderingContext);
					}
					else
					{
						this.m_reportItems = new Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection(this.ReportScope, this.m_parentDefinitionPath, this.SectionDef.ReportItems, this.m_renderingContext);
					}
				}
				return this.m_reportItems;
			}
		}

		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0006FBDC File Offset: 0x0006DDDC
		public ReportSize Height
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return new ReportSize(this.m_renderReport.Body.Height);
				}
				ReportSection sectionDef = this.SectionDef;
				if (sectionDef.HeightForRendering == null)
				{
					sectionDef.HeightForRendering = new ReportSize(sectionDef.Height, sectionDef.HeightValue);
				}
				return sectionDef.HeightForRendering;
			}
		}

		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0006FC33 File Offset: 0x0006DE33
		internal override Microsoft.ReportingServices.ReportRendering.ReportItem RenderReportItem
		{
			get
			{
				return this.m_renderReport.Body;
			}
		}

		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0006FC40 File Offset: 0x0006DE40
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

		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0006FC5B File Offset: 0x0006DE5B
		internal ReportSection SectionDef
		{
			get
			{
				return (ReportSection)this.m_reportItemDef;
			}
		}

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0006FC68 File Offset: 0x0006DE68
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

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0006FC7F File Offset: 0x0006DE7F
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x0006FC87 File Offset: 0x0006DE87
		public new BodyInstance Instance
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new BodyInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001BFB RID: 7163 RVA: 0x0006FCB2 File Offset: 0x0006DEB2
		internal void UpdateSubReportContents(Microsoft.ReportingServices.ReportRendering.Report newRenderSubreport)
		{
			this.m_renderReport = newRenderSubreport;
			if (this.m_reportItems != null)
			{
				this.m_reportItems.UpdateRenderReportItem(this.m_renderReport.Body.ReportItemCollection);
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0006FCDE File Offset: 0x0006DEDE
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.SectionDef != null)
			{
				this.SectionDef.ResetTextBoxImpls(this.m_renderingContext.OdpContext);
			}
			base.SetNewContext();
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0006FD17 File Offset: 0x0006DF17
		internal override void SetNewContextChildren()
		{
			if (this.m_reportItems != null)
			{
				this.m_reportItems.SetNewContext();
			}
		}

		// Token: 0x04000ED1 RID: 3793
		private Microsoft.ReportingServices.ReportRendering.Report m_renderReport;

		// Token: 0x04000ED2 RID: 3794
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItemCollection m_reportItems;

		// Token: 0x04000ED3 RID: 3795
		private BodyInstance m_instance;

		// Token: 0x04000ED4 RID: 3796
		private bool m_subreportInSubtotal;
	}
}
