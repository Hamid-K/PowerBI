using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000117 RID: 279
	internal sealed class GetInboxRulesRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002B3DD File Offset: 0x0002A3DD
		internal GetInboxRulesRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0002B3E6 File Offset: 0x0002A3E6
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0002B3EE File Offset: 0x0002A3EE
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

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002B3F7 File Offset: 0x0002A3F7
		internal override string GetXmlElementName()
		{
			return "GetInboxRules";
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002B3FE File Offset: 0x0002A3FE
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (!string.IsNullOrEmpty(this.mailboxSmtpAddress))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "MailboxSmtpAddress", this.mailboxSmtpAddress);
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002B41F File Offset: 0x0002A41F
		internal override string GetResponseXmlElementName()
		{
			return "GetInboxRulesResponse";
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002B428 File Offset: 0x0002A428
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetInboxRulesResponse getInboxRulesResponse = new GetInboxRulesResponse();
			getInboxRulesResponse.LoadFromXml(reader, "GetInboxRulesResponse");
			return getInboxRulesResponse;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002B448 File Offset: 0x0002A448
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2010_SP1;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002B44C File Offset: 0x0002A44C
		internal GetInboxRulesResponse Execute()
		{
			GetInboxRulesResponse getInboxRulesResponse = (GetInboxRulesResponse)base.InternalExecute();
			getInboxRulesResponse.ThrowIfNecessary();
			return getInboxRulesResponse;
		}

		// Token: 0x0400090C RID: 2316
		private string mailboxSmtpAddress;
	}
}
