using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000145 RID: 325
	internal class UnsubscribeRequest : MultiResponseServiceRequest<ServiceResponse>, IJsonSerializable
	{
		// Token: 0x06000FC9 RID: 4041 RVA: 0x0002E794 File Offset: 0x0002D794
		internal UnsubscribeRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0002E79E File Offset: 0x0002D79E
		internal override ServiceResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ServiceResponse();
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0002E7A5 File Offset: 0x0002D7A5
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0002E7A8 File Offset: 0x0002D7A8
		internal override string GetXmlElementName()
		{
			return "Unsubscribe";
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0002E7AF File Offset: 0x0002D7AF
		internal override string GetResponseXmlElementName()
		{
			return "UnsubscribeResponse";
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0002E7B6 File Offset: 0x0002D7B6
		internal override string GetResponseMessageXmlElementName()
		{
			return "UnsubscribeResponseMessage";
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002E7BD File Offset: 0x0002D7BD
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateNonBlankStringParam(this.SubscriptionId, "SubscriptionId");
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0002E7D5 File Offset: 0x0002D7D5
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "SubscriptionId", this.SubscriptionId);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0002E7EC File Offset: 0x0002D7EC
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("SubscriptionId", this.SubscriptionId);
			return jsonObject;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0002E811 File Offset: 0x0002D811
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0002E814 File Offset: 0x0002D814
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x0002E81C File Offset: 0x0002D81C
		public string SubscriptionId { get; set; }
	}
}
