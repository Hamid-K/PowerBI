using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C2 RID: 706
	internal sealed class DocMapNavigator : SnapshotUpdatingEvent<string, int>
	{
		// Token: 0x0600194F RID: 6479 RVA: 0x00066480 File Offset: 0x00064680
		public DocMapNavigator(ClientRequest session, RSService service, string userName, CatalogItemContext reportContext)
			: base(session, service, reportContext)
		{
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006648C File Offset: 0x0006468C
		protected override int RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc)
		{
			OnDemandProcessingResult onDemandProcessingResult;
			int num = base.ProcessingEngine.ProcessDocumentMapNavigationEvent(base.EventParameter, base.Session.SessionReport.EventInfo, pc, out onDemandProcessingResult);
			base.ProcessingResult = onDemandProcessingResult;
			return num;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x000664C4 File Offset: 0x000646C4
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.DocumentMapId = base.EventParameter;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001952 RID: 6482 RVA: 0x0002A6CA File Offset: 0x000288CA
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.DocumentMapNavigation;
			}
		}
	}
}
