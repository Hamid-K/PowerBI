using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000144 RID: 324
	internal sealed class UnpinTeamMailboxRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x0002E70D File Offset: 0x0002D70D
		public UnpinTeamMailboxRequest(ExchangeService service, EmailAddress emailAddress)
			: base(service)
		{
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			this.emailAddress = emailAddress;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0002E72B File Offset: 0x0002D72B
		internal override string GetXmlElementName()
		{
			return "UnpinTeamMailbox";
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0002E732 File Offset: 0x0002D732
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.emailAddress.WriteToXml(writer, XmlNamespace.Messages, "EmailAddress");
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0002E746 File Offset: 0x0002D746
		internal override string GetResponseXmlElementName()
		{
			return "UnpinTeamMailboxResponse";
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0002E750 File Offset: 0x0002D750
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponse serviceResponse = new ServiceResponse();
			serviceResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return serviceResponse;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002E771 File Offset: 0x0002D771
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0002E774 File Offset: 0x0002D774
		internal ServiceResponse Execute()
		{
			ServiceResponse serviceResponse = (ServiceResponse)base.InternalExecute();
			serviceResponse.ThrowIfNecessary();
			return serviceResponse;
		}

		// Token: 0x0400097A RID: 2426
		private readonly EmailAddress emailAddress;
	}
}
