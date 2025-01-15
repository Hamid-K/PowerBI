using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200017D RID: 381
	internal sealed class PlayOnPhoneResponse : ServiceResponse
	{
		// Token: 0x06001100 RID: 4352 RVA: 0x00031C63 File Offset: 0x00030C63
		internal PlayOnPhoneResponse(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "PlayOnPhoneResponse.ctor", "service is null");
			this.phoneCallId = new PhoneCallId();
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x00031C8C File Offset: 0x00030C8C
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "PhoneCallId");
			this.phoneCallId.LoadFromXml(reader, XmlNamespace.Messages, "PhoneCallId");
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "PhoneCallId");
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00031CB8 File Offset: 0x00030CB8
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.phoneCallId.LoadFromJson(responseObject.ReadAsJsonObject("PhoneCallId"), service);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06001103 RID: 4355 RVA: 0x00031CD9 File Offset: 0x00030CD9
		internal PhoneCallId PhoneCallId
		{
			get
			{
				return this.phoneCallId;
			}
		}

		// Token: 0x040009D6 RID: 2518
		private PhoneCallId phoneCallId;
	}
}
