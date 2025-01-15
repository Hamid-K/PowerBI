using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000179 RID: 377
	public class MarkAsJunkResponse : ServiceResponse
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x00031A08 File Offset: 0x00030A08
		internal MarkAsJunkResponse()
		{
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00031A10 File Offset: 0x00030A10
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			reader.Read();
			if (reader.IsStartElement(XmlNamespace.Messages, "MovedItemId"))
			{
				this.MovedItemId = new ItemId();
				this.MovedItemId.LoadFromXml(reader, XmlNamespace.Messages, "MovedItemId");
				reader.ReadEndElementIfNecessary(XmlNamespace.Messages, "MovedItemId");
			}
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00031A61 File Offset: 0x00030A61
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			if (responseObject.ContainsKey("Token"))
			{
				this.MovedItemId = new ItemId();
				this.MovedItemId.LoadFromJson(responseObject.ReadAsJsonObject("MovedItemId"), service);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x00031A9A File Offset: 0x00030A9A
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00031AA2 File Offset: 0x00030AA2
		public ItemId MovedItemId { get; private set; }
	}
}
