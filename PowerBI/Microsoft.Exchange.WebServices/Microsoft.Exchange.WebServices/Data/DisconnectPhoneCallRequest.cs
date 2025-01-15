using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000100 RID: 256
	internal sealed class DisconnectPhoneCallRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002976E File Offset: 0x0002876E
		internal DisconnectPhoneCallRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00029777 File Offset: 0x00028777
		internal override string GetXmlElementName()
		{
			return "DisconnectPhoneCall";
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002977E File Offset: 0x0002877E
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.id.WriteToXml(writer, XmlNamespace.Messages, "PhoneCallId");
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x00029792 File Offset: 0x00028792
		internal override string GetResponseXmlElementName()
		{
			return "DisconnectPhoneCallResponse";
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002979C File Offset: 0x0002879C
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponse serviceResponse = new ServiceResponse();
			serviceResponse.LoadFromXml(reader, "DisconnectPhoneCallResponse");
			return serviceResponse;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000297BC File Offset: 0x000287BC
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000297C0 File Offset: 0x000287C0
		internal ServiceResponse Execute()
		{
			ServiceResponse serviceResponse = (ServiceResponse)base.InternalExecute();
			serviceResponse.ThrowIfNecessary();
			return serviceResponse;
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x000297E0 File Offset: 0x000287E0
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x000297E8 File Offset: 0x000287E8
		internal PhoneCallId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x040008DB RID: 2267
		private PhoneCallId id;
	}
}
