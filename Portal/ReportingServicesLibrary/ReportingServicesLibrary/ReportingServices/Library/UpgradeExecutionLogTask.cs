using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200025D RID: 605
	internal class UpgradeExecutionLogTask : UpgradeTask
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x00057FBC File Offset: 0x000561BC
		public UpgradeExecutionLogTask(UpgradePollWorker worker)
			: base(worker)
		{
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00057FC5 File Offset: 0x000561C5
		public override string Name
		{
			get
			{
				return "MigrateExecutionLog";
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00057FCC File Offset: 0x000561CC
		public override void PerformUpgrade(string status)
		{
			if (status != null && Localization.CatalogCultureCompare(status, "True") == 0)
			{
				this.m_finished = true;
				return;
			}
			using (InstrumentedSqlCommand instrumentedSqlCommand = this.NewStandardSqlCommand("MigrateExecutionLog", null))
			{
				SqlParameter sqlParameter = instrumentedSqlCommand.Parameters.Add("@updatedRow", SqlDbType.Int);
				sqlParameter.Direction = ParameterDirection.Output;
				instrumentedSqlCommand.ExecuteNonQuery();
				if ((int)sqlParameter.Value <= 0)
				{
					this.m_finished = true;
					base.SetUpgradeItemStatus("True");
				}
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00058058 File Offset: 0x00056258
		public override bool Finished
		{
			get
			{
				return this.m_finished;
			}
		}

		// Token: 0x04000804 RID: 2052
		private const string GoodStatus = "True";

		// Token: 0x04000805 RID: 2053
		private bool m_finished;
	}
}
