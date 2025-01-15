using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000261 RID: 609
	internal abstract class UpgradeMultipleItemsTask : UpgradeTask
	{
		// Token: 0x06001615 RID: 5653 RVA: 0x00057FBC File Offset: 0x000561BC
		public UpgradeMultipleItemsTask(UpgradePollWorker pollWorker)
			: base(pollWorker)
		{
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0005839C File Offset: 0x0005659C
		public override void PerformUpgrade(string status)
		{
			if (status != null && Localization.CatalogCultureCompare(status, "True") == 0)
			{
				return;
			}
			UpgradeMultipleItemsTask.ItemCollection itemsToUpdate = this.GetItemsToUpdate();
			this.m_finished = this.UpdateItems(itemsToUpdate);
			if (this.m_finished)
			{
				base.SetUpgradeItemStatus("True");
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x000583E1 File Offset: 0x000565E1
		public override bool Finished
		{
			get
			{
				return this.m_finished;
			}
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x000583EC File Offset: 0x000565EC
		private bool UpdateItems(UpgradeMultipleItemsTask.ItemCollection items)
		{
			bool flag = true;
			foreach (UpgradeMultipleItemsTask.ItemInfo itemInfo in items)
			{
				if (!base.ContinueWorking)
				{
					break;
				}
				RSTrace.CatalogTrace.Assert(this.ConnectionManager.ConnectionTransactionType == ConnectionTransactionType.AutoCommit);
				try
				{
					this.ConnectionManager.ConnectionTransactionType = ConnectionTransactionType.Explicit;
					this.ConnectionManager.BeginTransaction();
					itemInfo.Upgrade(this);
					this.ConnectionManager.CommitTransaction();
				}
				catch (Exception ex)
				{
					this.ConnectionManager.AbortTransaction();
					flag = false;
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace("Error on upgrade {0} for item: {1}", new object[] { this.Name, itemInfo.TraceIdentifier });
						RSTrace.CatalogTrace.Trace(ex.ToString());
					}
				}
				finally
				{
					this.ConnectionManager.ConnectionTransactionType = ConnectionTransactionType.AutoCommit;
				}
			}
			return flag;
		}

		// Token: 0x06001619 RID: 5657
		protected abstract UpgradeMultipleItemsTask.ItemCollection GetItemsToUpdate();

		// Token: 0x04000810 RID: 2064
		private const string _GoodStatus = "True";

		// Token: 0x04000811 RID: 2065
		private bool m_finished;

		// Token: 0x020004BE RID: 1214
		protected sealed class ItemCollection : List<UpgradeMultipleItemsTask.ItemInfo>
		{
		}

		// Token: 0x020004BF RID: 1215
		protected abstract class ItemInfo
		{
			// Token: 0x17000A91 RID: 2705
			// (get) Token: 0x0600242A RID: 9258
			public abstract string TraceIdentifier { get; }

			// Token: 0x0600242B RID: 9259
			public abstract void Upgrade(Storage storage);
		}
	}
}
