using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C1 RID: 705
	internal sealed class ToggleAction : ReportProcessingEvent<string, bool>
	{
		// Token: 0x0600194B RID: 6475 RVA: 0x000663F0 File Offset: 0x000645F0
		public ToggleAction(ClientRequest session, RSService service)
			: base(session, service, null)
		{
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000663FC File Offset: 0x000645FC
		protected override bool Event()
		{
			bool flag = false;
			EventInformation eventInformation = null;
			bool flag2 = base.ProcessingEngine.ProcessToggleEvent(base.EventParameter, base.ReadOnlySessionSnapshot, base.Session.SessionReport.EventInfo, out eventInformation, out flag);
			if (flag)
			{
				base.Session.SessionReport.EventInfo = eventInformation;
				base.Session.SessionReport.Save(SessionReportItem.SaveAction.SaveSession, true);
			}
			return flag2;
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x0002B40C File Offset: 0x0002960C
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.Toggle;
			}
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0006645E File Offset: 0x0006465E
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.ToggleId = base.EventParameter;
			}
		}
	}
}
