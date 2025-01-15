using System;
using System.Globalization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200063B RID: 1595
	public sealed class ReportProcessingContext : ProcessingContext
	{
		// Token: 0x06005756 RID: 22358 RVA: 0x0016F520 File Offset: 0x0016D720
		public ReportProcessingContext(ICatalogItemContext reportContext, string requestUserName, ParameterInfoCollection parameters, RuntimeDataSourceInfoCollection dataSources, RuntimeDataSetInfoCollection sharedDataSetReferences, ReportProcessing.OnDemandSubReportCallback subReportCallback, IGetResource getResourceFunction, IChunkFactory createChunkFactory, ReportProcessing.ExecutionType interactiveExecution, CultureInfo culture, UserProfileState allowUserProfileState, UserProfileState initialUserProfileState, IProcessingDataExtensionConnection createDataExtensionInstanceFunction, ReportRuntimeSetup reportRuntimeSetup, CreateAndRegisterStream createStreamCallback, bool isHistorySnapshot, IJobContext jobContext, IExtensionFactory extFactory, IDataProtection dataProtection, ISharedDataSet dataSetExecute)
			: base(reportContext, requestUserName, parameters, subReportCallback, getResourceFunction, createChunkFactory, interactiveExecution, culture, allowUserProfileState, initialUserProfileState, reportRuntimeSetup, createStreamCallback, isHistorySnapshot, jobContext, extFactory, dataProtection)
		{
			this.m_dataSources = dataSources;
			this.m_sharedDataSetReferences = sharedDataSetReferences;
			this.m_createDataExtensionInstanceFunction = createDataExtensionInstanceFunction;
			this.m_dataSetExecute = dataSetExecute;
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x06005757 RID: 22359 RVA: 0x0016F570 File Offset: 0x0016D770
		internal override bool EnableDataBackedParameters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x06005758 RID: 22360 RVA: 0x0016F573 File Offset: 0x0016D773
		internal override RuntimeDataSourceInfoCollection DataSources
		{
			get
			{
				return this.m_dataSources;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06005759 RID: 22361 RVA: 0x0016F57B File Offset: 0x0016D77B
		internal override RuntimeDataSetInfoCollection SharedDataSetReferences
		{
			get
			{
				return this.m_sharedDataSetReferences;
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x0600575A RID: 22362 RVA: 0x0016F583 File Offset: 0x0016D783
		internal IProcessingDataExtensionConnection CreateDataExtensionInstanceFunction
		{
			get
			{
				return this.m_createDataExtensionInstanceFunction;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x0600575B RID: 22363 RVA: 0x0016F58B File Offset: 0x0016D78B
		internal override bool CanShareDataSets
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x0600575C RID: 22364 RVA: 0x0016F58E File Offset: 0x0016D78E
		// (set) Token: 0x0600575D RID: 22365 RVA: 0x0016F596 File Offset: 0x0016D796
		internal ISharedDataSet DataSetExecute
		{
			get
			{
				return this.m_dataSetExecute;
			}
			set
			{
				this.m_dataSetExecute = value;
			}
		}

		// Token: 0x0600575E RID: 22366 RVA: 0x0016F5A0 File Offset: 0x0016D7A0
		internal override ReportProcessing.ProcessingContext CreateInternalProcessingContext(string chartName, Report report, ErrorContext errorContext, DateTime executionTime, UserProfileState allowUserProfileState, bool isHistorySnapshot, bool snapshotProcessing, bool processWithCachedData, ReportProcessing.GetReportChunk getChunkCallback, ReportProcessing.CreateReportChunk cacheDataCallback)
		{
			SubreportCallbackAdapter subreportCallbackAdapter = new SubreportCallbackAdapter(base.OnDemandSubReportCallback, errorContext);
			return new ReportProcessing.ReportProcessingContext(chartName, this.DataSources, base.RequestUserName, base.UserLanguage, new ReportProcessing.SubReportCallback(subreportCallbackAdapter.SubReportCallback), base.ReportContext, report, errorContext, base.CreateReportChunkCallback, base.GetResourceCallback, base.InteractiveExecution, executionTime, allowUserProfileState, isHistorySnapshot, snapshotProcessing, processWithCachedData, getChunkCallback, cacheDataCallback, this.CreateDataExtensionInstanceFunction, base.ReportRuntimeSetup, base.JobContext, base.ExtFactory, base.DataProtection);
		}

		// Token: 0x0600575F RID: 22367 RVA: 0x0016F624 File Offset: 0x0016D824
		internal override ReportProcessing.ProcessingContext ParametersInternalProcessingContext(ErrorContext errorContext, DateTime executionTimeStamp, bool isSnapshot)
		{
			return new ReportProcessing.ReportProcessingContext(null, this.DataSources, base.RequestUserName, base.UserLanguage, base.ReportContext, errorContext, base.InteractiveExecution, executionTimeStamp, base.AllowUserProfileState, isSnapshot, this.CreateDataExtensionInstanceFunction, base.ReportRuntimeSetup, base.JobContext, base.ExtFactory, base.DataProtection);
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x06005760 RID: 22368 RVA: 0x0016F67C File Offset: 0x0016D87C
		internal override IProcessingDataExtensionConnection CreateAndSetupDataExtensionFunction
		{
			get
			{
				return this.m_createDataExtensionInstanceFunction;
			}
		}

		// Token: 0x04002E2E RID: 11822
		private readonly RuntimeDataSourceInfoCollection m_dataSources;

		// Token: 0x04002E2F RID: 11823
		private readonly RuntimeDataSetInfoCollection m_sharedDataSetReferences;

		// Token: 0x04002E30 RID: 11824
		private readonly IProcessingDataExtensionConnection m_createDataExtensionInstanceFunction;

		// Token: 0x04002E31 RID: 11825
		private ISharedDataSet m_dataSetExecute;
	}
}
