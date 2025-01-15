using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000111 RID: 273
	internal class GetEventsRequest : MultiResponseServiceRequest<GetEventsResponse>, IJsonSerializable
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0002B0D7 File Offset: 0x0002A0D7
		internal GetEventsRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0002B0E1 File Offset: 0x0002A0E1
		internal override GetEventsResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new GetEventsResponse();
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0002B0E8 File Offset: 0x0002A0E8
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002B0EB File Offset: 0x0002A0EB
		internal override string GetXmlElementName()
		{
			return "GetEvents";
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002B0F2 File Offset: 0x0002A0F2
		internal override string GetResponseXmlElementName()
		{
			return "GetEventsResponse";
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002B0F9 File Offset: 0x0002A0F9
		internal override string GetResponseMessageXmlElementName()
		{
			return "GetEventsResponseMessage";
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002B100 File Offset: 0x0002A100
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateNonBlankStringParam(this.SubscriptionId, "SubscriptionId");
			EwsUtilities.ValidateNonBlankStringParam(this.Watermark, "Watermark");
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002B128 File Offset: 0x0002A128
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "SubscriptionId", this.SubscriptionId);
			writer.WriteElementValue(XmlNamespace.Messages, "Watermark", this.Watermark);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002B150 File Offset: 0x0002A150
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("SubscriptionId", this.SubscriptionId);
			jsonObject.Add("Watermark", this.Watermark);
			return jsonObject;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002B186 File Offset: 0x0002A186
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0002B189 File Offset: 0x0002A189
		// (set) Token: 0x06000DA4 RID: 3492 RVA: 0x0002B191 File Offset: 0x0002A191
		public string SubscriptionId
		{
			get
			{
				return this.subscriptionId;
			}
			set
			{
				this.subscriptionId = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0002B19A File Offset: 0x0002A19A
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x0002B1A2 File Offset: 0x0002A1A2
		public string Watermark
		{
			get
			{
				return this.watermark;
			}
			set
			{
				this.watermark = value;
			}
		}

		// Token: 0x04000907 RID: 2311
		private string subscriptionId;

		// Token: 0x04000908 RID: 2312
		private string watermark;
	}
}
