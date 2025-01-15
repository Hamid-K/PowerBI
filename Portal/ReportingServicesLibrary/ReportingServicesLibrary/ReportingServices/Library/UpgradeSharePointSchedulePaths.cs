using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025F RID: 607
	internal class UpgradeSharePointSchedulePaths : UpgradeTask
	{
		// Token: 0x06001609 RID: 5641 RVA: 0x00057FBC File Offset: 0x000561BC
		public UpgradeSharePointSchedulePaths(UpgradePollWorker pollWorker)
			: base(pollWorker)
		{
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0005808B File Offset: 0x0005628B
		public override string Name
		{
			get
			{
				return "UpgradeSharePointSchedulePaths";
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00058092 File Offset: 0x00056292
		public override void PerformUpgrade(string status)
		{
			this.SetFinished();
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0005809A File Offset: 0x0005629A
		private void SetFinished()
		{
			this.m_finished = true;
			base.SetUpgradeItemStatus("True");
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x000580AE File Offset: 0x000562AE
		public override bool Finished
		{
			get
			{
				return this.m_finished;
			}
		}

		// Token: 0x04000808 RID: 2056
		private const string GoodStatus = "True";

		// Token: 0x04000809 RID: 2057
		private bool m_finished;
	}
}
