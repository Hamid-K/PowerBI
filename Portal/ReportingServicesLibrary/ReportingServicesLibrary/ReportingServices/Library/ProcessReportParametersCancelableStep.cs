using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B8 RID: 696
	internal sealed class ProcessReportParametersCancelableStep : CancelableLibraryStep
	{
		// Token: 0x0600192B RID: 6443 RVA: 0x0006513C File Offset: 0x0006333C
		internal ProcessReportParametersCancelableStep(RSService rs, CatalogItemContext reportContext, Guid reportID, Guid linkID, DateTime historyDate, IStoredParameterSource storedParamsSource, NameValueCollection values, RuntimeDataSourceInfoCollection allDataSources, RuntimeDataSetInfoCollection allDataSets, JobType jobType)
			: base(UrlFriendlyUIDGenerator.Create(), reportContext.OriginalItemPath, JobActionEnum.Render, jobType, rs.UserContext)
		{
			this.m_rs = rs;
			this.m_reportContext = reportContext;
			this.m_reportID = reportID;
			this.m_linkID = linkID;
			this.m_snapshotExecDate = historyDate;
			this.m_storedParamSource = storedParamsSource;
			this.m_values = values;
			this.m_allDataSources = allDataSources;
			this.m_allDataSets = allDataSets;
		}

		// Token: 0x0600192C RID: 6444 RVA: 0x000651A8 File Offset: 0x000633A8
		protected override void Execute()
		{
			Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.Source = ExecutionLogExecType.Live;
			this.m_resultingParameters = this.m_rs.InternalGetReportParametersForRendering(this.m_reportContext, this.m_reportID, this.m_linkID, this.m_snapshotExecDate, this.m_storedParamSource, this.m_values, this.m_allDataSources, this.m_allDataSets);
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00065206 File Offset: 0x00063406
		internal ParameterInfoCollection ResultParameters
		{
			get
			{
				return this.m_resultingParameters;
			}
		}

		// Token: 0x0400091E RID: 2334
		private readonly RSService m_rs;

		// Token: 0x0400091F RID: 2335
		private readonly CatalogItemContext m_reportContext;

		// Token: 0x04000920 RID: 2336
		private readonly Guid m_reportID;

		// Token: 0x04000921 RID: 2337
		private readonly Guid m_linkID;

		// Token: 0x04000922 RID: 2338
		private readonly DateTime m_snapshotExecDate;

		// Token: 0x04000923 RID: 2339
		private readonly IStoredParameterSource m_storedParamSource;

		// Token: 0x04000924 RID: 2340
		private readonly NameValueCollection m_values;

		// Token: 0x04000925 RID: 2341
		private readonly RuntimeDataSourceInfoCollection m_allDataSources;

		// Token: 0x04000926 RID: 2342
		private readonly RuntimeDataSetInfoCollection m_allDataSets;

		// Token: 0x04000927 RID: 2343
		private ParameterInfoCollection m_resultingParameters;
	}
}
