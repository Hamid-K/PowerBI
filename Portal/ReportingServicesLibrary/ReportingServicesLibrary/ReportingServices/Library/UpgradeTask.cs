using System;
using System.Data;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000265 RID: 613
	internal abstract class UpgradeTask : Storage
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x000586F5 File Offset: 0x000568F5
		public UpgradeTask(UpgradePollWorker pollWorker)
		{
			this.m_pollWorker = pollWorker;
			this.m_pollWorker.AddTask(this);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00058710 File Offset: 0x00056910
		public void Upgrade(string status)
		{
			this.ConnectionManager = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
			this.ConnectionManager.WillDisconnectStorage();
			try
			{
				this.PerformUpgrade(status);
			}
			finally
			{
				base.DisconnectStorage();
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001626 RID: 5670
		public abstract string Name { get; }

		// Token: 0x06001627 RID: 5671
		public abstract void PerformUpgrade(string status);

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001628 RID: 5672
		public abstract bool Finished { get; }

		// Token: 0x06001629 RID: 5673 RVA: 0x0005875C File Offset: 0x0005695C
		protected void SetUpgradeItemStatus(string status)
		{
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("SetUpgradeItemStatus", null))
			{
				instrumentedSqlCommand.Parameters.AddWithValue("@ItemName", this.Name);
				instrumentedSqlCommand.Parameters.AddWithValue("@Status", status);
				instrumentedSqlCommand.ExecuteNonQuery();
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600162A RID: 5674 RVA: 0x000587C4 File Offset: 0x000569C4
		protected bool ContinueWorking
		{
			get
			{
				return this.m_pollWorker.ContinueWorking;
			}
		}

		// Token: 0x04000813 RID: 2067
		private UpgradePollWorker m_pollWorker;
	}
}
