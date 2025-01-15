using System;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200020A RID: 522
	internal sealed class DisableSubscriptionAction : RSSoapAction<DisableSubscriptionActionParameters>
	{
		// Token: 0x0600128B RID: 4747 RVA: 0x00042528 File Offset: 0x00040728
		public DisableSubscriptionAction(RSService service)
			: base("DisableSubscriptionAction", service)
		{
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x000053DC File Offset: 0x000035DC
		internal override ConnectionTransactionType TransactionType
		{
			get
			{
				return ConnectionTransactionType.AutoCommit;
			}
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00042536 File Offset: 0x00040736
		internal override void PerformActionInBatch(CallParameters parameters)
		{
			base.ActionParameters.SubscriptionID = parameters.Item;
			base.BatchTraceInput();
			this.PerformActionNow();
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00042555 File Offset: 0x00040755
		internal override void PerformActionNow()
		{
			base.Service.SubscriptionManager.DisableSubscription(Globals.ParseGuidParameter(base.ActionParameters.SubscriptionID, "SubscriptionID"));
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0004257C File Offset: 0x0004077C
		protected override void AddActionToBatch()
		{
			base.Service.Storage.AddBatchRecord(base.BatchID, base.Service.UserName, CatalogCommand.DisableSubscription, base.ActionParameters.SubscriptionID, "subscriptionID", null, null, null, null, false, null, null);
		}
	}
}
