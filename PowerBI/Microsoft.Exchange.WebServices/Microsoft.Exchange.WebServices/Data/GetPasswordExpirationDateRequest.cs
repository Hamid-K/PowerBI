using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011D RID: 285
	internal sealed class GetPasswordExpirationDateRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000E02 RID: 3586 RVA: 0x0002B866 File Offset: 0x0002A866
		internal GetPasswordExpirationDateRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002B86F File Offset: 0x0002A86F
		internal override string GetXmlElementName()
		{
			return "GetPasswordExpirationDate";
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002B876 File Offset: 0x0002A876
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "MailboxSmtpAddress", this.MailboxSmtpAddress);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002B88C File Offset: 0x0002A88C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("MailboxSmtpAddress", this.MailboxSmtpAddress);
			return jsonObject;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002B8B1 File Offset: 0x0002A8B1
		internal override string GetResponseXmlElementName()
		{
			return "GetPasswordExpirationDateResponse";
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002B8B8 File Offset: 0x0002A8B8
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetPasswordExpirationDateResponse getPasswordExpirationDateResponse = new GetPasswordExpirationDateResponse();
			getPasswordExpirationDateResponse.LoadFromXml(reader, "GetPasswordExpirationDateResponse");
			return getPasswordExpirationDateResponse;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002B8D8 File Offset: 0x0002A8D8
		internal override object ParseResponse(JsonObject jsonBody)
		{
			GetPasswordExpirationDateResponse getPasswordExpirationDateResponse = new GetPasswordExpirationDateResponse();
			getPasswordExpirationDateResponse.LoadFromJson(jsonBody, base.Service);
			return getPasswordExpirationDateResponse;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002B8F9 File Offset: 0x0002A8F9
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002B8FC File Offset: 0x0002A8FC
		internal GetPasswordExpirationDateResponse Execute()
		{
			GetPasswordExpirationDateResponse getPasswordExpirationDateResponse = (GetPasswordExpirationDateResponse)base.InternalExecute();
			getPasswordExpirationDateResponse.ThrowIfNecessary();
			return getPasswordExpirationDateResponse;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0002B91C File Offset: 0x0002A91C
		// (set) Token: 0x06000E0C RID: 3596 RVA: 0x0002B924 File Offset: 0x0002A924
		internal string MailboxSmtpAddress
		{
			get
			{
				return this.mailboxSmtpAddress;
			}
			set
			{
				this.mailboxSmtpAddress = value;
			}
		}

		// Token: 0x04000915 RID: 2325
		private string mailboxSmtpAddress;
	}
}
