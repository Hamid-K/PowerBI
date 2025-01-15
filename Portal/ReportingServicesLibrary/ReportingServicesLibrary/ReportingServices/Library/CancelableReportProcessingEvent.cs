using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C8 RID: 712
	internal class CancelableReportProcessingEvent : CancelableLibraryStep
	{
		// Token: 0x06001972 RID: 6514 RVA: 0x00066AB8 File Offset: 0x00064CB8
		internal CancelableReportProcessingEvent(ExternalItemPath requestPath, RSService service, ReportProcessingEventBase executingEvent)
			: base(UrlFriendlyUIDGenerator.Create(), requestPath, JobActionEnum.Render, Microsoft.ReportingServices.Diagnostics.JobType.UserJobType, service.UserContext)
		{
			RSTrace.CatalogTrace.Assert(executingEvent != null, "executingEvent");
			this.m_event = executingEvent;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x00066AEC File Offset: 0x00064CEC
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x00066AF4 File Offset: 0x00064CF4
		internal CancelableReportProcessingEvent.CancelableMethod Method
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_method;
			}
			[DebuggerStepThrough]
			set
			{
				this.m_method = value;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x00066B00 File Offset: 0x00064D00
		protected override void Execute()
		{
			RSTrace.CatalogTrace.Assert(this.m_method != null, "m_method");
			RunningJobContext jobContext = ProcessingContext.JobContext;
			RSTrace.CatalogTrace.Assert(jobContext != null, "jobContext");
			ReportExecutionInfo executionInfo = jobContext.ExecutionInfo;
			RSTrace.CatalogTrace.Assert(executionInfo != null, "executionInfo");
			this.Event.AddExecutionInfo(executionInfo);
			this.Method();
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001976 RID: 6518 RVA: 0x00066B6F File Offset: 0x00064D6F
		private ReportProcessingEventBase Event
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_event;
			}
		}

		// Token: 0x0400094B RID: 2379
		private readonly ReportProcessingEventBase m_event;

		// Token: 0x0400094C RID: 2380
		private CancelableReportProcessingEvent.CancelableMethod m_method;

		// Token: 0x020004DD RID: 1245
		// (Invoke) Token: 0x0600248A RID: 9354
		internal delegate void CancelableMethod();
	}
}
