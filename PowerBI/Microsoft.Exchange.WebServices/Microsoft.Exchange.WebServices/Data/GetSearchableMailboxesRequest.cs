using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000121 RID: 289
	internal sealed class GetSearchableMailboxesRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000E26 RID: 3622 RVA: 0x0002BA95 File Offset: 0x0002AA95
		internal GetSearchableMailboxesRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002BA9E File Offset: 0x0002AA9E
		internal override string GetResponseXmlElementName()
		{
			return "GetSearchableMailboxesResponse";
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002BAA5 File Offset: 0x0002AAA5
		internal override string GetXmlElementName()
		{
			return "GetSearchableMailboxes";
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002BAAC File Offset: 0x0002AAAC
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetSearchableMailboxesResponse getSearchableMailboxesResponse = new GetSearchableMailboxesResponse();
			getSearchableMailboxesResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getSearchableMailboxesResponse;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002BAD0 File Offset: 0x0002AAD0
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "SearchFilter", this.SearchFilter ?? string.Empty);
			writer.WriteElementValue(XmlNamespace.Messages, "ExpandGroupMembership", this.ExpandGroupMembership.ToString().ToLower());
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002BB17 File Offset: 0x0002AB17
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0002BB1C File Offset: 0x0002AB1C
		internal GetSearchableMailboxesResponse Execute()
		{
			return (GetSearchableMailboxesResponse)base.InternalExecute();
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0002BB38 File Offset: 0x0002AB38
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0002BB4C File Offset: 0x0002AB4C
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0002BB54 File Offset: 0x0002AB54
		public string SearchFilter { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0002BB5D File Offset: 0x0002AB5D
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0002BB65 File Offset: 0x0002AB65
		public bool ExpandGroupMembership { get; set; }
	}
}
