using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000103 RID: 259
	internal class ExpandGroupRequest : MultiResponseServiceRequest<ExpandGroupResponse>
	{
		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002995C File Offset: 0x0002895C
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParam(this.EmailAddress, "EmailAddress");
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00029974 File Offset: 0x00028974
		internal override ExpandGroupResponse CreateServiceResponse(ExchangeService service, int responseIndex)
		{
			return new ExpandGroupResponse();
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002997B File Offset: 0x0002897B
		internal override int GetExpectedResponseMessageCount()
		{
			return 1;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x0002997E File Offset: 0x0002897E
		internal override string GetXmlElementName()
		{
			return "ExpandDL";
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x00029985 File Offset: 0x00028985
		internal override string GetResponseXmlElementName()
		{
			return "ExpandDLResponse";
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x0002998C File Offset: 0x0002898C
		internal override string GetResponseMessageXmlElementName()
		{
			return "ExpandDLResponseMessage";
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00029993 File Offset: 0x00028993
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			if (this.EmailAddress != null)
			{
				this.EmailAddress.WriteToXml(writer, XmlNamespace.Messages, "Mailbox");
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x000299AF File Offset: 0x000289AF
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000299B2 File Offset: 0x000289B2
		internal ExpandGroupRequest(ExchangeService service)
			: base(service, ServiceErrorHandling.ThrowOnError)
		{
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000299BC File Offset: 0x000289BC
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x000299C4 File Offset: 0x000289C4
		public EmailAddress EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
			set
			{
				this.emailAddress = value;
			}
		}

		// Token: 0x040008E0 RID: 2272
		private EmailAddress emailAddress;
	}
}
