using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000218 RID: 536
	internal sealed class SetCacheRefreshPlanPropertiesAction : RSSoapAction<SetCacheRefreshPlanPropertiesActionParameters>
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00043824 File Offset: 0x00041A24
		public SetCacheRefreshPlanPropertiesAction(RSService service)
			: base("SetCacheRefreshPlanPropertiesAction", service)
		{
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001307 RID: 4871 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00043834 File Offset: 0x00041A34
		internal override void PerformActionNow()
		{
			base.Service.SubscriptionManager.SetCacheRefreshPlanProperties(Globals.ParseGuidParameter(base.ActionParameters.CacheRefreshPlanID, "subscriptionID"), base.ActionParameters.EventType, base.ActionParameters.MatchData, base.ActionParameters.Description, base.ActionParameters.Parameters);
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x00037632 File Offset: 0x00035832
		protected override IsolationLevel IsolationLevel
		{
			get
			{
				return IsolationLevel.RepeatableRead;
			}
		}
	}
}
