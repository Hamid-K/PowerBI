using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000216 RID: 534
	internal sealed class CreateCacheRefreshPlanAction : RSSoapAction<CreateCacheRefreshPlanActionParameters>
	{
		// Token: 0x060012F7 RID: 4855 RVA: 0x0004353C File Offset: 0x0004173C
		public CreateCacheRefreshPlanAction(RSService service)
			: base("CreateCacheRefreshPlanAction", service)
		{
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0004354C File Offset: 0x0004174C
		internal override void PerformActionNow()
		{
			CatalogItem catalogItem = base.Service.EnsureCacheRefreshPlanIsAllowed(base.ActionParameters.ItemPath);
			if (base.ActionParameters.ItemType == ItemType.PowerBIReport)
			{
				base.ActionParameters.Parameters = null;
			}
			else
			{
				base.ActionParameters.Parameters = base.Service.SubscriptionManager.ValidateSubscriptionParameters(catalogItem.ItemContext.ItemPath, base.ActionParameters.Parameters, JobType.UserJobType);
			}
			base.ActionParameters.CacheRefreshPlanID = base.Service.SubscriptionManager.CreateCacheRefreshPlan(catalogItem.ItemContext.ItemPath, catalogItem.ItemID, base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.Description, base.ActionParameters.Parameters, catalogItem.SecurityDescriptor).ToString();
		}
	}
}
