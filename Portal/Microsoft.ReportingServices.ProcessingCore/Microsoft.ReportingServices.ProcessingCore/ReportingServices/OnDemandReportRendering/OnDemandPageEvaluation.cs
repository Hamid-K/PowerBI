using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002DB RID: 731
	internal sealed class OnDemandPageEvaluation : PageEvaluation
	{
		// Token: 0x06001B4C RID: 6988 RVA: 0x0006CA82 File Offset: 0x0006AC82
		internal OnDemandPageEvaluation(Microsoft.ReportingServices.OnDemandReportRendering.Report report)
			: base(report)
		{
			this.InitializeEnvironment();
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0006CA9C File Offset: 0x0006AC9C
		internal override void Add(string textboxName, object textboxValue)
		{
			if (textboxName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (this.m_processingContext.ReportItemsReferenced)
			{
				TextBoxImpl textBoxImpl = (TextBoxImpl)this.m_processingContext.ReportObjectModel.ReportItemsImpl[textboxName];
				if (textBoxImpl != null)
				{
					textBoxImpl.SetResult(new Microsoft.ReportingServices.RdlExpressions.VariantResult(false, textboxValue));
				}
				ReportSection reportSection;
				AggregatesImpl aggregatesImpl;
				if (this.m_reportItemToReportSection.TryGetValue(textboxName, out reportSection) && reportSection.PageAggregatesOverReportItems.TryGetValue(textboxName, out aggregatesImpl))
				{
					foreach (object obj in aggregatesImpl.Objects)
					{
						((Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj)obj).Update();
					}
				}
			}
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0006CB60 File Offset: 0x0006AD60
		internal override void UpdatePageSections(ReportSection section)
		{
			if (section.Page.PageHeader == null && section.Page.PageFooter == null)
			{
				return;
			}
			ObjectModelImpl reportObjectModel = this.m_processingContext.ReportObjectModel;
			reportObjectModel.GlobalsImpl.SetPageName(this.m_pageName);
			if (section.PageAggregatesOverReportItems == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidPageSectionState, new object[] { section.SectionIndex });
			}
			foreach (AggregatesImpl aggregatesImpl in section.PageAggregatesOverReportItems.Values)
			{
				foreach (object obj in aggregatesImpl.Objects)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj)obj;
					reportObjectModel.AggregatesImpl.Add(dataAggregateObj);
				}
			}
			section.PageAggregatesOverReportItems = null;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0006CC68 File Offset: 0x0006AE68
		internal override void Reset(ReportSection section, int newPageNumber, int newTotalPages, int newOverallPageNumber, int newOverallTotalPages)
		{
			base.Reset(section, newPageNumber, newTotalPages, newOverallPageNumber, newOverallTotalPages);
			if (section.Page.PageHeader != null || section.Page.PageFooter != null)
			{
				this.PageInit(section);
			}
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006CC98 File Offset: 0x0006AE98
		private void InitializeEnvironment()
		{
			this.m_processingContext = this.m_romReport.HeaderFooterRenderingContext.OdpContext;
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef = this.m_romReport.ReportDef;
			ObjectModelImpl reportObjectModel = this.m_processingContext.ReportObjectModel;
			if (reportDef.DataSetsNotOnlyUsedInParameters == 1)
			{
				this.m_processingContext.SetupFieldsForNewDataSetPageSection(reportDef.FirstDataSet);
			}
			else
			{
				this.m_processingContext.SetupEmptyTopLevelFields();
			}
			reportObjectModel.VariablesImpl = new VariablesImpl(false);
			if (reportDef.HasVariables)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance currentReportInstance = this.m_romReport.RenderingContext.OdpContext.CurrentReportInstance;
				this.m_processingContext.RuntimeInitializePageSectionVariables(reportDef, (currentReportInstance != null) ? currentReportInstance.VariableValues : null);
			}
			reportObjectModel.LookupsImpl = new LookupsImpl();
			if (reportDef.HasLookups)
			{
				this.m_processingContext.RuntimeInitializeLookups(reportDef);
			}
			ReportItemsImpl reportItemsImpl = new ReportItemsImpl(false);
			foreach (ReportSection reportSection in this.m_romReport.ReportSections)
			{
				ReportSection sectionDef = reportSection.SectionDef;
				reportSection.BodyItemsForHeadFoot = new ReportItemsImpl(false);
				reportSection.PageSectionItemsForHeadFoot = new ReportItemsImpl(false);
				reportObjectModel.ReportItemsImpl = reportSection.BodyItemsForHeadFoot;
				this.m_processingContext.RuntimeInitializeTextboxObjs(sectionDef.ReportItems, false);
				reportObjectModel.ReportItemsImpl = reportSection.PageSectionItemsForHeadFoot;
				Page page = sectionDef.Page;
				if (page.PageHeader != null)
				{
					if (this.m_processingContext.ReportRuntime.ReportExprHost != null)
					{
						page.PageHeader.SetExprHost(this.m_processingContext.ReportRuntime.ReportExprHost, reportObjectModel);
					}
					this.m_processingContext.RuntimeInitializeReportItemObjs(page.PageHeader.ReportItems, false);
					this.m_processingContext.RuntimeInitializeTextboxObjs(page.PageHeader.ReportItems, true);
				}
				if (page.PageFooter != null)
				{
					if (this.m_processingContext.ReportRuntime.ReportExprHost != null)
					{
						page.PageFooter.SetExprHost(this.m_processingContext.ReportRuntime.ReportExprHost, reportObjectModel);
					}
					this.m_processingContext.RuntimeInitializeReportItemObjs(page.PageFooter.ReportItems, false);
					this.m_processingContext.RuntimeInitializeTextboxObjs(page.PageFooter.ReportItems, true);
				}
				reportItemsImpl.AddAll(reportSection.BodyItemsForHeadFoot);
				reportItemsImpl.AddAll(reportSection.PageSectionItemsForHeadFoot);
			}
			reportObjectModel.ReportItemsImpl = reportItemsImpl;
			reportObjectModel.AggregatesImpl = new AggregatesImpl(this.m_processingContext);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0006CF14 File Offset: 0x0006B114
		private void PageInit(ReportSection section)
		{
			ObjectModelImpl reportObjectModel = this.m_processingContext.ReportObjectModel;
			AggregatesImpl aggregatesImpl = reportObjectModel.AggregatesImpl;
			Global.Tracer.Assert(section.BodyItemsForHeadFoot != null, "Missing cached BodyItemsForHeadFoot collection");
			Global.Tracer.Assert(section.PageSectionItemsForHeadFoot != null, "Missing cached PageSectionItemsForHeadFoot collection");
			section.BodyItemsForHeadFoot.ResetAll(default(Microsoft.ReportingServices.RdlExpressions.VariantResult));
			section.PageSectionItemsForHeadFoot.ResetAll();
			reportObjectModel.GlobalsImpl.SetPageNumbers(this.m_currentPageNumber, this.m_totalPages, this.m_currentOverallPageNumber, this.m_overallTotalPages);
			reportObjectModel.GlobalsImpl.SetPageName(this.m_pageName);
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDef = this.m_romReport.ReportDef;
			ReportSection sectionDef = section.SectionDef;
			Page page = sectionDef.Page;
			section.PageAggregatesOverReportItems = new Dictionary<string, AggregatesImpl>();
			this.m_processingContext.ReportObjectModel.ReportItemsImpl.SpecialMode = true;
			if (page.PageAggregates != null)
			{
				for (int i = 0; i < page.PageAggregates.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo = page.PageAggregates[i];
					aggregatesImpl.Remove(dataAggregateInfo);
					dataAggregateInfo.ExprHostInitialized = false;
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(dataAggregateInfo, this.m_processingContext);
					object[] array;
					DataFieldStatus dataFieldStatus;
					dataAggregateObj.EvaluateParameters(out array, out dataFieldStatus);
					string specialModeIndex = reportObjectModel.ReportItemsImpl.GetSpecialModeIndex();
					if (specialModeIndex == null)
					{
						aggregatesImpl.Add(dataAggregateObj);
					}
					else
					{
						AggregatesImpl aggregatesImpl2;
						if (!section.PageAggregatesOverReportItems.TryGetValue(specialModeIndex, out aggregatesImpl2))
						{
							aggregatesImpl2 = new AggregatesImpl(this.m_processingContext);
							section.PageAggregatesOverReportItems.Add(specialModeIndex, aggregatesImpl2);
						}
						aggregatesImpl2.Add(dataAggregateObj);
						this.m_reportItemToReportSection[specialModeIndex] = section;
					}
					dataAggregateObj.Init();
				}
			}
			reportObjectModel.ReportItemsImpl.SpecialMode = false;
			Microsoft.ReportingServices.ReportIntermediateFormat.PageSection pageSection = null;
			IReportScopeInstance reportScopeInstance = null;
			if (sectionDef.Page.PageHeader != null)
			{
				pageSection = sectionDef.Page.PageHeader;
				reportScopeInstance = section.Page.PageHeader.Instance.ReportScopeInstance;
				section.Page.PageHeader.SetNewContext();
			}
			if (sectionDef.Page.PageFooter != null)
			{
				pageSection = sectionDef.Page.PageFooter;
				reportScopeInstance = section.Page.PageFooter.Instance.ReportScopeInstance;
				section.Page.PageFooter.SetNewContext();
			}
			if (sectionDef != null)
			{
				this.m_processingContext.SetupContext(pageSection, reportScopeInstance);
			}
		}

		// Token: 0x04000D81 RID: 3457
		private OnDemandProcessingContext m_processingContext;

		// Token: 0x04000D82 RID: 3458
		private Dictionary<string, ReportSection> m_reportItemToReportSection = new Dictionary<string, ReportSection>();
	}
}
