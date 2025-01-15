using System;

namespace Microsoft.ReportingServices.Library.Soap2010
{
	// Token: 0x020002F2 RID: 754
	public class CacheRefreshPlan
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x0006CD50 File Offset: 0x0006AF50
		internal static CacheRefreshPlan SubscriptionToThis(SubscriptionImpl subscription)
		{
			if (subscription == null)
			{
				return null;
			}
			return new CacheRefreshPlan
			{
				Description = subscription.m_description,
				CacheRefreshPlanID = subscription.ID.ToString(),
				ItemPath = subscription.ItemPath.Value,
				State = new CacheRefreshPlanState(subscription.ActiveState),
				LastExecuted = subscription.m_lastRunTime,
				ModifiedDate = subscription.m_modifiedDate,
				ModifiedBy = subscription.ModifiedBy.UserName,
				LastRunStatus = subscription.LastStatus
			};
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0006CDE4 File Offset: 0x0006AFE4
		internal static CacheRefreshPlan[] LibrarySubscriptionArrayToThisArray(SubscriptionImpl[] subscriptions)
		{
			if (subscriptions == null)
			{
				return null;
			}
			CacheRefreshPlan[] array = new CacheRefreshPlan[subscriptions.Length];
			for (int i = 0; i < subscriptions.Length; i++)
			{
				array[i] = CacheRefreshPlan.SubscriptionToThis(subscriptions[i]);
			}
			return array;
		}

		// Token: 0x040009DF RID: 2527
		public string CacheRefreshPlanID;

		// Token: 0x040009E0 RID: 2528
		public string ItemPath;

		// Token: 0x040009E1 RID: 2529
		public string Description;

		// Token: 0x040009E2 RID: 2530
		public CacheRefreshPlanState State;

		// Token: 0x040009E3 RID: 2531
		public DateTime LastExecuted;

		// Token: 0x040009E4 RID: 2532
		public DateTime ModifiedDate;

		// Token: 0x040009E5 RID: 2533
		public string ModifiedBy;

		// Token: 0x040009E6 RID: 2534
		public string LastRunStatus;
	}
}
