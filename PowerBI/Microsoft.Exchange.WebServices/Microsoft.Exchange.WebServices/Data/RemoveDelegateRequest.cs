using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000132 RID: 306
	internal class RemoveDelegateRequest : DelegateManagementRequestBase<DelegateManagementResponse>
	{
		// Token: 0x06000EC8 RID: 3784 RVA: 0x0002C99E File Offset: 0x0002B99E
		internal RemoveDelegateRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002C9B2 File Offset: 0x0002B9B2
		internal override void Validate()
		{
			base.Validate();
			EwsUtilities.ValidateParamCollection(this.UserIds, "UserIds");
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0002C9CC File Offset: 0x0002B9CC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			writer.WriteStartElement(XmlNamespace.Messages, "UserIds");
			foreach (UserId userId in this.UserIds)
			{
				userId.WriteToXml(writer, "UserId");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0002CA40 File Offset: 0x0002BA40
		internal override string GetResponseXmlElementName()
		{
			return "RemoveDelegateResponse";
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0002CA47 File Offset: 0x0002BA47
		internal override string GetXmlElementName()
		{
			return "RemoveDelegate";
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0002CA4E File Offset: 0x0002BA4E
		internal override DelegateManagementResponse CreateResponse()
		{
			return new DelegateManagementResponse(false, null);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0002CA57 File Offset: 0x0002BA57
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x0002CA5A File Offset: 0x0002BA5A
		public List<UserId> UserIds
		{
			get
			{
				return this.userIds;
			}
		}

		// Token: 0x04000940 RID: 2368
		private List<UserId> userIds = new List<UserId>();
	}
}
