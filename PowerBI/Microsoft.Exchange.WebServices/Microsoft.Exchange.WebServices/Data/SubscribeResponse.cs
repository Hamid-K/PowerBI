using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000182 RID: 386
	internal sealed class SubscribeResponse<TSubscription> : ServiceResponse where TSubscription : SubscriptionBase
	{
		// Token: 0x06001115 RID: 4373 RVA: 0x00031E96 File Offset: 0x00030E96
		internal SubscribeResponse(TSubscription subscription)
		{
			EwsUtilities.Assert(subscription != null, "SubscribeResponse.ctor", "subscription is null");
			this.subscription = subscription;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00031EC0 File Offset: 0x00030EC0
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.subscription.LoadFromXml(reader);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x00031EDB File Offset: 0x00030EDB
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.subscription.LoadFromJson(responseObject, service);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00031EF8 File Offset: 0x00030EF8
		public TSubscription Subscription
		{
			get
			{
				return this.subscription;
			}
		}

		// Token: 0x040009DB RID: 2523
		private TSubscription subscription;
	}
}
