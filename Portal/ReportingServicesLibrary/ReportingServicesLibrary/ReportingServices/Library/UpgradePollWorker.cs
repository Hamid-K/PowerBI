using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025C RID: 604
	internal sealed class UpgradePollWorker : PollWorker
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x00057D74 File Offset: 0x00055F74
		public override bool Poll
		{
			get
			{
				if (this.m_firstPoll)
				{
					this.m_firstPoll = false;
					return true;
				}
				using (List<UpgradeTask>.Enumerator enumerator = this.m_activeTasks.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.Finished)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x00057DE0 File Offset: 0x00055FE0
		public override InstrumentedSqlCommand PollCommand
		{
			get
			{
				InstrumentedSqlCommand instrumentedSqlCommand = InstrumentedSqlCommand.GetInstrumentedSqlCommand(new SqlCommand("GetUpgradeItems"));
				instrumentedSqlCommand.CommandType = CommandType.StoredProcedure;
				return instrumentedSqlCommand;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x00057DF8 File Offset: 0x00055FF8
		protected override string PollingTraceName
		{
			get
			{
				return "UpgradePolling";
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00057DFF File Offset: 0x00055FFF
		public new bool ContinueWorking
		{
			get
			{
				return base.ContinueWorking;
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00057E07 File Offset: 0x00056007
		public void AddTask(UpgradeTask task)
		{
			this.m_upgradeTasks.Add(task);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00057E18 File Offset: 0x00056018
		public override void ProcessData(IDataReader reader)
		{
			try
			{
				this.m_working = true;
				this.m_upgradeTasks.Clear();
				new UpgradeSecurityDescriptor(this);
				new UpgradeConnectionEncryption(this);
				new UpgradeExecutionLogTask(this);
				new UpgradeSharePointCatalogPaths(this);
				new UpgradeSharePointSchedulePaths(this);
				new EnableCommentsTask(this);
				this.m_activeTasks.Clear();
				while (this.ContinueWorking && reader.Read())
				{
					string @string = reader.GetString(0);
					string text = null;
					if (!reader.IsDBNull(1))
					{
						text = reader.GetString(1);
					}
					foreach (UpgradeTask upgradeTask in this.m_upgradeTasks)
					{
						if (upgradeTask.Name == @string)
						{
							this.m_activeTasks.Add(upgradeTask);
							try
							{
								if (RSTrace.CatalogTrace.TraceVerbose)
								{
									RSTrace.CatalogTrace.Trace("Attempting upgrade of {0}", new object[] { @string });
								}
								upgradeTask.Upgrade(text);
								break;
							}
							catch (Exception ex)
							{
								if (RSTrace.CatalogTrace.TraceError)
								{
									RSTrace.CatalogTrace.Trace("Error upgrading the items with name '{0}'", new object[] { upgradeTask.Name });
									RSTrace.CatalogTrace.Trace(ex.ToString());
								}
							}
						}
					}
				}
			}
			finally
			{
				this.m_working = false;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00057FB4 File Offset: 0x000561B4
		public override bool IsStillWorking
		{
			get
			{
				return this.m_working;
			}
		}

		// Token: 0x04000800 RID: 2048
		private bool m_firstPoll = true;

		// Token: 0x04000801 RID: 2049
		private bool m_working;

		// Token: 0x04000802 RID: 2050
		private UpgradeTasks m_upgradeTasks = new UpgradeTasks();

		// Token: 0x04000803 RID: 2051
		private UpgradeTasks m_activeTasks = new UpgradeTasks();

		// Token: 0x020004BC RID: 1212
		private enum UpgradeItemsProjection
		{
			// Token: 0x040010FB RID: 4347
			Item,
			// Token: 0x040010FC RID: 4348
			Status
		}
	}
}
