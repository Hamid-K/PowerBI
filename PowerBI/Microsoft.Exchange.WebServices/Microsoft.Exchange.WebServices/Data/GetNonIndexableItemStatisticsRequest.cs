using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200011C RID: 284
	internal sealed class GetNonIndexableItemStatisticsRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002B74D File Offset: 0x0002A74D
		internal GetNonIndexableItemStatisticsRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002B756 File Offset: 0x0002A756
		internal override string GetResponseXmlElementName()
		{
			return "GetNonIndexableItemStatisticsResponse";
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002B75D File Offset: 0x0002A75D
		internal override string GetXmlElementName()
		{
			return "GetNonIndexableItemStatistics";
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0002B764 File Offset: 0x0002A764
		internal override void Validate()
		{
			base.Validate();
			if (this.Mailboxes == null || this.Mailboxes.Length == 0)
			{
				throw new ServiceValidationException(Strings.MailboxesParameterIsNotSpecified);
			}
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x0002B790 File Offset: 0x0002A790
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			GetNonIndexableItemStatisticsResponse getNonIndexableItemStatisticsResponse = new GetNonIndexableItemStatisticsResponse();
			getNonIndexableItemStatisticsResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return getNonIndexableItemStatisticsResponse;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002B7B4 File Offset: 0x0002A7B4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "Mailboxes");
			foreach (string text in this.Mailboxes)
			{
				writer.WriteElementValue(XmlNamespace.Types, "LegacyDN", text);
			}
			writer.WriteEndElement();
			writer.WriteElementValue(XmlNamespace.Messages, "SearchArchiveOnly", this.SearchArchiveOnly);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002B810 File Offset: 0x0002A810
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002B814 File Offset: 0x0002A814
		internal GetNonIndexableItemStatisticsResponse Execute()
		{
			return (GetNonIndexableItemStatisticsResponse)base.InternalExecute();
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002B830 File Offset: 0x0002A830
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0002B844 File Offset: 0x0002A844
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0002B84C File Offset: 0x0002A84C
		public string[] Mailboxes { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0002B855 File Offset: 0x0002A855
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0002B85D File Offset: 0x0002A85D
		public bool SearchArchiveOnly { get; set; }
	}
}
