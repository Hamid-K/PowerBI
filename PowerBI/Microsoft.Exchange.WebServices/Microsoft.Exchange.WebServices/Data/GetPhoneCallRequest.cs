using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011E RID: 286
	internal sealed class GetPhoneCallRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x0002B92D File Offset: 0x0002A92D
		internal GetPhoneCallRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0002B936 File Offset: 0x0002A936
		internal override string GetXmlElementName()
		{
			return "GetPhoneCallInformation";
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0002B93D File Offset: 0x0002A93D
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.id.WriteToXml(writer, XmlNamespace.Messages, "PhoneCallId");
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0002B951 File Offset: 0x0002A951
		internal override string GetResponseXmlElementName()
		{
			return "GetPhoneCallInformationResponse";
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0002B958 File Offset: 0x0002A958
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetPhoneCallResponse getPhoneCallResponse = new GetPhoneCallResponse(base.Service);
			getPhoneCallResponse.LoadFromXml(reader, "GetPhoneCallInformationResponse");
			return getPhoneCallResponse;
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0002B97E File Offset: 0x0002A97E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0002B984 File Offset: 0x0002A984
		internal GetPhoneCallResponse Execute()
		{
			GetPhoneCallResponse getPhoneCallResponse = (GetPhoneCallResponse)base.InternalExecute();
			getPhoneCallResponse.ThrowIfNecessary();
			return getPhoneCallResponse;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0002B9A4 File Offset: 0x0002A9A4
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0002B9AC File Offset: 0x0002A9AC
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

		// Token: 0x04000916 RID: 2326
		private PhoneCallId id;
	}
}
