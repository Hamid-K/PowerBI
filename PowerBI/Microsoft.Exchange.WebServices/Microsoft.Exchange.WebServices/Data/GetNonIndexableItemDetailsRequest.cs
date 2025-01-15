using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011B RID: 283
	internal sealed class GetNonIndexableItemDetailsRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0002B555 File Offset: 0x0002A555
		internal GetNonIndexableItemDetailsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0002B55E File Offset: 0x0002A55E
		internal override string GetResponseXmlElementName()
		{
			return "GetNonIndexableItemDetailsResponse";
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0002B565 File Offset: 0x0002A565
		internal override string GetXmlElementName()
		{
			return "GetNonIndexableItemDetails";
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002B56C File Offset: 0x0002A56C
		internal override void Validate()
		{
			base.Validate();
			if (this.Mailboxes == null || this.Mailboxes.Length == 0)
			{
				throw new ServiceValidationException(Strings.MailboxesParameterIsNotSpecified);
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002B598 File Offset: 0x0002A598
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetNonIndexableItemDetailsResponse getNonIndexableItemDetailsResponse = new GetNonIndexableItemDetailsResponse();
			getNonIndexableItemDetailsResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getNonIndexableItemDetailsResponse;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002B5BC File Offset: 0x0002A5BC
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "Mailboxes");
			foreach (string text in this.Mailboxes)
			{
				writer.WriteElementValue(XmlNamespace.Types, "LegacyDN", text);
			}
			writer.WriteEndElement();
			if (this.PageSize != null && this.PageSize != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "PageSize", this.PageSize.Value.ToString());
			}
			if (!string.IsNullOrEmpty(this.PageItemReference))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "PageItemReference", this.PageItemReference);
			}
			if (this.PageDirection != null && this.PageDirection != null)
			{
				writer.WriteElementValue(XmlNamespace.Messages, "PageDirection", this.PageDirection.Value.ToString());
			}
			writer.WriteElementValue(XmlNamespace.Messages, "SearchArchiveOnly", this.SearchArchiveOnly);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0002B6C3 File Offset: 0x0002A6C3
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0002B6C8 File Offset: 0x0002A6C8
		internal GetNonIndexableItemDetailsResponse Execute()
		{
			return (GetNonIndexableItemDetailsResponse)base.InternalExecute();
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002B6E4 File Offset: 0x0002A6E4
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0002B6F8 File Offset: 0x0002A6F8
		// (set) Token: 0x06000DEC RID: 3564 RVA: 0x0002B700 File Offset: 0x0002A700
		public string[] Mailboxes { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0002B709 File Offset: 0x0002A709
		// (set) Token: 0x06000DEE RID: 3566 RVA: 0x0002B711 File Offset: 0x0002A711
		public int? PageSize { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0002B71A File Offset: 0x0002A71A
		// (set) Token: 0x06000DF0 RID: 3568 RVA: 0x0002B722 File Offset: 0x0002A722
		public string PageItemReference { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DF1 RID: 3569 RVA: 0x0002B72B File Offset: 0x0002A72B
		// (set) Token: 0x06000DF2 RID: 3570 RVA: 0x0002B733 File Offset: 0x0002A733
		public SearchPageDirection? PageDirection { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000DF3 RID: 3571 RVA: 0x0002B73C File Offset: 0x0002A73C
		// (set) Token: 0x06000DF4 RID: 3572 RVA: 0x0002B744 File Offset: 0x0002A744
		public bool SearchArchiveOnly { get; set; }
	}
}
