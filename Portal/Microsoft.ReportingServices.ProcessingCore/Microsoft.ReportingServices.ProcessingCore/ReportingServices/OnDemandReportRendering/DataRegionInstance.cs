using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200029D RID: 669
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataRegionInstance : ReportItemInstance, IReportScopeInstance
	{
		// Token: 0x060019D0 RID: 6608 RVA: 0x0006885C File Offset: 0x00066A5C
		internal DataRegionInstance(Microsoft.ReportingServices.OnDemandReportRendering.DataRegion reportItemDef)
			: base(reportItemDef)
		{
			this.m_dataRegionDef = reportItemDef;
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x00068873 File Offset: 0x00066A73
		string IReportScopeInstance.UniqueName
		{
			get
			{
				return base.UniqueName;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0006887B File Offset: 0x00066A7B
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x00068883 File Offset: 0x00066A83
		bool IReportScopeInstance.IsNewContext
		{
			get
			{
				return this.m_isNewContext;
			}
			set
			{
				this.m_isNewContext = value;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0006888C File Offset: 0x00066A8C
		IReportScope IReportScopeInstance.ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00068894 File Offset: 0x00066A94
		public string NoRowsMessage
		{
			get
			{
				if (this.m_noRowsMessage == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_noRowsMessage = ((Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_reportElementDef.RenderReportItem).NoRowMessage;
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportElementDef.ReportItemDef;
						if (dataRegion.NoRowsMessage != null)
						{
							if (!dataRegion.NoRowsMessage.IsExpression)
							{
								this.m_noRowsMessage = dataRegion.NoRowsMessage.StringValue;
							}
							else
							{
								this.m_noRowsMessage = dataRegion.EvaluateNoRowsMessage(this, this.m_reportElementDef.RenderingContext.OdpContext);
							}
						}
					}
				}
				return this.m_noRowsMessage;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x00068934 File Offset: 0x00066B34
		public bool NoRows
		{
			get
			{
				if (this.m_noRows == null)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_noRows = new bool?(((Microsoft.ReportingServices.ReportRendering.DataRegion)this.m_reportElementDef.RenderReportItem).NoRows);
					}
					else
					{
						this.m_reportElementDef.RenderingContext.OdpContext.SetupContext(this.m_reportElementDef.ReportItemDef, this);
						this.m_noRows = new bool?(((Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportElementDef.ReportItemDef).NoRows);
					}
				}
				return this.m_noRows.Value;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x000689C9 File Offset: 0x00066BC9
		public override VisibilityInstance Visibility
		{
			get
			{
				if (this.m_reportElementDef.IsOldSnapshot && ((Microsoft.ReportingServices.OnDemandReportRendering.DataRegion)this.m_reportElementDef).DataRegionType == Microsoft.ReportingServices.OnDemandReportRendering.DataRegion.Type.List)
				{
					return null;
				}
				return base.Visibility;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000689F4 File Offset: 0x00066BF4
		public string PageName
		{
			get
			{
				if (!this.m_pageNameEvaluated)
				{
					if (this.m_reportElementDef.IsOldSnapshot)
					{
						this.m_pageName = null;
					}
					else
					{
						this.m_pageNameEvaluated = true;
						Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)this.m_reportElementDef.ReportItemDef;
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo pageName = dataRegion.PageName;
						if (pageName != null)
						{
							if (pageName.IsExpression)
							{
								this.m_pageName = dataRegion.EvaluatePageName(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
							}
							else
							{
								this.m_pageName = pageName.StringValue;
							}
						}
					}
				}
				return this.m_pageName;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00068A7F File Offset: 0x00066C7F
		internal override void SetNewContext()
		{
			this.m_isNewContext = true;
			this.m_noRowsMessage = null;
			this.m_noRows = null;
			this.m_pageNameEvaluated = false;
			this.m_pageName = null;
			base.SetNewContext();
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00068AB0 File Offset: 0x00066CB0
		internal void DoneSettingScopeID()
		{
			if (this.m_reportElementDef.RenderingContext.OdpContext.QueryRestartInfo.QueryRestartPosition.Count > 0)
			{
				this.m_reportElementDef.RenderingContext.OdpContext.QueryRestartInfo.EnableQueryRestart();
				this.m_reportElementDef.RenderingContext.OdpContext.SetupContext(this.m_dataRegionDef.ReportItemDef, (IReportScopeInstance)this.m_dataRegionDef.Instance);
				this.m_reportElementDef.RenderingContext.OdpContext.QueryRestartInfo.RomBasedRestart();
				return;
			}
			throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidScopeIDNotSet, new object[] { this.m_dataRegionDef.Name });
		}

		// Token: 0x04000CE2 RID: 3298
		private string m_noRowsMessage;

		// Token: 0x04000CE3 RID: 3299
		private bool m_isNewContext = true;

		// Token: 0x04000CE4 RID: 3300
		private bool? m_noRows;

		// Token: 0x04000CE5 RID: 3301
		private string m_pageName;

		// Token: 0x04000CE6 RID: 3302
		private bool m_pageNameEvaluated;

		// Token: 0x04000CE7 RID: 3303
		private readonly Microsoft.ReportingServices.OnDemandReportRendering.DataRegion m_dataRegionDef;
	}
}
