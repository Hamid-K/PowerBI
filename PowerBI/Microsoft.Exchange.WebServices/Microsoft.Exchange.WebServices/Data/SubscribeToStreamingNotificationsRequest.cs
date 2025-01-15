using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000140 RID: 320
	internal class SubscribeToStreamingNotificationsRequest : SubscribeRequest<StreamingSubscription>
	{
		// Token: 0x06000F8D RID: 3981 RVA: 0x0002E218 File Offset: 0x0002D218
		internal SubscribeToStreamingNotificationsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0002E221 File Offset: 0x0002D221
		internal override void Validate()
		{
			base.Validate();
			if (!string.IsNullOrEmpty(base.Watermark))
			{
				throw new ArgumentException("Watermarks cannot be used with StreamingSubscriptions.", "Watermark");
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0002E246 File Offset: 0x0002D246
		internal override string GetSubscriptionXmlElementName()
		{
			return "StreamingSubscriptionRequest";
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002E24D File Offset: 0x0002D24D
		internal override void InternalWriteElementsToXml(EwsServiceXmlWriter writer)
		{
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002E24F File Offset: 0x0002D24F
		internal override void AddJsonProperties(JsonObject jsonSubscribeRequest, ExchangeService service)
		{
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002E251 File Offset: 0x0002D251
		internal override SubscribeResponse<StreamingSubscription> CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new SubscribeResponse<StreamingSubscription>(new StreamingSubscription(service));
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0002E25E File Offset: 0x0002D25E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}
	}
}
