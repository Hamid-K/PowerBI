using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002C4 RID: 708
	internal sealed class FindStringNavigator : SnapshotUpdatingEvent<FindStringNavigator.Parameters, int>
	{
		// Token: 0x06001957 RID: 6487 RVA: 0x00066556 File Offset: 0x00064756
		public FindStringNavigator(ClientRequest session, RSService service, string userName, CatalogItemContext reportContext)
			: base(session, service, reportContext)
		{
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00066564 File Offset: 0x00064764
		protected override int RunEvent(Microsoft.ReportingServices.ReportProcessing.ProcessingContext pc)
		{
			OnDemandProcessingResult onDemandProcessingResult;
			int num = base.ProcessingEngine.ProcessFindStringEvent(base.EventParameter.StartPage, base.EventParameter.EndPage, base.EventParameter.FindValue, base.Session.SessionReport.EventInfo, pc, out onDemandProcessingResult);
			base.ProcessingResult = onDemandProcessingResult;
			return num;
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001959 RID: 6489 RVA: 0x000665B7 File Offset: 0x000647B7
		internal override ReportEventType EventType
		{
			get
			{
				return ReportEventType.FindString;
			}
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000665BC File Offset: 0x000647BC
		internal override void AddExecutionInfo(ReportExecutionInfo execInfo)
		{
			base.AddExecutionInfo(execInfo);
			if (base.WriteEventParameters)
			{
				execInfo.AdditionalInfo.StartPage = ReportProcessingEventBase.InvariantFormatNumber(base.EventParameter.StartPage);
				execInfo.AdditionalInfo.EndPage = ReportProcessingEventBase.InvariantFormatNumber(base.EventParameter.EndPage);
				execInfo.AdditionalInfo.FindValue = base.EventParameter.FindValue;
			}
		}

		// Token: 0x020004DB RID: 1243
		public sealed class Parameters
		{
			// Token: 0x06002483 RID: 9347 RVA: 0x000862E2 File Offset: 0x000844E2
			public Parameters(int startPage, int endPage, string findValue)
			{
				this.m_startPage = startPage;
				this.m_endPage = endPage;
				this.m_findValue = findValue;
			}

			// Token: 0x17000AA6 RID: 2726
			// (get) Token: 0x06002484 RID: 9348 RVA: 0x000862FF File Offset: 0x000844FF
			public int StartPage
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_startPage;
				}
			}

			// Token: 0x17000AA7 RID: 2727
			// (get) Token: 0x06002485 RID: 9349 RVA: 0x00086307 File Offset: 0x00084507
			public int EndPage
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_endPage;
				}
			}

			// Token: 0x17000AA8 RID: 2728
			// (get) Token: 0x06002486 RID: 9350 RVA: 0x0008630F File Offset: 0x0008450F
			public string FindValue
			{
				[DebuggerStepThrough]
				get
				{
					return this.m_findValue;
				}
			}

			// Token: 0x0400112E RID: 4398
			private readonly int m_startPage;

			// Token: 0x0400112F RID: 4399
			private readonly int m_endPage;

			// Token: 0x04001130 RID: 4400
			private readonly string m_findValue;
		}
	}
}
