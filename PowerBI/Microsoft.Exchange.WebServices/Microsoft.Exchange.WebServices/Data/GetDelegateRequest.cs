using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200010F RID: 271
	internal class GetDelegateRequest : DelegateManagementRequestBase<GetDelegateResponse>
	{
		// Token: 0x06000D81 RID: 3457 RVA: 0x0002AEE5 File Offset: 0x00029EE5
		internal GetDelegateRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002AEF9 File Offset: 0x00029EF9
		internal override GetDelegateResponse CreateResponse()
		{
			return new GetDelegateResponse(true);
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002AF01 File Offset: 0x00029F01
		internal override void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			base.WriteAttributesToXml(writer);
			writer.WriteAttributeValue("IncludePermissions", this.IncludePermissions);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002AF20 File Offset: 0x00029F20
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			base.WriteElementsToXml(writer);
			if (this.UserIds.Count > 0)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "UserIds");
				foreach (UserId userId in this.UserIds)
				{
					userId.WriteToXml(writer, "UserId");
				}
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x0002AFA0 File Offset: 0x00029FA0
		internal override string GetResponseXmlElementName()
		{
			return "GetDelegateResponse";
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x0002AFA7 File Offset: 0x00029FA7
		internal override string GetXmlElementName()
		{
			return "GetDelegate";
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x0002AFAE File Offset: 0x00029FAE
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2007_SP1;
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0002AFB1 File Offset: 0x00029FB1
		public List<UserId> UserIds
		{
			get
			{
				return this.userIds;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000D89 RID: 3465 RVA: 0x0002AFB9 File Offset: 0x00029FB9
		// (set) Token: 0x06000D8A RID: 3466 RVA: 0x0002AFC1 File Offset: 0x00029FC1
		public bool IncludePermissions
		{
			get
			{
				return this.includePermissions;
			}
			set
			{
				this.includePermissions = value;
			}
		}

		// Token: 0x04000902 RID: 2306
		private List<UserId> userIds = new List<UserId>();

		// Token: 0x04000903 RID: 2307
		private bool includePermissions;
	}
}
