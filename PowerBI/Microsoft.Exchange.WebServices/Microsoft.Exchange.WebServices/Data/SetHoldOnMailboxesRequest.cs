using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200013A RID: 314
	internal sealed class SetHoldOnMailboxesRequest : SimpleServiceRequestBase, IJsonSerializable
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x0002D958 File Offset: 0x0002C958
		internal SetHoldOnMailboxesRequest(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x0002D961 File Offset: 0x0002C961
		internal override string GetResponseXmlElementName()
		{
			return "SetHoldOnMailboxesResponse";
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0002D968 File Offset: 0x0002C968
		internal override string GetXmlElementName()
		{
			return "SetHoldOnMailboxes";
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x0002D970 File Offset: 0x0002C970
		internal override void Validate()
		{
			base.Validate();
			if (string.IsNullOrEmpty(this.HoldId))
			{
				throw new ServiceValidationException(Strings.HoldIdParameterIsNotSpecified);
			}
			if (string.IsNullOrEmpty(this.InPlaceHoldIdentity) && (this.Mailboxes == null || this.Mailboxes.Length == 0))
			{
				throw new ServiceValidationException(Strings.HoldMailboxesParameterIsNotSpecified);
			}
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0002D9D0 File Offset: 0x0002C9D0
		internal override object ParseResponse(EwsServiceXmlReader reader)
		{
			SetHoldOnMailboxesResponse setHoldOnMailboxesResponse = new SetHoldOnMailboxesResponse();
			setHoldOnMailboxesResponse.LoadFromXml(reader, this.GetResponseXmlElementName());
			return setHoldOnMailboxesResponse;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0002D9F4 File Offset: 0x0002C9F4
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteElementValue(XmlNamespace.Messages, "ActionType", this.ActionType.ToString());
			writer.WriteElementValue(XmlNamespace.Messages, "HoldId", this.HoldId);
			writer.WriteElementValue(XmlNamespace.Messages, "Query", this.Query ?? string.Empty);
			if (this.Mailboxes != null && this.Mailboxes.Length > 0)
			{
				writer.WriteStartElement(XmlNamespace.Messages, "Mailboxes");
				foreach (string text in this.Mailboxes)
				{
					writer.WriteElementValue(XmlNamespace.Types, "String", text);
				}
				writer.WriteEndElement();
			}
			if (!string.IsNullOrEmpty(this.Language))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "Language", this.Language);
			}
			if (!string.IsNullOrEmpty(this.InPlaceHoldIdentity))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "InPlaceHoldIdentity", this.InPlaceHoldIdentity);
			}
			if (!string.IsNullOrEmpty(this.ItemHoldPeriod))
			{
				writer.WriteElementValue(XmlNamespace.Messages, "ItemHoldPeriod", this.ItemHoldPeriod);
			}
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0002DAF4 File Offset: 0x0002CAF4
		internal SetHoldOnMailboxesResponse Execute()
		{
			return (SetHoldOnMailboxesResponse)base.InternalExecute();
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002DB0E File Offset: 0x0002CB0E
		internal override ExchangeVersion GetMinimumRequiredServerVersion()
		{
			return ExchangeVersion.Exchange2013;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002DB14 File Offset: 0x0002CB14
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			return new JsonObject();
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002DB28 File Offset: 0x0002CB28
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0002DB30 File Offset: 0x0002CB30
		public HoldAction ActionType { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0002DB39 File Offset: 0x0002CB39
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0002DB41 File Offset: 0x0002CB41
		public string HoldId { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0002DB4A File Offset: 0x0002CB4A
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0002DB52 File Offset: 0x0002CB52
		public string Query { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002DB5B File Offset: 0x0002CB5B
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0002DB63 File Offset: 0x0002CB63
		public string[] Mailboxes { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0002DB6C File Offset: 0x0002CB6C
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x0002DB74 File Offset: 0x0002CB74
		public string Language { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0002DB7D File Offset: 0x0002CB7D
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x0002DB85 File Offset: 0x0002CB85
		public string InPlaceHoldIdentity { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0002DB8E File Offset: 0x0002CB8E
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x0002DB96 File Offset: 0x0002CB96
		public string ItemHoldPeriod { get; set; }
	}
}
