using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016F RID: 367
	internal sealed class GetPhoneCallResponse : ServiceResponse
	{
		// Token: 0x060010CA RID: 4298 RVA: 0x000314EC File Offset: 0x000304EC
		internal GetPhoneCallResponse(ExchangeService service)
		{
			EwsUtilities.Assert(service != null, "GetPhoneCallResponse.ctor", "service is null");
			this.phoneCall = new PhoneCall(service);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00031516 File Offset: 0x00030516
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.Messages, "PhoneCallInformation");
			this.phoneCall.LoadFromXml(reader, XmlNamespace.Messages, "PhoneCallInformation");
			reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "PhoneCallInformation");
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x00031542 File Offset: 0x00030542
		internal PhoneCall PhoneCall
		{
			get
			{
				return this.phoneCall;
			}
		}

		// Token: 0x040009C8 RID: 2504
		private PhoneCall phoneCall;
	}
}
