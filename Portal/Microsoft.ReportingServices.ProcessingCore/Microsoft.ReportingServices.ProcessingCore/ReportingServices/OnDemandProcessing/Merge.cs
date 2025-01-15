using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x0200082A RID: 2090
	internal sealed class Merge
	{
		// Token: 0x0600755D RID: 30045 RVA: 0x001E6508 File Offset: 0x001E4708
		internal Merge(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandProcessingContext odpContext)
		{
			this.m_report = report;
			this.m_odpContext = odpContext;
			this.m_retrievalManager = new RetrievalManager(report, odpContext);
		}

		// Token: 0x0600755E RID: 30046 RVA: 0x001E652C File Offset: 0x001E472C
		internal void Init(bool includeParameters, bool parametersOnly)
		{
			if (this.m_odpContext.ReportRuntime == null)
			{
				this.m_odpContext.ReportObjectModel.Initialize(this.m_report, this.m_odpContext.CurrentReportInstance);
				this.m_odpContext.ReportRuntime = new Microsoft.ReportingServices.RdlExpressions.ReportRuntime(this.m_report.ObjectType, this.m_odpContext.ReportObjectModel, this.m_odpContext.ErrorContext);
			}
			if (!this.m_initialized && !this.m_odpContext.InitializedRuntime)
			{
				this.m_initialized = true;
				this.m_odpContext.ReportRuntime.LoadCompiledCode(this.m_report, includeParameters, parametersOnly, this.m_odpContext.ReportObjectModel, this.m_odpContext.ReportRuntimeSetup);
				if (this.m_odpContext.ReportRuntime.ReportExprHost != null)
				{
					this.m_report.SetExprHost(this.m_odpContext.ReportRuntime.ReportExprHost, this.m_odpContext.ReportObjectModel);
				}
			}
		}

		// Token: 0x0600755F RID: 30047 RVA: 0x001E661C File Offset: 0x001E481C
		internal void Init(ParameterInfoCollection parameters)
		{
			if (this.m_odpContext.ReportRuntime == null)
			{
				this.Init(false, false);
			}
			this.m_odpContext.ReportObjectModel.Initialize(parameters);
			this.m_odpContext.ReportRuntime.CustomCodeOnInit(this.m_odpContext.ReportDefinition);
		}

		// Token: 0x06007560 RID: 30048 RVA: 0x001E666C File Offset: 0x001E486C
		internal void EvaluateReportLanguage(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, string snapshotLanguage)
		{
			CultureInfo cultureInfo = null;
			if (snapshotLanguage != null)
			{
				this.m_reportLanguage = snapshotLanguage;
				cultureInfo = Merge.GetSpecificCultureInfoFromLanguage(snapshotLanguage, this.m_odpContext.ErrorContext);
			}
			else if (this.m_report.Language != null)
			{
				if (this.m_report.Language.Type != Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
				{
					OnDemandProcessingContext odpContext = this.m_odpContext;
					uint languageInstanceId = odpContext.LanguageInstanceId;
					odpContext.LanguageInstanceId = languageInstanceId + 1U;
					this.m_reportLanguage = this.m_odpContext.ReportRuntime.EvaluateReportLanguageExpression(this.m_report, out cultureInfo);
				}
				else
				{
					cultureInfo = Merge.GetSpecificCultureInfoFromLanguage(this.m_report.Language.StringValue, this.m_odpContext.ErrorContext);
				}
			}
			if (cultureInfo == null && !this.m_odpContext.InSubreport)
			{
				if (this.m_odpContext.UseUserLanguageForProcessing && this.m_odpContext.UserLanguage != null)
				{
					cultureInfo = this.m_odpContext.UserLanguage;
				}
				else
				{
					cultureInfo = Localization.DefaultReportServerSpecificCulture;
				}
			}
			if (cultureInfo != null)
			{
				Thread.CurrentThread.CurrentCulture = cultureInfo;
				reportInstance.Language = cultureInfo.ToString();
				this.m_odpContext.ThreadCulture = cultureInfo;
			}
		}

		// Token: 0x06007561 RID: 30049 RVA: 0x001E6774 File Offset: 0x001E4974
		private static CultureInfo GetSpecificCultureInfoFromLanguage(string language, ErrorContext errorContext)
		{
			CultureInfo cultureInfo = null;
			try
			{
				cultureInfo = new CultureInfo(language, false);
				if (cultureInfo.IsNeutralCulture)
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(language);
					cultureInfo = new CultureInfo(cultureInfo.Name, false);
				}
			}
			catch (Exception ex)
			{
				errorContext.Register(ProcessingErrorCode.rsInvalidLanguage, Severity.Warning, ObjectType.Report, null, "Language", new string[] { ex.Message });
			}
			return cultureInfo;
		}

		// Token: 0x06007562 RID: 30050 RVA: 0x001E67E0 File Offset: 0x001E49E0
		internal void FetchData(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance, bool mergeTransaction)
		{
			if (this.m_odpContext.ProcessWithCachedData)
			{
				if (reportInstance.IsMissingExpectedDataChunk(this.m_odpContext))
				{
					throw new ReportProcessing.DataCacheUnavailableException();
				}
			}
			else if (!this.m_odpContext.SnapshotProcessing)
			{
				if (this.m_odpContext.InSubreport)
				{
					this.m_odpContext.CreateAndSetupDataExtensionFunction.DataSetRetrieveForReportInstance(this.m_odpContext.ReportContext, this.m_parameters);
				}
				if (!this.m_retrievalManager.PrefetchData(reportInstance, this.m_parameters, mergeTransaction))
				{
					throw new ProcessingAbortedException();
				}
				reportInstance.NoRows = this.m_retrievalManager.NoRows;
			}
		}

		// Token: 0x06007563 RID: 30051 RVA: 0x001E6878 File Offset: 0x001E4A78
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance PrepareReportInstance(IReportInstanceContainer reportInstanceContainer)
		{
			IReference<Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance> reference;
			if (reportInstanceContainer.ReportInstance == null || reportInstanceContainer.ReportInstance.Value() == null)
			{
				reference = Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance.CreateInstance(reportInstanceContainer, this.m_odpContext, this.m_report, this.m_parameters);
			}
			else
			{
				reference = reportInstanceContainer.ReportInstance;
				reference.Value().InitializeFromSnapshot(this.m_odpContext);
			}
			return reference.Value();
		}

		// Token: 0x06007564 RID: 30052 RVA: 0x001E68D4 File Offset: 0x001E4AD4
		internal void SetupReport(Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance)
		{
			this.m_odpContext.CurrentReportInstance = reportInstance;
			if (!this.m_odpContext.InitializedRuntime)
			{
				this.m_odpContext.InitializedRuntime = true;
				List<ReportSection> reportSections = this.m_report.ReportSections;
				if (reportSections != null)
				{
					foreach (ReportSection reportSection in reportSections)
					{
						this.m_odpContext.RuntimeInitializeReportItemObjs(reportSection.ReportItems, true);
						this.m_odpContext.RuntimeInitializeTextboxObjs(reportSection.ReportItems, true);
					}
				}
				if (this.m_report.HasVariables)
				{
					this.m_odpContext.RuntimeInitializeVariables(this.m_report);
				}
				if (this.m_report.HasLookups)
				{
					this.m_odpContext.RuntimeInitializeLookups(this.m_report);
				}
				this.m_report.RegisterDataSetScopedAggregates(this.m_odpContext);
			}
		}

		// Token: 0x06007565 RID: 30053 RVA: 0x001E69C4 File Offset: 0x001E4BC4
		internal bool InitAndSetupSubReport(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport)
		{
			IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> currentSubReportInstance = subReport.CurrentSubReportInstance;
			bool flag = this.InitSubReport(subReport, currentSubReportInstance);
			if (flag)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance = currentSubReportInstance.Value().ReportInstance.Value();
				this.m_odpContext.SetupEnvironment(reportInstance);
				this.m_odpContext.ReportObjectModel.UserImpl.UpdateUserProfileLocationWithoutLocking(UserProfileState.OnDemandExpressions);
				this.m_odpContext.IsUnrestrictedRenderFormatReferenceMode = true;
			}
			currentSubReportInstance.Value().Initialized = true;
			return flag;
		}

		// Token: 0x06007566 RID: 30054 RVA: 0x001E6A30 File Offset: 0x001E4C30
		private bool InitSubReport(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, IReference<Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance> subReportInstanceRef)
		{
			bool flag = true;
			Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance = subReportInstanceRef.Value();
			if (this.m_odpContext.SnapshotProcessing && subReportInstance.ProcessedWithError)
			{
				return false;
			}
			try
			{
				if (!this.m_odpContext.SnapshotProcessing)
				{
					subReportInstance.RetrievalStatus = subReport.RetrievalStatus;
				}
				if (subReportInstance.RetrievalStatus == Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DefinitionRetrieveFailed)
				{
					subReportInstance.ProcessedWithError = true;
					return false;
				}
				if (this.m_odpContext.CurrentReportInstance == null && subReport.Report != null)
				{
					if (!this.m_odpContext.SnapshotProcessing)
					{
						subReport.OdpSubReportInfo.UserSortDataSetGlobalId = this.m_odpContext.ParentContext.UserSortFilterContext.DataSetGlobalId;
					}
					this.m_odpContext.UserSortFilterContext.UpdateContextForFirstSubreportInstance(this.m_odpContext.ParentContext.UserSortFilterContext);
				}
				if (!this.m_odpContext.SnapshotProcessing || this.m_odpContext.ReprocessSnapshot)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance = this.PrepareReportInstance(subReportInstance);
					this.m_odpContext.CurrentReportInstance = reportInstance;
					this.Init(true, false);
					subReportInstance.InstanceUniqueName = this.m_odpContext.CreateUniqueID().ToString(CultureInfo.InvariantCulture);
					this.m_odpContext.SetSubReportContext(subReportInstance, false);
					this.SetupReport(reportInstance);
					this.m_parameters = OnDemandProcessingContext.EvaluateSubReportParameters(this.m_odpContext.ParentContext, subReport);
					bool flag2;
					if (!this.m_odpContext.SnapshotProcessing && !this.m_odpContext.ProcessWithCachedData)
					{
						try
						{
							this.m_odpContext.ProcessReportParameters = true;
							this.m_odpContext.ReportObjectModel.ParametersImpl.Clear();
							ReportProcessing.ProcessReportParameters(subReport.Report, this.m_odpContext, this.m_parameters);
						}
						finally
						{
							this.m_odpContext.ProcessReportParameters = false;
						}
						if (!this.m_parameters.ValuesAreValid())
						{
							subReportInstance.RetrievalStatus = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.ParametersNotSpecified;
							throw new ReportProcessingException(ErrorCode.rsParametersNotSpecified);
						}
						this.m_odpContext.ReportParameters = this.m_parameters;
					}
					else if (!this.m_parameters.ValuesAreValid(out flag2) && flag2)
					{
						subReportInstance.RetrievalStatus = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.ParametersNotSpecified;
						throw new ReportProcessingException(ErrorCode.rsParametersNotSpecified);
					}
					this.Init(this.m_parameters);
					subReportInstance.Parameters = new ParametersImpl(this.m_odpContext.ReportObjectModel.ParametersImpl);
					this.m_odpContext.SetSubReportNameModifierAndParameters(subReportInstance, !this.m_odpContext.SnapshotProcessing);
					if (this.m_odpContext.ReprocessSnapshot)
					{
						reportInstance.InitializeFromSnapshot(this.m_odpContext);
					}
					this.EvaluateReportLanguage(reportInstance, null);
					subReportInstance.ThreadCulture = this.m_odpContext.ThreadCulture;
					if (!this.m_odpContext.SnapshotProcessing || this.m_odpContext.FoundExistingSubReportInstance)
					{
						flag = this.FetchSubReportData(subReport, subReportInstance);
						if (flag)
						{
							subReportInstance.RetrievalStatus = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DataRetrieved;
						}
						else
						{
							subReportInstance.RetrievalStatus = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DataRetrieveFailed;
							subReportInstance.ProcessedWithError = true;
						}
					}
					else
					{
						subReportInstance.RetrievalStatus = Microsoft.ReportingServices.ReportIntermediateFormat.SubReport.Status.DataNotRetrieved;
						subReportInstance.ProcessedWithError = true;
					}
					this.m_odpContext.ReportParameters = null;
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance2 = subReportInstance.ReportInstance.Value();
					this.m_odpContext.CurrentReportInstance = reportInstance2;
					this.m_odpContext.LoadExistingSubReportDataChunkNameModifier(subReportInstance);
					reportInstance2.InitializeFromSnapshot(this.m_odpContext);
					this.Init(true, false);
					this.m_odpContext.ThreadCulture = subReportInstance.ThreadCulture;
					this.SetupReport(reportInstance2);
					this.m_odpContext.SetSubReportContext(subReportInstance, true);
					this.m_odpContext.ReportRuntime.CustomCodeOnInit(this.m_odpContext.ReportDefinition);
					this.m_odpContext.OdpMetadata.SetUpdatedVariableValues(this.m_odpContext, reportInstance2);
				}
			}
			catch (ReportProcessing.DataCacheUnavailableException)
			{
				throw;
			}
			catch (Exception ex)
			{
				flag = false;
				subReportInstance.ProcessedWithError = true;
				if (subReportInstance.ReportInstance != null)
				{
					subReportInstance.ReportInstance.Value().NoRows = false;
				}
				if (ex is RSException)
				{
					this.m_odpContext.ErrorContext.Register((RSException)ex, subReport.ObjectType);
				}
			}
			return flag;
		}

		// Token: 0x06007567 RID: 30055 RVA: 0x001E6E38 File Offset: 0x001E5038
		internal bool FetchSubReportData(Microsoft.ReportingServices.ReportIntermediateFormat.SubReport subReport, Microsoft.ReportingServices.ReportIntermediateFormat.SubReportInstance subReportInstance)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportInstance reportInstance = subReportInstance.ReportInstance.Value();
			reportInstance.ResetReportVariables(subReport.OdpContext);
			bool flag;
			try
			{
				this.FetchData(reportInstance, subReport.MergeTransactions);
				if (subReport.OdpContext.ReprocessSnapshot && reportInstance.IsMissingExpectedDataChunk(subReport.OdpContext))
				{
					flag = false;
				}
				else
				{
					if (subReport.OdpContext.ReprocessSnapshot && !subReport.InDataRegion)
					{
						Merge.PreProcessTablixes(subReport.Report, subReport.OdpContext, false);
					}
					flag = true;
				}
			}
			catch (ProcessingAbortedException)
			{
				flag = false;
			}
			if (flag)
			{
				reportInstance.CalculateAndStoreReportVariables(subReport.OdpContext);
			}
			return flag;
		}

		// Token: 0x06007568 RID: 30056 RVA: 0x001E6EDC File Offset: 0x001E50DC
		internal static void TablixDataProcessing(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet specificDataSetOnly)
		{
			bool flag = false;
			while (!flag)
			{
				int num = specificDataSetOnly.IndexInCollection;
				int num2;
				bool[] array = odpContext.GenerateDataSetExclusionList(out num2);
				num = odpContext.ReportDefinition.CalculateDatasetRootIndex(num, array, num2);
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = odpContext.ReportDefinition.MappingDataSetIndexToDataSet[num];
				FullAtomicDataPipelineManager fullAtomicDataPipelineManager = new FullAtomicDataPipelineManager(odpContext, dataSet);
				fullAtomicDataPipelineManager.StartProcessing();
				fullAtomicDataPipelineManager.StopProcessing();
				flag = num == specificDataSetOnly.IndexInCollection;
			}
		}

		// Token: 0x06007569 RID: 30057 RVA: 0x001E6F40 File Offset: 0x001E5140
		internal static bool PreProcessTablixes(Microsoft.ReportingServices.ReportIntermediateFormat.Report report, OnDemandProcessingContext odpContext, bool onlyWithSubReports)
		{
			bool flag = false;
			foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource in report.DataSources)
			{
				if (dataSource.DataSets != null)
				{
					foreach (Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet in dataSource.DataSets)
					{
						if (odpContext.CurrentReportInstance.GetDataSetInstance(dataSet, odpContext) != null && dataSet.DataRegions.Count != 0 && !odpContext.IsTablixProcessingComplete(dataSet.IndexInCollection) && (!onlyWithSubReports || dataSet.HasSubReports || (odpContext.InSubreport && odpContext.HasUserSortFilter)))
						{
							flag = true;
							Merge.TablixDataProcessing(odpContext, dataSet);
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04003B8E RID: 15246
		private Microsoft.ReportingServices.ReportIntermediateFormat.Report m_report;

		// Token: 0x04003B8F RID: 15247
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003B90 RID: 15248
		private RetrievalManager m_retrievalManager;

		// Token: 0x04003B91 RID: 15249
		private string m_reportLanguage;

		// Token: 0x04003B92 RID: 15250
		private bool m_initialized;

		// Token: 0x04003B93 RID: 15251
		private ParameterInfoCollection m_parameters;
	}
}
