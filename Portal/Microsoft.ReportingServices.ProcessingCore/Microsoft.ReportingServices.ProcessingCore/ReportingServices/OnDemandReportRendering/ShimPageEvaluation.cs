using System;
using System.Collections;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DC RID: 732
	internal sealed class ShimPageEvaluation : PageEvaluation
	{
		// Token: 0x06001B52 RID: 6994 RVA: 0x0006D164 File Offset: 0x0006B364
		internal ShimPageEvaluation(Microsoft.ReportingServices.OnDemandReportRendering.Report report)
			: base(report)
		{
			this.InitializeEnvironment();
			this.PageInit();
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0006D179 File Offset: 0x0006B379
		internal override void Reset(ReportSection section, int newPageNumber, int newTotalPages, int newOverallPageNumber, int newOverallTotalPages)
		{
			base.Reset(section, newPageNumber, newTotalPages, newOverallPageNumber, newOverallTotalPages);
			this.PageInit();
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0006D190 File Offset: 0x0006B390
		internal override void Add(string textboxName, object textboxValue)
		{
			if (textboxName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			foreach (object obj in this.m_aggregates.Objects)
			{
				DataAggregateObj dataAggregateObj = (DataAggregateObj)obj;
				this.m_processingContext.ReportObjectModel.AggregatesImpl.Add(dataAggregateObj);
			}
			if (this.m_processingContext.ReportItemsReferenced)
			{
				TextBoxImpl textBoxImpl = (TextBoxImpl)this.m_processingContext.ReportObjectModel.ReportItemsImpl[textboxName];
				if (textBoxImpl != null)
				{
					textBoxImpl.SetResult(new VariantResult(false, textboxValue));
				}
				AggregatesImpl aggregatesImpl = (AggregatesImpl)this.m_aggregatesOverReportItems[textboxName];
				if (aggregatesImpl != null)
				{
					foreach (object obj2 in aggregatesImpl.Objects)
					{
						((DataAggregateObj)obj2).Update();
					}
				}
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0006D2A4 File Offset: 0x0006B4A4
		internal override void UpdatePageSections(ReportSection section)
		{
			Microsoft.ReportingServices.ReportRendering.PageSection pageSection = null;
			Microsoft.ReportingServices.ReportRendering.PageSection pageSection2 = null;
			foreach (object obj in this.m_aggregatesOverReportItems.Values)
			{
				foreach (object obj2 in ((AggregatesImpl)obj).Objects)
				{
					DataAggregateObj dataAggregateObj = (DataAggregateObj)obj2;
					this.m_processingContext.ReportObjectModel.AggregatesImpl.Add(dataAggregateObj);
				}
			}
			if (this.m_report.PageHeaderEvaluation)
			{
				pageSection = this.GenerateRenderPageSection(this.m_report.PageHeader, "ph");
			}
			if (this.m_report.PageFooterEvaluation)
			{
				pageSection2 = this.GenerateRenderPageSection(this.m_report.PageFooter, "pf");
			}
			this.m_aggregates = null;
			this.m_aggregatesOverReportItems = null;
			section.Page.UpdateWithCurrentPageSections(pageSection, pageSection2);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0006D3C0 File Offset: 0x0006B5C0
		private Microsoft.ReportingServices.ReportRendering.PageSection GenerateRenderPageSection(Microsoft.ReportingServices.ReportProcessing.PageSection pageSection, string uniqueNamePrefix)
		{
			PageSectionInstance pageSectionInstance = new PageSectionInstance(this.m_processingContext, this.m_currentPageNumber, pageSection);
			ReportProcessing.PageMerge.CreateInstances(this.m_processingContext, pageSectionInstance.ReportItemColInstance, pageSection.ReportItems);
			string text = this.m_currentPageNumber.ToString(CultureInfo.InvariantCulture) + uniqueNamePrefix;
			Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext = new Microsoft.ReportingServices.ReportRendering.RenderingContext(this.m_romReport.RenderReport.RenderingContext, text);
			return new Microsoft.ReportingServices.ReportRendering.PageSection(text, pageSection, pageSectionInstance, this.m_romReport.RenderReport, renderingContext, false);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0006D43C File Offset: 0x0006B63C
		private void InitializeEnvironment()
		{
			this.m_report = this.m_romReport.RenderReport.ReportDef;
			ReportInstance reportInstance = this.m_romReport.RenderReport.ReportInstance;
			Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext = this.m_romReport.RenderReport.RenderingContext;
			ReportSnapshot reportSnapshot = renderingContext.ReportSnapshot;
			ReportInstanceInfo reportInstanceInfo = (ReportInstanceInfo)reportInstance.GetInstanceInfo(renderingContext.ChunkManager);
			this.m_processingContext = new ReportProcessing.ProcessingContext(renderingContext.TopLevelReportContext, this.m_report.ShowHideType, renderingContext.GetResourceCallback, this.m_report.EmbeddedImages, this.m_report.ImageStreamNames, new ProcessingErrorContext(), !this.m_report.PageMergeOnePass, renderingContext.AllowUserProfileState, renderingContext.ReportRuntimeSetup, renderingContext.DataProtection);
			this.m_reportCulture = Localization.DefaultReportServerSpecificCulture;
			if (this.m_report.Language != null)
			{
				string text;
				if (this.m_report.Language.Type == ExpressionInfo.Types.Constant)
				{
					text = this.m_report.Language.Value;
				}
				else
				{
					text = reportInstance.Language;
				}
				if (text != null)
				{
					try
					{
						this.m_reportCulture = new CultureInfo(text, false);
						if (this.m_reportCulture.IsNeutralCulture)
						{
							this.m_reportCulture = CultureInfo.CreateSpecificCulture(text);
							this.m_reportCulture = new CultureInfo(this.m_reportCulture.Name, false);
						}
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
					}
				}
			}
			this.m_processingContext.ReportObjectModel = new ObjectModelImpl(this.m_processingContext);
			Global.Tracer.Assert(this.m_processingContext.ReportRuntime == null, "(m_processingContext.ReportRuntime == null)");
			this.m_processingContext.ReportRuntime = new ReportRuntime(this.m_processingContext.ReportObjectModel, this.m_processingContext.ErrorContext);
			this.m_processingContext.ReportObjectModel.FieldsImpl = new FieldsImpl();
			this.m_processingContext.ReportObjectModel.ParametersImpl = new ParametersImpl(reportInstanceInfo.Parameters.Count);
			this.m_processingContext.ReportObjectModel.GlobalsImpl = new GlobalsImpl(reportInstanceInfo.ReportName, this.m_currentPageNumber, this.m_totalPages, reportSnapshot.ExecutionTime, reportSnapshot.ReportServerUrl, reportSnapshot.ReportFolder);
			this.m_processingContext.ReportObjectModel.UserImpl = new UserImpl(reportSnapshot.RequestUserName, reportSnapshot.Language, this.m_processingContext.AllowUserProfileState);
			this.m_processingContext.ReportObjectModel.DataSetsImpl = new DataSetsImpl();
			this.m_processingContext.ReportObjectModel.DataSourcesImpl = new DataSourcesImpl(this.m_report.DataSourceCount);
			for (int i = 0; i < reportInstanceInfo.Parameters.Count; i++)
			{
				this.m_processingContext.ReportObjectModel.ParametersImpl.Add(reportInstanceInfo.Parameters[i].Name, new ParameterImpl(reportInstanceInfo.Parameters[i].Values, reportInstanceInfo.Parameters[i].Labels, reportInstanceInfo.Parameters[i].MultiValue));
			}
			this.m_processingContext.ReportRuntime.LoadCompiledCode(this.m_report, false, this.m_processingContext.ReportObjectModel, this.m_processingContext.ReportRuntimeSetup);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x0006D778 File Offset: 0x0006B978
		private void PageInit()
		{
			this.m_processingContext.ReportObjectModel.GlobalsImpl.SetPageNumbers(this.m_currentPageNumber, this.m_totalPages);
			this.m_processingContext.ReportObjectModel.ReportItemsImpl = new ReportItemsImpl();
			this.m_processingContext.ReportObjectModel.AggregatesImpl = new AggregatesImpl(this.m_processingContext.ReportRuntime);
			if (this.m_processingContext.ReportRuntime.ReportExprHost != null)
			{
				this.m_processingContext.RuntimeInitializeReportItemObjs(this.m_report.ReportItems, true, true);
				if (this.m_report.PageHeader != null)
				{
					if (this.m_processingContext.ReportRuntime.ReportExprHost != null)
					{
						this.m_report.PageHeader.SetExprHost(this.m_processingContext.ReportRuntime.ReportExprHost, this.m_processingContext.ReportObjectModel);
					}
					this.m_processingContext.RuntimeInitializeReportItemObjs(this.m_report.PageHeader.ReportItems, false, false);
				}
				if (this.m_report.PageFooter != null)
				{
					if (this.m_processingContext.ReportRuntime.ReportExprHost != null)
					{
						this.m_report.PageFooter.SetExprHost(this.m_processingContext.ReportRuntime.ReportExprHost, this.m_processingContext.ReportObjectModel);
					}
					this.m_processingContext.RuntimeInitializeReportItemObjs(this.m_report.PageFooter.ReportItems, false, false);
				}
			}
			this.m_aggregates = new AggregatesImpl(this.m_processingContext.ReportRuntime);
			this.m_aggregatesOverReportItems = new Hashtable();
			this.m_processingContext.ReportObjectModel.ReportItemsImpl.SpecialMode = true;
			if (this.m_report.PageAggregates != null)
			{
				for (int i = 0; i < this.m_report.PageAggregates.Count; i++)
				{
					DataAggregateInfo dataAggregateInfo = this.m_report.PageAggregates[i];
					dataAggregateInfo.ExprHostInitialized = false;
					DataAggregateObj dataAggregateObj = new DataAggregateObj(dataAggregateInfo, this.m_processingContext);
					object[] array;
					DataFieldStatus dataFieldStatus;
					dataAggregateObj.EvaluateParameters(out array, out dataFieldStatus);
					string specialModeIndex = this.m_processingContext.ReportObjectModel.ReportItemsImpl.GetSpecialModeIndex();
					if (specialModeIndex == null)
					{
						this.m_aggregates.Add(dataAggregateObj);
					}
					else
					{
						AggregatesImpl aggregatesImpl = (AggregatesImpl)this.m_aggregatesOverReportItems[specialModeIndex];
						if (aggregatesImpl == null)
						{
							aggregatesImpl = new AggregatesImpl(this.m_processingContext.ReportRuntime);
							this.m_aggregatesOverReportItems.Add(specialModeIndex, aggregatesImpl);
						}
						aggregatesImpl.Add(dataAggregateObj);
					}
					dataAggregateObj.Init();
				}
			}
			this.m_processingContext.ReportObjectModel.ReportItemsImpl.SpecialMode = false;
		}

		// Token: 0x04000D83 RID: 3459
		private Microsoft.ReportingServices.ReportProcessing.Report m_report;

		// Token: 0x04000D84 RID: 3460
		private CultureInfo m_reportCulture;

		// Token: 0x04000D85 RID: 3461
		private Hashtable m_aggregatesOverReportItems;

		// Token: 0x04000D86 RID: 3462
		private AggregatesImpl m_aggregates;

		// Token: 0x04000D87 RID: 3463
		private ReportProcessing.ProcessingContext m_processingContext;
	}
}
