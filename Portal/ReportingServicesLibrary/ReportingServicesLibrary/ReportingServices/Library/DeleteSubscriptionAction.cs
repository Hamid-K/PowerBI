using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020E RID: 526
	internal sealed class DeleteSubscriptionAction : RSSoapAction<DeleteSubscriptionActionParameters>
	{
		// Token: 0x060012A1 RID: 4769 RVA: 0x00042770 File Offset: 0x00040970
		public DeleteSubscriptionAction(RSService service)
			: base("DeleteSubscriptionAction", service)
		{
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00042780 File Offset: 0x00040980
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DeleteSubscription, base.ActionParameters.SubscriptionID, "subscriptionID", null, null, null, null, false, null, null);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x000427C7 File Offset: 0x000409C7
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.SubscriptionID = parameters.Item;
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x000427E6 File Offset: 0x000409E6
		internal override void PerformActionNow()
		{
			base.Service.SubscriptionManager.DeleteSubscription(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "SubscriptionID"), base.ActionParameters.IsCacheRefreshPlanExpected);
		}
	}
}
