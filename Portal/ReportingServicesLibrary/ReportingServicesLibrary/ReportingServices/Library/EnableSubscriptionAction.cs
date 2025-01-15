using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020C RID: 524
	internal sealed class EnableSubscriptionAction : RSSoapAction<EnableSubscriptionActionParameters>
	{
		// Token: 0x06001295 RID: 4757 RVA: 0x00042644 File Offset: 0x00040844
		public EnableSubscriptionAction(RSService service)
			: base("EnableSubscriptionAction", service)
		{
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x00042652 File Offset: 0x00040852
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.SubscriptionID = parameters.Item;
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00042671 File Offset: 0x00040871
		internal override void PerformActionNow()
		{
			base.Service.SubscriptionManager.EnableSubscription(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "SubscriptionID"));
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x00042698 File Offset: 0x00040898
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.EnableSubscription, base.ActionParameters.SubscriptionID, "subscriptionID", null, null, null, null, false, null, null);
		}
	}
}
