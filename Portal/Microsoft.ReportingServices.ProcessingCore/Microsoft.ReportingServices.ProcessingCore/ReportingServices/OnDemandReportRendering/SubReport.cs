using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200034A RID: 842
	public sealed class SubReport : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem
	{
		// Token: 0x06002072 RID: 8306 RVA: 0x0007E0EA File Offset: 0x0007C2EA
		internal SubReport(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.SubReport reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0007E100 File Offset: 0x0007C300
		internal SubReport(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.SubReport renderSubReport, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderSubReport, renderingContext)
		{
		}

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06002074 RID: 8308 RVA: 0x0007E116 File Offset: 0x0007C316
		public string ReportName
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return ((Microsoft.ReportingServices.ReportProcessing.SubReport)this.m_renderReportItem.ReportItemDef).ReportPath;
				}
				return ((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef).ReportName;
			}
		}

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06002075 RID: 8309 RVA: 0x0007E146 File Offset: 0x0007C346
		public Microsoft.ReportingServices.OnDemandReportRendering.Report Report
		{
			get
			{
				this.RetrieveSubreport();
				return this.m_report;
			}
		}

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x06002076 RID: 8310 RVA: 0x0007E154 File Offset: 0x0007C354
		public ReportStringProperty NoRowsMessage
		{
			get
			{
				if (this.m_noRowsMessage == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_noRowsMessage = new ReportStringProperty(((Microsoft.ReportingServices.ReportProcessing.SubReport)this.m_renderReportItem.ReportItemDef).NoRows);
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo noRowsMessage = ((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef).NoRowsMessage;
						if (noRowsMessage == null)
						{
							this.m_noRowsMessage = new ReportStringProperty(false, null, null);
						}
						else
						{
							this.m_noRowsMessage = new ReportStringProperty(noRowsMessage.IsExpression, noRowsMessage.OriginalText, noRowsMessage.StringValue);
						}
					}
				}
				return this.m_noRowsMessage;
			}
		}

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x06002077 RID: 8311 RVA: 0x0007E1DA File Offset: 0x0007C3DA
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return !this.m_isOldSnapshot && ((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef).OmitBorderOnPageBreak;
			}
		}

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06002078 RID: 8312 RVA: 0x0007E1F6 File Offset: 0x0007C3F6
		public bool KeepTogether
		{
			get
			{
				return this.m_isOldSnapshot || ((Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef).KeepTogether;
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x0007E212 File Offset: 0x0007C412
		internal bool ProcessedWithError
		{
			get
			{
				this.RetrieveSubreport();
				return this.m_processedWithError;
			}
		}

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x0007E220 File Offset: 0x0007C420
		internal SubReportErrorCodes ErrorCode
		{
			get
			{
				this.RetrieveSubreport();
				return this.m_errorCode;
			}
		}

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x0007E22E File Offset: 0x0007C42E
		internal string ErrorMessage
		{
			get
			{
				this.RetrieveSubreport();
				return this.m_errorMessage;
			}
		}

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x0007E23C File Offset: 0x0007C43C
		internal bool NoRows
		{
			get
			{
				this.RetrieveSubreport();
				return this.m_noRows;
			}
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0007E24A File Offset: 0x0007C44A
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new Microsoft.ReportingServices.OnDemandReportRendering.SubReportInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0007E268 File Offset: 0x0007C468
		internal void RetrieveSubreport()
		{
			if (this.m_isNewContext)
			{
				if (this.m_isOldSnapshot)
				{
					Microsoft.ReportingServices.ReportRendering.SubReport subReport = (Microsoft.ReportingServices.ReportRendering.SubReport)this.m_renderReportItem;
					if (subReport.Report != null)
					{
						if (this.m_report == null)
						{
							this.m_report = new Microsoft.ReportingServices.OnDemandReportRendering.Report(this, this.m_inSubtotal, subReport, this.m_renderingContext);
						}
						else
						{
							this.m_report.UpdateSubReportContents(this, subReport);
						}
					}
					this.m_noRows = subReport.NoRows;
					this.m_processedWithError = subReport.ProcessedWithError;
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport2 = (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef;
					Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext = null;
					try
					{
						if (subReport2.ExceededMaxLevel)
						{
							this.m_errorCode = SubReportErrorCodes.ExceededMaxRecursionLevel;
							this.m_errorMessage = RPResWrapper.rsExceededMaxRecursionLevel(subReport2.Name);
							this.FinalizeErrorMessageAndThrow();
						}
						else
						{
							this.CheckRetrievalStatus(subReport2.RetrievalStatus);
						}
						if (this.m_renderingContext.InstanceAccessDisallowed)
						{
							renderingContext = this.GetOrCreateRenderingContext(subReport2, null);
							renderingContext.SubReportHasNoInstance = true;
						}
						else
						{
							this.m_renderingContext.OdpContext.SetupContext(subReport2, base.Instance.ReportScopeInstance);
							if (subReport2.CurrentSubReportInstance == null)
							{
								renderingContext = this.GetOrCreateRenderingContext(subReport2, null);
								renderingContext.SubReportHasNoInstance = true;
							}
							else
							{
								Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance = subReport2.CurrentSubReportInstance.Value();
								this.m_noRows = subReportInstance.NoRows;
								this.m_processedWithError = subReportInstance.ProcessedWithError;
								if (this.m_processedWithError)
								{
									this.CheckRetrievalStatus(subReportInstance.RetrievalStatus);
								}
								Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance = subReportInstance.ReportInstance.Value();
								renderingContext = this.GetOrCreateRenderingContext(subReport2, reportInstance);
								renderingContext.OdpContext.LoadExistingSubReportDataChunkNameModifier(subReportInstance);
								renderingContext.OdpContext.SetSubReportContext(subReportInstance, true);
								reportInstance.SetupEnvironment(renderingContext.OdpContext);
							}
						}
					}
					catch (Exception ex)
					{
						this.m_processedWithError = true;
						ErrorContext errorContext = null;
						if (subReport2.OdpContext != null)
						{
							errorContext = subReport2.OdpContext.ErrorContext;
						}
						if (renderingContext == null && this.m_report != null)
						{
							renderingContext = this.m_report.RenderingContext;
						}
						ReportProcessing.HandleSubReportProcessingError(this.m_renderingContext.OdpContext.TopLevelContext.ErrorContext, subReport2, subReport2.UniqueName, errorContext, ex);
					}
					if (renderingContext != null)
					{
						renderingContext.SubReportProcessedWithError = this.m_processedWithError;
					}
				}
				if (this.m_processedWithError)
				{
					this.m_noRows = false;
					if (this.m_errorCode == SubReportErrorCodes.Success)
					{
						this.m_errorCode = SubReportErrorCodes.ProcessingError;
						this.m_errorMessage = RPRes.rsRenderSubreportError;
					}
				}
				this.m_isNewContext = false;
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0007E4B4 File Offset: 0x0007C6B4
		private Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext GetOrCreateRenderingContext(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance)
		{
			Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext;
			if (this.m_report == null)
			{
				renderingContext = new Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext(this.m_renderingContext, subReport.OdpContext);
				this.m_report = new Microsoft.ReportingServices.OnDemandReportRendering.Report(this, subReport.Report, reportInstance, renderingContext, subReport.ReportName, subReport.Description, this.m_inSubtotal);
			}
			else
			{
				renderingContext = this.m_report.RenderingContext;
				this.m_report.SetNewContext(reportInstance);
			}
			return renderingContext;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0007E520 File Offset: 0x0007C720
		private void CheckRetrievalStatus(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status status)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport = (Microsoft.ReportingServices.ReportIntermediateFormat.SubReport)this.m_reportItemDef;
			switch (status)
			{
			case Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.NotRetrieved:
			case Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DefinitionRetrieveFailed:
				this.m_errorCode = SubReportErrorCodes.MissingSubReport;
				this.m_errorMessage = RPResWrapper.rsMissingSubReport(subReport.Name, subReport.OriginalCatalogPath);
				goto IL_00C5;
			case Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DataRetrieveFailed:
				this.m_errorCode = SubReportErrorCodes.DataRetrievalFailed;
				this.m_errorMessage = RPResWrapper.rsSubReportDataRetrievalFailed(subReport.Name, subReport.OriginalCatalogPath);
				goto IL_00C5;
			case Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DataNotRetrieved:
				this.m_errorCode = SubReportErrorCodes.DataNotRetrieved;
				this.m_errorMessage = RPResWrapper.rsSubReportDataNotRetrieved(subReport.Name, subReport.OriginalCatalogPath);
				goto IL_00C5;
			case Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.ParametersNotSpecified:
				this.m_errorCode = SubReportErrorCodes.ParametersNotSpecified;
				this.m_errorMessage = RPResWrapper.rsSubReportParametersNotSpecified(subReport.Name, subReport.OriginalCatalogPath);
				goto IL_00C5;
			}
			this.m_errorCode = SubReportErrorCodes.Success;
			this.m_errorMessage = null;
			IL_00C5:
			this.FinalizeErrorMessageAndThrow();
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0007E5F8 File Offset: 0x0007C7F8
		private void FinalizeErrorMessageAndThrow()
		{
			if (this.m_errorMessage != null)
			{
				IConfiguration configuration = this.m_renderingContext.OdpContext.Configuration;
				string errorMessage = this.m_errorMessage;
				if (configuration == null || !configuration.ShowSubreportErrorDetails)
				{
					this.m_errorMessage = RPRes.rsRenderSubreportError;
				}
				throw new RenderingObjectModelException(errorMessage);
			}
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0007E640 File Offset: 0x0007C840
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			this.SetNewContext();
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0007E64F File Offset: 0x0007C84F
		internal override void SetNewContext()
		{
			base.SetNewContext();
			this.m_isNewContext = true;
			this.m_noRows = true;
			this.m_processedWithError = false;
			this.m_errorCode = SubReportErrorCodes.Success;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0007E673 File Offset: 0x0007C873
		internal override void SetNewContextChildren()
		{
			if (this.m_report != null)
			{
				this.m_report.SetNewContext();
			}
		}

		// Token: 0x04001053 RID: 4179
		private Microsoft.ReportingServices.OnDemandReportRendering.Report m_report;

		// Token: 0x04001054 RID: 4180
		private ReportStringProperty m_noRowsMessage;

		// Token: 0x04001055 RID: 4181
		private bool m_processedWithError;

		// Token: 0x04001056 RID: 4182
		private SubReportErrorCodes m_errorCode;

		// Token: 0x04001057 RID: 4183
		private string m_errorMessage;

		// Token: 0x04001058 RID: 4184
		private bool m_noRows;

		// Token: 0x04001059 RID: 4185
		private bool m_isNewContext = true;
	}
}
