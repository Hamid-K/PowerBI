using System;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025E RID: 606
	internal class UpgradeSharePointCatalogPaths : UpgradeTask
	{
		// Token: 0x06001604 RID: 5636 RVA: 0x00057FBC File Offset: 0x000561BC
		public UpgradeSharePointCatalogPaths(UpgradePollWorker pollWorker)
			: base(pollWorker)
		{
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00058060 File Offset: 0x00056260
		public override string Name
		{
			get
			{
				return "UpgradeSharePointCatalogPaths";
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00058067 File Offset: 0x00056267
		public override void PerformUpgrade(string status)
		{
			this.SetFinished();
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0005806F File Offset: 0x0005626F
		private void SetFinished()
		{
			this.m_finished = true;
			base.SetUpgradeItemStatus("True");
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x00058083 File Offset: 0x00056283
		public override bool Finished
		{
			get
			{
				return this.m_finished;
			}
		}

		// Token: 0x04000806 RID: 2054
		private const string GoodStatus = "True";

		// Token: 0x04000807 RID: 2055
		private bool m_finished;
	}
}
