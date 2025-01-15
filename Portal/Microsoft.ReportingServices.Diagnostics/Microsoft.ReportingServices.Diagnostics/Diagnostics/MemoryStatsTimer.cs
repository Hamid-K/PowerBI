using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200000F RID: 15
	internal sealed class MemoryStatsTimer : TimerActionBase
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002390 File Offset: 0x00000590
		public MemoryStatsTimer()
		{
			this.DoTimerAction();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000239E File Offset: 0x0000059E
		public override void DoTimerAction()
		{
			ResourceUtilities.UpdatePrivateMBytes();
		}
	}
}
