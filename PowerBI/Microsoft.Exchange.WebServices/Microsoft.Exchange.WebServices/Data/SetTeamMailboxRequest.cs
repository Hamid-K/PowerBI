using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013B RID: 315
	internal sealed class SetTeamMailboxRequest : SimpleServiceRequestBase
	{
		// Token: 0x06000F51 RID: 3921 RVA: 0x0002DBA0 File Offset: 0x0002CBA0
		internal SetTeamMailboxRequest(ExchangeService service, EmailAddress emailAddress, Uri sharePointSiteUrl, TeamMailboxLifecycleState state)
			: base(service)
		{
			if (emailAddress == null)
			{
				throw new ArgumentNullException("emailAddress");
			}
			if (sharePointSiteUrl == null)
			{
				throw new ArgumentNullException("sharePointSiteUrl");
			}
			this.emailAddress = emailAddress;
			this.sharePointSiteUrl = sharePointSiteUrl;
			this.state = state;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0002DBEC File Offset: 0x0002CBEC
		internal override string GetXmlElementName()
		{
			return "SetTeamMailbox";
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0002DBF4 File Offset: 0x0002CBF4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.emailAddress.WriteToXml(writer, XmlNamespace.Messages, "EmailAddress");
			writer.WriteElementValue(XmlNamespace.Messages, "SharePointSiteUrl", this.sharePointSiteUrl.ToString());
			writer.WriteElementValue(XmlNamespace.Messages, "State", this.state.ToString());
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0002DC46 File Offset: 0x0002CC46
		internal override string GetResponseXmlElementName()
		{
			return "SetTeamMailboxResponse";
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0002DC50 File Offset: 0x0002CC50
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			ServiceResponse serviceResponse = new ServiceResponse();
			serviceResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return serviceResponse;
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0002DC71 File Offset: 0x0002CC71
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002DC74 File Offset: 0x0002CC74
		internal ServiceResponse Execute()
		{
			ServiceResponse serviceResponse = (ServiceResponse)base.InternalExecute();
			serviceResponse.ThrowIfNecessary();
			return serviceResponse;
		}

		// Token: 0x04000963 RID: 2403
		private EmailAddress emailAddress;

		// Token: 0x04000964 RID: 2404
		private Uri sharePointSiteUrl;

		// Token: 0x04000965 RID: 2405
		private TeamMailboxLifecycleState state;
	}
}
