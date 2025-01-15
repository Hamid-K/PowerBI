using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002AF RID: 687
	public abstract class AlternateIdBase : ISelfValidate, IJsonSerializable
	{
		// Token: 0x06001884 RID: 6276 RVA: 0x000431D8 File Offset: 0x000421D8
		internal AlternateIdBase()
		{
		}

		// Token: 0x06001885 RID: 6277 RVA: 0x000431E0 File Offset: 0x000421E0
		internal AlternateIdBase(IdFormat format)
			: this()
		{
			this.Format = format;
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000431EF File Offset: 0x000421EF
		// (set) Token: 0x06001887 RID: 6279 RVA: 0x000431F7 File Offset: 0x000421F7
		public IdFormat Format { get; set; }

		// Token: 0x06001888 RID: 6280
		internal abstract string GetXmlElementName();

		// Token: 0x06001889 RID: 6281 RVA: 0x00043200 File Offset: 0x00042200
		internal virtual void WriteAttributesToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteAttributeValue("Format", this.Format);
		}

		// Token: 0x0600188A RID: 6282 RVA: 0x00043218 File Offset: 0x00042218
		internal virtual void LoadAttributesFromXml(EwsServiceXmlReader reader)
		{
			this.Format = reader.ReadAttributeValue<IdFormat>("Format");
		}

		// Token: 0x0600188B RID: 6283 RVA: 0x0004322B File Offset: 0x0004222B
		internal virtual void LoadAttributesFromJson(JsonObject responseObject)
		{
			this.Format = responseObject.ReadEnumValue<IdFormat>("Format");
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x0004323E File Offset: 0x0004223E
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Types, this.GetXmlElementName());
			this.WriteAttributesToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0600188D RID: 6285 RVA: 0x0004325C File Offset: 0x0004225C
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.InternalToJson(jsonObject);
			return jsonObject;
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00043277 File Offset: 0x00042277
		internal virtual void InternalToJson(JsonObject jsonObject)
		{
			jsonObject.Add("Format", this.Format);
			jsonObject.AddTypeParameter(this.GetXmlElementName());
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x0004329B File Offset: 0x0004229B
		internal virtual void InternalValidate()
		{
		}

		// Token: 0x06001890 RID: 6288 RVA: 0x0004329D File Offset: 0x0004229D
		void ISelfValidate.Validate()
		{
			this.InternalValidate();
		}
	}
}
