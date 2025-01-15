using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034B RID: 843
	public sealed class SubReportInstance : ReportItemInstance
	{
		// Token: 0x06002085 RID: 8325 RVA: 0x0007E688 File Offset: 0x0007C888
		internal SubReportInstance(Microsoft.ReportingServices.OnDemandReportRendering.SubReport reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0007E691 File Offset: 0x0007C891
		public bool ProcessedWithError
		{
			get
			{
				return this.SubReportDefinition.ProcessedWithError;
			}
		}

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x0007E69E File Offset: 0x0007C89E
		public SubReportErrorCodes ErrorCode
		{
			get
			{
				return this.SubReportDefinition.ErrorCode;
			}
		}

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0007E6AB File Offset: 0x0007C8AB
		public string ErrorMessage
		{
			get
			{
				return this.SubReportDefinition.ErrorMessage;
			}
		}

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0007E6B8 File Offset: 0x0007C8B8
		public string NoRowsMessage
		{
			get
			{
				if (this.m_noRowsMessageExpressionResult == null)
				{
					if (this.SubReportDefinition.IsOldSnapshot)
					{
						this.m_noRowsMessageExpressionResult = ((Microsoft.ReportingServices.ReportRendering.SubReport)this.SubReportDefinition.RenderReportItem).NoRowMessage;
					}
					else if (!this.SubReportDefinition.ProcessedWithError)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport = (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.SubReportDefinition.ReportItemDef;
						this.m_noRowsMessageExpressionResult = subReport.EvaulateNoRowMessage(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
					}
				}
				return this.m_noRowsMessageExpressionResult;
			}
		}

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0007E73D File Offset: 0x0007C93D
		public bool NoRows
		{
			get
			{
				return this.SubReportDefinition.NoRows;
			}
		}

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0007E74A File Offset: 0x0007C94A
		private Microsoft.ReportingServices.OnDemandReportRendering.SubReport SubReportDefinition
		{
			get
			{
				return this.m_reportElementDef as Microsoft.ReportingServices.OnDemandReportRendering.SubReport;
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x0007E757 File Offset: 0x0007C957
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_noRowsMessageExpressionResult = null;
		}

		// Token: 0x0400105A RID: 4186
		private string m_noRowsMessageExpressionResult;
	}
}
