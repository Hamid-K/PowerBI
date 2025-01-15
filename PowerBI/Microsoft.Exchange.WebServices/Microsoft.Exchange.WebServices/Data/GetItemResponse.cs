using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200016C RID: 364
	public sealed class GetItemResponse : ServiceResponse
	{
		// Token: 0x060010BB RID: 4283 RVA: 0x00031398 File Offset: 0x00030398
		internal GetItemResponse(Item item, PropertySet propertySet)
		{
			this.item = item;
			this.propertySet = propertySet;
			EwsUtilities.Assert(this.propertySet != null, "GetItemResponse.ctor", "PropertySet should not be null");
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000313CC File Offset: 0x000303CC
		internal override void ReadElementsFromXml(EwsServiceXmlReader reader)
		{
			base.ReadElementsFromXml(reader);
			List<Item> list = reader.ReadServiceObjectsCollectionFromXml<Item>("Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), true, this.propertySet, false);
			this.item = list[0];
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00031410 File Offset: 0x00030410
		internal override void ReadElementsFromJson(JsonObject responseObject, ExchangeService service)
		{
			base.ReadElementsFromJson(responseObject, service);
			List<Item> list = new EwsServiceJsonReader(service).ReadServiceObjectsCollectionFromJson<Item>(responseObject, "Items", new GetObjectInstanceDelegate<Item>(this.GetObjectInstance), true, this.propertySet, false);
			this.item = list[0];
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00031458 File Offset: 0x00030458
		private Item GetObjectInstance(ExchangeService service, string xmlElementName)
		{
			if (this.Item != null)
			{
				return this.Item;
			}
			return EwsUtilities.CreateEwsObjectFromXmlElementName<Item>(service, xmlElementName);
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x00031470 File Offset: 0x00030470
		public Item Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040009C4 RID: 2500
		private Item item;

		// Token: 0x040009C5 RID: 2501
		private PropertySet propertySet;
	}
}
