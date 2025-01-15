using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000206 RID: 518
	internal sealed class ListSubscriptionsAction : RSSoapAction<ListSubscriptionActionParameters>
	{
		// Token: 0x06001269 RID: 4713 RVA: 0x00041B90 File Offset: 0x0003FD90
		public ListSubscriptionsAction(RSService service)
			: base("ListSubscriptionsAction", service)
		{
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x0600126A RID: 4714 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00041BA0 File Offset: 0x0003FDA0
		internal override void PerformActionNow()
		{
			if (!string.IsNullOrEmpty(base.ActionParameters.Path))
			{
				CatalogItemContext catalogItemContext = new CatalogItemContext(base.Service, base.ActionParameters.Path, "path");
				CatalogItem catalogItem = base.Service.CatalogItemFactory.GetCatalogItem(catalogItemContext, false);
				base.ActionParameters.Path = catalogItemContext.ItemPath.Value;
				if (base.ActionParameters.PathIsSiteOrFolder)
				{
					catalogItem.ThrowIfWrongItemType(new ItemType[]
					{
						ItemType.Site,
						ItemType.Folder
					});
				}
				else
				{
					catalogItem.ThrowIfWrongItemType(new ItemType[]
					{
						ItemType.Report,
						ItemType.LinkedReport,
						ItemType.DataSet,
						ItemType.PowerBIReport
					});
				}
			}
			List<SubscriptionImpl> list = base.Service.SubscriptionManager.ListSubscriptions(base.ActionParameters.Owner, new ExternalItemPath(base.ActionParameters.Path), base.ActionParameters.PathIsSiteOrFolder, base.ActionParameters.SubscriptionType, base.ActionParameters.IncludeExtensionSettings);
			base.ActionParameters.Children = list.ToArray();
		}
	}
}
