using System;
using System.Collections.Specialized;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002B7 RID: 695
	internal sealed class ProcessDataSetParametersCancelableStep : CancelableLibraryStep
	{
		// Token: 0x06001928 RID: 6440 RVA: 0x000650C0 File Offset: 0x000632C0
		internal ProcessDataSetParametersCancelableStep(ReportProcessing repProc, RSService service, ItemParameterDefinition parameterDefinition, NameValueCollection requestParameterValues, JobType jobType)
			: base(UrlFriendlyUIDGenerator.Create(), parameterDefinition.ItemContext.OriginalItemPath, JobActionEnum.Render, jobType, service.UserContext)
		{
			this.m_parameterDefinition = parameterDefinition;
			this.m_rs = service;
			this.m_repProc = repProc;
			this.m_requestValues = requestParameterValues;
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x000650FE File Offset: 0x000632FE
		protected override void Execute()
		{
			Microsoft.ReportingServices.Diagnostics.ProcessingContext.JobContext.ExecutionInfo.Source = ExecutionLogExecType.Live;
			this.m_resultingParameters = this.m_rs.InternalGetDataSetParameters(this.m_repProc, this.m_parameterDefinition, this.m_requestValues);
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600192A RID: 6442 RVA: 0x00065133 File Offset: 0x00063333
		public ParameterInfoCollection ResultParameters
		{
			get
			{
				return this.m_resultingParameters;
			}
		}

		// Token: 0x04000919 RID: 2329
		private readonly ReportProcessing m_repProc;

		// Token: 0x0400091A RID: 2330
		private readonly RSService m_rs;

		// Token: 0x0400091B RID: 2331
		private readonly ItemParameterDefinition m_parameterDefinition;

		// Token: 0x0400091C RID: 2332
		private readonly NameValueCollection m_requestValues;

		// Token: 0x0400091D RID: 2333
		private ParameterInfoCollection m_resultingParameters;
	}
}
