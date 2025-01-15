using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000151 RID: 337
	[EditorBrowsable(1)]
	internal abstract class CreateItemResponseBase : ServiceResponse
	{
		// Token: 0x06001047 RID: 4167
		internal abstract Item GetObjectInstance(ExchangeService service, string xmlElementName);

		// Token: 0x06001048 RID: 4168 RVA: 0x0002FADC File Offset: 0x0002EADC
		internal CreateItemResponseBase()
		{
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002FAE4 File Offset: 0x0002EAE4
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			this.items = reader.ReadServiceObjectsCollectionFromXml<Item>("Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002FB0E File Offset: 0x0002EB0E
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			this.items = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Item>(responseObject, "Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), false, null, false);
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x0002FB3F File Offset: 0x0002EB3F
		public List<Item> Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x04000996 RID: 2454
		private List<Item> items;
	}
}
